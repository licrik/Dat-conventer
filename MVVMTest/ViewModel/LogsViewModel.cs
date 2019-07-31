using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataConventer.Logs
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
