namespace InteriorPlatform.Services.Data
{
    using System.Collections.Generic;

    public interface IDesignersService
    {
        IEnumerable<T> GetAll<T>();

        T GetById<T>(string id);
    }
}
