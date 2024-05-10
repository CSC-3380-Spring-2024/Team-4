using Godot;
using System;

public partial class goatMan : CharacterBody3D
{
	bool inHitRange;
	int currentPlayer;
	public const float goatManSpeed = 4.0f;
	//public const float JumpVelocity = 4.5f;
	public const float goatManRotationVelocity = 3.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float goatManGravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	
	private AnimationTree goatMan_anim;
	private AnimationNodeStateMachinePlayback goatMan_animPlayback;
	[Export] public Vector3 goatManVelocity;
	
	public override void _Ready(){
		goatMan_anim = GetNode<AnimationTree>("AnimationTree");
		goatMan_animPlayback = (AnimationNodeStateMachinePlayback) goatMan_anim.Get("parameters/playback");
		goatMan_anim.Active = true;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		goatManVelocity = Velocity;
		bool punched = false;

		// Add the gravity
		if (!IsOnFloor())
			goatManVelocity.Y -= goatManGravity * (float)delta;
		else{
			if (Input.IsActionJustPressed("spaceAttack")){
				punched = true;
			}
			goatMan_anim.Set("parameters/conditions/attack", punched);
		}
		if (goatMan_animPlayback.GetCurrentNode() == "attack"){
			goatManVelocity = Vector3.Zero;
			Velocity = goatManVelocity;
			return;
		}
		//what its supposed to look like, but retrieving the variable from the parent class proved to be an issue that we could not solve
		//if(currentPlayer == 3){
		if(Input.IsActionPressed("selP3")){
			
			// Get the input direction and handle the movement/deceleration.
			// As good practice, you should replace UI actions with custom gameplay actions.
			
			float turnStrength = Input.GetAxis("left", "right");
			float moveStrength = Input.GetAxis("forward", "backwards");
			RotateY(-Mathf.DegToRad(turnStrength * goatManRotationVelocity));
			Vector3 direction = (Transform.Basis * new Vector3(0, 0, moveStrength)).Normalized();
			
			if (direction != Vector3.Zero)
			{
				goatManVelocity.X = direction.X * goatManSpeed;
				goatManVelocity.Z = direction.Z * goatManSpeed;
			}
			else
			{
				goatManVelocity.X = Mathf.MoveToward(Velocity.X, 0, goatManSpeed);
				goatManVelocity.Z = Mathf.MoveToward(Velocity.Z, 0, goatManSpeed);
			}

			Velocity = goatManVelocity;
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
