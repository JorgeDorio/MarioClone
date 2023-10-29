// using System;
// using MarioClone.Managers;
// using Microsoft.Xna.Framework;
// using Microsoft.Xna.Framework.Graphics;
// using Microsoft.Xna.Framework.Input;

// namespace MarioClone.Moldels;

// public class Player
// {
//     private static Texture2D _texture;
//     public Vector2 Position;
//     public Rectangle Frame;
//     private readonly float _speed = 400f;
//     private float _gravity = 580f;
//     private bool _isJumping;
//     private float _jumpSpeed = 580f;
//     private float _verticalVelocity;
//     private float groundLevel = Globals.Graphics.PreferredBackBufferHeight - 192;
//     public float HorizontalVelocity { get; set; }
//     private Vector2 _previousPosition;
//     public Vector2 PreviousPosition => _previousPosition;
//     public bool CanMove { get; set; } = true;

//     public Player(Vector2 pos)
//     {
//         _texture ??= Globals.Content.Load<Texture2D>("assets/player_stopped");
//         Position = pos;
//         Frame = new Rectangle(0, 0, 16, 16);
//         _isJumping = false;
//         _verticalVelocity = 0f;

//     }

//     public void Draw()
//     {
//         Globals.SpriteBatch.Draw(_texture, Position, Frame, Color.White, 0, Vector2.Zero, new Vector2(4, 4), SpriteEffects.None, 1);
//     }

//     public void Update()
//     {
//         var keyboardState = Keyboard.GetState();
//         if (InputManager.Moving && CanMove) // Check CanMove before allowing movement
//         {
//             Position += Vector2.Normalize(InputManager.Direction) * _speed * Globals.TotalSeconds;
//         }
//         _previousPosition = Position;

//         if (InputManager.Moving)
//         {
//             Position += Vector2.Normalize(InputManager.Direction) * _speed * Globals.TotalSeconds;

//         }

//         if (InputManager.Jump && !_isJumping)
//         {
//             _isJumping = true;
//             _verticalVelocity -= _jumpSpeed;
//         }


//         if (keyboardState.GetPressedKeyCount() > 0)
//         {
//             if (keyboardState.IsKeyDown(Keys.A))
//             {
//                 if (HorizontalVelocity > -8)
//                     HorizontalVelocity -= 2f;
//             }
//             if (keyboardState.IsKeyDown(Keys.D))
//             {
//                 if (HorizontalVelocity < 8)
//                     HorizontalVelocity += 2f;
//                 if (Position.X < 400)
//                 {
//                     Position.X += 2f;
//                 }
//             }
//         }
//         else
//         {
//             if (HorizontalVelocity > 0) HorizontalVelocity--;
//             if (HorizontalVelocity < 0) HorizontalVelocity++;
//         }

//         if (_isJumping)
//         {
//             // Apply gravity
//             _verticalVelocity += _gravity * Globals.TotalSeconds;
//             Position.Y += _verticalVelocity * Globals.TotalSeconds;

//             // Check for landing
//             if (Position.Y >= groundLevel)
//             {
//                 Position.Y = groundLevel;
//                 _isJumping = false;
//             }
//         }
//     }
// }