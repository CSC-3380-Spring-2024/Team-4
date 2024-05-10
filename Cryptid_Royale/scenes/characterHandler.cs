using Godot;
using System;

public partial class characterHandler : Node
{
	public static int currentPlayer;
	int inputDetected;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{


		//sets the active plauyer character
		switch(inputDetected){
			case 1 when Input.IsActionPressed("selP1"):
			    inputDetected = 1;
				currentPlayer = inputDetected;
				break;
			case 2 when Input.IsActionPressed("selP2"):
			    inputDetected = 2;
				currentPlayer = inputDetected;
				break;
			case 3 when Input.IsActionPressed("selP3"):
			    inputDetected = 3;
				currentPlayer = inputDetected;
				break;
			case 4 when Input.IsActionPressed("selP4"):
			    inputDetected = 4;
				currentPlayer = inputDetected;
				break;
			case 5 when Input.IsActionPressed("selP5"):
			    inputDetected = 5;
				currentPlayer = inputDetected;
				break;
			case 6 when Input.IsActionPressed("selP6"):
			    inputDetected = 6;
				currentPlayer = inputDetected;
				break;
		}

	}
}
