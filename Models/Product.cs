using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerceAPI.models
{
    public class Product
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public string Categoria { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public string Imagem { get; set; }
    }
}