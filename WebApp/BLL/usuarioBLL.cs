using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Web;
using WebApp.DAO;

namespace WebApp.BLL
{
    public class usuarioBLL : IDisposable
    {
        public List<usuario> GetUsers()
        {
            using (DAOModelEntities ctx = new DAOModelEntities())
            {
                return ctx.usuarios.OrderBy(u => u.nome).ToList<usuario>();
            }
        }

        public usuario GetUserById(int userID) {
            using (DAOModelEntities ctx = new DAOModelEntities())
            {
                return ctx.usuarios.FirstOrDefault(u=>u.id_usuario.Equals(userID)) ;
            }
        }

        public usuario GetUserByLoginAndPassword(string login, string password) {
            // Create a MD5 hash of the password for comparison
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(password));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString());
            }
            string hashedPassword = sb.ToString();

            using (DAOModelEntities ctx = new DAOModelEntities())
            {
                return ctx.usuarios.FirstOrDefault(u => u.email.Equals(login) && u.senha.Equals(hashedPassword));
            }
        }

        public List<usuario> GetUsersFromACity(int cityID) {
            using (cidadeBLL bll = new cidadeBLL())
            {
                return bll.GetUsersFromACity(cityID);
            }
        }

        public List<customer> GetCustomersFromAUser(int userID) {
            using (DAOModelEntities ctx = new DAOModelEntities())
            {
                return ctx.customers.Where(c=>c.id_usuario.Equals(userID)).ToList<customer>();
            }
        }

        public List<usuario> GetUsersUpToAnAccessLevel(int accessLevel)
        {
            using (nivel_acessoBLL bll = new nivel_acessoBLL())
            {
                return bll.GetUsersUpToAnAccessLevel(accessLevel);
            }
        }

        public void Dispose()
        {
        }
    }
}