using AutoMapper;
using DATN_Back_end.Dto.DtoComment;
using DATN_Back_end.Dto.DtoDepartment;
using DATN_Back_end.Dto.DtoFormRequest;
using DATN_Back_end.Dto.DtoReport;
using DATN_Back_end.Dto.DtoStatus;
using DATN_Back_end.Dto.DtoUser;
using DATN_Back_end.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATN_Back_end.Services
{
    public static class MapperService
    {
        private static readonly MapperConfiguration config = new(CreateMap);
        private static readonly IMapper mapper = config.CreateMapper();

        private static void CreateMap(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<User, UserItem>();
            cfg.CreateMap<User, UserDetail>();
            cfg.CreateMap<UserDetail, User>();
            cfg.CreateMap<UserFormCreate, User>();
            cfg.CreateMap<UserFormUpdate, User>();

            cfg.CreateMap<Department, DepartmentDetail>();
            cfg.CreateMap<Department, DepartmentItem>()
                .ForMember(departmentItem => departmentItem.Manager,
                option => option.MapFrom(department => department.Manager.ConvertTo<UserDetail>()));
            cfg.CreateMap<DepartmentForm, Department>();

            cfg.CreateMap<FormRequest, FormRequestItem>();
            cfg.CreateMap<FormRequest, FormRequestDetail>();
            cfg.CreateMap<FormRequestForm, FormRequest>();

            cfg.CreateMap<FormStatus, FormStatusDetail>();
            cfg.CreateMap<FormStatus, FormStatusItem>();
            cfg.CreateMap<FormStatusForm, FormStatus>();

            cfg.CreateMap<CommentForm, Comment>();
            cfg.CreateMap<Comment, CommentDetail>();
            cfg.CreateMap<Comment, CommentItem>();
            cfg.CreateMap<CommentItem, Comment>();

            cfg.CreateMap<Report, ReportItem>()
                .ForMember(reportItem => reportItem.Comments,
                option => option.MapFrom(book => book.Comments.Select(x => x.ConvertTo<CommentItem>())));

            cfg.CreateMap<Report, ReportDetail>()
                .ForMember(reportItem => reportItem.Comments,
                option => option.MapFrom(book => book.Comments
                                                .Select(x => x)));
            cfg.CreateMap<ReportForm, Report>();
            cfg.CreateMap<ReportForm, ReportFormDto>();
            cfg.CreateMap<ReportFormDto, Report>();
        }

        public static T ConvertTo<T>(this object source)
        {
            if (source == null)
            {
                throw new NullReferenceException();
            }

            return mapper.Map<T>(source);
        }
        public static void CopyTo(this object source, object destination)
        {
            if (source == null)
            {
                throw new NullReferenceException("Source can't be null");
            }

            if (destination == null)
            {
                throw new NullReferenceException("Destination can't be null");
            }

            mapper.Map(source, destination);
        }
    }
}
