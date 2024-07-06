namespace CaelumWebsite.Components.Models;

[Serializable]
public class Participant
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Participant(int pId, string pName)
    {
        Id = pId;
        Name = pName;
    }
}