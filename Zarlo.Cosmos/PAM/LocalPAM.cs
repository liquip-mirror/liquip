using System;

namespace Zarlo.Cosmos.PAM;

public class LocalPAM : IPluggableAuthenticationModule
{
    public const string DefaultDbPath = "/etc/passwd";

    public LocalPAM(string? dbPath = null)
    {
        if (dbPath == null) dbPath = DefaultDbPath;
    }

    public void ChangePassword(uint userId, string? password)
    {
        throw new NotImplementedException();
    }

    public bool CheckPassword(uint userId, string? password)
    {
        throw new NotImplementedException();
    }

    public void MakeUser(uint userId, string username, uint[]? groups = null)
    {
        throw new NotImplementedException();
    }
}