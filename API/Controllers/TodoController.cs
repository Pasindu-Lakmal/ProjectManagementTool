using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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
            
            var assignee = await _userRepository.GetUserbyUsernameAsyncA(todo.Assignee);
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
            if(todos == null) return BadRequest("No Todos For This Work");             
            
            var work = await _workRepository.GetWorkByIdAsync(workId);
            
            var todosToReturn = _mapper.Map<IEnumerable<TodoDto>>(todos);
            return  Ok(todosToReturn);
        }
       
       [HttpGet("workName/{workId}")]
       public async Task<ActionResult<IEnumerable<TodoDto>>> GetWorkNameByWorkId(int workId)
       {
            var work = await _workRepository.GetWorkByIdAsync(workId);
            return Ok(work.WorkName);
       }

        
        [HttpDelete("delete/{todoId}")]
        public async Task<ActionResult> DeleteWork(int todoId)
        {
            var  todo = await _todoRepository.GetTodoByIdAsync(todoId);
            if(todo == null) return BadRequest("todo not Found");
            
            _todoRepository.DeleteTodo(todo);
            
            if(await _todoRepository.SaveAllAsync()) return Ok();
            return BadRequest("Delete Fail");

        }

       


    }
}