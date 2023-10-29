using MarioClone;
using MarioClone.Moldels;
using Microsoft.Xna.Framework;

public class GameManager
{
    private readonly Map _map;
    private readonly Player _player;
    private Matrix _translation;

    public GameManager()
    {
        _map = new();
        _player = new();
    }

    public void Update()
    {
        CalculateTranslation();
        _player.Update();
    }

    public void Draw()
    {
        Globals.SpriteBatch.Begin(transformMatrix: _translation);
        _map.Draw();
        _player.Draw();
        Globals.SpriteBatch.End();
    }

    private void CalculateTranslation()
    {
        var dx = (Globals.WindowSize.X / 2) - _player.Position.X;
        dx = MathHelper.Clamp(dx, -_map.MapSize.X + Globals.WindowSize.X + (Map.TILE_SIZE / 2), 0);
        var dy = (Globals.WindowSize.Y / 2) - _player.Position.Y;
        dy = MathHelper.Clamp(dy, -_map.MapSize.Y + Globals.WindowSize.Y + (Map.TILE_SIZE / 2), Map.TILE_SIZE / 2);
        _translation = Matrix.CreateTranslation(dx, dy, 0f);
    }
}