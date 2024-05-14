using Microsoft.EntityFrameworkCore;
using universityApiBackend.DataAccess;

namespace universityApiBackend.Repositories.Implements
{
    public class RepositoryBaseIMPL<T> : IRepositoryBase<T> where T : class

    {
        protected UniversityDBContext RepositoryContext {  get; set; }

        public RepositoryBaseIMPL(UniversityDBContext repositoryContext)
        {
            RepositoryContext = repositoryContext ?? throw new ArgumentNullException(nameof(repositoryContext));
        }
        public void Create(T entity)
        {
            RepositoryContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            RepositoryContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return RepositoryContext.Set<T>().AsNoTrackingWithIdentityResolution();
        }

        public IQueryable<T> FindAll(Func<IQueryable<T>, Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<T, object>> includes = null)
        {
            IQueryable<T> querytable = RepositoryContext.Set<T>();
            if (includes != null)
            {
                querytable = includes(querytable);
            }
            return querytable.AsNoTrackingWithIdentityResolution();
        }

        public IQueryable<T> FindByCondition(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return RepositoryContext.Set<T>().Where(expression).AsNoTrackingWithIdentityResolution(); 
        }

        public void SaveChanges()
        {
            RepositoryContext.SaveChanges();
        }

        public void UpdateClass(T entity)
        {
            RepositoryContext.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }
    }
}
