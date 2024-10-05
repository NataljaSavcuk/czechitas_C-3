using System;
public class GreedParsingService
{
    /// <summary>
    /// Parse string to number
    /// </summary>
    /// <param name="userResult">Part of user input after splitting and trimming</param>
    /// <returns> int number or 0 if input is NAN</returns>
    private int StringToNumber(string userResult)
    {
        if (int.TryParse(userResult, out int diceResult))
        {
            return diceResult;
        }
        return 0;
    }

    /// <summary>
    /// Valid is number is agree with game rules (int, from 1 to 6)
    /// </summary>
    /// <param name="userResult">Part of user input after splitting and trimming</param>
    /// <returns> int from 1 to 6 or 0 if input is incorrect</returns>
    private int ValidateResult(string userResult)
    {
        var diceResult = StringToNumber(userResult);
        if (diceResult >= 1 && diceResult <= 6)
        {
            return diceResult;
        }
        return 0;
    }

    /// <summary>
    /// Ensure no more than 6 dice values
    /// </summary>
    /// <param name="listOfPlayerDice">List<int>, each item is from 1 to 6</param>
    /// <returns>True if user have 6 or less dice results</returns>
    private bool IsListValid(List<int> listOfPlayerDice)
    {
        return listOfPlayerDice.Count <= 6;
    }

    /// <summary>
    /// Repeat asking for result till get valid numbers
    /// </summary>
    /// <returns>List<int>, each item is from 1 to 6</returns>
    public List<int> GetPlayerResults()
    {
        var listOfPlayerDice = new List<int>();

        while (listOfPlayerDice.Count == 0)
        {
            Console.WriteLine("Tell me please your results separated by commas (e.g., 1,2,3,5,6):");
            string userInputResults = Console.ReadLine();

            if (!string.IsNullOrEmpty(userInputResults))
            {
                string[] userResults = userInputResults.Split(',');
                foreach (var userResult in userResults)
                {
                    var diceResult = ValidateResult(userResult.Trim());
                    if (diceResult != 0)
                    {
                        listOfPlayerDice.Add(diceResult);
                    }
                    else
                    {
                        Console.WriteLine("Where did you get this dice? Try once more with correct one!");
                        Console.WriteLine();
                        listOfPlayerDice.Clear();
                    }
                }
            }
            if (!IsListValid(listOfPlayerDice))
            {
                Console.WriteLine("Don't you think you get too much dice? Or too much drink? Try once more!");
                Console.WriteLine();
                listOfPlayerDice.Clear();
            }
        }
        return listOfPlayerDice;
    }

}
