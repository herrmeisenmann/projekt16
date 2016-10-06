using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Client.Server;
using Newtonsoft.Json;
using System.Web.Helpers;
using System.Web.Script.Serialization;
using System.Collections;
using System.IO;

namespace Client.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult getUserById(int id)
        {
            ServiceClient server = new ServiceClient();
            User user = server.GetUserById(id);
            return Json(user, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getAppointmentsByUserId(int id)
        {
            ServiceClient server = new ServiceClient();
            List<Appointment> appointments = server.GetAppointmentsByUserId(id);
            return Json(appointments, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getSchedule(int schedule_id) 
        {
            string path = $"C:\\GitProjects\\projekt16new\\Client\\Pics\\{schedule_id}.png";
            ServiceClient server = new ServiceClient();
            //Wenn die Datei lokal nicht vorhanden ist, wird das Bild von dem Server geholt und lokal abgespeichert
            if (!System.IO.File.Exists(path))
            {
                FileStream fileStream = new FileStream($"C:\\GitProjects\\projekt16new\\Client\\Pics\\{schedule_id}.png", FileMode.Create, FileAccess.Write);
                Stream stream = server.GetClassScheduleById(schedule_id);
                stream.CopyTo(fileStream);
                fileStream.Dispose();
            }
            //Wenn das Bild Lokal gespeichert ist, wird es lokal geladen
            if(System.IO.File.Exists(path))
            {
                FileStream fileStream = new FileStream($"C:\\GitProjects\\projekt16new\\Client\\Pics\\{schedule_id}.png", FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader sr = new StreamReader(fileStream);
                var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                var jsonObject = serializer.DeserializeObject(sr.ReadToEnd());
                return Json(jsonObject, JsonRequestBehavior.AllowGet);
                //byte[] buffer = new byte[fileStream.Length];
                //fileStream.Read(buffer, 0, (int)fileStream.Length);
                //fileStream.Close();
                //string str = System.Convert.ToBase64String(buffer, 0, buffer.Length);
                //return Json(new { Image = str, JsonRequestBehavior.AllowGet });
            }
            //Fehlerfall: Zeige error Pic
            else
            {
                //Keine Datei da auch nicht auf Server errorrr..
                FileStream fileStream = new FileStream($"C:\\GitProjects\\projekt16new\\Client\\Pics\\error.png", FileMode.Create, FileAccess.Write);
                byte[] buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, (int)fileStream.Length);
                fileStream.Close();
                string str = System.Convert.ToBase64String(buffer, 0, buffer.Length);
                return Json(new { Image = str, JsonRequestBehavior.AllowGet });
            }
        }
    }
}