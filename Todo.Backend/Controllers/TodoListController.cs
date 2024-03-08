using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo.DataAccess.Data;
using Todo.Models;

namespace TodoBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        protected readonly TodoDBContext _todoDBContext;
        public TodoListController(TodoDBContext todoDBContext)
        {
            _todoDBContext = todoDBContext;
        }

        // GET: TodoListController
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<TodoList>> GetTodoLists()
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

        // POST: TodoListController/Create
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]

        public async Task<ActionResult<TodoList>> CreateTodoList(TodoList todoList)
        {
            try
            {
                if (todoList == null) return BadRequest("Fields cannot be empty");
                if (!ModelState.IsValid) return BadRequest(ModelState);

                await _todoDBContext.todoLists.AddAsync(todoList);
                await _todoDBContext.SaveChangesAsync();

                return Created();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        // POST: TodoListController/Edit/5
        /*[HttpPost]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/

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
