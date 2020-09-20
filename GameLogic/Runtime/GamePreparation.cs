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
                collection.Add(CardBuilder.Create(Card.Gnome));
                collection.Add(CardBuilder.Create(Card.Goblin));
                collection.Add(CardBuilder.Create(Card.Demon));
                collection.Add(CardBuilder.Create(Card.Elf));
            }

            playerSelection = new List<Card>();

            selectionTargetSize = _selectionTargetSize;
            selectionIsFull = false;
        }

        private static void CardSwapLists(List<Card> from, List<Card> to, Guid cardGuid)
        {
            int index = from.FindIndex(card => card.guid == cardGuid);
            to.Add(from[index]);
            from.RemoveAt(index);
        }

        public void MoveCardToPlayerSelection(Guid cardGuid)
        {
            if (selectionIsFull)
                return;
            CardSwapLists(collection, playerSelection, cardGuid);
            EvaluateSelectionFullness();
        }

        public void EvaluateSelectionFullness()
        {
            if (playerSelection.Count == selectionTargetSize)
            {
                selectionFullnessChanged(true);
                selectionIsFull = true;
            }
        }

        public void MoveCardToCollection(Guid cardGuid)
        {
            CardSwapLists(playerSelection, collection, cardGuid);
            selectionIsFull = false;
        }

    }


}