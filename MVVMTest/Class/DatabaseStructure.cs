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
        public Boolean filled_record { get; set; }

        public UInt16 fields_flags { get; set; }  

        public UInt32 groupID { get; set; }    

        public UInt32 userID { get; set; }      

        public Byte nameLen { get; set; }     

        public string name { get; set; }        

        public Boolean hidden { get; set; }     

        public Byte RSetLen { get; set; }     

        public string rightSet { get; set; }

        public Byte hashLen { get; set; }

        public string hashCode { get; set; }       // 1 byte 

        public Byte companyLen { get; set; }      // 100 byte

        public string company { get; set; }     // 1 byte

        public Byte airportLen { get; set; }  //

        public string airport { get; set; }

        public Byte terminalLen { get; set; }

        public string terminal { get; set; }

        public Byte checkSum { get; set; }

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

            nameLen = bytes_array[legth_class];
            legth_class++;

            name = Convert_to_string(bytes_array, 100, nameLen);

            hidden = BitConverter.ToBoolean(bytes_array, legth_class);
            legth_class += sizeof(Boolean);

            RSetLen = bytes_array[legth_class];
            legth_class++;

            rightSet = Convert_to_hex(bytes_array, 100, RSetLen);

            hashLen = bytes_array[legth_class];
            legth_class++;

            hashCode = Convert_to_hex(bytes_array, 100, hashLen);

            companyLen = bytes_array[legth_class];
            legth_class++;

            company = Convert_to_string(bytes_array, 50, companyLen);

            airportLen = bytes_array[legth_class];
            legth_class++;

            airport = Convert_to_string(bytes_array, 50, airportLen);

            terminalLen = bytes_array[legth_class];
            legth_class++;

            terminal = Convert_to_string(bytes_array, 25, terminalLen);

            checkSum = bytes_array[legth_class];
            legth_class++;
        }

        public string Convert_to_hex(byte[] array, int length, int trueLength)
        {
            if (trueLength == 0) {
                this.legth_class += length;

                return "";
            }

            byte[] tmp = new byte[length];
            Buffer.BlockCopy(array, legth_class, tmp, 0, tmp.Length);
            this.legth_class += length;

            return BitConverter.ToString(tmp).Replace("-", string.Empty).Substring(0, trueLength * 2);
        }

        public string Convert_to_string(byte[] array, int length, int trueLen)
        {
            if (trueLen == 0) {
                this.legth_class += length;

                return "";
            }

            byte[] tmp = new byte[length];
            Buffer.BlockCopy(array, legth_class, tmp, 0, tmp.Length);
            this.legth_class += length;

            return Encoding.ASCII.GetString(tmp).Replace("\0", string.Empty).Substring(0, trueLen);
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
