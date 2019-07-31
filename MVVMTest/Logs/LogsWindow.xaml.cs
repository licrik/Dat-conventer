using System;
using System.Windows;


namespace DataConventer.Logs
{
    /// <summary>
    /// Логика взаимодействия для LogsWindow.xaml
    /// </summary>
    public partial class LogsWindow : Window
    {
        public LogsWindow(LogsViewModel logsView)
        {
            InitializeComponent();
            DataContext = logsView;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
