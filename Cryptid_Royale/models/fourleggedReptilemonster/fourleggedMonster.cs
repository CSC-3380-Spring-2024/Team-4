using Godot;
using System;

public partial class fourLeggedMonster : CharacterBody3D
{
	bool inHitRange;
	public const float lizardSpeed = 4.0f;
	//public const float JumpVelocity = 4.5f;
	public const float lizardRotationVelocity = 3.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float lizardGravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	
	private AnimationTree lizard_anim;
	private AnimationNodeStateMachinePlayback lizard_animPlayback;
	[Export] public Vector3 lizardVelocity;
	
	public override void _Ready(){
		lizard_anim = GetNode<AnimationTree>("AnimationTree");
		lizard_animPlayback = (AnimationNodeStateMachinePlayback) lizard_anim.Get("parameters/playback");
		lizard_anim.Active = true;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		lizardVelocity = Velocity;
		bool punched = false;

		// Add the gravity
		if (!IsOnFloor())
			lizardVelocity.Y -= lizardGravity * (float)delta;
		else{
			if (Input.IsActionJustPressed("spaceAttack")){
				punched = true;
			}
			lizard_anim.Set("parameters/conditions/attack", punched);
		}
		if (lizard_animPlayback.GetCurrentNode() == "attack"){
			lizardVelocity = Vector3.Zero;
			Velocity = lizardVelocity;
			return;
		}
		//what its supposed to look like, but retrieving the variable from the parent class proved to be an issue that we could not solve
		//if(currentPlayer == 6){
		if(Input.IsActionPressed("selP6")){
			
			// Get the input direction and handle the movement/deceleration.
			// As good practice, you should replace UI actions with custom gameplay actions.
			
			float turnStrength = Input.GetAxis("left", "right");
			float moveStrength = Input.GetAxis("forward", "backwards");
			RotateY(-Mathf.DegToRad(turnStrength * lizardRotationVelocity));
			Vector3 direction = (Transform.Basis * new Vector3(0, 0, moveStrength)).Normalized();
			
			if (direction != Vector3.Zero)
			{
				lizardVelocity.X = direction.X * lizardSpeed;
				lizardVelocity.Z = direction.Z * lizardSpeed;
			}
			else
			{
				lizardVelocity.X = Mathf.MoveToward(Velocity.X, 0, lizardSpeed);
				lizardVelocity.Z = Mathf.MoveToward(Velocity.Z, 0, lizardSpeed);
			}

			Velocity = lizardVelocity;
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
