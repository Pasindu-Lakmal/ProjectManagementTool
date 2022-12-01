using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class NewController: BaseApiController
    {
        private readonly IUserRepository _userRepository;
        public NewController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            
        }
        
        [HttpPost("register")]
        public async Task<ActionResult>AddLikes(){
            var sourceUserId = User.GetUserId();
            var likedUser = await _userRepository.GetUserbyUsernameAsync("lisa");
            return Ok(sourceUserId);

        }
    }
}