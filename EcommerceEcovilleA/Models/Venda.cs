using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EcommerceEcovilleA.Models
{
    public class Venda
    {
        [Key]
        public int VendaId { get; set; }

        public Usuario Usuario { get; set; }

        public DateTime Data { get; set; }

        public List<ItemVenda> Items { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Uf { get; set; }
        public double Frete { get; set; }
    }
}