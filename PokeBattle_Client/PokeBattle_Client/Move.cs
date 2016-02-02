﻿using System;

namespace PokeBattle_Client
{
    public enum DamageClass { None = 1, Physical, Special };
    public class Move
    {
        public string Name { get; set; }
        public int? Power { get; set; }
        public int? Accuracy { get; set; }
        public int? Pp { get; set; }
        public int TypeId { get; set; }
        public DamageClass DamageClass { get; set; }
    }
}