using System.Net.Sockets;
using System.Text;

namespace client;

public class TcpClientCon
{
    private TcpClient client;
    private NetworkStream stream;


    public void Start(string ip, int port)
    { 
        client = new TcpClient(ip, port);
        stream = client.GetStream();
    }

    public void Stop()
    {
        client.Close();
        stream.Close();
    }

    public void Read()
    {
        byte[] data = new byte[1024];
        string responseData = string.Empty;
        int bytes = stream.Read(data, 0, data.Length);
        responseData = Encoding.ASCII.GetString(data, 0, bytes);
        Console.WriteLine("Received: {0}", responseData);
    }

    public void Write(string msg)
    {
        byte[] data = new byte[1024];
        data = Encoding.ASCII.GetBytes(msg + "<|EOM|>");
        stream.Write(data, 0, data.Length);
    }
}