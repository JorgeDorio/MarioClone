
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.Moldels;

public class Map
{
    private readonly RenderTarget2D _target;
    public Point MapSize { get; private set; }
    public static readonly int TILE_SIZE = 16;

    public static readonly List<List<int>> Tiles = new();
    private static Rectangle[,] Colliders { get; set; }

    private static void GetTileMap()
    {
        var path = "Content/tilemap";

        foreach (var line in File.ReadLines(path))
        {
            var tileList = new List<int>();
            foreach (var tileString in line.Split(","))
            {

                if (!string.IsNullOrEmpty(tileString))
                    tileList.Add(int.Parse(tileString));
            }
            Tiles.Add(tileList);
        }
    }


    public Map()
    {
        GetTileMap();
        MapSize = new(Tiles[0].Count * TILE_SIZE, Tiles.Count * TILE_SIZE);

        Colliders = new Rectangle[Tiles.Count, Tiles[0].Count];

        _target = new(Globals.GraphicsDevice, Tiles[0].Count * TILE_SIZE, Tiles.Count * TILE_SIZE);

        var tile1tex = Globals.Content.Load<Texture2D>("assets/world/tile1");
        var tile2tex = Globals.Content.Load<Texture2D>("assets/world/tile2");
        var tile3tex = Globals.Content.Load<Texture2D>("assets/world/tile3");
        var tile4tex = Globals.Content.Load<Texture2D>("assets/world/tile4");
        var tile5tex = Globals.Content.Load<Texture2D>("assets/world/tile5");
        var tile6tex = Globals.Content.Load<Texture2D>("assets/world/tile6");
        var tile7tex = Globals.Content.Load<Texture2D>("assets/world/tile7");
        var tile8tex = Globals.Content.Load<Texture2D>("assets/world/tile8");

        Globals.GraphicsDevice.SetRenderTarget(_target);
        Globals.GraphicsDevice.Clear(Color.Transparent);
        Globals.SpriteBatch.Begin();
        Texture2D tex;

        for (int x = 0; x < Tiles.Count; x++)
        {
            for (int y = 0; y < Tiles[0].Count; y++)
            {
                if (Tiles[x][y] == 0) continue;
                var posX = y * TILE_SIZE;
                var posY = x * TILE_SIZE;
                switch (Tiles[x][y])
                {
                    case 1:
                        tex = tile1tex;
                        break;
                    case 2:
                        tex = tile2tex;
                        break;
                    case 3:
                        tex = tile3tex;
                        break;
                    case 4:
                        tex = tile4tex;
                        break;
                    case 5:
                        tex = tile5tex;
                        break;
                    case 6:
                        tex = tile6tex;
                        break;
                    case 7:
                        tex = tile7tex;
                        break;
                    case 8:
                        tex = tile8tex;
                        break;
                    default:
                        continue;
                }

                Colliders[x, y] = new(posX, posY, TILE_SIZE, TILE_SIZE);

                Globals.SpriteBatch.Draw(tex, new Vector2(posX, posY), Color.White);
            }
        }

        Globals.SpriteBatch.End();
        Globals.GraphicsDevice.SetRenderTarget(null);
    }

    public static List<Rectangle> GetNearestColliders(Rectangle bounds)
    {
        int leftTile = (int)Math.Floor((float)bounds.Left / TILE_SIZE);
        int rightTile = (int)Math.Ceiling((float)bounds.Right / TILE_SIZE) - 1;
        int topTile = (int)Math.Floor((float)bounds.Top / TILE_SIZE);
        int bottomTile = (int)Math.Ceiling((float)bounds.Bottom / TILE_SIZE) - 1;

        leftTile = MathHelper.Clamp(leftTile, 0, Tiles[0].Count);
        rightTile = MathHelper.Clamp(rightTile, 0, Tiles[0].Count);
        topTile = MathHelper.Clamp(topTile, 0, Tiles.Count);
        bottomTile = MathHelper.Clamp(bottomTile, 0, Tiles.Count);

        List<Rectangle> result = new();

        for (int x = topTile; x <= bottomTile; x++)
        {
            for (int y = leftTile; y <= rightTile; y++)
            {
                if (Tiles[x][y] != 0) result.Add(Colliders[x, y]);
            }
        }

        return result;
    }

    public void Draw()
    {
        Globals.SpriteBatch.Draw(_target, Vector2.Zero, Color.White);
    }
}