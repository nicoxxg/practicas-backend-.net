using universityApiBackend.Models.DataModels;

namespace universityApiBackend.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        void Save(User user);
        User FindById(int id);
        User FindByEmail(string email);
        void Update(User user);
    }
}
