using EcommerceEcovilleA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceEcovilleA.DAL
{
    public class UFDAO
    {
        public static List<UF> ListaEstados()
        {
            using (var ctx = new Context())
            {
                return ctx.UFs.AsNoTracking().ToList();
            }
        }

        public static bool CadastrarUF(UF uf)
        {
            using (var ctx = new Context())
            {
                try
                {
                    if (BuscarEstado(uf) == null)
                    {
                        ctx.UFs.Add(uf);
                        ctx.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static UF BuscarEstado(UF uf)
        {
            using (var ctx = new Context())
            {
                return ctx.UFs.AsNoTracking().FirstOrDefault
                (x => x.Descricao.Equals(uf.Descricao));
            }
        }
        public static UF BuscarEstadoId(int id)
        {
            using (var ctx = new Context())
            {
                return ctx.UFs.AsNoTracking().FirstOrDefault
                (x => x.UfId.Equals(id));
            }
        }
        public string BuscarEstadoS(int id)
        {
            using (var ctx = new Context())
            {
                UF estado = ctx.UFs.AsNoTracking().FirstOrDefault(x => x.UfId.Equals(id));
                return estado.Descricao;
            }
        }
    }
}