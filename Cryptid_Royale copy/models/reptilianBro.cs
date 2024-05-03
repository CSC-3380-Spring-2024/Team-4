using Godot;
using System;

public partial class reptilianBro : CharacterBody3D
{
	public const float reptilianSpeed = 4.0f;
	//public const float reptilianJumpVelocity = 4.5f;
	public const float reptilianRotationVelocity = 3.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float reptiliangravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	
	private AnimationTree reptilian_anim;
	private AnimationNodeStateMachinePlayback reptilian_animPlayback;
	
	[Export] public Vector3 reptilianvelocity;
	
	public override void _Ready(){
		reptilian_anim = GetNode<AnimationTree>("AnimationTree");
		reptilian_animPlayback = (AnimationNodeStateMachinePlayback) reptilian_anim.Get("parameters/playback");
		reptilian_anim.Active = true;
	}
	public override void _PhysicsProcess(double delta)
	{
		reptilianvelocity = Velocity;
		bool punched = false;

		// Add the gravity.
		if (!IsOnFloor())
			reptilianvelocity.Y -= reptiliangravity * (float)delta;
		else{
			// Handle Jump.
			//if (Input.IsActionJustPressed("ui_accept") && IsOnFloor() )
				//velocity.Y = JumpVelocity;
			if (Input.IsActionJustPressed("spaceAttack"))
				punched = true;
			reptilian_anim.Set("parameters/conditions/attack", punched);
		}
		
		if (reptilian_animPlayback.GetCurrentNode() == "attack"){
			reptilianvelocity = Vector3.Zero;
			Velocity = reptilianvelocity;
			return;
		}
		
		

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		float turnStrength = Input.GetAxis("left", "right");
		float moveStrength = Input.GetAxis("forward", "backwards");
		
		RotateY(-Mathf.DegToRad(turnStrength * reptilianRotationVelocity));
		Vector3 direction = (Transform.Basis * new Vector3(0, 0, moveStrength)).Normalized();
		if (direction != Vector3.Zero)
		{
			reptilianvelocity.X = direction.X * reptilianSpeed;
			reptilianvelocity.Z = direction.Z * reptilianSpeed;
		}
		else
		{
			reptilianvelocity.X = Mathf.MoveToward(Velocity.X, 0, reptilianSpeed);
			reptilianvelocity.Z = Mathf.MoveToward(Velocity.Z, 0, reptilianSpeed);
		}

		Velocity = reptilianvelocity;
		MoveAndSlide();
	}
}
