using System;
using System.Data.SqlServerCe;

namespace DataConventer.Class
{
    class FileWriter
    {
        private SqlCeConnection connection;
        private SqlCeCommand command;
        private ApplicationViewModel model;

        /// <summary>
        /// Initializing connection to destination file
        /// </summary>
        /// <param name="path">Path to file</param>
        /// <param name="model"></param>
        public FileWriter(string path, ApplicationViewModel model)
        {
            this.model = model;

            model.logsViewModel.AddTextToLogs("Start conection to SDF file.");

            if (String.IsNullOrEmpty(path))
            {
                path = "UserDbStorage.sdf";
                CreateDBFIle(path);
            }

            try
            {
                connection = new SqlCeConnection("Data Source = " + path);

                connection.Open();

                command = connection.CreateCommand();
            }
            catch(Exception ex)
            {
                model.logsViewModel.AddTextToLogs("Error when try connect to SDF file. Error: " + ex.Message);
            }
        }

        /// <summary>
        /// Creat new local database 
        /// </summary>
        /// <param name="path"></param>
        private void CreateDBFIle(string path)
        {
            try
            {
                SqlCeEngine engine = new SqlCeEngine("Data Source = " + path);
                engine.CreateDatabase();
                engine.Dispose();

                Connect(path);

                model.logsViewModel.AddTextToLogs("Create new local SDF file.");
            }
            catch {
                model.logsViewModel.AddTextToLogs("Conection to local SDF file.");
            }
        }

        /// <summary>
        /// Initializing conection and create table
        /// </summary>
        /// <param name="path"></param>
        private void Connect(string path)
        {
            connection = new SqlCeConnection("Data Source = " + path);

            connection.Open();

            command = connection.CreateCommand();

            CreateTableGroup();
            CreateTable();

            ConnectionClose();
        }

        /// <summary>
        /// Create groupe table and input rows
        /// </summary>
        private void CreateTableGroup()
        {
            string sql_comand_Group = @"CREATE TABLE DbGroups (
                                                                Id int IDENTITY(1,1) PRIMARY KEY,
                                                                Name nvarchar(50))";

            command.CommandText = sql_comand_Group;
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO DbGroups (\"name\") VALUES('Administrators')";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO DbGroups (\"name\") VALUES('Operators')";
            command.ExecuteNonQuery();

            command.CommandText = "INSERT INTO DbGroups (\"name\") VALUES('Observers')";
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Create table users
        /// </summary>
        private void CreateTable()
        {  
            string sql_comand_User = @"CREATE TABLE DbUsers (
                                                        Id int IDENTITY(1,1) PRIMARY KEY,  
                                                        Name nvarchar(100) NOT NULL, 
                                                        CreateDate datetime, 
                                                        Password nvarchar(100), 
                                                        PasswordChangeData datetime, 
                                                        LastLoginDate datetime, 
                                                        DbGroup_Id int,
                                                        FOREIGN KEY(DbGroup_Id)REFERENCES DbGroups(Id))";

            command.CommandText = sql_comand_User;
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Writing data to the database
        /// </summary>
        /// <param name="name">Name user</param>
        /// <param name="password">Password</param>
        /// <param name="group_id"></param>
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

        public void ConnectionClose()
        {
            connection.Close();
            command.Dispose();
        }
    }
}
