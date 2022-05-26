using AutoMapper;
using MessengerServer.DAL.Entities;
using MessengerServer.DAL.Repositories;
using System.Threading.Tasks;

namespace MessengerServer.BLL
{
    public class ImageService
    {
        private readonly ImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public ImageService(
            ImageRepository imageRepository, 
            IMapper mapper)
        {
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<int> CreateImage(Image image)
        {
            int createdImageId = await _imageRepository.CreateAsync(image);

            return createdImageId;
        }
    }
}
