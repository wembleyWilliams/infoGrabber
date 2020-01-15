using System;

namespace infoGrabber
{
    public class PCData
    {
        
        public string modelPC { get; set; }
       
        public string vendorPC { get; set; }
      
        public string serialNumberPC { get; set; }
       
        public string version { get; set; }

        public string systemName { get; set; }

        public string assetName { get; set; }

        public string domain { get; set; }

        public void show()
        {
            Console.WriteLine(modelPC + " -PC Model");
            Console.WriteLine(vendorPC + " -PC Brand");
            Console.WriteLine(serialNumberPC + " -PC Serial Number");

            Console.WriteLine(systemName + " -System Name");
            Console.WriteLine(version + " -Windows Version");
            Console.WriteLine(assetName + " -Asset Name");
            Console.WriteLine(domain + " -Domain name");
        }


    }
}
