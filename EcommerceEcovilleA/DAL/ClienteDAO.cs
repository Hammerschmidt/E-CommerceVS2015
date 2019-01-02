using EcommerceEcovilleA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceEcovilleA.DAL
{
    public class ClienteDAO
    {

        public static void CadastrarCliente(Cliente cliente)
        {
            using (var ctx = new Context())
            {
                ctx.Clientes.Add(cliente);
                ctx.SaveChanges();
            }
        }
    }
}