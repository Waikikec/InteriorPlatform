namespace InteriorPlatform.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using InteriorPlatform.Data.Common.Repositories;
    using InteriorPlatform.Data.Models;

    public class ImageDbService : IImageDbService
    {
        private readonly IRepository<CloudImage> imagesRepository;

        public ImageDbService(IRepository<CloudImage> imagesRepository)
        {
            this.imagesRepository = imagesRepository;
        }

        public string GetPublicId(int id)
        {
            var image = this.imagesRepository
                .AllAsNoTracking()
                .FirstOrDefault(x => x.Id == id);

            return image.PicturePublicId;
        }

        public async Task<int> WriteToDatabaseAsync(string imageUrl, string imagePublicId)
        {
            var image = new CloudImage
            {
                PictureUrl = imageUrl,
                PicturePublicId = imagePublicId,
            };

            await this.imagesRepository.AddAsync(image);
            await this.imagesRepository.SaveChangesAsync();

            return image.Id;
        }
    }
}
