using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using Newtonsoft.Json;
using DataConventer.Class;

namespace Data_converterTest
{
    [TestClass]
    public class DatabaseStructureTest
    {
        private struct FileEmulator
        {
            
            public DatabaseStructure class_data;
            public List<byte> bytes_array;
        }

        private FileEmulator CreateFileEmulator()
        {
            FileEmulator file = new FileEmulator();
            file.bytes_array = new List<byte>();
            file.class_data = new DatabaseStructure();

            file.class_data.filled_record = true;
            file.class_data.fields_flags = 511;
            file.class_data.groupID = 1;
            file.class_data.userID = 8;
            file.class_data.nameLen = 16;
            file.class_data.name = "Default Operator";
            file.class_data.hidden = false;
            file.class_data.RSetLen = 0;
            file.class_data.rightSet = "";
            file.class_data.hashLen = 16;
            file.class_data.hashCode = "202CB962AC59075B964B07152D234B70";
            file.class_data.companyLen = 0;
            file.class_data.company = "";
            file.class_data.airportLen = 0;
            file.class_data.airport = "";
            file.class_data.terminalLen = 0;
            file.class_data.terminal = "";
            file.class_data.checkSum = 81;
            file.class_data.legth_class = 444;


            file.bytes_array.Add(Convert.ToByte(file.class_data.filled_record));
            file.bytes_array.AddRange(BitConverter.GetBytes(file.class_data.fields_flags));
            file.bytes_array.AddRange(BitConverter.GetBytes(file.class_data.groupID));
            file.bytes_array.AddRange(BitConverter.GetBytes(file.class_data.userID));
            file.bytes_array.Add(Convert.ToByte(file.class_data.nameLen));

            byte[] tmp = new byte[100];
            Encoding.ASCII.GetBytes(file.class_data.name).CopyTo(tmp, 0);
            file.bytes_array.AddRange(tmp);

            file.bytes_array.Add(Convert.ToByte(file.class_data.hidden));
            file.bytes_array.Add(Convert.ToByte(file.class_data.RSetLen));

            tmp = new byte[100];
            HexToByte(file.class_data.rightSet).CopyTo(tmp, 0);
            file.bytes_array.AddRange(tmp);

            file.bytes_array.Add(Convert.ToByte(file.class_data.hashLen));

            tmp = new byte[100];
            HexToByte(file.class_data.hashCode).CopyTo(tmp, 0);
            file.bytes_array.AddRange(tmp);

            file.bytes_array.Add(Convert.ToByte(file.class_data.companyLen));

            tmp = new byte[50];
            Encoding.ASCII.GetBytes(file.class_data.company).CopyTo(tmp, 0);
            file.bytes_array.AddRange(tmp);

            file.bytes_array.Add(Convert.ToByte(file.class_data.airportLen));

            tmp = new byte[50];
            Encoding.ASCII.GetBytes(file.class_data.airport).CopyTo(tmp, 0);
            file.bytes_array.AddRange(tmp);

            file.bytes_array.Add(Convert.ToByte(file.class_data.terminalLen));

            tmp = new byte[25];
            Encoding.ASCII.GetBytes(file.class_data.terminal).CopyTo(tmp, 0);
            file.bytes_array.AddRange(tmp);

            file.bytes_array.Add(Convert.ToByte(file.class_data.checkSum));

            return file;
        }

        private byte[] HexToByte(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);

            return bytes;
        }

        [TestMethod]
        public void ConvertByteToClass_ClassRerurn()
        {
            //arrange
            FileEmulator tmp = CreateFileEmulator();
            //act
            DatabaseStructure expected_structure = new DatabaseStructure(tmp.bytes_array.ToArray());
            //assert
            Assert.AreEqual(JsonConvert.SerializeObject(expected_structure), JsonConvert.SerializeObject(tmp.class_data));
        }

        
    }
}
