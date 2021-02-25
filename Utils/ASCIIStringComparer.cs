using System.Collections.Generic;

namespace GTA_3_GXT_Editor.Utils
{
    public class ASCIIStringComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            var compareLength = x.Length > y.Length ? y.Length : x.Length;

            for (int charIndex = 0; charIndex < compareLength; charIndex++)
            {
                if (x[charIndex] > y[charIndex])
                {
                    return 1;
                }
                else if (x[charIndex] < y[charIndex])
                {
                    return -1;
                }
            }

            return 0;
        }
    }
}
