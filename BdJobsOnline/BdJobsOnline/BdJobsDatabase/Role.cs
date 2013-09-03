
namespace BdJobsOnline.BdJobsDatabase
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Role
    {
        public Role()
        {
            this.Users = new HashSet<User>();
        }
    
        public int RoleId { get; set; }

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }
    
        public virtual ICollection<User> Users { get; set; }
    }
}
