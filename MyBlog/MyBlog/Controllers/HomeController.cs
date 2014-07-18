using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    public class HomeController : Controller
    {
        //set up a connection to the database
        Models.MyBlogEntities db = new Models.MyBlogEntities();
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //Pass all the posts to the view, order by newest first
            return View(db.Posts.OrderByDescending(x => x.DateCreated));
        }
        //GET: /Home/Like/id
        [HttpGet]
        public ActionResult Like(int id)
        {
            //go to the database and retrieve the post that matches the id
            Models.Post post = db.Posts.Find(id);
            post.Likes = post.Likes + 1;

            db.SaveChanges();
            return Content(post.Likes + " likes");
        }
    }
}
