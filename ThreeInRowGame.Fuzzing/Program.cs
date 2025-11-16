using System;
using System.IO;
using System.Text;
using SharpFuzz;
using ThreeInRowGame;

internal class Program
{
    public static void Main(string[] args)
    {
        Fuzzer.Run(stream =>
        {
            try
            {
                // Read data safely
                using (var reader = new System.IO.StreamReader(stream, Encoding.UTF8))
                {
                    string input = reader.ReadToEnd();
                        
                    // Simple test function
                    YourFunctionToTest(input);
                }
            }
            catch (InvalidOperationException)
            {
                // Only let intended crashes through
                throw;
            }
            catch (Exception)
            {
                // Suppress other exceptions
                Console.WriteLine();
            }
        });
        
        // PrepareGame();
        //
        // switch (args[0])
        // {
        //     case "init":
        //         FuzzGameInitialization();
        //         break;
        //     case "input":
        //         PrepareGame();
        //         FuzzInput();
        //         break;
        // }
    }
    
    static void YourFunctionToTest(string input)
    {
        // Example vulnerable function
        if (input.Length > 3 && 
            input[0] == 'b' && 
            input[1] == 'a' && 
            input[2] == 'd')
        {
            throw new InvalidOperationException("Found a bug!");
        }
            
        // Your actual code to test goes here
    }

    private static void FuzzGameInitialization()
    {
        Fuzzer.OutOfProcess.Run(_ =>
        {
            try
            {
                var gameManager = new GameManager();
                gameManager.InitializeNewGame();
            }
            catch
            {
                // ignored
            }
        });
    }

    private static void FuzzInput()
    {
        Fuzzer.OutOfProcess.Run(stream =>
        {
            try
            {
                
            }
            catch
            {
                // ignored
            }
        });
    }

    private static void PrepareGame()
    {
        var gameManager = new GameManager();
        gameManager.InitializeNewGame();
    }
}
