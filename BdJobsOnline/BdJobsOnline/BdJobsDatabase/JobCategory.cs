
using System.ComponentModel.DataAnnotations;

namespace BdJobsOnline.BdJobsDatabase
{
    using System;
    using System.Collections.Generic;
    
    public partial class JobCategory
    {
        public JobCategory()
        {
            this.JobDescriptions = new HashSet<JobDescription>();
        }
    
        public int JobCategoryId { get; set; }

        [Display(Name = "Job Category Name")]
        public string CategoryName { get; set; }
    
        public virtual ICollection<JobDescription> JobDescriptions { get; set; }
    }
}
