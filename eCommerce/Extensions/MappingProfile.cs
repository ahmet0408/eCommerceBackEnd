using AspNetCoreHero.ToastNotification.Abstractions;
using AutoMapper;
using eCommerce.bll.DTOs.LanguageDTO;
using eCommerce.bll.DTOs.NotificationDTO;
using eCommerce.bll.DTOs.OrderDTO;
using eCommerce.bll.DTOs.ProductDTO;
using eCommerce.dal.Model.Language;
using eCommerce.dal.Model.Notification;
using eCommerce.dal.Model.Order;
using eCommerce.dal.Model.Product;
using System.Linq;

namespace eCommerce.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Language, LanguageDTO>();
            CreateMap<LanguageDTO, Language>();

            CreateMap<CreateCategoryDTO, Category>();
            CreateMap<CategoryTranslateDTO, CategoryTranslate>().ReverseMap();
            CreateMap<EditCategoryDTO, Category>().ReverseMap();
            CreateMap<Category, CategoryDTO>()
                .ForMember(p => p.Name, p => p.MapFrom(p => p.CategoryTranslates.Select(p => p.Name).FirstOrDefault()))
                .ForMember(p => p.Id, p => p.MapFrom(p => p.CategoryId));                

            CreateMap<CreateProductDTO, Product>();
            CreateMap<EditProductDTO, Product>().ReverseMap();
            CreateMap<ProductTranslateDTO, ProductTranslate>().ReverseMap();
            CreateMap<Product, ProductDetailDTO>()
                .ForMember(p => p.Id, p => p.MapFrom(p => p.ProductId))
                .ForMember(p => p.Name, p => p.MapFrom(p => p.ProductTranslates.Select(p => p.Name).FirstOrDefault()))
                .ForMember(p => p.ShortDescription, p => p.MapFrom(p => p.ProductTranslates.Select(p => p.ShortDescription).FirstOrDefault()))
                .ForMember(p => p.Description, p => p.MapFrom(p => p.ProductTranslates.Select(p => p.Description).FirstOrDefault()))
                .ForMember(p => p.CategoryId, p => p.MapFrom(p => p.ProductCategory.Select(p => p.CategoryId).FirstOrDefault()));
            CreateMap<ProductCategory, ProductDTO>()
                .ForMember(p => p.Id, p => p.MapFrom(p => p.Product.ProductId))
                .ForMember(p => p.SKU, p => p.MapFrom(p => p.Product.SKU))
                .ForMember(p => p.BrandId, p => p.MapFrom(p => p.Product.BrandId))
                .ForMember(p => p.CategoryId, p => p.MapFrom(p => p.CategoryId))
                .ForMember(p => p.IsNew, p => p.MapFrom(p => p.Product.IsNew))
                .ForMember(p => p.IsStock, p => p.MapFrom(p => p.Product.IsStock))
                .ForMember(p => p.ImagePath, p => p.MapFrom(p => p.Product.ImagePath))
                .ForMember(p => p.DiscountPrice, p => p.MapFrom(p => p.Product.DiscountPrice))
                .ForMember(p => p.RegularPrice, p => p.MapFrom(p => p.Product.RegularPrice))
                .ForMember(p => p.Order, p => p.MapFrom(p => p.Product.Order))
                .ForMember(p => p.Name, p => p.MapFrom(p => p.Product.ProductTranslates.Select(p => p.Name).FirstOrDefault()))
                .ForMember(p => p.ShortDescription, p => p.MapFrom(p => p.Product.ProductTranslates.Select(p => p.ShortDescription).FirstOrDefault()));
            CreateMap<Product, ProductDTO>()
                .ForMember(p => p.ShortDescription, p => p.MapFrom(p => p.ProductTranslates.Select(p => p.ShortDescription).FirstOrDefault()))
                .ForMember(p => p.Name, p => p.MapFrom(p => p.ProductTranslates.Select(p => p.Name).FirstOrDefault()))
                .ForMember(p => p.Id, p => p.MapFrom(p => p.ProductId))
                .ForMember(p => p.CategoryId, p => p.MapFrom(p => p.ProductCategory.Select(p => p.CategoryId).FirstOrDefault()));

            CreateMap<BrandTranslateDTO, BrandTranslate>().ReverseMap();
            CreateMap<CreateBrandDTO, Brand>();
            CreateMap<EditBrandDTO, Brand>().ReverseMap();
            CreateMap<BrandCategory, BrandDTO>()
                .ForMember(p => p.Id, p => p.MapFrom(p => p.Brand.BrandId))
                .ForMember(p => p.Order, p => p.MapFrom(p => p.Brand.Order))
                .ForMember(p => p.Icon, p => p.MapFrom(p => p.Brand.Icon))
                .ForMember(p => p.Name, p => p.MapFrom(p => p.Brand.BrandTranslates.Select(p => p.Name).FirstOrDefault()))
                .ForMember(p => p.Category, p => p.MapFrom(p => p.Category.CategoryTranslates.Select(p => p.Name).FirstOrDefault()));
            CreateMap<Brand, BrandDTO>()
                .ForMember(p => p.Id, p => p.MapFrom(p => p.BrandId))
                .ForMember(p => p.Name, p => p.MapFrom(p => p.BrandTranslates.Select(p => p.Name).FirstOrDefault()));

            CreateMap<Order, OrderDTO>()
                .ForMember(p => p.FirstName, p => p.MapFrom(p => p.User.FirstName))
                .ForMember(p => p.LastName, p => p.MapFrom(p => p.User.LastName))
                .ForMember(p => p.OrderStatus, p => p.MapFrom(p => p.OrderStatus.Name));

            CreateMap<CreateNotificationDTO, dal.Model.Notification.Notification>();
            CreateMap<NotificationTranslateDTO, NotificationTranslate>();
            CreateMap<dal.Model.Notification.Notification, NotificationDTO>()
                .ForMember(p => p.Title, p => p.MapFrom(p => p.NotificationTranslates.Select(p => p.Title).FirstOrDefault()))
                .ForMember(p => p.ShortDesc, p => p.MapFrom(p => p.NotificationTranslates.Select(p => p.ShortDesc).FirstOrDefault()));
        }
    }
}
