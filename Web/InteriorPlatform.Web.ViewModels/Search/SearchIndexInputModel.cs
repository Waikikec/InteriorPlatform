namespace InteriorPlatform.Web.ViewModels.Search
{
    using System.Collections.Generic;

    public class SearchIndexInputModel
    {
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<int> Styles { get; set; }
    }
}
