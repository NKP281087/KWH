using AutoMapper;
using KWH.BAL.IRepository;
using KWH.Common.Infrastrcture;
using KWH.Common.ViewModel;
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
            return Ok(_mapper.Map<IEnumerable<RFIdDtos>>(data));
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
        public async Task<HttpResponseMessage> SubmitRFData(RFIdViewModel model)
        {
            RFIdDtos dtos = new RFIdDtos();

            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            dtos.TimeIn = model.TimeIn;
            dtos.TimeOut = model.TimeOut;

            var RFFormModel = _mapper.Map<RFId>(dtos);
            var response = await _adminService.SubmitRFData(RFFormModel);
            if (response == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            } 
            return new HttpResponseMessage(HttpStatusCode.OK); 
        }

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
    }
}
