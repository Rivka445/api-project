using AutoMapper;
using Entities;
using DTOs;
using Repositories;
namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserPasswordService _userPasswordService;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository,IMapper mapper,IUserPasswordService userPasswordService)
        {
            _userRepository = userRepository;
            _userPasswordService = userPasswordService;
            _mapper = mapper;
        }
        public async Task<List<UserDTO>> GetUsers()
        {
            List<User> users = await _userRepository.GetUsers();
            List<UserDTO> usersDTO = _mapper.Map<List<User>, List<UserDTO>>(users);
            return usersDTO;
        }
        public async Task<UserDTO> GetUserById(int id)
        {
            User? user= await _userRepository.GetUserById(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found.");

            UserDTO userDTO = _mapper.Map<User, UserDTO>(user);
            return userDTO;
        }
        public async Task<UserDTO> AddUser(UserRegisterDTO newUser)
        {
            if (newUser == null)
                throw new ArgumentNullException(nameof(newUser));
            if (_userPasswordService.CheckPassword(newUser.Password) <= 2)
                throw new ArgumentException("Password is too weak.");

            User userRegister = _mapper.Map<UserRegisterDTO, User>(newUser);
            User user = await _userRepository.AddUser(userRegister);
            UserDTO userDTO = _mapper.Map<User, UserDTO>(user);
            return userDTO;
        }
        public async Task<UserDTO> LogIn(UserLoginDTO existUser)
        {
            if (existUser == null)
                throw new ArgumentNullException(nameof(existUser));

            User loginUser= _mapper.Map<UserLoginDTO,User>(existUser);
            User? user = await _userRepository.LogIn(loginUser);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid username or password.");

            UserDTO userDTO = _mapper.Map<User, UserDTO>(user);
            return userDTO;
        }
        public async Task UpdateUser(int id, UserDTO updateUser)
        {
            if (updateUser == null)
                throw new ArgumentNullException(nameof(updateUser));
            if (_userPasswordService.CheckPassword(updateUser.Password) <= 2)
                throw new ArgumentException("Password is too weak.");

            User user = _mapper.Map<UserDTO,User>(updateUser);
            await _userRepository.UpdateUser(user);
        }
    }
}
