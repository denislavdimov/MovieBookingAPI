using MovieBooking.Models.Models;
using MovieBooking.Models.Requests;

namespace MovieBooking.BL.Interfaces
{
	public interface IUserService
	{
		public Task<User?> GetUserInfo(string username, string password);
		public Task Add(AddUserRequest user);
	}
}
