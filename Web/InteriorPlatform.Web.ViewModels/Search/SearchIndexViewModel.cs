namespace InteriorPlatform.Web.ViewModels.Search
{
    using System.Collections.Generic;

    public class SearchIndexViewModel
    {
        public string Name { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CategoriesItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> StylesItems { get; set; }

    }
}
