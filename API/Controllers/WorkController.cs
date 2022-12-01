


using API.Data;
using API.DTOs;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class WorkController: BaseApiController
    {
        private readonly IUserRepository _userRepository;
        // private readonly IWorkRepository _workRepository,IWorkRepository workRepository,;
        private readonly DataContext _context;
        public WorkController(IUserRepository userRepository,DataContext context)
        {
            _context = context;
            // _workRepository = workRepository;
            _userRepository = userRepository;
             
        }
        
        
        [HttpPost]
        public async Task<ActionResult>AddWork(WorkDto work)
        {
            var sourceUserId = User.GetUserId();
            var user = await _userRepository.GetUserbyUsernameAsyncA(User.GetUsername());

            // return Ok(user.UserName);
            var newWork = new Entities.Work
            {
                AppUser =user,
                WorkName = work.WorkName,
                WorkDescription = work.WorkDescription, 
            };
        
            _context.Works.Add(newWork);
            if(await _userRepository.SaveAllAsync()) return Ok("added");
            return BadRequest("Failed to add work");
        }







        // [HttpPost]
        // public async Task<ActionResult>AddWork(WorkDto work)
        // {
        //     var sourceUserId = User.GetUserId();
        //     var user = await _userRepository.GetUserbyUsernameAsyncA(User.GetUsername());

            
        //     var newWork = new Entities.Work
        //     {
        //         AppUser = user,
        //         WorkName = work.WorkName,
        //         WorkDescription = work.WorkDescription,
        //     };
           
        //     user.Works.Add(newWork);
        //     if(await _userRepository.SaveAllAsync()) return Ok();
        //     return BadRequest("Failed to add work");
        // }



    }
}