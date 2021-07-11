using GTA_3_GXT_Editor.Utils;
using GTA_GXT_Editor.Common;
using GTA_GXT_Editor.Contracts;
using GTA_GXT_Editor.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace GTA_GXT_Editor.Forms
{
    public partial class MainForm : Form
    {
        private CommonGXTManager gxtManager;
        private int _searchStartIndex = 0;
        private string _gxtPath;

        private GXTType _loadedGxtType = GXTType.NONE;

        public MainForm(string[] args)
        {
            InitializeComponent();

            if (args != null && args.Length != 0 && !string.IsNullOrEmpty(args[0]) && File.Exists(args[0]))
            {
                _gxtPath = args[0];
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            txtBoxGxtFilePath.Text = _gxtPath;

            if (File.Exists(txtBoxGxtFilePath.Text))
            {
                if (InitGXTManager())
                {
                    LoadGXTDataToGUI();
                }
                else
                {
                    txtBoxGxtFilePath.Text = string.Empty;
                }
            }

            foreach (var columnHeader in lstKeys.Columns)
            {
                cmbSearchColumn.Items.Add((columnHeader as ColumnHeader).Text);
            }
            cmbSearchColumn.SelectedIndex = 0;
        }

        private bool InitGXTManager(bool noMessages = false)
        {
            btnAddEntry.Enabled = false;
            btnEditEntry.Enabled = false;
            btnDeleteEntry.Enabled = false;
            btnAddMissingEntries.Enabled = false;
            btnSaveChanges.Enabled = false;
            btnConvertToOtherDict.Enabled = false;
            btnReload.Enabled = false;

            lstKeys.Items.Clear();

            _loadedGxtType = GXTType.NONE;

            if (File.Exists(txtBoxGxtFilePath.Text))
            {
                try
                {
                    var gtxType = DetectGxtType(txtBoxGxtFilePath.Text);

                    string charDictionaryPath = null;
                    if (noMessages)
                    {
                        charDictionaryPath = gxtManager.CyryllicCharsDictionaryPath;
                    }
                    else
                    {
                        var dialogResult = MessageBox.Show(this, "Использовать встроенный словарь символов?", "Словарь символов", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dialogResult == DialogResult.No)
                        {
                            charDictionaryPath = RequestCharsDictionary();
                        }

                        // При загрузке нового файла индекс поиска сбрасывается
                        _searchStartIndex = 0;
                    }

                    if (gtxType == GXTType.GTA_III)
                    {
                        gxtManager = new GTAIII.GXTManager(txtBoxGxtFilePath.Text, charDictionaryPath);
                        _loadedGxtType = gtxType;
                        return true;
                    }
                    else if (gtxType == GXTType.GTA_VC)
                    {
                        gxtManager = new GTAVC.GXTManager(txtBoxGxtFilePath.Text, charDictionaryPath);
                        _loadedGxtType = gtxType;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show(this, $"Не удалось открыть '{Path.GetFileName(txtBoxGxtFilePath.Text)}'. Файл повреждён или не является совместимым.", "Ошибка открытия");
                        return false;
                    }
                }
                catch
                {
                    MessageBox.Show(this, $"Не удалось открыть '{Path.GetFileName(txtBoxGxtFilePath.Text)}'. Файл повреждён или не является совместимым.", "Ошибка открытия");
                    return false;
                }
            }
            else
            {
                MessageBox.Show(this, $"Файл '{Path.GetFileName(txtBoxGxtFilePath.Text)}' не существует или недоступен.", "Ошибка открытия");
                return false;
            }
        }

        private void LoadGXTDataToGUI()
        {
            lstKeys.Items.Clear();
            lstKeys.Clear();

            lstKeys.Columns.Add("Название");
            lstKeys.Columns.Add("Текст");
            if (_loadedGxtType == GXTType.GTA_VC)
            {
                lstKeys.Columns.Add("Таблица");
            }

            ListViewItem[] listViewItems = new ListViewItem[gxtManager.GXTEntries.Count];
            for (var gxtEntryIndex = 0; gxtEntryIndex < gxtManager.GXTEntries.Count; gxtEntryIndex++)
            {
                var entryName = gxtManager.GXTEntries[gxtEntryIndex].DatName.GetClearName();
                var entryValue = gxtManager.ConvertBytesToText(gxtManager.GXTEntries[gxtEntryIndex].Value).GetClearName();

                if (_loadedGxtType == GXTType.GTA_III)
                {
                    listViewItems[gxtEntryIndex] = new ListViewItem(new string[] { entryName, entryValue });
                }
                else if (_loadedGxtType == GXTType.GTA_VC)
                {
                    var entryTable = (gxtManager.GXTEntries[gxtEntryIndex] as GTAVC.GXTEntry).TableName.GetClearName();
                    listViewItems[gxtEntryIndex] = new ListViewItem(new string[] { entryName, entryValue, entryTable });
                }
            }
            lstKeys.Items.AddRange(listViewItems);
            lstKeys.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            btnAddEntry.Enabled = true;
            btnAddMissingEntries.Enabled = true;
            btnSaveChanges.Enabled = true;
            btnConvertToOtherDict.Enabled = true;
            btnReload.Enabled = true;
        }

        private bool ExecuteSearch()
        {
            if (lstKeys.Items.Count == 0)
            {
                return false;
            }
            
            if (string.IsNullOrEmpty(txtSearchText.Text))
            {
                return false;
            }

            List<ListViewItem> items = lstKeys.Items.Cast<ListViewItem>().ToList();

            string[] searchValues;
            if (chkCaseCheck.Checked)
            {
                searchValues = GetStringCombinations(txtSearchText.Text);
            }
            else
            {
                searchValues = GetStringCombinations(txtSearchText.Text.ToLower()).Union(GetStringCombinations(txtSearchText.Text.ToUpper())).ToArray();
            }

            int searchIndex = -1;
            for (int itemIndex = _searchStartIndex; itemIndex < lstKeys.Items.Count; itemIndex++)
            {
                string itemValue = cmbSearchColumn.SelectedIndex == 0 ? lstKeys.Items[itemIndex].Text : lstKeys.Items[itemIndex].SubItems[cmbSearchColumn.SelectedIndex].Text;

                if (chkCaseCheck.Checked)
                {
                    if (searchValues.Any(x => itemValue.Contains(x)))
                    {
                        searchIndex = itemIndex;
                        break;
                    }
                }
                else
                {
                    if (searchValues.Any(x => itemValue.ToLower().Contains(x)))
                    {
                        searchIndex = itemIndex;
                        break;
                    }
                }
            }

            if (searchIndex != -1)
            {
                lstKeys.Items[searchIndex].Focused = true;
                lstKeys.Items[searchIndex].Selected = true;
                lstKeys.Items[searchIndex].EnsureVisible();

                _searchStartIndex = searchIndex;

                return true;
            }
            else if (chkLoopSearch.Checked && _searchStartIndex != 0)
            {
                _searchStartIndex = 0;
                return ExecuteSearch();
            }

            return false;
        }

        private string[] GetStringCombinations(string inputString)
        {
            List<KeyValuePair<char, char>> charVariations = GetVariations();

            List<string> stringVariations = new List<string>();
            stringVariations.Add(inputString);

            foreach (KeyValuePair<char, char> pair in charVariations)
            {
                for (int i = 0; i < stringVariations.Count; i++)
                {
                    if (stringVariations[i].Contains(pair.Key))
                    {
                        string newVariation = stringVariations[i].Replace(pair.Key, pair.Value);

                        if (!stringVariations.Contains(newVariation))
                        {
                            stringVariations.Add(newVariation);
                        }
                    }
                    else if (stringVariations[i].Contains(pair.Value))
                    {
                        string newVariation = stringVariations[i].Replace(pair.Value, pair.Key);

                        if (!stringVariations.Contains(newVariation))
                        {
                            stringVariations.Add(newVariation);
                        }
                    }
                }
            }

            return stringVariations.ToArray();
        }

        private List<KeyValuePair<char, char>> GetVariations()
        {
            List<KeyValuePair<char, char>> charVariations = new List<KeyValuePair<char, char>>();

            charVariations.Add(new KeyValuePair<char, char>('а', 'a'));
            charVariations.Add(new KeyValuePair<char, char>('б', 'b'));
            charVariations.Add(new KeyValuePair<char, char>('г', 'r'));
            charVariations.Add(new KeyValuePair<char, char>('е', 'e'));
            charVariations.Add(new KeyValuePair<char, char>('д', 'g'));
            charVariations.Add(new KeyValuePair<char, char>('к', 'k'));
            charVariations.Add(new KeyValuePair<char, char>('т', 'm'));
            charVariations.Add(new KeyValuePair<char, char>('о', 'o'));
            charVariations.Add(new KeyValuePair<char, char>('р', 'p'));
            charVariations.Add(new KeyValuePair<char, char>('с', 'c'));
            charVariations.Add(new KeyValuePair<char, char>('т', 't'));
            charVariations.Add(new KeyValuePair<char, char>('у', 'y'));
            charVariations.Add(new KeyValuePair<char, char>('х', 'x'));

            charVariations.Add(new KeyValuePair<char, char>('А', 'A'));
            charVariations.Add(new KeyValuePair<char, char>('В', 'B'));
            charVariations.Add(new KeyValuePair<char, char>('С', 'C'));
            charVariations.Add(new KeyValuePair<char, char>('Е', 'E'));
            charVariations.Add(new KeyValuePair<char, char>('Н', 'H'));
            charVariations.Add(new KeyValuePair<char, char>('К', 'K'));
            charVariations.Add(new KeyValuePair<char, char>('М', 'M'));
            charVariations.Add(new KeyValuePair<char, char>('О', 'O'));
            charVariations.Add(new KeyValuePair<char, char>('Р', 'P'));
            charVariations.Add(new KeyValuePair<char, char>('Т', 'T'));
            charVariations.Add(new KeyValuePair<char, char>('Х', 'X'));
            charVariations.Add(new KeyValuePair<char, char>('У', 'Y'));

            return charVariations;
        }

        private GXTType DetectGxtType(string gtxPath)
        {
            if (new FileInfo(gtxPath).Length < 4)
            {
                return GXTType.NONE;
            }

            string startOfFile = string.Empty;
            using (StreamReader streamReader = new StreamReader(txtBoxGxtFilePath.Text))
            {
                var charBuffer = new char[4];

                if (streamReader.Read(charBuffer, 0, charBuffer.Length) == charBuffer.Length)
                {
                    startOfFile = new string(charBuffer);
                }
                else
                {
                    MessageBox.Show(this, $"Не удалось открыть '{Path.GetFileName(txtBoxGxtFilePath.Text)}'. Файл повреждён или не является совместимым.", "Ошибка открытия");
                    return GXTType.NONE;
                }
            }

            if (startOfFile == "TKEY")
            {
                return GXTType.GTA_III;
            }
            else if (startOfFile == "TABL")
            {
                return GXTType.GTA_VC;
            }
            else
            {
                MessageBox.Show(this, $"Не удалось открыть '{Path.GetFileName(txtBoxGxtFilePath.Text)}'. Файл повреждён или не является совместимым.", "Ошибка открытия");
                return GXTType.NONE;
            }
        }

        private string RequestCharsDictionary()
        {
            openFileDialog.Filter = $"Словарь шрифта (*.txt)|*.txt|Все файлы (*.*)|*.*";
            openFileDialog.FileName = "";

            var dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                return openFileDialog.FileName;
            }

            return null;
        }


        private void chkLiveSearch_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLiveSearch.Checked)
            {
                ExecuteSearch();
            }
        }

        private void txtSearchText_TextChanged(object sender, EventArgs e)
        {
            if (chkLiveSearch.Checked)
            {
                ExecuteSearch();
            }
        }

        private void cmbSearchColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chkLiveSearch.Checked)
            {
                ExecuteSearch();
            }
        }

        private void btnNextSearch_Click(object sender, EventArgs e)
        {
            // Осуществлять поиск следующего вхождения только при заполненном списке
            if (lstKeys.Items.Count == 0)
            {
                return;
            }

            // Инкрементировать индекс поиска только в том случае, если он находится в границах списка
            if (_searchStartIndex < lstKeys.Items.Count)
            {
                _searchStartIndex++;
            }

            // В противном случае, если включён циклический поиск, то перемещать индекс в самое начало
            else if (chkLoopSearch.Checked)
            {
                _searchStartIndex = 0;
            }

            // Если поиск завершился неудачей и индекс не находится в начале списка - отменить его инкремент
            if (!ExecuteSearch() && _searchStartIndex != 0)
            {
                _searchStartIndex--;
            }
        }

        private void txtSearchText_KeyDown(object sender, KeyEventArgs e)
        {
            // Если нажат Enter, то выполнить поиск
            if (e.KeyCode == Keys.Return)
            {
                ExecuteSearch();
            }
        }        

        private void lstKeys_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (lstKeys.Sorting == SortOrder.Ascending)
            {
                lstKeys.ListViewItemSorter = new ListComparer(e.Column, true);
                lstKeys.Sorting = SortOrder.Descending;
            }
            else
            {
                lstKeys.ListViewItemSorter = new ListComparer(e.Column, false);
                lstKeys.Sorting = SortOrder.Ascending;
            }
            lstKeys.Sort();
        }

        private void btnAddEntry_Click(object sender, EventArgs e)
        {
            GXTEntryEditor entryEditor = new GXTEntryEditor(string.Empty, string.Empty, "MAIN\0\0\0\0");
            entryEditor.SetAddingMode();

            if (_loadedGxtType == GXTType.GTA_III)
            {
                entryEditor.HideTableComboBox();
            }
            else if (_loadedGxtType == GXTType.GTA_VC)
            {
                var tableNames = gxtManager.GXTEntries.Select(x => (x as GTAVC.GXTEntry).TableName).Distinct().OrderBy(x => x, new ASCIIStringComparer()).ToList();

                tableNames.Remove("MAIN\0\0\0\0");
                tableNames.Insert(0, "MAIN\0\0\0\0");

                entryEditor.ShowTableComboBox(tableNames.ToArray());
            }

            entryEditor.ShowDialog();

            if (entryEditor.IsOperationCanceled)
            {
                return;
            }

            gxtManager.AddGXTEntry(entryEditor.GxtEntryName, entryEditor.GxtEntryValue, entryEditor.GxtEntryTable);

            if (_loadedGxtType == GXTType.GTA_III)
            {
                lstKeys.Items.Add(
                    new ListViewItem(new string[]
                    {
                        gxtManager.GXTEntries.Last().DatName.GetClearName(),
                        gxtManager.ConvertBytesToText(gxtManager.GXTEntries.Last().Value).GetClearName()
                    }));
            }
            else if (_loadedGxtType == GXTType.GTA_III)
            {
                lstKeys.Items.Add(
                    new ListViewItem(new string[]
                    {
                        gxtManager.GXTEntries.Last().DatName.GetClearName(),
                        gxtManager.ConvertBytesToText(gxtManager.GXTEntries.Last().Value).GetClearName(),
                        (gxtManager.GXTEntries.Last() as GTAVC.GXTEntry).TableName.GetClearName()
                    }));
            }
            else
            {
                throw new Exception("Непредвиденная ошибка");
            }
        }

        private void btnEditEntry_Click(object sender, EventArgs e)
        {
            if (lstKeys.SelectedIndices == null || lstKeys.SelectedIndices.Count == 0)
            {
                return;
            }

            var gxtName = lstKeys.SelectedItems[0].Text;
            var gxtValue = lstKeys.SelectedItems[0].SubItems[1].Text;
            var gxtTable = string.Empty;

            GXTEntryEditor entryEditor = null;

            if (_loadedGxtType == GXTType.GTA_III)
            {
                entryEditor = new GXTEntryEditor(gxtName, gxtValue, gxtTable);
                entryEditor.HideTableComboBox();
            }
            else if (_loadedGxtType == GXTType.GTA_VC)
            {
                gxtTable = lstKeys.SelectedItems[0].SubItems[2].Text;

                var tableNames = gxtManager.GXTEntries.Select(x => (x as GTAVC.GXTEntry).TableName).Distinct().OrderBy(x => x, new ASCIIStringComparer()).ToList();

                tableNames.Remove("MAIN\0\0\0\0");
                tableNames.Insert(0, "MAIN\0\0\0\0");

                entryEditor = new GXTEntryEditor(gxtName, gxtValue, gxtTable);
                entryEditor.ShowTableComboBox(tableNames.ToArray());
            }
            else
            {
                throw new Exception("Неожиданная ошибка");
            }

            entryEditor.SetEditMode();
            entryEditor.ShowDialog();

            if (entryEditor.IsOperationCanceled)
            {
                return;
            }

            gxtManager.EditGXTEntry(gxtName, entryEditor.GxtEntryValue, entryEditor.GxtEntryTable);

            lstKeys.SelectedItems[0].SubItems[1].Text = entryEditor.GxtEntryValue;

            if (_loadedGxtType == GXTType.GTA_VC)
            {
                lstKeys.SelectedItems[0].SubItems[2].Text = entryEditor.GxtEntryTable;
            }
        }

        private void btnDeleteEntry_Click(object sender, EventArgs e)
        {
            if (lstKeys.SelectedIndices == null || lstKeys.SelectedIndices.Count == 0)
            {
                return;
            }

            var gxtName = lstKeys.SelectedItems[0].Text.GetClearName();
            var gxtTable = string.Empty;

            if (_loadedGxtType == GXTType.GTA_VC)
            {
                gxtTable = lstKeys.SelectedItems[0].SubItems[2].Text;
            }


            var dialogResult = MessageBox.Show(this, $"Вы уверены, что хотите удалить элемент с именем '{gxtName}'?", "Подтверждение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                gxtManager.RemoveGXTEntry(gxtName, gxtTable);

                lstKeys.SelectedItems[0].Remove();
            }
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            gxtManager.SaveGXTChanges(Path.Combine(Path.GetDirectoryName(txtBoxGxtFilePath.Text), Path.GetFileNameWithoutExtension(txtBoxGxtFilePath.Text) + "_modified.gxt"));
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnSelectGxtFilePath_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "GTA III/Vice City GXT файл (*.gxt)|*.gxt|Все файлы (*.*)|*.*";
            openFileDialog.FileName = "";

            var dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                txtBoxGxtFilePath.Text = openFileDialog.FileName;

                if (InitGXTManager())
                {
                    LoadGXTDataToGUI();
                }
                else
                {
                    txtBoxGxtFilePath.Text = string.Empty;
                }
            }
        }

        private void lstKeys_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstKeys.SelectedIndices != null && lstKeys.SelectedIndices.Count != 0 && lstKeys.SelectedIndices[0] != -1)
            {
                btnEditEntry.Enabled = true;
                btnDeleteEntry.Enabled = true;

                _searchStartIndex = lstKeys.SelectedIndices[0];
            }
            else
            {
                btnEditEntry.Enabled = false;
                btnDeleteEntry.Enabled = false;
            }
        }

        private void lstKeys_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (btnEditEntry.Enabled)
            {
                btnEditEntry.PerformClick();
            }
        }

        private void btnAddMissingEntries_Click(object sender, EventArgs e)
        {
            var filterPart = string.Empty;
            if (_loadedGxtType == GXTType.GTA_III)
            {
                filterPart = "GTA III";
            }
            else if (_loadedGxtType == GXTType.GTA_VC)
            {
                filterPart = "GTA Vice City";
            }
            else
            {
                throw new Exception("Нет открытого файла для добавление элементов.");
            }

            openFileDialog.Filter = $"{filterPart} GXT файл (*.gxt)|*.gxt|Все файлы (*.*)|*.*";
            openFileDialog.FileName = "";

            var dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                var gtxType = DetectGxtType(openFileDialog.FileName);

                if (gtxType != _loadedGxtType)
                {
                    MessageBox.Show(this, $"Файл '{Path.GetFileName(openFileDialog.FileName)}' поврежден или его тип не соответствует текущему.", "Ошибка открытия");
                    return;
                }

                CommonGXTManager anotherGxtManager = null;
                try
                {
                    if (gtxType == GXTType.GTA_III)
                    {
                        anotherGxtManager = new GTAIII.GXTManager(openFileDialog.FileName);
                    }
                    else if (gtxType == GXTType.GTA_VC)
                    {
                        anotherGxtManager = new GTAVC.GXTManager(openFileDialog.FileName);
                    }
                }
                catch
                {
                    MessageBox.Show(this, $"Файл '{Path.GetFileName(openFileDialog.FileName)}' поврежден или его тип не соответствует текущему.", "Ошибка открытия");
                    return;
                }

                var missingEntries = anotherGxtManager.GXTEntries.Except(gxtManager.GXTEntries, new GXTEntryEqualityComparer()).ToList();

                foreach (var missingEntry in missingEntries)
                {
                    if (_loadedGxtType == GXTType.GTA_III)
                    {
                        gxtManager.AddGXTEntry(missingEntry.DatName, gxtManager.ConvertBytesToText(missingEntry.Value));
                        lstKeys.Items.Add(
                        new ListViewItem(new string[]
                        {
                            gxtManager.GXTEntries.Last().DatName.GetClearName(),
                            gxtManager.ConvertBytesToText(gxtManager.GXTEntries.Last().Value).GetClearName()
                        }));
                    }
                    else if (_loadedGxtType == GXTType.GTA_VC)
                    {
                        gxtManager.AddGXTEntry(missingEntry.DatName, gxtManager.ConvertBytesToText(missingEntry.Value), (missingEntry as GTAVC.GXTEntry).TableName);
                        lstKeys.Items.Add(
                        new ListViewItem(new string[]
                        {
                            gxtManager.GXTEntries.Last().DatName.GetClearName(),
                            gxtManager.ConvertBytesToText(gxtManager.GXTEntries.Last().Value).GetClearName(),
                            (missingEntry as GTAVC.GXTEntry).TableName
                        }));
                    }
                }

                MessageBox.Show(this, $"Было добавлено {missingEntries.Count} элементов.", "Опепация ");
            }
        }

        private void btnConvertToOtherDict_Click(object sender, EventArgs e)
        {
            // Получить путь к требуемому словарю
            string targetCharDictionaryPath = RequestCharsDictionary();
            if (string.IsNullOrEmpty(targetCharDictionaryPath))
            {
                MessageBox.Show("Путь к словарю символов не может быть пустым.");
                return;
            }

            // Получить сами словари для дальнейшей работы
            Dictionary<int[], char> mainCharsDictionary = gxtManager.CyryllicCharsDictionary;
            Dictionary<int[], char> alternativeCharsDictionary = targetCharDictionaryPath.LoadCyryllicCharsDictionary();

            if (mainCharsDictionary.Count != alternativeCharsDictionary.Count)
            {
                MessageBox.Show("Количество символов в оригинальном словаре и запрашиваемом отличается. Их количество должно совпадать.");
                return;
            }
            if (!mainCharsDictionary.Select(p => p.Value).SequenceEqual(alternativeCharsDictionary.Select(p => p.Value)))
            {
                MessageBox.Show("Список символов в оригинальном словаре и запрашиваемом отличается. Они должны совпадать.");
                return;
            }

            // Пройтись по всем записям GXT файла и для каждого байта записи заменить значение из оригинального словаря на значение из требуемого словаря
            for (int gxtIndex = 0; gxtIndex < gxtManager.GXTEntries.Count; gxtIndex++)
            {
                foreach (KeyValuePair<int[], char> keyValuePair in mainCharsDictionary)
                {
                    byte[] processingBytes = gxtManager.GXTEntries[gxtIndex].Value;

                    for (int byteIndex = 0; byteIndex < processingBytes.Length; byteIndex++)
                    {
                        if (processingBytes[byteIndex] == 0)
                        {
                            continue;
                        }
                        else if (keyValuePair.Key.Any(k => k == processingBytes[byteIndex]))
                        {
                            processingBytes[byteIndex] = (byte)alternativeCharsDictionary.First(p => p.Value == keyValuePair.Value).Key.First();
                        }
                    }
                }
            }

            gxtManager.CyryllicCharsDictionaryPath = targetCharDictionaryPath;
            gxtManager.ReloadCyryllicCharsDictionary();

            LoadGXTDataToGUI();

            MessageBox.Show("Конвертация успешно завершена.");
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            var scrollSpot = lstKeys.TopItem.Index;
            var selectedIndex = -1;

            if (lstKeys.SelectedItems.Count > 0)
            {
                selectedIndex = lstKeys.SelectedItems[0].Index;
            }

            if (InitGXTManager(true))
            {
                LoadGXTDataToGUI();
            }

            lstKeys.TopItem = lstKeys.Items[scrollSpot];
            if (selectedIndex != -1)
            {
                lstKeys.Items[selectedIndex].Selected = true;
            }
        }
    }

    public class ListComparer : IComparer
    {
        private readonly int columnNumber;
        private readonly bool ascSort;

        public ListComparer(int columnNumber, bool ascSort)
        {
            this.columnNumber = columnNumber;
            this.ascSort = ascSort;
        }

        public int Compare(object x, object y)
        {
            var sortMultiplier = ascSort ? 1 : -1;

            string valueX = columnNumber == 0 ? ((ListViewItem)x).Text : ((ListViewItem)x).SubItems[columnNumber].Text;
            string valueY = columnNumber == 0 ? ((ListViewItem)y).Text : ((ListViewItem)y).SubItems[columnNumber].Text;

            return sortMultiplier * valueX.CompareTo(valueY);
        }
    }
}
