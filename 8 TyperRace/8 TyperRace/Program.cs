using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TyperRace
{
    class Program
    {
        static string textToType = "Англичане пьют чая больше, чем жители любой другой страны. Например, в двадцать раз больше, чем американцы.";
        static List<Record> records = new List<Record>();
        static Stopwatch stopwatch = new Stopwatch();

        static void Main(string[] args)
        {
            LoadRecords();
            while (true)
            {
                Console.WriteLine("Введите своё имя: ");
                string name = Console.ReadLine();
                Console.WriteLine("Введите текст ниже так быстро, как только сможете:");
                Console.WriteLine(textToType);

                stopwatch.Start();
                string typedText = Console.ReadLine();
                stopwatch.Stop();

                if (typedText == textToType)
                {
                    Record record = new Record
                    {
                        Name = name,
                        CharactersPerMinute = (int)(textToType.Length / (stopwatch.Elapsed.TotalMinutes)),
                        CharactersPerSecond = (int)(textToType.Length / (stopwatch.Elapsed.TotalSeconds))
                    };
                    records.Add(record);
                    SaveRecords();
                }

                Console.WriteLine("Рекорды:");
                Console.WriteLine("Имя\t\tСимволов в минуту\tСимволов в секунду");
                foreach (var record in records)
                {
                    Console.WriteLine("{0}\t\t\t{1}\t\t\t\t\t{2}", record.Name, record.CharactersPerMinute, record.CharactersPerSecond);
                }
            }
        }

        static void LoadRecords()
        {
            if (File.Exists("records.json"))
            {
                string json = File.ReadAllText("records.json");
                records = JsonConvert.DeserializeObject<List<Record>>(json);
            }
        }

        static void SaveRecords()
        {
            string json = JsonConvert.SerializeObject(records);
            File.WriteAllText("records.json", json);
        }
    }

    class Record
    {
        public string Name { get; set; }
        public int CharactersPerMinute { get; set; }
        public int CharactersPerSecond { get; set; }
    }
}
