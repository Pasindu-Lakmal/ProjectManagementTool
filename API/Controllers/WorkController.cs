using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{   
    [Authorize]
    public class WorkController: BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IWorkRepository _workRepository;
        private readonly IMapper _mapper;
        public WorkController(IUserRepository userRepository,IWorkRepository workRepository, IMapper mapper)
        {
            _mapper = mapper;
            _workRepository = workRepository;
            _userRepository = userRepository;
             
        }

        [HttpPost]
        public async Task<ActionResult>AddWork(WorkDto work)
        {
            var sourceUserId = User.GetUserId();
            var user = await _userRepository.GetUserbyUsernameAsyncA(User.GetUsername());

            
            var newWork = new Work
            {
                CreaterName = user.UserName,
                AppUser = user,
                WorkName = work.WorkName,
                WorkDescription = work.WorkDescription,
            };
           
            _workRepository.AddWork(newWork);
            if(await _workRepository.SaveAllAsync()) return Ok();
            return BadRequest("Failed to add work");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkDetailsDto>>> GetWorks()
        {
            var sourceUser = User.GetUsername();
            if(sourceUser == null) return BadRequest("user Not Found");
            var works = await _workRepository.GetWorks();
            var usersToReturn = _mapper.Map<IEnumerable<WorkDetailsDto>>(works);
            return  Ok(usersToReturn);
        }

        [HttpDelete("delete/{workId}")]

        public async Task<ActionResult> DeleteWork(int workId)
        {
            var  work = await _workRepository.GetWorkByIdAsync(workId);
            
            if(work == null) return BadRequest("Work not Found");
            
            _workRepository.DeleteWork(work);
            
            if(await _workRepository.SaveAllAsync()) return Ok();
            return BadRequest("Delete Fail");

        }

        [HttpGet("{workId}")]
        public async Task<ActionResult<WorkDetailsDto>> GetWork(int workId)
        {
          
            var work = await _workRepository.GetWorkByIdAsync(workId);
            var workToReturn = _mapper.Map<WorkDetailsDto>(work);
            return  Ok(workToReturn);
        }

        [HttpPut]
        public async Task<ActionResult>UpdateUser(WorkUpdateDto workUpdateDto)
        {
            var work = await _workRepository.GetWorkByIdAsync(workUpdateDto.WorkId);

            _mapper.Map(workUpdateDto, work);

            _workRepository.Update(work);

            if( await _workRepository.SaveAllAsync())return NoContent();
        
            return BadRequest("Faild to update user");
        }



    }
}
