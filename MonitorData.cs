using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infoGrabber
{
    class MonitorData
    {
        public string modelM { get; set; }
        public string serialNumberM { get; set; }
        public string vendorM { get; set; }

        public void show()
        {
            //Console.WriteLine(description + " Description PC/Monitor");

            Console.WriteLine(modelM, " Monitor Model");
            Console.WriteLine(vendorM + " Monitor Brand");
            Console.WriteLine(serialNumberM + " Monitor Serial Number");
            Console.ReadKey();
        }
    }
   
}
