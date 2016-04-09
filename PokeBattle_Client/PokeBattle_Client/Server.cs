using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Web.Script.Serialization;
using System.IO;

namespace PokeBattle_Client
{
    class Server
    {
        TcpClient client;
        NetworkStream stream;
        StreamReader streamReader;
        JavaScriptSerializer serializer;

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
            streamReader = new StreamReader(stream);
            serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new List<JavaScriptConverter>() { new JSTuple2Converter<int, int?>() });
        }

        public async Task<MessageType> ReadType()
        {
            return (MessageType)await Task.Run(() => streamReader.Read());
        }

        public async Task<string> ReadLine()
        {
            return await streamReader.ReadLineAsync();
        }

        public async Task<Pokemon> ReadPokemon()
        {
            return serializer.Deserialize<Pokemon>(await ReadLine());
        }

        public async Task<Pokemon[]> ReadPokeTeam()
        {
            return serializer.Deserialize<Pokemon[]>(await ReadLine());
        }

        public async Task<InBattleClass> ReadInBattle()
        {
            return serializer.Deserialize<InBattleClass>(await ReadLine());
        }

        // Each turn the player can either use a move (0) or change the active pokemon (1)

        public async Task SendMove(byte moveIdx)
        {
            await stream.WriteAsync(new byte[] { 0, moveIdx }, 0, 2);
        }

        public async Task SendSwitchPokemon(byte pokemonIdx)
        {
            await stream.WriteAsync(new byte[] { 1, pokemonIdx }, 0, 2);
        }
    }

    enum MessageType : byte { Text, ChangeOpponent, PokeTeam, InBattleUser, InBattleOpponent, BeginTurn, UserFainted }
}
