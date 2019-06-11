using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Newtonsoft.Json;

namespace Basics
{
    class Program
    {
        private static IDictionary<string, HorsePedigreeItemDTO> _items =
            new Dictionary<string, HorsePedigreeItemDTO>();

        private static string[] _positions = { "X", "M", "F", "MM", "MF", "FM", "FF", "MMM", "MMF", "MFM", "MFF", "FMM", "FMF", "FFM", "FFF" };

        static void Main(string[] args)
        {
            var itemsRaw = new List<HorsePedigreeItemDTO>()
            {
                new HorsePedigreeItemDTO()
                {
                    Name = "A",
                    IdNumber = "0001"
                },
                new HorsePedigreeItemDTO()
                {
                    Name = "B",
                    IdNumber = "0002"
                },
                new HorsePedigreeItemDTO()
                {
                    Name = "C",
                    IdNumber = "0003"
                },
                new HorsePedigreeItemDTO()
                {
                    Name = "D",
                    IdNumber = "0004"
                },
                new HorsePedigreeItemDTO()
                {
                    Name = "E",
                    IdNumber = "0005"
                },
                new HorsePedigreeItemDTO()
                {
                    Name = "F",
                    IdNumber = "0006"
                },
                new HorsePedigreeItemDTO()
                {
                    Name = "G",
                    IdNumber = "0007"
                },
                new HorsePedigreeItemDTO()
                {
                    Name = "H",
                    IdNumber = "0008"
                },
                new HorsePedigreeItemDTO()
                {
                    Name = "I",
                    IdNumber = "0009"
                },
                new HorsePedigreeItemDTO()
                {
                    Name = "J",
                    IdNumber = "0010"
                },
                new HorsePedigreeItemDTO()
                {
                    Name = "K",
                    IdNumber = "0011"
                },
                new HorsePedigreeItemDTO()
                {
                    Name = "L",
                    IdNumber = "0012"
                },
                new HorsePedigreeItemDTO()
                {
                    Name = "M",
                    IdNumber = "0013"
                },
                new HorsePedigreeItemDTO()
                {
                    Name = "N",
                    IdNumber = "0014"
                },
                new HorsePedigreeItemDTO()
                {
                    Name = "P",
                    IdNumber = "0015"
                },
                new HorsePedigreeItemDTO()
                {
                    Name = "Q",
                    IdNumber = "0016"
                }
            };

            foreach (var itemRaw in itemsRaw)
            {
                _items.Add(itemRaw.Name, itemRaw);
            }

            //var pedigreeARaw = "A,C,D,G,H,G,I,K,L,M,N,K,L,P,L";
            //var pedigreeBRaw = "B,E,F,G,I,J,H,K,L,P,L,K,Q,M,N";

            var pedigreeARaw = "A,B,C,D,E,F,G";
            var pedigreeBRaw = "H,G,I,J,K,L,M";

            var pedigreeA = ParseStringToPedigree(pedigreeARaw).ToList();
            var pedigreeB = ParseStringToPedigree(pedigreeBRaw).ToList();




            var pedigreeProcessor = new PedigreeProcessor();
            
            var unitedPedigree = pedigreeProcessor.ProcessPedigrees(pedigreeA, pedigreeB).ToList();

            var shortenPedigree = unitedPedigree.Select(PedigreeUnitShort.Parse);

            var calculator = new InbreedingCalculator();

            var father = unitedPedigree.FirstOrDefault(x => x.Name.Equals("H"));
            var mother = unitedPedigree.FirstOrDefault(x => x.Name.Equals("A"));

            var coefficient =
                calculator.CalculateInbreedingCoefficient(unitedPedigree, father.ItemId, mother.ItemId);


            using (var tw = File.CreateText(@"D:\\CombinedPedigree.json"))
            {
                tw.Write(JsonConvert.SerializeObject(shortenPedigree));
            }



            var date = DateTime.UtcNow;

            var dateAsStr = date.ToString("yyyy-MM-dd-HH-mm");

            var day = DaysOfWeek.Enum.Friday;

            string str = @"   UXdW-_ akE0       SlRO- R   Q%3D%3D";

            str = $"{str}";

            var watch = new Stopwatch();

            watch.Start();
            for (int i = 0; i < 1_000_000; i++)
            {
                string cleanStr = Sanitizer.SanitizeURL(str); 
            }
            watch.Stop();

            var elapsedMs = watch.ElapsedMilliseconds;

            var result = 14 / 5;

            var emptyString = string.Empty;

            var enumerable = new[] { "1", "2", "3", "4", "5"};

            EnumerableProcessor.ProcessEnumerable(enumerable);

            Console.WriteLine("Hello World!");
        }

        private static IEnumerable<HorsePedigreeDTO> ParseStringToPedigree(string pedigreeCode)
        {
            var pedigreeCodeParsed = pedigreeCode.Split(',', StringSplitOptions.RemoveEmptyEntries);

            var result = new List<HorsePedigreeDTO>();

            for (int i = 0; i < pedigreeCodeParsed.Length; i++)
            {
                var item = new HorsePedigreeDTO()
                {
                    PedigreePosition = _positions[i],
                    Item = _items[pedigreeCodeParsed[i]]
                };

                result.Add(item);
            }

            return result;
        }
    }

    class EnumerableProcessor
    {
        public static void ProcessEnumerable(IEnumerable<string> enumerable)
        {
            var total = enumerable.Count();

            //var enumerator = enumerable.GetEnumerator();

            foreach (var item in enumerable)
            {
                Console.WriteLine(item);
            }
        }
    }
}
