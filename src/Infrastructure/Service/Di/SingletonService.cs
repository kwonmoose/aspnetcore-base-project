namespace Infrastructure.Service.Di;

public class SingletonService
{
    private Guid guid { get; set; }

    public SingletonService()
    {
        guid = Guid.NewGuid();
    }

    public string GetGuid()
    {
        return guid.ToString();
    }
}