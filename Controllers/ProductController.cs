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
            var products = _context.Products.Find(Id);

            if(products == null)
                return NotFound();
            
            return Ok(products);
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

        
    }
}