namespace AwsSocialMedia.Insfrastructure.Mapping
{
    using AutoMapper;
    using Core.DTOs;
    using Core.Entities;
    
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Post, PostGetDto>()
                .ForMember(dest => dest.PostId, opt => opt.MapFrom(src => src.Id));

            CreateMap<PostGetDto, Post>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PostId))
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore());

            CreateMap<PostCreateDto, Post>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())        
                .ForMember(dest => dest.Date, opt => opt.Ignore())      
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore());

            CreateMap<PostUpdateDto, Post>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.PostId))
                .ForMember(dest => dest.UserId, opt => opt.Ignore())    
                .ForMember(dest => dest.Date, opt => opt.Ignore())      
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForMember(dest => dest.Comments, opt => opt.Ignore());
        }
    }
}
