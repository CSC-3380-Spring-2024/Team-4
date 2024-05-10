using Godot;
using System;
<<<<<<< Updated upstream
<<<<<<< Updated upstream
using playerHandler;

public partial class tongueMonster : CharacterBody3D
{
	bool inHitRange = false;
	string playerNum = "p1";
	playerHandler.characterHandler handler = new characterHandler();
=======

public partial class tongueMonster : CharacterBody3D
{
>>>>>>> Stashed changes
=======

public partial class tongueMonster : CharacterBody3D
{
>>>>>>> Stashed changes
	public const float tongueMonsterSpeed = 4.0f;
	//public const float JumpVelocity = 4.5f;
	public const float tongueMonsterRotationVelocity = 3.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float tongueMonstergravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	
	private AnimationTree tongueMonster_anim;
	private AnimationNodeStateMachinePlayback tongueMonster_animPlayback;
	[Export] public Vector3 tongueMonstervelocity;
	
<<<<<<< Updated upstream
<<<<<<< Updated upstream
	public override void _Ready(){ //main function, runs every tick of the game

		//charHandler characterHandlerInstance = GetNode<characterHandler>("res://characterHandler.cs");

=======
	public override void _Ready(){
>>>>>>> Stashed changes
=======
	public override void _Ready(){
>>>>>>> Stashed changes
		tongueMonster_anim = GetNode<AnimationTree>("AnimationTree");
		tongueMonster_animPlayback = (AnimationNodeStateMachinePlayback) tongueMonster_anim.Get("parameters/playback");
		tongueMonster_anim.Active = true;
	}
	
	public override void _PhysicsProcess(double delta)
	{
<<<<<<< Updated upstream
<<<<<<< Updated upstream
		var characterHandler = new characterHandler();
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
		tongueMonstervelocity = Velocity;
		bool punched = false;

		// Add the gravity.
<<<<<<< Updated upstream
<<<<<<< Updated upstream
		

		// Handle Jump.
		/*
=======
=======
>>>>>>> Stashed changes
		if (!IsOnFloor())
			tongueMonstervelocity.Y -= tongueMonstergravity * (float)delta;
		else{
			if (Input.IsActionJustPressed("spaceAttack"))
				punched = true;
			tongueMonster_anim.Set("parameters/conditions/attack", punched);
		}

		// Handle Jump.
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
		if (tongueMonster_animPlayback.GetCurrentNode() == "attack"){
			tongueMonstervelocity = Vector3.Zero;
			Velocity = tongueMonstervelocity;
		}
<<<<<<< Updated upstream
<<<<<<< Updated upstream
		*/
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
			//fishMonstervelocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
<<<<<<< Updated upstream
<<<<<<< Updated upstream
		if(Input.IsActionPressed("selP1")){
			if (!IsOnFloor()){
				tongueMonstervelocity.Y -= tongueMonstergravity * (float)delta;
			}else{
			if (Input.IsActionJustPressed("spaceAttack") && inHitRange == true)
				punched = true;
				tongueMonster_anim.Set("parameters/conditions/attack", punched);
			}

			float turnStrength = Input.GetAxis("left", "right");
			float moveStrength = Input.GetAxis("forward", "backwards");
			RotateY(-Mathf.DegToRad(turnStrength * tongueMonsterRotationVelocity));
			Vector3 direction = (Transform.Basis * new Vector3(0, 0, moveStrength)).Normalized();
				
			if (direction != Vector3.Zero)
			{
				tongueMonstervelocity.X = direction.X * tongueMonsterSpeed;
				tongueMonstervelocity.Z = direction.Z * tongueMonsterSpeed;
			}
			else
			{
				tongueMonstervelocity.X = Mathf.MoveToward(Velocity.X, 0, tongueMonsterSpeed);
				tongueMonstervelocity.Z = Mathf.MoveToward(Velocity.Z, 0, tongueMonsterSpeed);
			}
			Velocity = tongueMonstervelocity;
			MoveAndSlide();
		}
	}

	public void isInHitBox(Area3D area){
		GD.Print("Fight!!");
		inHitRange = true;
	}

	

	


=======
=======
>>>>>>> Stashed changes
		
		float turnStrength = Input.GetAxis("left", "right");
		float moveStrength = Input.GetAxis("forward", "backwards");
		RotateY(-Mathf.DegToRad(turnStrength * tongueMonsterRotationVelocity));
		Vector3 direction = (Transform.Basis * new Vector3(0, 0, moveStrength)).Normalized();
		
		if (direction != Vector3.Zero)
		{
			tongueMonstervelocity.X = direction.X * tongueMonsterSpeed;
			tongueMonstervelocity.Z = direction.Z * tongueMonsterSpeed;
		}
		else
		{
			tongueMonstervelocity.X = Mathf.MoveToward(Velocity.X, 0, tongueMonsterSpeed);
			tongueMonstervelocity.Z = Mathf.MoveToward(Velocity.Z, 0, tongueMonsterSpeed);
		}

		Velocity = tongueMonstervelocity;
		MoveAndSlide();
	}
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
}
