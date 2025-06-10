namespace DarkMode.Core.Utils;

public static class EnvHelper
{
    public static bool IsDebugMode
    {
        get
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}