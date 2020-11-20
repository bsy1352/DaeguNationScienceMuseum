using MultiRobots.Manager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiRobots.Test
{
    class Program
    {
        static CancellationTokenSource cts;
        static RobotManager robotManager;

        static void Main(string[] args)
        {
            robotManager = new RobotManager();
            robotManager.Open("cifX0", 0);
            Thread.Sleep(1000);

            robotManager.Reset();
            Debug.WriteLine("All robot reset");

            //while (true)
            //{
            //    // 모든 로봇이 모터 온이 되었다면
            //    if (robotManager.IsMotorOn(Robots.R1)
            //        && robotManager.IsMotorOn(Robots.R2)
            //        && robotManager.IsMotorOn(Robots.R3))
            //    {
            //        Debug.WriteLine("All robot's motor is on");
            //        break;
            //    }

            //    if (!robotManager.IsMotorOn(Robots.R1))
            //    {
            //        robotManager.MotorOn(Robots.R1);
            //        Thread.Sleep(500);
            //        robotManager.Reset(Robots.R1);

            //        Debug.WriteLine("R1 motor on send");
            //    }

            //    if (!robotManager.IsMotorOn(Robots.R2))
            //    {
            //        robotManager.MotorOn(Robots.R2);
            //        Thread.Sleep(500);
            //        robotManager.Reset(Robots.R2);

            //        Debug.WriteLine("R2 motor on send");
            //    }

            //    if (!robotManager.IsMotorOn(Robots.R3))
            //    {
            //        robotManager.MotorOn(Robots.R3);
            //        Thread.Sleep(500);
            //        robotManager.Reset(Robots.R3);

            //        Debug.WriteLine("R3 motor on send");
            //    }

            //    Thread.Sleep(1000);
            //}

            //StepType stepType = StepType.Full;

            //// 프로그램 밀어넣기
            //if (robotManager.SetProgram())
            //{
            //    Thread.Sleep(500);

            //    // 오더 밀어넣기
            //    robotManager.SendOrder(Robots.R1, stepType);
            //    Thread.Sleep(500);
            //    robotManager.SendOrder(Robots.R2, stepType);
            //    Thread.Sleep(500);
            //    robotManager.SendOrder(Robots.R3, stepType);
            //}

            //Thread.Sleep(1000);

            //robotManager.RobotRun();

            //if (robotManager.RestartOn(Robots.R1)) robotManager.RobotRunReset(Robots.R1);
            //if (robotManager.RestartOn(Robots.R2)) robotManager.RobotRunReset(Robots.R2);
            //if (robotManager.RestartOn(Robots.R3)) robotManager.RobotRunReset(Robots.R3);

            cts = new CancellationTokenSource();
            CheckError(cts.Token);
            ReadTrigger(cts.Token);

            Console.ReadLine();
        }

        static void CheckError(CancellationToken token)
        {
            Task.Run(() => 
            {
                while (true)
                {
                    if (token.IsCancellationRequested) break;

                    if (robotManager.IsError)
                    {
                        //robotManager.RobotPause(Robots.R1);
                        //Thread.Sleep(10);
                        //robotManager.RobotPause(Robots.R2);
                        //Thread.Sleep(10);
                        //robotManager.RobotPause(Robots.R3);
                        //Thread.Sleep(10);
                        //robotManager.Reset();

                        Debug.WriteLine("Is Error()");
                    }

                    Thread.Sleep(500);
                }
            });
        }

        static void ReadTrigger(CancellationToken token)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    if (token.IsCancellationRequested) break;

                    int trigger = robotManager.GetTriggerSignal();
                    Debug.WriteLine("Trigger = {0}", trigger);

                    Thread.Sleep(500);
                }
            });
        }

        static void InterlockR1(CancellationToken token)
        {
            Task.Run(() => 
            {
                while (true)
                {
                    if (token.IsCancellationRequested) break;

                    robotManager.SetInterLock(Robots.R2
                        , 1
                        , robotManager.IsInterLock(Robots.R1, 1) ? true : false);

                    robotManager.SetInterLock(Robots.R3
                        , 3
                        , robotManager.IsInterLock(Robots.R1, 3) ? true : false);

                    Thread.Sleep(100);
                }
            });
        }        

        static void InterlockR2(CancellationToken token)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    if (token.IsCancellationRequested) break;

                    robotManager.SetInterLock(Robots.R1
                        , 1
                        , robotManager.IsInterLock(Robots.R2, 1) ? true : false);

                    robotManager.SetInterLock(Robots.R3
                        , 2
                        , robotManager.IsInterLock(Robots.R2, 2) ? true : false);

                    Thread.Sleep(100);
                }
            });
        }

        static void InterlockR3(CancellationToken token)
        {
            Task.Run(() =>
            {
                while (true)
                {
                    if (token.IsCancellationRequested) break;

                    robotManager.SetInterLock(Robots.R2
                        , 2
                        , robotManager.IsInterLock(Robots.R3, 2) ? true : false);

                    robotManager.SetInterLock(Robots.R1
                        , 3
                        , robotManager.IsInterLock(Robots.R3, 3) ? true : false);

                    Thread.Sleep(100);
                }
            });
        }
    }
}