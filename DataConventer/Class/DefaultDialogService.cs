﻿using Microsoft.Win32;
using System.Windows;

namespace DataConventer
{
    public class DefaultDialogService : IDialogService
    {
        public string FilePath { get; set; }

        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "(*.dat)|*.dat";
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }

            return false;
        }

        public bool SaveFileDialog()
        {
            OpenFileDialog saveFileDialog = new OpenFileDialog();
            saveFileDialog.Filter = "(*.sdf)|*.sdf";
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                return true;
            }

            return false;
        }
    }
}
