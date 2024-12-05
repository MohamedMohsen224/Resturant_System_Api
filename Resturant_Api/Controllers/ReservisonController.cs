using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Resturant_Api.Dtos;
using Resturant_Api.HandleErrors;
using Resturant_Api.Helper;
using Resturant_Api_Core.Entites;
using Resturant_Api_Core.IUnitOfWork;
using Resturant_Api_Core.Services.EntitesServices;
using Resturant_Api_Core.Specification.ReservisonSpecification;
using Resturant_Api_Core.Specification.TableSpecification;
using Resturant_Api_Reposatry.UnitOfWork;
using Resturant_Api_Services.EntitesSERVICES;

namespace Resturant_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservisonController : ControllerBase
    {
        private readonly IReservisonServices reservisonServices;
        private readonly IMapper mapper;
        private readonly ITableServices tableServices;

        public ReservisonController(IReservisonServices reservisonServices,IMapper mapper,ITableServices tableServices)
        {
            this.reservisonServices = reservisonServices;
            this.mapper = mapper;
            this.tableServices = tableServices;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<ReserveDto>>> GetAllReservison([FromQuery]ResrveParms parms)
        {
            var Resrvison = await reservisonServices.GetAllReservisons(parms);
            var Data = mapper.Map<IReadOnlyList<Reservison>, IReadOnlyList<ReserveDto>>(Resrvison);
            if(Resrvison == null)
            {
                return NotFound(new ApiErrorResponse(404, "No Reservison For today"));
            }
            return Ok(Data);

        }
       
        [HttpPost("CreateReservation")]
        public async Task<IActionResult> CreateReservation([FromBody]ReserveDto requestResrve)
        {

            if (requestResrve?.TableId == null)
            {
                return BadRequest("Invalid TableId.");
            }

            // Fetch the table from the database using the provided TableId
            var table = await tableServices.GetTableById(requestResrve.TableId);

            // Check if the table exists
            if (table == null)
            {
                return NotFound("Table not found.");
            }

            try
            {
                // Proceed to reserve the table
                var reservation = await tableServices.ReserveTable(table ,requestResrve.ReservationName);
                return Ok(reservation);
            }
            catch (InvalidOperationException ex)
            {
                // Handle specific exceptions
                return BadRequest(ex.Message);
            }


        }

        [HttpPost("CompleteReservation/{reservationId}")]
        public async Task<IActionResult> CompleteReservation(int reservationId)
        {
            try
            {
                // Call the CompleteReservationAsync function from TableServices to complete the reservation
                await reservisonServices.CompleteReservationAsync(reservationId);

                return Ok("Reservation completed successfully.");
            }
            catch (Exception ex)
            {
                // Handle exceptions (reservation not found, etc.)
                return StatusCode(500, ex.Message);
            }
        }

    }
}
