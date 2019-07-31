using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace DataConventer.Class
{
    class FileReader
    {
        private List<Class.DatabaseStructure> database_list = new List<Class.DatabaseStructure>();
        private int classLength = 444;
        private ApplicationViewModel model;

        /// <summary>
        /// The constructor initializes the components and starts the work
        /// </summary>
        /// <param name="datFilePath">Dat file path</param>
        /// <param name="sdfFilePath">Sdf file path</param>
        /// <param name="model">Viev model is needed to change state </param>
        public FileReader(string datFilePath, string sdfFilePath, ApplicationViewModel model) {
            this.model = model;

            ReadDataFromFile(datFilePath);

            ConvertToDb(sdfFilePath);
        }

        /// <summary>
        /// Read data from file and convert to class
        /// </summary>
        /// <param name="path">Dat file path</param>
        private void ReadDataFromFile(string path)
        {
            byte[] array_test = new byte[classLength];
            try
            {
                using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
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
            }
            catch (Exception ex)
            {
                model.logsViewModel.AddTextToLogs("Error when read DAT file. Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Сalculation and setting the maximum Progress bar value
        /// </summary>
        /// <param name="dataCount">Number of records to process</param>
        private void PutMaxLength(long dataCount)
        {
            model.ProgressBar_maxValue = (Convert.ToInt32(dataCount) / classLength) * 2;
        }

        private void IncrementProgressBar()
        {
            model.ProgressBar_value++;
        }

        /// <summary>
        /// Write data from class to file
        /// </summary>
        /// <param name="sdf_file">File path</param>
        private void ConvertToDb(string sdf_file)
        {
            FileWriter fileWriter = null;

            try
            {
                model.logsViewModel.AddTextToLogs("Start wtite in SDF file.");

                fileWriter = new FileWriter(sdf_file, model);
                for (int i = 0; i < this.database_list.Count; i++)
                {
                    fileWriter.AddValueInDataBase(name: this.database_list[i].name, password: this.database_list[i].hashCode, group_id: this.database_list[i].Get_GroupId());

                    IncrementProgressBar();
                }
            }
            catch (Exception ex)
            {
                model.logsViewModel.AddTextToLogs("Error when read to SDF file. Error: " + ex.Message);
            }
            finally
            {
                fileWriter.ConnectionClose();
                model.Enabled = true;
                model.logsViewModel.AddTextToLogs("End operation.");

                MessageBox.Show("Operation end.");
                model.ProgressBar_value = 0;
            }
        }
    }
}
