using System;
using System.Collections.Generic;

namespace SolitaireCipher
{
    class Deck
    {
        #region Constants

        private const int JokerA = 53;
        private const int JokerB = 54;
        private const int DeckIndexSize = 53;

        #endregion

        #region Collections

        private List<int> CurrentDeck { get; set; }

        #endregion

        #region Classes

        private Helper helper = new Helper();

        #endregion

        #region Public Methods

        /// <summary>
        /// Constructor
        /// </summary>
        public Deck()
        {
            KeyTheDeck();
        }

        /// <summary>
        /// Generate the keystream for the string list
        /// </summary>
        /// <param name="message">The string list to get the keystream for</param>
        /// <returns>The keystream list</returns>
        public List<string> GenerateKeystream(List<string> message)
        {
            List<string> tempList = new List<string>();
            string returnString = String.Empty;

            // Loop through each string in the list.
            foreach (string str in message)
            {
                string newString = String.Empty;

                // Loop through each letter in the string.
                foreach (char c in str)
                {
                    string foundLetter = String.Empty;

                    // We need to move the cards until we find a valid letter.
                    while (String.IsNullOrEmpty(foundLetter))
                    {
                        MoveCards();
                        foundLetter = FindTheOutputLetter(CurrentDeck);
                    }

                    // Add the letter to the new string.
                    newString = String.Format("{0}{1}", newString, foundLetter);
                }

                // Add the new string to the list that we will be returning.
                tempList.Add(newString);
            }

            return tempList;            
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Perform the card movements
        /// </summary>
        private void MoveCards()
        {
            CurrentDeck = MoveDown(CurrentDeck, JokerA, 1);
            CurrentDeck = MoveDown(CurrentDeck, JokerB, 2);
            CurrentDeck = TripleCut(CurrentDeck);
            CurrentDeck = CountCut(CurrentDeck);
        }

        /// <summary>
        /// We will just be keying the deck with the cards in order.
        /// The two jokers will be at the bottom of the deck.
        /// </summary>
        private void KeyTheDeck()
        {
            List<int> deck = new List<int>();

            // The numbers 1-52 will represent the normal cards.
            // The numbers 53 and 54 will be the two jokers.
            for (int card = 1; card <= 54; card++)
            {
                deck.Add(card);
            }

            CurrentDeck = deck;
        }

        /// <summary>
        /// Move the jokers down in the deck.
        /// </summary>
        /// <param name="deck">The deck of cards that we are working with</param>
        /// <param name="cardToMove">The Joker card that we are moving</param>
        /// <param name="placesToMove">The number of places to move the card down</param>
        /// <returns>The updated deck with the card moved</returns>
        private List<int> MoveDown(List<int> deck, int cardToMove, int placesToMove)
        {
            int cardIndex = deck.IndexOf(cardToMove);
            int newCardIndex = 0;
            List<int> newDeck = new List<int>();

            newDeck.AddRange(deck);

            if (placesToMove == 1)
            {
                // If the joker is the last card in the deck, move it below the top card in the deck.
                // Otherwise, just move it down one position.
                if (cardIndex == DeckIndexSize)
                {
                    newCardIndex = 1;
                }
                else
                {
                    newCardIndex = cardIndex + 1;
                }
            }
            else
            {
                // The places to move is two.
                // If the joker is the last card in the deck, move it below the second card in the deck.
                // If the joker is the second to last card in the deck, move it below the top card in the deck.
                // Otherwise, move it down two positions.
                if (cardIndex == DeckIndexSize)
                {
                    newCardIndex = 2;
                }
                else if (cardIndex == DeckIndexSize - 1)
                {
                    newCardIndex = 1;
                }
                else
                {
                    newCardIndex = cardIndex + 2;
                }
            }

            // Remove the card that we are moving and insert it back into the correct spot in the deck.
            newDeck.Remove(cardToMove);
            newDeck.Insert(newCardIndex, cardToMove);

            return newDeck;
        }

        /// <summary>
        /// Move the cards above the top joker to the bottom of the deck and move the cards
        /// below the bottom joker to the top of the deck.  The middle of the deck, including the jokers
        /// remains in place.
        /// </summary>
        /// <param name="deck">The deck of cards that we are working with</param>
        /// <returns>The updated deck after the cut</returns>
        private List<int> TripleCut(List<int> deck)
        {
            List<int> newDeck = new List<int>();
            List<int> TopOfDeck = new List<int>();
            List<int> MiddleOfDeck = new List<int>();
            List<int> BottomOfDeck = new List<int>();

            // Get the index positions of the jokers.
            int indexA = deck.IndexOf(JokerA);
            int indexB = deck.IndexOf(JokerB);

            int lowIndex = 0;
            int highIndex = 0;

            // Determine the indexes of JokerA and JokerB in order to determine the cut.
            if (indexA < indexB)
            {
                lowIndex = indexA;
                highIndex = indexB;
            }
            else
            {
                lowIndex = indexB;
                highIndex = indexA;
            }

            // Start by cutting off the top of the deck from top to just above the joker with the low index.
            // Pull the cards into a new list.
            for (int x = 0; x < lowIndex; x++)
            {
                TopOfDeck.Add(deck[x]);
            }

            // The middle of the deck will include both jokers, so pull out the cards starting from the low
            // index to the high index.
            for (int x = lowIndex; x <= highIndex; x++)
            {
                MiddleOfDeck.Add(deck[x]);
            }

            // The bottom of the deck will be the cards starting after the joker with the high index.
            // Pull the cards into a new list.
            for (int x = (highIndex + 1); x <= DeckIndexSize; x++)
            {
                BottomOfDeck.Add(deck[x]);
            }

            // Complete the cut by creating a new deck with the bottom pile on top, followed by the middle pile,
            // and finally the bottom pile.
            newDeck.AddRange(BottomOfDeck);
            newDeck.AddRange(MiddleOfDeck);
            newDeck.AddRange(TopOfDeck);

            return newDeck;
        }

        /// <summary>
        /// Get the value of the last card in the deck.  Pull that many cards from the top of the deck
        /// and put them just above the last card in the deck.
        /// </summary>
        /// <param name="deck">The deck of cards that we are working with</param>
        /// <returns>The updated deck after the cut</returns>
        private List<int> CountCut(List<int> deck)
        {
            List<int> TopOfDeck = new List<int>();
            List<int> newDeck = new List<int>();

            // Get the value of the last card in the deck.
            int lastCardValue = deck[DeckIndexSize];

            newDeck.AddRange(deck);

            // If the last card in the deck is a joker, the deck will remain unchanged.
            if ((lastCardValue == JokerA) || (lastCardValue == JokerB))
            {
                return newDeck;
            }

            // Pull off the number of cards from the top of the deck that equals the value
            // of the last card in the deck.
            for (int x = 0; x < lastCardValue; x++)
            {
                TopOfDeck.Add(deck[x]);
            }

            // Remove the last card from the deck.
            newDeck.Remove(lastCardValue);

            // Remove the number of cards from the top of the deck that equals the value of 
            // the bottom card.
            newDeck.RemoveRange(0, lastCardValue);

            // Add the cards from the top of the deck to the bottom of the deck.
            newDeck.AddRange(TopOfDeck);

            // Put the bottom card back on the bottom of the deck.
            newDeck.Add(lastCardValue);

            return newDeck;
        }

        /// <summary>
        /// Look at the value of the top card, and count down that many cards in the deck.
        /// The value of the card is what will be used to determine the letter that is chosen.
        /// </summary>
        /// <param name="deck">The deck of cards that we are working with</param>
        /// <returns>The letter from the chosen card</returns>
        private string FindTheOutputLetter(List<int> deck)
        {
            int topCardValue = deck[0];

            // If the top card is JokerB, subtract one from the value.
            if (topCardValue == JokerB)
            {
                topCardValue -= 1;
            }

            // Get the value of the card at the top of the deck.
            int letterValue = deck[topCardValue];

            // If the card we count down to is a joker, skip.
            if (letterValue >= JokerA)
            {
                return String.Empty;
            }

            // If the letter value is greater than 26, subtract 26.
            if (letterValue > 26)
            {
                letterValue -= 26;
            }

            // If the letter value is less than 1, add 26.
            if (letterValue < 1)
            {
                letterValue += 26;
            }

            // Get the letter from the value.
            return helper.GetTheLetter(letterValue);         
        }

        #endregion
    }
}
