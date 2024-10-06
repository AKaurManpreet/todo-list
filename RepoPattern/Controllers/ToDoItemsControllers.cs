using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemsController : ControllerBase
{
    private readonly DataContext _context;

    public ToDoItemsController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _context.ToDoItems
            .Where(x => !x.CompletedDate.HasValue)
            .ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _context.ToDoItems.FindAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ToDoItem item)
    {
        await _context.ToDoItems.AddAsync(item);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Complete(int id)
    {
        var item = await _context.ToDoItems.FindAsync(id);
        if (item == null) return NotFound();

        item.CompletedDate = DateTime.UtcNow;
        _context.ToDoItems.Update(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.ToDoItems.FindAsync(id);
        if (item == null) return NotFound();

        _context.ToDoItems.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}