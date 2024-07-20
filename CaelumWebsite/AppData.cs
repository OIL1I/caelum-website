using CaelumWebsite.Components.Models;
using CaelumWebsite.Components.Pages;

namespace CaelumWebsite;

public static class AppData
{
    private static List<Components.Models.Winchallenge>? _winchallenges;
    
    public static List<Components.Models.Winchallenge> GetWinchallenges()
    {
        if (_winchallenges == null) _winchallenges = new List<Components.Models.Winchallenge>();
        return _winchallenges;
    }

    public static bool IsLoggedIn { get; set; } = false;
    public static bool LastLoginFailed { get; set; } = false;
}