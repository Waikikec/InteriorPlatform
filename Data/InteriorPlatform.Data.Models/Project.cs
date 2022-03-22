namespace InteriorPlatform.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using InteriorPlatform.Data.Common.Models;

    public class Project : BaseDeletableModel<int>
    {
        public Project()
        {
            this.Favourites = new HashSet<ApplicationUser>();
            this.Images = new HashSet<Image>();
            this.Styles = new HashSet<Style>();
            this.Likes = new HashSet<Like>();
            this.Visits = 0;
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Visits { get; set; }

        public bool IsRealized { get; set; }

        public bool IsPublic { get; set; }

        public bool IsAwarded { get; set; }

        [Required]
        public string AddedByUserId { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }

        // Relationships
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [ForeignKey(nameof(Town))]
        public int TownId { get; set; }

        public virtual Town Town { get; set; }

        [ForeignKey(nameof(Company))]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        // Collections
        public virtual ICollection<ApplicationUser> Favourites { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Style> Styles { get; set; }

        public virtual ICollection<Like> Likes { get; set; }
    }
}
