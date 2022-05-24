using AutoMapper;
using MessengerServer.DAL.Entities;
using MessengerServer.DTOs;
using MessengerServer.DTOs.Chat;
using MessengerServer.DTOs.ChatsUsers;
using MessengerServer.DTOs.Message;

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

            CreateMap<UserEditRequestDTO, User>()
                .ForMember(entity => entity.Id,
                    cfg => cfg.MapFrom(dto => dto.Id))
                .ForMember(entity => entity.Username,
                    cfg => cfg.MapFrom(dto => dto.Username))
                .ForMember(entity => entity.Password,
                    cfg => cfg.MapFrom(dto => dto.Password))
                .ForMember(entity => entity.Avatar,
                    cfg => cfg.Ignore())
                .ForMember(entity => entity.Messages,
                    cfg => cfg.Ignore())
                .ForMember(entity => entity.Chats,
                    cfg => cfg.Ignore())
                .ForMember(entity => entity.UserPictureId,
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

            CreateMap<ChatCreateRequestDTO, Chat>()
                .ForMember(entity => entity.Title,
                    cfg => cfg.MapFrom(dto => dto.Title))
                .ForMember(entity => entity.Id,
                    cfg => cfg.Ignore())
                .ForMember(entity => entity.Users,
                    cfg => cfg.Ignore())
                .ForMember(entity => entity.Messages,
                    cfg => cfg.Ignore());

            CreateMap<ChatEditRequestDTO, Chat>()
                .ForMember(entity => entity.Id,
                    cfg => cfg.MapFrom(dto => dto.Id))
                .ForMember(entity => entity.Title,
                    cfg => cfg.MapFrom(dto => dto.Title))
                .ForMember(entity => entity.Id,
                    cfg => cfg.Ignore())
                .ForMember(entity => entity.Users,
                    cfg => cfg.Ignore())
                .ForMember(entity => entity.Messages,
                    cfg => cfg.Ignore());

            CreateMap<UserChatInteractRequestDTO, ChatsUsers>()
                .ForMember(entity => entity.Chat,
                    cfg => cfg.Ignore())
                .ForMember(entity => entity.User,
                    cfg => cfg.Ignore());

            CreateMap<Message, MessageDTO>();

            CreateMap<MessageDTO, Message>();
        }
    }
}
