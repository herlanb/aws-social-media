namespace AwsSocialMedia.Insfrastructure.Mapping
{
    using AutoMapper;
    using Core.DTOs;
    using Core.Entities;
    
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Post, PostDto>();
            CreateMap<PostDto, Post>();
        }
    }
}
