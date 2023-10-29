using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MarioClone.Moldels;

public class Animation
{
    private readonly Texture2D Texture;
    private readonly List<Rectangle> SourceRectangles = new();
    private readonly int Frames;
    private int Frame;
    private readonly float FrameTime;
    private float FrameTimeLeft;
    private bool Active = true;

    public Animation(Texture2D texture, int frameX, int frameY, float frameTime, int row = 1)
    {
        Texture = texture;
        FrameTime = frameTime;
        FrameTimeLeft = FrameTime;
        Frames = frameX;
        var frameWidth = Texture.Width / frameX;
        var frameHeight = Texture.Height / frameY;

        for (int i = 0; i < Frames; i++)
        {
            SourceRectangles.Add(new(i * frameWidth, (row - 1) * frameHeight, 16, 16));
        }
    }

    public void Stop()
    {
        Active = false;
    }

    public void Start()
    {
        Active = true;
    }

    public void Reset()
    {
        Frame = 0;
        FrameTimeLeft = FrameTime;
    }

    public void Update()
    {
        if (!Active) return;

        FrameTimeLeft -= Globals.Time;

        if (FrameTimeLeft <= 0)
        {
            FrameTimeLeft += FrameTime;
            Frame = (Frame + 1) % Frames;
        }
    }

    public void Draw(Vector2 pos)
    {
        Globals.SpriteBatch.Draw(Texture, pos, SourceRectangles[Frame], Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 1);
    }
}