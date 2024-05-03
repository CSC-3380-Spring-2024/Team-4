using Godot;
using System;


public partial class scorpion2 : CharacterBody3D
{
	public const float scorpSpeed = 4.0f;
	//public const float JumpVelocity = 4.5f;
	public const float scorpRotationVelocity = 3.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float scorpgravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	
	private AnimationTree scorp_anim;
	private AnimationNodeStateMachinePlayback scorp_animPlayback;
	[Export] public Vector3 scorpvelocity;
	
	public override void _Ready(){
		scorp_anim = GetNode<AnimationTree>("AnimationTree");
		scorp_animPlayback = (AnimationNodeStateMachinePlayback) scorp_anim.Get("parameters/playback");
		scorp_anim.Active = true;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		scorpvelocity = Velocity;
		bool punched = false;

		// Add the gravity.
		if (!IsOnFloor())
			scorpvelocity.Y -= scorpgravity * (float)delta;
		else{
			if (Input.IsActionJustPressed("spaceAttack"))
				punched = true;
			scorp_anim.Set("parameters/conditions/attack", punched);
		}

		// Handle Jump.
		if (scorp_animPlayback.GetCurrentNode() == "attack"){
			scorpvelocity = Vector3.Zero;
			Velocity = scorpvelocity;
		}
			//fishMonstervelocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		
		float turnStrength = Input.GetAxis("left", "right");
		float moveStrength = Input.GetAxis("forward", "backwards");
		RotateY(-Mathf.DegToRad(turnStrength * scorpRotationVelocity));
		Vector3 direction = (Transform.Basis * new Vector3(0, 0, moveStrength)).Normalized();
		
		if (direction != Vector3.Zero)
		{
			scorpvelocity.X = direction.X * scorpSpeed;
			scorpvelocity.Z = direction.Z * scorpSpeed;
		}
		else
		{
			scorpvelocity.X = Mathf.MoveToward(Velocity.X, 0, scorpSpeed);
			scorpvelocity.Z = Mathf.MoveToward(Velocity.Z, 0, scorpSpeed);
		}

		Velocity = scorpvelocity;
		MoveAndSlide();
	}
}
