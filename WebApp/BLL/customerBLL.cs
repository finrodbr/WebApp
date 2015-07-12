using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DAO;
using System.Text;

namespace WebApp.BLL
{
    public class customerBLL : IDisposable
    {
        public List<customer> GetCustomers() {
            using (DAOModelEntities ctx = new DAOModelEntities())
            {
                return ctx.customers.OrderBy(c => c.nome).ToList<customer>();
            }
        }

        public customer GetCustomerById(int customerID) {
            using (DAOModelEntities ctx = new DAOModelEntities())
            {
                return ctx.customers.FirstOrDefault(c=>c.id_customer.Equals(customerID));
            }
        }

        public List<customer> GetCustomersFromACity(int cityID) {
            using (cidadeBLL bll = new cidadeBLL())
            {
                return bll.GetCustomersFromACity(cityID);
            }
        }

        public List<customer> GetCustomersByCriteria(customer criteria)
        {
            List<customer> customers = GetCustomers();

            if (!string.IsNullOrEmpty(criteria.nome)) {
                customers = customers.Where(c => c.nome.ToUpper().Contains(criteria.nome.ToUpper())).ToList<customer>();
            }

            if (criteria.sexo > 0)
            {
                customers = customers.Where(c => c.sexo.Equals(criteria.sexo)).ToList<customer>();
            }

            if (criteria.id_cidade > 0) {
                customers = customers.Where(c => c.id_cidade.Equals(criteria.id_cidade)).ToList<customer>();
                if (criteria.id_regiao > 0) {
                    customers = customers.Where(c => c.id_regiao.Equals(criteria.id_regiao)).ToList<customer>();
                }
            }

            if (criteria.fromLastPurchase != DateTime.MinValue) {
                customers = customers.Where(c => c.ultima_compra >= criteria.fromLastPurchase).ToList<customer>();
            }

            if (criteria.toLastPurchase != DateTime.MinValue)
            {
                customers = customers.Where(c => c.ultima_compra <= criteria.toLastPurchase).ToList<customer>();
            }

            if (criteria.id_classificacao > 0) {
                customers = customers.Where(c=>c.id_classificacao.Equals(criteria.id_classificacao)).ToList<customer>();
            }

            if (criteria.id_usuario > 0) {
                customers = customers.Where(c => c.id_usuario.Equals(criteria.id_usuario)).ToList<customer>();
            }

            foreach (var item in customers)
            {
                item.classificacaoNome = new classificacaoBLL().GetClassificationById(item.id_classificacao).nome;
                item.usuarioNome = new usuarioBLL().GetUserById(item.id_usuario).nome;
                item.cidadeNome = new cidadeBLL().GetCityById(item.id_cidade).nome;
                item.regiaoNome = new regiaoBLL().GetRegionById(item.id_regiao).nome;
            }

            // Filter the list
            return customers;            
        }

        public void Dispose()
        {
        }
    }
}