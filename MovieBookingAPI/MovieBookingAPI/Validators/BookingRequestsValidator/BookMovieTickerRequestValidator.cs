using FluentValidation;
using MovieBooking.Models.Requests.BookingRequests;

namespace MovieBookingAPI.Validators.BookingRequestsValidator
{
	public class BookMovieTickerRequestValidator : AbstractValidator<BookMovieTicketRequest>
	{
        public BookMovieTickerRequestValidator()
        {
            RuleFor(b => b.FullName)
                .NotEmpty()
                .NotNull();

			RuleFor(b => b.PhoneNumber)
				.NotEmpty()
				.NotNull()
				.Matches("/^\\s*(?:\\+?(\\d{1,3}))?[-. (]*(\\d{3})[-. )]*(\\d{3})[-. ]*(\\d{4})(?: *x(\\d+))?\\s*$/")
				.MaximumLength(10);

			RuleFor(b => b.Seats)
				.NotEmpty()
				.NotNull()
				.InclusiveBetween(1, 20);

			RuleFor(b => b.Showtime)
				.NotEmpty()
				.NotNull()
				.Matches("^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$");

			RuleFor(b => b.Price)
				.NotEmpty()
				.NotNull()
				.GreaterThan(0);

			RuleFor(m => m.Movie.Id)
				.NotEmpty()
				.NotNull();
		}
    }
}
