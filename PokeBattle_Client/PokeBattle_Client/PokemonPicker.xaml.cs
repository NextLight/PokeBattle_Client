﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PokeBattle_Client
{
    public partial class PokemonPicker : ListBox
    {
        public PokemonPicker()
        {
            InitializeComponent();
        }

        Pokemon[] pokeTeam;
        public Pokemon[] PokeTeam
        {
            get { return pokeTeam; }
            set
            {
                pokeTeam = value;
                foreach (Pokemon p in pokeTeam)
                    Items.Add(p);
            }
        }

        public void Show(int activePokemonIndex)
        {
            Visibility = Visibility.Visible;
            if (pokeTeam != null && pokeTeam.Length > activePokemonIndex)
            {
                Items.Insert(0, PokeTeam[activePokemonIndex]);
                Items.Remove(PokeTeam[activePokemonIndex]);
            }
        }

        public event EventHandler<PickedPokemonArgs> Picked;
        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {   
            if (Picked != null)
            {
                if (SelectedIndex != -1)
                    Picked(this, new PickedPokemonArgs(pokeTeam[SelectedIndex], SelectedIndex));
                Visibility = Visibility.Hidden;
            }
        }
    }

    public class PickedPokemonArgs : EventArgs
    {
        public Pokemon PickedPokemon { get; private set; }
        public int PickedIndex { get; private set; }
        
        public PickedPokemonArgs(Pokemon poke, int idx)
        {
            PickedPokemon = poke;
            PickedIndex = idx;
        }
    }
}