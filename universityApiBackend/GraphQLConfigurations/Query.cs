using universityApiBackend.DataAccess;
using universityApiBackend.Models.DataModels;

namespace universityApiBackend.GraphQLConfigurations
{
    public class Query<T> where T : class
    {

        //[UseSelection]
        //[UseFiltering]
        //[UseSorting]
        //public IQueryable<T> GetItems([Service] UniversityDBContext dbContext)  =>
        //dbContext.Set<T>().AsQueryable();

    }
}
