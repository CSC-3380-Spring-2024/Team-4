using Godot;
using System;
using playerHandler;

public partial class tongueMonster : CharacterBody3D
{
	bool inHitRange = false;
	string playerNum = "p1";
	playerHandler.characterHandler handler = new characterHandler();
	public const float tongueMonsterSpeed = 4.0f;
	//public const float JumpVelocity = 4.5f;
	public const float tongueMonsterRotationVelocity = 3.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float tongueMonstergravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	
	private AnimationTree tongueMonster_anim;
	private AnimationNodeStateMachinePlayback tongueMonster_animPlayback;
	[Export] public Vector3 tongueMonstervelocity;
	
	public override void _Ready(){ //main function, runs every tick of the game

		//charHandler characterHandlerInstance = GetNode<characterHandler>("res://characterHandler.cs");

		tongueMonster_anim = GetNode<AnimationTree>("AnimationTree");
		tongueMonster_animPlayback = (AnimationNodeStateMachinePlayback) tongueMonster_anim.Get("parameters/playback");
		tongueMonster_anim.Active = true;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		var characterHandler = new characterHandler();
		tongueMonstervelocity = Velocity;
		bool punched = false;

		// Add the gravity.
		

		// Handle Jump.
		/*
		if (tongueMonster_animPlayback.GetCurrentNode() == "attack"){
			tongueMonstervelocity = Vector3.Zero;
			Velocity = tongueMonstervelocity;
		}
		*/
			//fishMonstervelocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
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

	

	


}
