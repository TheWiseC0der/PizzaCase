using System;
using System.Net.Sockets;
using System.Text;

namespace client;

public class TcpClientCon : IClient
{
    private TcpClient client;
    private NetworkStream stream;
    private readonly string _ip;
    private readonly int _port;

    public TcpClientCon(string? ip, int port)
    {
        _ip = ip ?? throw new ArgumentNullException(nameof(ip));
        _port = port;
    }

    public TcpClientCon() : this("localhost", 8080)
    {
    }

    public void Start()
    {
        client = new TcpClient(_ip, _port);
        stream = client.GetStream();
    }

    public void Stop()
    {
        client.Close();
        stream.Close();
    }

    public string Read()
    {
        byte[] data = new byte[1024];
        int bytes = stream.Read(data, 0, data.Length);
        var responseData = Encoding.ASCII.GetString(data, 0, bytes);
        if (responseData.Contains("<|EOM|>"))
        {
            responseData = responseData.Replace("<|EOM|>", "");
            responseData = Cryptography.DecryptBytesToString_Aes(responseData);
            Console.WriteLine("received: {0}", responseData);
            return responseData;
        }
        throw new Exception("Error, something went wrong");
    }

    public void Write(string msg)
    {
        var data = Encoding.ASCII.GetBytes(Cryptography.EncryptStringToBytes_Aes(msg) + "<|EOM|>");
        stream.Write(data, 0, data.Length);
    }
}