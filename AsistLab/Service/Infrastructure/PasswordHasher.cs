namespace Service.Infrastructure;

public static class PasswordHasher
{
    public static bool Verify(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
    
    public static string GenerateHash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);;
    }
}