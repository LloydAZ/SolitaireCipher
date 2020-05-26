using System;
using System.Collections.Generic;

namespace SolitaireCipher
{
    class Helper
    {
        #region Collections

        private Dictionary<int, string> Alphabet = new Dictionary<int, string>();

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public Helper()
        {
            BuildTheDictionary();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Return the letter from the alphabet based on its position.
        /// </summary>
        /// <param name="letterPosition">Position of the letter in the alphabet</param>
        /// <returns>The letter based upon its position</returns>
        public string GetTheLetter(int letterPosition)
        {
            return Alphabet[letterPosition];
        }

        /// <summary>
        /// Convert the list of arrays of integers to a string.
        /// </summary>
        /// <param name="message">The list of arrays of integers</param>
        /// <returns>The converted message</returns>
        public string ConvertNumberListToString(List<int[]> message)
        {
            string converted = String.Empty;

            // Loop through the list of arrays.
            for (int x = 0; x < message.Count; x++)
            {
                int[] tempArray = message[x];
                string tempString = String.Empty;

                // Loop 5 times and convert the number value from the array to a letter.
                for (int y = 0; y < 5; y++)
                {
                    // Build the string for this group of five letters.
                    tempString = String.Format("{0}{1}", tempString, this.GetTheLetter(tempArray[y]));    
                }

                // Append the tempString value to the string that we are returning.
                converted = String.Format("{0} {1}", converted, tempString);
            }

            // Trim off any blanks from the ends and return the converted string.
            return converted.Trim();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// We'll be creating a dictionary by pulling the letters from the ASCII table
        /// and indexing them 1 through 26 appropriately.
        /// </summary>
        private void BuildTheDictionary()
        {
            // 'A' starts at value 65 of the ASCII table.  'Z' is at value 90.
            for (byte value = 65; value <= 90; value++)
            {
                // Subtract 64 from the byte value to get the correct index for the dictionary.
                int index = value - 64;

                // Convert the ASCII value to a string and store it in the dictionary.
                string letter = Char.ConvertFromUtf32(value).ToString();
                Alphabet.Add(index, letter);
            }
        }

        #endregion
    }
}
