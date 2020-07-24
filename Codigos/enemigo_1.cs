using Godot;
using System;

public class enemigo_1 : Spatial
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    private AnimationNodeStateMachinePlayback playback;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        playback = (AnimationNodeStateMachinePlayback)GetNode<AnimationTree>("AnimationTree").Get("parameters/playback");
        playback.Start("Enemigo_idle001");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
