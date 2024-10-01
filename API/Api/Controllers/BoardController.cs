using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]s")]
public class BoardController : ControllerBase
{
    private readonly ILogger<BoardController> _logger;
    private readonly ProductsDbContext _db;

    public BoardController(ILogger<BoardController> logger, ProductsDbContext db)
    {
        _logger = logger;
        _db = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<Board>>> Get()
    {
        List<Board> boards = await _db.Boards
            .Include(e => e.BoardEquipment)
                .ThenInclude(e => e.Equipment)
            .ToListAsync(); 

        return Ok(boards);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Board>> Get(int id)
    {
        Board? board = await _db.Boards
            .Include(e => e.BoardEquipment)
                .ThenInclude(e => e.Equipment)
            .FirstOrDefaultAsync(e => e.Id == id);
        
        if (board is null)
            return NotFound();

        return Ok(board);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Board board)
    {
        await _db.Boards.AddAsync(board);

        int rowsAffected = await _db.SaveChangesAsync();

        if (rowsAffected == 0)
            return BadRequest();

        return Created(board.Id.ToString(), board);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Board board)
    {
        if (id != board.Id)
            return BadRequest();
        
        _db.Entry(board).State = EntityState.Modified;

        try
        {
            await _db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Board? board = await _db.Boards.FirstOrDefaultAsync(e => e.Id == id);

        if (board is null)
            return NotFound();

        _db.Boards.Remove(board);

        await _db.SaveChangesAsync();

        return NoContent();
    }
}