using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final
{
    public class FileAccessHelper
    {
        public static string GetLocalFilePath(string filename)
        {
            string path = System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);
            Console.WriteLine("Ya esta " + path);
            return path;
        }
    }
}
