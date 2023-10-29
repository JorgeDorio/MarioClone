using MarioClone;
using MarioClone.Moldels;

public class GameManager
{
    private readonly Map _map;
    private readonly Player _player;

    public GameManager()
    {
        _map = new();
        _player = new();
    }

    public void Update()
    {
        _player.Update();
    }

    public void Draw()
    {
        Globals.SpriteBatch.Begin();
        _map.Draw();
        _player.Draw();
        Globals.SpriteBatch.End();
    }
}