using System.Text.Json;

namespace ChallengeViewer.Models;

[Serializable]
public class Winchallenge
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreationDate { get; set; }
    public ChallengeState Status { get; set; }
    public Challenge[] Challenges { get; set; }
    public Participant[] Participants { get; set; }
    public string ShareCode { get; set; }
    public bool IsShared { get; set; }
}