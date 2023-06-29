namespace Zarlo.Cosmos.PAM;

public interface IPluggableAuthenticationModule
{
    public void ChangePassword(uint userId, string? password);
    public void MakeUser(uint userId, string username, uint[]? groups = null);

    public bool CheckPassword(uint userId, string? password);
}
