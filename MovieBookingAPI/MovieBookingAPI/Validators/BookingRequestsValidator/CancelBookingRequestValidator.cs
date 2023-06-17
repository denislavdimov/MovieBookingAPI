using FluentValidation;
using MovieBooking.Models.Requests.BookingRequests;

namespace MovieBookingAPI.Validators.BookingRequestsValidator
{
	public class CancelBookingRequestValidator : AbstractValidator<CancelBookingRequest>
	{
        public CancelBookingRequestValidator()
        {
            RuleFor(cB => cB.Id)
                .NotNull()
                .NotEmpty();
        }
    }
}
