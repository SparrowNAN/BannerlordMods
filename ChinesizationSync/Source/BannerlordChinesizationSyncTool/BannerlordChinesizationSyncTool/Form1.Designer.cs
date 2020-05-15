namespace BannerlordChinesizationSyncTool
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
            this.label1 = new System.Windows.Forms.Label();
            this.GameDirPath = new System.Windows.Forms.TextBox();
            this.SelectGamePathButton = new System.Windows.Forms.Button();
            this.GamePathDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.public_desc = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ea_desc = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.ea_list = new System.Windows.Forms.ListBox();
            this.public_list = new System.Windows.Forms.ListBox();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.label1.Location = new System.Drawing.Point(62, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "游戏目录";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // GameDirPath
            // 
            this.GameDirPath.CausesValidation = false;
            this.GameDirPath.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.GameDirPath.Location = new System.Drawing.Point(199, 84);
            this.GameDirPath.Name = "GameDirPath";
            this.GameDirPath.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.GameDirPath.Size = new System.Drawing.Size(711, 35);
            this.GameDirPath.TabIndex = 1;
            this.GameDirPath.TextChanged += new System.EventHandler(this.GameDirPath_TextChanged);
            // 
            // SelectGamePathButton
            // 
            this.SelectGamePathButton.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.SelectGamePathButton.Location = new System.Drawing.Point(954, 84);
            this.SelectGamePathButton.Name = "SelectGamePathButton";
            this.SelectGamePathButton.Size = new System.Drawing.Size(159, 35);
            this.SelectGamePathButton.TabIndex = 2;
            this.SelectGamePathButton.Text = "选择目录";
            this.SelectGamePathButton.UseVisualStyleBackColor = true;
            this.SelectGamePathButton.MouseClick += new System.Windows.Forms.MouseEventHandler(this.button1_MouseClick);
            // 
            // GamePathDialog
            // 
            this.GamePathDialog.Description = "请选择游戏目录";
            this.GamePathDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.label2.Location = new System.Drawing.Point(21, 266);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(238, 26);
            this.label2.TabIndex = 3;
            this.label2.Text = "公共版汉化文件";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.button1.Location = new System.Drawing.Point(62, 180);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(221, 38);
            this.button1.TabIndex = 4;
            this.button1.Text = "获取最新汉化文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // public_desc
            // 
            this.public_desc.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.public_desc.Location = new System.Drawing.Point(273, 338);
            this.public_desc.Name = "public_desc";
            this.public_desc.Size = new System.Drawing.Size(277, 304);
            this.public_desc.TabIndex = 6;
            this.public_desc.Text = "";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.label3.Location = new System.Drawing.Point(589, 266);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(238, 26);
            this.label3.TabIndex = 7;
            this.label3.Text = "测试版汉化文件";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ea_desc
            // 
            this.ea_desc.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.ea_desc.Location = new System.Drawing.Point(851, 338);
            this.ea_desc.Name = "ea_desc";
            this.ea_desc.Size = new System.Drawing.Size(279, 304);
            this.ea_desc.TabIndex = 12;
            this.ea_desc.Text = "";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.button2.Location = new System.Drawing.Point(62, 682);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(221, 38);
            this.button2.TabIndex = 14;
            this.button2.Text = "使用选中的公共版汉化文件";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.button3.Location = new System.Drawing.Point(640, 682);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(221, 38);
            this.button3.TabIndex = 15;
            this.button3.Text = "使用选中的测试版汉化文件";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ea_list
            // 
            this.ea_list.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.ea_list.FormattingEnabled = true;
            this.ea_list.ItemHeight = 25;
            this.ea_list.Location = new System.Drawing.Point(640, 338);
            this.ea_list.Name = "ea_list";
            this.ea_list.Size = new System.Drawing.Size(205, 304);
            this.ea_list.TabIndex = 16;
            this.ea_list.SelectedIndexChanged += new System.EventHandler(this.ea_list_SelectedIndexChanged);
            // 
            // public_list
            // 
            this.public_list.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.public_list.FormattingEnabled = true;
            this.public_list.ItemHeight = 25;
            this.public_list.Location = new System.Drawing.Point(62, 338);
            this.public_list.Name = "public_list";
            this.public_list.Size = new System.Drawing.Size(205, 304);
            this.public_list.TabIndex = 17;
            this.public_list.SelectedIndexChanged += new System.EventHandler(this.public_list_SelectedIndexChanged);
            // 
            // button4
            // 
            this.button4.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.button4.Location = new System.Drawing.Point(347, 180);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(248, 38);
            this.button4.TabIndex = 18;
            this.button4.Text = "清除当前游戏路径的汉化";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 789);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.public_list);
            this.Controls.Add(this.ea_list);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.ea_desc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.public_desc);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SelectGamePathButton);
            this.Controls.Add(this.GameDirPath);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte) (134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "霸主汉化同步工具";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.RichTextBox ea_desc;
        private System.Windows.Forms.ListBox ea_list;
        public System.Windows.Forms.TextBox GameDirPath;
        private System.Windows.Forms.FolderBrowserDialog GamePathDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox public_desc;
        private System.Windows.Forms.ListBox public_list;
        private System.Windows.Forms.Button SelectGamePathButton;

        #endregion
    }
}