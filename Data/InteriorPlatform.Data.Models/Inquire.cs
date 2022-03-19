namespace InteriorPlatform.Data.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    using InteriorPlatform.Data.Common.Models;

    public class Inquire : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Info { get; set; }

        [ForeignKey(nameof(AddedByUser))]
        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
    }
}
