namespace CostControl.BusinessLogic.Profiles.CostControl
{
    using AutoMapper;

    public class SalePointProfile : Profile
    {
        public override string ProfileName => GetType().Name;

        public SalePointProfile()
        {
            CreateMap<Entity.Models.CostControl.SalePoint, BusinessEntity.Models.CostControl.SalePoint>(MemberList.None)
                //.ForMember(dest => dest.Users, opt => opt.Ignore())
                //.ForMember(dest => dest.Users, opt => opt.MapFrom(src => src.Users))
                .ReverseMap();

            //CreateMap<Entity.Models.Base.SupperNameEntity<long>, BusinessEntity.Models.CostControl.SalePoint>(MemberList.None)
            //    .ForMember(o => o.Name, opt => opt.MapFrom(src => src.Name));

            //CreateMap<Entity.Models.Base.SupperNameEntity<long>, BusinessEntity.Models.CostControl.SalePoint>(MemberList.None)
            //        .AfterMap((s, d) =>
            //        {
            //            s.Name = d.Name;
            //        });

            //Mapper.CreateMap<DomainClass, Child>();
            //Mapper.CreateMap<DomainClass, Parent>()
            //      .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
            //      .ForMember(d => d.A, opt => opt.MapFrom(s => s.A))
            //      .ForMember(d => d.Child,
            //                 opt => opt.MapFrom(s => Mapper.Map<DomainClass, Child>(s)));

            //.ForMember(d => d.children,
            //    o => o.MapFrom(s =>
            //        from child in s.children
            //        select new ChildDestination
            //        {
            //            childId = child.childId,
            //            parentId = s.parentId,
            //            parent = s
            //        }));

            //public class CustomTypeConverter : AutoMapper.TypeConverter<ParentSource, ParentDestination>
            //        {
            //            protected override ParentDestination ConvertCore(ParentSource source)
            //            {
            //                var result = new ParentDestination() { parentId = source.parentId };
            //                result.children = source.children.Select(c => new ChildDestination() { childId = c.childId, parentId = source.parentId, parent = result }).ToList();
            //                return result;
            //            }
            //        }

            //https://docs.automapper.org/en/stable/Mapping-inheritance.html
            //https://docs.automapper.org/en/stable/Nested-mappings.html
            //https://www.c-sharpcorner.com/article/some-useful-tips-while-using-automapper-in-C-Sharp/

            //Mapper.CreateMap<Child, ChildViewModel>().ForMember(
            //d => d.Nicknames, o => o.ResolveUsing<ListToStringConverter>().FromMember(s => s.Nicknames);

            //Mapper.CreateMap<ParentDto, Parent>()
            //  .ForMember(m => m.Children, o => o.Ignore()) // To avoid automapping attempt
            //  .AfterMap((p, o) => { o.Children = ToISet<ChildDto, Child>(p.Children); });

        }
    }
}
