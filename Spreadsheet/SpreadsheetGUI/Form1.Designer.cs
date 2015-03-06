namespace SpreadsheetGUI
{
    partial class Form1
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
            this.spreadsheetPanel1 = new SS.SpreadsheetPanel();
            this.contents = new System.Windows.Forms.TextBox();
            this.addButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BoxCellName = new System.Windows.Forms.TextBox();
            this.BoxCellContent = new System.Windows.Forms.TextBox();
            this.LabelName = new System.Windows.Forms.Label();
            this.LabelContent = new System.Windows.Forms.Label();
            this.LabelValue = new System.Windows.Forms.Label();
            this.BoxCellValue = new System.Windows.Forms.TextBox();
            this.Evaluate = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // spreadsheetPanel1
            // 
            this.spreadsheetPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spreadsheetPanel1.Location = new System.Drawing.Point(0, 41);
            this.spreadsheetPanel1.Name = "spreadsheetPanel1";
            this.spreadsheetPanel1.Size = new System.Drawing.Size(898, 546);
            this.spreadsheetPanel1.TabIndex = 0;
            // 
            // contents
            // 
            this.contents.Location = new System.Drawing.Point(689, 14);
            this.contents.Name = "contents";
            this.contents.Size = new System.Drawing.Size(100, 20);
            this.contents.TabIndex = 1;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(795, 12);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 23);
            this.addButton.TabIndex = 2;
            this.addButton.Text = "ADD";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(899, 27);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.closeToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(41, 23);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.saveAsToolStripMenuItem.Text = "Save as";
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(123, 24);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // BoxCellName
            // 
            this.BoxCellName.Location = new System.Drawing.Point(148, 14);
            this.BoxCellName.Name = "BoxCellName";
            this.BoxCellName.ReadOnly = true;
            this.BoxCellName.Size = new System.Drawing.Size(64, 20);
            this.BoxCellName.TabIndex = 4;
            // 
            // BoxCellContent
            // 
            this.BoxCellContent.Location = new System.Drawing.Point(281, 14);
            this.BoxCellContent.Name = "BoxCellContent";
            this.BoxCellContent.Size = new System.Drawing.Size(133, 20);
            this.BoxCellContent.TabIndex = 5;
            // 
            // LabelName
            // 
            this.LabelName.AutoSize = true;
            this.LabelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelName.Location = new System.Drawing.Point(70, 15);
            this.LabelName.Name = "LabelName";
            this.LabelName.Size = new System.Drawing.Size(72, 17);
            this.LabelName.TabIndex = 6;
            this.LabelName.Text = "Cell Name";
            // 
            // LabelContent
            // 
            this.LabelContent.AutoSize = true;
            this.LabelContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelContent.Location = new System.Drawing.Point(218, 15);
            this.LabelContent.Name = "LabelContent";
            this.LabelContent.Size = new System.Drawing.Size(57, 17);
            this.LabelContent.TabIndex = 7;
            this.LabelContent.Text = "Content";
            // 
            // LabelValue
            // 
            this.LabelValue.AutoSize = true;
            this.LabelValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelValue.Location = new System.Drawing.Point(522, 15);
            this.LabelValue.Name = "LabelValue";
            this.LabelValue.Size = new System.Drawing.Size(44, 17);
            this.LabelValue.TabIndex = 8;
            this.LabelValue.Text = "Value";
            // 
            // BoxCellValue
            // 
            this.BoxCellValue.Location = new System.Drawing.Point(572, 14);
            this.BoxCellValue.Name = "BoxCellValue";
            this.BoxCellValue.Size = new System.Drawing.Size(111, 20);
            this.BoxCellValue.TabIndex = 9;
            // 
            // Evaluate
            // 
            this.Evaluate.Location = new System.Drawing.Point(420, 12);
            this.Evaluate.Name = "Evaluate";
            this.Evaluate.Size = new System.Drawing.Size(99, 22);
            this.Evaluate.TabIndex = 10;
            this.Evaluate.Text = "Evaluate";
            this.Evaluate.UseVisualStyleBackColor = true;
            this.Evaluate.Click += new System.EventHandler(this.Evaluate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 590);
            this.Controls.Add(this.Evaluate);
            this.Controls.Add(this.BoxCellValue);
            this.Controls.Add(this.LabelValue);
            this.Controls.Add(this.LabelContent);
            this.Controls.Add(this.LabelName);
            this.Controls.Add(this.BoxCellContent);
            this.Controls.Add(this.BoxCellName);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.contents);
            this.Controls.Add(this.spreadsheetPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Spreadsheet";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SS.SpreadsheetPanel spreadsheetPanel1;
        private System.Windows.Forms.TextBox contents;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.TextBox BoxCellName;
        private System.Windows.Forms.TextBox BoxCellContent;
        private System.Windows.Forms.Label LabelName;
        private System.Windows.Forms.Label LabelContent;
        private System.Windows.Forms.Label LabelValue;
        private System.Windows.Forms.TextBox BoxCellValue;
        private System.Windows.Forms.Button Evaluate;
    }
}

