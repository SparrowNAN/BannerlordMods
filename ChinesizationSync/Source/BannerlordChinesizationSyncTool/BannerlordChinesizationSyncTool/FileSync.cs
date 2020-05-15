using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;
using Newtonsoft.Json;

namespace BannerlordChinesizationSyncTool.file
{
    internal class FileSync
    {
        public static string CURRENT_RUN_DIR = Directory.GetCurrentDirectory();
        public static string TEMP_DIR_NAME = "banerlord_temp";
        public static string EA_DIR_NAME = "ea";
        public static string PUBLIC_DIR_NAME = "public";
        public static string PUBLIC_DIR_PATH = BASE_PATH + SPARATORS + PUBLIC_DIR_NAME + SPARATORS;
        public static string EA_DIR_PATH = BASE_PATH + SPARATORS + EA_DIR_PATH + SPARATORS;
        public static string BACK_DIR = "back";
        public static string VERSION_FILE_NAME = "version.json";
        public static string SPARATORS = "/";
        public static string BASE_PATH = CURRENT_RUN_DIR + SPARATORS + TEMP_DIR_NAME;
        public static WebClient webClient = new WebClient();
        public static string VERSION_URL = "http://bannerlord.wangyanan.online/Localization/version.json";
        public static string VERSION_BACK_FILE_PATH = BASE_PATH + SPARATORS + BACK_DIR + SPARATORS + VERSION_FILE_NAME;
        public static string VERSION_FILE_PATH = BASE_PATH + SPARATORS + VERSION_FILE_NAME;
        public static string GAME_PATH_FILE = BASE_PATH + SPARATORS + "path.json";
        public static VersionInfo VERSION_INFO;
        
        public static List<string> NEED_EMPTY_DIR = new List<string>(5)
        {
            "/Modules/Native/ModuleData/Languages/CNs",
            "/Modules/Native/ModuleData/Languages/CNt",
            "/Modules/SandBox/ModuleData/Languages/CNs",
            "/Modules/SandBox/ModuleData/Languages/CNt",
            "/Modules/SandBoxCore/ModuleData/Languages/CNs",
            "/Modules/SandBoxCore/ModuleData/Languages/CNt",
            "/Modules/StoryMode/ModuleData/Languages/CNs",
            "/Modules/StoryMode/ModuleData/Languages/CNt"
        };

        public static string GetGamePath()
        {
            FileInfo gamePathInfo = new FileInfo(GAME_PATH_FILE);
            return gamePathInfo.Exists ? File.ReadAllText(GAME_PATH_FILE, Encoding.GetEncoding("utf-8")) : "";
        }

        public static void StorageGamePath(string pathValue)
        {
            FileInfo gamePathInfo = new FileInfo(GAME_PATH_FILE);
            if (gamePathInfo.Exists)
            {
                gamePathInfo.Delete();
            }
            File.WriteAllText(GAME_PATH_FILE, pathValue, Encoding.UTF8);
        }

        public static string ReadVersionFile()
        {
            try
            {
                return File.ReadAllText(VERSION_FILE_PATH, Encoding.GetEncoding("utf-8"));
            }
            catch (FileNotFoundException e)
            {
                return "";
            }
        }

        public static VersionInfo DeserializationVersionFile()
        {
            string jsonString = ReadVersionFile();
            VersionInfo versionInfo = JsonConvert.DeserializeObject<VersionInfo>(jsonString);
            versionInfo.publicVersions.Sort((x , y) => -x.sort.CompareTo(y.sort));
            versionInfo.eaVersions.Sort((x , y) => -x.sort.CompareTo(y.sort));
            return versionInfo;
            // VERSION_INFO = versionInfo;
        }

        public static void BackVersionFile(FileInfo versionFile)
        {
            if (versionFile.Exists)
            {
                FileInfo backFile = new FileInfo(VERSION_BACK_FILE_PATH);
                if (backFile.Exists)
                {
                    backFile.Delete();
                }
                versionFile.CopyTo(VERSION_BACK_FILE_PATH);
            }
        }

        public static void CheckVersionFile()
        {
            FileInfo versionFileInfo = new FileInfo(VERSION_FILE_PATH);
            // not exist or expire
            if (!versionFileInfo.Exists || versionFileInfo.CreationTimeUtc.AddDays(1).CompareTo(DateTime.UtcNow) < 0)
            {
                BackVersionFile(versionFileInfo);
                DownloadVersionFile(versionFileInfo);
            }
            VersionInfo versionInfo = DeserializationVersionFile();
            VERSION_INFO = versionInfo;
        }

        private static bool DownloadVersionFile(FileInfo versionFileInfo)
        {
            if (versionFileInfo.Exists)
            {
                versionFileInfo.Delete();
            }

            try
            {
                webClient.DownloadFile(VERSION_URL, VERSION_FILE_PATH);
            }
            catch (Exception e)
            {
                MessageBox.Show("下载版本文件出错，异常信息:" + e.Message, "错误提示");
                return false;
            }

            return true;
        }
        
        public static VersionInfo DownloadVersionFile()
        {
            FileInfo fileInfo = new FileInfo(VERSION_FILE_PATH);
            if (!DownloadVersionFile(fileInfo))
            {
                return null;
            }
            VersionInfo info = DeserializationVersionFile();
            return info;
        }

        public static void CreateDir()
        {
            string basePath = BASE_PATH;
            string eaPath = basePath + SPARATORS + EA_DIR_NAME;
            string publicPath = basePath + SPARATORS + PUBLIC_DIR_NAME;
            string backPath = basePath + SPARATORS + BACK_DIR;
            DirectoryInfo basePathDir = new DirectoryInfo(basePath);
            DirectoryInfo eaPathDir = new DirectoryInfo(eaPath);
            DirectoryInfo publicPathDir = new DirectoryInfo(publicPath);
            DirectoryInfo backPathDir = new DirectoryInfo(backPath);
            if (!basePathDir.Exists)
            {
                basePathDir.Create();
            }
            if (!eaPathDir.Exists)
            {
                eaPathDir.Create();
            }
            if (!publicPathDir.Exists)
            {
                publicPathDir.Create();
            }
            if (!backPathDir.Exists)
            {
                backPathDir.Create();
            }
        }

        public static void DeleteChinesization()
        {
            string gamePath = GetGamePath();
            gamePath = gamePath.EndsWith("/") ? gamePath : gamePath + "/";
            foreach (var s in NEED_EMPTY_DIR)
            {
               DeleteFolder(gamePath + s); 
            }
        }

        public static Boolean Use(string prefix, string fileName, string path)
        {
            string filePath = GetVersionFilePath(prefix, fileName);
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("汉化文件路径错误，请到论坛相应帖子中反馈");
                return false;
            }
            string gamePath = GetGamePath();
            if (string.IsNullOrEmpty(gamePath))
            {
                
                MessageBox.Show("未选择游戏路径，请重新选择");
                return false;
                
            }
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Exists)
                {
                    if (fileInfo.Length < 100)
                    {
                        fileInfo.Delete();
                        if (!DownloadChinesizationFile(path, filePath))
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    if (!DownloadChinesizationFile(path, filePath))
                    {
                        return false;
                    }
                }
                UnZip(filePath, gamePath, null);
            }
            catch (Exception e)
            {
                MessageBox.Show("使用汉化文件出错，请尝试其它汉化文件，或到论坛相应帖子中反馈。异常信息:" + e.Message, "错误提示");
                return false;
            }

            return true;
        }
        
        public static bool UnZip(string fileToUnZip, string zipedFolder, string password)
        {
            bool result = true;
            FileStream fs = null;
            ZipInputStream zipStream = null;
            ZipEntry ent = null;
            string fileName;

            if (!File.Exists(fileToUnZip))
                return false;

            if (!Directory.Exists(zipedFolder))
                Directory.CreateDirectory(zipedFolder);

            try
            {
                zipStream = new ZipInputStream(File.OpenRead(fileToUnZip));
                if (!string.IsNullOrEmpty(password)) zipStream.Password = password;
                while ((ent = zipStream.GetNextEntry()) != null)
                {
                    if (!string.IsNullOrEmpty(ent.Name))
                    {
                        fileName = Path.Combine(zipedFolder, ent.Name);
                        // fileName = fileName.Replace('/', '\\');//change by Mr.HopeGi   

                        if (fileName.EndsWith("/"))
                        {
                            Directory.CreateDirectory(fileName);
                            continue;
                        }

                        fs = File.Create(fileName);
                        int size = 2048;
                        byte[] data = new byte[size];
                        while (true)
                        {
                            size = zipStream.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                fs.Write(data, 0, size);
                            }
                            else
                                break;
                        }
                    }
                }
            }
            catch
            {
                result = false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }

                if (zipStream != null)
                {
                    zipStream.Close();
                    zipStream.Dispose();
                }

                if (ent != null)
                {
                    ent = null;
                }

                GC.Collect();
                GC.Collect(1);
            }

            return result;
        }

        private static bool DownloadChinesizationFile(string path, string filePath)
        {
            try
            {
                webClient.DownloadFile(path, filePath);
            }
            catch (Exception e)
            {
                MessageBox.Show("下载汉化文件出错，异常信息:" + e.Message, "错误提示");
                return false;
            }

            return true;
        }

        private static string GetVersionFilePath(string prefix, string fileName)
        {
            if (prefix == "public")
            {
                return BASE_PATH + SPARATORS + PUBLIC_DIR_PATH + SPARATORS + fileName;
            }

            if (prefix == "ea")
            {
                return BASE_PATH + SPARATORS + EA_DIR_PATH + SPARATORS + fileName;
            }
            return String.Empty;
        }
        
        private static void DeleteFolder(string dir)
        {
            if (Directory.Exists(dir))
            {
                string[] fileSystemEntries = Directory.GetFileSystemEntries(dir);
                for (int i = 0; i < fileSystemEntries.Length; i++)
                {
                    string text = fileSystemEntries[i];
                    if (File.Exists(text))
                    {
                        File.Delete(text);
                    }
                    else
                    {
                        DeleteFolder(text);
                    }
                }

                Directory.Delete(dir);
            }
        }
    }
}