using System;
using System.IdentityModel.Selectors;
namespace WCFSecurite
{
    public class CustomUserNameValidator : UserNamePasswordValidator
    {
        public override void Validate(String userName, String password)
        {
            if (userName == null || password == null)
            {
                throw new ArgumentNullException();
            }
            if (!XXXXXX.verifieUtilisateur(userName, password))
            {
                throw new Exception("Utilisateur ou Mot de passe incorrect");
            }
        }
    }
}