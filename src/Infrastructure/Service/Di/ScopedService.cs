namespace Infrastructure.Service.Di;

public class ScopedService
{
    private Guid guid { get; set; }

    public ScopedService()
    {
        guid = Guid.NewGuid();
    }

    public string GetGuid()
    {
        return guid.ToString();
    }
}