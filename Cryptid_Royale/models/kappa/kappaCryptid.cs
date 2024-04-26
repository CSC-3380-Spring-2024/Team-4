using Godot;
using System;

public partial class kappaCryptid : CharacterBody3D
{
	public const float kappaSpeed = 4.0f;
	public const float kappaJumpVelocity = 4.5f;
	public const float kappaRotationVelocity = 3.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float kappagravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	
	private AnimationTree kappa_anim;
	private AnimationNodeStateMachinePlayback kappa_animPlayback;
	
	[Export] public Vector3 kappavelocity;
	
	public override void _Ready(){
		kappa_anim = GetNode<AnimationTree>("AnimationTree");
		kappa_animPlayback = (AnimationNodeStateMachinePlayback) kappa_anim.Get("parameters/playback");
		kappa_anim.Active = true;
	}
	public override void _PhysicsProcess(double delta)
	{
		kappavelocity = Velocity;
		bool punched = false;

		// Add the gravity.
		if (!IsOnFloor())
			kappavelocity.Y -= kappagravity * (float)delta;
		else{
			// Handle Jump.
			//if (Input.IsActionJustPressed("ui_accept") && IsOnFloor() )
				//velocity.Y = JumpVelocity;
			if (Input.IsActionJustPressed("spaceAttack"))
				punched = true;
			kappa_anim.Set("parameters/conditions/attack", punched);
		}
		
		if (kappa_animPlayback.GetCurrentNode() == "attack"){
			kappavelocity = Vector3.Zero;
			Velocity = kappavelocity;
			return;
		}
		
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		float turnStrength = Input.GetAxis("left", "right");
		float moveStrength = Input.GetAxis("forward", "backwards");
		
		RotateY(-Mathf.DegToRad(turnStrength * kappaRotationVelocity));
		Vector3 direction = (Transform.Basis * new Vector3(0, 0, moveStrength)).Normalized();
		if (direction != Vector3.Zero)
		{
			kappavelocity.X = direction.X * kappaSpeed;
			kappavelocity.Z = direction.Z * kappaSpeed;
		}
		else
		{
			kappavelocity.X = Mathf.MoveToward(Velocity.X, 0, kappaSpeed);
			kappavelocity.Z = Mathf.MoveToward(Velocity.Z, 0, kappaSpeed);
		}

		Velocity = kappavelocity;
		MoveAndSlide();
	}
}
