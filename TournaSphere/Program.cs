namespace TournaSphere
{
    class Program
    {
        static void Main(string[] args)
        {
            bool continueProgram = true;

            while (continueProgram)
            {
                Console.Clear();
                Console.WriteLine("=== Tournament Manager ===");
                Console.WriteLine("1 - Swiss Bracket");
                Console.WriteLine("2 - Double Bracket (Upper and Lower)");
                Console.WriteLine("0 - Exit");
                Console.Write("\nChoose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ManageSwissTournament();
                        break;
                    case "2":
                        ManageDoubleTournament();
                        break;
                    case "0":
                        continueProgram = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                if (continueProgram)
                {
                    Console.WriteLine("\nPress any key to return to the main menu...");
                    Console.ReadKey();
                }
            }
        }

        private static void ManageSwissTournament()
        {
            var swissTournament = new SwissTournament();
            bool continueTournament = true;

            while (continueTournament)
            {
                Console.Clear();
                Console.WriteLine("Managing Swiss Tournament");
                Console.WriteLine("1. Add team");
                Console.WriteLine("2. Generate round");
                Console.WriteLine("3. Display matches");
                Console.WriteLine("4. Export results (JSON)");
                Console.WriteLine("5. Return to the main menu");
                Console.Write("\nChoose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTeams(swissTournament);
                        break;
                    case "2":
                        // Implement round generation logic
                        Console.WriteLine("Rounds generated!");
                        break;
                    case "3":
                        DisplayMatches(swissTournament.Matches);
                        break;
                    case "4":
                        ExportTournament(swissTournament);
                        break;
                    case "5":
                        continueTournament = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                if (continueTournament)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        private static void ManageDoubleTournament()
        {
            var doubleTournament = new DoubleTournament();
            bool continueTournament = true;

            while (continueTournament)
            {
                Console.Clear();
                Console.WriteLine("Managing Elimination Tournament");
                Console.WriteLine("1. Add team");
                Console.WriteLine("2. Generate upper bracket");
                Console.WriteLine("3. Choose winner of matches");
                Console.WriteLine("4. Generate next round");
                Console.WriteLine("5. Display matches");
                Console.WriteLine("6. Export results (JSON)");
                Console.WriteLine("7. Return to the main menu");
                Console.Write("\nChoose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTeams(doubleTournament);
                        break;
                    case "2":
                        try
                        {
                            doubleTournament.GenerateUpperBracket();
                            Console.WriteLine("Upper bracket generated successfully!");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;
                    case "3":
                        doubleTournament.GenerateNextRound();
                        Console.WriteLine("Next round generated successfully!");
                        break;
                    case "4":
                        DisplayMatches(doubleTournament.UpperBracket);
                        break;
                    case "5":
                        ExportTournament(doubleTournament);
                        break;
                    case "6":
                        continueTournament = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                if (continueTournament)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        private static void DisplayMatches(List<Match> matches)
        {
            foreach (var match in matches)
            {
                Console.WriteLine($"{match.TeamA?.Name} vs {match.TeamB?.Name}");
            }
        }

        private static void AddTeams(ITournament tournament)
        {
            Console.WriteLine("Add teams to the tournament (type 'end' to finish):");

            while (true)
            {
                Console.Write("Team name: ");
                string name = Console.ReadLine();

                if (name.ToLower() == "end")
                    break;

                tournament.AddTeam(name);
            }
        }


        private static void ExportTournament(ITournament tournament)
        {
            Console.WriteLine("\nDo you want to export the tournament to JSON? (y/n)");
            string export = Console.ReadLine()?.ToLower();

            if (export == "y")
            {
                var exporter = new JsonExporter("Exports");
                Console.Write("Enter the file name (without extension): ");
                string fileName = Console.ReadLine();

                if (tournament is SwissTournament swissTournament)
                {
                    exporter.ExportTournament(swissTournament.Matches, fileName);
                }
                else if (tournament is DoubleTournament doubleTournament)
                {
                    exporter.ExportTournament(doubleTournament.Matches, fileName);
                }
            }
            Console.WriteLine("\nPress any key to return to the main menu.");
            Console.ReadKey();
        }
    }
}
