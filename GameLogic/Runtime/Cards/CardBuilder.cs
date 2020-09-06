using System.Collections.Generic;


namespace BoardGame.Cards {

    public class CardBuilder
    {
        static Dictionary<string, Card> cardTemplates;

        static CardBuilder() {
            cardTemplates = new Dictionary<string, Card>();
            cardTemplates.Add(Card.Gnome, new Card(Card.Gnome, 1, 5));  // TODO load this from yaml config
            cardTemplates.Add(Card.Goblin, new Card(Card.Goblin, 2, 4));
            cardTemplates.Add(Card.Demon, new Card(Card.Demon, 3, 3));
            cardTemplates.Add(Card.Elf, new Card(Card.Elf, 4, 2));
        }

        public static Card create(string name) {
            return new Card(cardTemplates[name]);
        }
    }


}