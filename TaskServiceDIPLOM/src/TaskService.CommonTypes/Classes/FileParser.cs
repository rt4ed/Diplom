namespace TaskService.CommonTypes.Classes
{
    public class FileParser
    {
        public int CurrentRowNumber { get; set; } = 0;

        public void ResetRowNumber() => CurrentRowNumber = 0;

        public FileRow GetNextRow(StreamReader reader, TaskDTO task)
        {
            if (reader is null || task is null)
                throw new ArgumentNullException(nameof(FileParser));

            var rowValues = ReadLine(reader, task);

            TryToRestoreRow(ref reader, ref rowValues, task);

            return new FileRow { RowValues = rowValues.Split(task.FieldsSeparator).SkipLast(1).ToList() };
        }

        private void TryToRestoreRow(ref StreamReader reader, ref string values, TaskDTO task)
        {
            var separatorCount = GetSeparatorCount(values, task.FieldsSeparator);

            if (separatorCount == task.FieldsCount)
                return;

            if (separatorCount > task.FieldsCount)
                throw new ArgumentException("Row have more fields, than task field count");

            if (!reader.EndOfStream)
            {
                var nextRowValues = ReadLine(reader, task);
                values += nextRowValues;
                TryToRestoreRow(ref reader, ref values, task);
            }
            else if (separatorCount < task.FieldsCount)
                throw new ArgumentException("Can not restore row!");
        }

        private string ReadLine(StreamReader reader, TaskDTO task)
        {
            CurrentRowNumber++;
            return reader.ReadLine();
        }

        private int GetSeparatorCount(string input, string separator) => input.Split(char.Parse(separator)).Length - 1;
    }
}
