namespace MovieBooking.Models.Models
{
	public class Booking
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public int TotalSeats { get; set; }
        public int Seats { get; set; }
        public DateTime DateAndTime { get; set; }
        public string Showtime { get; set; }
        public decimal Price { get; set; }
    }
}
