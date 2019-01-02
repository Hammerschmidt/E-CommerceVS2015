using EcommerceEcovilleA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EcommerceEcovilleA.DAL
{
    public class CategoriaDAO
    {
        public static List<Categoria> RetornarCategorias()
        {
            using (var ctx = new Context())
            {
                return ctx.Categorias.AsNoTracking().ToList();
            }
        }

        public static bool CadastrarCategoria(Categoria categoria)
        {
            using (var ctx = new Context())
            {
                if (BuscarCategoriaPorNome(categoria) == null)
                {
                    ctx.Categorias.Add(categoria);
                    ctx.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public static Categoria BuscarCategoriaPorNome(Categoria categoria)
        {
            using (var ctx = new Context())
            {
                return ctx.Categorias.AsNoTracking().FirstOrDefault(x => x.Nome.Equals(categoria.Nome));
            }
        }

        public static void RemoverCategoria(int id)
        {
            using (var ctx = new Context())
            {
                ctx.Categorias.Remove(BuscarCategoriaPorId(id));
                ctx.SaveChanges();
            }
        }

        public static Categoria BuscarCategoriaPorId(int? id)
        {
            using (var ctx = new Context())
            {
                return ctx.Categorias.AsNoTracking().FirstOrDefault(a => a.CategoriaId == id);
            }
        }

        public static bool AlterarCategoria(Categoria categoria)
        {
            using (var ctx = new Context())
            {
                if (ctx.Categorias.FirstOrDefault
                (x => x.Nome.Equals(categoria.Nome) &&
                x.CategoriaId != categoria.CategoriaId) == null)
                {
                    ctx.Entry(categoria).State = EntityState.Modified;
                    ctx.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}