using System;
using System.Collections.Generic;
using BoardGame.Cards;

namespace BoardGame.Preparation
{

    public class PreparationLogic
    {
        public List<Card> collection;
        public List<Card> playerSelection;

        public PreparationLogic()
        {
            collection = new List<Card>();
            for (int i = 0; i < 2; i++)
            {
                collection.Add(CardBuilder.create(Card.Gnome));
                collection.Add(CardBuilder.create(Card.Goblin));
                collection.Add(CardBuilder.create(Card.Demon));
                collection.Add(CardBuilder.create(Card.Elf));
            }

            playerSelection = new List<Card>();

            playerSelection.Add(CardBuilder.create(Card.Demon));  // just for test
            playerSelection.Add(CardBuilder.create(Card.Elf));  // just for test
        }

        private static void cardSwapLists(List<Card> from, List<Card> to, Guid cardGuid)
        {
            int index = from.FindIndex(card => card.guid == cardGuid);
            to.Add(from[index]);
            from.RemoveAt(index);
        }

        public void moveCardToPlayerSelection(Guid cardGuid)
        {
            cardSwapLists(collection, playerSelection, cardGuid);
        }

        public void moveCardToCollection(Guid cardGuid)
        {
            cardSwapLists(playerSelection, collection, cardGuid);
        }

    }


}