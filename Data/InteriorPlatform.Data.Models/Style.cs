namespace InteriorPlatform.Data.Models
{
    using System.Collections.Generic;

    using InteriorPlatform.Data.Common.Models;

    public class Style : BaseDeletableModel<int>
    {
        public Style()
        {
            this.Projects = new List<Project>();
        }

        public string Name { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
