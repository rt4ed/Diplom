using Microsoft.Extensions.Logging;
using TaskService.CommonTypes.Classes;
using TaskService.CommonTypes.Helpers;
using TaskService.CommonTypes.Interfaces;
using TaskService.CommonTypes.Sql;
using TaskService.Plugin.CBRTasks.Api;
using TaskService.Plugin.CBRTasks.Models;

namespace TaskService.Plugin.CBRTasks
{
    public class RatesTask : ITask
    {
        public string Name => "CurrentCBRRates";

        public TaskDTO ServiceTask { get; set; }

        private DailyInfoSoapClient _cbrClient = new DailyInfoSoapClient(DailyInfoSoapClient.EndpointConfiguration.DailyInfoSoap12);

        private XmlHelper _serializer = new XmlHelper();

        public TaskResult Execute(CancellationToken token, ILogger logger)
        {
            if (token.IsCancellationRequested)
                return new TaskResult(true);

            logger.LogInformation($"Start working - {Name}");
            var taskResult = new TaskResult();

            try
            {
                var cursOnDateXml = _cbrClient.GetCursOnDateXML(DateTime.Now);
                var valuteData = _serializer.Deserailize<ValuteData>(cursOnDateXml.OuterXml);

                var data = MapData(valuteData);
                taskResult.AffectedRows = data.Count;
                ClearTableAndInsert(data, "[dbo].[RatesCB]");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error while processing - {Name}");
                taskResult.AddWarning($"Error while processing - {Name}", true);
            }

            logger.LogInformation($"End working - {Name}; Affected rows - {taskResult.AffectedRows}");
            return taskResult;
        }

        private ICollection<CurrencyModel> MapData(ValuteData data)
        {
            List<CurrencyModel> mappedModel = new List<CurrencyModel>();

            foreach(var valute in data.Items)
            {
                mappedModel.Add(new CurrencyModel
                {
                    Name = valute.Vname.Trim(),
                    Curs = valute.Vcurs / valute.Vnom,
                    Code = int.Parse(valute.Vcode.Trim()),
                    Date = DateTime.Now
                });
            }

            return mappedModel;
        }

        /// <summary>
        /// Insert into table
        /// </summary>
        private void ClearTableAndInsert(ICollection<CurrencyModel> model, string dest)
        {
            SqlDapper.ClearTable(dest);
            var table = SqlDapper.CreateDataTable(model);
            var mapping = SqlDapper.PrepareColumnMapping(table);

            SqlDapper.BulkInsertIntoTable(table, dest, mapping);
        }
    }
}
