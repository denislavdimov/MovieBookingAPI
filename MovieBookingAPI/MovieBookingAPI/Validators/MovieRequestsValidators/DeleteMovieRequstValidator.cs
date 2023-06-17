using FluentValidation;
using MovieBooking.Models.Requests.MovieRequests;

namespace MovieBookingAPI.Validators.MovieRequestsValidators
{
	public class DeleteMovieRequstValidator : AbstractValidator<DeleteMovieRequest>
	{
        public DeleteMovieRequstValidator()
        {
			RuleFor(n => n.Id)
				.NotEmpty()
				.NotNull();
		}
    }
}
