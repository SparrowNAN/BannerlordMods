using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BannerlordChinesizationSyncTool.file;

namespace BannerlordChinesizationSyncTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            // GamePath
            string gamePath = FileSync.GetGamePath();
            if (!string.IsNullOrEmpty(gamePath))
            {
                GameDirPath.Text = gamePath;
            }
        }

        private void ListBoxAddItem(ListBox checkedListBox, List<Version> versions)
        {
            checkedListBox.DataSource = versions;
            checkedListBox.DisplayMember = "version";   //要显示的属性名
            checkedListBox.ValueMember = "path"; //存储的属性名
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListBoxAddItem(public_list, FileSync.VERSION_INFO.publicVersions);
            ListBoxAddItem(ea_list, FileSync.VERSION_INFO.eaVersions);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // throw new System.NotImplementedException();
        }

        //  选择游戏路径的按钮
        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            DialogResult dialogResult = GamePathDialog.ShowDialog();
            if (dialogResult == DialogResult.OK || dialogResult == DialogResult.Yes)
            {
                string gamePath = GamePathDialog.SelectedPath;
                if (gamePath.EndsWith("Mount & Blade II Bannerlord"))
                {
                    GameDirPath.Text = GamePathDialog.SelectedPath;
                }
                else
                {
                    DialogResult boxResult = MessageBox.Show("检测到您选中的路径并不是以Mount & Blade II Bannerlord结尾，您确定选择的路径正确？",
                        "提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                    if (boxResult == DialogResult.Yes)
                    {
                        GameDirPath.Text = GamePathDialog.SelectedPath;
                    }
                    else
                    {
                        button1_MouseClick(sender, e);
                    }
                }
            }
        }

        //  当文本框中的目录改变时触发
        private void GameDirPath_TextChanged(object sender, EventArgs e)
        {
            string gamePath = GameDirPath.Text;
            if (!string.IsNullOrEmpty(gamePath) 
                && !gamePath.EndsWith("Mount & Blade II Bannerlord") 
                && !gamePath.Equals(FileSync.GetGamePath()))
            {

                DialogResult boxResult = MessageBox.Show("检测到您选中的路径并不是以Mount & Blade II Bannerlord结尾，您确定选择的路径正确？",
                    "提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (boxResult == DialogResult.Yes)
                {
                    FileSync.StorageGamePath(GameDirPath.Text);
                }
            }
            else
            {
                FileSync.StorageGamePath(GameDirPath.Text);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 点击更新版本文件的时候，更新版本
            FileSync.DownloadVersionFile();
            MessageBox.Show("已经获取最新的版本信息");
        }

        /**
         *  公共版本汉化
         */
        private void button2_Click(object sender, EventArgs e)
        {
            Use("public", public_list);
        }

        private void Use(string prefix, ListBox listBox)
        {
            Version row = listBox.SelectedItem as Version;
            if (row != null)
            {
                string downloadPath = row.path;
                string name = row.version + ".zip";
                bool useStatus = FileSync.Use(prefix, name, downloadPath);
                if (useStatus)
                {
                    MessageBox.Show("已经成功使用版本号为 "+row.version+ " 的汉化文件，赶紧去玩吧");   
                }
            }
            else
            {
                MessageBox.Show("确认已经选择了汉化文件的版本？？");
            }
        } 

        private void public_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DisplayDesc(public_list, public_desc);
        }

        private void ea_list_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DisplayDesc(ea_list, ea_desc);
        }

        private void DisplayDesc(ListBox listBox, RichTextBox richTextBox)
        {
            Version row = listBox.SelectedItem as Version;
            if (row != null) richTextBox.Text = row.desc;
        }
        
        /**
         * 清楚本地汉化文件
         */
        private void button4_Click(object sender, EventArgs e)
        {
            string tips = String.Empty;
            foreach (var dir in FileSync.NEED_EMPTY_DIR)
            {
                tips = tips + dir + "\n";
            }

            DialogResult dialogResult = MessageBox.Show("这将会删除游戏的汉化文件，包括\n" + tips + "中的汉化文件", "警告", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                FileSync.DeleteChinesization();   
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Use("ea", ea_list);
        }
    }
}