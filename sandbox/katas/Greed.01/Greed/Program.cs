var parsingService = new GreedParsingService();
var evaluateService = new GreedEvaluateService();
Console.WriteLine("Hi there! Wellcome to our game! Have you already rolled the dice?");
var listOfPlayerDice = parsingService.GetPlayerResults();
Console.WriteLine("Your great result is: " + evaluateService.FinalResults(listOfPlayerDice));
