using System;
using System.IO;
using System.Threading.Tasks;

namespace Lab6.Module.Logic
{
    public class Task2
    {
        public class Task_EventArgs : EventArgs
        {
            public string Name { get; set; }
            public string TextRead { get; set; }
            public override string ToString() => Name;
        }
        private string[] filesToRead;
        private int currentIndex;

        public event EventHandler<Task_EventArgs> FileRead;
        public Task2(string[] files)
        {

            filesToRead = files;
            currentIndex = 0;
        }
        public async  void ReadFilesAsync()
        {

           await ReadNextFile();
        }
        byte[] buffer;
        private async Task ReadNextFile()
        {
            await Task.Run(() => {
                if (currentIndex < filesToRead.Length)
                {
                    string fileName = filesToRead[currentIndex];

                    // Start reading the current file asynchronously.
                    FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read, 1024, true);
                    buffer = new byte[fileStream.Length];
                    IAsyncResult asyncResult = fileStream.BeginRead(buffer, 0, buffer.Length, OnFileRead, fileStream);
                }
            });
           
        }

        private async void OnFileRead(IAsyncResult result)
        {
            FileStream fileStream = (FileStream)result.AsyncState;
            string fileName = fileStream.Name;
            int bytesRead = fileStream.EndRead(result);
            if (bytesRead > 0)
            {
                string fileContent = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);
                FileRead?.Invoke(this, new Task_EventArgs() { Name = fileName,TextRead = fileContent});
            }
            currentIndex++;
            fileStream.Close();
            await ReadNextFile();
        }
    }
}
