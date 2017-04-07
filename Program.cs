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
            // Starte WCF-Web-Service
            using (ServiceHost host = new ServiceHost(typeof(Service)))
            {
                host.Open();

                Console.WriteLine("Service Läuft auf:");
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
