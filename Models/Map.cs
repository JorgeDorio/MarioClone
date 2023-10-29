
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.Moldels;

public class Map
{
    private readonly RenderTarget2D _target;
    public static readonly int TILE_SIZE = 16;

    public static readonly List<List<int>> Tiles = new();
    private static Rectangle[,] Colliders { get; set;}

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
        Colliders = new Rectangle[Tiles.Count, Tiles[0].Count];

        _target = new(Globals.GraphicsDevice, Tiles.Count * TILE_SIZE, Tiles[0].Count * TILE_SIZE);

        var tile1tex = Globals.Content.Load<Texture2D>("assets/world/tile1");
        var tile2tex = Globals.Content.Load<Texture2D>("assets/world/tile2");

        Globals.GraphicsDevice.SetRenderTarget(_target);
        Globals.GraphicsDevice.Clear(Color.Transparent);
        Globals.SpriteBatch.Begin();

        for (int x = 0; x < Tiles.Count; x++)
        {
            for (int y = 0; y < Tiles[0].Count; y++)
            {
                if (Tiles[x][y] == 0) continue;
                var posX = y * TILE_SIZE;
                var posY = x * TILE_SIZE;
                var tex = Tiles[x][y] == 1 ? tile1tex : tile2tex;
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

// using System;
// using System.Collections.Generic;
// using System.IO;
// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;

// namespace MarioClone.Moldels;

// public class Map
// {
//     public readonly RenderTarget2D Target;
//     public static readonly int TILE_SIZE = 16;

//     public static readonly List<List<int>> Tiles = new();

//     private static void GetTileMap()
//     {
//         var path = "Content/tilemap";

//         foreach (var line in File.ReadLines(path))
//         {
//             var tileList = new List<int>();
//             foreach (var tileString in line.Split(","))
//             {

//                 if (!string.IsNullOrEmpty(tileString))
//                     tileList.Add(int.Parse(tileString));
//             }
//             Tiles.Add(tileList);
//         }
//     }

//     public Map()
//     {
//         GetTileMap();

//         Target = new(Globals.GraphicsDevice, Tiles.Count * TILE_SIZE, Tiles[0].Count * TILE_SIZE);
//         var tileTexture = Globals.Content.Load<Texture2D>("assets/world_tiles");
//         Tileset tileset = new(tileTexture, 16, 8);

//         Globals.GraphicsDevice.SetRenderTarget(Target);
//         Globals.GraphicsDevice.Clear(Color.Transparent);
//         Globals.SpriteBatch.Begin();

//         for (var y = 0; y < Tiles.Count; y++)
//         {
//             for (var x = 0; x < Tiles.Count; x++)
//             {
//                 if (Tiles[x][y] == 0) continue;
//                 var posX = x * TILE_SIZE;
//                 var posY = y * TILE_SIZE;

//                 switch (Tiles[x][y])
//                 {
//                     case 1:
//                         tileset.Draw(0, 0);
//                         break;
//                     case 2:
//                         tileset.Draw(16, 16);
//                         break;
//                     default:
//                         continue;
//                 }
//             }
//         }
//     }

//     public void Draw()
//     {
//         Globals.SpriteBatch.Draw(Target, Vector2.Zero, Color.White);
//     }
// }