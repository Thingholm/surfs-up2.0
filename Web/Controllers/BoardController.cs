// using EntityFramework.Infrastructure;
// using EntityFramework.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Utils;
using Web.Repositories;


namespace Web.Controllers
{
    public class BoardController : Controller
    {
        private readonly CartItemRepository _cartItemRepository;
        private readonly HttpClient _httpClient;
        public BoardController(CartItemRepository cartItemRepository, HttpClient httpClient)
        {
            _cartItemRepository = cartItemRepository;
            _httpClient = httpClient;
        }

        
        //[Route("produkter")]
        public async Task<IActionResult> Index()
        {
            string ApiUrl = "http://localhost:5281/api/boards";
            var response = await _httpClient.GetAsync(ApiUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var board = JsonConvert.DeserializeObject<List<Board>>(jsonData);
                return View(board);
            }
            return View("Error");

            // List<ProductType> productTypes = _dbContext.ProductTypes.ToList();
            // List<Board>? Board;
            // if (types == null)
            // {
            //     Board = _dbContext.Products.ToList();
            // }
            // else
            // {
            //     List<int> typeList = types.Contains(",") ? types.Split(",").Select(x => int.Parse(x)).ToList() : new List<int>() { 1 };
            //     Board = _dbContext.Products.Where(p => typeList.Any(pt => pt == p.ProductTypeId)).ToList();
            // }


        }


        //[Route("produkter/{id}/{name?}")]
        // public IActionResult Product(int id, string? name)
        // {
        //     Board? board = _dbContext.Products.Include(p => p.ProductType).FirstOrDefault(p => p.Id == id);
        //     if (board != null)
        //     {
        //         if (name == null)
        //         {
        //             string formattedString = StringFormatter.GenerateUrlSlug(board.Name);
        //             return RedirectToAction("Product", new { id = id, name = formattedString });
        //         }
        //         return View(board);
        //     }
        //     return View("Index");
        // }

        // [HttpPost]
        // public IActionResult AddToCart(int BoardId, int Amount)
        // {
        //     Board? board = _dbContext.Products.FirstOrDefault(b => b.Id == BoardId);
        //     if (board != null)
        //     {
        //         CartItem cartItem = new CartItem
        //         {
        //             Id = board.Id,
        //             Board = board,
        //             Amount = Amount
        //         };

        //         _cartItemRepository.AddCartItem(cartItem);
        //     }

        //     return RedirectToAction("Board", new { id = BoardId, name = StringFormatter.GenerateUrlSlug(_dbContext.Products.FirstOrDefault(p => p.Id == BoardId).Name) });
        // }
    }
}