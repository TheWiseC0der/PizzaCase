namespace client;

public interface IClient
{
    public void Start(); //TODO: change to bool
    public void Stop(); //TODO: change to bool
    public string Read();
    public void Write(string message); //TODO: change to bool
}