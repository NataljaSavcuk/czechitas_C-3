using System;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace Greed;

public class Calculation
{
    Dictionary<int, int> result = new Dictionary<int, int>();
    public List<int> GetPlayerResults()
    {
        var listOfPlayerDice = new List<int>();
        while (listOfPlayerDice.Count == 0)
        {
            Console.WriteLine("Tell me please your results separated by commas (e.g., 1,2,3,5,6):");
            string input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                string[] parts = input.Split(',');

                foreach (string part in parts)
                {
                    if (int.TryParse(part.Trim(), out int diceResult))
                    {
                        if (diceResult >= 1 && diceResult <= 6)
                        {
                            listOfPlayerDice.Add(diceResult);
                        }
                        else
                        {
                            Console.WriteLine("Where did you get this dice? Try once more with correct one!");
                            listOfPlayerDice.Clear();
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Hej, it's not a valid numbers! Try once more!");
                        listOfPlayerDice.Clear();
                        Console.WriteLine();
                    }
                }
            }

        }
        return listOfPlayerDice;
    }

    private bool IsListValid(List<int> listOfPlayerDice)
    {
        if (listOfPlayerDice.Count > 6)
        {
            return false;
        }
        return true;
    }

    private Dictionary<int, int> SortResults()
    {
        var listOfPlayerDice = GetPlayerResults();
        var isListValid = IsListValid(listOfPlayerDice);
        if (isListValid)
        {
            foreach (var item in listOfPlayerDice)
            {
                if (result.ContainsKey(item))
                {
                    result[item]++;
                }
                else
                {
                    result[item] = 1;
                }
            }
        }
        else
        {
            Console.WriteLine("Don't you think you get too much dice? Or too much drink? Think you have to go home!");
            listOfPlayerDice.Clear();
        }
        return result;
    }

    public int FinalResults()
    {
        SortResults();
        int finalResult = 0;
        int pairsCount = 0;
        if (result.Count == 6)
        {
            finalResult = 1200;
        }
        else
        {
            foreach (var item in result)
            {
                switch (item.Value)
                {
                    case 1:
                        if (item.Key == 1)
                        { finalResult += 100; }
                        if (item.Key == 5)
                        { finalResult += 50; }
                        break;
                    case 2:
                        if (item.Key == 1)
                        { finalResult += 100; }
                        if (item.Key == 5)
                        { finalResult += 50; }
                        pairsCount += 1;
                        break;
                    case 3:
                        if (item.Key == 1)
                        {
                            finalResult += 1000;
                        }
                        else
                        {
                            finalResult += item.Key * 100;
                        }
                        break;
                    case 4:
                        if (item.Key == 1)
                        {
                            finalResult += 1000 * 2;
                        }
                        else
                        {
                            finalResult += item.Key * 100 * 2;
                        }
                        break;
                    case 5:
                        if (item.Key == 1)
                        {
                            finalResult += 1000 * 4;
                        }
                        else
                        {
                            finalResult += item.Key * 100 * 4;
                        }
                        break;
                    case 6:
                        if (item.Key == 1)
                        {
                            finalResult += 1000 * 8;
                        }
                        else
                        {
                            finalResult += item.Key * 100 * 8;
                        }
                        break;
                }
                if (pairsCount == 3)
                {
                    finalResult = 800;
                }
            }
        }
        return finalResult;
    }

}


