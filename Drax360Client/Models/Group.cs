using Drax360Client.Panels.Email;

public class Group
{
    public Group()
    {
        Addresses = new List<Address>();
    }

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Address> Addresses { get; set; }
    // other properties...

}