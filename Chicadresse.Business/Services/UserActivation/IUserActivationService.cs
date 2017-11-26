using System.Collections.Generic;
using Chicadresse.Entities.Domain;

namespace Chicadresse.Business.Services
{
    public interface IUserActivationService
    {
        void AddUser(User_Activation obj);

        User_Activation GetUser(string activationCode);
    }
}
