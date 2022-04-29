using KWH.Common.Infrastrcture;
using KWH.Common.ViewModel;
using KWH.Presentation.BAL.IRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KWH.Presentation.Web.KWHWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService adminService;
        private string token = string.Empty;
        public AdminController(IAdminService _adminService)
        {
            adminService = _adminService;
            token = "GTJrAgayIOq8gkBpJwyrVFu0XjnwQHdrew3jG5JkazMlxTGn22t8XQEloQ0SSqHG2zeYEsonSG0eZ5eXfb8nZrZnAjss2u9TK-YggIDsreKdZspLu9kGdjudfdcqomHUgv1YrmybbLn_f_ffxH4YxqwnccF6vT_aFyT5byxkoqeT9XZhxjJ2qpqauHg_-0djj6UnxlaKrH89k_GoiKdgbyYvWjKHCnxr8fGZlAb8oF6FjSC-WExhavnrX9_uAORwVaZsPX4sRSAjUJYEhBNMOLUOMXbeug8MDSwGlYQDeEXaBgFc1M_iz6fFxxuvCRUKM4feOgZ4981-txA0xMwssNabgFKBNgC3aclkWTZYxysMkj-hNFCzeyyBB58QLCdSqtMBjqf3CGqxZOoMZQN92MpY-JdfaIPLjcp3IpS4dDG0yY-lAoNCQpN6q0xl4y1xlXQlxSOLZf5gFuLBlRNKHPXSTmYirDCMKcJ3A2ZEimonh5ZFzWwvjA7eq5dIm3tHMsqrn0f1MIn1IMoDcFo6yAOMJHkYCYz8lxLMM_lCYC09d2GGCgcDIgjsZ1c71LhrAlU6wiOVsU9ugO-f_HFu37MAmg9d2OkNHkEqbdeV6Xn_QImCiYdb0ZJV4_MmAjwM";
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> RFData()
        { 
            var data = await adminService.GetAllRFData(token);
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(Convert.ToString(data));
            var result = JsonConvert.DeserializeObject<IEnumerable<RFIdViewModel>>(apiResponse.Result.ToString());
            return View(result);

        }

        public ActionResult AddEditRF()
        {
            return View();
        }     

        public async Task<IActionResult> GetRFById(int id)
        {
            var data = await adminService.GetRFById(id, token);
            var list = JsonConvert.DeserializeObject<RFIdViewModel>(Convert.ToString(data));
            return View("AddEditRF", list);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateRFData(int Id, RFIdViewModel model)
        {
            var result = await adminService.UpdateRFData(Id, new RequestViewModel<RFIdViewModel> { Token = token, ModelObject = model });
            return Json(result);
        }

        [HttpPost]
        public async Task<JsonResult> SubmitRFData(RFIdViewModel model)
        {
            var result = await adminService.SubmitRFData(new RequestViewModel<RFIdViewModel> { Token = token, ModelObject = model });
            return Json(result);
        }

        public ActionResult AddEditSection()
        {
            return View();
        }


        [HttpGet]
        public async Task<ActionResult> GetAllSectionData()
        {
            var data = await adminService.GetAllSectionData(token); 
             
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(Convert.ToString(data));
            var result = JsonConvert.DeserializeObject<IEnumerable<SectionViewModel>>(apiResponse.Result.ToString());

            return View(result);
        }
        [HttpGet]
        public async Task<ActionResult> GetSectionById(Guid Id)
        {
            var data = await adminService.GetSectionById(Id, token);
            var list = JsonConvert.DeserializeObject<SectionViewModel>(Convert.ToString(data));
            return View("AddEditSection", list);

        }

        [HttpPost]
        public async Task<JsonResult> SaveSectionData(SectionViewModel model)
        { 
            var result = await adminService.SaveSectionData(new RequestViewModel<SectionViewModel> { Token = token, ModelObject = model });
            return Json(result);
        }
        [HttpPost]
        public async Task<JsonResult> UpdateSectionData(Guid Id, SectionViewModel model)
        {
            var result = await adminService.UpdateSectionData(Id, new RequestViewModel<SectionViewModel> { Token = token, ModelObject = model });
            return Json(result);
        }    
    }
}
