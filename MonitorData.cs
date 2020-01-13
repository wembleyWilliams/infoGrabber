using System;

namespace infoGrabber
{
    public class MonitorData
    {
        
        public string modelM { get; set; }
        public string serialNumberM { get; set; }
        public string vendorM { get; set; }

        public void show()
        {
            Console.WriteLine(modelM, " -Monitor Model");
            Console.WriteLine(vendorM + " -Monitor Brand");
            Console.WriteLine(serialNumberM + " -Monitor Serial Number");
        }
    }
   
}
