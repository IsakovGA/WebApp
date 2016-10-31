using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;
using PagedList.Mvc;
using PagedList;
using DatGen.BL;
using DatGen.Dat;
using System.Data.Entity;
using System.Text;
using System.IO;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private UsersDBEntities db = new UsersDBEntities();
        readonly string testDBName = "BlogUsers";
        private static string _tableName = "def";
        
        


        public ActionResult Index()
        {
            return View();
        }


        public ActionResult ListOfUsers(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            List<BlogUser> usersList = (from user in db.BlogUsers orderby user.Surname select user).ToList();

            return View(usersList.ToPagedList(pageNumber,pageSize));
        }

        public ActionResult GenerateScript ()
        {
            return View();
        }

        public ActionResult GenerateScriptResult (int? entityCount, string tableName)
        {
            int _entityCount = (entityCount ?? 0);
            if ((tableName.Length > 0) && !(tableName.Any(Char.IsWhiteSpace)) && (_entityCount > 0))
            {

                ScriptGen sg = new ScriptGen(new Repository());

                string generatedScript = "";
                generatedScript = sg.CreateScript(_entityCount, tableName);

                ViewBag.script = generatedScript;

                _tableName = tableName;

                return View();
            }
            else {
                return View("GenerateScript");
            }
        }

        public RedirectResult ScriptApply(string text)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(text);
            int index = sb.ToString().IndexOf(_tableName);
            sb.Remove(index, _tableName.Length);
            sb.Insert(index, testDBName);
            text = sb.ToString();

            db.Database.ExecuteSqlCommand("TRUNCATE TABLE " + testDBName);
            db.Database.ExecuteSqlCommand(text);
            db.SaveChanges();

            return Redirect("ListOfUsers");
        }

        public FileStreamResult SaveFile(string textForSave)
        {
            var byteArray = Encoding.UTF8.GetBytes(textForSave);
            var stream = new MemoryStream(byteArray);

            return File(stream, "text/plain", "textForSave.sql");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}