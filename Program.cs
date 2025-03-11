
using Chess;

GameSettings gameSettings = new GameSettings();
var (c1, c2, c3, c4) = gameSettings.SelectSettings();
Game game = new Game(c1, c2, c3, c4);
Console.OutputEncoding = System.Text.Encoding.Unicode;


Console.ForegroundColor = ConsoleColor.Black;
while (true)
{
    game.Print();
    game.CursorAction();
}
