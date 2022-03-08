namespace InteriorPlatform.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class UserProject
    {
        public UserProject()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
