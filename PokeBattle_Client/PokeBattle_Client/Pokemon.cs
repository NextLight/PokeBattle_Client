using System;
using System.Windows.Media.Imaging;
using System.ComponentModel;

namespace PokeBattle_Client
{
    class Pokemon : INotifyPropertyChanged
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

        InBattleClass inBattle;
        public InBattleClass InBattle
        {
            get { return inBattle; }
            set
            {
                inBattle = value;
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
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }

    public class InBattleClass
    {
        public int Hp { get; set; }
        // TODO: status modifiers
    }
}
