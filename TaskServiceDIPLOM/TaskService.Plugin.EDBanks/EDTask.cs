using Microsoft.Extensions.Logging;
using System.Xml;
using TaskService.CommonTypes.Classes;
using TaskService.CommonTypes.Helpers;
using TaskService.CommonTypes.Interfaces;
using TaskService.Plugin.CBRTasks.DataManager;
using TaskService.Plugin.CBRTasks.Mappers;
using TaskService.Plugin.EDBanks.Models;

namespace TaskService.Plugin.CBRTasks
{
    public class EDTask : ITask
    {
        public string Name => "EDBanks";

        public TaskDTO ServiceTask { get; set; }

        private EDMapper _mapper = new EDMapper();

        private EDDataManager _dataManager = new EDDataManager();

        public TaskResult Execute(CancellationToken token, ILogger logger)
        {
            if (token.IsCancellationRequested)
                return new TaskResult(true);

            logger.LogInformation($"Start working - {Name}");
            _dataManager.CleartTempTable();

            var taskResult = new TaskResult();

            var date = DateTime.Now.Date;
            var fileName = $"ED807_{date.Day}_{date.Month}_{date.Year}.zip";

            var modelBuffer = new List<EDBanksModel>();
            var maxBufferCount = 500;

            var rowsCount = 0;

            try
            {
                GetFile(fileName);
                fileName = FileHelper.GetFileByMask(ServiceTask.FilePath, ServiceTask.FileMask);

                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(fileName);
                XmlElement? xRoot = xDoc.DocumentElement;

                if (xRoot != null)
                {
                    var businessDay = ParseBusinessDay(xRoot.Attributes?.GetNamedItem("BusinessDay")?.Value);

                    foreach (XmlElement xnode in xRoot)
                    {
                        modelBuffer.Add(_mapper.Map(xnode, businessDay));
                        rowsCount++;
                        if (modelBuffer.Count >= maxBufferCount)
                        {
                            _dataManager.InsertIntoTemp(modelBuffer.ToArray());
                            modelBuffer.Clear();
                        }
                    }

                    if(modelBuffer.Any())
                    {
                        _dataManager.InsertIntoTemp(modelBuffer.ToArray());
                        modelBuffer.Clear();
                    }

                    taskResult.AffectedRows = rowsCount;
                    _dataManager.ImportFromTemp();
                    _dataManager.CleartTempTable();
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error while processing - {Name}; Exception - {ex.Message}");
                taskResult.AddWarning($"Error while processing - {Name}; Exception - {ex.Message}", true);
            }

            logger.LogInformation($"End working - {Name}; Affected rows - {taskResult.AffectedRows}");
            return taskResult;
        }

        private void GetFile(string fileName)
        {
            var isDownloadSuccess = FileHelper.DownloadAndSaveFile(ServiceTask.Url, Path.Combine(ServiceTask.FilePath, fileName));

            if (!isDownloadSuccess)
                throw new ApplicationException($"Error on file downloading from - {ServiceTask.Url}");

            var isUnZipSuccess = FileHelper.UnZipFile(Path.Combine(ServiceTask.FilePath, fileName), ServiceTask.FilePath);

            if (!isUnZipSuccess)
                throw new ApplicationException($"Error on file UnZipping - {fileName}");

            var filePath = FileHelper.GetFileByMask(ServiceTask.FilePath, ServiceTask.FileMask);

            if (string.IsNullOrEmpty(filePath))
                throw new ApplicationException($"Error on getting file - {fileName}");
        }

        private DateTime ParseBusinessDay(string date)
        {
            if (DateTime.TryParse(date, out var result))
                return result;

            return DateTime.Now;
        }
    }
}
