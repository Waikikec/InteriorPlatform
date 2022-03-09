namespace InteriorPlatform.Services.Data
{
    using System.Collections.Generic;

    public interface IStylesService
    {
        IEnumerable<KeyValuePair<string, string>> GetStyles();
    }
}
