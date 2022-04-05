namespace InteriorPlatform.Services
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudImageService : ICloudImageService
    {
        private readonly Cloudinary cloudUtility;

        public CloudImageService(Cloudinary cloudUtility)
        {
            this.cloudUtility = cloudUtility;
        }

        public async Task DeleteImages(params string[] publicIds)
        {
            var delParams = new DelResParams
            {
                PublicIds = publicIds.ToList(),
                Invalidate = true,
            };

            await this.cloudUtility.DeleteResourcesAsync(delParams);
        }

        public string GetImageUrl(string imagePublicId)
        {
            string imageUrl = this.cloudUtility
                                      .Api
                                      .UrlImgUp
                                      .Transform(new Transformation().Quality("auto"))
                                      .BuildUrl(string.Format("{0}.jpg", imagePublicId));

            return imageUrl;
        }

        public string GetProfilePic(string imagePublicId)
        {
            var imageUrl = this.cloudUtility
                     .Api
                     .UrlImgUp
                     .Transform(new Transformation()
                         .Height(200)
                         .Width(200)
                         .Gravity("faces")
                         .Crop("fill"))
                         .BuildUrl(string.Format("{0}.jpg", imagePublicId));

            return imageUrl;
        }

        public async Task<ImageUploadResult> UploadImageAsync(IFormFile imageFile)
        {
            byte[] destinationData;

            using (var ms = new MemoryStream())
            {
                await imageFile.CopyToAsync(ms);
                destinationData = ms.ToArray();
            }

            var fileName = imageFile.FileName;

            using (var ms = new MemoryStream(destinationData))
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(fileName, ms),
                };

                var uploadResult = await this.cloudUtility.UploadAsync(uploadParams);

                return uploadResult;
            }
        }
    }
}
