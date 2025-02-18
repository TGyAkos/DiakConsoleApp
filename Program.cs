namespace DiakConsoleApp
{
    internal class Program
    {
        List<Diak> diakok { get; set; } = new List<Diak>();
        static void Main(string[] args)
        {
            Program program = new Program();
            program.ReadData();
            program.Masodik();
            program.Harmadik();
            program.Negyedik();
            program.Otodik();
            program.Hatodik();
            program.Hetedik();
            program.Nyolcadik();
            program.Kilendecik();
            program.Tizedik();
            program.Tizenegyedik();
            program.Tizenkettedik();
            program.Tizenharmadik();
            program.Tizennegyedik();
            program.Tizenotodik();
            program.Tizenhatodik();
        }

        void ReadData()
        {
            string[] lines = File.ReadAllLines(@"../../../diakok.txt");

            foreach (string line in lines)
            {
                string[] parts = line.Split("\t");
                Diak diak = new Diak
                {
                    Nev = parts[0],
                    Evfolyam = int.Parse(parts[1]),
                    Osztaly = parts[2],
                    SzinhazSzam = int.Parse(parts[3]),
                    MoziSzam = int.Parse(parts[4]),
                    OldalSzam = int.Parse(parts[5])
                };
                diakok.Add(diak);
            }
        }

        void Masodik()
        {
            Console.WriteLine("2. feladat");
            Console.WriteLine($"\tA mozi számainak osszege: {diakok.Sum(diak => diak.MoziSzam)}");
            Console.WriteLine($"\tA szinhaz számainak osszege: {diakok.Sum(diak => diak.SzinhazSzam)}");
        }

        void Harmadik()
        {
            Console.WriteLine("3. feladat");
            Console.WriteLine($"\tA legtobb oldalt olvaso diák: {diakok.Max(diak => diak.OldalSzam)}");
        }

        void Negyedik()
        {
            Console.WriteLine("4. feladat");
            Console.WriteLine($"\tA legnepesebb osztaly: {diakok.GroupBy(diak => diak.Osztaly).OrderByDescending(group => group.Count()).First().Key}");
        }

        void Otodik()
        {
            Console.WriteLine("5. feladat");
            Console.WriteLine($"\tAz A osztalyban atlagosan a mozikban ennyiszer voltak: {diakok.Where(diak => diak.Osztaly == "A").Average(diak => diak.MoziSzam)}");
        }

        void Hatodik()
        {
            Console.WriteLine("6. feladat");
            Console.WriteLine($"\tA legtobben ebben az evfolyamban jarnak: {diakok.GroupBy(diak => diak.Evfolyam).OrderByDescending(group => group.Count()).First().Key}");
        }

        void Hetedik()
        {
            Console.WriteLine("7. feladat");
            diakok.OrderByDescending(diak => diak.OldalSzam).ToList().ForEach(diak => Console.WriteLine($"\t{diak.Nev}"));
        }

        void Nyolcadik()
        {
            Console.WriteLine("8. feladat");
            diakok.Where(diakok => diakok.Evfolyam == 12).OrderByDescending(diakok => diakok.Nev).ToList().ForEach(diak => Console.WriteLine($"\t{diak.Nev}"));
        }

        void Kilendecik()
        {
            Console.WriteLine("9. feladat");
            Console.WriteLine($"\tVan-e olyan diak aki semmilyen eloadast nem latogatott a honapban: {(diakok.Any(diak => diak.MoziSzam == 0 && diak.SzinhazSzam == 0) ? "van" : "nincs")}");
        }

        void Tizedik()
        {
            Console.WriteLine("10. feladat");
            Console.WriteLine($"\tA legtobbet olvaso osztaly: {diakok.GroupBy(diak => diak.Osztaly).OrderByDescending(group => group.Sum(diak => diak.OldalSzam)).First().Key}");
        }

        void Tizenegyedik()
        {
            Console.WriteLine("11. feladat");
            diakok
                .Select(diak => new { diak.Nev, diak.OldalSzam, TotalSzam = diak.SzinhazSzam + diak.MoziSzam })
                .OrderByDescending(diak => diak.TotalSzam)
                .OrderByDescending(diak => diak.OldalSzam)
                .ToList()
                .ForEach(diak => Console.WriteLine($"\t{diak.Nev}"));
        }

        void Tizenkettedik()
        {
            Console.WriteLine("12. feladat");
            Console.WriteLine($"\tVan-e olyan eset, hogy ket diak ugyanannyit olvasott: {(diakok.GroupBy(diak => diak.OldalSzam).Any(group => group.Count() > 1) ? "van" : "nincs")}");
        }

        void Tizenharmadik()
        {
            Console.WriteLine("13. feladat");
            Console.WriteLine($"\tA legtobbet olvaso 9-es diak es a legkevesebbet olvaso 12-es diak kozott ennyi a kulonbseg: {diakok.Where(diak => diak.Evfolyam == 9).Max(diak => diak.OldalSzam) - diakok.Where(diak => diak.Evfolyam == 12).Min(diak => diak.OldalSzam)}");
        }

        void Tizennegyedik()
        {
            Console.WriteLine("14. feladat");
            Console.WriteLine($"\tA legkedveltebb kulturalis program a diakok kozott: {(diakok.Max(diak => diak.MoziSzam) > diakok.Max(diak => diak.SzinhazSzam) ? "mozi" : "szinhaz")}");
        }

        void Tizenotodik()
        {
            Console.WriteLine("15. feladat");
            Console.WriteLine("\tA diakok olvasasanak kategorizasala 100-as intervallumokkent:");
            diakok.GroupBy(diak => diak.OldalSzam / 100).OrderBy(group => group.Key).ToList().ForEach(group => Console.WriteLine($"\t{group.Key * 100 + 1}-{group.Key * 100 + 100} oldal: {group.Count()} diak"));
        }

        void Tizenhatodik()
        {
            Console.WriteLine("16. feladat");
            var groupedDiakok = diakok.GroupBy(diak => diak.OldalSzam / 100).OrderBy(group => group.Key).ToList();
            Console.WriteLine("\tA diakok olvasasanak kategorizasala 100-as intervallumokkent, minimum ertekek:");
            groupedDiakok.Where(group => group.Count() == groupedDiakok.Min(group => group.Count())).ToList().ForEach(group => Console.WriteLine($"\t{group.Key * 100 + 1}-{group.Key * 100 + 100} oldal: {group.Count()} diak"));
            Console.WriteLine("\tA diakok olvasasanak kategorizasala 100-as intervallumokkent, maximum ertekek:");
            groupedDiakok.Where(group => group.Count() == groupedDiakok.Max(group => group.Count())).ToList().ForEach(group => Console.WriteLine($"\t{group.Key * 100 + 1}-{group.Key * 100 + 100} oldal: {group.Count()} diak"));

        }
    }
}