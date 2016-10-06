using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Client.Server;

namespace Client.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult _newAppointmentPartialView()
        {
            return View();
        }
        public ActionResult _chatPartialView()
        {
            return View();
        }
        public JsonResult getUserById(int id)
        {
            ServiceClient server = new ServiceClient();
            User user = server.GetUserById(id);
            return Json(user, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getUserByName(string username)
        {
            ServiceClient server = new ServiceClient();
            User user = server.GetUserByName(username);
            return Json(user, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAppointmentsByUserId(int id)
        {
            ServiceClient server = new ServiceClient();
            List<Appointment> appointments = server.GetAppointmentsByUserId(id);
            return Json(appointments, JsonRequestBehavior.AllowGet);
        }
        public String saveAppointment(int userId, string name, string comment, DateTime date, string subject, int grade)
        {
            ServiceClient server = new ServiceClient();
            bool save = server.InsertUserAppointment(userId, name, comment, date, subject, grade);
            if (save == true)
            {
                return "Termin wurde gespeichert!";
            }
            else
            {
                return "Termin wurde nicht gespeichert!";
            }

        }
        public JsonResult getChat()
        {
            ServiceClient server = new ServiceClient();
            List<String> chatMessages = server.GetChatMessages(50);
            return Json(chatMessages, JsonRequestBehavior.AllowGet);
        }
        public void writeChat(string username, string message)
        {
            ServiceClient server = new ServiceClient();
            server.WriteToChat(username, message);
        }
        //Gibt nur ein Leeren String zurück ????
        public JsonResult getImage()
        {
            //FileStream fileStream = new FileStream($"G:\\gitProjects\\projekt16\\Client\\Pics\\error.png", FileMode.Open, FileAccess.Read);
            Byte[] bytes = System.IO.File.ReadAllBytes("G:\\gitProjects\\projekt16\\Client\\Pics\\1.png");
            String file = Convert.ToBase64String(bytes);
            return Json(file, JsonRequestBehavior.AllowGet);
           
        }
        //public JsonResult getSchedule(int id)
        //{
        //    string path = $"G:\\gitProjects\\projekt16\\Client\\Pics\\{id}.png";
        //    //string path = $"C:\\GitProjects\\projekt16new\\Client\\Pics\\{id}.png";
        //    ServiceClient server = new ServiceClient();
        //    //Wenn die Datei lokal nicht vorhanden ist, wird das Bild von dem Server geholt und lokal abgespeichert
        //    if (!System.IO.File.Exists(path))
        //    {
        //        FileStream fileStream = new FileStream($"G:\\gitProjects\\projekt16\\Client\\Pics\\{id}.png", FileMode.Create, FileAccess.Write);
        //        //FileStream fileStream = new FileStream($"C:\\GitProjects\\projekt16new\\Client\\Pics\\{id}.png", FileMode.Create, FileAccess.Write);
        //        Stream stream = server.GetClassScheduleById(id);
        //        stream.CopyTo(fileStream);
        //        fileStream.Dispose();
        //    }
        //    //Wenn das Bild Lokal gespeichert ist, wird es lokal geladen
        //    if (System.IO.File.Exists(path))
        //    {
        //        FileStream fileStream = new FileStream($"G:\\gitProjects\\projekt16\\Client\\Pics\\{id}.png", FileMode.OpenOrCreate, FileAccess.Read);
        //        //FileStream fileStream = new FileStream($"C:\\GitProjects\\projekt16new\\Client\\Pics\\{id}.png", FileMode.OpenOrCreate, FileAccess.Read);
        //        //StreamReader sr = new StreamReader(fileStream);
        //        //var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //        //var jsonObject = serializer.DeserializeObject(sr.ReadToEnd());
        //        //return Json(jsonObject, JsonRequestBehavior.AllowGet);
        //        byte[] buffer = new byte[fileStream.Length];
        //        fileStream.Read(buffer, 0, (int)fileStream.Length);
        //        fileStream.Close();
        //        string str = System.Convert.ToBase64String(buffer, 0, buffer.Length);
        //        return Json(new { Image = str, JsonRequestBehavior.AllowGet });
        //    }
        //    //Fehlerfall: Zeige error Pic
        //        else
        //        {
        //        //Keine Datei da auch nicht auf Server errorrr..
        //        FileStream fileStream = new FileStream($"C:\\GitProjects\\projekt16new\\Client\\Pics\\error.png", FileMode.Create, FileAccess.Write);
        //        byte[] buffer = new byte[fileStream.Length];
        //        fileStream.Read(buffer, 0, (int)fileStream.Length);
        //        fileStream.Close();
        //        string str = System.Convert.ToBase64String(buffer, 0, buffer.Length);
        //        return Json(new { Image = str, JsonRequestBehavior.AllowGet });
        //    }
        //}
        }
}