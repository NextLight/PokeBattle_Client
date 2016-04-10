using System.Windows;

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
        }

        private async void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            gridConnection.Visibility = Visibility.Hidden;
            server = new Server(txtIP.Text);
            await server.Connect();
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
                        break;
                    case MessageType.InBattleOpponent:
                        opponent.InBattle = await server.ReadInBattle();
                        break;
                    case MessageType.InBattleUser:
                        ActivePokemon.InBattle = await server.ReadInBattle();
                        break;
                    case MessageType.PokeTeam: // should happen only once
                        pokeTeam = await server.ReadPokeTeam();
                        pokePicker.PokeTeam = pokeTeam;
                        ActiveIndex = 0;
                        break;
                    case MessageType.Text:
                        textBlock.Text = await server.ReadLine();
                        break;
                    case MessageType.UserFainted:
                        pokePicker.Show(activeIndex);
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
            pokePicker.Show(activeIndex);
        }
    }
}
