using KWH.Common.Infrastrcture;
using KWH.Common.ViewModel;
using KWH.Common.ViewModel.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWH.Presentation.BAL.IRepository
{
    public interface IAdminService
    {
        Task<object> GetAllRFData(string token);
        Task<object> GetRFById(int id, string token);
        Task<object> SubmitRFData(RequestViewModel<RFIdViewModel> entity);
        Task<object> UpdateRFData(int Id, RequestViewModel<RFIdViewModel> entity);

        Task<object> GetAllSectionData(string token);
        Task<object> GetSectionById(Guid Id, string token);
        Task<object> SaveSectionData(RequestViewModel<SectionViewModel> entity);
        Task<object> UpdateSectionData(RequestViewModel<SectionViewModel> entity);
        Task<object> DeleteSectionData(RequestViewModel<SectionViewModel> entity);
        Task<object> GetAllClassMasterData(string token);
        Task<object> GetClassMasterById(Guid Id, string token);
        Task<object> SaveClassData(RequestViewModel<ClassMasterDtos> entity);
        Task<object> UpdateClassData(RequestViewModel<ClassMasterDtos> entity); 
        Task<object> GetSectionDropdownData(string token);
        Task<object> DeleteClassData(RequestViewModel<ClassMasterDtos> entity);
    }
}
