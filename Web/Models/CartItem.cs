using System.Xml.Linq;
using Web.Models;
//using EntityFramework.Models;

namespace Web.Models
{
    public class CartItem
    {
        public int Id {get; set;}
        public Board Board {get; set;}
        public int Amount {get; set;}

        public override string ToString()
        {
            return $"Id: {Id}, Product: {Board}, Amount: {Amount}";
        }
    }
}