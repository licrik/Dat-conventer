using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoFixture;
using Newtonsoft.Json;

namespace Database_conventer_Unit_tests
{
    [TestClass]
    public class DarabaseStructureTest
    {
        private struct FileEmulator
        {
            public Database_conventer.Class.DatabaseStructure class_data;
            public List<byte> bytes_array;
        }

        private FileEmulator CreateFileEmulator()
        {
            FileEmulator file = new FileEmulator();
            file.bytes_array = new List<byte>();
            file.class_data = new Database_conventer.Class.DatabaseStructure();

            file.class_data.filled_record = true;
            file.class_data.fields_flags = 511;
            file.class_data.groupID = 1;
            file.class_data.userID = 8;
            file.class_data.NameLen = 16;
            file.class_data.name = "Default Operator";
            file.class_data.Hidden = false;
            file.class_data.RSetLen = 0;
            file.class_data.RightSet = "";
            file.class_data.HashLen = 16;
            file.class_data.HashCode = "202CB962AC59075B964B07152D234B70";
            file.class_data.CompanyLen = 0;
            file.class_data.Company = "";
            file.class_data.AirportLen = 0;
            file.class_data.Airport = "";
            file.class_data.TerminalLen = 0;
            file.class_data.Terminal = "";
            file.class_data.CheckSum = 81;
            file.class_data.legth_class = 444;


            file.bytes_array.Add(Convert.ToByte(file.class_data.filled_record));
            file.bytes_array.AddRange(BitConverter.GetBytes(file.class_data.fields_flags));
            file.bytes_array.AddRange(BitConverter.GetBytes(file.class_data.groupID));
            file.bytes_array.AddRange(BitConverter.GetBytes(file.class_data.userID));
            file.bytes_array.Add(Convert.ToByte(file.class_data.NameLen));

            byte[] tmp = new byte[100];
            Encoding.ASCII.GetBytes(file.class_data.name).CopyTo(tmp, 0);
            file.bytes_array.AddRange(tmp);

            file.bytes_array.Add(Convert.ToByte(file.class_data.Hidden));
            file.bytes_array.Add(Convert.ToByte(file.class_data.RSetLen));

            tmp = new byte[100];
            Encoding.ASCII.GetBytes(file.class_data.RightSet).CopyTo(tmp, 0);
            file.bytes_array.AddRange(tmp);

            file.bytes_array.Add(Convert.ToByte(file.class_data.HashLen));

            tmp = new byte[100];
            Encoding.ASCII.GetBytes(file.class_data.HashCode).CopyTo(tmp, 0);
            file.bytes_array.AddRange(tmp);

            file.bytes_array.Add(Convert.ToByte(file.class_data.CompanyLen));

            tmp = new byte[50];
            Encoding.ASCII.GetBytes(file.class_data.Company).CopyTo(tmp, 0);
            file.bytes_array.AddRange(tmp);

            file.bytes_array.Add(Convert.ToByte(file.class_data.AirportLen));

            tmp = new byte[50];
            Encoding.ASCII.GetBytes(file.class_data.Airport).CopyTo(tmp, 0);
            file.bytes_array.AddRange(tmp);

            file.bytes_array.Add(Convert.ToByte(file.class_data.TerminalLen));

            tmp = new byte[25];
            Encoding.ASCII.GetBytes(file.class_data.Terminal).CopyTo(tmp, 0);
            file.bytes_array.AddRange(tmp);

            file.bytes_array.Add(Convert.ToByte(file.class_data.CheckSum));

            return file;
        } 

        [TestMethod]
        public void ConvertByteToClass_ClassRerurn()
        {
            //arrange
            FileEmulator tmp = CreateFileEmulator();
            //act
            Database_conventer.Class.DatabaseStructure expected_structure = new Database_conventer.Class.DatabaseStructure(tmp.bytes_array.ToArray());
            //assert
            Assert.AreEqual(JsonConvert.SerializeObject(expected_structure), JsonConvert.SerializeObject(tmp.class_data));
        }

        [TestMethod]
        public void ConvertToString_bytes_stringreturn()
        {
            //arrange
            int length = 100;
            string str = "Default Operator";
            byte[] tmp = new byte[length];
            Encoding.ASCII.GetBytes(str).CopyTo(tmp, 0);

            //act
            Database_conventer.Class.DatabaseStructure dt = new Database_conventer.Class.DatabaseStructure();
            string expected = dt.Convert_to_string(tmp, length);

            //assert
            Assert.AreEqual(expected, str);
        }
    }
}
