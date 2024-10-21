using Web.Models;

namespace Web.Repositories
{
    public class CartItemRepository
    { 
        private List<CartItem> cartItems = new List<CartItem>();

        public void AddCartItem(CartItem cartItem)
        {
            cartItems.Add(cartItem);
        }

        public List<CartItem> GetAllCartItems() 
        {
            if (cartItems == null || cartItems.Count < 1)
            {
                return null;
            }
            
            return cartItems;
            
        }
        public CartItem? GetCartItemById(int id)
        {
            return cartItems.FirstOrDefault(item => item.Id == id);
        }

        public void UpdateCartItemAmount(int cartItemId, int amount) 
        {
             if(cartItemId == null || amount < 0)
                return;

            CartItem cartItemToBeUpdated = cartItems.FirstOrDefault(x => x.Id == cartItemId);
            if (cartItemToBeUpdated == null)
                return;
            
            cartItemToBeUpdated.Amount = amount;
        }

        public void DeleteCartItem(int id)
        {
            if (cartItems.Count < 1)
                return;

            CartItem cartItemToDelete = cartItems.FirstOrDefault(item => item.Id == id);
            if (cartItemToDelete == null)
                return;

            cartItems.Remove(cartItemToDelete);
        }
    }
}