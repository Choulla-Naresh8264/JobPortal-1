using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using BdJobsOnline.BdJobsDatabase;

namespace BdJobsOnline.Controllers
{ 
    public class UserController : Controller
    {
        private BdJobsEntities db = new BdJobsEntities();
        

        
        [Authorize(Roles = "Admin")]
        public ViewResult Index()
        {
            var users = db.Users.Include(u => u.Role);
            return View(users.ToList());
        }

        
        [Authorize(Roles = "Admin")]
        public ViewResult Details(int id)
        {
            User user = db.Users.Find(id);
            return View(user);
        }
        

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            User user = db.Users.Find(id);
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

       
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(db.Roles, "RoleId", "RoleName", user.RoleId);
            return View(user);
        }

       
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            User user = db.Users.Find(id);
            return null;
        }

       

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {            
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}