using System.Collections.Generic;

namespace Goggles.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<Models.Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}