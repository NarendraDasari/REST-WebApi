using Api.DomainObjects;
using System;

namespace Api.Services
{
    public class TokenValidationService : ServiceBase, IToken
    {
        bool IToken.Validate(string token)
        {
            bool isValid = false;
            var dbContext = GetDatabaseInstance();
            var collection = dbContext.GetCollection<AppToken>("Tokens");
            AppToken tokenObj = collection.FindOne(x => x.Token == token);
            if (tokenObj != null && !string.IsNullOrEmpty(tokenObj.Token))
            {
                isValid = DateTime.Now <= tokenObj.ValidTill ;
                if (!isValid)
                {
                    collection.Delete(x => x.Token == token);
                }

            }
            return isValid;
         }

        bool IToken.SaveToken(string token)
        {
            var dbContext = GetDatabaseInstance();
            var collection = dbContext.GetCollection<AppToken>("Tokens");
            AppToken tokenObj = new AppToken { Token = token, ValidTill = DateTime.Now.AddMinutes(10) };
            collection.Insert(tokenObj);
            collection.EnsureIndex(x => x.Token);
            collection.EnsureIndex(x => x.ValidTill);
            return true;
        }

        void IToken.RenewToken(string token)
        {
            var dbContext = GetDatabaseInstance();
            var collection = dbContext.GetCollection<AppToken>("Tokens");
            AppToken tokenObj = collection.FindOne(x => x.Token == token);
            if (token != null)
            {
                tokenObj.ValidTill = DateTime.Now.AddMinutes(10);
                collection.Update(tokenObj);
            }

        }

    }
}
