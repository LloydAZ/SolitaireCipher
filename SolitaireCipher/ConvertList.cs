using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SolitaireCipher
{
    class ConvertList
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ConvertList()
        {
            // Does nothing
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Takes the message and converts it to a list of five letter groups.
        /// </summary>
        /// <param name="message">The message to convert</param>
        /// <returns>The message converted to a list</returns>
        public List<string> ConvertMessageToList(string message)
        {
            List<string> messageList = new List<string>();
            string convertedMessage = String.Empty;

            // Remove any punctuation and spaces and convert to uppercase.
            string onlyLetters = Regex.Replace(message.ToUpper(), @"[^A-Z]+", String.Empty);

            // Split the string into five character chunks.
            while (onlyLetters.Length > 5)
            {
                string messageBlock = onlyLetters.Substring(0, 5);
                onlyLetters = onlyLetters.Substring(5);
                messageList.Add(messageBlock);
            }

            // If after breaking the string into five character chunks we have additional
            // characters, we need to append an 'X' for each missing character until we have
            // a five character chunk.
            if (onlyLetters.Length > 0)
            {
                // Loop until we have five characters in the string.
                while (onlyLetters.Length < 5)
                {
                    // Append an 'X' to the end of the string.
                    onlyLetters = String.Format("{0}X", onlyLetters);
                }

                // We now have five characters.  Add the string to the list.
                messageList.Add(onlyLetters);
            }

            return messageList;
        }

        /// <summary>
        /// Convert the list of strings into a list of arrays of integers representing
        /// each letter for each string in the list.
        /// </summary>
        /// <param name="messageList">The list of strings</param>
        /// <returns>The converted list of arrays of integers</returns>
        public List<int[]> ConvertToNumbers(List<string> messageList)
        {
            List<int[]> intArrayList = new List<int[]>();

            // Loop through each string in the list.
            foreach (string str in messageList)
            {
                // Create an array of bytes.  Each byte represents the ASCII value for
                // each letter in the string.
                byte[] asciiBytes = Encoding.ASCII.GetBytes(str);


                int[] convertedBytes = new int[5];
                List<int> tempList = new List<int>();

                // Loop through each byte in the array.
                foreach (byte myByte in asciiBytes)
                {
                    // Subtract 64 from the byte value and convert to an integer.
                    // Store the integer into a temporary list.
                    tempList.Add((int)myByte - 64);
                }

                // Convert the temporary list into an array of integers.
                convertedBytes = tempList.ToArray();

                // Add the array of integers to a new list that will be returned.
                intArrayList.Add(convertedBytes);
            }

            return intArrayList;
        }

        #endregion
    }
}
