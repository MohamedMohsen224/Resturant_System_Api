namespace Resturant_Api.Dtos
{
    public class ReserveDto
    {
        public string ReservationName { get; set; }
        public DateTime ReservationDate { get; set; }
        public TimeOnly ReservationEndTime { get; set; }
        public int TableId { get; set; }
    }
}
