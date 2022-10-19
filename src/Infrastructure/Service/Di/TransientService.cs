namespace Infrastructure.Service.Di;

public class TransientService
{
    private Guid guid { get; set; }

    public TransientService()
    {
        guid = Guid.NewGuid();
    }

    public string GetGuid()
    {
        return guid.ToString();
    }
}