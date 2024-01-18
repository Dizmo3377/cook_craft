using AutoMapper;
using Cook_Craft.Models;
using Cook_Craft.View_Models;

namespace Cook_Craft.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Recipe, EditRecipeViewModel>()
            .ForMember(dest => dest.URL, opt => opt.MapFrom(src => src.Image))
            .ForMember(dest => dest.Image, opt => opt.Ignore())
            .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => src.Steps.Select(step => step.Description).ToList()))
            .ForMember(dest => dest.Ingridients, opt => opt.MapFrom(src => src.Ingridients.Select(ingredient => ingredient.Name).ToList()));

        CreateMap<EditRecipeViewModel, Recipe>()
            .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => MapSteps(src.Steps)))
            .ForMember(dest => dest.Ingridients, opt => opt.MapFrom(src => MapIngridients(src.Ingridients)));

        CreateMap<CreateRecipeViewModel, Recipe>()
            .ForMember(dest => dest.Steps, opt => opt.MapFrom(src => MapSteps(src.Steps)))
            .ForMember(dest => dest.Ingridients, opt => opt.MapFrom(src => MapIngridients(src.Ingridients)));
    }

    private List<Step> MapSteps(List<string> steps)
    {
        return steps?.Select(step => new Step { Description = step }).ToList() ?? new List<Step>();
    }

    private List<Ingridient> MapIngridients(List<string> ingridients)
    {
        return ingridients?.Select(ingredient => new Ingridient { Name = ingredient }).ToList() ?? new List<Ingridient>();
    }
}