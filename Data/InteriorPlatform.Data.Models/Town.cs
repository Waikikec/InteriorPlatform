namespace InteriorPlatform.Data.Models
{
    using System.Collections.Generic;

    using InteriorPlatform.Data.Common.Models;

    public class Town : BaseDeletableModel<int>
    {
        public Town()
        {
            this.Projects = new HashSet<Project>();
        }

        public string Name { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
