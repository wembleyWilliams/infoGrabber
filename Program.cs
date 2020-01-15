using System;
using System.Management;
using System.Text;

namespace infoGrabber
{
    class Program
    {
        static void Main(string[] args)
        { 
            //PC
            setPCData().show();
            //MONITOR
            setMonitorData().show();            
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
                Console.WriteLine(ex.Message); 
            }

            return sb.ToString();
        }

        private static MonitorData setMonitorData() {
            MonitorData md = new MonitorData();
            try
            {
               

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
                                        //d.description = "Monitor";
                                        switch (p.Name)
                                        {

                                            case "ManufacturerName":
                                                {
                                                    md.vendorM = getString((UInt16[])p.Value);
                                                    break;
                                                }
                                            case "SerialNumberID":
                                                {
                                                    md.serialNumberM = getString((UInt16[])p.Value);
                                                    break;
                                                }
                                            case "UserFriendlyName":
                                                {
                                                    md.modelM = getString((UInt16[])p.Value);
                                                    break;
                                                }
                                        }
                                        break;
                                    }
                            }
                        }
                    }
                }

                //add to database
                Database d = new Database();
                if (!d.verifyMonitor(md.serialNumberM))
                    d.insertMonitor(md);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return md;

        }

        private static PCData setPCData() {
            PCData pd = new PCData();
            try
            {
                
                System.Management.SelectQuery query = new System.Management.SelectQuery(@"Select * from Win32_ComputerSystem");

                //initialize the searcher with the query it is supposed to execute
                using (System.Management.ManagementObjectSearcher searcher1 = new System.Management.ManagementObjectSearcher(query))
                {
                    //execute the query
                    foreach (System.Management.ManagementObject process in searcher1.Get())
                    {
                        //print system info
                        process.Get();

                        pd.vendorPC = "" + process["Manufacturer"];
                        pd.modelPC = "" + process["Model"];
                    }
                }
                //to start searching at Windows BIOS table for the device serial number
                //shows the serial number of the PC
                ManagementObjectSearcher MOS = new ManagementObjectSearcher("Select * From Win32_BIOS");

                foreach (ManagementObject getserial in MOS.Get())
                {
                    pd.serialNumberPC = getserial["SerialNumber"].ToString();
                }

                pd.systemName = System.Environment.MachineName;
                pd.version = GetOSFriendlyName();
                pd.domain = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
                pd.assetName = pd.vendorPC + " " + pd.modelPC;

                Database d = new Database();
                if (!d.verifyPC(pd.serialNumberPC))
                    d.insertPC(pd);
            }
            catch(Exception t)
            {
                Console.WriteLine(t.Message);
            }
            
            return pd;

        }

    }
    
}