using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyBlog.Controllers
{
    //Authorize data annotation requires a user
    //logged in to access anything in the database
    [Authorize()]
    public class PostController : Controller
    {
        //make an instance of the database
        Models.MyBlogEntities db = new Models.MyBlogEntities();
        //
        // GET: /Post/

        public ActionResult Index()
        {
            //the index will return all of the user's posts
            return View(db.Posts.Where(x => x.UserName.ToLower() == User.Identity.Name.ToLower()));
        }
        //GET: /Post/Create
        [HttpGet]
        public ActionResult Create()
        {
            //pass a blank post object o the view
            return View(new Models.Post());
        }
        //POST: /Post/Create
        [HttpPost]
        public ActionResult Create(Models.Post post)
        {
            //set the auto property values
            post.UserName = User.Identity.Name;
            post.DateCreated = DateTime.Now;
            post.Likes = 0;

            //make sure the image is nullable
            if (post.ImageURL == null)
            {
                post.ImageURL = "";
            }

            //add post to the database
            db.Posts.Add(post);
            //save the change
            db.SaveChanges();

            return RedirectToAction("Index", "Post");
        }
    }
}
