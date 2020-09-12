using System;
using System.Collections.Generic;
using BoardGame.Cards;

namespace BoardGame.Preparation
{

    public class PreparationLogic
    {
        int collectionRowsCount = 3;

        public List<Card> collection;
        public List<Card> playerSelection;

        int selectionTargetSize;
        public delegate void SelectionFullnessChanged(bool isFull);
        public SelectionFullnessChanged selectionFullnessChanged;
        bool selectionIsFull;

        public PreparationLogic(int _selectionTargetSize)
        {
            collection = new List<Card>();
            for (int i = 0; i < collectionRowsCount; i++)
            {
                collection.Add(CardBuilder.create(Card.Gnome));
                collection.Add(CardBuilder.create(Card.Goblin));
                collection.Add(CardBuilder.create(Card.Demon));
                collection.Add(CardBuilder.create(Card.Elf));
            }

            playerSelection = new List<Card>();

            selectionTargetSize = _selectionTargetSize;
            selectionIsFull = false;
        }

        private static void cardSwapLists(List<Card> from, List<Card> to, Guid cardGuid)
        {
            int index = from.FindIndex(card => card.guid == cardGuid);
            to.Add(from[index]);
            from.RemoveAt(index);
        }

        public void moveCardToPlayerSelection(Guid cardGuid)
        {
            if (selectionIsFull)
                return;
            cardSwapLists(collection, playerSelection, cardGuid);
            evaluateSelectionFullness();
        }

        public void evaluateSelectionFullness()
        {
            if (playerSelection.Count == selectionTargetSize)
            {
                selectionFullnessChanged(true);
                selectionIsFull = true;
            }
        }

        public void moveCardToCollection(Guid cardGuid)
        {
            cardSwapLists(playerSelection, collection, cardGuid);
            selectionIsFull = false;
        }

    }


}