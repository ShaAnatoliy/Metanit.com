namespace SQLite_Project
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
            this.components = new System.ComponentModel.Container();
            this.textBoxNameDB = new System.Windows.Forms.TextBox();
            this.buttonCreateDB = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxNameTable = new System.Windows.Forms.TextBox();
            this.buttonCreateTable = new System.Windows.Forms.Button();
            this.textBoxImg = new System.Windows.Forms.TextBox();
            this.buttonBrowseImg = new System.Windows.Forms.Button();
            this.buttonAddImg = new System.Windows.Forms.Button();
            this.textBoxFile = new System.Windows.Forms.TextBox();
            this.buttonBrowseFile = new System.Windows.Forms.Button();
            this.buttonAddFile = new System.Windows.Forms.Button();
            this.openFileDialogImg = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialogFile = new System.Windows.Forms.OpenFileDialog();
            this.buttonViewImg = new System.Windows.Forms.Button();
            this.buttonViewFile = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.buttonUpdateData = new System.Windows.Forms.Button();
            this.buttonDeleteData = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxUpdateData = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxDeleteData = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxNameDB
            // 
            this.textBoxNameDB.Location = new System.Drawing.Point(200, 21);
            this.textBoxNameDB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxNameDB.Name = "textBoxNameDB";
            this.textBoxNameDB.Size = new System.Drawing.Size(204, 22);
            this.textBoxNameDB.TabIndex = 0;
            this.textBoxNameDB.Text = "dbTest";
            // 
            // buttonCreateDB
            // 
            this.buttonCreateDB.Location = new System.Drawing.Point(489, 21);
            this.buttonCreateDB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonCreateDB.Name = "buttonCreateDB";
            this.buttonCreateDB.Size = new System.Drawing.Size(188, 28);
            this.buttonCreateDB.TabIndex = 1;
            this.buttonCreateDB.Text = "Создать базу данных";
            this.buttonCreateDB.UseVisualStyleBackColor = true;
            this.buttonCreateDB.Click += new System.EventHandler(this.buttonCreateDB_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Имя базы данных:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(405, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = ".db";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 82);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Имя таблицы:";
            // 
            // textBoxNameTable
            // 
            this.textBoxNameTable.Location = new System.Drawing.Point(200, 79);
            this.textBoxNameTable.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxNameTable.Name = "textBoxNameTable";
            this.textBoxNameTable.Size = new System.Drawing.Size(204, 22);
            this.textBoxNameTable.TabIndex = 5;
            this.textBoxNameTable.Text = "tableTest";
            // 
            // buttonCreateTable
            // 
            this.buttonCreateTable.Location = new System.Drawing.Point(489, 79);
            this.buttonCreateTable.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonCreateTable.Name = "buttonCreateTable";
            this.buttonCreateTable.Size = new System.Drawing.Size(188, 28);
            this.buttonCreateTable.TabIndex = 6;
            this.buttonCreateTable.Text = "Создать таблицу";
            this.buttonCreateTable.UseVisualStyleBackColor = true;
            this.buttonCreateTable.Click += new System.EventHandler(this.buttonCreateTable_Click);
            // 
            // textBoxImg
            // 
            this.textBoxImg.Location = new System.Drawing.Point(200, 140);
            this.textBoxImg.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxImg.Name = "textBoxImg";
            this.textBoxImg.ReadOnly = true;
            this.textBoxImg.Size = new System.Drawing.Size(476, 22);
            this.textBoxImg.TabIndex = 7;
            // 
            // buttonBrowseImg
            // 
            this.buttonBrowseImg.Location = new System.Drawing.Point(685, 138);
            this.buttonBrowseImg.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonBrowseImg.Name = "buttonBrowseImg";
            this.buttonBrowseImg.Size = new System.Drawing.Size(92, 28);
            this.buttonBrowseImg.TabIndex = 8;
            this.buttonBrowseImg.Text = "Выбрать";
            this.buttonBrowseImg.UseVisualStyleBackColor = true;
            this.buttonBrowseImg.Click += new System.EventHandler(this.buttonBrowseImg_Click);
            // 
            // buttonAddImg
            // 
            this.buttonAddImg.Location = new System.Drawing.Point(785, 137);
            this.buttonAddImg.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonAddImg.Name = "buttonAddImg";
            this.buttonAddImg.Size = new System.Drawing.Size(159, 28);
            this.buttonAddImg.TabIndex = 9;
            this.buttonAddImg.Text = "Добавить в базу";
            this.buttonAddImg.UseVisualStyleBackColor = true;
            this.buttonAddImg.Click += new System.EventHandler(this.buttonAddImg_Click);
            // 
            // textBoxFile
            // 
            this.textBoxFile.Location = new System.Drawing.Point(200, 196);
            this.textBoxFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxFile.Name = "textBoxFile";
            this.textBoxFile.ReadOnly = true;
            this.textBoxFile.Size = new System.Drawing.Size(476, 22);
            this.textBoxFile.TabIndex = 10;
            // 
            // buttonBrowseFile
            // 
            this.buttonBrowseFile.Location = new System.Drawing.Point(685, 193);
            this.buttonBrowseFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonBrowseFile.Name = "buttonBrowseFile";
            this.buttonBrowseFile.Size = new System.Drawing.Size(92, 28);
            this.buttonBrowseFile.TabIndex = 11;
            this.buttonBrowseFile.Text = "Выбрать";
            this.buttonBrowseFile.UseVisualStyleBackColor = true;
            this.buttonBrowseFile.Click += new System.EventHandler(this.buttonBrowseFile_Click);
            // 
            // buttonAddFile
            // 
            this.buttonAddFile.Location = new System.Drawing.Point(785, 193);
            this.buttonAddFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonAddFile.Name = "buttonAddFile";
            this.buttonAddFile.Size = new System.Drawing.Size(159, 28);
            this.buttonAddFile.TabIndex = 12;
            this.buttonAddFile.Text = "Добавить в базу";
            this.buttonAddFile.UseVisualStyleBackColor = true;
            this.buttonAddFile.Click += new System.EventHandler(this.buttonAddFile_Click);
            // 
            // buttonViewImg
            // 
            this.buttonViewImg.Location = new System.Drawing.Point(959, 137);
            this.buttonViewImg.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonViewImg.Name = "buttonViewImg";
            this.buttonViewImg.Size = new System.Drawing.Size(159, 28);
            this.buttonViewImg.TabIndex = 13;
            this.buttonViewImg.Text = "Просмотреть";
            this.buttonViewImg.UseVisualStyleBackColor = true;
            this.buttonViewImg.Click += new System.EventHandler(this.buttonViewImg_Click);
            // 
            // buttonViewFile
            // 
            this.buttonViewFile.Location = new System.Drawing.Point(959, 193);
            this.buttonViewFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonViewFile.Name = "buttonViewFile";
            this.buttonViewFile.Size = new System.Drawing.Size(159, 28);
            this.buttonViewFile.TabIndex = 14;
            this.buttonViewFile.Text = "Просмотреть";
            this.buttonViewFile.UseVisualStyleBackColor = true;
            this.buttonViewFile.Click += new System.EventHandler(this.buttonViewFile_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 144);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 17);
            this.label4.TabIndex = 15;
            this.label4.Text = "Изображение:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 199);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 17);
            this.label5.TabIndex = 16;
            this.label5.Text = "Файл:";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(327, 424);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(385, 20);
            this.label6.TabIndex = 17;
            this.label6.Text = "Заполните все данные (поля) на форме";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonUpdateData
            // 
            this.buttonUpdateData.Location = new System.Drawing.Point(489, 258);
            this.buttonUpdateData.Margin = new System.Windows.Forms.Padding(4);
            this.buttonUpdateData.Name = "buttonUpdateData";
            this.buttonUpdateData.Size = new System.Drawing.Size(187, 28);
            this.buttonUpdateData.TabIndex = 19;
            this.buttonUpdateData.Text = "Обновить данные";
            this.buttonUpdateData.UseVisualStyleBackColor = true;
            this.buttonUpdateData.Click += new System.EventHandler(this.buttonUpdateData_Click);
            // 
            // buttonDeleteData
            // 
            this.buttonDeleteData.Location = new System.Drawing.Point(489, 324);
            this.buttonDeleteData.Margin = new System.Windows.Forms.Padding(4);
            this.buttonDeleteData.Name = "buttonDeleteData";
            this.buttonDeleteData.Size = new System.Drawing.Size(187, 28);
            this.buttonDeleteData.TabIndex = 20;
            this.buttonDeleteData.Text = "Удалить данные";
            this.buttonDeleteData.UseVisualStyleBackColor = true;
            this.buttonDeleteData.Click += new System.EventHandler(this.buttonDeleteData_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 264);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(274, 17);
            this.label7.TabIndex = 21;
            this.label7.Text = "ID записи, которую надо обновить в БД:";
            // 
            // textBoxUpdateData
            // 
            this.textBoxUpdateData.Location = new System.Drawing.Point(298, 261);
            this.textBoxUpdateData.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxUpdateData.Name = "textBoxUpdateData";
            this.textBoxUpdateData.Size = new System.Drawing.Size(167, 22);
            this.textBoxUpdateData.TabIndex = 22;
            this.textBoxUpdateData.Text = "1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 330);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(266, 17);
            this.label8.TabIndex = 23;
            this.label8.Text = "ID записи, которую надо удалить в БД:";
            // 
            // textBoxDeleteData
            // 
            this.textBoxDeleteData.Location = new System.Drawing.Point(298, 327);
            this.textBoxDeleteData.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxDeleteData.Name = "textBoxDeleteData";
            this.textBoxDeleteData.Size = new System.Drawing.Size(167, 22);
            this.textBoxDeleteData.TabIndex = 24;
            this.textBoxDeleteData.Text = "1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1133, 453);
            this.Controls.Add(this.textBoxDeleteData);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxUpdateData);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.buttonDeleteData);
            this.Controls.Add(this.buttonUpdateData);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonViewFile);
            this.Controls.Add(this.buttonViewImg);
            this.Controls.Add(this.buttonAddFile);
            this.Controls.Add(this.buttonBrowseFile);
            this.Controls.Add(this.textBoxFile);
            this.Controls.Add(this.buttonAddImg);
            this.Controls.Add(this.buttonBrowseImg);
            this.Controls.Add(this.textBoxImg);
            this.Controls.Add(this.buttonCreateTable);
            this.Controls.Add(this.textBoxNameTable);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonCreateDB);
            this.Controls.Add(this.textBoxNameDB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxNameDB;
        private System.Windows.Forms.Button buttonCreateDB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxNameTable;
        private System.Windows.Forms.Button buttonCreateTable;
        private System.Windows.Forms.TextBox textBoxImg;
        private System.Windows.Forms.Button buttonBrowseImg;
        private System.Windows.Forms.Button buttonAddImg;
        private System.Windows.Forms.TextBox textBoxFile;
        private System.Windows.Forms.Button buttonBrowseFile;
        private System.Windows.Forms.Button buttonAddFile;
        private System.Windows.Forms.OpenFileDialog openFileDialogImg;
        private System.Windows.Forms.OpenFileDialog openFileDialogFile;
        private System.Windows.Forms.Button buttonViewImg;
        private System.Windows.Forms.Button buttonViewFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttonUpdateData;
        private System.Windows.Forms.Button buttonDeleteData;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxUpdateData;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxDeleteData;
    }
}

