using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyBlog.Controllers
{
    public class AccountController : Controller
    {
        //instantiate new database
                Models.MyBlogEntities db = new Models.MyBlogEntities();
        //
        // GET: /Account/

        public ActionResult Index()
        {
            //create a user
            Membership.CreateUser("admin", "techIsFun1");

            //validate the username and password
            if (Membership.ValidateUser("admin", "techIsFun1"))
            {
                //user pass is a match
                FormsAuthentication.SetAuthCookie("admin", false);

                
            }
            return View();
        }
        //GET: /Account/Register
        [HttpGet]
        public ActionResult Register()
        {
            return View(new Models.Register());
        }
        //POST: /Account/Register
        [HttpPost]
        public ActionResult Register(Models.Register reg)
        {
            //post action receives a Register Object
            //check to see if the user name has been used
            if (Membership.GetUser(reg.Username) == null)
            {
                //user is unique, so add the user to the database
                Membership.CreateUser(reg.Username, reg.Password);

                //add the user to our myBlog authors table
                Models.Author author = new Models.Author();
                //set the properties of our author
                author.UserName = reg.Username;
                author.Name = reg.Name;
                author.ImageURL = reg.ImageURL;

                //add the object to the database
                db.Authors.Add(author);

                //save changes
                db.SaveChanges();

                //Log the user in
                FormsAuthentication.SetAuthCookie(reg.Username, false);

                //kick the user back to the landing page
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //username is already in use
                ViewBag.Error = "Someone else has that username.";
                return View(reg);
            }
            
            
        }
        //GET: /Account/Logout
        [HttpGet]
        public ActionResult Logout()
        {
            //log out the user
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        //GET: /Account/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View(new Models.Login());
        }
        //GET: /Account/Login
        [HttpPost]
        public ActionResult Login(Models.Login log)
        {
            if (Membership.ValidateUser(log.UserName, log.Password))
            {
                FormsAuthentication.SetAuthCookie(log.UserName, false);
            }
            else
            {
                ViewBag.Error = "The Username and Password did not match.";
                return View(log);
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
