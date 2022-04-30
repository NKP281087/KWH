using KWH.Common.ViewModel;
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

        Task<IEnumerable<Section>> GetAllSectionData();
        Task<Section> GetSectionById(Guid Id);
        Task<bool> SaveSectionData(Section entity);
        Task<bool> UpdateSectionData(Section entity);
        Task<bool> DeleteSectionData(Guid Id);

        Task<IEnumerable<ClassMasterViewModel>> GetAllClassMasterData();
        Task<ClassMaster> GetClassMasterById(Guid Id);
        Task<bool> SaveClassData(ClassMaster entity);
        Task<bool> UpdateClassData(ClassMaster entity);
        Task<bool> DeleteClassData(Guid Id);
    }
}
