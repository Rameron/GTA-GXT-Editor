
namespace GTA_GXT_Editor.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDeleteEntry = new System.Windows.Forms.Button();
            this.btnEditEntry = new System.Windows.Forms.Button();
            this.btnAddEntry = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkLoopSearch = new System.Windows.Forms.CheckBox();
            this.btnNextSearch = new System.Windows.Forms.Button();
            this.chkCaseCheck = new System.Windows.Forms.CheckBox();
            this.txtSearchText = new System.Windows.Forms.TextBox();
            this.cmbSearchColumn = new System.Windows.Forms.ComboBox();
            this.lstKeys = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBoxGxtFilePath = new System.Windows.Forms.TextBox();
            this.btnSelectGxtFilePath = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.btnAddMissingEntries = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.lstKeys);
            this.groupBox1.Location = new System.Drawing.Point(12, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(944, 560);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Элементы";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnDeleteEntry);
            this.groupBox3.Controls.Add(this.btnEditEntry);
            this.groupBox3.Controls.Add(this.btnAddEntry);
            this.groupBox3.Location = new System.Drawing.Point(693, 450);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(245, 103);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Операции с элементами";
            // 
            // btnDeleteEntry
            // 
            this.btnDeleteEntry.Enabled = false;
            this.btnDeleteEntry.Location = new System.Drawing.Point(6, 71);
            this.btnDeleteEntry.Name = "btnDeleteEntry";
            this.btnDeleteEntry.Size = new System.Drawing.Size(233, 23);
            this.btnDeleteEntry.TabIndex = 9;
            this.btnDeleteEntry.Text = "Удалить выделенное значение";
            this.btnDeleteEntry.UseVisualStyleBackColor = true;
            this.btnDeleteEntry.Click += new System.EventHandler(this.btnDeleteEntry_Click);
            // 
            // btnEditEntry
            // 
            this.btnEditEntry.Enabled = false;
            this.btnEditEntry.Location = new System.Drawing.Point(6, 46);
            this.btnEditEntry.Name = "btnEditEntry";
            this.btnEditEntry.Size = new System.Drawing.Size(233, 23);
            this.btnEditEntry.TabIndex = 8;
            this.btnEditEntry.Text = "Редактировать выделенное значение";
            this.btnEditEntry.UseVisualStyleBackColor = true;
            this.btnEditEntry.Click += new System.EventHandler(this.btnEditEntry_Click);
            // 
            // btnAddEntry
            // 
            this.btnAddEntry.Enabled = false;
            this.btnAddEntry.Location = new System.Drawing.Point(6, 20);
            this.btnAddEntry.Name = "btnAddEntry";
            this.btnAddEntry.Size = new System.Drawing.Size(233, 23);
            this.btnAddEntry.TabIndex = 6;
            this.btnAddEntry.Text = "Создать новое значение";
            this.btnAddEntry.UseVisualStyleBackColor = true;
            this.btnAddEntry.Click += new System.EventHandler(this.btnAddEntry_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chkLoopSearch);
            this.groupBox2.Controls.Add(this.btnNextSearch);
            this.groupBox2.Controls.Add(this.chkCaseCheck);
            this.groupBox2.Controls.Add(this.txtSearchText);
            this.groupBox2.Controls.Add(this.cmbSearchColumn);
            this.groupBox2.Location = new System.Drawing.Point(6, 450);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(372, 103);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Поиск";
            // 
            // chkLoopSearch
            // 
            this.chkLoopSearch.AutoSize = true;
            this.chkLoopSearch.Location = new System.Drawing.Point(6, 75);
            this.chkLoopSearch.Name = "chkLoopSearch";
            this.chkLoopSearch.Size = new System.Drawing.Size(113, 17);
            this.chkLoopSearch.TabIndex = 7;
            this.chkLoopSearch.Text = "Зациклить поиск";
            this.chkLoopSearch.UseVisualStyleBackColor = true;
            // 
            // btnNextSearch
            // 
            this.btnNextSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextSearch.Location = new System.Drawing.Point(298, 12);
            this.btnNextSearch.Name = "btnNextSearch";
            this.btnNextSearch.Size = new System.Drawing.Size(68, 32);
            this.btnNextSearch.TabIndex = 6;
            this.btnNextSearch.Text = "Далее";
            this.btnNextSearch.UseVisualStyleBackColor = true;
            this.btnNextSearch.Click += new System.EventHandler(this.btnNextSearch_Click);
            // 
            // chkCaseCheck
            // 
            this.chkCaseCheck.AutoSize = true;
            this.chkCaseCheck.Location = new System.Drawing.Point(6, 50);
            this.chkCaseCheck.Name = "chkCaseCheck";
            this.chkCaseCheck.Size = new System.Drawing.Size(124, 17);
            this.chkCaseCheck.TabIndex = 5;
            this.chkCaseCheck.Text = "Учитывать регистр";
            this.chkCaseCheck.UseVisualStyleBackColor = true;
            // 
            // txtSearchText
            // 
            this.txtSearchText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchText.Location = new System.Drawing.Point(152, 19);
            this.txtSearchText.Name = "txtSearchText";
            this.txtSearchText.Size = new System.Drawing.Size(140, 20);
            this.txtSearchText.TabIndex = 4;
            this.txtSearchText.TextChanged += new System.EventHandler(this.txtSearchText_TextChanged);
            this.txtSearchText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchText_KeyDown);
            // 
            // cmbSearchColumn
            // 
            this.cmbSearchColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchColumn.FormattingEnabled = true;
            this.cmbSearchColumn.Location = new System.Drawing.Point(6, 19);
            this.cmbSearchColumn.Name = "cmbSearchColumn";
            this.cmbSearchColumn.Size = new System.Drawing.Size(140, 21);
            this.cmbSearchColumn.TabIndex = 3;
            this.cmbSearchColumn.SelectedIndexChanged += new System.EventHandler(this.cmbSearchColumn_SelectedIndexChanged);
            // 
            // lstKeys
            // 
            this.lstKeys.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstKeys.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colText});
            this.lstKeys.FullRowSelect = true;
            this.lstKeys.HideSelection = false;
            this.lstKeys.Location = new System.Drawing.Point(6, 19);
            this.lstKeys.MultiSelect = false;
            this.lstKeys.Name = "lstKeys";
            this.lstKeys.Size = new System.Drawing.Size(932, 425);
            this.lstKeys.TabIndex = 0;
            this.lstKeys.UseCompatibleStateImageBehavior = false;
            this.lstKeys.View = System.Windows.Forms.View.Details;
            this.lstKeys.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstKeys_ColumnClick);
            this.lstKeys.SelectedIndexChanged += new System.EventHandler(this.lstKeys_SelectedIndexChanged);
            this.lstKeys.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lstKeys_MouseDoubleClick);
            // 
            // colName
            // 
            this.colName.Text = "Название";
            this.colName.Width = 100;
            // 
            // colText
            // 
            this.colText.Text = "Текст";
            this.colText.Width = 300;
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSaveChanges.Enabled = false;
            this.btnSaveChanges.Location = new System.Drawing.Point(12, 599);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(168, 38);
            this.btnSaveChanges.TabIndex = 5;
            this.btnSaveChanges.Text = "Сохранить изменения в GXT";
            this.btnSaveChanges.UseVisualStyleBackColor = true;
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(788, 599);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(168, 38);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Путь к GXT файлу:";
            // 
            // txtBoxGxtFilePath
            // 
            this.txtBoxGxtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxGxtFilePath.Location = new System.Drawing.Point(121, 10);
            this.txtBoxGxtFilePath.Name = "txtBoxGxtFilePath";
            this.txtBoxGxtFilePath.Size = new System.Drawing.Size(787, 20);
            this.txtBoxGxtFilePath.TabIndex = 8;
            // 
            // btnSelectGxtFilePath
            // 
            this.btnSelectGxtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectGxtFilePath.Location = new System.Drawing.Point(915, 10);
            this.btnSelectGxtFilePath.Name = "btnSelectGxtFilePath";
            this.btnSelectGxtFilePath.Size = new System.Drawing.Size(41, 20);
            this.btnSelectGxtFilePath.TabIndex = 9;
            this.btnSelectGxtFilePath.Text = "...";
            this.btnSelectGxtFilePath.UseVisualStyleBackColor = true;
            this.btnSelectGxtFilePath.Click += new System.EventHandler(this.btnSelectGxtFilePath_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // btnAddMissingEntries
            // 
            this.btnAddMissingEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddMissingEntries.Enabled = false;
            this.btnAddMissingEntries.Location = new System.Drawing.Point(186, 599);
            this.btnAddMissingEntries.Name = "btnAddMissingEntries";
            this.btnAddMissingEntries.Size = new System.Drawing.Size(168, 38);
            this.btnAddMissingEntries.TabIndex = 10;
            this.btnAddMissingEntries.Text = "Добавить отсутствующие элементы с другого GXT";
            this.btnAddMissingEntries.UseVisualStyleBackColor = true;
            this.btnAddMissingEntries.Click += new System.EventHandler(this.btnAddMissingEntries_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(968, 649);
            this.Controls.Add(this.btnAddMissingEntries);
            this.Controls.Add(this.btnSelectGxtFilePath);
            this.Controls.Add(this.txtBoxGxtFilePath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnSaveChanges);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "GTA GXT Editor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListView lstKeys;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colText;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkCaseCheck;
        private System.Windows.Forms.TextBox txtSearchText;
        private System.Windows.Forms.ComboBox cmbSearchColumn;
        private System.Windows.Forms.Button btnNextSearch;
        private System.Windows.Forms.Button btnSaveChanges;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBoxGxtFilePath;
        private System.Windows.Forms.Button btnSelectGxtFilePath;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button btnAddEntry;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnEditEntry;
        private System.Windows.Forms.Button btnDeleteEntry;
        private System.Windows.Forms.CheckBox chkLoopSearch;
        private System.Windows.Forms.Button btnAddMissingEntries;
    }
}

