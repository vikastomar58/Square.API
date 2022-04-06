using Squares.API.DataLayer.Entities;

namespace Squares.API.Domain.Manager
{
    public interface ITokenGeneration
    {
        string GenerateToken(UserDetail user);
    }
}
