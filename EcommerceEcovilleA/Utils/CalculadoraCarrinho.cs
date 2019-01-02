using EcommerceEcovilleA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceEcovilleA.Utils
{
    public class CalculadoraCarrinho
    {
        public static double CalcularTotal(List<ItemVenda> carrinho)
        {
            return carrinho.Sum(x => CalcularSubtotal(x));
        }

        public static double CalcularSubtotal(ItemVenda item)
        {
            return item.Produto.Preco * item.Quantidade;
        }
    }
}