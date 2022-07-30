using System;
using CodeChallenge.Config;

public class Program
{

    public static void Main(string[] args)
    {
        Console.WriteLine("Beginning");
        new App().Configure(args).Run();
        Console.WriteLine("End");
    }
}