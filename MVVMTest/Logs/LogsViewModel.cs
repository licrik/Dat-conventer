using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MVVMTest.Logs
{
    public class LogsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ClassLogs> TextList { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void AddTextToLogs(string text)
        {
            try
            {
                TextList.Add(new ClassLogs(text));
            }
            catch
            {

            }
        }

        public LogsViewModel()
        {
            TextList = new ObservableCollection<ClassLogs>
            {
                new ClassLogs("Start session.")
            };
        }
    }
}
