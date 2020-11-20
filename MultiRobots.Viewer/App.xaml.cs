using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows;

namespace MultiRobots.Viewer
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        Mutex _mutex = null; 
        
        protected override void OnStartup(StartupEventArgs e) 
        { 
            string mutexName = "MultiRobots.Viewer"; 
            bool isCreatedNew = false; 
            
            try 
            { 
                _mutex = new Mutex(true, mutexName, out isCreatedNew); 
                if (isCreatedNew) 
                { 
                    base.OnStartup(e); 
                } 
                else 
                { 
                    MessageBox.Show("프로그램이 이미 실행 중 입니다.", "Error", MessageBoxButton.OK, MessageBoxImage.Information); 
                    Application.Current.Shutdown(); 
                } 
            } 
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message + "\n\n" + ex.StackTrace + "\n\n" + "Application Existing...", "Exception thrown"); 
                Application.Current.Shutdown(); 
            } 
        }
    }
}
