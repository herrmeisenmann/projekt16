using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Client.Server;
using System.IO;
/**
 * Created by Niklas Grieger on 27.09.2016.
 * Controller for page /Client
 */
namespace Client.Controllers
{
    public class ClientController : Controller
    {
        #region ActionResult
        //Methode für die View - /Views/Client/Index.cshtml
        public ActionResult Index()
        {
            return View();
        }
        //Methode für die Partial View - /Views/Client/_newAppointmentPartialView.cshtml
        public ActionResult _newAppointmentPartialView()
        {
            return View();
        }
        //Methode für die Partial View - /Views/Client/_chatPartialView.cshtml
        public ActionResult _chatPartialView()
        {
            return View();
        }
        #endregion
        #region JsonResult
        //Gibt UserInfos anhand der user_id vom Server als JsonObject zurück
        public JsonResult getUserById(int id)
        {
            ServiceClient server = new ServiceClient();
            User user = server.GetUserById(id);
            return Json(user, JsonRequestBehavior.AllowGet);
        }
        //Gibt UserInfos anhand dem username vom Server als JsonObject zurück
        public JsonResult getUserByName(string username)
        {
            ServiceClient server = new ServiceClient();
            User user = server.GetUserByName(username);
            return Json(user, JsonRequestBehavior.AllowGet);
        }
        //Gibt Termin anhand dem user_id vom Server als JsonObject zurück
        public JsonResult getAppointmentsByUserId(int id)
        {
            ServiceClient server = new ServiceClient();
            List<Appointment> appointments = server.GetAppointmentsByUserId(id);
            return Json(appointments, JsonRequestBehavior.AllowGet);
        }
        //Gibt den Notendurchschnitt anhand der user_id vom Server als JsonObject zurück
        public JsonResult getGradAvg(int id)
        {
            ServiceClient server = new ServiceClient();
            Double gradeAVG = server.GetAverageGradeByUserId(id);
            return Json(gradeAVG, JsonRequestBehavior.AllowGet);
        }
        //Sendet values an die Methode auf dem Server, dort werden diese in die Termine Tabelle in der Datenbank geschrieben
        //Gibt einen String zurück
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
        //Gibt die Chat Messages in der Datenbank als JsonObject zurück
        public JsonResult getChat()
        {
            ServiceClient server = new ServiceClient();
            List<String> chatMessages = server.GetChatMessages(10);
            return Json(chatMessages, JsonRequestBehavior.AllowGet);
        }
        //Sendet values an die Methode auf dem Server, dort wird eine Chat Message in die Datenbank geschrieben
        public void writeChat(string username, string message)
        {
            ServiceClient server = new ServiceClient();
            server.WriteToChat(username, message);
        }
        //Gibt das Stundenplan Bild als JSonObject zurück
        public JsonResult getSchedule(int id)
        {
            string path = $"C:\\GitProjects\\projekt16new\\Client\\Pics\\{id}.jpg";
            ServiceClient server = new ServiceClient();
            //Wenn die Datei lokal nicht vorhanden ist, wird das Bild von dem Server geholt und lokal abgespeichert
            if (!System.IO.File.Exists(path))
            {
                FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
                Stream stream = server.GetClassScheduleById(id);
                stream.CopyTo(fileStream);
                fileStream.Dispose();
            }
            //Wenn das Bild Lokal gespeichert ist, wird es lokal geladen
            if (System.IO.File.Exists(path))
            {
                Byte[] bytes = System.IO.File.ReadAllBytes(path);
                String file = Convert.ToBase64String(bytes);
                return Json(file, JsonRequestBehavior.AllowGet);
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
        #endregion

    }
}