using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.Moldels;

public class Sprite
{
    public Texture2D Texture { get; }
    public Vector2 Position;
    public Vector2 Origin { get; protected set; }

    public Sprite(Texture2D texture, Vector2 position)
    {
        Texture = texture;
        Position = position;
        Origin = new(Texture.Width / 8, Texture.Height / 2);
    }

    public virtual void Draw()
    {
        Globals.SpriteBatch.Draw(Texture, Position, Color.White);
    }
}