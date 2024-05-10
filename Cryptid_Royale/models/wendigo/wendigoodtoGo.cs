using Godot;
using System;

public partial class wendigoodtoGo : CharacterBody3D
{
	bool inHitRange;
	public const float wendigoSpeed = 4.0f;
	//public const float JumpVelocity = 4.5f;
	public const float wendigoRotationVelocity = 3.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float wendigoGravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	
	private AnimationTree wendigo_anim;
	private AnimationNodeStateMachinePlayback wendigo_animPlayback;
	[Export] public Vector3 wendigoVelocity;
	
	public override void _Ready(){
		wendigo_anim = GetNode<AnimationTree>("AnimationTree");
		wendigo_animPlayback = (AnimationNodeStateMachinePlayback) wendigo_anim.Get("parameters/playback");
		wendigo_anim.Active = true;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		wendigoVelocity = Velocity;
		bool punched = false;

		// Add the gravity
		if (!IsOnFloor())
			wendigoVelocity.Y -= wendigoGravity * (float)delta;
		else{
			if (Input.IsActionJustPressed("spaceAttack")){
				punched = true;
			}
			wendigo_anim.Set("parameters/conditions/attack", punched);
		}
		if (wendigo_animPlayback.GetCurrentNode() == "attack"){
			wendigoVelocity = Vector3.Zero;
			Velocity = wendigoVelocity;
			return;
		}
		//what its supposed to look like, but retrieving the variable from the parent class proved to be an issue that we could not solve
		//if(currentPlayer == 5){
		if(Input.IsActionPressed("selP5")){
			
			// Get the input direction and handle the movement/deceleration.
			// As good practice, you should replace UI actions with custom gameplay actions.
			
			float turnStrength = Input.GetAxis("left", "right");
			float moveStrength = Input.GetAxis("forward", "backwards");
			RotateY(-Mathf.DegToRad(turnStrength * wendigoRotationVelocity));
			Vector3 direction = (Transform.Basis * new Vector3(0, 0, moveStrength)).Normalized();
			
			if (direction != Vector3.Zero)
			{
				wendigoVelocity.X = direction.X * wendigoSpeed;
				wendigoVelocity.Z = direction.Z * wendigoSpeed;
			}
			else
			{
				wendigoVelocity.X = Mathf.MoveToward(Velocity.X, 0, wendigoSpeed);
				wendigoVelocity.Z = Mathf.MoveToward(Velocity.Z, 0, wendigoSpeed);
			}

			Velocity = wendigoVelocity;
			MoveAndSlide();
		}
	}
	public void isInHitBox(){
		inHitRange = true;
	}
	public void isNotInHitBox(){
		inHitRange = false;
	}
}
