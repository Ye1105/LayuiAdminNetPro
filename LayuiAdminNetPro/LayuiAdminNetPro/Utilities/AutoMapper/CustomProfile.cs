using AutoMapper;
using CodeHelper.Common;
using LayuiAdminNetCore.AdminModels;
using LayuiAdminNetCore.DtoModels;
using LayuiAdminNetCore.Enums;

namespace LayuiAdminNetPro.Utilities.AutoMapper
{
    /// <summary>
    /// 使用 profiles 统一管理 mapping 信息
    /// </summary>
    public class CustomProfile : Profile
    {
        public CustomProfile()
        {
            CreateMap<AdminAccount, DtoAdminAccount>()
                .ForMember(dest => dest.Sex, opt => opt.MapFrom(x => EnumDescriptionAttribute.GetEnumDescription((SexStatus)x.Sex)))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(x => EnumDescriptionAttribute.GetEnumDescription((Status)x.Status)))
                .ForMember(dest => dest.JobStatus, opt => opt.MapFrom(x => EnumDescriptionAttribute.GetEnumDescription((JobStatus)x.JobStatus)));
        }
    }
}