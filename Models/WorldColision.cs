
// using System;
// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;

// namespace MarioClone.Moldels;

// public class WorldColision
// {
//     private static Texture2D _texture;
//     private Vector2 _pos;
//     private Rectangle _frame;
//     private Player _player;
//     public Color[] Colision;


//     public WorldColision(Vector2 pos, Player player)
//     {
//         _texture ??= Globals.Content.Load<Texture2D>("assets/world_colision");
//         _pos = pos;
//         _frame = new(0, 0, 3376, 240);
//         _player = player;
//         Colision = new Color[_texture.Width * _texture.Height];

//         _texture.GetData(Colision);
//     }

//     public void Draw()
//     {
//         Globals.SpriteBatch.Draw(_texture, _pos, _frame, Color.White, 0, Vector2.Zero, new Vector2(4, 4), SpriteEffects.None, 1);
//         _pos.X -= _player.HorizontalVelocity;
//         CheckCollision();

//         if (IsColliding(_player.PreviousPosition, _player.Frame))
//         {
//             // A collision is detected, prevent player movement
//             _player.CanMove = false;
//             // Optionally, you can add logic to resolve the collision here.
//         }
//         else
//         {
//             _player.CanMove = true;
//         }
//     }

//     private void CheckCollision()
//     {
//         int playerLeft = (int)_player.Position.X;
//         int playerTop = (int)_player.Position.Y;
//         int playerRight = playerLeft + _player.Frame.Width;
//         int playerBottom = playerTop + _player.Frame.Height;

//         int worldCollisionWidth = _texture.Width;
//         int worldCollisionHeight = _texture.Height;

//         for (int y = 0; y < worldCollisionHeight; y++)
//         {
//             for (int x = 0; x < worldCollisionWidth; x++)
//             {
//                 if (Colision[y * worldCollisionWidth + x] == Color.Red)
//                 {
//                     int collisionLeft = (int)(_pos.X + x * 4);
//                     int collisionTop = (int)(_pos.Y + y * 4);
//                     int collisionRight = collisionLeft + 4;
//                     int collisionBottom = collisionTop + 4;

//                     if (playerRight > collisionLeft && playerLeft < collisionRight && playerBottom > collisionTop && playerTop < collisionBottom)
//                     {
//                         // Collision detected, handle it here
//                         // For example, you can set a flag or perform other actions
//                         // when a collision occurs.
//                     }
//                 }
//             }
//         }
//     }

//     private bool IsColliding(Vector2 playerPosition, Rectangle playerFrame)
//     {
//         int playerLeft = (int)playerPosition.X;
//         int playerTop = (int)playerPosition.Y;
//         int playerRight = playerLeft + playerFrame.Width;
//         int playerBottom = playerTop + playerFrame.Height;

//         int worldCollisionWidth = _texture.Width;
//         int worldCollisionHeight = _texture.Height;

//         for (int y = 0; y < worldCollisionHeight; y++)
//         {
//             for (int x = 0; x < worldCollisionWidth; x++)
//             {
//                 if (Colision[y * worldCollisionWidth + x] == Color.Red)
//                 {
//                     int collisionLeft = (int)(_pos.X + x * 4);
//                     int collisionTop = (int)(_pos.Y + y * 4);
//                     int collisionRight = collisionLeft + 4;
//                     int collisionBottom = collisionTop + 4;

//                     if (playerRight > collisionLeft && playerLeft < collisionRight && playerBottom > collisionTop && playerTop < collisionBottom)
//                     {
//                         Console.WriteLine("teste");
//                         return true;
//                     }
//                 }
//             }
//         }

//         return false;
//     }
// }