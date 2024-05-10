using Godot;
using System;

public partial class fishMonster : CharacterBody3D
{
	bool inHitRange;
	public const float fishMonsterSpeed = 4.0f;
	//public const float JumpVelocity = 4.5f;
	public const float fishMonsterRotationVelocity = 3.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float fishMonsterGravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	
	private AnimationTree fishMonster_anim;
	private AnimationNodeStateMachinePlayback fishMonster_animPlayback;
	[Export] public Vector3 fishMonsterVelocity;
	
	public override void _Ready(){
		fishMonster_anim = GetNode<AnimationTree>("AnimationTree");
		fishMonster_animPlayback = (AnimationNodeStateMachinePlayback) fishMonster_anim.Get("parameters/playback");
		fishMonster_anim.Active = true;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		fishMonsterVelocity = Velocity;
		bool punched = false;

		// Add the gravity
		if (!IsOnFloor())
			fishMonsterVelocity.Y -= fishMonsterGravity * (float)delta;
		else{
			if (Input.IsActionJustPressed("spaceAttack")){
				punched = true;
			}
			fishMonster_anim.Set("parameters/conditions/attack", punched);
		}
		if (fishMonster_animPlayback.GetCurrentNode() == "attack"){
			fishMonsterVelocity = Vector3.Zero;
			Velocity = fishMonsterVelocity;
			return;
		}
		//what its supposed to look like, but retrieving the variable from the parent class proved to be an issue that we could not solve
		//if(currentPlayer == 2){
		if(Input.IsActionPressed("selP2")){
			
			// Get the input direction and handle the movement/deceleration.
			// As good practice, you should replace UI actions with custom gameplay actions.
			
			float turnStrength = Input.GetAxis("left", "right");
			float moveStrength = Input.GetAxis("forward", "backwards");
			RotateY(-Mathf.DegToRad(turnStrength * fishMonsterRotationVelocity));
			Vector3 direction = (Transform.Basis * new Vector3(0, 0, moveStrength)).Normalized();
			
			if (direction != Vector3.Zero)
			{
				fishMonsterVelocity.X = direction.X * fishMonsterSpeed;
				fishMonsterVelocity.Z = direction.Z * fishMonsterSpeed;
			}
			else
			{
				fishMonsterVelocity.X = Mathf.MoveToward(Velocity.X, 0, fishMonsterSpeed);
				fishMonsterVelocity.Z = Mathf.MoveToward(Velocity.Z, 0, fishMonsterSpeed);
			}

			Velocity = fishMonsterVelocity;
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
