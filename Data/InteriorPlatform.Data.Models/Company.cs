namespace InteriorPlatform.Data.Models
{
    using System.Collections.Generic;

    using InteriorPlatform.Data.Common.Models;

    public class Company : BaseDeletableModel<int>
    {
        public Company()
        {
            this.Users = new HashSet<ApplicationUser>();
            this.Projects = new HashSet<Project>();
        }

        public string Name { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
