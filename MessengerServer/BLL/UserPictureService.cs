using AutoMapper;
using MessengerServer.DAL.Entities;
using MessengerServer.DAL.Repositories;
using System.Threading.Tasks;

namespace MessengerServer.BLL
{
    public class UserPictureService
    {
        private readonly UserPictureRepository _avatarRepository;
        private readonly IMapper _mapper;

        public UserPictureService(
            UserPictureRepository avatarRepository,
            IMapper mapper)
        {
            _avatarRepository = avatarRepository;
            _mapper = mapper;
        }

        public async Task<int> CreatePicture(UserPicture image)
        {
            int createdImageId = await _avatarRepository.CreateAsync(image);

            return createdImageId;
        }
    }
}
