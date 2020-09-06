using System.Collections.Generic;
using BoardGame.Cards;

namespace BoardGame.Preparation {

    public class PreparationLogic
    {
        public List<Card> collection;
        public List<Card> playerSelection;

        public PreparationLogic() {
            collection = new List<Card>();
            for (int i = 0; i < 2; i++) {
                collection.Add(CardBuilder.create("gnome"));
                collection.Add(CardBuilder.create("goblin"));
                collection.Add(CardBuilder.create("demon"));
                collection.Add(CardBuilder.create("elf"));
            }

            playerSelection = new List<Card>();
        }
    }


}