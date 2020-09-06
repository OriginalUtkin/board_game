namespace BoardGame.Cards {

    public class Card
    {
        private string name;
        private int health;
        private int attack;

        public Card(string name, int health, int attack) {
            this.name = name;
            this.health = health;
            this.attack = attack;
        }

        public Card(Card previous) {
            this.name = previous.Name;
            this.health = previous.Health;
            this.attack = previous.Attack;
        }

        public string Name{
            get { return this.name; }
            set { this.name = value; }
        }
        public int Health{
            get { return this.health; }
            set { this.health = value; }
        }

        public int Attack{
            get { return this.attack; }
            set { this.attack = value; }
        }

    }

}