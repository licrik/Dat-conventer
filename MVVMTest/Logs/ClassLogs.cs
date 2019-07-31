using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTest.Logs
{
    public class ClassLogs : INotifyPropertyChanged
    {
        private string text;
        private string date;

        public string Text
        {
            get { return date + " | " + text; }
            set
            {
                text = value;
                date = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");

                OnPropertyChanged("Text");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public ClassLogs(string value)
        {
            Text = value;
            WriteToFile();
        }

        private void WriteToFile()
        {
            using (StreamWriter stream = new StreamWriter("Logs.txt", true))
            {
                stream.WriteLineAsync(date + " | " + text);
            }
        }
    }
}
