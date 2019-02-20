using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DataAccess;
using ModelClasses.Entities;

namespace Services
{
    public interface IUserService
    {
        bool IsLoginDataCorrect(string name, string password);
        IEnumerable<User> AllAdmins();
    }

    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public bool IsLoginDataCorrect(string name, string password) 
            => null != _userRepository.Get(user => user.Username == name && user.Password == password);

        public IEnumerable<User> AllAdmins()
        {
           return _userRepository.GetAll();
        }
    }
}
