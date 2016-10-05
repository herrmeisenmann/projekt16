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
        public ActionResult Index()
        {
            return View();
        }
        public String getClassesJson()
        {
            ServiceClient server = new ServiceClient();
            List<Classroom> list = server.GetAllClasses();
            var json = JsonConvert.SerializeObject(list);
            return json;
        }
    }
}