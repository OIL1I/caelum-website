namespace CaelumWebsite.Components.Models;

[Serializable]
public class Challenge
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsBackToBack { get; set; }
    public int RequiredCompletions { get; set; }
    public int Completions { get; set; }
    public bool IsDone => Completions == RequiredCompletions;
    public List<Participant> DoneBy { get; set; }

    public Challenge(int pId, string pName, bool pIsBackToBack = false, int pRequiredCompletions = 1)
    {
        Id = pId;
        Name = pName;
        IsBackToBack = pIsBackToBack;
        RequiredCompletions = pRequiredCompletions;
        Completions = 0;
        DoneBy = new List<Participant>();
    }
}