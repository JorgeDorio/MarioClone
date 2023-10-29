using MarioClone.Managers;
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
    private readonly AnimationManager _animationManager = new();
    public static Vector2 Direction;

    public Player() : base(Globals.Content.Load<Texture2D>("assets/player_walking"), new(32, 240 - 60))
    {
        _animationManager.AddAnimation(new Vector2(1, 0), new(Texture, 4, 1, 0.1f));
        _animationManager.AddAnimation(new Vector2(-1, 0), new(Texture, 4, 1, 0.1f));
    }

    private Rectangle CalculateBounds(Vector2 pos)
    {
        return new((int)pos.X, (int)pos.Y, Texture.Width / 4, Texture.Height);
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
                    if (newPos.X > Position.X) newPos.X = collider.Left - Texture.Width / 4 + OFFSET;
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
        Direction = Vector2.Zero;

        if (keyboardState.IsKeyDown(Keys.A)) { _velocity.X = -SPEED; Direction.X--; }
        else if (keyboardState.IsKeyDown(Keys.D)) { _velocity.X = SPEED; Direction.X++; }
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

        _animationManager.Update(Direction);
    }


    public override void Draw()
    {
        _animationManager.Draw(Position);
    }
}