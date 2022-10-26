using Admixer.App;

Game game = new(9, 9);
Console.WriteLine("Initial matrix");
Game.DisplayMatrix(game.Matrix);
int i = 0;
while (!game.IsOver)
{
    game.Update();
    i++;
}
Console.WriteLine("Final matrix, step " + i);
Game.DisplayMatrix(game.Matrix);

#region Console game visualization
/*Game game2 = new(9, 9);
int j = 0;
while (!game2.IsOver)
{
    Console.WriteLine("Step " + j);
    Game.DisplayMatrix(game2.Matrix);
    game2.Update();
    Game.DisplayMatrix(game2.Indexes);
    j++;
}*/
#endregion