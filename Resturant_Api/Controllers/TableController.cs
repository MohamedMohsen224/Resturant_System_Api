using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resturant_Api.Dtos;
using Resturant_Api.HandleErrors;
using Resturant_Api.Helper;
using Resturant_Api_Core.Entites;
using Resturant_Api_Core.Entites.Enum;
using Resturant_Api_Core.Services.EntitesServices;
using Resturant_Api_Core.Specification.TableSpecification;

namespace Resturant_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableServices tableServices;
        private readonly IMapper mapper;

        public TableController(ITableServices tableServices , IMapper mapper)
        {
            this.tableServices = tableServices;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<TableDto>>> GetAllTables([FromQuery]TableParms parms)
        {
            var tables = await tableServices.GetAllTables(parms);
            var mappTb = mapper.Map<IReadOnlyList<Table>, IReadOnlyList<TableDto>>(tables);
            return Ok(mappTb);
               
        }

        [HttpGet("GETBYID")]
        public async Task<ActionResult<TableDto>> GetTableById(int Id)
        {
            var Table = await tableServices.GetTableById(Id);
            var tb = mapper.Map<Table, TableDto>(Table);
            if (Table == null)
            {
                new ApiErrorResponse(404, "No Table With this id");
            }
            return Ok(tb);
        }
        [HttpGet("GetAvalibleTables")]
        public async Task<ActionResult<Pagination<TableDto>>> GetAvalibleTables([FromQuery] TableParms parms)
        {
            var tables = await tableServices.GetAvalibleTables(parms);
            var mappTb = mapper.Map<IReadOnlyList<Table>, IReadOnlyList<TableDto>>(tables);
            if(parms.IsAvalible == TableStatus.Avalible)
            {
                new ApiErrorResponse(404, "No Avalible Tables U Can Check In Another Time");
            }
            return Ok(mappTb);

        }



    }
}
