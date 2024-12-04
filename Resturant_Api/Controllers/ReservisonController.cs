using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resturant_Api.Dtos;
using Resturant_Api.HandleErrors;
using Resturant_Api.Helper;
using Resturant_Api_Core.Entites;
using Resturant_Api_Core.Services.EntitesServices;
using Resturant_Api_Core.Specification.ReservisonSpecification;

namespace Resturant_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservisonController : ControllerBase
    {
        private readonly IReservisonServices reservisonServices;
        private readonly IMapper mapper;

        public ReservisonController(IReservisonServices reservisonServices,IMapper mapper)
        {
            this.reservisonServices = reservisonServices;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<ReserveDto>>> GetAllReservison(ResrveParms parms)
        {
            var Resrvison = await reservisonServices.GetAllReservisons(parms);
            var Data = mapper.Map<IReadOnlyList<Reservison>, IReadOnlyList<ReserveDto>>(Resrvison);
            if(Resrvison == null)
            {
                return NotFound(new ApiErrorResponse(404, "No Reservison For today"));
            }
            return Ok(Data);

        }

    }
}
