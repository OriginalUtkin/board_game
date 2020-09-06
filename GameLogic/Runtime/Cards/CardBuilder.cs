using System.Collections.Generic;


namespace BoardGame.Cards {

    public class CardBuilder
    {
        static Dictionary<string, Card> cardTemplates;

        static CardBuilder() {
            cardTemplates = new Dictionary<string, Card>();
            cardTemplates.Add("gnome", new Card("gnome", 1, 5));
            cardTemplates.Add("goblin", new Card("goblin", 2, 4));
            cardTemplates.Add("demon", new Card("demon", 3, 3));
            cardTemplates.Add("elf", new Card("elf", 4, 2));
        }

        public Card create(string name) {
            return new Card(cardTemplates[name]);
        }
    }


}