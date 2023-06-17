using MovieBooking.Models.Models;

namespace MovieBooking.DL.Interfaces
{
	public interface IUserRepository
	{
		public Task Add(User user);
		public Task<User?> GetUserInfo(string email, string password);
	}
}
