using System;
using System.Globalization;
using System.Linq;
using AutoMapper;
using ModelClasses.Entities.Testing;
using ModelClasses.Entities.TestParts;
using QuizApp.ViewModel.Managing;
using QuizApp.ViewModel.PassingQuiz;

namespace QuizApp.ViewModel.Mapping
{
    public class AutoMapperConfiguration
    {
        public static IMapper MapperInstance =>
            new MapperConfiguration(cfg =>
            {
                //Map Model to ViewModel
                cfg.CreateMap<TestAnswer, AnswerViewModel>();

                cfg.CreateMap<TestQuestion, QuestionViewModel>()
                    .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.TestAnswers));
                
                cfg.CreateMap<Test, TestViewModel>()
                    .ForMember(dest => dest.TestTimeLimit, opt => opt.MapFrom(src => 
                                    src.TestTimeLimit == null ? "00:00:00" : src.TestTimeLimit.Value.ToString()))
                    .ForMember(dest => dest.QuestionTimeLimit, opt => opt.MapFrom(src => 
                                    src.QuestionTimeLimit == null ? "00:00:00" : src.QuestionTimeLimit.Value.ToString()))
                    .ForMember(dest => dest.Questions, opt => opt.MapFrom(src => src.TestQuestions));

                cfg.CreateMap<TestingUrl, TestingUrlViewModel>()
                    .ForMember(dest => dest.TestGuid, opts => opts.MapFrom(src => src.Test.Guid))
                    .ForMember(dest => dest.TestName, opts => opts.MapFrom(src => src.Test.Name))
                    .ForMember(dest => dest.AllowedStartDate, opts => opts.MapFrom(src => src.AllowedStartDate.Value.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture)))
                    .ForMember(dest => dest.AllowedEndDate, opts => opts.MapFrom(src => src.AllowedEndDate.Value.ToString("MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture)))
                    .ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

                cfg.CreateMap<TestAnswer, AnswerPassingViewModel>();

                cfg.CreateMap<TestQuestion, QuestionPassingViewModel>()
                    .ForMember(dest => dest.Answers, opt => opt.MapFrom(src => src.TestAnswers))
                    .ForMember(dest => dest.Multiple, opt => opt.MapFrom(src => src.TestAnswers.Count(a => a.IsCorrect) > 1));


                cfg.CreateMap<TestingResult, TestingUrViewModel>()
                    .ForMember(dest => dest.TestName, opt => opt.MapFrom(src => src.Test.Name))
                    .ForMember(dest => dest.TestGuid, opt => opt.MapFrom(src => src.Test.Guid))
                    .ForMember(dest => dest.TotalQuestions,
                        opt => opt.MapFrom(src => src.Test.TestQuestions.Count))
                    .ForMember(dest => dest.TestingStartDateTime,
                        opts => opts.MapFrom(src => src.TestingStartDateTime.ToString("MM/dd/yyyy HH:mm:ss")))
                    .ForMember(dest => dest.TestingEndDateTime,
                        opts => opts.MapFrom(src => src.TestingEndDateTime.ToString("MM/dd/yyyy HH:mm:ss")))
                    .ForMember(dest => dest.Duration,
                        opts => opts.MapFrom(src => src.Duration.ToString()));




                //Map ViewModel to Model
                cfg.CreateMap<AnswerViewModel, TestAnswer>()
                    .ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

                cfg.CreateMap<QuestionViewModel, TestQuestion>()
                    .ForMember(dest => dest.TestAnswers, opt => opt.MapFrom(src => src.Answers))
                    .ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));
                

                cfg.CreateMap<TestViewModel, Test>()
                    .ForMember(dest => dest.TestQuestions, opt => opt.MapFrom(src => src.Questions))
                    .ForMember(dest => dest.QuestionTimeLimit, opt => opt.MapFrom(src => TimeSpan.Parse(src.QuestionTimeLimit)))
                    .ForMember(dest => dest.TestTimeLimit, opt => opt.MapFrom(src => TimeSpan.Parse(src.TestTimeLimit)))
                    .ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));
                

                cfg.CreateMap<TestingUrlViewModel, TestingUrl>()
                  .ForMember(dest => dest.AllowedStartDate, opt => opt.MapFrom(src => DateTime.Parse(src.AllowedStartDate)))
                  .ForMember(dest => dest.AllowedEndDate, opt => opt.MapFrom(src => DateTime.Parse(src.AllowedEndDate)))
                  .ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

                cfg.CreateMap<TestPassingViewModel, TestingResult>()
                  .ForMember(dest => dest.TestingStartDateTime, opt => opt.MapFrom(src => DateTime.Parse(src.TestingStartDateTime)))
                  .ForMember(dest => dest.TestingEndDateTime, opt => opt.MapFrom(src => DateTime.Parse(src.TestingEndDateTime)))
                  //.ForMember(dest => dest.Duration, opt => opt.MapFrom(src => TimeSpan.Parse(src.Duration)))
                  .ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));

            }).CreateMapper();
    }
}
