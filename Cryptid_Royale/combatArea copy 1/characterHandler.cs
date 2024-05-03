
using Godot;
using System;

namespace playerHandler{
	public partial class characterHandler : Node {

		public string newActivePlayer;
		public int p1 = 1;
		public int p2 = 2;
		public int p3 = 3;
		public int p4 = 4;
		public int p5 = 5;
		public int p6 = 6;

		// Called when the node enters the scene tree for the first time.
		public override void _Ready(){
        	if(Input.IsActionPressed("selP1")){
				newActivePlayer = "p1";
				GD.Print("p1");	
			}
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta){

		}

		public void OnActionPressed(InputEvent @event){
			
			//selP1...P6 represents 'player 1', and so on. "selP1" is the input map action for the num key 1, selP2 is 2, and so on..
			if(Input.IsActionPressed("selP1")){
				newActivePlayer = "p1";
				GD.Print("p1");	
			}
			if(Input.IsActionPressed("selP2")){
				newActivePlayer = "p2";
				GD.Print("p2");
			}
			if(Input.IsActionPressed("selP3")){
				newActivePlayer = "p3";
				GD.Print("p3");
			}
			if(Input.IsActionPressed("selP4")){
				newActivePlayer = "p4";
				GD.Print("p4");
			}
			if(Input.IsActionPressed("selP5")){
				newActivePlayer = "p5";
				GD.Print("p5");
			}
			if(Input.IsActionPressed("selP6")){
				newActivePlayer = "p6";
				GD.Print("p6");
			}
		}
	}
}
