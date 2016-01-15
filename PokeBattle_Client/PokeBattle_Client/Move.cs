using System;

namespace PokeBattle_Client
{
    enum DamageClass { None = 1, Physical, Special };
    class Move
    {
        public string Name { get; set; }
        public int? Power { get; set; }
        public int? Accuracy { get; set; }
        public int? Pp { get; set; }
        public int TypeId { get; set; }
        public DamageClass DamageClass { get; set; }
    }
}