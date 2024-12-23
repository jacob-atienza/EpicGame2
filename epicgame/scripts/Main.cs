using Godot;
using System;

public partial class Main : Control
{
	public override void _Ready()
	{
		GetNode<TextureButton>("VBoxContainer/start").GrabFocus();
		var planet = GetNode<AnimatedSprite2D>("purple");
		planet.Play("rotate");
		var planet2 = GetNode<AnimatedSprite2D>("saturn");
		planet2.Play("rotate");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_start_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/initialGame.tscn");
	}

	private void _on_options_pressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/options.tscn");
	}

	private void _on_quit_pressed()
	{
		GetTree().Quit();
	}
}
