using System.Diagnostics;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Authorize]
    public class TodoController : BaseApiController
    {
        private readonly ITodoRepository _todoRepository;
      
        private readonly IMapper _mapper;
        private readonly IWorkRepository _workRepository;
      
        private readonly IUserRepository _userRepository;
        public TodoController(IUserRepository userRepository,ITodoRepository todoRepository, IMapper mapper,IWorkRepository workRepository)
        {
            _workRepository = workRepository;
            _mapper = mapper;
            _todoRepository = todoRepository;
            _userRepository = userRepository;
         
        }

        [HttpPost]
        public async Task<ActionResult>AddTodo(TodoDto todo)
        {
            var sourceUserId = User.GetUserId();
            var creater = await _userRepository.GetUserbyUsernameAsyncA(User.GetUsername());

            var work = await _workRepository. GetWorkByIdAsync(todo.WorkId);
            if(work == null) return BadRequest("Project Not Found");
            
            var assignee = await _userRepository.GetUserbyUsernameAsyncA(todo.AssigneeName);
            if(assignee == null) return BadRequest("assignee Not Found");
            
        //add to todo entity < - creater name 
            
            var newTodo = new Todo
            {
                Title = todo.Title,
                Description = todo.Description,
                DueDate = todo.DueDate,
                status = "pending",
                CreaterName = creater.UserName,
                Works = work,
                AssigneeName = assignee.UserName,
                AppUser = assignee,
            };
         
            _todoRepository.AddTodo(newTodo);
            if(await _todoRepository.SaveAllAsync()) return Ok();
            return BadRequest("Failed to add work");
        
        }


        [HttpGet("{workId}")]
         public async Task<ActionResult<IEnumerable<TodoDto>>> GetTodosByWorkId(int workId)
        {
            var todos = await _todoRepository.GetTodoByWorkIdAsync(workId);
            
            var todosToReturn = _mapper.Map<IEnumerable<TodoDto>>(todos);
            // return Ok(todos);
            return  Ok(todosToReturn);
        }
       
       [HttpGet("workName/{workId}")]
       public async Task<ActionResult> GetWorkNameByWorkId(int workId)
       {

            var work = await _workRepository.GetWorkByIdAsync(workId);
            return Ok(JsonConvert.SerializeObject(work.WorkName));
       }

        
        [HttpDelete("delete/{todoId}")]
        public async Task<ActionResult> DeleteTodo(int todoId)
        {
            var  todo = await _todoRepository.GetTodoByIdAsync(todoId);

            if(todo == null) return BadRequest("todo not Found");
            
            _todoRepository.DeleteTodo(todo);
            
            if(await _todoRepository.SaveAllAsync()) return Ok();
            return BadRequest("Delete Fail");

        }

        
        [HttpGet("assigneeId/{assigneeId}")]
         public async Task<ActionResult<IEnumerable<TodoDto>>> GetTodosByAssigneeId(int assigneeId)
        {
            var todos = await _todoRepository.GetTodoByAssigneeIdAsync(assigneeId);
            
            var todosToReturn = _mapper.Map<IEnumerable<TodoDto>>(todos);
            // return Ok(todos);
            return  Ok(todosToReturn);
        }
        
        
        [HttpGet("assigneeName/{assigneename}")]
         public async Task<ActionResult<IEnumerable<TodoDto>>> GetTodosByAssigneeName(string assigneename)
        {
            var todos = await _todoRepository.GetTodoByAssigneeName(assigneename);
            
            var todosToReturn = _mapper.Map<IEnumerable<TodoDto>>(todos);
            // return Ok(todos);
            return  Ok(todosToReturn);
        }


      


        
        [HttpPut]
        public async Task<ActionResult>UpdateUser(TodoUpdateDto todoUpdateDto)
        {
            var todo = await _todoRepository.GetTodoByIdAsync(todoUpdateDto.TodoId);

            _mapper.Map(todoUpdateDto, todo);

            _todoRepository.Update(todo);

            if( await _todoRepository.SaveAllAsync())return NoContent();
        
            return BadRequest("Faild to update user");
        }
       

         [HttpGet("todobyId/{todoId}")]
        public async Task<ActionResult<TodoDto>> GetTodo(int todoId)
        {
          
            var todo = await _todoRepository.GetTodoByIdAsync(todoId);
            var todoToReturn = _mapper.Map<TodoDto>(todo);
            return  Ok(todoToReturn);
        }

        [HttpGet("CreaterName/{createrName}")]
         public async Task<ActionResult<IEnumerable<TodoDto>>> GetTodosByCreaterName(string createrName)
        {
            var todos = await _todoRepository.GetTodoByCreaterName(createrName);
            
            var todosToReturn = _mapper.Map<IEnumerable<TodoDto>>(todos);
            // return Ok(todos);
            return  Ok(todosToReturn);
        }

    }
}