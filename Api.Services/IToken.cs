namespace Api.Services
{
    public interface IToken
    {
        bool Validate(string token);

        bool SaveToken(string token);
        void RenewToken(string token);

    }
}
