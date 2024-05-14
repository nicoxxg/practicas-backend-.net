using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using universityApiBackend.DataAccess;
using universityApiBackend.Models.DataModels;

namespace universityApiBackend.Repositories.Implements
{
    public class UserRepositoryIMPL : RepositoryBaseIMPL<User>, IUserRepository
    {

        public UserRepositoryIMPL(UniversityDBContext repositoryContext): base(repositoryContext) {  }
        public User FindByEmail(string email)
        {
            return FindByCondition(user => user.Email.ToLower() == email.ToLower())
                .FirstOrDefault();
        }

        public User FindById(int id)
        {
            return FindByCondition(client => client.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return FindAll().ToList();
        }

        public void Save(User user)
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            Create(user);
            SaveChanges();
        }

        public new void Update(User user)
        {
            base.UpdateClass(user);
        }
    }
}
