using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TodoController : BaseApiController
    {
        private readonly IWorkRepository _workRepository;
        private readonly IUserRepository _userRepository;
        public TodoController(IUserRepository userRepository,IWorkRepository workRepository)
        {
            _userRepository = userRepository;
            _workRepository = workRepository;
        }

        [HttpPost]
        public async Task<ActionResult>AddWork(TodoDto work)
        {
            var sourceUserId = User.GetUserId();
            var user = await _userRepository.GetUserbyUsernameAsyncA(User.GetUsername());
            return Ok();
        //add to todo entity < - creater name 
            
        //     var newWork = new Work
        //     {
        //         CreaterName = user.UserName,
        //         AppUser = user,
        //         WorkName = work.WorkName,
        //         WorkDescription = work.WorkDescription,
        //     };
           
        //     _workRepository.AddWork(newWork);
        //     if(await _workRepository.SaveAllAsync()) return Ok();
        //     return BadRequest("Failed to add work");
        // }
        }

       
    }
}