using System.ComponentModel.DataAnnotations;

namespace Domain.Enums;

public enum VagonType
{
    [Display(Name = "I Class", Description = "I კლასი")]
    FirstClass = 0,

    [Display(Name = "II Class", Description = "II კლასი")]
    SecondClass = 1,

    [Display(Name = "Business Class", Description = "ბიზნეს კლასი")]
    BusinessClass = 2
}
