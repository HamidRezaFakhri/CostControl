using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using CostControlBusinessModel = CostControl.BusinessEntity.Models.CostControl;
using CostControlEntityModel = CostControl.Entity.Models.CostControl;
using SecurityBusinessModel = CostControl.BusinessEntity.Models.Security;
using SecurityEntityModel = CostControl.Entity.Models.Security;

namespace CostControl.BusinessLogic.Mapper
{
    //public class DateTimeTypeConverter : ITypeConverter<string, DateTime>
    //{
    //    public DateTime Convert(string source, DateTime destination, ResolutionContext context)
    //    {
    //        return System.Convert.ToDateTime(source);
    //    }
    //}

    public class CurrencyFormatter : IValueConverter<decimal, string>
    {
        public string Convert(decimal sourceMember, ResolutionContext context)
            => sourceMember.ToString("c");
    }

    public class TypeTypeConverter : ITypeConverter<string, Type>
    {
        public Type Convert(string source, Type destination, ResolutionContext context)
        {
            return Assembly.GetExecutingAssembly().GetType(source);
        }
    }

    //http://docs.automapper.org/en/stable/Configuration.html
    //https://visualstudiomagazine.com/Articles/2012/02/01/Simplify-Your-Projections-with-AutoMapper.aspx?Page=2
    //https://csharp.hotexamples.com/examples/-/AutoMapper/CreateMap/php-automapper-createmap-method-examples.html
    //https://www.fuget.org/packages/AutoMapper/6.2.0/lib/net40/AutoMapper.dll/AutoMapper/IMappingExpression%602
    //https://searchcode.com/codesearch/view/4344762/
    //https://code.i-harness.com/en/q/2b331be
    public class ClientMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ClientMappings"; }
        }

        public ClientMappingProfile()
        {
            //SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
            //DestinationMemberNamingConvention = new PascalCaseNamingConvention();

            CreateMap<SecurityEntityModel.Role, SecurityBusinessModel.Role>(MemberList.None)
                //.ForMember(dest => dest.Users, opt => opt.Ignore())
                //.ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users))
                .ReverseMap();

            CreateMap<CostControlEntityModel.SalePoint, CostControlBusinessModel.SalePoint>(MemberList.None)
                .ReverseMap();

            //CreateMap<CostControlBusinessModel.SaleCenter, CostControlEntityModel.SaleCenter>()
            //    .ForMember(dest => dest.Depo, opt => opt.Ignore())
            //    .ForMember(dest => dest.Name, opt => opt.Ignore())
            //    .ForMember(dest => dest.Sales, opt => opt.Ignore())
            //    .ReverseMap();

            //CreateMap<IEnumerable<CostControlBusinessModel.SaleCenter>, IEnumerable<CostControlEntityModel.SaleCenter>>()
            //    .ReverseMap();

            CreateMap<CostControlEntityModel.ConsumptionUnit, CostControlBusinessModel.ConsumptionUnit>(MemberList.None).ReverseMap();
            CreateMap<CostControlEntityModel.OverCostType, CostControlBusinessModel.OverCostType>(MemberList.None).ReverseMap();
            CreateMap<CostControlEntityModel.OverCost, CostControlBusinessModel.OverCost>(MemberList.None).ReverseMap();
            CreateMap<CostControlEntityModel.Ingredient, CostControlBusinessModel.Ingredient>(MemberList.None).ReverseMap();

            CreateMap<CostControlEntityModel.Food, CostControlBusinessModel.Food>(MemberList.None).ReverseMap();
            CreateMap<CostControlEntityModel.Recipe, CostControlBusinessModel.Recipe>(MemberList.None).ReverseMap();

            CreateMap<CostControlEntityModel.Menu, CostControlBusinessModel.Menu>(MemberList.None).ReverseMap();
            CreateMap<CostControlEntityModel.MenuItem, CostControlBusinessModel.MenuItem>(MemberList.None).ReverseMap();
            CreateMap<CostControlEntityModel.Depo, CostControlBusinessModel.Depo>(MemberList.None).ReverseMap();
            CreateMap<CostControlEntityModel.Buffet, CostControlBusinessModel.Buffet>(MemberList.None).ReverseMap();
            CreateMap<CostControlEntityModel.Draft, CostControlBusinessModel.Draft>(MemberList.None).ReverseMap();
            CreateMap<CostControlEntityModel.DraftItem, CostControlBusinessModel.DraftItem>(MemberList.None).ReverseMap();
            CreateMap<CostControlEntityModel.Inventory, CostControlBusinessModel.Inventory>(MemberList.None).ReverseMap();
            CreateMap<CostControlEntityModel.Sale, CostControlBusinessModel.Sale>(MemberList.None).ReverseMap();
            CreateMap<CostControlEntityModel.SaleItem, CostControlBusinessModel.SaleItem>(MemberList.None).ReverseMap();
            CreateMap<CostControlEntityModel.CostPointGroup, CostControlBusinessModel.CostPointGroup>(MemberList.None).ReverseMap();

            CreateMap<CostControlEntityModel.CostPoint, CostControlBusinessModel.CostPoint>(MemberList.None)
                .ForMember(dest => dest.CostPointGroupName, opt => opt.MapFrom(src => src.CostPointGroup.Name))
                .PreserveReferences()
                .ReverseMap();

            CreateMap<CostControlEntityModel.SaleCostPoint, CostControlBusinessModel.SaleCostPoint>(MemberList.None)
                //.ForMember(dest => dest.CostPointName, opt => opt.MapFrom(src => src.CostPoint.Name))
                //.ForMember(dest => dest.SalePointName, opt => opt.MapFrom(src => src.SalePoint.Name))
                .ReverseMap();

            CreateMap<CostControlEntityModel.Setting, CostControlBusinessModel.Setting>(MemberList.None).ReverseMap();
            CreateMap<CostControlEntityModel.IntakeRemittance, CostControlBusinessModel.IntakeRemittance>(MemberList.None).ReverseMap();
            CreateMap<CostControlEntityModel.DataImport, CostControlBusinessModel.DataImport>(MemberList.None).ReverseMap();
            CreateMap<CostControlEntityModel.IncommingUser, CostControlBusinessModel.IncommingUser>(MemberList.None).ReverseMap();
        }

        public static IQueryable<CostControlBusinessModel.SalePoint> ConvertToDTO(IQueryable<CostControlEntityModel.SalePoint> source)
        {
            return source.ProjectTo<CostControlBusinessModel.SalePoint>();
        }

        //public static Expression<Func<CostControlBusinessModel.SaleCenter, bool>> GetMappedSelector(Expression<Func<CostControlEntityModel.SaleCenter, bool>> selector)
        //{
        //    Expression<Func<CostControlBusinessModel.SaleCenter,
        //        CostControlEntityModel.SaleCenter>> mapper =
        //        Mapper.CreateMapExpression<CostControlBusinessModel.SaleCenter, CostControlEntityModel.SaleCenter>();
        //    Expression<Func<CostControlBusinessModel.SaleCenter, bool>> mappedSelector = selector.Compose(mapper);
        //    return mappedSelector;
        //}



        //private Expression<Func<TDestination, TProperty>> GetMappedSelector<TSource, TDestination, TProperty>(Expression<Func<TSource, TProperty>> selector)
        //{
        //    var map = Mapper.FindTypeMapFor<TSource, TDestination>();

        //    var mInfo = ReflectionHelper.GetMemberInfo(selector);

        //    if (mInfo == null)
        //    {
        //        throw new Exception(string.Format(
        //            "Can't get PropertyMap. \"{0}\" is not a member expression", selector));
        //    }

        //    PropertyMap propmap = map
        //        .GetPropertyMaps()
        //        .SingleOrDefault(m =>
        //            m.SourceMember != null &&
        //            m.SourceMember.MetadataToken == mInfo.MetadataToken);

        //    if (propmap == null)
        //    {
        //        throw new Exception(
        //            string.Format(
        //            "Can't map selector. Could not find a PropertyMap for {0}", selector.GetPropertyName()));
        //    }

        //    var param = Expression.Parameter(typeof(TDestination));
        //    var body = Expression.MakeMemberAccess(param, propmap.DestinationProperty.MemberInfo);
        //    var lambda = Expression.Lambda<Func<TDestination, TProperty>>(body, param);

        //    return lambda;
        //}

        //private static class ReflectionHelper
        //{
        //    public static MemberInfo GetMemberInfo(Expression memberExpression)
        //    {
        //        var memberExpr = memberExpression as MemberExpression;

        //        if (memberExpr == null && memberExpression is LambdaExpression)
        //        {
        //            memberExpr = (memberExpression as LambdaExpression).Body as MemberExpression;
        //        }

        //        return memberExpr != null ? memberExpr.Member : null;
        //    }
        //}


        //public IQueryable<CostControlBusinessModel.SaleCenter> Find(Expression<Func<CostControlEntityModel.SaleCenter, bool>> predicate)
        //{
        //    Expression<Func<CostControlBusinessModel.SaleCenter, CostControlEntityModel.SaleCenter>> mapper =
        //        Mapper.Engine.CreateMapExpression<CostControlBusinessModel.SaleCenter, CostControlEntityModel.SaleCenter>();

        //    Expression<Func<CostControlBusinessModel.SaleCenter, bool>> mappedSelector = predicate.Compose(mapper);

        //    return _context.Users.Where(mappedSelector);
        //}

        //public static IQueryable<T> ConvertToDTO<T, U>(IQueryable<U> source)
        //    where T : CostControlBusinessModel.SaleCenter
        //    //where U : IQueryable<CostControlEntityModel.SaleCenter
        //{
        //    return source.ProjectTo<T>();
        //}
    }
}