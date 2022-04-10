using System.Configuration;

public static class DBConn
{
    private static string m_MMORPG_Account;

    public static string MMORPG_Account
    {
        get
        {
            if (string.IsNullOrEmpty(m_MMORPG_Account))
            {
                m_MMORPG_Account = ConfigurationManager.ConnectionStrings["MMORPG_AccountServerDB"].ConnectionString;
            }
            return m_MMORPG_Account;
        }
    }
}