using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Woodbrass.Data;
using Woodbrass.Models;

namespace Woodbrass.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        private readonly WoodBrassDbContext _context;

        public CartController(WoodBrassDbContext context)
        {
            _context = context;
        }

        // PUT: api/cart/update
        [HttpPut("update")]
        public async Task<IActionResult> UpdateCart([FromBody] UpdateCartModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Récupérer l'utilisateur connecté
            var userId = int.Parse(User.FindFirst("Id")?.Value);

            // Récupérer le panier de l'utilisateur
            var cart = await _context.Carts.Include(c => c.ProductCarts)
                                            .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                return NotFound("Cart not found.");
            }

            // Mettre à jour le panier
            foreach (var item in model.ProductCarts)
            {
                var productCart = cart.ProductCarts.FirstOrDefault(pc => pc.ProductId == item.ProductId);
                if (productCart != null)
                {
                    // Mettre à jour la quantité
                    productCart.Quantity = item.Quantity;
                    productCart.UnitPrice = item.UnitPrice;
                }
                else
                {
                    // Ajouter un nouveau produit au panier
                    cart.ProductCarts.Add(new ProductCart
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice
                    });
                }
            }

            cart.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(cart);
        }
    }

    public class UpdateCartModel
    {
        public ICollection<ProductCart> ProductCarts { get; set; }
    }
}