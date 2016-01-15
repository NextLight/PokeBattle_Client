using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Web.Script.Serialization;

namespace PokeBattle_Client
{
    class Server
    {
        TcpClient client;
        NetworkStream stream;
        
        public Server(string ip)
        {
            this.Ip = ip;
            client = new TcpClient();
        }

        public string Ip { get; set; }

        public async Task Connect()
        {
            await client.ConnectAsync(IPAddress.Parse(this.Ip), 9073);
            stream = client.GetStream();
        }

        // quite ugly tbh
        public async Task<byte[]> ReadBytes(int bufferSize = 1024)
        {
            List<byte> res = new List<byte>();
            byte[] buffer = new byte[bufferSize];
            do
            {
                int len = await stream.ReadAsync(buffer, 0, bufferSize);
                res.AddRange(buffer.Take(len));
            } while (stream.DataAvailable);
            return res.ToArray();
        }

        public async Task<byte> ReadByte()
        {
            return (await ReadBytes(1))[0];
        }

        public async Task<string> ReadString()
        {
            return Encoding.ASCII.GetString(await ReadBytes());
        }

        public async Task<Pokemon[]> ReadPokeTeam()
        {
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new List<JavaScriptConverter>() { new JSTuple2Converter<int, int?>() });
            return serializer.Deserialize<Pokemon[]>(await ReadString());
        }
    }
}
