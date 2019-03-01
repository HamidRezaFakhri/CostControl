using AutoMapper;
using System;
using System.Reflection;
using CostControlBusinessModel = CostControl.BusinessEntity.Models.CostControl;
using CostControlEntityModel = CostControl.Entity.Models.CostControl;
using SecurityBusinessModel = CostControl.BusinessEntity.Models.Security;
using SecurityEntityModel = CostControl.Entity.Models.Security;

//MapExpressionAsInclude
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
        {
            return sourceMember.ToString("c");
        }
    }

    public class TypeTypeConverter : ITypeConverter<string, Type>
    {
        public Type Convert(string source, Type destination, ResolutionContext context)
        {
            return Assembly.GetExecutingAssembly().GetType(source);
        }
    }

    //public class MyPropertyResolver : IValueResolver<MyClass, MyClassDTO, string>
    //{
    //    private ISomeService _someService;

    //    public MyPropertyResolver(ISomeService someService)
    //    {
    //        _someService = someService;
    //    }

    //    public string Resolve(MyClass source, MyClassDTO destination, string destMember, ResolutionContext context)
    //    {
    //        return _someService.GetSomeProperty(source.Id);
    //    }
    //}

    //http://docs.automapper.org/en/stable/Configuration.html
    //https://visualstudiomagazine.com/Articles/2012/02/01/Simplify-Your-Projections-with-AutoMapper.aspx?Page=2
    //https://csharp.hotexamples.com/examples/-/AutoMapper/CreateMap/php-automapper-createmap-method-examples.html
    //https://www.fuget.org/packages/AutoMapper/6.2.0/lib/net40/AutoMapper.dll/AutoMapper/IMappingExpression%602
    //https://searchcode.com/codesearch/view/4344762/
    //https://code.i-harness.com/en/q/2b331be
    //https://social.technet.microsoft.com/wiki/contents/articles/51043.automapper-handling-profile-dependencies-using-custom-value-resolvers.aspx
    public class ClientMappingProfile : Profile
    {
        //public override string ProfileName => "ClientMappings";
        public override string ProfileName => GetType().Name;

        public ClientMappingProfile()
        {
            //SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
            //DestinationMemberNamingConvention = new PascalCaseNamingConvention();
        }

        //public static IQueryable<CostControlBusinessModel.SalePoint> ConvertToDTO(IQueryable<CostControlEntityModel.SalePoint> source)
        //{
        //    return source.ProjectTo<CostControlBusinessModel.SalePoint>();
        //}

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