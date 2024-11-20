using System;

class CricketTeam
{
    public (int count, int sum, double average) Pointscalculation(int no_of_matches)
    {
        int[] scores = new int[no_of_matches];
        int sum = 0;

        Console.WriteLine($"Enter the scores for {no_of_matches} matches:");

        for (int i = 0; i < no_of_matches; i++)
        {
            Console.Write($"Match {i + 1} score: ");
            scores[i] = int.Parse(Console.ReadLine());
            sum += scores[i];
        }

        double average = (double)sum / no_of_matches;

        return (no_of_matches, sum, average);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter the number of matches: ");
        int no_of_matches = int.Parse(Console.ReadLine());

        CricketTeam team = new CricketTeam();
        var result = team.Pointscalculation(no_of_matches);

        Console.WriteLine("\n..........Results ...........");
        Console.WriteLine($"Count of the Matches: {result.count}");
        Console.WriteLine($"Sum of Scores is : {result.sum}");
        Console.WriteLine($"Average Score is : {result.average:F2}");
        Console.ReadLine();
    }
}
