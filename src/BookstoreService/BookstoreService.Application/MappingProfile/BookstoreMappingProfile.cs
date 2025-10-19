using AutoMapper;
using BookstoreService.Application.Models;
using BookstoreService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreService.Application.MappingProfile
{
    public class BookstoreMappingProfile : Profile
    {
        public BookstoreMappingProfile()
        {
            // Create
            CreateMap<BookstoreCreateRequest, Bookstore>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())       // Tự sinh PK
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()) // Cloudinary xử lý
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) // Service set
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore()); // Service set

            // Update
            CreateMap<BookstoreUpdateRequest, Bookstore>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())       // Service set
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore()) // Cloudinary xử lý
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) // Giữ nguyên
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsActive, opt => opt.Ignore()); // Giữ nguyên
        }
    }
}
