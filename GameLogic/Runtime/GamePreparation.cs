using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using BoardGame.Cards;

namespace BoardGame.Preparation
{
    public class PreparationLogic
    {
        int collectionRowsCount = 3;

        public List<Card> collection = new List<Card>();
        public List<Card> playerSelection = new List<Card>();

        int selectionTargetSize;
        public delegate void SelectionFullnessChanged(bool isFull);
        [XmlIgnore]
        public SelectionFullnessChanged selectionFullnessChanged;
        bool selectionIsFull;

        public PreparationLogic() { }

        public PreparationLogic(int _selectionTargetSize)
        {
            for (int i = 0; i < collectionRowsCount; i++)
            {
                collection.Add(CardBuilder.Create(Card.Gnome));
                collection.Add(CardBuilder.Create(Card.Goblin));
                collection.Add(CardBuilder.Create(Card.Demon));
                collection.Add(CardBuilder.Create(Card.Elf));
            }

            selectionTargetSize = _selectionTargetSize;
            selectionIsFull = false;
        }

        public void SaveToFile(string path)
        {
            XmlSerializer mySerializer = new XmlSerializer(typeof(PreparationLogic));
            StreamWriter myWriter = new StreamWriter(path);
            mySerializer.Serialize(myWriter, this);
            myWriter.Close();
        }
        public static PreparationLogic LoadFromFile(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(PreparationLogic));

            using (Stream reader = new FileStream(path, FileMode.Open))
            {
                return (PreparationLogic)serializer.Deserialize(reader);
            }
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