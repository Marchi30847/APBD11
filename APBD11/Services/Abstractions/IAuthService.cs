using APBD11.Contracts.Requests;
using APBD11.Contracts.Responses;

namespace APBD11.Services.Abstractions;

public interface IAuthService
{
    Task RegisterUserAsync(CreateUserDto request, CancellationToken token);
    Task<AuthResponseDto> LoginAsync(CreateUserDto request, CancellationToken token);
    Task<string> RefreshTokenAsync(string refreshToken, CancellationToken token);
}