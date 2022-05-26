using AutoMapper;
using MessengerServer.DAL.Entities;
using MessengerServer.DTOs;
using MessengerServer.DTOs.Chat;
using MessengerServer.DTOs.ChatsUsers;
using MessengerServer.DTOs.Message;
using MessengerServer.DTOs.User;
using System.Linq;

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
                    cfg => cfg.UseDestinationValue())
                .ForMember(entity => entity.Messages,
                    cfg => cfg.UseDestinationValue())
                .ForMember(entity => entity.Chats,
                    cfg => cfg.UseDestinationValue())
                .ForMember(entity => entity.UserPictureId,
                    cfg => cfg.UseDestinationValue());

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
                .ForMember(entity => entity.Users,
                    cfg => cfg.Ignore())
                .ForMember(entity => entity.Messages,
                    cfg => cfg.Ignore());

            CreateMap<UserChatInteractRequestDTO, ChatsUsers>()
                .ForMember(entity => entity.Chat,
                    cfg => cfg.Ignore())
                .ForMember(entity => entity.User,
                    cfg => cfg.Ignore());

            CreateMap<Message, MessageDTO>()
                .ForMember(dto => dto.Username,
                    cfg => cfg.MapFrom(entity => entity.Sender.Username))
                .ForMember(dto => dto.SendTime,
                    cfg => cfg.MapFrom(entity => entity.SendTime.ToShortTimeString()))
                .ForMember(dto => dto.SendDate,
                    cfg => cfg.MapFrom(entity => entity.SendTime.ToShortDateString()))
                .ForMember(dto => dto.UserAvatar,
                    cfg => cfg.Ignore())
                .ForMember(dto => dto.Image,
                    cfg => cfg.Ignore())
                .AfterMap(async (entity, dto) =>
                {
                    if (entity.Picture != null)
                    {
                        dto.Image = await FileService.ConvertFileToByteArray(entity.Picture.Path);
                    }

                    if (entity.Sender.Avatar != null)
                    {
                        dto.UserAvatar = await FileService.ConvertFileToByteArray(entity.Sender.Avatar.Path);
                    }
                });

            CreateMap<EditMessageRequestDTO, Message>();

            CreateMap<CreateMessageRequestDTO, Message>()
                .ForMember(entity => entity.ChatId,
                    cfg => cfg.MapFrom(dto => dto.ChatId))
                .ForMember(entity => entity.UserId,
                    cfg => cfg.MapFrom(dto => dto.UserId))
                .ForMember(entity => entity.Text,
                    cfg => cfg.MapFrom(dto => dto.Text))
                .ForMember(entity => entity.Picture,
                    cfg => cfg.Ignore());

            CreateMap<User, UserDTO>()
                .ForMember(dto => dto.Avatar,
                    cfg => cfg.Ignore())
                .ForMember(dto => dto.Chats,
                    cfg => cfg.MapFrom(entity => entity.Chats.Select(x => x.Chat).ToList()))
                .AfterMap(async (entity, dto) =>
                {
                    if (entity.Avatar != null)
                    {
                        dto.Avatar = await FileService.ConvertFileToByteArray(entity.Avatar.Path);
                    }
                });

            CreateMap<Chat, ChatDTO>()
                .ForMember(dto => dto.Users,
                    cfg => cfg.MapFrom(entity => entity.Users.Select(x => x.User).ToList()));
        }
    }
}
