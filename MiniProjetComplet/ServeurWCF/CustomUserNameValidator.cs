using System;
using System.IdentityModel.Selectors;
using LibrairieServeur;

namespace ServeurWCF
{
    public class CustomUserNameValidator : UserNamePasswordValidator
    {
        GestionBDD gest = new GestionBDD(new DataBase());

        public override void Validate(String userName, String password)
        {
            if (userName == null || password == null)
            {
                throw new ArgumentNullException();
            }
            if (gest.getUser(userName, password) == null)
            {
                throw new Exception("Utilisateur ou Mot de passe incorrect");
            }
        }
    }
}