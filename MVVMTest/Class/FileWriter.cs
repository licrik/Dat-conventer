using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTest.Class
{
    class FileWriter
    {
        private SqlCeConnection connection;
        private SqlCeCommand command;
        private ApplicationViewModel model;


        public FileWriter(string way, ApplicationViewModel model)
        {
            this.model = model;

            model.logsViewModel.AddTextToLogs("Start conection to SDF file.");

            try
            {
                connection = new SqlCeConnection("Data Source = " + way);

                connection.Open();

                command = connection.CreateCommand();
            }
            catch(Exception ex)
            {
                model.logsViewModel.AddTextToLogs("Error when try connect to SDF file. Error: " + ex.Message);
            }
        }

        public void AddValueInDataBase(string name, string password, int group_id)
        {
            try
            {
                string current_time = DateTime.Now.ToString("MM.dd.yyyy HH:mm:ss");
                command.CommandText = String.Format("INSERT INTO DbUsers (\"name\", \"createDate\",\"password\", \"DbGroup_Id\") VALUES('{0}', '{1}', '{2}', '{3}')", name, current_time, password, group_id);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                model.logsViewModel.AddTextToLogs("Error when try write data in SDF file. Error: " + ex.Message);

            }
        }
    }
}
