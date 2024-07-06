namespace ChallengeViewer.Models;

[Serializable]
public class Challenge
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsBackToBack { get; set; }
    public int RequiredCompletions { get; set; }
    // public bool IsDone { get; set; }
    public int Completions { get; set; }
    public bool IsDone => Completions == RequiredCompletions;
    public List<Participant> DoneBy { get; set; }
}