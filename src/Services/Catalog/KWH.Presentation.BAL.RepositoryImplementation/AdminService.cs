using KWH.Common.Infrastrcture;
using KWH.Common.ViewModel;
using KWH.Common.ViewModel.Dtos;
using KWH.Presentation.BAL.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWH.Presentation.BAL.RepositoryImplementation
{
    public class AdminService : IAdminService
    {
        private readonly GenericHttpClient httpClient;
        private string baseURL = "http://localhost:5098/";
        public AdminService()
        {
            httpClient = new GenericHttpClient();
            httpClient.BaseURL = baseURL;
        }
        public async Task<object> GetAllRFData(string token)
        {
            httpClient.ApiUrl = "api/Admin/GetAllRFData";
            return await httpClient.GetWithTokenAsync(token);
        }
        public async Task<object> GetRFById(int Id, string token)
        {
            httpClient.ApiUrl = "api/Admin/GetRFById/" + Id;
            return await httpClient.GetWithTokenAsync(token);
        }
        public async Task<object> SubmitRFData(RequestViewModel<RFIdViewModel> model)
        {
            httpClient.ApiUrl = "api/Admin/SubmitRFData";
            return await httpClient.PostWithToken(model);
        }
        public async Task<object> UpdateRFData(int Id, RequestViewModel<RFIdViewModel> model)
        {
            httpClient.ApiUrl = "api/Admin/UpdateRFData";
            return await httpClient.PostWithTokenAsync(model);
        }

        public async Task<object> GetAllSectionData(string token)
        {
            httpClient.ApiUrl = "api/Admin/GetAllSectionData";
            return await httpClient.GetWithTokenAsync(token);
        }
        public async Task<object> GetSectionById(Guid Id, string token)
        {
            httpClient.ApiUrl = "api/Admin/GetSectionById/" + Id;
            return await httpClient.GetWithTokenAsync(token);
        }
        public async Task<object> SaveSectionData(RequestViewModel<SectionViewModel> model)
        {
            httpClient.ApiUrl = "api/Admin/SaveSectionData";
            return await httpClient.PostWithTokenAsync(model);
        }
        public async Task<object> UpdateSectionData(RequestViewModel<SectionViewModel> model)
        {
            httpClient.ApiUrl = "api/Admin/UpdateSectionData";
            return await httpClient.PostWithTokenAsync(model);
        }
        public async Task<object> DeleteSectionData(RequestViewModel<SectionViewModel> model)
        {
            httpClient.ApiUrl = "api/Admin/DeleteSectionData";
            return await httpClient.PostWithTokenAsync(model);
        }
        public async Task<object> GetAllClassMasterData(string token)
        {
            httpClient.ApiUrl = "api/Admin/GetAllClassMasterData";
            return await httpClient.GetWithTokenAsync(token);
        }
        public async Task<object> GetClassMasterById(Guid Id, string token)
        {
            httpClient.ApiUrl = "api/Admin/GetClassMasterById/" + Id;
            return await httpClient.GetWithTokenAsync(token);
        }
        public async Task<object> SaveClassData(RequestViewModel<ClassMasterDtos> model)
        {
            httpClient.ApiUrl = "api/Admin/SaveClassData";
            return await httpClient.PostWithTokenAsync(model);
        }
        public async Task<object> UpdateClassData(RequestViewModel<ClassMasterDtos> entity)
        {
            httpClient.ApiUrl = "api/Admin/UpdateClassData";
            return await httpClient.PostWithTokenAsync(entity);
        }
        public async Task<object> GetSectionDropdownData(string token)
        {
            httpClient.ApiUrl = "api/Admin/GetSectionDropdownData";
            return await httpClient.GetWithTokenAsync(token);
        }
        public async Task<object> DeleteClassData(RequestViewModel<ClassMasterDtos> entity)
        {
            httpClient.ApiUrl = "api/Admin/DeleteClassData";
            return await httpClient.PostWithTokenAsync(entity);
        }

        public async Task<object> GetAllCategoryData(string token)
        {
            httpClient.ApiUrl = "api/Admin/GetAllCategoryData";
            return await httpClient.GetWithTokenAsync(token);
        }

        public async Task<object> GetCategoryById(Guid Id, string token)
        {
            httpClient.ApiUrl = "api/Admin/GetCategoryById/" + Id;
            return await httpClient.GetWithTokenAsync(token);
        }
        public async Task<object> SubmitCategoryData(RequestViewModel<CategoryDtos> entity)
        {
            httpClient.ApiUrl = "api/Admin/SubmitCategoryData";
            return await httpClient.PostWithTokenAsync(entity);
        }
        public async Task<object> UpdateCategoryData(RequestViewModel<CategoryDtos> entity)
        {
            httpClient.ApiUrl = "api/Admin/UpdateCategoryData";
            return await httpClient.PostWithTokenAsync(entity);
        }
        public async Task<object> DeleteCategoryData(RequestViewModel<CategoryDtos> entity)
        {
            httpClient.ApiUrl = "api/Admin/DeleteCategoryData";
            return await httpClient.PostWithTokenAsync(entity);
        }

    }
}
