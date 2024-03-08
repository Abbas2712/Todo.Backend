using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.DataAccess.Data;
using Todo.Models;

namespace TodoBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        protected readonly TodoDBContext _todoDBContext;
        public TodoItemController(TodoDBContext todoDBContext)
        {
            _todoDBContext = todoDBContext;
        }

        // GET: TodoItems
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<TodoItem>> GetAllTodosTitle()
        {
            var getAll = await _todoDBContext.todoItems.Select(i=> new { id = i.Id ,Title = i.Title }).ToListAsync();

            if (getAll.Count == 0) return NotFound("No Data Found");

            return Ok(getAll);
        }

        // GET: TodoItems
        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<TodoItem>> GetOneTodo(int id)
        {
            if (id == 0) return BadRequest("Id cannot be empty");
            var SingleTodoItem = await _todoDBContext.todoItems.Where(i => i.Id == id).FirstOrDefaultAsync();

            if (SingleTodoItem == null) return NotFound("No Data Found");

            return Ok(SingleTodoItem);
        }

        // POST: TodoItems/createitem
        [HttpPost]
        [Route("createitem")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<TodoItem>> CreateTodo(TodoItem todoItem)
        {
            if (todoItem == null) return BadRequest("Fields Cannot be empty");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _todoDBContext.AddAsync(todoItem);
            await _todoDBContext.SaveChangesAsync();
            return Created();
        }

        // PUT: TodoItems/updateitem
        [HttpPut]
        [Route("updateitem")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<TodoItem>> UpdateTodo(TodoItem todoItem)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var oldtodoitem = await _todoDBContext.todoItems.AsNoTracking().Where(todo => todo.Id == todoItem.Id).FirstOrDefaultAsync();

            if (oldtodoitem == null) return NotFound("Todo item not found");

            oldtodoitem.UpdatedAt = DateTime.Now;
            _todoDBContext.todoItems.Update(todoItem);
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

                _todoDBContext.Update(oldtodoitem);

                // saving todoitems in todolist 
                foreach(var op in patchDocument.Operations)
                {
                    var value = op.value; // value from jsonpatchdocuments
                    var todolist = await _todoDBContext.todoLists.Where(l=> l.Id == Convert.ToInt32(value)).FirstOrDefaultAsync();

                    if (todolist != null)
                    {
                        _todoDBContext.Attach(todolist);
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
        public async Task<ActionResult<TodoItem>> DeleteTodo(int id) 
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
