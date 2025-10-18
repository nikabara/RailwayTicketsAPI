using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Abstractions;

public interface IEmailTemplateRepository
{
    public Task<EmailTemplate> GetEmailTemplate(Expression<Func<EmailTemplate, bool>> expression);
}
