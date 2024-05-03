using Godot;
using System;

public partial class axMonster : CharacterBody3D
{
	public const float axSpeed = 4.0f;
	public const float axJumpVelocity = 4.5f;
	public const float axRotationVelocity = 3.5f;
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float axgravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float leechgravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	
	private AnimationTree ax_anim;
	private AnimationNodeStateMachinePlayback ax_animPlayback;
	
	[Export] public Vector3 axvelocity;
	
	public override void _Ready(){
		ax_anim = GetNode<AnimationTree>("AnimationTree");
		ax_animPlayback = (AnimationNodeStateMachinePlayback) ax_anim.Get("parameters/playback");
		ax_anim.Active = true;
	}
	public override void _PhysicsProcess(double delta)
	{
		axvelocity = Velocity;
		bool punched = false;

		// Add the gravity.
		if (!IsOnFloor())
			axvelocity.Y -= axgravity * (float)delta;
		else{
			if (Input.IsActionJustPressed("spaceAttack"))
				punched = true;
			ax_anim.Set("parameters/conditions/attack", punched);
		}
		if (ax_animPlayback.GetCurrentNode() == "attack"){
			axvelocity = Vector3.Zero;
			Velocity = axvelocity;
			return;
		}

		// Handle Jump.
		

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		
		float turnStrength = Input.GetAxis("left", "right");
		float moveStrength = Input.GetAxis("forward", "backwards");
		
		RotateY(-Mathf.DegToRad(turnStrength * axRotationVelocity));
		Vector3 direction = (Transform.Basis * new Vector3(0, 0, moveStrength)).Normalized();
		
		if (direction != Vector3.Zero)
		{
			axvelocity.X = direction.X * axSpeed;
			axvelocity.Z = direction.Z * axSpeed;
		}
		else
		{
			axvelocity.X = Mathf.MoveToward(Velocity.X, 0, axSpeed);
			axvelocity.Z = Mathf.MoveToward(Velocity.Z, 0, axSpeed);
		}

		Velocity = axvelocity;
		MoveAndSlide();
	}
}
