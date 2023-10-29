using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.Moldels;

public class Tileset
{
    public Texture2D Texture { get; private set; }
    public int TileSize { get; private set; }
    public int TileCount { get; private set; }

    public Tileset(Texture2D texture, int tileSize, int tileCount)
    {
        Texture = texture;
        TileSize = tileSize;
        TileCount = tileCount;
    }

    public void Draw(int x, int y)
    {
        Rectangle sourceRectangle = new Rectangle(
            x,
            y,
            TileSize,
            TileSize);

        Globals.SpriteBatch.Draw(Texture, new Vector2(x, y), sourceRectangle, Color.White);
    }
}
