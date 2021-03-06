﻿using System.Windows;

namespace PokeBattle_Client
{
    public partial class MainWindow : Window
    {
        Server server;
        Pokemon[] pokeTeam;
        int activeIndex;
        int ActiveIndex
        {
            get { return activeIndex; }
            set
            {
                if (value < pokeTeam.Length)
                {
                    activeIndex = value;
                    pokev1.DataContext = ActivePokemon;
                    movePick.DataContext = ActivePokemon.Moves;
                }
            }
        }
        Pokemon ActivePokemon => pokeTeam[activeIndex];

        Pokemon opponent;

        public MainWindow()
        {
            InitializeComponent();
            gridMain.Children.Add(PokemonView.InfoPopup);
        }

        private async void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            server = new Server(txtIP.Text);
            await server.Connect();
            gridConnection.Visibility = Visibility.Hidden;
            MainLoop();
        }

        private async void MainLoop()
        {
            while (true)
            {
                switch (await server.ReadType())
                {
                    case MessageType.BeginTurn:
                        gridFooter.IsEnabled = true;
                        break;
                    case MessageType.ChangeOpponent:
                        opponent = await server.ReadPokemon();
                        pokev2.DataContext = opponent;
                        textBlock.Text += $"The opponent sent {opponent.Name}.\n";
                        break;
                    case MessageType.InBattleOpponent:
                        opponent.InBattle = await server.ReadInBattle();
                        break;
                    case MessageType.InBattleUser:
                        ActivePokemon.InBattle = await server.ReadInBattle();
                        break;
                    case MessageType.OpponentFainted:
                        textBlock.Text += opponent.Name + " opponent fainted.\n";
                        popAskChange.Visibility = Visibility.Visible;
                        break;
                    case MessageType.PokeTeam: // should happen only once
                        pokeTeam = await server.ReadPokeTeam();
                        pokePicker.PokeTeam = pokeTeam;
                        ActiveIndex = 0;
                        break;
                    case MessageType.Text:
                        textBlock.Text += await server.ReadText();
                        break;
                    case MessageType.UserFainted:
                        textBlock.Text += ActivePokemon.Name + " fainted. Choose another pokemon.\n";
                        pokePicker.Show(activeIndex, false);
                        break;
                }
            }
        }

        private async void movePick_MoveSelected(object sender, MoveSelectedArgs e)
        {
            gridFooter.IsEnabled = false;
            await server.SendMove((byte)e.SelectedIndex);
        }

        private async void pokePicker_Picked(object sender, PickedPokemonArgs e)
        {
            gridFooter.IsEnabled = false;
            ActiveIndex = e.PickedIndex;
            await server.SendSwitchPokemon((byte)e.PickedIndex);
        }

        private void btnSwitch_Click(object sender, RoutedEventArgs e)
        {
            pokePicker.Show(activeIndex, true);
        }

        private void btnSwitchY_Click(object sender, RoutedEventArgs e)
        {
            popAskChange.Visibility = Visibility.Hidden;
            pokePicker.Show(activeIndex, false);
        }

        private async void btnSwitchN_Click(object sender, RoutedEventArgs e)
        {
            popAskChange.Visibility = Visibility.Hidden;
            await server.SendDontSwitchPokemon();
        }

        private async void popAskChange_Closed(object sender, System.EventArgs e)
        {
            await server.SendDontSwitchPokemon();
        }
    }
}
