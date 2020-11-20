using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MultiRobots.Viewer.Pages
{
    /// <summary>
    /// login.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class login : Window
    {

        MainWindow parentWindow;

        Settings settings;

        private System.Windows.Forms.Timer tmr;

        public login(MainWindow main)
        {
            parentWindow = main;
            InitializeComponent();

            tmr = new System.Windows.Forms.Timer();
            tmr.Tick += delegate {
                this.Close();
            };
            tmr.Interval = (int)TimeSpan.FromMinutes(0.15).TotalMilliseconds;
            tmr.Start();
        }

        private void Button_Click_No1(object sender, RoutedEventArgs e)
        {
            Button button;

            button = (Button)sender;
            String number = button.Content.ToString();
            PasswordBox.Password = PasswordBox.Password + number;


        }



        private void Button_Click_Enter(object sender, RoutedEventArgs e)
        {

            String passing_code1 = "2019";
            


            try
            {
                while (true)
                {
                    if (PasswordBox.Password == "" || PasswordBox.Password.Length > 4)
                    {

                        AutoClosingMessageBox.Show("비밀번호 길이 초과 혹은 입력되지 않았습니다.","Empty or Exceeded", 2000);
                        PasswordBox.Password = "";
                        return;
                    }
                    else if (PasswordBox.Password == passing_code1)
                    {


                        settings = new Settings(parentWindow);

                        parentWindow.frame_content.NavigationService.Navigate(settings);

                        parentWindow.btnHome.Visibility = Visibility.Visible;
                        this.Close();
                        break;


                    }
                    



                    else
                    {
                        PasswordBox.Password = "";
                        AutoClosingMessageBox.Show("비밀번호가 틀렸습니다. 다시 입력하여 주세요","Wrong Input", 2000);
                        return;



                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            PasswordBox.Password = PasswordBox.Password.Remove(PasswordBox.Password.Length - 1);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            PasswordBox.Password = "";
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
