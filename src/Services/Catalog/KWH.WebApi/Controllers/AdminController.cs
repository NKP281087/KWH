using AutoMapper;
using KWH.BAL.IRepository;
using KWH.Common.Infrastrcture;
using KWH.Common.ViewModel;
using KWH.Common.ViewModel.Dtos;
using KWH.DAL.Entities;
using KWH.WebApi.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace KWH.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminBALService _adminService;
        private readonly IMapper _mapper;

        public AdminController(IAdminBALService adminService, IMapper mapper)
        {
            _adminService = adminService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllRFData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllRFData()
        {
            var data = await _adminService.GetAllRFData();
            if (data == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<IEnumerable<RFIdDtos>>(data);
            return Ok(new { StatusCode = HttpStatusCode.OK, result, Message = "Success" });
        }


        [HttpGet]
        [Route("GetRFById/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRFById(int id)
        {

            var data = await _adminService.GetRFById(id);
            //return data != null ? Ok(data) : NotFound();
            if (data != null)
            {
                return Ok(_mapper.Map<RFIdDtos>(data));
            }
            return NotFound();

        }

        [HttpPost]
        [Route("SubmitRFData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SubmitRFData(RFIdViewModel model)
        {
            RFIdDtos dtos = new RFIdDtos();

            if (!ModelState.IsValid)
            {
                return BadRequest(new { StatusCode = StatusCodes.Status400BadRequest });
            }

            dtos.TimeIn = model.TimeIn;
            dtos.TimeOut = model.TimeOut;

            var RFFormModel = _mapper.Map<RFId>(dtos);
            var response = await _adminService.SubmitRFData(RFFormModel);
            if (response == null)
            {
                return BadRequest(new { StatusCode = StatusCodes.Status400BadRequest });
            }
            return Ok(new { StatusCode = StatusCodes.Status200OK, Message = "Success" });
        }

        //public async Task<HttpResponseMessage> SubmitRFData(RFIdViewModel model)
        //{
        //    RFIdDtos dtos = new RFIdDtos();

        //    if (!ModelState.IsValid)
        //    {
        //        return new HttpResponseMessage(HttpStatusCode.BadRequest);
        //    }

        //    dtos.TimeIn = model.TimeIn;
        //    dtos.TimeOut = model.TimeOut;

        //    var RFFormModel = _mapper.Map<RFId>(dtos);
        //    var response = await _adminService.SubmitRFData(RFFormModel);
        //    if (response == null)
        //    {
        //        return new HttpResponseMessage(HttpStatusCode.BadRequest);
        //    } 
        //    return new HttpResponseMessage(HttpStatusCode.OK); 
        //}

        [HttpPost]
        [Route("UpdateRFData")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> UpdateRFData(int Id, RFId model)
        {
            if (ModelState.IsValid)
            {
                return BadRequest();
            }
            return await _adminService.UpdateRFData(Id, model);
        }

        [HttpGet]
        [Route("GetAllSectionData")]
        public async Task<IActionResult> GetAllSectionData()
        {
            var data = await _adminService.GetAllSectionData();

            if (data == null && data.Count() == 0)
            {
                return NotFound(new { StatusCode = StatusCodes.Status404NotFound, Message = "No Data Found" });
            }
            var result = _mapper.Map<IEnumerable<Section>>(data);
            return Ok(new { StatusCode = StatusCodes.Status200OK, result });
        }

        [HttpGet]
        [Route("GetSectionById/{Id}")]
        public async Task<IActionResult> GetSectionById(Guid Id)
        {
            var data = await _adminService.GetSectionById(Id);
            return Ok(new { StatusCode = StatusCodes.Status200OK, result = data });
        }

        [HttpPost]
        [Route("SaveSectionData")]
        public async Task<IActionResult> SaveSectionData(SectionViewModel model)
        {
            SectionDtos Dtos = new SectionDtos();
            if (!ModelState.IsValid)
            {
                return BadRequest(new { StatusCode = StatusCodes.Status400BadRequest, Message = "Validation Failed" });
            }

            Dtos.SectionId = Guid.NewGuid();
            Dtos.SectionName = model.SectionName;
            Dtos.IsActive = true;

            var sectionDtos = _mapper.Map<Section>(Dtos);
            var response = await _adminService.SaveSectionData(sectionDtos);

            if (!response)
            {
                return Ok(new { StatusCode = StatusCodes.Status200OK, Message = "Section Name Already Exists Or Data Not Found!" });
            }
            else
            {
                return Ok(new { StatusCode = StatusCodes.Status200OK, Message = "Success" });
            }

        }
        [HttpPost]
        [Route("UpdateSectionData")]
        public async Task<IActionResult> UpdateSectionData(SectionViewModel model)
        {
            SectionDtos Dtos = null;
            if (!ModelState.IsValid)
            {
                return BadRequest(new { StatusCode = StatusCodes.Status400BadRequest, Message = "Validation Failed" });
            }
            Dtos = new SectionDtos();
            Dtos.SectionName = model.SectionName;
            Dtos.IsActive = model.IsActive;
            Dtos.SectionId = model.SectionId;

            var sectionDtos = _mapper.Map<Section>(Dtos);
            var response = await _adminService.UpdateSectionData(sectionDtos);
            if (!response)
            {
                return Ok(new { StatusCode = StatusCodes.Status200OK, Message = "Section Name Already Exists Or Data Not Found!" });
            }
            else
            {
                return Ok(new { StatusCode = StatusCodes.Status200OK, Message = "Success" });
            }
        }
        [HttpPost]
        [Route("DeleteSectionData")]
        public async Task<IActionResult> DeleteSectionData(SectionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { StatusCode = HttpStatusCode.BadRequest, Message = "Validation Failed" });
            }
            var response = await _adminService.DeleteSectionData(model.SectionId);
            return Ok(new { StatusCode = StatusCodes.Status200OK, response, Message = "Success" });
        }
        [HttpPost]
        [Route("SaveClassData")]
        public async Task<IActionResult> SaveClassData(ClassMasterDtos model)
        {            
            if (!ModelState.IsValid)
            {
                return BadRequest(new { StatusCode = HttpStatusCode.BadRequest, Message = "Validation Failed" });
            }             
            var classDtos = _mapper.Map<ClassMaster>(model);
            var response = await _adminService.SaveClassData(classDtos);
            if (!response)
            {
                return Ok(new { StatusCode = StatusCodes.Status200OK, Message = "Data Already Exists Or Data Not Found!" });
            }
            return Ok(new { StatusCode = StatusCodes.Status200OK, response, Message = "Success" });
        }

        [HttpGet]
        [Route("GetAllClassMasterData")]
        public async Task<IActionResult> GetAllClassMasterData()
        {
            var response = await _adminService.GetAllClassMasterData();
            if (response == null && response.Count() > 0)
            {
                return NotFound(new { StatusCode = StatusCodes.Status404NotFound, Message = "No Data Found" });
            }
            return Ok(new { StatusCode = StatusCodes.Status200OK, result = response, Message = "Success" });
        }

        [HttpGet]
        [Route("GetClassMasterById/{Id}")]
        public async Task<IActionResult> GetClassMasterById(Guid Id)
        {
            var response = await _adminService.GetClassMasterById(Id);
            return Ok(new { StatusCode = StatusCodes.Status200OK, result = response, Message = "No Data Found" });
        }

        [HttpPost]
        [Route("UpdateClassData")]
        public async Task<IActionResult> UpdateClassData(ClassMasterDtos model)
        {           
            if (!ModelState.IsValid)
            {
                return BadRequest(new { StatusCode = StatusCodes.Status400BadRequest, Message = "Validation Failed" });
            }            
            var classDtos = _mapper.Map<ClassMaster>(model);
            var response = await _adminService.UpdateClassData(classDtos);
            return Ok(new { StatusCode = StatusCodes.Status200OK, result = response, Message = "Success" });

        }
        [HttpGet]
        [Route("GetSectionDropdownData")]
        public async Task<IActionResult> GetSectionDropdownData()
        {
            var response = await _adminService.GetSectionDropdownData();
            return Ok(new { StatusCode = StatusCodes.Status200OK, result = response, Message = "Success" });
        }

        [HttpPost]
        [Route("DeleteClassData")]
        public async Task<IActionResult> DeleteClassData(ClassMasterDtos model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { StatusCode = HttpStatusCode.BadRequest, Message = "Validation Failed" });
            }
            var response = await _adminService.DeleteClassData(model.ClassId);
            return Ok(new { StatusCode = StatusCodes.Status200OK, result = response, Message = "Success" });
        }

        [HttpGet]
        [Route("GetAllCategoryData")]
        public async Task<IActionResult> GetAllCategoryData()
        {
            var response = await _adminService.GetAllCategoryData();
            if (response == null && response.Count() > 0)
            {
                return NotFound(new { StatusCode = StatusCodes.Status404NotFound, Message = "No Data Found" });
            }
            return Ok(new { StatusCode = StatusCodes.Status200OK, result = response, Message = "Success" });
        }

        [HttpGet]
        [Route("GetCategoryById/{Id}")]
        public async Task<IActionResult> GetCategoryById(Guid Id)
        {
            var response = await _adminService.GetCategoryById(Id);
            return Ok(new { StatusCode = StatusCodes.Status200OK, result = response, Message = "Success" });
        }

        [HttpPost]
        [Route("SubmitCategoryData")]
        public async Task<IActionResult> SubmitCategoryData(Category model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { StatusCode = HttpStatusCode.BadRequest, Message = "Validation Failed" });
            }
            var categoryDtos = _mapper.Map<Category>(model);
            var response = await _adminService.SubmitCategoryData(categoryDtos);
            if (!response)
            {
                return Ok(new { StatusCode = StatusCodes.Status200OK, Message = "Data Already Exists Or Data Not Found!" });
            }
            return Ok(new { StatusCode = StatusCodes.Status200OK, result = response, Message = "Success" });
        }

        [HttpPost]
        [Route("UpdateCategoryData")]
        public async Task<IActionResult> UpdateCategoryData(Category model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { StatusCode = HttpStatusCode.BadRequest, Message = "Validation Failed" });
            }
            var categoryDtos = _mapper.Map<Category>(model);
            var response = await _adminService.UpdateCategoryData(categoryDtos);
            if (!response)
            {
                return Ok(new { StatusCode = StatusCodes.Status200OK, Message = "Data Already Exists Or Data Not Found!" });
            }
            return Ok(new { StatusCode = StatusCodes.Status200OK, result = response, Message = "Success" });
        }

        [HttpPost]
        [Route("DeleteCategoryData")]
        public async Task<IActionResult> DeleteCategoryData(Category model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { StatusCode = HttpStatusCode.BadRequest, Message = "Validation Failed" });
            }
            var response = await _adminService.DeleteCategoryData(model.CategoryId);
            return Ok(new { StatusCode = StatusCodes.Status200OK, result = response, Message = "Success" });
        } 
    }
}
