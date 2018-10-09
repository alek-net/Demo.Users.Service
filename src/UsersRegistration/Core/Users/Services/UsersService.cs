using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersRegistration
{
    public class UsersService : IUsersService
	{
		public const string MESSAGE_USER_CREATED = "User created";
		public const string MESSAGE_USER_EXISTS = "User with such email already exists";

		private IUsersRepository _usersRepository;

		public UsersService(IUsersRepository usersRepository)
		{		
			_usersRepository = usersRepository;
		}

		public async Task<CreateUserResponse> CreateUser(CreateUserRequest request)
		{
			var user = MapCreateUserRequestToUser(request);

			var response = new CreateUserResponse()
			{
				Message = MESSAGE_USER_CREATED,
				Status = ResponseStatus.Success,
				User = user
			};

			try
			{
				if (await _usersRepository.CheckUserExists(request.TenantId, user.Email))
				{
					response.Status = ResponseStatus.ValidationError;
					response.Message = MESSAGE_USER_EXISTS;
					response.User = null;
				}
				else
				{
					user = await _usersRepository.CreateUser(user);
				}
			}
			catch (Exception ex)
			{
				response.Status = ResponseStatus.Error;
				response.Message = ex.Message;
				response.User = null;
			}
			
			return response;
		}

		private static User MapCreateUserRequestToUser(CreateUserRequest request)
		{
			var hashResult = CryptoService.GenerateHash(request.Password);

			var user = new User(
				tenantId: request.TenantId,
				email: request.Email,
				firstName: request.FirstName,
				lastName: request.LastName,
				address: request.Address,
				personalNumber: request.PersonalNumber,
				favoriteFootballTeam: request.FavoriteFootballTeam,
				passwordHash: hashResult.Hash,
				passwordSalt: hashResult.Salt
			);

			return user;
		}

		public void ValidateUser(CreateUserRequest request)
		{

		}

		public async Task<User> GetUserById(int userId)
		{
			var users = await _usersRepository.LoadUsers(user => user.UserId == userId);

			return users.FirstOrDefault();
		}

		public async Task<IEnumerable<User>> GetUsers(int tenantId)
		{
			var users = await _usersRepository.LoadUsers(user => user.TenantId == tenantId);

			return users;
		}

		public async Task<bool> DeleteUser(DeleteUserRequest request)
		{
			var result = await _usersRepository.DeleteUser(request.TenantId, request.UserId);

			return result;
		}

		public bool DoesUserExists(string email)
		{
			return false;
		}

		public async Task<User> UpdateUser(UpdateUserRequest request)
		{
			var userToBeUpdated = await GetUserById(request.UserId);

			userToBeUpdated.Update(
				email: request.Email,
				firstName: request.FirstName,
				lastName: request.LastName,
				address: request.Address,
				personalNumber: request.PersonalNumber,
				favoriteFootballTeam: request.FavoriteFootballTeam
				);
			 
			var users = await _usersRepository.UpdateUser(userToBeUpdated);

			return userToBeUpdated;
		}
	}


}
