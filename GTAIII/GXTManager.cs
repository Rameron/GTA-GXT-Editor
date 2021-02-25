using GTA_3_GXT_Editor.Utils;
using GTA_GXT_Editor.Common;
using GTA_GXT_Editor.Contracts;
using GTA_GXT_Editor.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GTA_GXT_Editor.GTAIII
{
    public class GXTManager : CommonGXTManager
    {
        private const string RUSSIAN_CHARS_FILENAME = "russian_chars.txt";

        private string _gxtPath;

        private List<GXTBase> _gxtEntries;
        private Dictionary<int[], char> _cyryllicCharsDictionary;

        public override List<GXTBase> GXTEntries { get => _gxtEntries; }
        public override Dictionary<int[], char> CyryllicCharsDictionary { get => _cyryllicCharsDictionary; set => _cyryllicCharsDictionary = value; }

        public GXTManager(string gxtPath)
        {
            _gxtPath = gxtPath;
            _cyryllicCharsDictionary = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), RUSSIAN_CHARS_FILENAME).LoadCyryllicCharsDictionary();
            _gxtEntries = ReadGXTFile(gxtPath);
        }


        public override void AddGXTEntry(string newDatName, string newDatValue, string tableName = default(string))
        {
            _gxtEntries.Add(new GXTEntry { DatName = newDatName.FillWithZeros(8), Value = ConvertTextToBytes(newDatValue) });
        }

        public override void EditGXTEntry(string datName, string newDatValue, string tableName = default(string))
        {
            var editIndex = _gxtEntries.FindIndex(x => x.DatName.GetClearName() == datName);
            var editValue = _gxtEntries[editIndex].Value;

            _gxtEntries[editIndex].Value = ConvertTextToBytes(newDatValue);
        }

        public override void RemoveGXTEntry(string datName, string tableName = default(string))
        {
            var removeIndex = _gxtEntries.FindIndex(x => x.DatName.GetClearName() == datName);
            _gxtEntries.RemoveAt(removeIndex);
        }

        public override void SaveGXTChanges(string gxtFilePath)
        {
            WriteGXTFile(gxtFilePath);
        }


        public override List<GXTBase> ReadGXTFile(string gxtFilePath)
        {
            List<GXTEntry> localGXTEntries = new List<GXTEntry>();

            using (FileStream fsStream = new FileStream(gxtFilePath, FileMode.Open, FileAccess.Read))
            {
                //TKEY
                string tKeyString = fsStream.ReadString(4);

                //Size of TKEY
                int tKeyBlockSize = fsStream.ReadInt();

                //TKEY Entries
                List<KeyValuePair<int, string>> valueOffsets = new List<KeyValuePair<int, string>>();
                int readedBytes = 0;
                do
                {
                    int tDatOffset = fsStream.ReadInt();
                    string tDatName = fsStream.ReadString(8);

                    valueOffsets.Add(new KeyValuePair<int, string>(tDatOffset, tDatName));

                    readedBytes += 12;
                } while (readedBytes != tKeyBlockSize);

                //TDAT
                tKeyString = fsStream.ReadString(4);

                //Size of TDAT
                int tDatBlockSize = fsStream.ReadInt();

                //TDAT Entries                                
                var orderedOffsets = valueOffsets.Select(x => x.Key).OrderBy(x => x).ToList();
                for (var orderedOffsetsIndex = 0; orderedOffsetsIndex < orderedOffsets.Count; orderedOffsetsIndex++)
                {
                    int readLength = 0;

                    if (orderedOffsetsIndex != orderedOffsets.Count - 1)
                    {
                        readLength = orderedOffsets[orderedOffsetsIndex + 1] - orderedOffsets[orderedOffsetsIndex];
                    }
                    else
                    {
                        readLength = (int)fsStream.Length - 16 - tKeyBlockSize - orderedOffsets[orderedOffsetsIndex];
                    }

                    fsStream.Seek(16 + tKeyBlockSize + orderedOffsets[orderedOffsetsIndex], SeekOrigin.Begin);

                    var valueName = valueOffsets.First(x => x.Key == orderedOffsets[orderedOffsetsIndex]).Value;
                    var valueBlock = fsStream.ReadBytes(readLength);

                    localGXTEntries.Add(new GXTEntry { DatName = valueName, Value = valueBlock });
                }
                if (tKeyBlockSize + tDatBlockSize + 16 == fsStream.Length)
                {
                    return localGXTEntries.Cast<GXTBase>().ToList();
                }
            }
            throw new Exception("Ошибка при чтении GXT-файла.");
        }

        public override void WriteGXTFile(string gxtFilePath)
        {
            _gxtEntries = _gxtEntries.OrderBy(x => x.DatName, new ASCIIStringComparer()).ToList();

            using (FileStream fsStream = new FileStream(gxtFilePath, FileMode.Create, FileAccess.Write))
            {
                fsStream.WriteString("TKEY");
                fsStream.WriteInt(12 * _gxtEntries.Count);

                var nextOffset = 0;
                for (int gtxEntryIndex = 0; gtxEntryIndex < _gxtEntries.Count; gtxEntryIndex++)
                {
                    fsStream.WriteInt(nextOffset);
                    fsStream.WriteString(_gxtEntries[gtxEntryIndex].DatName.FillWithZeros(8));

                    nextOffset += _gxtEntries[gtxEntryIndex].Value.Length;
                }

                fsStream.WriteString("TDAT");
                fsStream.WriteInt(_gxtEntries.Sum(x => x.Value.Length));

                for (int gtxEntryIndex = 0; gtxEntryIndex < _gxtEntries.Count; gtxEntryIndex++)
                {
                    fsStream.WriteBytes(_gxtEntries[gtxEntryIndex].Value);
                }
            }
        }
    }


}
