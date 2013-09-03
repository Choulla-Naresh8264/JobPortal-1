using System.Data;
using System.Linq;
using System.Web.Mvc;
using BdJobsOnline.BdJobsDatabase;

namespace BdJobsOnline.Controllers
{ 
    public class JobCategoryController : Controller
    {
        private BdJobsEntities db = new BdJobsEntities();

       
        [Authorize(Roles = "Admin")]
        public ViewResult Index()
        {
            return View(db.JobCategories.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ViewResult Details(int id)
        {
            JobCategory jobcategory = db.JobCategories.Find(id);
            return View(jobcategory);
        }

       
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        } 

        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(JobCategory jobcategory)
        {
            if (ModelState.IsValid)
            {
                db.JobCategories.Add(jobcategory);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(jobcategory);
        }
        
      
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            JobCategory jobcategory = db.JobCategories.Find(id);
            return View(jobcategory);
        }

        
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(JobCategory jobcategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobcategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobcategory);
        }

        
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            JobCategory jobcategory = db.JobCategories.Find(id);
            return null;
        }

        

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {            
            JobCategory jobcategory = db.JobCategories.Find(id);
            db.JobCategories.Remove(jobcategory);
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