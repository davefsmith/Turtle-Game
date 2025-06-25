using Godot;
using System;

public partial class Jeffery : Area2D
{
	[Export]
	public int Acc {get; set;} = 100;
	[Export]
	public Vector2 Velocity = Vector2.Zero;
	public Vector2 ScreenSize;


	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
	}

	public override void _Process(double delta)
	{
		var movementAcc = Vector2.Zero; // The player's movement vector.
		if (Input.IsActionPressed("move_right"))
		{
			movementAcc.X += 1;
		}
		if (Input.IsActionPressed("move_left"))
		{
			movementAcc.X -= 1;
		}
		if (Input.IsActionPressed("move_down"))
		{
			movementAcc.Y += 1;
		}
		if (Input.IsActionPressed("move_up"))
		{
			movementAcc.Y -= 1;
		}
		movementAcc = movementAcc.Normalized() * Acc;
		Vector2 drag = Velocity.Sign() * -0.5f * Velocity.LengthSquared() / Acc;
		
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		
		if (movementAcc.LengthSquared() > 0)
		{
			animatedSprite2D.Play();
			animatedSprite2D.SetSpeedScale((movementAcc.LengthSquared()-drag.LengthSquared())/(Acc*Acc)+0.1f);
			Rotation = movementAcc.Angle() + 0.7156f;
		}
		else
		{
			animatedSprite2D.Stop();
		}
		
		Velocity += (movementAcc + drag) * (float)delta;
		Position += Velocity * (float)delta;
		Position = new Vector2(	x: Mathf.Clamp(Position.X, 0, ScreenSize.X), y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y));
	}
}
