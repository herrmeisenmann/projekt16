using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(Service)))
            {
                host.Open();

                Console.WriteLine("Service up and running at:");
                foreach (var ea in host.Description.Endpoints)
                {
                    Console.WriteLine(ea.Address);
                }

                Console.ReadLine();
                host.Close();
            }

        }
    }
}
