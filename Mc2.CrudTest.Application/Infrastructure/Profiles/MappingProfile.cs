using AutoMapper;
using Mc2.CrudTest.Application.DTOs.Customer;
using Mc2.CrudTest.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mc2.CrudTest.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CustomerDto, CreateCustomerDto>().ReverseMap();
            CreateMap<CustomerDto, UpdateCustomerDto>().ReverseMap();

            CreateMap<Customer, CustomerDto>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => $"+{src.PhoneNumber}"));
                
            CreateMap<Customer, CreateCustomerDto>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => $"+{src.PhoneNumber}"));

            CreateMap<Customer, UpdateCustomerDto>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => $"+{src.PhoneNumber}"));

            CreateMap<CustomerDto, Customer>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => ulong.Parse(src.PhoneNumber
                                                                                                .Trim()
                                                                                                .Replace(" ", "")
                                                                                                .Replace("+", "")
                                                                                                .Replace("-", ""))))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname.ToUpper()))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname.ToUpper()))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.ToUpper()));

            CreateMap<CreateCustomerDto, Customer>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => ulong.Parse(src.PhoneNumber
                                                                                                .Trim()
                                                                                                .Replace(" ", "")
                                                                                                .Replace("+", "")
                                                                                                .Replace("-", ""))))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname.ToUpper()))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname.ToUpper()))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.ToUpper()));

            CreateMap<UpdateCustomerDto, Customer>()
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => ulong.Parse(src.PhoneNumber
                                                                                                .Trim()
                                                                                                .Replace(" ", "")
                                                                                                .Replace("+", "")
                                                                                                .Replace("-", ""))))
                .ForMember(dest => dest.Firstname, opt => opt.MapFrom(src => src.Firstname.ToUpper()))
                .ForMember(dest => dest.Lastname, opt => opt.MapFrom(src => src.Lastname.ToUpper()))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.ToUpper()));
        }
    }
}
