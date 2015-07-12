using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DAO;

namespace WebApp.BLL
{
    public class nivel_acessoBLL : IDisposable
    {
        public List<nivel_acesso> GetAccessLevels() {
            using (DAOModelEntities ctx = new DAOModelEntities())
            {
                return ctx.niveis_acesso.OrderBy(na => na.nome).ToList<nivel_acesso>();
            }
        }

        public nivel_acesso GetAccessLevelById(int accessLevelID) { 
            using (DAOModelEntities ctx = new DAOModelEntities())
            {
                return ctx.niveis_acesso.FirstOrDefault(na => na.id_nivel_acesso.Equals(accessLevelID));
            }
        }

        public List<usuario> GetUsersWithAnAccessLevel(int accessLevel) { 
            using (DAOModelEntities ctx = new DAOModelEntities())
            {
                return ctx.usuarios.Where(u=>u.valor_nivel_acesso.Equals(accessLevel)).ToList<usuario>();
            }
        }

        public List<usuario> GetUsersUpToAnAccessLevel(int accessLevel)
        {
            using (DAOModelEntities ctx = new DAOModelEntities())
            {
                return ctx.usuarios.Where(u => u.valor_nivel_acesso < accessLevel).ToList<usuario>();
            }
        }

        public void Dispose()
        {
        }
    }
}