using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceEcovilleA.Models
{
    public class Frete
    {
        public int FreteId { get; set; }
        public double Valor { get; set; }
        public UF UnidadeFederativa { get; set; }
    }
}