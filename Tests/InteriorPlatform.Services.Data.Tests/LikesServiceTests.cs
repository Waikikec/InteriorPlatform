namespace InteriorPlatform.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using InteriorPlatform.Data.Common.Repositories;
    using InteriorPlatform.Data.Models;
    using Moq;
    using Xunit;

    public class LikesServiceTests
    {
        [Fact]
        public async Task WhenUserVotes2TimesOnly1VoteShouldBeCounted()
        {
            var list = new List<Like>();
            var mockRepo = new Mock<IRepository<Like>>();

            mockRepo
                .Setup(x => x.All())
                .Returns(list.AsQueryable());
            mockRepo
                .Setup(x => x.AddAsync(It.IsAny<Like>()))
                .Callback((Like like) => list.Add(like));

            var service = new LikesService(mockRepo.Object);

            await service.SetLikeAsync(projectId: 1, userId: "Martin", value: 1);
            await service.SetLikeAsync(projectId: 1, userId: "Martin", value: 0);
            await service.SetLikeAsync(projectId: 1, userId: "Martin", value: 1);
            await service.SetLikeAsync(projectId: 1, userId: "Martin", value: 0);
            await service.SetLikeAsync(projectId: 1, userId: "Martin", value: 1);

            Assert.Single(list);
            Assert.Equal(1, list.First().Value);
        }

        [Fact]
        public async Task When2UsersLikeTheSameProjectTheAverageVoteShouldBeCorrect()
        {
            var list = new List<Like>();
            var mockRepo = new Mock<IRepository<Like>>();

            mockRepo
                .Setup(x => x.All())
                .Returns(list.AsQueryable());

            mockRepo
                .Setup(x => x.AddAsync(It.IsAny<Like>()))
                .Callback((Like like) => list.Add(like));

            var service = new LikesService(mockRepo.Object);

            await service.SetLikeAsync(projectId: 2, userId: "Martin", value: 1);
            await service.SetLikeAsync(projectId: 2, userId: "Teodor", value: 1);
            await service.SetLikeAsync(projectId: 2, userId: "Teodor", value: 0);

            mockRepo.Verify(x => x.AddAsync(It.IsAny<Like>()), Times.Exactly(2));

            Assert.Equal(2, list.Count);
            Assert.Equal(1, service.GetAllLikes(2));
        }
    }
}
