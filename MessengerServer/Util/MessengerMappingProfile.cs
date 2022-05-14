using AutoMapper;
using MessengerServer.DAL.Entities;
using MessengerServer.DTOs;

namespace MessengerServer.Util
{
    public class MessengerMappingProfile : Profile
    {
        public MessengerMappingProfile()
        {
            CreateMap<UserRegisterRequestDTO, User>()
                .ForMember(entity => entity.Username,
                    cfg => cfg.MapFrom(dto => dto.username))
                .ForMember(entity => entity.Password,
                    cfg => cfg.MapFrom(dto => dto.password))
                .ForMember(entity => entity.UserPictureId,
                    cfg => cfg.Ignore())
                .ForMember(entity => entity.Avatar,
                    cfg => cfg.Ignore())
                .ForMember(entity => entity.Id,
                    cfg => cfg.Ignore())
                .ForMember(entity => entity.Chats,
                    cfg => cfg.Ignore())
                .ForMember(entity => entity.Messages,
                    cfg => cfg.Ignore());

            CreateMap<User, UserAuthResponseDTO>()
                .ForMember(entity => entity.userID,
                    cfg => cfg.MapFrom(dto => dto.Id))
                .ForMember(entity => entity.rejectReason,
                    cfg => cfg.Ignore());

            CreateMap<UserAuthRequestDTO, User>()
                .ForMember(entity => entity.Username,
                    cfg => cfg.MapFrom(dto => dto.username))
                .ForMember(entity => entity.Password,
                    cfg => cfg.MapFrom(dto => dto.password))
                .ForMember(entity => entity.UserPictureId,
                    cfg => cfg.Ignore())
                .ForMember(entity => entity.Avatar,
                    cfg => cfg.Ignore())
                .ForMember(entity => entity.Id,
                    cfg => cfg.Ignore())
                .ForMember(entity => entity.Chats,
                    cfg => cfg.Ignore())
                .ForMember(entity => entity.Messages,
                    cfg => cfg.Ignore());
        }
    }
}
