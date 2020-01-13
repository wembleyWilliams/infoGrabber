using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infoGrabber
{
    public class PCData
    {

        public string modelPC { get; set; }
       
        public string vendorPC { get; set; }
      
        public string serialNumberPC { get; set; }
       
        public string version { get; set; }
        public string systemName { get; set; }
        public string description { get; set; }

        public void show()
        {
            Console.WriteLine(description + " Description PC/Monitor");
            Console.ReadKey();
            Console.WriteLine(modelPC + " PC Model");
            Console.WriteLine(vendorPC + " PC Brand");
            Console.WriteLine(serialNumberPC + " PC Serial Number");
            Console.ReadKey();

            Console.WriteLine(systemName + " System Name");
            Console.WriteLine(version + " Windows Version");
            Console.ReadKey();
        }


    }
}
