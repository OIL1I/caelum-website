using System.Text.Json;

namespace CaelumWebsite.Components.Models;

[Serializable]
public class Winchallenge
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreationDate { get; }
    public ChallengeState Status { get; set; }
    public List<Challenge> Challenges { get; set; }
    public List<Participant> Participants { get; set; }
    public string? ShareCode { get; }
    public bool IsShared { get; set; }

    public Winchallenge(int pId, string pName)
    {
        Id = pId;
        Name = pName;
        CreationDate = DateTime.Now;
        Status = ChallengeState.Running;
        Challenges = new List<Challenge>();
        Participants = new List<Participant>();
        ShareCode = Helpers.StringHelper.GetRandomString(8);
        IsShared = false;
    }

    public string ToJson()
    {
        return JsonSerializer.Serialize<Winchallenge>(this);
    }
}