using Application.Abstractions;
using Domain.Entities;
using Infrastructure.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EmailTemplateRepository : IEmailTemplateRepository
{
    #region Properties
    public ApplicationDbContext _applicationDbContext { get; set; }
    #endregion

    #region Constructors
    public EmailTemplateRepository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    #endregion

    #region Methods
    public async Task<EmailTemplate?> GetEmailTemplate(Expression<Func<EmailTemplate, bool>> expression)
    {
        var result = new EmailTemplate();

        result = await _applicationDbContext.EmailTemplates
            .Where(expression)
            .FirstOrDefaultAsync();

        return result;
    }
    #endregion
}
