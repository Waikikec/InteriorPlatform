namespace InteriorPlatform.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using InteriorPlatform.Data.Common.Models;

    public class Like : BaseModel<int>
    {
        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        [ForeignKey(nameof(User))]
        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public LikeType LikeType { get; set; }
    }
}
