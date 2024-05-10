using Godot;
using System;

public partial class leechMan : CharacterBody3D
{
	public const float leechSpeed = 4.0f;
	public const float leechJumpVelocity = 4.5f;
	public const float leechRotationVelocity = 3.5f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float leechgravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	
	private AnimationTree leech_anim;
	private AnimationNodeStateMachinePlayback leech_animPlayback;
	
	[Export] public Vector3 leechvelocity;
	
	public override void _Ready(){
		leech_anim = GetNode<AnimationTree>("AnimationTree");
		leech_animPlayback = (AnimationNodeStateMachinePlayback) leech_anim.Get("parameters/playback");
		leech_anim.Active = true;
	}
	public override void _PhysicsProcess(double delta)
	{
		leechvelocity = Velocity;
		bool punched = false;

		// Add the gravity.
		if (!IsOnFloor())
			leechvelocity.Y -= leechgravity * (float)delta;
		else{
			// Handle Jump.
			//if (Input.IsActionJustPressed("ui_accept") && IsOnFloor() )
				//velocity.Y = JumpVelocity;
			if (Input.IsActionJustPressed("spaceAttack"))
				punched = true;
			leech_anim.Set("parameters/conditions/attack", punched);
		}
		
		if (leech_animPlayback.GetCurrentNode() == "attack"){
			leechvelocity = Vector3.Zero;
			Velocity = leechvelocity;
			return;
		}
		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		if(Input.IsActionPressed("selP5")){
			float turnStrength = Input.GetAxis("left", "right");
			float moveStrength = Input.GetAxis("forward", "backwards");
			
			RotateY(-Mathf.DegToRad(turnStrength * leechRotationVelocity));
			Vector3 direction = (Transform.Basis * new Vector3(0, 0, moveStrength)).Normalized();
			if (direction != Vector3.Zero)
			{
				leechvelocity.X = direction.X * leechSpeed;
				leechvelocity.Z = direction.Z * leechSpeed;
			}
			else
			{
				leechvelocity.X = Mathf.MoveToward(Velocity.X, 0, leechSpeed);
				leechvelocity.Z = Mathf.MoveToward(Velocity.Z, 0, leechSpeed);
			}

			Velocity = leechvelocity;
			MoveAndSlide();
		}
	}
	//private void _on_area_3d_input_event(Node camera, InputEvent @event, Vector3 position, Vector3 normal, long shape_idx)
	//{
	//if(Input.IsActionJustPressed("RightMouseClick"))
	//Replace with function body.
		//return;
	//}
}

