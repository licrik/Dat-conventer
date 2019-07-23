using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database_conventer.Class;

namespace Database_conventer.CLass
{
    class ReaderFile
    {
        private List<Class.DatabaseStructure> database_list = new List<Class.DatabaseStructure>();

        int IDFromHex(string HexID)
        {
            return int.Parse(HexID, System.Globalization.NumberStyles.HexNumber);
        }

        public ReaderFile(string dat_file, string sdf_file)
        {
            Windows.LogsWindows.Write_logs("Start read;");

            byte[] array_test = new byte[444];

            using (BinaryReader reader = new BinaryReader(File.Open(dat_file, FileMode.Open)))
            {
                //Skip first bytes
                reader.BaseStream.Seek(314, SeekOrigin.Begin);

                int current_length = database_list.Count * 444;

                while ( reader.PeekChar() > -1)
                {

                    reader.Read(array_test, current_length, 444);
                    database_list.Add(new DatabaseStructure(array_test));
                } 
            }
            this.ConvertToDb(sdf_file);
        }

        private void ConvertToDb(string sdf_file)
        {
            try
            {
                DB dB = new DB(sdf_file);
                for (int i = 0; i < this.database_list.Count; i++)
                {
                    dB.AddValueInDataBase(name: this.database_list[i].name, password: this.database_list[i].HashCode, group_id: this.database_list[i].Get_GroupId());
                }
            } catch (Exception ex)
            {
                Windows.LogsWindows.Write_logs("Error when read dat file | " + ex.Message);
            }
        }
    }

    class DB
    {
        private SqlCeConnection connection;
        private SqlCeCommand command;
        public DB(string way)
        {
            try
            {
                connection = new SqlCeConnection(way);
               
                connection.Open();

                command = connection.CreateCommand();
            }
            finally
            {
              
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
            catch(Exception error)
            {
                Windows.LogsWindows.Write_logs("Error when write data in file. | " + error.Message);
            }
        }
    }
}
