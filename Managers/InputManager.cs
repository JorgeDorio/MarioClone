using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MarioClone.Managers;

public static class InputManager
{
    private static Vector2 _direction;
    public static Vector2 Direction => _direction;
    public static bool Moving => _direction != Vector2.Zero;
    public static bool Jump => Keyboard.GetState().IsKeyDown(Keys.W);

    public static void Update()
    {
        _direction = Vector2.Zero;
    }
}