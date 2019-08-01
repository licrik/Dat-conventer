using DataConventer.Class;
using DataConventer.Logs;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Input;

namespace DataConventer
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        IDialogService dialogService;
        public Logs.LogsViewModel logsViewModel;
        
        #region Button Activate
        private bool enabled = false;

        public bool Enabled
        {
            get { return enabled; }
            set
            {
                enabled = value;
                OnPropertyChanged("Enabled");
            }
        }

        private bool[] stringNotNull = new bool[2];

        public void Up(bool value, int position)
        {
            stringNotNull[position] = value;

            Enabled = (stringNotNull[0] == true && stringNotNull[1] == true);
        }

        #endregion

        #region ProgressBar
        private int progressBar_value;
        private static int progressBar_maxValue;

        public int ProgressBar_maxValue
        {
            get { return progressBar_maxValue; }
            set
            {
                progressBar_maxValue = value;
                OnPropertyChanged("ProgressBar_maxValue");
            }
        }

        public int ProgressBar_value
        {
            get { return this.progressBar_value; }
            set
            {
                progressBar_value = value;
                OnPropertyChanged("ProgressBar_value");
            }
        }
        #endregion

        #region Files path
        private string path_file_open;
        private string path_file_save;

        public string Path_file_open
        {
            get { return this.path_file_open; }
            set
            {
                path_file_open = value;
                OnPropertyChanged("Path_file_open");
            }
        }

        public string Path_file_save
        {
            get { return this.path_file_save; }
            set
            {
                path_file_save = value;
                OnPropertyChanged("Path_file_save");
            }
        }
        #endregion

        public ApplicationViewModel()
        {
            //Setting button actions
            dialogService = new DefaultDialogService();
            StartWork = new RelayCommand(new Action(DoWork));
            ShowLogs = new RelayCommand(new Action(ShowLogWindow));
            //Setting the maximum value of the ProgressBar
            ProgressBar_maxValue = 1;
            //Create logs vieswModel when we write logs
            logsViewModel = new Logs.LogsViewModel();
        }

        #region SDF File
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.SaveFileDialog() == true)
                          {
                              //fileService.Save(dialogService.FilePath, Phones.ToList());
                              Path_file_save = dialogService.FilePath;
                          }
                      }
                      catch (Exception ex)
                      {
                          logsViewModel.AddTextToLogs("Error when select destination file. Error: " + ex);
                      }
                  }));
            }
        }
        #endregion

        #region DAT File
        // команда открытия файла
        private RelayCommand openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return openCommand ??
                  (openCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (dialogService.OpenFileDialog() == true)
                          {
                              Path_file_open = dialogService.FilePath;
                              Enabled = true;
                          }
                      }
                      catch (Exception ex)
                      {
                          logsViewModel.AddTextToLogs("Error when select source file. Error: " + ex);
                      }
                  }));
            }
        }
        #endregion

        #region Star Work
        private ICommand startWork;

        public ICommand StartWork
        {
            get { return startWork; }
            set { startWork = value; }
        }

        
        /// <summary>
        /// Start of data conversion operation
        /// </summary>
        public void DoWork()
        {
            Enabled = false;
            FileReader fileRedaer;

            //Сreating a stream that performs data conversion
            Thread thread = new Thread(new ThreadStart(new Action(() => { fileRedaer = new FileReader(path_file_open, path_file_save, this); })));
            thread.Start();
        }
        #endregion

        #region Logs
        private ICommand showLogs;

        public ICommand ShowLogs
        {
            get { return showLogs; }
            set { showLogs = value; }
        }
        public void ShowLogWindow()
        {
            //Opening the logs window
            var win = new Logs.LogsWindow(logsViewModel);
            win.Show();
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
