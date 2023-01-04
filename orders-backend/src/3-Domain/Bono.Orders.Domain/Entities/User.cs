using System;
using System.Collections.Generic;
using System.Text;
using Bono.Orders.Domain.Models;

namespace Bono.Orders.Domain.Entities
{
    public class User: Entity
    {

        #region Parameters
        //public Guid UniqueIdentifierInCompany { get; private set; }
        public string Cpf { get; private set; }
        public string Email { get; private set; }
        public bool EmailConfirmed { get; private set; }
        public string Password { get; private set; }
        public string SecurityStamp { get; private set; }
        public string PhoneNumber { get; private set; }
        public bool PhoneNumberConfirmed { get; private set; }
        public bool TwoFactorEnabled { get; private set; }
        public DateTime? LockoutEndDateUtc { get; private set; }
        public bool LockoutEnabled { get; private set; }
        public int AccessFailedCount { get; private set; }
        public string UserName { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Discriminator { get; private set; }
        public IEnumerable<UserRole> Roles { get; private set; }

        #endregion

        #region Constructors
        protected User() { }

        public User(string UserName, string Password, string FirstName, string LastName, string Cpf, string Email, string PhoneNumber)
        {
            this.Id = Guid.NewGuid();
            this.UserName = UserName;
            this.Password = Password;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Cpf = Cpf;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
        }

        #endregion

        #region Methods

        public void SetPassword(string Password)
        {
            this.Password = Password;
        }       

        #endregion

    }
}
