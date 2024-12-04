using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerceAPI.Context;
using Microsoft.AspNetCore.Mvc;
using ecommerceAPI.Models;

namespace ecommerceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _context;

        public ProductController(ProductContext context)
        {
            _context = context;
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }

        [HttpGet("{Id}")]
        public IActionResult ObterPorId(int Id)
        {
            var product = _context.Products.Find(Id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpGet("Categoria")]
        public IActionResult ObterPorCategoria(string categoria)
        {
            var products = _context.Products
                .Where(p => p.Categoria.ToLower() == categoria.ToLower())
                .ToList();

            if (products == null || !products.Any())
                return NotFound($"Nenhum produto encontrado na categoria '{categoria}'.");

            return Ok(products);
        }

        [HttpPost("AdicionarAoCarrinho")]
        public IActionResult AdicionarAoCarrinho(int productId, int quantidade)
        {
            var product = _context.Products.Find(productId);

            if (product == null)
                return NotFound("Produto não encontrado.");

            if (product.Quantidade < quantidade)
                return BadRequest("Quantidade solicitada excede o estoque disponível.");

            product.Quantidade -= quantidade;
            var carrinho = _context.CartItems.FirstOrDefault(c => c.ProductId == productId);

            if (carrinho == null)
            {
                _context.CartItems.Add(new CartItem
                {
                    ProductId = productId,
                    Quantidade = quantidade
                });
            }
            else
            {
                carrinho.Quantidade += quantidade;
            }

            _context.SaveChanges();
            return Ok("Produto adicionado ao carrinho.");
        }

        [HttpDelete("RemoverDoCarrinho")]
        public IActionResult RemoverDoCarrinho(int productId, int quantidade)
        {
            var carrinho = _context.CartItems.FirstOrDefault(c => c.ProductId == productId);

            if (carrinho == null)
                return NotFound("Produto não encontrado no carrinho.");

            if (carrinho.Quantidade < quantidade)
                return BadRequest("Quantidade para remoção excede a quantidade no carrinho.");

            var product = _context.Products.Find(productId);

            if (product != null)
            {
                product.Quantidade += quantidade;
            }

            carrinho.Quantidade -= quantidade;

            if (carrinho.Quantidade == 0)
            {
                _context.CartItems.Remove(carrinho);
            }

            _context.SaveChanges();
            return Ok("Produto removido do carrinho.");
        }
    }
}
