
// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;

// namespace MarioClone.Moldels;

// public class World
// {
//     private static Texture2D _texture;
//     private Vector2 _pos;
//     private Rectangle _frame;
//     private Player _player;

//     public World(Vector2 pos, Player player)
//     {
//         _texture ??= Globals.Content.Load<Texture2D>("assets/world");
//         _pos = pos;
//         _frame = new(0, 0, 3376, 240);
//         _player = player;
//     }

//     public void Draw()
//     {
//         Globals.SpriteBatch.Draw(_texture, _pos, _frame, Color.White, 0, Vector2.Zero, new Vector2(4, 4), SpriteEffects.None, 1);
//         _pos.X -= _player.HorizontalVelocity;
//     }
// }