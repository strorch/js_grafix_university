namespace Lab3
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Button2D = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Button3D = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.SplineNodesNumeric = new System.Windows.Forms.NumericUpDown();
            this.dataGridNodes = new System.Windows.Forms.DataGridView();
            this.Columnt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox3D = new System.Windows.Forms.CheckedListBox();
            this.buttonReturn = new System.Windows.Forms.Button();
            this.buttonReflect = new System.Windows.Forms.Button();
            this.buttonDelate = new System.Windows.Forms.Button();
            this.buttonRotate = new System.Windows.Forms.Button();
            this.buttonTranslate = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplineNodesNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridNodes)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(784, 562);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::Lab3.Properties.Resources.Untitled;
            this.pictureBox1.Location = new System.Drawing.Point(10, 32);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Padding = new System.Windows.Forms.Padding(5);
            this.pictureBox1.Size = new System.Drawing.Size(614, 520);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripSeparator1,
            this.Button2D,
            this.toolStripSeparator2,
            this.Button3D});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(634, 22);
            this.toolStrip2.TabIndex = 2;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(89, 19);
            this.toolStripLabel1.Text = "Зміна режиму:";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 22);
            // 
            // Button2D
            // 
            this.Button2D.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Button2D.Image = ((System.Drawing.Image)(resources.GetObject("Button2D.Image")));
            this.Button2D.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button2D.Name = "Button2D";
            this.Button2D.Size = new System.Drawing.Size(25, 19);
            this.Button2D.Text = "2D";
            this.Button2D.Click += new System.EventHandler(this.Button2D_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 22);
            // 
            // Button3D
            // 
            this.Button3D.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Button3D.Image = ((System.Drawing.Image)(resources.GetObject("Button3D.Image")));
            this.Button3D.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Button3D.Name = "Button3D";
            this.Button3D.Size = new System.Drawing.Size(25, 19);
            this.Button3D.Text = "3D";
            this.Button3D.Click += new System.EventHandler(this.Button3D_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripButton1,
            this.toolStripButton5});
            this.toolStrip1.Location = new System.Drawing.Point(634, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStrip1.Size = new System.Drawing.Size(150, 22);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 19);
            this.toolStripButton3.Text = "X";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 19);
            this.toolStripButton1.Text = "[]";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 19);
            this.toolStripButton5.Text = "_";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.SplineNodesNumeric);
            this.panel1.Controls.Add(this.dataGridNodes);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.checkBox3D);
            this.panel1.Controls.Add(this.buttonReturn);
            this.panel1.Controls.Add(this.buttonReflect);
            this.panel1.Controls.Add(this.buttonDelate);
            this.panel1.Controls.Add(this.buttonRotate);
            this.panel1.Controls.Add(this.buttonTranslate);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(637, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(144, 534);
            this.panel1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 279);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Вузли сплайну";
            // 
            // SplineNodesNumeric
            // 
            this.SplineNodesNumeric.Enabled = false;
            this.SplineNodesNumeric.Location = new System.Drawing.Point(3, 295);
            this.SplineNodesNumeric.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.SplineNodesNumeric.Name = "SplineNodesNumeric";
            this.SplineNodesNumeric.Size = new System.Drawing.Size(138, 20);
            this.SplineNodesNumeric.TabIndex = 8;
            this.SplineNodesNumeric.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.SplineNodesNumeric.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
            // 
            // dataGridNodes
            // 
            this.dataGridNodes.AllowUserToAddRows = false;
            this.dataGridNodes.AllowUserToDeleteRows = false;
            this.dataGridNodes.AllowUserToResizeColumns = false;
            this.dataGridNodes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.dataGridNodes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridNodes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Columnt});
            this.dataGridNodes.Enabled = false;
            this.dataGridNodes.Location = new System.Drawing.Point(3, 321);
            this.dataGridNodes.Name = "dataGridNodes";
            this.dataGridNodes.Size = new System.Drawing.Size(135, 204);
            this.dataGridNodes.TabIndex = 7;
            // 
            // Columnt
            // 
            this.Columnt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Columnt.HeaderText = "t";
            this.Columnt.Name = "Columnt";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Побудова 3D кривої:";
            // 
            // checkBox3D
            // 
            this.checkBox3D.BackColor = System.Drawing.SystemColors.Control;
            this.checkBox3D.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkBox3D.CheckOnClick = true;
            this.checkBox3D.Enabled = false;
            this.checkBox3D.FormattingEnabled = true;
            this.checkBox3D.Items.AddRange(new object[] {
            "Параметрично",
            "Кубічним сплайном"});
            this.checkBox3D.Location = new System.Drawing.Point(6, 242);
            this.checkBox3D.Name = "checkBox3D";
            this.checkBox3D.Size = new System.Drawing.Size(135, 30);
            this.checkBox3D.TabIndex = 5;
            // 
            // buttonReturn
            // 
            this.buttonReturn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonReturn.Location = new System.Drawing.Point(3, 7);
            this.buttonReturn.Name = "buttonReturn";
            this.buttonReturn.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonReturn.Size = new System.Drawing.Size(138, 33);
            this.buttonReturn.TabIndex = 0;
            this.buttonReturn.Text = "Відновити";
            this.buttonReturn.UseVisualStyleBackColor = true;
            this.buttonReturn.Click += new System.EventHandler(this.buttonReturn_Click);
            // 
            // buttonReflect
            // 
            this.buttonReflect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonReflect.Location = new System.Drawing.Point(3, 163);
            this.buttonReflect.Name = "buttonReflect";
            this.buttonReflect.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonReflect.Size = new System.Drawing.Size(138, 33);
            this.buttonReflect.TabIndex = 4;
            this.buttonReflect.Text = "Відобразити";
            this.buttonReflect.UseVisualStyleBackColor = true;
            this.buttonReflect.Click += new System.EventHandler(this.buttonReflect_Click);
            // 
            // buttonDelate
            // 
            this.buttonDelate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonDelate.Location = new System.Drawing.Point(3, 124);
            this.buttonDelate.Name = "buttonDelate";
            this.buttonDelate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonDelate.Size = new System.Drawing.Size(138, 33);
            this.buttonDelate.TabIndex = 3;
            this.buttonDelate.Text = "Розтягнути/ Стиснути";
            this.buttonDelate.UseVisualStyleBackColor = true;
            this.buttonDelate.Click += new System.EventHandler(this.buttonDelate_Click);
            // 
            // buttonRotate
            // 
            this.buttonRotate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonRotate.Location = new System.Drawing.Point(3, 85);
            this.buttonRotate.Name = "buttonRotate";
            this.buttonRotate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonRotate.Size = new System.Drawing.Size(138, 33);
            this.buttonRotate.TabIndex = 2;
            this.buttonRotate.Text = "Повернути";
            this.buttonRotate.UseVisualStyleBackColor = true;
            this.buttonRotate.Click += new System.EventHandler(this.buttonRotate_Click);
            // 
            // buttonTranslate
            // 
            this.buttonTranslate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonTranslate.Location = new System.Drawing.Point(3, 46);
            this.buttonTranslate.Name = "buttonTranslate";
            this.buttonTranslate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonTranslate.Size = new System.Drawing.Size(138, 33);
            this.buttonTranslate.TabIndex = 1;
            this.buttonTranslate.Text = "Зсунути";
            this.buttonTranslate.UseVisualStyleBackColor = true;
            this.buttonTranslate.Click += new System.EventHandler(this.buttonTranslate_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CG: Splines and curves";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplineNodesNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridNodes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton Button2D;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton Button3D;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonReturn;
        private System.Windows.Forms.Button buttonReflect;
        private System.Windows.Forms.Button buttonDelate;
        private System.Windows.Forms.Button buttonRotate;
        private System.Windows.Forms.Button buttonTranslate;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox checkBox3D;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown SplineNodesNumeric;
        private System.Windows.Forms.DataGridView dataGridNodes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columnt;
    }
}

