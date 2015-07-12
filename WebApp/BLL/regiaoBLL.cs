using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApp.DAO;

namespace WebApp.BLL
{
    public class regiaoBLL : IDisposable
    {
        public List<regiao> GetRegionsFromACity(int cityID)
        {
            using (cidadeBLL bll = new cidadeBLL())
            {
                return bll.GetRegionsFromACity(cityID);
            }
        }

        public regiao GetRegionById(int regionID) {
            using (DAOModelEntities ctx = new DAOModelEntities())
            {
                return ctx.regioes.FirstOrDefault(r => r.id_regiao.Equals(regionID));
            }
        }

        public void Dispose()
        {
        }
    }
}