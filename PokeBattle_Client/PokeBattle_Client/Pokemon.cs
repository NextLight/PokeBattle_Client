using System;
using System.Windows.Media.Imaging;
using System.ComponentModel;

namespace PokeBattle_Client
{
    public class Pokemon : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public Tuple<int, int?> Types { get; set; }
        public Move[] Moves { get; set; }
        public int Nature { get; set; }

        InBattleClass _inBattle;
        public InBattleClass InBattle
        {
            get { return _inBattle; }
            set
            {
                _inBattle = value;
                OnPropertyChanged("InBattle");
            }
        }

        public BitmapImage Image
        {
            get 
            {
                try
                {
                    return new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "\\sprites\\" + Id + ".png", UriKind.Absolute));
                }
                catch
                {
                    return null;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

    public class InBattleClass
    {
        public StatsClass Stats { get; set; }
        // TODO: status modifiers
    }

    public class StatsClass
    {
        public int Hp { get; set; }

        public int MaxHp { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefense { get; set; }
        public int Speed { get; set; }
        public int AccuracyStage { get; set; }
        public int EvasionStage { get; set; }
    }
}
