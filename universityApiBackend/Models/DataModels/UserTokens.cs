using universityApiBackend.Repositories;

namespace universityApiBackend.Models.DataModels
{
    public class UserTokens
    {

        public IUserRepository _userRepository;

        public UserTokens(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int id { get; set; }
        public string Token { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public TimeSpan Validity { get; set; }
        public string RefreshToken { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public Guid GuidId { get; set; }
        public DateTime ExpiredTime { get; set; }
        public Rol rol { get; set; } = Rol.User;

        public void SetRol(string email)
        {
            rol = GetRol(email);
        }
        public Rol GetRol(string email)
        {
           return _userRepository.FindByEmail(email).rol;
        }
    }
}
