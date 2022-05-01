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
        Task<object> SubmitRFData(RequestViewModel<RFIdDtos> entity);
        Task<object> UpdateRFData(int Id, RequestViewModel<RFIdDtos> entity);

        /// <summary>
        /// Section Master Functionality
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>

        Task<object> GetAllSectionData(string token);
        Task<object> GetSectionById(int Id, string token);
        Task<object> SaveSectionData(RequestViewModel<SectionDtos> entity);
        Task<object> UpdateSectionData(RequestViewModel<SectionDtos> entity);
        Task<object> DeleteSectionData(RequestViewModel<SectionDtos> entity);


        /// <summary>
        /// Class Master Functionality
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>

        Task<object> GetAllClassMasterData(string token);
        Task<object> GetClassMasterById(int Id, string token);
        Task<object> SaveClassData(RequestViewModel<ClassMasterDtos> entity);
        Task<object> UpdateClassData(RequestViewModel<ClassMasterDtos> entity); 
        Task<object> GetSectionDropdownData(string token);
        Task<object> DeleteClassData(RequestViewModel<ClassMasterDtos> entity);

        /// <summary>
        /// Category Master Functionality
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>

        Task<object> GetAllCategoryData(string token);
        Task<object> GetCategoryById(int Id, string token);
        Task<object> SubmitCategoryData(RequestViewModel<CategoryDtos> entity);
        Task<object> UpdateCategoryData(RequestViewModel<CategoryDtos> entity);
        Task<object> DeleteCategoryData(RequestViewModel<CategoryDtos> entity);

    }
}
