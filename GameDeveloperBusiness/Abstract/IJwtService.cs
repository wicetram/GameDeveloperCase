namespace GameDeveloperBusiness.Abstract
{
    public interface IJwtService<T>
    {
        string GenerateToken(T payload);
        bool ValidateToken(string token, out T payload);
    }
}
