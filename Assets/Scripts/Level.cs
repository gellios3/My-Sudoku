using SimpleJSON;

public class Level
{
    private readonly JSONNode _node;

    public int[,] Board { get; } = new int[9, 9];

    public Level(JSONNode node )
    {
        _node = node;

        DeserializeBoard();
    }

    private void DeserializeBoard()
    {
        for (var y = 0; y < 9; ++y)
        {
            for (var x = 0; x < 9; ++x)
            {
                Board[x, y] = _node[y][x].AsInt;
            }
        }
    }
}