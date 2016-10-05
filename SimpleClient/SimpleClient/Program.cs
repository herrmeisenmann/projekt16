using Newtonsoft.Json;
using SimpleClient.ServiceReference1;
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
            User user = client.GetUserById(9);
            Classroom classroom = client.GetClassByUserId(8);
            
            //while(true)
            //{
            //    String[] chat = client.GetChatMessages(3);
            //    foreach(var message in chat)
            //    {
            //        Console.WriteLine(message);
            //    }
            //    client.WriteToChat("Jonas", Console.ReadLine());
            //}
            
            //var fileStream = new FileStream("C:\\test\\hallo.png", FileMode.Create, FileAccess.Write);


            //Stream test = client.GetClassScheduleById(1);
            //test.CopyTo(fileStream);
            //fileStream.Dispose();
            Console.ReadKey();
        }
    }
}
