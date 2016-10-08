using Client.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/**
 * Created by Niklas Grieger on 27.09.2016.
 * Controller for page /ClientAccount
 */

namespace Client.Views.ClientAccount
{
    public class ClientAccountController : Controller
    {
        #region ActionResult
        //Methode für die View - /Views/ClientAccount/Register.cshtml
        public ActionResult Register()
        {
            return View();
        }
        //Methode für die View - /Views/ClientAccount/Login.cshtml
        public ActionResult Login()
        {
            return View();
        }
        #endregion
        #region JsonResult
        //Gibt alle Klassen vom Server als JsonObject zurück
        public JsonResult getClasses()
        {
            ServiceClient server = new ServiceClient();
            List<Classroom> list = server.GetAllClasses();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //Gibt alle Berufe vom Server als JsonObject zurück
        public JsonResult getProfessions()
        {
            ServiceClient server = new ServiceClient();
            List<Profession> list = server.GetAllProfessions();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        //Sendet values an die Methode auf dem Server, dort werden diese in die User Tabelle in der Datenbank geschrieben
        //Gibt einen String zurück
        public String saveUser(string username, string firstname, string lastname, string password, int profession_id, int class_id)
        {
            ServiceClient server = new ServiceClient();
            bool save = server.InsertUserIntoDb(username, firstname, lastname, password, profession_id, class_id);
            if (save == true) {
                return "User wurde gespeichert!";
            }
            else
            {
                return "User wurde nicht gespeichert!";
            }
            
        }
        //Sendet values an den Server um den Login zu checken und gibt einen String zurück
        public String checkLogin(string username, string password)
        {
            ServiceClient server = new ServiceClient();
            bool check = server.LoginUser(username, password);
            if (check == true)
            {
                return "Login war erfolgreich!";
            }
            else
            {
                return "Ooops!\nDa ist was schiefgelaufen\nUsername oder Password sind falsch!";
            }
        }
        #endregion
    }
}