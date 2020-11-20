using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace MultiRobots.Net
{
    public class Server
    {
        public delegate void ClientConnectCallback(int clientNumber);
        public delegate void ClientDisconnectCallback(int clientNumber);
        public delegate void ReceiveDataCallback(int clientNumber, byte[] message, int messageSize);

        private ClientConnectCallback _clientConnect = null;
        private ClientDisconnectCallback _clientDisconnect = null;
        private ReceiveDataCallback _receive = null;

        private Socket _mainSocket;
        private Timer _broadcastTimer;
        private int _currentClientNumber = 0;

        public class UserSock
        {
            public UserSock(int nClientID, Socket s)
            {
                _iClientID = nClientID;
                _UserSocket = s;
                _dTimer = DateTime.Now;
                _szStationName = string.Empty;
                _szClientName = string.Empty;
                _UserListentingPort = 9998;
                _szAlternateIP = string.Empty;
                _pingStatClass = new PingStatsClass();
            }

            public int iClientID { get { return _iClientID; } }
            public Socket UserSocket { get { return _UserSocket; } }
            public DateTime dTimer { get { return _dTimer; } set { _dTimer = value; } }
            public string szClientName { get { return _szClientName; } set { _szClientName = value; } }
            public string szStationName { get { return _szStationName; } set { _szStationName = value; } }
            public UInt16 UserListentingPort { get { return _UserListentingPort; } set { _UserListentingPort = value; } }
            public string szAlternateIP { get { return _szAlternateIP; } set { _szAlternateIP = value; } }
            public PingStatsClass PingStatClass { get { return _pingStatClass; } set { _pingStatClass = value; } }


            public int ZeroDataCount { get; internal set; }

            private Socket _UserSocket;
            private DateTime _dTimer;
            private int _iClientID;
            private string _szClientName;
            private string _szStationName;
            private UInt16 _UserListentingPort;
            private string _szAlternateIP;
            private PingStatsClass _pingStatClass;
        }

        public Dictionary<int, UserSock> workerSockets = new Dictionary<int, UserSock>();

        public ClientConnectCallback OnClientConnect
        {
            get
            {
                return _clientConnect;
            }
            set
            {
                _clientConnect = value;
            }
        }

        public ClientDisconnectCallback OnClientDisconnect
        {
            get
            {
                return _clientDisconnect;
            }

            set
            {
                _clientDisconnect = value;
            }
        }

        public ReceiveDataCallback OnReceiveData
        {
            get
            {
                return _receive;
            }

            set
            {
                _receive = value;
            }
        }

        public bool IsListening
        {
            get
            {
                if (_mainSocket == null)
                    return false;
                else
                    return _mainSocket.IsBound;
            }
        }

        public void Listen(int listenPort)
        {
            try
            {
                Stop();

                _mainSocket = new Socket(AddressFamily.InterNetwork
                                        , SocketType.Stream
                                        , ProtocolType.Tcp);
                _mainSocket.Bind(new IPEndPoint(IPAddress.Any, listenPort));
                _mainSocket.Listen(100);
                _mainSocket.BeginAccept(new AsyncCallback(OnReceiveConnection), null);
            }
            catch (SocketException)
            {

            }
        }

        public void Stop()
        {
            lock (workerSockets)
            {
                foreach (UserSock s in workerSockets.Values)
                {
                    if (s.UserSocket.Connected)
                        s.UserSocket.Close();
                }
                workerSockets.Clear();
            }

            if (IsListening)
                _mainSocket.Close();
        }

        public void SendMessage(byte[] message, bool testConnections = false)
        {
            if (testConnections)
            {
                List<int> ClientsToRemove = new List<int>();
                foreach (int clientId in workerSockets.Keys)
                {
                    if (workerSockets[clientId].UserSocket.Connected)
                    {
                        try
                        {
                            workerSockets[clientId].UserSocket.Send(message);
                        }
                        catch
                        {
                            ClientsToRemove.Add(clientId);
                        }

                        Thread.Sleep(10);
                    }
                    else
                    {
                        ClientsToRemove.Add(clientId);
                    }
                }

                {
                    if (ClientsToRemove.Count > 0)
                    {
                        foreach (int cID in ClientsToRemove)
                        {
                            if (OnClientDisconnect != null)
                            {
                                OnClientDisconnect(cID);
                            }
                        }
                    }
                }
                ClientsToRemove.Clear();
                ClientsToRemove = null;
            }
            else
            {
                foreach (UserSock s in workerSockets.Values)
                {
                    try
                    {
                        if (s.UserSocket.Connected)
                            s.UserSocket.Send(message);
                    }
                    catch (SocketException)
                    {

                    }
                }
            }
        }

        /// <summary>
        /// Send a message to a specific client.
        /// </summary>
        /// <param name="clientNumber"></param>
        /// <param name="message"></param>
        public void SendMessage(int clientNumber, byte[] message)
        {
            if (!workerSockets.ContainsKey(clientNumber))
            {
                // Invalid Client Number!
                return;
            }
            try
            {
                ((UserSock)workerSockets[clientNumber]).UserSocket.Send(message);
            }
            catch (SocketException)
            {
                // 
            }
        }

        public void BeginBroadcast(byte[] message, int port, int frequency)
        {
            Socket sock = new Socket(AddressFamily.InterNetwork
                                    , SocketType.Dgram
                                    , ProtocolType.Udp);
            sock.EnableBroadcast = true;

            Packet pack = new Packet(sock, port);
            pack.DataBuffer = message;

            if (_broadcastTimer != null)
                _broadcastTimer.Dispose();

            _broadcastTimer = new Timer(new TimerCallback(BroadcastTimerCallback), pack, 0, frequency * 1000);
        }

        public void EndBroadcast()
        {
            if (_broadcastTimer != null)
                _broadcastTimer.Dispose();
        }

        private void BroadcastTimerCallback(object state)
        {
            ((Packet)state).CurrentSocket.SendTo(((Packet)state).DataBuffer, new IPEndPoint(IPAddress.Broadcast, ((Packet)state).ClientNumber));
        }

        private void OnReceiveConnection(IAsyncResult async)
        {
            try
            {
                lock (workerSockets)
                {
                    Interlocked.Increment(ref _currentClientNumber); // Thread Safe
                    UserSock us = new UserSock(_currentClientNumber, _mainSocket.EndAccept(async));
                    workerSockets.Add(_currentClientNumber, us);
                }

                if (_clientConnect != null)
                    _clientConnect(_currentClientNumber);

                WaitForData(_currentClientNumber);
                _mainSocket.BeginAccept(new AsyncCallback(OnReceiveConnection), null);
            }
            catch (ObjectDisposedException)
            {
                System.Console.WriteLine("OnClientConnection: Socket has been closed");
            }
            catch (SocketException se)
            {
                Debug.WriteLine("SERVER EXCEPTION in OnReceiveConnection: " + se.Message);

                if (workerSockets.ContainsKey(_currentClientNumber))
                {
                    Console.WriteLine("RemoteEndPoint: " + workerSockets[_currentClientNumber].UserSocket.RemoteEndPoint.ToString());
                    Console.WriteLine("LocalEndPoint: " + workerSockets[_currentClientNumber].UserSocket.LocalEndPoint.ToString());

                    Console.WriteLine("Closing socket from OnReceiveConnection");
                }

                //Socket gets closed and removed from OnClientDisconnect
                if (OnClientDisconnect != null)
                    OnClientDisconnect(_currentClientNumber);
            }
        }

        private void WaitForData(int clientNumber)
        {
            if (!workerSockets.ContainsKey(clientNumber))
            {
                return;
            }

            try
            {
                Packet pack = new Packet(workerSockets[clientNumber].UserSocket, clientNumber);
                workerSockets[clientNumber].UserSocket.BeginReceive(pack.DataBuffer
                                                                    , 0
                                                                    , pack.DataBuffer.Length
                                                                    , SocketFlags.None
                                                                    , new AsyncCallback(OnDataReceived)
                                                                    , pack);
            }
            catch (SocketException se)
            {
                try
                {
                    if (OnClientDisconnect != null)
                        OnClientDisconnect(clientNumber);

                    Debug.WriteLine($"SERVER EXCEPTION in WaitForClientData: {se.Message}");
                }
                catch { }
            }
            catch (Exception ex)
            {
                // Socket gets closed and removed from OnClientDisconnect
                if (OnClientDisconnect != null)
                    OnClientDisconnect(clientNumber);

                string msg = (ex.InnerException != null)
                            ? ex.InnerException.Message : ex.Message;
                Debug.WriteLine($"SERVER EXCEPTION in WaitForClientData2: {msg}");
            }
        }

        private void OnDataReceived(IAsyncResult async)
        {
            Packet socketData = (Packet)async.AsyncState;

            try
            {
                int dataSize = socketData.CurrentSocket.EndReceive(async);

                if (dataSize.Equals(0))
                {
                    if (workerSockets.ContainsKey(socketData.ClientNumber))
                    {
                        if (((UserSock)workerSockets[socketData.ClientNumber]).ZeroDataCount++ == 10)
                        {
                            if (OnClientDisconnect != null)
                                OnClientDisconnect(socketData.ClientNumber);
                        }
                    }
                }
                else
                {
                    _receive(socketData.ClientNumber, socketData.DataBuffer, dataSize);

                    ((UserSock)workerSockets[socketData.ClientNumber]).ZeroDataCount = 0;
                }

                WaitForData(socketData.ClientNumber);
            }
            catch (ObjectDisposedException)
            {
                Debug.WriteLine("OnDataReceived: Socket has been closed");

                //Socket gets closed and removed from OnClientDisconnect
                if (OnClientDisconnect != null)
                    OnClientDisconnect(socketData.ClientNumber);
            }
            catch (SocketException se)
            {
                //10060 - A connection attempt failed because the connected party did not properly respond after a period of time,
                //or established connection failed because connected host has failed to respond.
                if (se.ErrorCode == 10054 || se.ErrorCode == 10060) //10054 - Error code for Connection reset by peer
                {
                    try
                    {
                        Debug.WriteLine("SERVER EXCEPTION in OnClientDataReceived, ServerObject removed:(" + se.ErrorCode.ToString() + ") " + socketData.ClientNumber + ", (happens during a normal client exit)");
                        Debug.WriteLine("RemoteEndPoint: " + workerSockets[socketData.ClientNumber].UserSocket.RemoteEndPoint.ToString());
                        Debug.WriteLine("LocalEndPoint: " + workerSockets[socketData.ClientNumber].UserSocket.LocalEndPoint.ToString());
                    }
                    catch { }

                    //Socket gets closed and removed from OnClientDisconnect
                    if (OnClientDisconnect != null)
                        OnClientDisconnect(socketData.ClientNumber);

                    Console.WriteLine("Closing socket from OnDataReceived");
                }
                else
                {
                    string mess = "CONNECTION BOOTED for reason other than 10054: code = " + se.ErrorCode.ToString() + ",   " + se.Message;
                    Debug.WriteLine(mess);
                }
            }
        }

        private class Packet
        {
            public Socket CurrentSocket;
            public int ClientNumber;
            public byte[] DataBuffer = new byte[1024];

            public Packet(Socket sock, int client)
            {
                CurrentSocket = sock;
                ClientNumber = client;
            }
        }
    }


    public class PingStatsClass
    {
        public PingStatsClass()
        {
            sw = new Stopwatch();
            PingCounter = 0;
            PingTimeTotal = 0;
            LongestPing = 0;
            LongestPingDateTimeStamp = DateTime.Now;
        }

        private Stopwatch sw = null;

        public Int32 PingCounter;
        public Int64 PingTimeTotal;
        public Int64 LongestPing;

        public DateTime LongestPingDateTimeStamp;

        public Int64 StopTheClock()
        {
            if (sw.IsRunning)
            {
                sw.Stop();
                PingCounter++;

                if (sw.ElapsedMilliseconds > LongestPing)
                {
                    LongestPing = sw.ElapsedMilliseconds;
                    LongestPingDateTimeStamp = DateTime.Now;
                }

                PingTimeTotal += sw.ElapsedMilliseconds;
            }
            return sw.ElapsedMilliseconds;
        }

        public void StartTheClock()
        {
            sw.Reset();

            if (!sw.IsRunning) sw.Start();
            else sw.Restart();
        }

        public Int64 GetElapsedTime
        {
            get { return sw.ElapsedMilliseconds; }
        }
    }
}
