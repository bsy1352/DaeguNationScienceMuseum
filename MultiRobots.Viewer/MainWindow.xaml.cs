using log4net;
using MultiRobots.Manager;
using MultiRobots.Viewer.Pages;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MultiRobots.Viewer
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private CancellationTokenSource cts;

        private RobotManager robotManager;

        DispatcherTimer timer;

        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //private bool _loadCompleted = false;
        //private bool _started = false;
        private bool _setting = false;
        public bool Setting { get { return _setting; } set { _setting = value; } }
        public StepType LastStepType { get; set; } = StepType.None;
        public bool InfinitLoop { get; set; } = false;
        public int Location { get; set; } = 0;

        BitmapImage bitmapImagePlay = new BitmapImage(new Uri("Assets/Images/play-circle-outline.png", UriKind.Relative));
        BitmapImage bitmapImageStop = new BitmapImage(new Uri("Assets/Images/stop-circle-outline.png", UriKind.Relative));

        BitmapImage bitmapLogo0 = new BitmapImage(new Uri("Assets/Images/logo.png", UriKind.Relative));     // 대구과학관 로고
        BitmapImage bitmapLogo1 = new BitmapImage(new Uri("Assets/Images/logo2.png", UriKind.Relative));    // 창원전시회 로고

        Image btnStateImage;
        TextBlock txtState;
        TextBlock txtWheel;
        TextBlock txtDoor;
        TextBlock txtBonnet;

        Assembly assembly;
        Settings settings;

        public static String a;

        public MainWindow()
        {
            InitializeComponent();
            LoadControlsByLocation();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                cts = new CancellationTokenSource();

                robotManager = new RobotManager();
                uint ret = robotManager.Open("cifX0", 0);

                assembly = new Assembly();
                settings = new Settings(this);

                RobotStatus(cts.Token);
                DisplayAssembly(cts.Token);
                GetStep(cts.Token);
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("프로그램 실행 중 에러가 발생하였습니다.","Error",2000);
                logger.Error(ex);
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cts != null)
                {
                    cts.Cancel();
                    cts = null;
                }

                robotManager?.Close();
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        public void LoadControlsByLocation()
        {
            try
            {
                Location = Config.GetLocation();
                if (Location == 0)
                {
                    imgLogo.Source = bitmapLogo0;

                    btnState_bonnet.Visibility = Visibility.Visible;
                    btnState_wheel.Visibility = Visibility.Visible;
                    btnState_door.Visibility = Visibility.Visible;

                    frame_content.Source = new Uri("Pages/Home.xaml", UriKind.Relative);
                }
                else
                {
                    imgLogo.Source = bitmapLogo1;

                    btnState_bonnet.Visibility = Visibility.Hidden;
                    btnState_wheel.Visibility = Visibility.Hidden;
                    btnState_door.Visibility = Visibility.Hidden;

                    frame_content.Source = new Uri("Pages/Home3.xaml", UriKind.Relative);
                }
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Home button event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            _setting = false;
            frame_content.Source = (Location == 0) ? new Uri("Pages/Home.xaml", UriKind.Relative) : new Uri("Pages/Home3.xaml", UriKind.Relative);
            btnHome.IsEnabled = false;
            btnHome.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Setting button event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 


        private void BtnSetting_Click(object sender, RoutedEventArgs e)
        {
            _setting = true;
            login login = new login(this);
            btnHome.IsEnabled = true;
            login.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            login.ShowDialog();
            /*_setting = true;
            settings.ParentWindow = this;
            frame_content.NavigationService.Navigate(settings);
            btnHome.IsEnabled = true;
            btnHome.Visibility = Visibility.Visible;*/
        }

        /// <summary>
        /// Power button event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPower_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();        
        }

        /// <summary>
        /// Start/Stop button event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnState_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SendStep(StepType.Full);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnWheel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SendStep(StepType.Wheel);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnBonnet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SendStep(StepType.Bonnet);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnDoor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SendStep(StepType.Door);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 현재 작업중인 스탭을 가져온다.
        /// </summary>
        /// <param name="token"></param>
        void GetStep(CancellationToken token)
        {
            Task.Factory.StartNew(() => 
            {
                StepType beforeStep = StepType.None;

                while (true)
                {
                    if (token.IsCancellationRequested) break;

                    try
                    {
                        StepType stepType;

                        if (robotManager.IsConnected)
                        {
                            stepType = robotManager.GetCurrentStep();
                            if (stepType == StepType.None && LastStepType == StepType.Full && InfinitLoop)
                            {
                                SendStep(StepType.Full);
                            }
                        }
                        else
                        {
                            stepType = StepType.None;
                        }

                        if (beforeStep != stepType)
                        {
                            logger.Debug(string.Format("current step = {0}", stepType.ToString()));

                            SetButtonProperty(stepType);
                            beforeStep = stepType;
                        }
                    }
                    catch (Exception ex)
                    {
                        AutoClosingMessageBox.Show("작업 실행 중 에러가 발생하였습니다.", "Error", 2000);
                        logger.Error(ex);
                    }

                    Thread.Sleep(100);
                }
            });           
        }

        /// <summary>
        /// 오더 넣기
        /// </summary>
        /// <param name="stepType"></param>
        void SendStep(StepType stepType)
        { 
            try
            {
                if (robotManager.IsConnected)
                {
                    if (!robotManager.IsMotorOn())
                    {
                        AutoClosingMessageBox.Show("모든 로봇의 모터가 정상적으로 기동되지 않았습니다.", "Error", 2000);
                        return;
                    }

                    if (robotManager.IsReadyCommand())
                    {
                        if (robotManager.SetProgram())
                        {
                            Thread.Sleep(500);

                            SetStep(stepType);
                            SetButtonProperty(stepType);

                            robotManager.RobotRun();

                            if (robotManager.RestartOn(Robots.R1)) robotManager.RobotRunReset(Robots.R1);
                            if (robotManager.RestartOn(Robots.R2)) robotManager.RobotRunReset(Robots.R2);
                            if (robotManager.RestartOn(Robots.R3)) robotManager.RobotRunReset(Robots.R3);

                            
                            LastStepType = stepType;

                            // 2초후 스탭 초기화 (연속 동작 방지) => 테스트 필요
                            Thread.Sleep(2000);
                            SetStep(StepType.None);
                        }
                    }
                    else
                    {
                        AutoClosingMessageBox.Show("현재 작업 진행중 입니다.\r\n작업 종료 후 다시 실행하여 주세요!","Error",2000);
                    }
                }
            }
            catch (Exception ex)
            {
                AutoClosingMessageBox.Show("작업 실행 중 에러가 발생하였습니다.", "Error", 2000);
                logger.Error(ex);
            }
        }

        /// <summary>
        /// 스탭 설정
        /// </summary>
        /// <param name="stepType"></param>
        void SetStep(StepType stepType)
        {
            try
            {
                // 오더 밀어넣기
                robotManager.SendOrder(Robots.R1, stepType);
                Thread.Sleep(100);
                robotManager.SendOrder(Robots.R2, stepType);
                Thread.Sleep(100);
                robotManager.SendOrder(Robots.R3, stepType);
                Thread.Sleep(100);
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        /// <summary>
        /// 로봇의 상태를 가져오고 에러를 확인 한다.
        /// </summary>
        /// <param name="token"></param>
        void RobotStatus(CancellationToken token)
        {
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    try
                    {
                        if (token.IsCancellationRequested)
                            break;

                        string status = robotManager.GetStatus();
                        if (!string.IsNullOrEmpty(status))
                        {
                            settings.DisplayRobotStatus(status);

                            //logger.Debug(string.Format("robot has error = {0}", robotManager.IsError));
                            if (robotManager.IsError)
                            {
                                //RobotPause();
                            }
                        }

                        status = "";
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                    }

                    Thread.Sleep(100);
                }
            });
        }

        public void RobotPause()
        {
            robotManager.RobotPause(Robots.R1);
            Thread.Sleep(100);
            robotManager.RobotPause(Robots.R2);
            Thread.Sleep(100);
            robotManager.RobotPause(Robots.R3);
            Thread.Sleep(100);

            robotManager.Reset();
        }

        public void RobotRun()
        {
            robotManager.RobotRun(Robots.R1);
            Thread.Sleep(100);
            robotManager.RobotRun(Robots.R2);
            Thread.Sleep(100);
            robotManager.RobotRun(Robots.R3);
            Thread.Sleep(100);

            robotManager.Reset();
        }

        public void AlarmRelease()
        {
            robotManager.AlarmRelease();
        }

        /// <summary>
        /// Display vision picture by trigger index
        /// </summary>
        /// <param name="triggerIndex"></param>
        void DisplayAssembly(CancellationToken token)
        {
            Task.Factory.StartNew(() => 
            {
                    while (true)
                    {
                        try
                        {
                            if (token.IsCancellationRequested) break;

                            int triggerR1 = robotManager.GetTriggerSignal(Robots.R1);
                            int triggerR3 = robotManager.GetTriggerSignal(Robots.R3);
                            logger.DebugFormat("R1 Trigger = {0}, R3 Trigger = {1}", triggerR1, triggerR3);

                            Dispatcher.BeginInvoke((Action)(() =>
                            {
                                if (!_setting)
                                {
                                    if (triggerR3 == 5 || triggerR3 == 9)
                                    {
                                        frame_content.NavigationService.Navigate(assembly);
                                        assembly.RaiseTriggerSignal(triggerR3);
                                    }
                                    else
                                    {
                                        if (triggerR1 == 0)
                                        {
                                            assembly.RaiseTriggerSignal(triggerR1);
                                        }
                                        else if (triggerR1 > 0 && triggerR1 <= 9)
                                        {
                                            frame_content.NavigationService.Navigate(assembly);
                                            assembly.RaiseTriggerSignal(triggerR1);
                                        }
                                    }

                                }
                            }));
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex.Message);
                        }

                    Thread.Sleep(1000);
                }                
            });
        }

        /// <summary>
        /// 해당 로봇 모터 온
        /// </summary>
        /// <param name="robot"></param>
        public void MotorOn(Robots robot)
        { 
            try
            {
                if (!robotManager.IsMotorOn(robot))
                {
                    robotManager.MotorOn(robot);
                    Thread.Sleep(500);
                    robotManager.Reset(robot);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        /// <summary>
        /// 스탭에 따라 버튼 속성을 변경한다.
        /// </summary>
        /// <param name="stepType"></param>
        private void SetButtonProperty(StepType stepType)
        {
            try
            {
                //if (LastStepType == stepType) return;

                //Debug.WriteLine(string.Format("{0} - SetButtonProperty", DateTime.Now));

                Dispatcher.BeginInvoke((Action)(() =>
                {
                    btnStateImage = FindVisualChildByName<Image>(btnState, "imgStartStop");                    
                    txtState = FindVisualChildByName<TextBlock>(btnState, "txtStartStop");
                    txtWheel = FindVisualChildByName<TextBlock>(btnState_wheel, "txtStartStop");
                    txtDoor = FindVisualChildByName<TextBlock>(btnState_door, "txtStartStop");
                    txtBonnet = FindVisualChildByName<TextBlock>(btnState_bonnet, "txtStartStop");

                    if (stepType != StepType.None)
                    {
                        txtState.Text = (stepType == StepType.Full) ? "작업 진행 중" : "모든 조립 공정";                        
                        txtState.Foreground = (stepType == StepType.Full) ? Brushes.White : Brushes.Black;
                        btnState.Background = (stepType == StepType.Full) ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00AB56")) : new SolidColorBrush(Colors.Gray);

                        txtBonnet.Text = (stepType == StepType.Bonnet) ? "작업 진행 중" : "보닛 조립 공정";
                        txtBonnet.Foreground = (stepType == StepType.Bonnet) ? Brushes.White : Brushes.Black;
                        btnState_bonnet.Background = (stepType == StepType.Bonnet) ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00AB56")) : new SolidColorBrush(Colors.Gray);

                        txtWheel.Text = (stepType == StepType.Wheel) ? "작업 진행 중" : "바퀴 조립 공정";
                        txtWheel.Foreground = (stepType == StepType.Wheel) ? Brushes.White : Brushes.Black;
                        btnState_wheel.Background = (stepType == StepType.Wheel) ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00AB56")) : new SolidColorBrush(Colors.Gray);

                        txtDoor.Text = (stepType == StepType.Door) ? "작업 진행 중" : "차문 조립 공정";
                        txtDoor.Foreground = (stepType == StepType.Door) ? Brushes.White : Brushes.Black;
                        btnState_door.Background = (stepType == StepType.Door) ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00AB56")) : new SolidColorBrush(Colors.Gray);

                        btnState.IsEnabled = false;
                        btnState_bonnet.IsEnabled = false;
                        btnState.IsEnabled = false;
                        btnState_wheel.IsEnabled = false;
                        btnState_door.IsEnabled = false;

                        if (!_setting) frame_content.NavigationService.Navigate(assembly);
                    }
                    else
                    {
                        txtState.Text = "모든 조립 공정";
                        txtState.Foreground = Brushes.White;
                        btnState.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00AB56"));
                        
                        txtBonnet.Text = "보닛 조립 공정";
                        txtBonnet.Foreground = Brushes.White;
                        btnState_bonnet.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00AB56"));

                        txtWheel.Text = "바퀴 조립 공정";
                        txtWheel.Foreground = Brushes.White;
                        btnState_wheel.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00AB56"));

                        txtDoor.Text = "차문 조립 공정";
                        txtDoor.Foreground = Brushes.White;
                        btnState_door.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00AB56"));

                        btnState_bonnet.IsEnabled = true;
                        btnState.IsEnabled = true;
                        btnState_wheel.IsEnabled = true;
                        btnState_door.IsEnabled = true;

                        if (!_setting) frame_content.Source = (Location == 0) ? new Uri("Pages/Home.xaml", UriKind.Relative) : new Uri("Pages/Home3.xaml", UriKind.Relative);
                    }
                }));

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                //MessageBox.Show(ex.Message);
            }
        }


        private T FindVisualChildByName<T>(DependencyObject parent, string name) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                string controlName = child.GetValue(Control.NameProperty) as string;

                if (controlName == name)
                {
                    return child as T;
                }
                else
                {
                    T result = FindVisualChildByName<T>(child, name);

                    if (result != null)

                        return result;
                }
            }

            return null;
        }

        public void Recover_EmergencyBit1(Robots robot)
        {
            try
            {

                robotManager.Stop_recovery0(robot);
                Thread.Sleep(500);
                robotManager.Reset(robot);

            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        public void Recover_EmergencyBit2(Robots robot)
        {
            try
            {

                robotManager.Stop_recovery1(robot);
                Thread.Sleep(500);
                robotManager.Reset(robot);

            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        public void Recover_EmergencyBit3(Robots robot)
        {
            try
            {

                robotManager.Stop_recovery2(robot);
                Thread.Sleep(500);
                robotManager.Reset(robot);

            }
            catch (Exception ex)
            {
                logger.Error(ex);
            }
        }

        public class AutoClosingMessageBox
        {
            System.Threading.Timer _timeoutTimer;
            string _caption;
            AutoClosingMessageBox(string text, string caption, int timeout)
            {
                _caption = caption;
                _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                    null, timeout, System.Threading.Timeout.Infinite);
                MessageBox.Show(text, caption);
            }

            public static void Show(string text, string caption, int timeout)
            {
                new AutoClosingMessageBox(text, caption, timeout);
            }

            void OnTimerElapsed(object state)
            {
                IntPtr mbWnd = FindWindow(null, _caption);
                if (mbWnd != IntPtr.Zero)
                    SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                _timeoutTimer.Dispose();
            }
            const int WM_CLOSE = 0x0010;
            [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
            static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
            [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
            static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        }
    }
}
