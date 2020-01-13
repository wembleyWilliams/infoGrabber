using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.SqlClient;

namespace infoGrabber
{
    class Program
    {
        static void Main(string[] args)
        {
            string serialNumber = string.Empty;
            PCData d = new PCData();
            MonitorData md = new MonitorData();

          
            //d.description = "PC";
            System.Management.SelectQuery query = new System.Management.SelectQuery(@"Select * from Win32_ComputerSystem");

            //initialize the searcher with the query it is supposed to execute
            using (System.Management.ManagementObjectSearcher searcher1 = new System.Management.ManagementObjectSearcher(query))
            {
                //execute the query
                foreach (System.Management.ManagementObject process in searcher1.Get())
                {
                    //print system info
                    process.Get();

                    d.vendorPC = "" + process["Manufacturer"];
                    d.modelPC = "" + process["Model"];
                }
            }
            //to start searching at Windows BIOS table for the device serial number
            //shows the serial number of the PC
            ManagementObjectSearcher MOS = new ManagementObjectSearcher("Select * From Win32_BIOS");

            foreach (ManagementObject getserial in MOS.Get())
            {
                d.serialNumberPC = getserial["SerialNumber"].ToString();
            }


            //this queries the information of all available monitors
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"root\WMI", "SELECT * FROM WmiMonitorID");
            foreach (ManagementObject obj in searcher.Get())
            {
                foreach (PropertyData p in obj.Properties)
                {
                    if (p.Value != null)
                    {
                        switch (p.Value.GetType().ToString())
                        {
                            case "System.UInt16[]":
                                {
                                    d.description = "Monitor";
                                    switch (p.Name)
                                    {
                                        
                                        case "ManufacturerName":
                                            {
                                                d.vendorM = getString((UInt16[])p.Value);
                                                break;
                                            }
                                        case "SerialNumberID":
                                            {
                                                d.serialNumberM = getString((UInt16[])p.Value);
                                                break;
                                            }
                                        case "UserFriendlyName":
                                            {
                                                d.modelM = getString((UInt16[])p.Value);
                                                break;
                                            }
                                    }
                                    break;
                                }
                        }
                    }
                }
            }

            d.systemName = System.Environment.MachineName;
            //d.version = System.Environment.OSVersion.ToString();
            d.version = GetOSFriendlyName();



            d.show();
        }

        public static string GetOSFriendlyName()
        {
            string result = string.Empty;
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem");
            foreach (ManagementObject os in searcher.Get())
            {
                result = os["Caption"].ToString();
                break;
            }
            return result;
        }

        private static string getString(UInt16[] Val)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                foreach (UInt16 u in Val)
                    sb.Append(char.ConvertFromUtf32(u));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return sb.ToString();
        }


    }
    
}