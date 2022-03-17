namespace InteriorPlatform.Web.ViewModels.Designer
{
    using System.Collections.Generic;

    public class AllDesignersViewModel : PagingViewModel
    {
        public IEnumerable<SingleDesignerViewModel> Designers { get; set; }
    }
}
