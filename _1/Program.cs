// See https://aka.ms/new-console-template for more information

using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace _1
{
    public static class Program
    {
        static readonly HttpClient client = new HttpClient();

        private static async Task Main()
        {
            //Level 1
            var puzzleUtils = new PuzzleUtils(2021,1, client);
            var input = await puzzleUtils.LoadIntArrayInput();

            var result = 0;
            for (var i = 0; i < input.Length -1; i++)
            {
                if (input[i] < input[i+1])
                {
                    result++;
                } 
            }
            
            Console.WriteLine(result);
            await puzzleUtils.PostResult(level: 1, result);
            
            //Level 2
            result = 0;
            //for (var i = 0; i < input.Length -1; i++)
            for (var i = input.Length - 3 - 1; i >= 0; i--)
            {
                //if (input[i] < input[i+1])
                if (input[i] + input[i+1] + input[i+2] < input[i + 1]+ input[i+2] + input[i+3])
                {
                    result++;
                } 
            }

            Console.WriteLine(result);
            
            await puzzleUtils.PostResult(2, result);
        }
    }
}
