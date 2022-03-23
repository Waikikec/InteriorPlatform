namespace InteriorPlatform.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using InteriorPlatform.Data.Common.Repositories;
    using InteriorPlatform.Data.Models;

    public class LikesService : ILikesService
    {
        private readonly IRepository<Like> likesRepository;

        public LikesService(IRepository<Like> likesRepository)
        {
            this.likesRepository = likesRepository;
        }

        public int GetAllLikes(int projectId)
            => this.likesRepository.All()
            .Where(x => x.ProjectId == projectId)
            .Sum(x => x.Value);

        public async Task SetLikeAsync(int projectId, string userId, byte value)
        {
            var like = this.likesRepository
                .All()
                .FirstOrDefault(x => x.ProjectId == projectId && x.UserId == userId);

            if (like == null)
            {
                like = new Like
                {
                    ProjectId = projectId,
                    UserId = userId,
                };

                await this.likesRepository.AddAsync(like);
            }

            like.Value = value;
            await this.likesRepository.SaveChangesAsync();
        }
    }
}
