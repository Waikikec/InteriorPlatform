namespace InteriorPlatform.Services.Data
{
    using System.Threading.Tasks;

    public interface ILikesService
    {
        Task SetLikeAsync(int projectId, string userId, byte value);

        int GetAllLikes(int projectId);
    }
}
