using BookShop.Application.Dto;

namespace BookShop.Application.Interfaces
{
    public interface ITokenManager
    {
        TokenDto GenerateToken(string email, string username);
    }
}
