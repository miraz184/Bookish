using UserService.Exceptions;
using UserService.Models;
using UserService.Repository;

namespace UserService.Service
{
    public class UserService : IUserService
    {
        #region Variable
        IUserRepository _userRepository;
        #endregion

        #region Constructor Injection
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        #endregion

        #region DeleteUser
        public bool DeleteUser(string userId)
        {
            if (_userRepository.DeleteUser(userId))
            {
                return true;
            }
            else
            {
                throw new UserNotFoundException("This user id does not exist");
            }
        }
        #endregion

        #region GetUserById
        public User GetUserById(string userId)
        {
            var getUserId = _userRepository.GetUserById(userId);
            if (getUserId != null)
            {
                return getUserId;
            }
            else
            {
                throw new UserNotFoundException("This user id does not exist");
            }
        }
        #endregion

        #region RegisterUser
        public User RegisterUser(User user)
        {
            var getUserId = _userRepository.GetUserById(user.UserName);
            if (getUserId == null)
            {
                return _userRepository.RegisterUser(user);
            }
            else
            {
                throw new UserNotCreatedException("This user id already exists");
            }
        }
        #endregion

        #region UpdateUser
        public bool UpdateUser(string userId, User user)
        {
            if (_userRepository.UpdateUser(userId, user))
            {
                return true;
            }
            else
            {
                throw new UserNotFoundException("This user id does not exist");
            }
        }
        #endregion
    }
}
