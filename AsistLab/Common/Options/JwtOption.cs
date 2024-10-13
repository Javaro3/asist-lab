using System.Text;

namespace Common.Options;

public class JwtOption
{
    public string Key { get; set; } = string.Empty;

    public string Issuer { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;

    public string ExpireMinutes { get; set; } = string.Empty;

    public byte[] ByteKey => Encoding.UTF8.GetBytes(Key);
}