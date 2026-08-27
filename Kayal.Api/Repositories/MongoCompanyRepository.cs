using Kayal.Api.Models;
using MongoDB.Driver;

namespace Kayal.Api.Repositories;

public class MongoCompanyRepository : ICompanyRepository
{
    private readonly IMongoCollection<Company> _companies;

    public MongoCompanyRepository(IMongoDatabase database)
    {
        _companies = database.GetCollection<Company>("Companies");
    }

    public async Task<Company?> FindByNameAsync(string companyName)
    {
            return await _companies
             .Find(company => company.Name == companyName)
              .FirstOrDefaultAsync();
        throw new NotImplementedException();
    }

   public async Task<Company> CreateAsync(Company company)
{
    await _companies.InsertOneAsync(company);

    return company;
}
}