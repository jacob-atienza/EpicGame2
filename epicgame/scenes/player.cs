using Godot;
using System;

public partial class player : CharacterBody2D
{
	private AnimationPlayer _animationPlayer;
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;
	public override void _PhysicsProcess(double delta)
	{
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "", "");
		if (direction != Vector2.Zero)
		{
			_animationPlayer.Play("walking");
			if (direction.X == 0)
			{
				return;
			}
			GetNode<Sprite2D>("playerSprite").FlipH = direction.X < 0;
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
