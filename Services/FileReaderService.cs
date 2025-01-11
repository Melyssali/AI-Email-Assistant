using System;
using System.IO;

namespace OpenMindProject.Services.FileReader
{
    public class FileReaderService
    {
        public string ReadFile()
        {
            try
            {
                using var streamReader = new StreamReader("companysummary.txt");
                return streamReader.ReadToEnd();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                return string.Empty;
            }
        }
    }
}
