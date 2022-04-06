namespace InteriorPlatform.Services.Data
{
    using System.Threading.Tasks;

    using InteriorPlatform.Data.Models;

    public interface IImageDbService
    {
        Task<int> WriteToDatabaseAsync(CloudImage image);

        string GetPublicId(int id);
    }
}
