namespace MovieBooking.Models.Models
{
	public class Booking
	{
        public int BookingId { get; set; }
        public string MovieName { get; set; }
        public int PhoneNumber { get; set; }
        public int TotalSeats { get; set; }
        public int BookedSeats { get; set; }
        public DateTime DateAndTime { get; set; }
        public string MovieTime { get; set; }
        public decimal Price { get; set; }
    }
}
