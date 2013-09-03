using System.Data;
using System.Linq;
using System.Web.Mvc;
using BdJobsOnline.BdJobsDatabase;

namespace BdJobsOnline.Controllers
{ 
    public class RoleController : Controller
    {
        private BdJobsEntities db = new BdJobsEntities();

        
        [Authorize(Roles = "Admin")]
        public ViewResult Index()
        {
            return View(db.Roles.ToList());
        }

        
        [Authorize(Roles = "Admin")]
        public ViewResult Details(int id)
        {
            Role role = db.Roles.Find(id);
            return View(role);
        }

       
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        } 

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(role);
        }
        
        
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            Role role = db.Roles.Find(id);
            return View(role);
        }

      
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Role role = db.Roles.Find(id);
            return null;
        }

        
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Role role = db.Roles.Find(id);
            db.Roles.Remove(role);
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