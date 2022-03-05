namespace InteriorPlatform.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using InteriorPlatform.Data.Common.Models;

    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Extension { get; set; }

        public string RemoteImageUrl { get; set; }

        [ForeignKey(nameof(AddedByUser))]
        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        [ForeignKey(nameof(Project))]
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }
    }
}
