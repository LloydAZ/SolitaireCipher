using System;
using System.Collections.Generic;
using System.Linq;

namespace SolitaireCipher
{
    class Cipher
    {
        #region classes

        private Helper helper = new Helper();

        #endregion

        #region Constructor

        public Cipher()
        {
            // Does nothing
        }

        #endregion

        /// <summary>
        /// Encrypt the message using the Solitaire Cipher
        /// </summary>
        /// <param name="message">The message to encrypt</param>
        /// <returns>The encrypted message</returns>
        public string Encrypt(string message)
        {
            string encrypted = String.Empty;

            Deck deck = new Deck();
            ConvertList convert = new ConvertList();

            // Convert the message into a list of five letter blocks.
            List<string> converted = convert.ConvertMessageToList(message);

            // Generate the keystream for the encryption.
            List<string> keystream = deck.GenerateKeystream(converted);

            // Convert the message list and the keystream list into lists of arrays of integers.
            List<int[]> originalNumList = convert.ConvertToNumbers(converted);
            List<int[]> keystreamNumList = convert.ConvertToNumbers(keystream);

            // Add the two values in each of the lists together to get the alphabetic
            // cipher value for the messaage.
            encrypted = helper.ConvertNumberListToString(AddValues(originalNumList, keystreamNumList));

            return encrypted;
        }

        /// <summary>
        /// Decrypt the message using the Solitaire Cipher
        /// </summary>
        /// <param name="message">The encrypted message</param>
        /// <returns>The decrypted message</returns>
        public string Decrypt(string message)
        {
            string decrypted = String.Empty;

            Deck deck = new Deck();
            ConvertList convert = new ConvertList();

            // Convert the message into a list of five letter blocks.
            List<string> converted = convert.ConvertMessageToList(message);

            // Generate the keystream for the decryption.
            List<string> keystream = deck.GenerateKeystream(converted);

            // Convert the message list and the keystream list into lists of arrays of integers.
            List<int[]> originalNumList = convert.ConvertToNumbers(converted);
            List<int[]> keystreamNumList = convert.ConvertToNumbers(keystream);

            // Subtract the two values in each of the lists together to get the alphabetic
            // value for the messaage.
            decrypted = helper.ConvertNumberListToString(SubtractValues(originalNumList, keystreamNumList));

            return decrypted;
        }

        /// <summary>
        /// Add the values of the two lists together and return a new list
        /// </summary>
        /// <param name="original">The original message list of arrays of integers</param>
        /// <param name="keystream">The keystream list of arrays of integers</param>
        /// <returns>New list of array of integers</returns>
        private List<int[]> AddValues(List<int[]> original, List<int[]> keystream)
        {
            List<int[]> intArrayList = new List<int[]>();

            List<int> tempOrig;
            List<int> tempKeystream;
            List<int> tempTotals;

            // Loop through each array in the collection.
            for (int x = 0; x < original.Count; x++)
            {
                tempOrig = new List<int>(original[x].ToList());
                tempKeystream = new List<int>(keystream[x].ToList());
                tempTotals = new List<int>();

                // Loop five times and add the values from each array together.
                for (int y = 0; y < 5; y++)
                {
                    int tempVal = tempOrig[y] + tempKeystream[y];

                    // If the value of the addition is greater than 26, subtract 26.
                    if (tempVal > 26)
                    {
                        tempVal -= 26;
                    }

                    // Add the calculated value to a list.
                    tempTotals.Add(tempVal);
                }

                // Convert the tempTotals list into an array and add it to list that will be returned.
                intArrayList.Add(tempTotals.ToArray());
            }

            return intArrayList;
        }

        /// <summary>
        /// Subtract the values of the two lists and return a new list
        /// </summary>
        /// <param name="original">The original message list of arrays of integers</param>
        /// <param name="keystream">The keystream list of arrays of integers</param>
        /// <returns>New list of array of integers</returns>
        private List<int[]> SubtractValues(List<int[]> original, List<int[]> keystream)
        {
            List<int[]> intArrayList = new List<int[]>();

            List<int> tempOrig;
            List<int> tempKeystream;
            List<int> tempTotals;

            // Loop through each array in the collection.
            for (int x = 0; x < original.Count; x++)
            {
                tempOrig = new List<int>(original[x].ToList());
                tempKeystream = new List<int>(keystream[x].ToList());
                tempTotals = new List<int>();

                // Loop five times and subtract the value of the keystream from the original.
                for (int y = 0; y < 5; y++)
                {
                    int tempVal = tempOrig[y] - tempKeystream[y];

                    // If the value of the subtraction is less than 1, add 26.
                    if (tempVal < 1)
                    {
                        tempVal += 26;
                    }

                    // Add the calculated value to a list.
                    tempTotals.Add(tempVal);
                }

                // Convert the tempTotals list into an array and add it to list that will be returned.
                intArrayList.Add(tempTotals.ToArray());
            }

            return intArrayList;
        }
    }
}
