using AutoMapper;
using System;
using System.Collections.Generic;
using Bono.Orders.Application.Interfaces;
using Bono.Orders.Application.ViewModels;
using Bono.Orders.Infrastructure.Auth.Services;
using Bono.Orders.Domain.Entities;
using Bono.Orders.Domain.Interfaces.Repository;
using System.ComponentModel.DataAnnotations;
using ValidationContext = System.ComponentModel.DataAnnotations.ValidationContext;
using ValidationResult = Bono.Orders.Domain.Validations.ValidationResult;
using Bono.Orders.Infrastructure.Utils;

namespace Bono.Orders.Application.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly ValidationResult validationResult;
        private readonly Security security;

        public UserService(IUserRepository userRepository, IMapper mapper, ValidationResult validationResult, Security security)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.validationResult = validationResult;
            this.security = security;
        }        

        public IEnumerable<UserViewModel> GetAll()
        {
            IEnumerable<User> _users = this.userRepository.GetAll();

            List<UserViewModel> _userViewModels = mapper.Map<List<UserViewModel>>(_users);

            return _userViewModels;
        }

        public ValidationResult Post(UserViewModel userViewModel)
        {
            if (userViewModel.Id != Guid.Empty)
            {
                validationResult.Add("UserID must be empty");
                return validationResult;
            }
            
            if (string.IsNullOrEmpty(userViewModel.Cpf) || string.IsNullOrEmpty(userViewModel.Email) || string.IsNullOrEmpty(userViewModel.FirstName) || string.IsNullOrEmpty(userViewModel.LastName) || string.IsNullOrEmpty(userViewModel.ConfirmPassword) || string.IsNullOrEmpty(userViewModel.Password) || string.IsNullOrEmpty(userViewModel.UserName))
            {
                validationResult.Add("The sent object was empty.");
                return validationResult;
            }


            Validator.ValidateObject(userViewModel, new ValidationContext(userViewModel), true);

            User user = new(userViewModel.UserName, userViewModel.Password, userViewModel.FirstName, userViewModel.LastName, userViewModel.Cpf, userViewModel.Email, userViewModel.PhoneNumber);
            user.SetPassword(security.EncryptPassword(userViewModel.Password));

            validationResult.Data = this.userRepository.Create(user);

            //if (validationResult.Entity == null)
            //{
            //    validationResult.Add("The Entity you are trying to record is null, please try again!");
            //    return validationResult;
            //}
            
            return validationResult;
            
        }

        public UserViewModel GetById(string id)
        {
            if (!Guid.TryParse(id, out Guid userId))
                throw new Exception("UserID is not valid");

            User _user = this.userRepository.Find(x => x.Id == userId && !x.IsDeleted);
            if (_user == null)
                throw new Exception("User not found");

            return mapper.Map<UserViewModel>(_user);
        }

        public ValidationResult Put(UserViewModel userViewModel)
        {
            if (userViewModel.Id == Guid.Empty)
            {
                validationResult.Add("ID is invalid");
                return validationResult;
            }
            

            User user = this.userRepository.Find(x => x.Id == userViewModel.Id && !x.IsDeleted);
            if (user == null)
            {
                validationResult.Add("User not found");
                return validationResult;
            }
            
            
            user = mapper.Map<User>(userViewModel);
            user.SetPassword(security.EncryptPassword(user.Password));

            try
            {
                this.userRepository.Update(user);
            }
            catch (Exception ex)
            {
                validationResult.Add(ex.Message);
            }

            return validationResult;
        }

        public ValidationResult Delete(string id)
        {
            if (!Guid.TryParse(id, out Guid userId))
            {
                validationResult.Add("UserID is not valid");
                return validationResult;
            }

            User _user = this.userRepository.Find(x => x.Id == userId && !x.IsDeleted);
            
            if (_user == null)
                throw new Exception("User not found");

            try
            {
                this.userRepository.Delete(_user);
            }
            catch (Exception ex)
            {
                validationResult.Add(ex.Message);
            }
           
            return validationResult;

            
        }

        public ValidationResult Authenticate(UserAuthenticateRequestViewModel user)
        {
            
            if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                validationResult.Add("Email/Password are required.");
                return validationResult;
            }
                

            user.Password = security.EncryptPassword(user.Password);

            User _user = this.userRepository.Find(x => !x.IsDeleted && x.Email.ToLower() == user.Email.ToLower()
                                                    && x.Password.ToLower() == user.Password.ToLower());
            if (_user == null)
            {
                validationResult.Add("User not found");
                return validationResult;
            }
            

            if (validationResult.IsValid)
                validationResult.Data = new UserAuthenticateResponseViewModel(mapper.Map<UserViewModel>(_user), TokenService.GenerateToken(_user));


            return validationResult;
        }

        
    }
}
