using System.Net.Sockets;
using System.Text;

namespace client;

public class TcpClientCon
{
    public TcpClient client;
    public NetworkStream stream;
    public byte[] data = new byte[1024];
    public void Start(string ip, int port)
    {
        TcpClient client = new TcpClient(ip, port);
        stream = client.GetStream();
    }

    public void Stop()
    {
        client.Close();
        stream.Close();
    }

    public void Read()
    {
        string responseData = string.Empty;
        int bytes = stream.Read(data, 0, data.Length);
        responseData = Encoding.ASCII.GetString(data, 0, bytes);
        Console.WriteLine("Received: {0}", responseData);

    }

    public void Write(string msg)
    {
        data = Encoding.ASCII.GetBytes(msg + "<|EOM|>");
        stream.Write(data, 0, data.Length);
    }
}