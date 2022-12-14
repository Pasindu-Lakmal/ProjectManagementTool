using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        
        private readonly IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository, IMapper mapper, IPhotoService photoService)
        {
            _photoService = photoService;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        //endpoint for get all users
        
        [HttpGet]

        public async Task<ActionResult<PagedList<MemberDto>>> GetUsers([FromQuery]UserParams userParams)
        {
            var currentUser = await _userRepository.GetUserbyUsernameAsync(User.GetUsername());
            userParams.CurrentUsername = currentUser.UserName;

            if(string.IsNullOrEmpty(userParams.Gender))
            {
                userParams.Gender = currentUser.Gender =="male" ? "female" : "male";
            }

            var users = await _userRepository.GetMembersAsync(userParams);

            Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages));

            var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);

            return  Ok(usersToReturn);
        }

        //endpoint for get user by id
        // api/user/3(id)
         [HttpGet("{username}", Name = "GetUser") ]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            return await _userRepository.GetMemberAsync(username);
        }

        [HttpPut]
        public async Task<ActionResult>UpdateUser(MemberUpdateDto memberUpdateDto)
        {
            var user = await _userRepository.GetUserbyUsernameAsync(User.GetUsername());

            _mapper.Map(memberUpdateDto, user);

            _userRepository.Update(user);

            if( await _userRepository.SaveAllAsync())return NoContent();
        
            return BadRequest("Faild to update user");
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

        [HttpPut("set-main-photo/{photoId}")]
         public async Task<ActionResult>SetMainPhoto(int photoId)
        {
            var user = await _userRepository.GetUserbyUsernameAsync(User.GetUsername());

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

            if(photo.IsMain)return BadRequest("This is already your main photo");

            var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);

            if(currentMain != null) currentMain.IsMain = false;
            
            photo.IsMain = true;

            if(await _userRepository.SaveAllAsync()) return NoContent();

            return BadRequest ("Faild to set main Photo");
        }

        [HttpDelete("delete-photo/{photoId}")]
        public async Task<ActionResult> DeletePhoto(int photoId)
        {
            
            var user = await _userRepository.GetUserbyUsernameAsync(User.GetUsername());

            var photo = user.Photos.FirstOrDefault(x => x.Id == photoId);

            if(photo ==null) return NotFound();

            if(photo.IsMain) return BadRequest("You cannot delete your main photo");
            
            //remove from cloudinary
            if(photo.PublicId != null)
            {
                var result =  await _photoService.DeletephotoAsync(photo.PublicId);
                if(result.Error != null) return BadRequest(result.Error.Message);

            }

            //delete any type of user from database
            user.Photos.Remove(photo);

            //save changes
            if(await _userRepository.SaveAllAsync()) return Ok();

            return  BadRequest("Faild to delete photo");
        }

        [HttpGet("name")]

        public async Task<ActionResult<IEnumerable<NameDto>>> GetUsersName()
        {
          
            var users = await _userRepository.GetUsersNameAsync();
            var usersToReturn = _mapper.Map<IEnumerable<MemberDto>>(users);
            return  Ok(usersToReturn);
        }

    }
}