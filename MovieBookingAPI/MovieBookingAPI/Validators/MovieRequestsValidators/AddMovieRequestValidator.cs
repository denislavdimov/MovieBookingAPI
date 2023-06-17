using FluentValidation;
using MovieBooking.Models.Requests.MovieRequests;

namespace MovieBookingAPI.Validators.MovieRequestsValidators
{
	public class AddMovieRequestValidator : AbstractValidator<AddMovieRequest>
	{
		public AddMovieRequestValidator()
		{
			RuleFor(n => n.Name)
				.NotEmpty()
				.NotNull();

			RuleFor(n => n.Price)
				.NotEmpty()
				.NotNull()
				.GreaterThan(0);

			RuleFor(n => n.Showtime)
				.NotEmpty()
				.NotNull()
				.Matches("^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$");
		}
	}
}
