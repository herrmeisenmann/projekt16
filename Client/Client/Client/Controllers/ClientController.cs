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
    }
}