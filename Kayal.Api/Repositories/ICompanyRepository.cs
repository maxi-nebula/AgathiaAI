using Kayal.Api.Models;

namespace Kayal.Api.Repositories;

public interface ICompanyRepository
{
    Task<Company?> FindByNameAsync(string companyName);

    Task<Company> CreateAsync(Company company);
}