using Godot;
using System;

public partial class hideBehind : CharacterBody3D
{
	public const float hideBehindSpeed = 4.0f;
	//public const float hideBehindJumpVelocity = 4.5f;
	public const float hideBehindRotationVelocity = 3.5f;

	
	public float hideBehindgravity = ProjectSettings.GetSetting("physics/3d/default_gravity").AsSingle();
	private AnimationTree hideBehind_anim;
	private AnimationNodeStateMachinePlayback hideBehind_animPlayback;
	
	[Export] public Vector3 hideBehindvelocity;
	
	public override void _Ready(){
		hideBehind_anim = GetNode<AnimationTree>("AnimationTree");
		hideBehind_animPlayback = (AnimationNodeStateMachinePlayback) hideBehind_anim.Get("parameters/playback");
		hideBehind_anim.Active = true;
	}
	public override void _PhysicsProcess(double delta)
	{
		hideBehindvelocity = Velocity;
		bool punched = false;

		if (!IsOnFloor())
			hideBehindvelocity.Y -= hideBehindgravity * (float)delta;
		else{
	
			if (Input.IsActionJustPressed("spaceAttack"))
				punched = true;
			hideBehind_anim.Set("parameters/conditions/attack", punched);
		}
		
		if (hideBehind_animPlayback.GetCurrentNode() == "attack"){
			hideBehindvelocity = Vector3.Zero;
			Velocity = hideBehindvelocity;
			return;
		}
		if(Input.IsActionPressed("selP4")){
			float turnStrength = Input.GetAxis("left", "right");
			float moveStrength = Input.GetAxis("forward", "backwards");
			
			RotateY(-Mathf.DegToRad(turnStrength * hideBehindRotationVelocity));
			Vector3 direction = (Transform.Basis * new Vector3(0, 0, moveStrength)).Normalized();
			if (direction != Vector3.Zero)
			{
				hideBehindvelocity.X = direction.X * hideBehindSpeed;
				hideBehindvelocity.Z = direction.Z * hideBehindSpeed;
			}
			else
			{
				hideBehindvelocity.X = Mathf.MoveToward(Velocity.X, 0, hideBehindSpeed);
				hideBehindvelocity.Z = Mathf.MoveToward(Velocity.Z, 0, hideBehindSpeed);
			}

			Velocity = hideBehindvelocity;
			MoveAndSlide();
		}
	}
}
