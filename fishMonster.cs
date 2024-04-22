using Godot;
using System;

public partial class fishMonster : CharacterBody3D
{
	public const float fishMonsterSpeed = 4.0f;
	//public const float JumpVelocity = 4.5f;
	public const float fishMonsterRotationVelocity = 3.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float fishMonstergravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	
	private AnimationTree fishMonster_anim;
	private AnimationNodeStateMachinePlayback fishMonster_animPlayback;
	[Export] public Vector3 fishMonstervelocity;
	
	public override void _Ready(){
		fishMonster_anim = GetNode<AnimationTree>("AnimationTree");
		fishMonster_animPlayback = (AnimationNodeStateMachinePlayback) fishMonster_anim.Get("parameters/playback");
		fishMonster_anim.Active = true;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		fishMonstervelocity = Velocity;
		bool punched = false;

		// Add the gravity.
		if (!IsOnFloor())
			fishMonstervelocity.Y -= fishMonstergravity * (float)delta;
		else{
			if (Input.IsActionJustPressed("spaceAttack"))
				punched = true;
			fishMonster_anim.Set("parameters/conditions/attack", punched);
		}

		// Handle Jump.
		if (fishMonster_animPlayback.GetCurrentNode() == "attack"){
			fishMonstervelocity = Vector3.Zero;
			Velocity = fishMonstervelocity;
		}
			//fishMonstervelocity.Y = JumpVelocity;

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		
		float turnStrength = Input.GetAxis("left", "right");
		float moveStrength = Input.GetAxis("forward", "backwards");
		RotateY(-Mathf.DegToRad(turnStrength * fishMonsterRotationVelocity));
		Vector3 direction = (Transform.Basis * new Vector3(0, 0, moveStrength)).Normalized();
		
		if (direction != Vector3.Zero)
		{
			fishMonstervelocity.X = direction.X * fishMonsterSpeed;
			fishMonstervelocity.Z = direction.Z * fishMonsterSpeed;
		}
		else
		{
			fishMonstervelocity.X = Mathf.MoveToward(Velocity.X, 0, fishMonsterSpeed);
			fishMonstervelocity.Z = Mathf.MoveToward(Velocity.Z, 0, fishMonsterSpeed);
		}

		Velocity = fishMonstervelocity;
		MoveAndSlide();
	}
}
