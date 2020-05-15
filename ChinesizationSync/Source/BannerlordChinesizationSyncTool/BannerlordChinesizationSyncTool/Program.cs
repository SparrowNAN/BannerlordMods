using System;
using System.Windows.Forms;
using BannerlordChinesizationSyncTool.file;

namespace BannerlordChinesizationSyncTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 创建、检查相关文件夹
            FileSync.CreateDir();
            // check,备份,download version.json文件
            // FileSync.CheckVersionFile();
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}