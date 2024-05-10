using Godot;
using System;

public partial class fourleggedMonster : CharacterBody3D
{
	bool inHitRange = false;

	public const float lizardSpeed = 4.0f;
	public const float JumpVelocity = 3.5f;
	public const float lizardRotationVelocity = 3.5f;


	public float lizardgravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	private AnimationTree lizard_anim;
	private AnimationNodeStateMachinePlayback lizard_animPlayback;
	
	[Export] public Vector3 lizardvelocity;
	
	public override void _Ready(){
		lizard_anim = GetNode<AnimationTree>("AnimationTree");
		lizard_animPlayback = (AnimationNodeStateMachinePlayback) lizard_anim.Get("parameters/playback");
		lizard_anim.Active = true;
	}

	public override void _PhysicsProcess(double delta)
	{
		lizardvelocity = Velocity;
		bool punched = false;

		// Add the gravity.
		if (!IsOnFloor())
			lizardvelocity.Y -= lizardgravity * (float)delta;
		else{
			
			if (Input.IsActionJustPressed("spaceAttack"))
				punched = true;
			lizard_anim.Set("parameters/conditions/attack", punched);
		}
		if (lizard_animPlayback.GetCurrentNode() == "attack"){
			lizardvelocity = Vector3.Zero;
			Velocity = lizardvelocity;
			return;
		}

		if(Input.IsActionPressed("selP4")){
			// Add the gravity.
			if (!IsOnFloor())
				lizardvelocity.Y -= lizardgravity * (float)delta;
			else{
				if (Input.IsActionJustPressed("spaceAttack"))
					punched = true;
				lizard_anim.Set("parameters/conditions/attack", punched);
			}
			if (lizard_animPlayback.GetCurrentNode() == "attack"){
				lizardvelocity = Vector3.Zero;
				Velocity = lizardvelocity;
				return;
			}
			float turnStrength = Input.GetAxis("left", "right");
			float moveStrength = Input.GetAxis("forward", "backwards");
			
			RotateY(-Mathf.DegToRad(turnStrength * lizardRotationVelocity));
			Vector3 direction = (Transform.Basis * new Vector3(0, 0, moveStrength)).Normalized();
			if (direction != Vector3.Zero)
			{
				lizardvelocity.X = direction.X * lizardSpeed;
				lizardvelocity.Z = direction.Z * lizardSpeed;
			}
			else
			{
				lizardvelocity.X = Mathf.MoveToward(Velocity.X, 0, lizardSpeed);
				lizardvelocity.Z = Mathf.MoveToward(Velocity.Z, 0, lizardSpeed);
			}

			Velocity = lizardvelocity;
			MoveAndSlide();
		}
	}
	public void isInHitBox(Area3D area){
		GD.Print("Fight!!");
	}
}
