using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MVVMTest.Class
{
    class FileReader
    {
        private List<Class.DatabaseStructure> database_list = new List<Class.DatabaseStructure>();
        private int classLength = 444;
        private ApplicationViewModel model;

        int IDFromHex(string HexID)
        {
            return int.Parse(HexID, System.Globalization.NumberStyles.HexNumber);
        }

        public FileReader(string dat_file, string sdf_file, ApplicationViewModel model) {
            this.model = model;
            byte[] array_test = new byte[classLength];

            using (BinaryReader reader = new BinaryReader(File.Open(dat_file, FileMode.Open)))
            {
                model.logsViewModel.AddTextToLogs("Start read DAT file");
                //Skip first bytes
                reader.BaseStream.Seek(314, SeekOrigin.Begin);
                PutMaxLength(reader.BaseStream.Length);
              
                int current_length = database_list.Count * classLength;

                while (reader.PeekChar() > -1)
                {
                    reader.Read(array_test, current_length, classLength);
                    database_list.Add(new DatabaseStructure(array_test));

                    IncrementProgressBar();
                }
            }
            this.ConvertToDb(sdf_file);
        }

        private void PutMaxLength(long fileCount)
        {
            model.ProgressBar_maxValue = (Convert.ToInt32(fileCount) / classLength) * 2;
        }

        private void IncrementProgressBar()
        {
            model.ProgressBar_value++;
        }

        private void ConvertToDb(string sdf_file)
        {
            try
            {
                model.logsViewModel.AddTextToLogs("Start wtite in SDF file.");

                FileWriter fileWriter = new FileWriter(sdf_file, model);
                for (int i = 0; i < this.database_list.Count; i++)
                {
                    fileWriter.AddValueInDataBase(name: this.database_list[i].name, password: this.database_list[i].HashCode, group_id: this.database_list[i].Get_GroupId());

                    IncrementProgressBar();
                }
            }
            catch (Exception ex)
            {
                model.logsViewModel.AddTextToLogs("Error when read to SDF file. Error: " + ex.Message);
            }
            finally
            {
                model.Enabled = true;
                model.logsViewModel.AddTextToLogs("End operation.");

                MessageBox.Show("Operation end.");
                model.ProgressBar_value = 0;
            }
        }

        private void writeLog(string text)
        {
        }
    }
}
