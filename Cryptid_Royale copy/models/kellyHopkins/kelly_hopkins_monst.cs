using Godot;
using System;

public partial class kelly_hopkins_monst : CharacterBody3D
{
	public const float kellyHopsSpeed = 4.0f;
	public const float kellyHopsJumpVelocity = 4.5f;
	public const float kellyHopsRotationVelocity = 3.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float kellyHopsgravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	
	private AnimationTree kellyHops_anim;
	private AnimationNodeStateMachinePlayback kellyHops_animPlayback;
	
	[Export] public Vector3 kellyHopsvelocity;
	
	public override void _Ready(){
		kellyHops_anim = GetNode<AnimationTree>("AnimationTree");
		kellyHops_animPlayback = (AnimationNodeStateMachinePlayback) kellyHops_anim.Get("parameters/playback");
		kellyHops_anim.Active = true;
	}
	public override void _PhysicsProcess(double delta)
	{
		kellyHopsvelocity = Velocity;
		bool punched = false;

		// Add the gravity.
		if (!IsOnFloor())
			kellyHopsvelocity.Y -= kellyHopsgravity * (float)delta;
		else{
			// Handle Jump.
			//if (Input.IsActionJustPressed("ui_accept") && IsOnFloor() )
				//velocity.Y = JumpVelocity;
			if (Input.IsActionJustPressed("spaceAttack"))
				punched = true;
			kellyHops_anim.Set("parameters/conditions/attack", punched);
		}
		
		if (kellyHops_animPlayback.GetCurrentNode() == "attack"){
			kellyHopsvelocity = Vector3.Zero;
			Velocity = kellyHopsvelocity;
			return;
		}
		
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		float turnStrength = Input.GetAxis("left", "right");
		float moveStrength = Input.GetAxis("forward", "backwards");
		
		RotateY(-Mathf.DegToRad(turnStrength * kellyHopsRotationVelocity));
		Vector3 direction = (Transform.Basis * new Vector3(0, 0, moveStrength)).Normalized();
		if (direction != Vector3.Zero)
		{
			kellyHopsvelocity.X = direction.X * kellyHopsSpeed;
			kellyHopsvelocity.Z = direction.Z * kellyHopsSpeed;
		}
		else
		{
			kellyHopsvelocity.X = Mathf.MoveToward(Velocity.X, 0, kellyHopsSpeed);
			kellyHopsvelocity.Z = Mathf.MoveToward(Velocity.Z, 0, kellyHopsSpeed);
		}

		Velocity = kellyHopsvelocity;
		MoveAndSlide();
	}
}
