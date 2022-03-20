namespace InteriorPlatform.Web.ViewModels.Inquire
{
    using InteriorPlatform.Data.Models;
    using InteriorPlatform.Services.Mapping;

    public class InquireViewModel : IMapFrom<Inquire>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Info { get; set; }
    }
}
