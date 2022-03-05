namespace InteriorPlatform.Data.Models
{
    using InteriorPlatform.Data.Common.Models;

    public class Position : BaseDeletableModel<int>
    {
        public string Name { get; set; }
    }
}
