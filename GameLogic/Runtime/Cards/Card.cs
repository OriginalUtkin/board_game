using System;

namespace BoardGame.Cards
{

    public class Card
    {
        public static readonly string Elf = "elf";
        public static readonly string Demon = "demon";
        public static readonly string Goblin = "goblin";
        public static readonly string Gnome = "gnome";


        private string name;
        private int health;
        private int attack;
        private Guid _guid;

        public Card()
        {
            _guid = Guid.NewGuid();
        }

        public Card(string name, int health, int attack)
        {
            this.name = name;
            this.health = health;
            this.attack = attack;
            _guid = Guid.NewGuid();
        }

        public Card(Card previous)
        {
            this.name = previous.Name;
            this.health = previous.Health;
            this.attack = previous.Attack;
            _guid = Guid.NewGuid();
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public int Health
        {
            get { return this.health; }
            set { this.health = value; }
        }

        public int Attack
        {
            get { return this.attack; }
            set { this.attack = value; }
        }

        public Guid guid
        {
            get { return this._guid; }
        }

    }

}