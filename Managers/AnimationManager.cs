using System;
using System.Collections.Generic;
using MarioClone.Moldels;
using Microsoft.Xna.Framework;

namespace MarioClone.Managers;

public class AnimationManager
{
    private readonly Dictionary<object, Animation> Animations = new();
    private object LastKey;

    public void AddAnimation(object key, Animation animation)
    {
        Animations.Add(key, animation);
        LastKey ??= key;
    }

    public void Update(object key)
    {
        if (Animations.ContainsKey(key))
        {
            Animations[key].Start();
            Animations[key].Update();
            LastKey = key;
        }
        else
        {
            Animations[LastKey].Reset();
            Animations[LastKey].Stop();
        }
    }

    public void Draw(Vector2 pos)
    {
        Animations[LastKey].Draw(pos);
    }
}