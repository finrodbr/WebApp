using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DAO;

namespace WebApp.BLL
{
    public class classificacaoBLL : IDisposable
    {
        public List<classificacao> GetClassifications()
        {
            using (DAOModelEntities ctx = new DAOModelEntities())
            {
                return ctx.classificacoes.OrderBy(c => c.nome).ToList<classificacao>();
            }
        }

        public classificacao GetClassificationById(int classificationID)
        {
            using (DAOModelEntities ctx = new DAOModelEntities())
            {
                return ctx.classificacoes.FirstOrDefault(c => c.id_classificacao.Equals(classificationID));
            }
        }

        public void Dispose()
        {
        }
    }
}