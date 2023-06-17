using MovieBooking.BL.Interfaces;
using MovieBooking.DL.Interfaces;
using MovieBooking.Models.Models;
using MovieBooking.Models.Requests;

namespace MovieBooking.BL.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task Add(AddUserRequest user)
		{
			await _userRepository.Add(new User()
			{
				Id = Guid.NewGuid(),
				Email = user.Email,
				Password = user.Password,
			});
		}

		public async Task<User?> GetUserInfo(string email, string password)
		{
			return await _userRepository.GetUserInfo(email, password);
		}
    }
}
