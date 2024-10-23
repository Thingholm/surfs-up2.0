using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Repositories;


namespace Web.Controllers
{
    [Route("kurv")]
    public class CartController : Controller
    {
        private readonly CartItemRepository _cartItemRepository;
        public CartController(CartItemRepository cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }
        public IActionResult Index()
        {
            List<CartItem> cartItems = _cartItemRepository.GetAllCartItems();
            return View(cartItems);
        }

        [HttpPost]
        [Route("delete-item")]
        public IActionResult DeleteItem(int id)
        {
            _cartItemRepository.DeleteCartItem(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Route("increase-amount")]
        public IActionResult IncreaseAmount(int id)
        {
            CartItem? cartItemToBeUpdated = _cartItemRepository.GetCartItemById(id);
            if (cartItemToBeUpdated != null)
            {
                cartItemToBeUpdated.Amount++;
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Route("decrease-amount")]
        public IActionResult DecreaseAmount(int id)
        {
            CartItem? cartItemToBeUpdated = _cartItemRepository.GetCartItemById(id);
            if (cartItemToBeUpdated != null)
            {
                cartItemToBeUpdated.Amount--;
            }
            return RedirectToAction("Index");
        }

        

    }
}