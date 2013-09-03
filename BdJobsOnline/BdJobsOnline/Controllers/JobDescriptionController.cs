using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using BdJobsOnline.BdJobsDatabase;
using BdJobsOnline.Models;

namespace BdJobsOnline.Controllers
{
    public class JobDescriptionController : Controller
    {
        private BdJobsEntities db = new BdJobsEntities();


        public ViewResult Index()
        {
            var jobdescriptions = db.JobDescriptions.Include(j => j.JobCategory).Include(j => j.User);

            return View(jobdescriptions.ToList());
        }


        public ActionResult AjaxHandler(jQueryDataTableParamModel param)
        {

            var list = db.JobDescriptions.ToList();

            int jobCatagoryId;
            if (int.TryParse(param.sSearch, out jobCatagoryId))
            {
                list = list.Where(description => description.JobCategoryId == jobCatagoryId).ToList();
            }

            IEnumerable<JobDescription> filteredCompanies = list;

            var displayedCompanies = filteredCompanies
                                .Skip(param.iDisplayStart)
                                .Take(param.iDisplayLength);

            var result = from c in displayedCompanies
                         select new string[] { c.CompanyName, BuildJobLink(c.Title, c.JobId), c.EducationalQualification, c.LastDate.ToString() };
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = list.Count(),
                iTotalDisplayRecords = filteredCompanies.Count(),
                aaData = result
            },
                                JsonRequestBehavior.AllowGet);
        }

        private string BuildJobLink(string jobTitle, int jobId)
        {
            return string.Format("<a href={0}>{1}</a>", Url.Action("Details", "JobDescription", new { id = jobId }), jobTitle);
        }

        public ActionResult CategoryFilter()
        {
            var allFilters = db.JobCategories;
            return PartialView(allFilters);
        }



        public ViewResult Details(int id)
        {
            JobDescription jobdescription = db.JobDescriptions.Find(id);
            return View(jobdescription);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.JobCategoryId = new SelectList(db.JobCategories, "JobCategoryId", "CategoryName");

            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(JobDescription jobdescription)
        {
            if (ModelState.IsValid)
            {
                db.JobDescriptions.Add(jobdescription);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JobCategoryId = new SelectList(db.JobCategories, "JobCategoryId", "CategoryName", jobdescription.JobCategoryId);
            ViewBag.UserId = db.Users.First(u => u.UserName == User.Identity.Name).UserId;
            return View(jobdescription);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            JobDescription jobdescription = db.JobDescriptions.Find(id);
            ViewBag.JobCategoryId = new SelectList(db.JobCategories, "JobCategoryId", "CategoryName", jobdescription.JobCategoryId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", jobdescription.UserId);
            return View(jobdescription);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(JobDescription jobdescription)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobdescription).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JobCategoryId = new SelectList(db.JobCategories, "JobCategoryId", "CategoryName", jobdescription.JobCategoryId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", jobdescription.UserId);
            return View(jobdescription);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            JobDescription jobdescription = db.JobDescriptions.Find(id);
            return null;
        }


        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            JobDescription jobdescription = db.JobDescriptions.Find(id);
            db.JobDescriptions.Remove(jobdescription);
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