using KWH.Common.Infrastrcture;
using KWH.Common.ViewModel;
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
        public async Task<object> UpdateSectionData(Guid Id, RequestViewModel<SectionViewModel> model)
        {
            httpClient.ApiUrl = "api/Admin/UpdateSectionData/Id" + Id;
            return await httpClient.PostWithTokenAsync(model);
        }
    }
}
