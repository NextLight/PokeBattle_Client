using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace PokeBattle_Client
{
    class Server
    {
        TcpClient _client;
        NetworkStream _stream;
        StreamReader _streamReader;
        JavaScriptSerializer _serializer;

        public Server(string ip)
        {
            Ip = ip;
            _client = new TcpClient();
        }

        public string Ip { get; }

        public async Task Connect()
        {
            await _client.ConnectAsync(IPAddress.Parse(Ip).MapToIPv4(), 9073);
            _stream = _client.GetStream();
            _streamReader = new StreamReader(_stream);
            _serializer = new JavaScriptSerializer();
            _serializer.RegisterConverters(new List<JavaScriptConverter>() { new JSTuple2Converter<int, int?>() });
        }

        public Task<byte> ReadByte() =>
            Task.Run(() => (byte)_streamReader.Read());

        public async Task<MessageType> ReadType() =>
            (MessageType)await ReadByte();

        private Task<string> ReadLine() => 
            _streamReader.ReadLineAsync();

        public async Task<string> ReadText() =>
            Encoding.UTF8.GetString(Convert.FromBase64String(await ReadLine()));

        public async Task<Pokemon> ReadPokemon() => 
            _serializer.Deserialize<Pokemon>(await ReadLine());

        public async Task<Pokemon[]> ReadPokeTeam() => 
            _serializer.Deserialize<Pokemon[]>(await ReadLine());

        public async Task<InBattleClass> ReadInBattle() => 
            _serializer.Deserialize<InBattleClass>(await ReadLine());

        // Each turn the player can either use a move (0), change (1) or don't change (2) the active pokemon

        public Task SendMove(byte moveIdx) =>
            _stream.WriteAsync(new byte[] { 0, moveIdx }, 0, 2);

        public Task SendSwitchPokemon(byte pokemonIdx) =>
            _stream.WriteAsync(new byte[] { 1, pokemonIdx }, 0, 2);

        public Task SendDontSwitchPokemon() =>
            _stream.WriteAsync(new byte[] { 2, 0 }, 0, 2);
    }

    enum MessageType : byte { AcivePokemon, Text, ChangeOpponent, PokeTeam, InBattleUser, InBattleOpponent, BeginTurn, UserFainted, OpponentFainted }
}
