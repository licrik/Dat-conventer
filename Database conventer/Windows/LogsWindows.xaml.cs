using System;
using System.Collections.Generic;
using System.IO;
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

namespace Database_conventer.Windows
{
    /// <summary>
    /// Логика взаимодействия для LogsWindows.xaml
    /// </summary>
    public partial class LogsWindows : Window
    {
        static public void Write_logs(string text)
        {
            using (StreamWriter wr = new StreamWriter("logs.txt"))
            {
                wr.WriteLineAsync(text);

                wr.Close();
            }
        }

        public LogsWindows()
        {
            InitializeComponent();

            Write_text();
        }

        private void Write_text()
        {
            using (StreamReader st = new StreamReader("logs.txt"))
            {
                while (!st.EndOfStream)
                {
                    this.logs_textbox.Document.Blocks.Add(new Paragraph(new Run(st.ReadLine())));
                }
            }
        }

        private void Clear_logs_button_Click(object sender, RoutedEventArgs e)
        {
            this.logs_textbox.Document.Blocks.Clear();

            Write_text();
        }

        private void Close_window_logs_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
