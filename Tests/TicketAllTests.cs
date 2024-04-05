using System.Text.RegularExpressions;

namespace Autotest2024
{
    [TestFixture]
    public class TicketAllTests
    {

        [Test]
        public void TestAllTickets()
        {
            // Путь к папке с билетами относительно корневой папки проекта
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\Tickets\");
            folderPath = Path.GetFullPath(folderPath);

            // Перебираем все содержимое билетов

            var files = Directory.GetFiles(folderPath, "*.txt");
            foreach (var file in files)
            {
                // Читаем и возвращаем содержимое каждого файла
                string ticketContent = File.ReadAllText(file);
                TestContext.Out.WriteLine("Файл: " + file);
                ExtractDate(ticketContent);
                ExtractDepartureStation(ticketContent);
                ExtractDestinationStation(ticketContent);
                ExtractTicketNumber(ticketContent);
                ExtractSystemNumber(ticketContent);
                ExtractTransportation(ticketContent);
                ExtractTariffCost(ticketContent);
                ExtractTotal(ticketContent);
                TestContext.Out.WriteLine("--------------");
            }
        }
        // Метод для извлечения даты поездки из содержимого билета
        private string ExtractDate(string content)
        {
            // Выражение, учитывающее пробелы или их отсутсвие между числами и точками
            string pattern = @"на\s+([\d\s|\d]+[\d\s|\d]+[\.\s|\.]+[\d\s|\d]+[\d\s|\d]+[\.\s|\.]+[\d\s|\d]+[\d\s|\d]+[\d\s|\d]+\d)\s+-\s+>\s+П";
            var match = Regex.Match(content, pattern);
            // Если дата найдена, извлекаем и выводим ее
            if (match.Success)
            {
                string extractedDate = match.Groups[1].Value.Replace(" ", "");
                TestContext.Out.WriteLine("Извлеченная дата: " + extractedDate);
                return extractedDate;
            }
            Assert.IsTrue(match.Success, "Дата поездки не найдена");
            return null;
        }
        // Метод для извлечения cтанции отправления из содержимого билета
        private string ExtractDepartureStation(string content)
        {
            string pattern = @"от\s+(.*?)\s+до";
            var match = Regex.Match(content, pattern);
            if (match.Success)
            {
                string departureStation = match.Groups[1].Value.Trim();
                TestContext.Out.WriteLine("Станция отправления: " + departureStation);
                return departureStation;
            }
            Assert.IsTrue(match.Success, "Станция отправления не найдена");
            return null;
        }
        // Метод для извлечения cтанции назначения из содержимого билета
        private string ExtractDestinationStation(string content)
        {
            string pattern = @"до\s+(.*?)\r";
            var match = Regex.Match(content, pattern);
            if (match.Success)
            {
            string destinationStation = match.Groups[1].Value.Trim();
            TestContext.Out.WriteLine("Станция назначения: " + destinationStation);
                return destinationStation;
            }
            Assert.IsTrue(match.Success, "Станция назначения не найдена");
            return null;
        }
        // Метод для извлечения номера билета
        private string ExtractTicketNumber(string content)
        {
            string pattern = @"Билет\s+N+[:\s|:]+(.*?)\s";
            var match = Regex.Match(content, pattern);
            if (match.Success)
            {
                string ticketNumber = match.Groups[1].Value.Trim();
                TestContext.Out.WriteLine("Номер билета: " + ticketNumber);
                return ticketNumber;
            }
            Assert.IsTrue(match.Success, "Номер билета не найден");
            return null;
        }
        // Метод для извлечения системного номера
        private string ExtractSystemNumber(string content)
        {
            string pattern = @"Сист.N+[:\s|:]+(.*?)\r";
            var match = Regex.Match(content, pattern);
            if (match.Success)
            {
                string systemNumber = match.Groups[1].Value.Trim();
                TestContext.Out.WriteLine("Системный номера: " + systemNumber);
                return systemNumber;
            }
            Assert.IsTrue(match.Success, "Системный номер билета не найден");
            return null;
        }
        // Метод для извлечения типа перевозки
        private string ExtractTransportation(string content)
        {
            string pattern = @"Перевозка\s+(.+?)\s+Стоимость";
            var match = Regex.Match(content, pattern);
            if (match.Success)
            {
                string transportation = match.Groups[1].Value.Trim();
                TestContext.Out.WriteLine("Перевозка: " + transportation);
                return transportation;
            }
            Assert.IsTrue(match.Success, "Перевозка не найдена");
            return null;
        }
        // Метод для извлечения стоимости по тарифу
        private string ExtractTariffCost(string content)
        {
            string pattern = @"Стоимость\s+по\s+тарифу:\s+[=\s|=]+(.*?)\s";
            var match = Regex.Match(content, pattern);
            if (match.Success)
            {
                string tariffCost = match.Groups[1].Value.Trim();
                TestContext.Out.WriteLine("Стоимость по тарифу: " + tariffCost);
                return tariffCost;
            }
            Assert.IsTrue(match.Success, "Стоимость по тарифу не найдена");
            return null;
        }
        // Метод для извлечения итоговой стоимости
        private string ExtractTotal(string content)
        {
            string pattern = @"ИТОГ+[:\s|:]+(.*?)\s";
            var match = Regex.Match(content, pattern);
            if (match.Success)
            {
                string total = match.Groups[1].Value.Trim();
                TestContext.Out.WriteLine("Итоговая стоимость: " + total);
                return total;
            }
            Assert.IsTrue(match.Success, "Итоговая стоимость не найдена");
            return null;
        }
    }
}