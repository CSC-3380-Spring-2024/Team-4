using Godot;
using System;

public partial class goatMan : CharacterBody3D
{
<<<<<<< Updated upstream
<<<<<<< Updated upstream
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
=======
=======
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
}
