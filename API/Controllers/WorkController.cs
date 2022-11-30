using API.DTOs;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class WorkController:BaseApiController
    {
        private readonly IUserRepository _userRepository;
    
        private readonly IMapper _mapper;
        public WorkController(IUserRepository userRepository,  IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            
        }

       [HttpPost("add-photo")]
        public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
        {
            var user = await _userRepository.GetUserbyUsernameAsync(User.GetUsername());
            var result = await _photoService.AddPhotoAsync(file);
            if(result.Error != null) return BadRequest(result.Error.Message);
            var photo = new Photo
            {
                Url =result.SecureUrl.AbsoluteUri,
                PublicId = result.PublicId
            };

            if(user.Photos.Count == 0)
            {
                photo.IsMain = true;
            }

            user.Photos.Add(photo);

            if(await _userRepository.SaveAllAsync())
            {
                // return _mapper.Map<PhotoDto>(photo);
                //return 201 ok we user createAtRoute method
                return CreatedAtRoute("GetUser",new{username = user.UserName},_mapper.Map<PhotoDto>(photo) );
            }
                
            
            return BadRequest("Problem adding photo");

        }

    }
}