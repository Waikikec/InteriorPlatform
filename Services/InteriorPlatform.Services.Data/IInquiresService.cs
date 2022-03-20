namespace InteriorPlatform.Services.Data
{
    using System.Collections.Generic;

    public interface IInquiresService
    {
        IEnumerable<T> GetAllInquiresByUserId<T>(string id);
    }
}
