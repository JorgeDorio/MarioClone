using MarioClone.Moldels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MarioClone;

public class Game1 : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private GameManager _gameManager;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // Globals.WindowSize = new(Map.Tiles[0].Count * Map.TILE_SIZE, Map.Tiles.Count * Map.TILE_SIZE);
        _graphics.PreferredBackBufferWidth = 320;
        _graphics.PreferredBackBufferHeight = 240;
        _graphics.ApplyChanges();

        Globals.Content = Content;
        base.Initialize();
    }

    protected override void LoadContent()
    {
        Globals.SpriteBatch = new SpriteBatch(GraphicsDevice);
        Globals.GraphicsDevice = GraphicsDevice;

        _gameManager = new();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        Globals.Update(gameTime);
        _gameManager.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.SkyBlue);

        _gameManager.Draw();

        base.Draw(gameTime);
    }
}