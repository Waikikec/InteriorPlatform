namespace InteriorPlatform.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using InteriorPlatform.Services.Data;
    using InteriorPlatform.Web.ViewModels.Likes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class LikesController : BaseController
    {
        private readonly ILikesService likesService;

        public LikesController(ILikesService likesService)
        {
            this.likesService = likesService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PostLikeResponseModel>> Post(PostLikeInputModel input)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.likesService.SetLikeAsync(input.ProjectId, userId, input.Value);
            var likesCount = this.likesService.GetAllLikes(input.ProjectId);
            return new PostLikeResponseModel { LikesCount = likesCount };
        }
    }
}
