using Client.Server;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Views.ClientAccount
{
    public class ClientAccountController : Controller
    {
        // GET: ClientAccount
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public JsonResult getClasses()
        {
            ServiceClient server = new ServiceClient();
            List<Classroom> list = server.GetAllClasses();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getProfessions()
        {
            ServiceClient server = new ServiceClient();
            List<Profession> list = server.GetAllProfessions();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
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
    }
}