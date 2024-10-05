using System;
using System.Collections.Generic;

public class GreedEvaluateService
{
    // Create histogram array to store counts for each dice value (index 0 for 1, index 1 for 2, etc.)
    int[] histogram = new int[6];

    /// <summary>
    /// Increment the count for this dice value
    /// </summary>
    /// <param name="listOfPlayerDice">list of valid player results</param>
    private void BuildHistogram(List<int> listOfPlayerDice)
    {
        Array.Clear(histogram, 0, histogram.Length);

        foreach (var dice in listOfPlayerDice)
        {
            histogram[dice - 1]++;
        }
    }

    /// <summary>
    /// Evaluating the user results
    /// </summary>
    /// <param name="listOfPlayerDice">list of valid player results</param>
    /// <returns>int result in points</returns>
    public int FinalResults(List<int> listOfPlayerDice)
    {
        BuildHistogram(listOfPlayerDice);
        int finalResult = 0;
        int pairsCount = 0;

        for (int i = 0; i < histogram.Length; i++)
        {
            int count = histogram[i];
            int diceValue = i + 1;

            switch (count)
            {
                case 1:
                    if (diceValue == 1)
                        finalResult += 100;
                    if (diceValue == 5)
                        finalResult += 50;
                    break;
                case 2:
                    if (diceValue == 1)
                        finalResult += 200;
                    if (diceValue == 5)
                        finalResult += 100;
                    pairsCount++;
                    break;
                case 3:
                    finalResult += (diceValue == 1) ? 1000 : diceValue * 100;
                    break;
                case 4:
                    finalResult += (diceValue == 1) ? 2000 : diceValue * 200;
                    break;
                case 5:
                    finalResult += (diceValue == 1) ? 4000 : diceValue * 400;
                    break;
                case 6:
                    finalResult += (diceValue == 1) ? 8000 : diceValue * 800;
                    break;
            }
        }
        //If it's Three Pairs
        if (pairsCount == 3)
        {
            finalResult = 800;
        }
        // If it's Straight
        if (listOfPlayerDice.Count == 6 && new HashSet<int>(listOfPlayerDice).Count == 6)
        {
            finalResult = 1200;
        }
        return finalResult;
    }
}
