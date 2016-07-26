namespace Cure.Utils
{
    using System.IO;
    using System.Web;
    using System.Web.UI;

    public static class FileUtils
    {
        public static void DeleteFolder(string folderName)
        {
            if (Directory.Exists(folderName))
            {
                string[] files = Directory.GetFiles(folderName);
                string[] dirs = Directory.GetDirectories(folderName);

                foreach (string file in files)
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                }

                foreach (string dir in dirs)
                {
                    DeleteFolder(dir);
                }

                Directory.Delete(folderName, true);
            }

        }

        public static void DeleteFileFromSubfolders(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            DeleteFromSubdirectories(fileInfo.Directory, fileInfo.Name);
        }

        public static void CreateFolderIfNotExists(HttpServerUtilityBase server, string folder)
        {
            var f = server.MapPath(folder);
            if (!Directory.Exists(f))
            {
                Directory.CreateDirectory(f);
            }
        }

        private static void DeleteFromSubdirectories(DirectoryInfo directory, string fileName)
        {
            if (directory.Exists)
            {
                foreach (DirectoryInfo subDir in directory.GetDirectories())
                {
                    DeleteFromSubdirectories(subDir, fileName);
                    DeleteFile(subDir, fileName);
                }
                DeleteFile(directory, fileName);
            }
        }

        public static void DeleteFile(DirectoryInfo folder, string fileName)
        {
            string subFile = Path.Combine(folder.FullName, fileName);
            if (File.Exists(subFile))
            {
                File.Delete(subFile);
            }
        }
    }
}
