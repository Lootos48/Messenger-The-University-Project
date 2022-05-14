using AutoMapper;
using MessengerServer.DAL.Entities;
using MessengerServer.DAL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MessengerServer.BLL
{
    public class ChatService
    {
        private readonly ChatRepository _chatRepository;
        private readonly IMapper _mapper;


        public ChatService(
            ChatRepository chatRepository,
            IMapper mapper)
        {
            _chatRepository = chatRepository;
            _mapper = mapper;
        }
    }
}
