using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult getStundenplanImage()
        {
            FileStream fileStream = new FileStream(@"C:\Users\Nik\Desktop\stdPlan.jpg", FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0, (int)fileStream.Length);
            fileStream.Close();
            string str = System.Convert.ToBase64String(buffer, 0, buffer.Length);
            return Json(new { Image = str, JsonRequestBehavior.AllowGet });
        }
    }
}