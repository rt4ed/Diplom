using Microsoft.Extensions.Logging;
using TaskService.CommonTypes.Classes;
using TaskService.CommonTypes.Common;
using TaskService.CommonTypes.Helpers;
using TaskService.CommonTypes.Interfaces;
using TaskService.Plugin.Clients.DataManager;
using TaskService.Plugin.Clients.Mapper;
using TaskService.Plugin.Clients.Models;

namespace TaskService.Plugin.Clients
{
    public class ClientsTask : ITask
    {
        public string Name => "Clients";

        public TaskDTO ServiceTask { get; set; }

        private ClientDataManager _dataManager = new ClientDataManager();

        private FileParser _fileParser = new FileParser();

        private ClientsMapper _mapper = new ClientsMapper();

        private CommonValidator _validator = new CommonValidator();

        public TaskResult Execute(CancellationToken token, ILogger logger)
        {
            if (token.IsCancellationRequested)
                return new TaskResult(true);

            logger.LogInformation($"Start working - {Name}");
            _dataManager.CleartTempTable();

            var taskResult = new TaskResult();

            var date = DateTime.Now.Date;

            var modelBuffer = new List<ClientsDBModel>();
            var maxBufferCount = 500;
            var rowsCount = 0;

            var path = FileHelper.GetFileByMask(ServiceTask.FilePath, ServiceTask.FileMask);

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    while (!reader.EndOfStream)
                    {
                        FileRow row;
                        try
                        {
                            row = _fileParser.GetNextRow(reader, ServiceTask);
                        }
                        catch
                        {
                            taskResult.AddWarning("Row were skipped due to error", false, _fileParser.CurrentRowNumber);
                            continue;
                        }

                        var mapperWarnings = new List<TaskWarning>();
                        var model = _mapper.Map(row, ServiceTask.FieldsCount.Value, ref mapperWarnings);

                        mapperWarnings.ForEach(x => taskResult.TaskWarnings.Add(x));

                        if (!mapperWarnings.Any(x => x.IsCritical))
                        {
                            _validator.IsValid(model, out var result);

                            result.ToList().ForEach(x => taskResult.TaskWarnings.Add(new TaskWarning { IsCritical = true, Message = x.ErrorMessage}));

                            if (!result.Any())
                            {
                                if (modelBuffer.Count >= maxBufferCount)
                                {
                                    _dataManager.InsertIntoTemp(modelBuffer.ToArray());
                                    modelBuffer.Clear();
                                }

                                modelBuffer.Add(model);
                                rowsCount++;
                            }
                        }
                    }
                }
                if (modelBuffer.Any())
                {
                    _dataManager.InsertIntoTemp(modelBuffer.ToArray());
                    modelBuffer.Clear();
                }

                _dataManager.ImportFromTemp();
                _dataManager.CleartTempTable();

                FileHelper.MoveFile(path, Path.Combine(ServiceTask.FilePath, "Worked", Path.GetFileName(path)));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error while processing - {Name}; Exception - {ex.Message}");
                taskResult.AddWarning($"Error while processing - {Name}; Exception - {ex.Message}", true);
            }

            logger.LogInformation($"End working - {Name}; Affected rows - {rowsCount}");
            return taskResult;
        }
    }
}
