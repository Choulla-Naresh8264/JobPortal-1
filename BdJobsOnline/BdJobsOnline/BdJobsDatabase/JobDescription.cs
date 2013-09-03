
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BdJobsOnline.BdJobsDatabase
{
    using System;
    using System.Collections.Generic;
    
    public partial class JobDescription
    {
        public int JobId { get; set; }

         [Display(Name = "Job Title")]
        public string Title { get; set; }

        [Display(Name = "No of Vacancy")]
        public string Vacancy { get; set; }

        [Display(Name = "Job Type")]
        public string Type { get; set; }

         [Display(Name = "Job Level")]
        public string JobLevel { get; set; }

        [Display(Name = "Job Location")]
        public string Location { get; set; }

        public int JobCategoryId { get; set; }

         [Display(Name = "Age Range")]
        public string Age { get; set; }

         [UIHint("tinymce_jquery_full"), AllowHtml]
         [Display(Name = "Education")]
        public string EducationalQualification { get; set; }

         [UIHint("tinymce_jquery_full"), AllowHtml]
         [Display(Name = "Additional Requirement")]
        public string AdditionalRequirement { get; set; }

        [UIHint("tinymce_jquery_full"), AllowHtml]
        [Display(Name = "Experience")]
        public string Experience { get; set; }

        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }

        public int UserId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        [Display(Name = "Last Date")]
        public System.DateTime LastDate { get; set; }

        public virtual JobCategory JobCategory { get; set; }
        public virtual User User { get; set; }
    }
}
