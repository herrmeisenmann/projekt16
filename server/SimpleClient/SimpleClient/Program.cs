using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceReference1.ServiceClient client = new ServiceReference1.ServiceClient();
            var fileStream = new FileStream("C:\\test\\hallo.png", FileMode.Create, FileAccess.Write);
            
            
            Stream test = client.GetClassScheduleById(1);
            test.CopyTo(fileStream);
            fileStream.Dispose();

        }
    }
}
