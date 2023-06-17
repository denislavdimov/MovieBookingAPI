using FluentValidation;
using MovieBooking.Models.Requests.MovieRequests;

namespace MovieBookingAPI.Validators.MovieRequestsValidators
{
	public class GetByIdRequestValidator : AbstractValidator<GetByIdMovieRequest>
	{
        public GetByIdRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .NotNull();
        }
    }
}
