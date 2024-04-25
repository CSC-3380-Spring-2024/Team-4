using Godot;
using System;

public partial class goatMan : CharacterBody3D
{
	public const float goatSpeed = 4.0f;
	public const float goatJumpVelocity = 4.5f;
	public const float goatRotationVelocity = 3.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float goatgravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	
	private AnimationTree goat_anim;
	private AnimationNodeStateMachinePlayback goat_animPlayback;
	
	[Export] public Vector3 goatvelocity;
	
	public override void _Ready(){
		goat_anim = GetNode<AnimationTree>("AnimationTree");
		goat_animPlayback = (AnimationNodeStateMachinePlayback) goat_anim.Get("parameters/playback");
		goat_anim.Active = true;
	}
	public override void _PhysicsProcess(double delta)
	{
		goatvelocity = Velocity;
		bool punched = false;

		// Add the gravity.
		if (!IsOnFloor())
			goatvelocity.Y -= goatgravity * (float)delta;
		else{
			// Handle Jump.
			//if (Input.IsActionJustPressed("ui_accept") && IsOnFloor() )
				//velocity.Y = JumpVelocity;
			if (Input.IsActionJustPressed("spaceAttack"))
				punched = true;
			goat_anim.Set("parameters/conditions/attack", punched);
		}
		
		if (goat_animPlayback.GetCurrentNode() == "attack"){
			goatvelocity = Vector3.Zero;
			Velocity = goatvelocity;
			return;
		}
		
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		if(Input.IsActionPressed("selP3")){
			float turnStrength = Input.GetAxis("left", "right");
			float moveStrength = Input.GetAxis("forward", "backwards");
			
			RotateY(-Mathf.DegToRad(turnStrength * goatRotationVelocity));
			Vector3 direction = (Transform.Basis * new Vector3(0, 0, moveStrength)).Normalized();
			if (direction != Vector3.Zero)
			{
				goatvelocity.X = direction.X * goatSpeed;
				goatvelocity.Z = direction.Z * goatSpeed;
			}
			else
			{
				goatvelocity.X = Mathf.MoveToward(Velocity.X, 0, goatSpeed);
				goatvelocity.Z = Mathf.MoveToward(Velocity.Z, 0, goatSpeed);
			}

			Velocity = goatvelocity;
			MoveAndSlide();
		}
	}
}
