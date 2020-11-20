using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MultiRobots.Viewer.Pages
{
    /// <summary>
    /// Settings.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Settings : UserControl
    {
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SolidColorBrush on = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00AB56"));
        private SolidColorBrush off = new SolidColorBrush(Colors.Red);
        private SolidColorBrush normal = new SolidColorBrush(Colors.Gray);
        BitmapImage bitmapImageStop = new BitmapImage(new Uri("Assets/Images/logo2.png", UriKind.Relative));

        MainWindow parentWindow;

        public MainWindow ParentWindow
        {
            get { return parentWindow; }
            set { parentWindow = value; }
        }

        public Settings(MainWindow parents)
        {
            parentWindow = parents;
            InitializeComponent();

            /*int location = Config.GetLocation();

            if (location == 0)
            {
                changwon.IsEnabled = true;
                daegu.IsEnabled = false;
            }
            else
            {
                changwon.IsEnabled = false;
                daegu.IsEnabled = true;
            }

            toggleInfinity.Selected = Config.GetLoopCount() == 0 ? true : false;

            changwon.Background = (location == 0)
                ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#10AC27")) : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3333"));

            daegu.Background = (location == 0)
                ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3333")) : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#10AC27"));*/
        }

        public void DisplayRobotStatus(string status)
        {           
            try
            {
                //logger.DebugFormat("status={0}", status);

                if (!string.IsNullOrEmpty(status))
                {
                    string[] arrStatus = status.Split(new char[] { ',' });

                    char[] r1 = new char[8];
                    char[] r2 = new char[8];
                    char[] r3 = new char[8];

                    if (arrStatus.Length == 3)
                    {
                        r1 = arrStatus[0].ToCharArray();
                        r2 = arrStatus[1].ToCharArray();
                        r3 = arrStatus[2].ToCharArray();
                    }

                    Dispatcher.BeginInvoke((Action)(() =>
                    {
                        R1Error.Fill = (r1[5] == '1') ? on : off;
                        btnR1MotorOn.Background = r1[0] == '1' ? on : normal;
                        //btnR1Run.Background = r1[1] == '1' ? on : normal;
                        //btnR1Pause.Background = r1[2] == '1' ? on : normal;
                        //btnR1AlarmReset.Background = r1[3] == '1' ? on : normal;

                        R2Error.Fill = r2[5] == '1' ? on : off;
                        btnR2MotorOn.Background = r2[0] == '1' ? on : normal;
                        //btnR2Run.Background = r2[1] == '1' ? on : normal;
                        //btnR2Pause.Background = r2[2] == '1' ? on : normal;
                        //btnR2AlarmReset.Background = r2[3] == '1' ? on : normal;

                        R3Error.Fill = r3[5] == '1' ? on : off;
                        btnR3MotorOn.Background = r3[0] == '1' ? on : normal;
                        //btnR3Run.Background = r3[1] == '1' ? on : normal;
                        //btnR3Pause.Background = r3[2] == '1' ? on : normal;
                        //btnR3AlarmReset.Background = r3[3] == '1' ? on : normal;
                    }));
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }

        private void BtnMotorOn_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int tag = int.Parse(button.Tag.ToString());

            if (parentWindow != null)
            {
                parentWindow.MotorOn((Manager.Robots)tag);
            }
        }


        private void BtnPause_Click(object sender, RoutedEventArgs e)
        {
            if (parentWindow != null)
            {
                parentWindow.RobotPause();
            }
        }

        private void BtnAlarmReset_Click(object sender, RoutedEventArgs e)
        {
            if (parentWindow != null)
            {
                parentWindow.AlarmRelease();
            }            
        }


        private void ToggleInfinity_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ToggleButton button = (ToggleButton)sender;
                if (button.Selected)
                {
                    parentWindow.InfinitLoop = true;
                    Config.SetLoopCountToFile(0);
                }
                else
                {
                    parentWindow.InfinitLoop = false;
                    Config.SetLoopCountToFile(1);
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.ToString());
            }
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            if (parentWindow != null)
            {
                parentWindow.RobotRun();
            }
        }

        private void changwon_Click(object sender, RoutedEventArgs e)
        {
            LoadControlsByLocation(1);
        }

        private void daegu_Click(object sender, RoutedEventArgs e)
        {
            LoadControlsByLocation(0);
        }

        private void LoadControlsByLocation(int location)
        {
            Config.SetLocationStepToFile(location);

            parentWindow.Setting = false;
            parentWindow.LoadControlsByLocation();
            parentWindow.btnHome.Visibility = Visibility.Hidden;

            parentWindow.btnState_bonnet.Visibility = (location == 0) ? Visibility.Visible : Visibility.Hidden;
            parentWindow.btnState_wheel.Visibility = (location == 0) ? Visibility.Visible : Visibility.Hidden;
            parentWindow.btnState_door.Visibility = (location == 0) ? Visibility.Visible : Visibility.Hidden;

            /*daegu.IsEnabled = (location == 0) ? false : true;
            changwon.IsEnabled = (location == 0) ? true : false;

            changwon.Background = (location == 0) 
                ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#10AC27")) : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3333"));

            daegu.Background = (location == 0) 
                ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF3333")) : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#10AC27"));*/
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

        private void btnRecover_Click1(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int tag = int.Parse(button.Tag.ToString());

            if (parentWindow != null)
            {
                parentWindow.Recover_EmergencyBit1((Manager.Robots)tag);

            }
        }

        private void btnRecover_Click2(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int tag = int.Parse(button.Tag.ToString());

            if (parentWindow != null)
            {
                parentWindow.Recover_EmergencyBit2((Manager.Robots)tag);

            }
        }

        private void btnRecover_Click3(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int tag = int.Parse(button.Tag.ToString());

            if (parentWindow != null)
            {
                parentWindow.Recover_EmergencyBit3((Manager.Robots)tag);

            }
        }
    }
}
