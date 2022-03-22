namespace InteriorPlatform.Services.Data
{
    using System;
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
            .Sum(x => (int)x.LikeType);

        public async Task SetLikeAsync(int projectId, string userId, byte value)
        {
            throw new NotImplementedException();
        }
    }
}
