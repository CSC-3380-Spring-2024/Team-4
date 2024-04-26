using Godot;
using System;

public partial class wereWolfBro : CharacterBody3D
{
	public const float wereSpeed = 4.0f;
	public const float wereJumpVelocity = 4.5f;
	public const float wereRotationVelocity = 3.5f;
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float weregravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float leechgravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	
	private AnimationTree were_anim;
	private AnimationNodeStateMachinePlayback were_animPlayback;
	
	[Export] public Vector3 werevelocity;
	
	public override void _Ready(){
		were_anim = GetNode<AnimationTree>("AnimationTree");
		were_animPlayback = (AnimationNodeStateMachinePlayback) were_anim.Get("parameters/playback");
		were_anim.Active = true;
	}
	public override void _PhysicsProcess(double delta)
	{
		werevelocity = Velocity;
		bool punched = false;

		// Add the gravity.
		if (!IsOnFloor())
			werevelocity.Y -= weregravity * (float)delta;
		else{
			if (Input.IsActionJustPressed("spaceAttack"))
				punched = true;
			were_anim.Set("parameters/conditions/attack", punched);
		}
		if (were_animPlayback.GetCurrentNode() == "attack"){
			werevelocity = Vector3.Zero;
			Velocity = werevelocity;
			return;
		}

		// Handle Jump.
		

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		
		float turnStrength = Input.GetAxis("left", "right");
		float moveStrength = Input.GetAxis("forward", "backwards");
		
		RotateY(-Mathf.DegToRad(turnStrength * wereRotationVelocity));
		Vector3 direction = (Transform.Basis * new Vector3(0, 0, moveStrength)).Normalized();
		
		if (direction != Vector3.Zero)
		{
			werevelocity.X = direction.X * wereSpeed;
			werevelocity.Z = direction.Z * wereSpeed;
		}
		else
		{
			werevelocity.X = Mathf.MoveToward(Velocity.X, 0, wereSpeed);
			werevelocity.Z = Mathf.MoveToward(Velocity.Z, 0, wereSpeed);
		}

		Velocity = werevelocity;
		MoveAndSlide();
	}
}
