using System;

namespace PokeBattle_Client
{
    class Pokemon
    {
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

        public override string ToString()
        {
            return $"{Name} : lvl. {Level}\nHp: {Hp}\nAttack: {Attack}\nDefense: {Defense}\nSpecialAttack: {SpecialAttack}\nSpecialDefense: {SpecialDefense}\nSpeed: {Speed}";
        }
    }
}
