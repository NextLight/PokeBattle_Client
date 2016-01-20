using System;

namespace PokeBattle_Client
{
    class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public Tuple<int, int?> Types { get; set; }
        public Move[] Moves { get; set; }
        public int Hp { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }
        public int Nature { get; set; }
    }
}
