using Godot;
using System;

public partial class hide_behind : CharacterBody3D
{
	public const float hideBeSpeed = 4.0f;
	public const float hideBeJumpVelocity = 4.5f;
	public const float hideBeRotationVelocity = 3.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float hideBegravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	
	private AnimationTree hideBe_anim;
	private AnimationNodeStateMachinePlayback hideBe_animPlayback;
	
	[Export] public Vector3 hideBevelocity;
	
	public override void _Ready(){
		hideBe_anim = GetNode<AnimationTree>("AnimationTree");
		hideBe_animPlayback = (AnimationNodeStateMachinePlayback) hideBe_anim.Get("parameters/playback");
		hideBe_anim.Active = true;
	}
	public override void _PhysicsProcess(double delta)
	{
		hideBevelocity = Velocity;
		bool punched = false;

		// Add the gravity.
		if (!IsOnFloor())
			hideBevelocity.Y -= hideBegravity * (float)delta;
		else{
			// Handle Jump.
			//if (Input.IsActionJustPressed("ui_accept") && IsOnFloor() )
				//velocity.Y = JumpVelocity;
			if (Input.IsActionJustPressed("spaceAttack"))
				punched = true;
			hideBe_anim.Set("parameters/conditions/attack", punched);
		}
		
		if (hideBe_animPlayback.GetCurrentNode() == "attack"){
			hideBevelocity = Vector3.Zero;
			Velocity = hideBevelocity;
			return;
		}
		
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		float turnStrength = Input.GetAxis("left", "right");
		float moveStrength = Input.GetAxis("forward", "backwards");
		
		RotateY(-Mathf.DegToRad(turnStrength * hideBeRotationVelocity));
		Vector3 direction = (Transform.Basis * new Vector3(0, 0, moveStrength)).Normalized();
		if (direction != Vector3.Zero)
		{
			hideBevelocity.X = direction.X * hideBeSpeed;
			hideBevelocity.Z = direction.Z * hideBeSpeed;
		}
		else
		{
			hideBevelocity.X = Mathf.MoveToward(Velocity.X, 0, hideBeSpeed);
			hideBevelocity.Z = Mathf.MoveToward(Velocity.Z, 0, hideBeSpeed);
		}

		Velocity = hideBevelocity;
		MoveAndSlide();
	}
}
