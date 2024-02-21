using EntityLayer.Entities;
public interface IAuthService
{
    Task<string> Authenticate(User user);
}