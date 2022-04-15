namespace InteriorPlatform.Web.ViewModels.Search
{
    using System.Collections.Generic;

    public class SearchIndexInputModel
    {
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<string> Styles { get; set; }
    }
}
