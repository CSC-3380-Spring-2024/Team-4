using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	public override void _PhysicsProcess(double delta)
	{
		Godot.Sprite2D child =this.GetNode<Godot.Sprite2D>("Player");

		float Amnt = 5;
		if (Input.IsKeyPressed(Key.W)){
			this.Position += new Vector2(0, -Amnt);
			child.GlobalPosition = new Vector2(0, 0);
		}if (Input.IsKeyPressed(Key.S)){
			this.Position += new Vector2(0, Amnt);
		}if (Input.IsKeyPressed(Key.A)){
			this.Position += new Vector2(-Amnt, 0);
		}if (Input.IsKeyPressed(Key.D)){
			this.Position += new Vector2(Amnt, 0);
		}
	}
}
