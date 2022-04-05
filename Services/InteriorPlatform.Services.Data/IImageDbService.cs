namespace InteriorPlatform.Services.Data
{
    using System.Threading.Tasks;

    public interface IImageDbService
    {
        Task<int> WriteToDatabaseAsync(string imageUrl, string imagePublicId);

        string GetPublicId(int id);
    }
}
