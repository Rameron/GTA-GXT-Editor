
namespace GTA_GXT_Editor.Forms
{
    partial class GXTEntryEditor
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxValue = new System.Windows.Forms.TextBox();
            this.txtBoxName = new System.Windows.Forms.TextBox();
            this.btnProcessEntry = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.comboTables = new System.Windows.Forms.ComboBox();
            this.lblTableCaption = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Текст:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Название:";
            // 
            // txtBoxValue
            // 
            this.txtBoxValue.Location = new System.Drawing.Point(81, 37);
            this.txtBoxValue.Name = "txtBoxValue";
            this.txtBoxValue.Size = new System.Drawing.Size(270, 20);
            this.txtBoxValue.TabIndex = 13;
            // 
            // txtBoxName
            // 
            this.txtBoxName.Location = new System.Drawing.Point(81, 13);
            this.txtBoxName.Name = "txtBoxName";
            this.txtBoxName.Size = new System.Drawing.Size(270, 20);
            this.txtBoxName.TabIndex = 12;
            // 
            // btnProcessEntry
            // 
            this.btnProcessEntry.Location = new System.Drawing.Point(18, 93);
            this.btnProcessEntry.Name = "btnProcessEntry";
            this.btnProcessEntry.Size = new System.Drawing.Size(333, 33);
            this.btnProcessEntry.TabIndex = 11;
            this.btnProcessEntry.Text = "Добавить ключ";
            this.btnProcessEntry.UseVisualStyleBackColor = true;
            this.btnProcessEntry.Click += new System.EventHandler(this.btnProcessEntry_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(18, 132);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(333, 33);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // comboTables
            // 
            this.comboTables.FormattingEnabled = true;
            this.comboTables.Location = new System.Drawing.Point(81, 62);
            this.comboTables.Name = "comboTables";
            this.comboTables.Size = new System.Drawing.Size(270, 21);
            this.comboTables.TabIndex = 17;
            // 
            // lblTableCaption
            // 
            this.lblTableCaption.AutoSize = true;
            this.lblTableCaption.Location = new System.Drawing.Point(15, 65);
            this.lblTableCaption.Name = "lblTableCaption";
            this.lblTableCaption.Size = new System.Drawing.Size(53, 13);
            this.lblTableCaption.TabIndex = 18;
            this.lblTableCaption.Text = "Таблица:";
            // 
            // GXTEntryEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 176);
            this.Controls.Add(this.lblTableCaption);
            this.Controls.Add(this.comboTables);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBoxValue);
            this.Controls.Add(this.txtBoxName);
            this.Controls.Add(this.btnProcessEntry);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "GXTEntryEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "GXTEntryEditor";
            this.Load += new System.EventHandler(this.GXTEntryEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxValue;
        private System.Windows.Forms.TextBox txtBoxName;
        private System.Windows.Forms.Button btnProcessEntry;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox comboTables;
        private System.Windows.Forms.Label lblTableCaption;
    }
}