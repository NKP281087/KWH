using KWH.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWH.BAL.IRepository
{
    public interface IAdminBALService
    {
        Task<IEnumerable<RFId>> GetAllRFData();  
        Task<RFId> GetRFById(int id);
        Task<RFId> SubmitRFData(RFId entity);
        Task<bool> UpdateRFData(int Id, RFId entity);

    }
}
