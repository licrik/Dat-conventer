using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataConventer.Logs
{
    public class LogsViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ClassLogs> textList;
      
        public ObservableCollection<ClassLogs> TextList
        {
            get
            {
                return textList;
            }

            set
            {
                textList = value;
                OnPropertyChanged("TextList");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void AddTextToLogs(string text)
        {
            App.Current.Dispatcher.Invoke(
                (Action)delegate
                {
                    TextList.Add(new ClassLogs(text));
                }
                );
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
