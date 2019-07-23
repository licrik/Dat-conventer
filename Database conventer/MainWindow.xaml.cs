using Database_conventer.CLass;
using Microsoft.Win32;
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

namespace Database_conventer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Windows.LogsWindows logs = new Windows.LogsWindows();
        private string source_file;
        private string destination_file;
        public MainWindow()
        {
            InitializeComponent();
            Windows.LogsWindows.Write_logs("[" + DateTime.Now.ToString("HH:mm:ss") +  "] Star session");
        }

        private void Source_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Database (*.dat)|*.dat";
            openFileDialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            if (openFileDialog.ShowDialog() == true)
            {
                source_file = openFileDialog.FileName;
                this.source_label.Content = source_file;
            }
        }

        private void Destination_Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Database (*.sdf)|*.sdf";
            saveFileDialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            if (saveFileDialog.ShowDialog() == true)
            {
                destination_file = saveFileDialog.SafeFileName;
                this.destination_label.Content = destination_file;
            }
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Logs_Button_Click(object sender, RoutedEventArgs e)
        {
            logs.Show();
        }

        private void Transform_button_Click(object sender, RoutedEventArgs e)
        {
            CLass.ReaderFile file = new CLass.ReaderFile(source_file ,destination_file);
        }

    }
}
