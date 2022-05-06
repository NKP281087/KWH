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
        Task<Section> GetSectionById(int Id);
        Task<bool> SaveSectionData(Section entity);
        Task<bool> UpdateSectionData(Section entity);
        Task<bool> DeleteSectionData(int Id);

        Task<IEnumerable<ClassMasterViewModel>> GetAllClassMasterData();
        Task<ClassMaster> GetClassMasterById(int Id);
        Task<bool> SaveClassData(ClassMaster entity);
        Task<bool> UpdateClassData(ClassMaster entity);
        Task<bool> DeleteClassData(int Id);
        Task<IEnumerable<DropdownBindingViewModel>> GetSectionDropdownData();

        Task<IEnumerable<Category>> GetAllCategoryData();
        Task<Category> GetCategoryById(int Id);
        Task<bool> SubmitCategoryData(Category entity);
        Task<bool> UpdateCategoryData(Category entity);
        Task<bool> DeleteCategoryData(int Id);

        Task<IEnumerable<CandidateViewModel>> GetAllCandidateInfoData();
        Task<CandidateViewModel> GetCandidateById(int Id);
        Task<bool> SubmitCandidateData(CandidateInfo entity);
        //Task<bool> UpdateCandidateData(CandidateInfo entity);
        Task<bool> DeleteCandidateData(int Id);

        Task<IEnumerable<DropdownBindingViewModel>> GetClassDropdownData();
        Task<IEnumerable<DropdownBindingViewModel>> GetSectionDropdownDataByClassId(int Id);
        Task<IEnumerable<DropdownBindingViewModel>> GetCategoryDropdownData();


    }
}
