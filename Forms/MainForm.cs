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

        private bool InitGXTManager()
        {
            btnAddEntry.Enabled = false;
            btnEditEntry.Enabled = false;
            btnDeleteEntry.Enabled = false;
            btnAddMissingEntries.Enabled = false;
            btnSaveChanges.Enabled = false;

            lstKeys.Items.Clear();

            _loadedGxtType = GXTType.NONE;

            if (File.Exists(txtBoxGxtFilePath.Text))
            {
                try
                {
                    var gtxType = DetectGxtType(txtBoxGxtFilePath.Text);

                    if (gtxType == GXTType.GTA_III)
                    {
                        gxtManager = new GTAIII.GXTManager(txtBoxGxtFilePath.Text);
                        _loadedGxtType = gtxType;
                        return true;
                    }
                    else if (gtxType == GXTType.GTA_VC)
                    {
                        gxtManager = new GTAVC.GXTManager(txtBoxGxtFilePath.Text);
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
        }

        private void ExecuteSearch()
        {
            List<ListViewItem> items = lstKeys.Items.Cast<ListViewItem>().ToList();

            var searchIndex = items.FindIndex(_searchStartIndex, x => cmbSearchColumn.SelectedIndex == 0 ?
                chkCaseCheck.Checked ?
                    x.Text.Contains(txtSearchText.Text) :
                    x.Text.ToLower().Contains(txtSearchText.Text.ToLower()) :
                chkCaseCheck.Checked ?
                    x.SubItems[cmbSearchColumn.SelectedIndex].Text.Contains(txtSearchText.Text) :
                    x.SubItems[cmbSearchColumn.SelectedIndex].Text.ToLower().Contains(txtSearchText.Text.ToLower()));

            if (searchIndex != -1)
            {
                lstKeys.Items[searchIndex].Focused = true;
                lstKeys.Items[searchIndex].Selected = true;
                lstKeys.Items[searchIndex].EnsureVisible();

                _searchStartIndex = searchIndex;
            }
            else if (chkLoopSearch.Checked && _searchStartIndex != 0)
            {
                _searchStartIndex = 0;
                ExecuteSearch();
            }
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


        private void txtSearchText_TextChanged(object sender, EventArgs e)
        {
            _searchStartIndex = 0;
            ExecuteSearch();
        }

        private void cmbSearchColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExecuteSearch();
        }

        private void btnNextSearch_Click(object sender, EventArgs e)
        {
            if (lstKeys.Items.Count == 0)
            {
                return;
            }

            _searchStartIndex++;
            ExecuteSearch();
        }

        private void txtSearchText_KeyDown(object sender, KeyEventArgs e)
        {
            btnNextSearch_Click(sender, e);
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
