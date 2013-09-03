
using System.ComponentModel.DataAnnotations;

namespace BdJobsOnline.BdJobsDatabase
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.JobDescriptions = new HashSet<JobDescription>();
        }
    
        public int UserId { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "password")]
        public string Password { get; set; }

        public int RoleId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }
    
        public virtual ICollection<JobDescription> JobDescriptions { get; set; }
        public virtual Role Role { get; set; }
    }
}
