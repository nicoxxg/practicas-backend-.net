using universityApiBackend.DataAccess;

namespace universityApiBackend.GraphQLConfigurations
{
    public class Mutation<T> where T : class
    {
        //public async Task<T> CreateItem([Service] UniversityDBContext dbContext, T item)
        //{
        //    dbContext.Set<T>().Add(item);
        //    await dbContext.SaveChangesAsync();
        //    return item;
        //}

        //public async Task<T> UpdateItem([Service] UniversityDBContext dbContext, int id, T item)
        //{
        //    var existingItem = await dbContext.Set<T>().FindAsync(id);
        //    if (existingItem == null)
        //        return default;

        //    dbContext.Entry(existingItem).CurrentValues.SetValues(item);
        //    await dbContext.SaveChangesAsync();
        //    return existingItem;
        //}

        //public async Task<bool> DeleteItem([Service] UniversityDBContext dbContext, int id)
        //{
        //    var item = await dbContext.Set<T>().FindAsync(id);
        //    if (item == null)
        //        return false;

        //    dbContext.Set<T>().Remove(item);
        //    await dbContext.SaveChangesAsync();
        //    return true;
        //}
    }
}
