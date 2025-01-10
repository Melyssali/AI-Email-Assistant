using System;
using System.IO;

namespace OpenMindProject.Services.FileReader
{
    public class FileReaderService
    {
        public string ReadFile()
        {
            using var streamReader = new StreamReader("companysummary.txt");
            return streamReader.ReadToEnd();
        }
    }
}
