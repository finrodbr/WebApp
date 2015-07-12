using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DAO;

namespace WebApp.BLL
{
    public class cidadeBLL : IDisposable
    {
        public List<cidade> GetCities()
        {
            using (DAOModelEntities ctx = new DAOModelEntities())
            {
                return ctx.cidades.OrderBy(c => c.nome).ToList<cidade>();
            }
        }

        public cidade GetCityById(int cityID) {
            using (DAOModelEntities ctx = new DAOModelEntities())
            {
                return ctx.cidades.FirstOrDefault(c => c.id_cidade.Equals(cityID));
            }
        }

        public List<regiao> GetRegionsFromACity(int cityID)
        {
            using (DAOModelEntities ctx = new DAOModelEntities())
            {
                return ctx.regioes.Where(r => r.id_cidade.Equals(cityID)).ToList<regiao>();
            }
        }

        public List<usuario> GetUsersFromACity(int cityID) {
            using (DAOModelEntities ctx = new DAOModelEntities())
            {
                return ctx.usuarios.Where(u => u.cidade.Equals(cityID)).OrderBy(u => u.nome).ToList<usuario>();
            }
        }

        public List<customer> GetCustomersFromACity(int cityID) {
            using (DAOModelEntities ctx = new DAOModelEntities())
            {
                return ctx.customers.Where(c => c.id_cidade.Equals(cityID)).OrderBy(c => c.nome).ToList<customer>();
            }
        }

        public void Dispose()
        {
        }
    }
}