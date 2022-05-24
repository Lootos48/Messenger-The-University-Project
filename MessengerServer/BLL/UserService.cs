using AutoMapper;
using MessengerServer.DAL.Entities;
using MessengerServer.DAL.Repositories;
using MessengerServer.DTOs;
using MessengerServer.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerServer.BLL
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(
            UserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Create method
        /// </summary>
        /// <param name="registerUserRequest"></param>
        /// <returns></returns>
        public async Task<UserAuthResponseDTO> RegisterAsync(UserRegisterRequestDTO registerUserRequest)
        {
            User user = _mapper.Map<User>(registerUserRequest);
            if (_userRepository.FindByUserNameAsync(registerUserRequest.username) != null)
            {
                return new UserAuthResponseDTO { rejectReason = "User with that credentials is already exist" };
            }

            await _userRepository.CreateAsync(user);
            user = await _userRepository.FindByUserNameAsync(registerUserRequest.username);

            UserAuthResponseDTO response = _mapper.Map<UserAuthResponseDTO>(user);
            return response;
        }

        /// <summary>
        /// Login with credentials
        /// </summary>
        /// <param name="loginUserRequest"></param>
        /// <returns></returns>
        public async Task<UserAuthResponseDTO> LoginAsync(UserAuthRequestDTO loginUserRequest)
        {
            User user = await _userRepository.FindByUserNameAsync(loginUserRequest.username);
            return BuildAuthResponse(loginUserRequest, user);
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        public Task<List<User>> GetUsers()
        {
            return _userRepository.GetAllAsync();
        }

        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<User> GetUserById(int id)
        {
            return _userRepository.FindByIdAsync(id);
        }

        /// <summary>
        /// Get user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Task<User> GetUserByUsername(string username)
        {
            return _userRepository.FindByUserNameAsync(username);
        }

        /// <summary>
        /// Edit user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="ParametersValidationException"></exception>
        public async Task EditAsync(UserEditRequestDTO request)
        {
            User user = await _userRepository.FindByUserNameAsync(request.Username);
            if (user.Id != request.Id)
            {
                throw new ParametersValidationException("User with that username is already exist");
            }

            User userToUpdate = _mapper.Map<User>(request);

            await _userRepository.UpdateAsync(userToUpdate);
        }

        public async Task DeleteAsync(int userId)
        {
            User userToDelete = await _userRepository.FindByIdAsync(userId);
            if (userToDelete is null)
            {
                throw new NotFoundException("User not found");
            }

            await DeleteAsync(userToDelete);
        }

        /// <summary>
        /// Delete user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Task DeleteAsync(User user)
        {
            return _userRepository.DeleteAsync(user);
        }

        private static UserAuthResponseDTO BuildAuthResponse(UserAuthRequestDTO loginUserRequest, User user)
        {
            UserAuthResponseDTO response = new UserAuthResponseDTO();
            if (user is null)
            {
                response.rejectReason = "User with that credentials wasn`t found";
            }
            else if (user.Password != loginUserRequest.password)
            {
                response.rejectReason = "User with that credentials wasn`t found";
            }
            else
            {
                response.userID = user.Id;
            }

            return response;
        }
    }
}
