using GTA_GXT_Editor.Common;
using System.Collections.Generic;
using System.Linq;

namespace GTA_GXT_Editor.Contracts
{
    public abstract class CommonGXTManager
    {
        public abstract Dictionary<int[], char> CyryllicCharsDictionary { get; set; }

        public abstract List<GXTBase> GXTEntries { get; }

        public abstract void AddGXTEntry(string newDatName, string newDatValue, string tableName = default(string));

        public abstract void EditGXTEntry(string datName, string newDatValue, string tableName = default(string));

        public abstract void RemoveGXTEntry(string datName, string tableName = default(string));

        public abstract void SaveGXTChanges(string gxtFilePath);

        public abstract List<GXTBase> ReadGXTFile(string gxtFilePath);

        public abstract void WriteGXTFile(string gxtFilePath);

        public string ConvertBytesToText(byte[] inputBytes)
        {
            string targetString = string.Empty;

            for (int arrayIndex = 0; arrayIndex < inputBytes.Length; arrayIndex += 2)
            {
                char targetChar = CyryllicCharsDictionary.FirstOrDefault(x => x.Key.Contains(inputBytes[arrayIndex])).Value;

                if (targetChar == default(char))
                {
                    targetString += ((char)inputBytes[arrayIndex]).ToString();
                }
                else
                {
                    targetString += targetChar.ToString();
                }
            }

            return targetString.Remove(targetString.Length - 1);
        }

        public byte[] ConvertTextToBytes(string inputString)
        {
            List<byte> targetBytes = new List<byte>();

            for (int stringChar = 0; stringChar < inputString.Length; stringChar++)
            {
                int targetByte = 0;
                if (CyryllicCharsDictionary.Any(x => x.Value == inputString[stringChar]))
                {
                    targetByte = CyryllicCharsDictionary.First(x => x.Value == inputString[stringChar]).Key[0];
                }
                else
                {
                    targetByte = (byte)inputString[stringChar];
                }

                targetBytes.Add((byte)targetByte);
                targetBytes.Add(0);
            }
            targetBytes.Add(0);
            targetBytes.Add(0);

            return targetBytes.ToArray();
        }
    }
}
