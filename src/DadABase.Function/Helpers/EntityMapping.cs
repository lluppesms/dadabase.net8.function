//namespace Dadabase.Function.Helpers;

//public static class EntityMapping
//{
//    public static IMapper? Mapper;

//    /// <summary>
//    /// Set up Auto Mapper --- need to do this in startup, not here...!
//    /// </summary>
//    public static void SetupAutoMapper()
//    {
//        if (Mapper == null)
//        {
//            var mapperConfig = new MapperConfiguration(cfg =>
//            {
//                cfg.CreateMap<Joke, JokeBasic>()
//                    .ForMember(dest => dest.Joke, opt => opt.MapFrom(src => src.JokeTxt))
//                    .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.JokeCategoryTxt));
//                //.ForMember(dest => dest.ImageText, opt => opt.MapFrom(src => src.ImageTxt));
//                cfg.CreateMap<Joke, JokeBasicPlus>()
//                    .ForMember(dest => dest.Joke, opt => opt.MapFrom(src => src.JokeTxt))
//                    .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.JokeCategoryTxt))
//                    .ForMember(dest => dest.Attribution, opt => opt.MapFrom(src => src.Attribution));
//                cfg.CreateMap<JokeCategory, CategoryBasic>()
//                    .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.JokeCategoryTxt));
//            });
//            mapperConfig.AssertConfigurationIsValid();
//            Mapper = mapperConfig.CreateMapper();
//        }
//    }
//}
