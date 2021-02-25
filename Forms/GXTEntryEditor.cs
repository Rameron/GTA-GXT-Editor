using System;
using System.Windows.Forms;

namespace GTA_GXT_Editor.Forms
{
    public partial class GXTEntryEditor : Form
    {
        private string _gxtEntryName;
        private string _gxtEntryValue;
        private string _gxtEntryTable;

        private bool _isOperationCanceled;

        public GXTEntryEditor(string gxtEntryName, string gxtEntryValue, string gxtEntryTable)
        {
            InitializeComponent();

            _gxtEntryName = gxtEntryName;
            _gxtEntryValue = gxtEntryValue;
            _gxtEntryTable = gxtEntryTable;

            _isOperationCanceled = true;
        }

        public bool IsOperationCanceled { get => _isOperationCanceled; }

        public string GxtEntryName { get => _gxtEntryName; }
        public string GxtEntryValue { get => _gxtEntryValue; }
        public string GxtEntryTable { get => _gxtEntryTable; }

        public void ShowTableComboBox(string[] tableNames)
        {
            btnProcessEntry.Top = 93;
            btnCancel.Top = 132;
            Height = 215;

            lblTableCaption.Visible = true;
            comboTables.Visible = true;

            comboTables.Items.Clear();
            comboTables.Items.AddRange(tableNames);
            comboTables.SelectedIndex = 0;
        }

        public void HideTableComboBox()
        {
            btnProcessEntry.Top = 62;
            btnCancel.Top = 101;
            Height = 191;

            lblTableCaption.Visible = false;
            comboTables.Visible = false;

            comboTables.Items.Clear();
            comboTables.Text = default(string);
        }

        public void SetAddingMode()
        {
            Text = "Добавление нового элемента";
            btnProcessEntry.Text = "Добавить элемент";

            txtBoxName.ReadOnly = false;
        }

        public void SetEditMode()
        {
            Text = "Редактирование существующего элемента";
            btnProcessEntry.Text = "Редактировать элемент";

            txtBoxName.ReadOnly = true;
        }

        private void GXTEntryEditor_Load(object sender, EventArgs e)
        {
            txtBoxName.Text = _gxtEntryName;
            txtBoxValue.Text = _gxtEntryValue;
            comboTables.Text = _gxtEntryTable;
        }

        private void btnProcessEntry_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBoxName.Text))
            {
                MessageBox.Show(this, "Имя элемента не может быть пустым", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (string.IsNullOrEmpty(txtBoxValue.Text))
            {
                MessageBox.Show(this, "Текст элемента не может быть пустым", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!string.IsNullOrEmpty(_gxtEntryTable) && string.IsNullOrEmpty(comboTables.Text))
            {
                MessageBox.Show(this, "Таблица элемента не может быть пустой", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            _isOperationCanceled = false;

            _gxtEntryName = txtBoxName.Text;
            _gxtEntryValue = txtBoxValue.Text;
            _gxtEntryTable = comboTables.Text;

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
