using Godot;
using System;

public partial class leechMan : CharacterBody3D
{
	bool inHitRange;
	public const float leechManSpeed = 4.0f;
	//public const float JumpVelocity = 4.5f;
	public const float leechManRotationVelocity = 3.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float leechManGravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	
	private AnimationTree leechMan_anim;
	private AnimationNodeStateMachinePlayback leechMan_animPlayback;
	[Export] public Vector3 leechManVelocity;
	
	public override void _Ready(){
		leechMan_anim = GetNode<AnimationTree>("AnimationTree");
		leechMan_animPlayback = (AnimationNodeStateMachinePlayback) leechMan_anim.Get("parameters/playback");
		leechMan_anim.Active = true;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		leechManVelocity = Velocity;
		bool punched = false;

		// Add the gravity
		if (!IsOnFloor())
			leechManVelocity.Y -= leechManGravity * (float)delta;
		else{
			if (Input.IsActionJustPressed("spaceAttack")){
				punched = true;
			}
			leechMan_anim.Set("parameters/conditions/attack", punched);
		}
		if (leechMan_animPlayback.GetCurrentNode() == "attack"){
			leechManVelocity = Vector3.Zero;
			Velocity = leechManVelocity;
			return;
		}
		//what its supposed to look like, but retrieving the variable from the parent class proved to be an issue that we could not solve
		//if(currentPlayer == 4){
		if(Input.IsActionPressed("selP4")){
			
			// Get the input direction and handle the movement/deceleration.
			// As good practice, you should replace UI actions with custom gameplay actions.
			
			float turnStrength = Input.GetAxis("left", "right");
			float moveStrength = Input.GetAxis("forward", "backwards");
			RotateY(-Mathf.DegToRad(turnStrength * leechManRotationVelocity));
			Vector3 direction = (Transform.Basis * new Vector3(0, 0, moveStrength)).Normalized();
			
			if (direction != Vector3.Zero)
			{
				leechManVelocity.X = direction.X * leechManSpeed;
				leechManVelocity.Z = direction.Z * leechManSpeed;
			}
			else
			{
				leechManVelocity.X = Mathf.MoveToward(Velocity.X, 0, leechManSpeed);
				leechManVelocity.Z = Mathf.MoveToward(Velocity.Z, 0, leechManSpeed);
			}

			Velocity = leechManVelocity;
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
