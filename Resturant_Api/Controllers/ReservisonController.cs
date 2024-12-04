using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Resturant_Api.Dtos;
using Resturant_Api.HandleErrors;
using Resturant_Api.Helper;
using Resturant_Api_Core.Entites;
using Resturant_Api_Core.Services.EntitesServices;
using Resturant_Api_Core.Specification.ReservisonSpecification;
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

        [HttpPost("CreateReservation")]
        public async Task<IActionResult> CreateReservation([FromBody] RequestResrve requestResrve)
        {
            try
            {
                // Retrieve the table by ID using TableServices
                var table = await tableServices.GetTableById(requestResrve.Id);

                // Call the ReserveTable method to create the reservation
                var reservation = await tableServices.ReserveTable(table);

                return Ok(reservation); // Return the created reservation details
            }
            catch (Exception ex)
            {
                // Handle known exceptions and return appropriate responses
                if (ex.Message.Contains("No Table with this Capcity"))
                {
                    return NotFound(ex.Message);
                }
                if (ex.Message.Contains("The table is already reserved"))
                {
                    return BadRequest(ex.Message);
                }

                // Catch unexpected errors
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("CompleteReservation/{reservationId}")]
        public async Task<IActionResult> CompleteReservation(int reservationId)
        {
            try
            {
                // Call the CompleteReservationAsync function from TableServices to complete the reservation
                await tableServices.CompleteReservationAsync(reservationId);

                return Ok("Reservation completed successfully.");
            }
            catch (Exception ex)
            {
                // Handle exceptions (reservation not found, etc.)
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteReservison(int TableId , int ReservationId)
        {
              var isdeleted = tableServices.MakeTableAvalible(TableId, ReservationId);
              return Ok("Reservison is deleted");
            
        }
    }
}
