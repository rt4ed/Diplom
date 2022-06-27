using System.Text;
using TaskService.CommonTypes.Helpers;

namespace TaskService.CommonTypes.Classes
{
    public class FileBuilder
    {
        public int RowCounter { get; set; } = 0;

        private string _fileName;

        public void Build(ICollection<FileRow> fileRows, TaskDTO task)
        {
            if (fileRows is null || task is null)
                throw new ArgumentNullException(nameof(FileBuilder.Build));

            StringBuilder sb = new StringBuilder();

            foreach(var row in fileRows)
            {
                foreach(var value in row.RowValues)
                {
                    sb.Append(value);
                    sb.Append(task.FieldsSeparator);
                }
                sb.Append(Environment.NewLine);
                RowCounter++;
            }

            if(string.IsNullOrEmpty(_fileName))
                _fileName = $"{DateTime.Now.ToFileTime}_{task.FileMask}";

            FileHelper.CreateFile(task.FilePath, _fileName);
            FileHelper.AppendToFile(task.FilePath, _fileName, sb);
        }
    }
}
