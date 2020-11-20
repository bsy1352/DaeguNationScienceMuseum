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
using log4net;

namespace MultiRobots.Viewer.Pages
{
    /// <summary>
    /// Assembly.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Assembly : UserControl
    {
        ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Assembly()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            canvas.Background = new SolidColorBrush(Colors.Transparent);
        }

        public void RaiseTriggerSignal(int triggerIndex)
        {
            try
            {
                if (triggerIndex == 0)
                {
                    canvas.Background = new SolidColorBrush(Colors.Black);
                }
                else
                {
                    BitmapImage explanation
                        = new BitmapImage(new Uri(string.Format("Assets/Images/trigger_{0}.bmp", triggerIndex), UriKind.Relative));
                    ImageBrush imageBrush = new ImageBrush(explanation);
                    canvas.Background = imageBrush;

                }

            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }
        }
    }
}
