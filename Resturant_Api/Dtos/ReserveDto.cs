namespace Resturant_Api.Dtos
{
    public class ReserveDto
    {
        public int Id { get; set; }
        public string ReservationName { get; set; }
        public DateTime ReservationDate { get; set; }
        public DateTime ReservationEndTime { get; set; } = DateTime.Now.AddHours(23);
        public int TableId { get; set; }
    }
}
