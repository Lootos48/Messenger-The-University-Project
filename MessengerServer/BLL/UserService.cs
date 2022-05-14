using AutoMapper;
using MessengerServer.DAL.Entities;
using MessengerServer.DAL.Repositories;
using MessengerServer.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessengerServer.BLL
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly UserChatsRepository _userChatsRepository;
        private readonly IMapper _mapper;

        public UserService(
            UserRepository userRepository,
            UserChatsRepository userChatsRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _userChatsRepository = userChatsRepository;
            _mapper = mapper;
        }

        public async Task<UserAuthResponseDTO> RegisterAsync(UserRegisterRequestDTO registerUserRequest)
        {
            User user = _mapper.Map<User>(registerUserRequest);

            await _userRepository.CreateAsync(user);
            user = await _userRepository.FindUserByUserNameAsync(registerUserRequest.username);

            UserAuthResponseDTO response = _mapper.Map<UserAuthResponseDTO>(user);
            return response;
        }

        public async Task<UserAuthResponseDTO> LoginAsync(UserAuthRequestDTO loginUserRequest)
        {
            User user = await _userRepository.FindUserByUserNameAsync(loginUserRequest.username);
            return BuildAuthResponse(loginUserRequest, user);
        }

        public async Task<List<Chat>> GetChatsAsync(string userID)
        {
            List<ChatsUsers> chatsUsers = await _userChatsRepository.GetUserChats(userID);
            List<Chat> userChats = new List<Chat>();

            chatsUsers.ForEach(x =>
            {
                userChats.Add(x.Chat);
            });

            return userChats;
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
                response.userID = user.Id.ToString();
            }

            return response;
        }
    }
}
