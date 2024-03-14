using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.DataAccess.Data;
using Todo.Models;
using Todo.Models.DTOS;

namespace TodoBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        protected readonly TodoDBContext _todoDBContext;
        private readonly IMapper _mapper;
        public TodoItemController(TodoDBContext todoDBContext, IMapper mapper)
        {
            _todoDBContext = todoDBContext;
            _mapper = mapper;
        }

        // GET: TodoItems
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetAllTodosTitle()
        {
            var getAll = await _todoDBContext.todoItems.ToListAsync();

            if (getAll.Count == 0) return NotFound("No Todo's Found");

            var itemDTO = _mapper.Map<List<TodoItemDTO>>(getAll);

            return Ok(itemDTO);
        }

        // GET: TodoItems/important
        [HttpGet]
        [Route("important")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetAllImportantTodosTitle()
        {
            var getAll = await _todoDBContext.todoItems.Where(_=> _.IsImportant == true).ToListAsync();

            if (getAll.Count == 0) return NotFound("No Todo's Found");

            var itemDTO = _mapper.Map<List<TodoItemDTO>>(getAll);

            return Ok(itemDTO);
        }

        // GET: TodoItems/daily
        [HttpGet]
        [Route("daily")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<TodoItemDTO>>> GetAllDailyTodosTitle()
        {
            var getAll = await _todoDBContext.todoItems.Where(_ => _.IsTodayTodo == true).ToListAsync();

            if (getAll.Count == 0) return NotFound("No Todo's Found");

            var itemDTO = _mapper.Map<List<TodoItemDTO>>(getAll);

            return Ok(itemDTO);
        }
        
        // GET: TodoItems/:id
        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<TodoItemDTO>> GetOneTodo(int id)
        {
            if (id == 0) return BadRequest("Id cannot be empty");
            var SingleTodoItem = await _todoDBContext.todoItems.Where(i => i.Id == id).FirstOrDefaultAsync();

            if (SingleTodoItem == null) return NotFound("No Data Found");

            var SingleTodoItemDTO = _mapper.Map<TodoItemDTO>(SingleTodoItem);

            return Ok(SingleTodoItemDTO);
        }

        // POST: TodoItems/createitem
        [HttpPost]
        [Route("createitem")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<TodoItemDTO>> CreateTodo(TodoItemDTO todoItemDTO)
        {
            if (todoItemDTO == null) return BadRequest("Fields Cannot be empty");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            TodoItem todoItem = _mapper.Map<TodoItem>(todoItemDTO);
            todoItem.ListId = null;

            await _todoDBContext.todoItems.AddAsync(todoItem);
            await _todoDBContext.SaveChangesAsync();
            return Created();
        }

        // PUT: TodoItems/updateitem/:id
        [HttpPut("updateitem/{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<TodoItemDTO>> UpdateTodo(int id ,TodoItemDTO todoItemDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var doesitemexists = await _todoDBContext.todoItems.AsNoTracking().Where(todo => todo.Id == id).FirstOrDefaultAsync();

            if (doesitemexists == null) return NotFound("Todo item not found");

            TodoItem todoItemToUpdate = _mapper.Map<TodoItem>(todoItemDTO);
            todoItemToUpdate.UpdatedAt = DateTime.Now;
            _todoDBContext.todoItems.Update(todoItemToUpdate);
            await _todoDBContext.SaveChangesAsync();
            return NoContent();
        }

        // PATCH: TodoItems/:id
        [HttpPatch("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UpdatePartialTodo(int id,[FromBody] JsonPatchDocument<TodoItem> patchDocument) 
        {
            try 
            {
                if (patchDocument == null) return BadRequest("Fields Cannot be Null");
                var oldtodoitem = _todoDBContext.todoItems.Where(i => i.Id == id).FirstOrDefault();
                if (oldtodoitem == null) return NotFound("Todo item not found");

                patchDocument.ApplyTo(oldtodoitem, ModelState); // Applying the patchDocument data to todoitems data with model state to check the errors if any

                if (!ModelState.IsValid) return BadRequest(ModelState);
                oldtodoitem.UpdatedAt = DateTime.Now;
                _todoDBContext.Update(oldtodoitem);

                // saving todoitems in todolist 
                foreach(var op in patchDocument.Operations)
                {
                    var value = op.value; // value from jsonpatchdocuments
                    var todolist = await _todoDBContext.todoLists.Where(l=> l.Id == Convert.ToInt32(value)).FirstOrDefaultAsync();

                    if (todolist != null)
                    {
                        _todoDBContext.Attach(todolist);
                        todolist.UpdatedAt = DateTime.Now;
                        todolist.Items.Add(oldtodoitem);
                    }
                }

                await _todoDBContext.SaveChangesAsync();
                return NoContent();

            } catch (Exception ex)
            { 
                throw new Exception(ex.ToString());
            }
            
        }

        // DELETE: TodoItems/:id
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteTodo(int id) 
         {
            if (id == 0) return BadRequest("id cannot be empty");

            var deleteTodo = await _todoDBContext.todoItems.Where(todo=> todo.Id == id).FirstOrDefaultAsync();

            if (deleteTodo == null) return NotFound("Todo item Not found");

            _todoDBContext.todoItems.Remove(deleteTodo);
            await _todoDBContext.SaveChangesAsync();

            return NoContent();
         }
    }
}
