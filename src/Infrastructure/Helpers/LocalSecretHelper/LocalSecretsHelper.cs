using Defender.Common.Enums;
using Defender.Common.Helpers;

namespace Defender.NotificationService.Infrastructure.Helpers.LocalSecretHelper;

public class LocalSecretsHelper
{
    public static string GetSecret(Secret secret)
    {
        return SecretsHelper.GetSecret(secret);
    }

    public static string GetSecret(LocalSecret secret)
    {
        return SecretsHelper.GetSecret(secret.ToString());
    }
}
