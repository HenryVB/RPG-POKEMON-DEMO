using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator
{
    private SpriteRenderer sr;
    private List<Sprite> frames;
    private float frameRate;

    private int currentFrame;
    private float timer;

    public List<Sprite> Frames { get => frames; set => frames = value; }

    public SpriteAnimator(List<Sprite> frames, SpriteRenderer sr, float frameRate=0.16f)
    {
        this.sr = sr;
        this.Frames = frames;
        this.frameRate = frameRate;
    }

    public void Start() {
        currentFrame = 0;
        timer = 0f;
        sr.sprite = Frames[0];
    }

    public void HandleUpdate()
    {
        timer += Time.deltaTime;
        if(timer > frameRate)
        {
            currentFrame = (currentFrame + 1) % Frames.Count;
            sr.sprite = Frames[currentFrame];
            timer -= frameRate;
        }
    }

}
