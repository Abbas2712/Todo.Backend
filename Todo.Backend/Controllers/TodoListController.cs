using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.DataAccess.Data;
using Todo.Models;
using Todo.Models.DTOS;

namespace TodoBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        protected readonly TodoDBContext _todoDBContext;
        private readonly IMapper _mapper;
        public TodoListController(TodoDBContext todoDBContext, IMapper mapper)
        {
            _todoDBContext = todoDBContext;
            _mapper = mapper;
        }

        // GET: TodoList
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<TodoList>>> GetTodoLists()
        {
            try 
            {
                var TodoLists = await _todoDBContext.todoLists.Include(l=>l.Items).ToListAsync();
                if (TodoLists.Count == 0) return Ok("No TodoList Found");
                return Ok(TodoLists);
            }
            catch (Exception ex) 
            {
                throw new Exception(ex.ToString());
            }
        }

        // GET: TodoList/:id
        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<TodoList>> GetOneTodoList(int id)
        {
            try
            {
                if (id == 0) return BadRequest("Id cannot be empty");
                var getOneList = await _todoDBContext.todoLists.Include(_=> _.Items).Where(_ => _.Id == id).FirstOrDefaultAsync();
                if (getOneList == null) return NotFound($"The TodoList with id: {id} was not found");

                return Ok(getOneList);
            }
            catch (Exception ex) { throw new Exception(ex.ToString()); }
        }

        // POST: TodoListController/Create
        [HttpPost]
        [Route("createlist")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<TodoListDTO>> CreateTodoList(TodoListDTO todoListDTO)
        {
            try
            {
                if (todoListDTO == null) return BadRequest("Fields cannot be empty");
                if (!ModelState.IsValid) return BadRequest(ModelState);

                TodoList newTodoList = _mapper.Map<TodoList>(todoListDTO);

                await _todoDBContext.todoLists.AddAsync(newTodoList);
                await _todoDBContext.SaveChangesAsync();

                return Created();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [HttpPut("{id:int}")]
        [Route("updatelist")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdateTodoList(int id,TodoListDTO todoListDTO)
        {
            try 
            {
                if (!ModelState.IsValid) return BadRequest("Invalid Data or fields");
                var listexists = await _todoDBContext.todoLists.Where(_ => _.Id == id).FirstOrDefaultAsync();
                if (listexists == null) return NotFound("Todo list does not exists");

                TodoList todoListToUpdate = _mapper.Map<TodoList>(todoListDTO);
                todoListToUpdate.UpdatedAt = DateTime.Now;
                _todoDBContext.todoLists.Update(todoListToUpdate);
                await _todoDBContext.SaveChangesAsync();

                return NoContent();
            } catch (Exception ex) { throw new Exception(ex.ToString()); }
        }

        // POST: TodoList/:id
        [HttpDelete("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var deleteItem = _todoDBContext.todoLists.Where(_=> _.Id == id).FirstOrDefault();
                if (deleteItem == null) return NotFound("Todo item not found");
                _todoDBContext.todoLists.Remove(deleteItem);
                await _todoDBContext.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
