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
    private readonly IWebHostEnvironment _env;  // To get the environment for saving files

    public BoardController(ILogger<BoardController> logger, ProductsDbContext db, IWebHostEnvironment env)
    {
        _logger = logger;
        _db = db;
        _env = env;
    }

    [HttpGet]
    public async Task<ActionResult<List<Board>>> Get()
    {
        List<Board> boards = await _db.Boards
            .Include(e => e.BoardEquipment)
                .ThenInclude(e => e.Equipment)
            .ToListAsync(); 

        fixBoards(boards);

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
        
        return Ok(fixBoard(board));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromForm] IFormFile image, [FromForm] string name, [FromForm] double length, [FromForm] double width, [FromForm] double thickness, [FromForm] double volume, [FromForm] double price, [FromForm] int boardTypeId)
    {
        // Handle board type validation
        var boardType = await _db.BoardTypes.FindAsync(boardTypeId);
        if (boardType == null)
        {
            return BadRequest("Invalid BoardTypeId.");
        }

        // Upload the image and save its URL
        string imageUrl = null;
        if (image != null && image.Length > 0)
        {
            var supportedTypes = new[] { "jpg", "jpeg", "png", "webp" };
            var extension = Path.GetExtension(image.FileName).Substring(1);
            if (!supportedTypes.Contains(extension.ToLower()))
            {
                return BadRequest("Invalid file type. Only images are allowed.");
            }

            // Save image to wwwroot/images folder
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            var filePath = Path.Combine(_env.WebRootPath, "images", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            imageUrl = $"/images/{fileName}";  // Set the image URL
        }

        // Create the board with the image URL
        var board = new Board
        {
            Name = name,
            Length = length,
            Width = width,
            Thickness = thickness,
            Volume = volume,
            Price = price,
            BoardType = boardType,
            ImageUrl = imageUrl // Save the image URL
        };

        await _db.Boards.AddAsync(board);
        await _db.SaveChangesAsync();

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

    private Board fixBoard(Board board) {
        
        
        if (board != null) {
            string url = board.ImageUrl;
            
            if (url !=null && !url.StartsWith("/images/")) {
                url = "/images/" + url;
                board.ImageUrl = url;
            }
        }
        return board;
    }

    private List<Board> fixBoards(List<Board> boards) {
        if (boards != null) {

            foreach (Board board in boards) {
                fixBoard(board);
            }
        }
        return boards;
    }
}
