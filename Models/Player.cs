using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MarioClone.Moldels;

public class Player : Sprite
{
    private const float SPEED = 100f;
    private const float GRAVITY = 625f;
    private const float JUMP = 300f;
    private const int OFFSET = 0;
    private Vector2 _velocity;
    private bool _onGround = true;

    public Player() : base(Globals.Content.Load<Texture2D>("assets/player_stopped"), new(32, 240 - 60))
    {
    }

    private Rectangle CalculateBounds(Vector2 pos)
    {
        return new((int)pos.X, (int)pos.Y, Texture.Width, Texture.Height);
    }

    private void UpdatePosition()
    {
        _onGround = false;
        var newPos = Position + (_velocity * Globals.Time);
        Rectangle newRect = CalculateBounds(newPos);

        foreach (var collider in Map.GetNearestColliders(newRect))
        {
            if (newPos.X != Position.X)
            {
                newRect = CalculateBounds(new(newPos.X, Position.Y));
                if (newRect.Intersects(collider))
                {
                    if (newPos.X > Position.X) newPos.X = collider.Left - Texture.Width + OFFSET;
                    else newPos.X = collider.Right - OFFSET;
                    continue;
                }
            }

            newRect = CalculateBounds(new(Position.X, newPos.Y));
            if (newRect.Intersects(collider))
            {
                if (_velocity.Y > 0)
                {
                    newPos.Y = collider.Top - Texture.Height;
                    _onGround = true;
                    _velocity.Y = 0;
                }
                else
                {
                    newPos.Y = collider.Bottom;
                    _velocity.Y = 0;
                }
            }
        }

        Position = newPos;
    }

    private void UpdateVelocity()
    {
        var keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.A)) _velocity.X = -SPEED;
        else if (keyboardState.IsKeyDown(Keys.D)) _velocity.X = SPEED;
        else _velocity.X = 0;

        if (!_onGround) _velocity.Y += GRAVITY * Globals.Time;

        if (keyboardState.IsKeyDown(Keys.W) && _onGround)
        {
            _velocity.Y = -JUMP;
            _onGround = false;
        }
    }

    public void Update()
    {
        UpdateVelocity();
        UpdatePosition();
    }
}