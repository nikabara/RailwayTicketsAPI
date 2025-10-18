//using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class EmailTemplate
{
    public int EmailTemplateId { get; set; }

    // NC : tamplate_{desired template name}{#variation if exists}
    //[RegularExpression(@"^template_[a-zA-Z0-9_-]+(#([a-zA-Z0-9_-]+))?$",
    //    ErrorMessage = "Template name must start with 'template_', " +
    //    "followed by the template name, and optionally a variation " +
    //    "prefixed by '#' (e.g., template_welcome#v1).")]
    public string TemplateName { get; set; } = string.Empty; 
    public string EmailTemplateHTML { get; set; } = string.Empty;
}
