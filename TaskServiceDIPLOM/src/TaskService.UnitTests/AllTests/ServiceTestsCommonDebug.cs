using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using TaskService.CommonTypes.Classes;
using TaskService.CommonTypes.Interfaces;
using TaskService.CommonTypes.Sql;
using TaskService.Plugin.CBRTasks;
using TaskService.Plugin.Clients;

namespace TaskService.UnitTests.AllTests
{
    [TestFixture]
    public class ServiceTestsCommonDebug
    {
        private Mock<ILogger> _loggerMock;
        private CancellationToken _token;
        private ITask _task;

        [SetUp]
        public void SetUp()
        {
            SqlDapper.InitDapper("Data Source=.;Initial Catalog=BankDatabase;Integrated Security=true", "200");

            _loggerMock = new Mock<ILogger>();
            _token = new CancellationToken();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        [Test]
        public void FileParser_GetNextRow_ExpectedValues_Correct()
        {
            FileParser parser = new FileParser();
            var task = new TaskDTO
            {
                FieldsCount = 3,
                FieldsSeparator = "^"
            };

            List<FileRow> fileValues = new List<FileRow>();

            using StreamReader reader = new StreamReader(@"C:\Users\andre\source\repos\TaskService\src\TaskService.UnitTests\TestFiles\test_parser.txt");
            while(!reader.EndOfStream)
                fileValues.Add(parser.GetNextRow(reader, task));

            Assert.AreEqual(2, fileValues.Count);
            Assert.AreEqual(3, fileValues.First().RowValues.Count);
        }

        [Test]
        public void CBRRates_Execute_ExpectedWork_NoSpecialParams()
        {
            _task = new RatesTask();
            var result = _task.Execute(_token, _loggerMock.Object);

            Assert.IsTrue(!result.TaskWarnings.Any());
        }

        [Test]
        public void EDTask_Execute_ExpectedWork_NoSpecialParams()
        {
            _task = new EDTask();
            _task.ServiceTask = new TaskDTO
            {
                Url = "https://www.cbr.ru/s/newbik",
                FilePath = AppDomain.CurrentDomain.BaseDirectory,
                FileMask = "*_ED807_full.xml"
            };

            var result = _task.Execute(_token, _loggerMock.Object);

            Assert.IsTrue(!result.TaskWarnings.Any());
        }

        [Test]

        public void ClientsTask_Execute_ExpectedWork_NoSpecialPArams()
        {
            _task = new ClientsTask();

            _task.ServiceTask = new TaskDTO
            {
                FieldsCount = 22,
                FilePath = @"C:\Users\andre\source\repos\TaskService\src\TaskService.UnitTests\TestFiles\",
                FileMask = "clients_*.txt",
                FieldsSeparator = ";"
            };

            var result = _task.Execute(_token, _loggerMock.Object);

            Assert.IsTrue(!result.TaskWarnings.Any());
        }

        [Test]
        public void DateTimeParser()
        {
            Assert.IsTrue(DateTime.TryParse("29/04/2000", out var res));
        }
    }
}
