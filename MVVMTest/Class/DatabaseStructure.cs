using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTest.Class
{
    class DatabaseStructure
    {
        public int legth_class { get; set; }
        public Boolean filled_record { get; set; } // 1 byte

        public UInt16 fields_flags { get; set; }  //2 byte

        public UInt32 groupID { get; set; }    // 4 byte

        public UInt32 userID { get; set; }      // 4 byte

        public Byte NameLen { get; set; }     // 1 byte

        public string name { get; set; }        // 100 byte

        public Boolean Hidden { get; set; }     // 100 byte

        public Byte RSetLen { get; set; }     // 1 byte

        public string RightSet { get; set; }

        public Byte HashLen { get; set; }

        public string HashCode { get; set; }       // 1 byte 

        public Byte CompanyLen { get; set; }      // 100 byte

        public string Company { get; set; }     // 1 byte

        public Byte AirportLen { get; set; }  //

        public string Airport { get; set; }

        public Byte TerminalLen { get; set; }

        public string Terminal { get; set; }

        public Byte CheckSum { get; set; }

        public DatabaseStructure() { }

        public DatabaseStructure(byte[] bytes_array)
        {
            legth_class = 0;
            filled_record = BitConverter.ToBoolean(bytes_array, legth_class);
            legth_class += sizeof(Boolean);

            fields_flags = BitConverter.ToUInt16(bytes_array, legth_class);
            legth_class += sizeof(UInt16);

            groupID = BitConverter.ToUInt32(bytes_array, legth_class);
            legth_class += sizeof(UInt32);

            userID = BitConverter.ToUInt32(bytes_array, legth_class);
            legth_class += sizeof(UInt32);

            NameLen = bytes_array[legth_class];
            legth_class++;

            name = Convert_to_string(bytes_array, 100);

            Hidden = BitConverter.ToBoolean(bytes_array, legth_class);
            legth_class += sizeof(Boolean);

            RSetLen = bytes_array[legth_class];
            legth_class++;

            RightSet = Convert_to_string(bytes_array, 100);

            HashLen = bytes_array[legth_class];
            legth_class++;

            HashCode = Convert_to_string(bytes_array, 100);

            CompanyLen = bytes_array[legth_class];
            legth_class++;

            Company = Convert_to_string(bytes_array, 50);

            AirportLen = bytes_array[legth_class];
            legth_class++;

            Airport = Convert_to_string(bytes_array, 50);

            TerminalLen = bytes_array[legth_class];
            legth_class++;

            Terminal = Convert_to_string(bytes_array, 25);

            CheckSum = bytes_array[legth_class];
            legth_class++;
        }

        public string Convert_to_string(byte[] array, int length)
        {
            byte[] tmp = new byte[length];
            Buffer.BlockCopy(array, legth_class, tmp, 0, tmp.Length);
            this.legth_class += length;

            return Encoding.ASCII.GetString(tmp).Replace("\0", string.Empty);
        }

        public int Get_GroupId()
        {
            switch (groupID)
            {
                case 1:
                    {
                        return 2;
                    }
                case 2:
                case 3:
                    {
                        return 3;
                    }
                case 4:
                    {
                        return 1;
                    }
            }

            return 1;
        }
    }
}
