using EcommerceEcovilleA.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EcommerceEcovilleA.DAL
{
    public class FreteDAO
    {
        public static List<Frete> ListaFretes()
        {
            using (var ctx = new Context())
            {
                UF estado = new Models.UF();
                var ufs = ctx.UFs.AsNoTracking().ToList();
                return ctx.Fretes.AsNoTracking().ToList();
            }
        }
        public static bool CadastrarFrete(Frete frete)
        {
            using (var ctx = new Context())
            {
                try
                {
                    UF estado = new Models.UF();
                    estado.Descricao = frete.UnidadeFederativa.Descricao;
                    frete.UnidadeFederativa = UFDAO.BuscarEstado(estado);
                    ctx.UFs.Attach(frete.UnidadeFederativa);
                    ctx.Fretes.Add(frete);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public static Frete BuscarFrete(Frete frete)
        {
            using (var ctx = new Context())
            {
                return ctx.Fretes.AsNoTracking().FirstOrDefault
                (x => x.FreteId.Equals(frete.FreteId));
            }
        }
        public static Frete BuscarFretePorUF(UF uf)
        {
            using (var ctx = new Context())
            {
                try
                {
                    return ctx.Fretes.AsNoTracking().FirstOrDefault(x => x.UnidadeFederativa.UfId == uf.UfId);
                }
                catch (Exception)
                {
                    return null;
                    throw;
                }
            }
        }

        public static Frete BuscarFretePorUF(string uf)
        {
            using (var ctx = new Context())
            {
                try
                {
                    return ctx.Fretes.AsNoTracking().FirstOrDefault(x => x.UnidadeFederativa.Descricao == uf);
                }
                catch (Exception)
                {
                    return null;
                    throw;
                }
            }
        }
        public static bool AtualizarFrete(Frete frete)
        {
            using (var ctx = new Context())
            {
                try
                {
                    ctx.Entry(frete).State = EntityState.Modified;
                    ctx.Fretes.Attach(frete);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public static bool Remover(Frete frete)
        {
            using (var ctx = new Context())
            {
                try
                {
                    ctx.Fretes.Remove(frete);
                    ctx.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}