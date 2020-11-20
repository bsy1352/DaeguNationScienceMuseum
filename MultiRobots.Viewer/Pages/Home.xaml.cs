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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MultiRobots.Viewer.Pages
{
    /// <summary>
    /// Home.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Home : UserControl
    {
        MediaElement m ;
        Uri myuri;

        public Home()
        {
            InitializeComponent();
            m = new MediaElement();
            myuri = new Uri("C:\\introduction.wmv");

            this.myGrid.Children.Add(m);

            m.LoadedBehavior = MediaState.Manual;
            m.MediaEnded += new RoutedEventHandler(mediaElement_OnMediaEnded);
            m.Source = myuri;
            m.Play();
        }

        private void mediaElement_OnMediaEnded(object sender, RoutedEventArgs e)
        {
            m.Position = new TimeSpan(0, 0, 1);
            m.Play();
        }
    }
}
