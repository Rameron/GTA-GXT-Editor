using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GTA_GXT_Editor.Utils
{
    public static class StructExtensions
    {
        public static bool GXTValueIsValid(this byte[] inputBytes)
        {
            if (inputBytes.Length == 2)
            {
                if (inputBytes[0] == 0 && inputBytes[1] == 0)
                {
                    return true;
                }
            }

            for (int arrayIndex = 0; arrayIndex < inputBytes.Length; arrayIndex++)
            {
                if (inputBytes[arrayIndex] == 0)
                {
                    if (arrayIndex != inputBytes.Length - 1)
                    {
                        if (inputBytes[arrayIndex + 1] == 0)
                        {
                            if (arrayIndex < inputBytes.Length - 3)
                            {
                                return false;
                            }
                            else
                            {
                                if (inputBytes[arrayIndex + 2] == 0)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static string FillWithZeros(this string inputString, int desiredLength)
        {
            return inputString + new string('\0', desiredLength - inputString.Length);
        }

        public static Dictionary<int[], char> LoadCyryllicCharsDictionary(this string charsFilePath)
        {
            Dictionary<int[], char> cyryllicCharsDictionary = new Dictionary<int[], char>();

            string[] dictLines = File.ReadAllLines(charsFilePath, System.Text.Encoding.GetEncoding(1251));
            foreach (var line in dictLines)
            {
                var lineItems = line.Split(' ');
                var indexes = lineItems[0].Split(',');

                if (indexes.Length == 1)
                {
                    cyryllicCharsDictionary.Add(new int[] { int.Parse(lineItems[0]) }, lineItems[1][0]);
                }
                else
                {
                    var parsedIndexes = indexes.Select(x => int.Parse(x)).ToArray();
                    cyryllicCharsDictionary.Add(parsedIndexes, lineItems[1][0]);
                }
            }

            return cyryllicCharsDictionary;
        }

        public static string GetClearName(this string dirtyName)
        {
            var nullIndex = dirtyName.IndexOf('\0');

            return dirtyName.Substring(0, nullIndex == -1 ? dirtyName.Length : nullIndex);
        }
    }
}
