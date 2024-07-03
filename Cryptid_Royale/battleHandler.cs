using Godot;
using System;
using System.Collections.Generic;

public partial class battleHandler : Node3D
{
	[Export] private NodePath character1Path;
	[Export] private NodePath character2Path;
	[Export] private NodePath character3Path;
	[Export] private NodePath character4Path;
	[Export] private NodePath character5Path;
	[Export] private NodePath character6Path;
	[Export] private NodePath cameraPath;
	[Export] private Vector3 character1Scale = new Vector3(10, 10, 10);
	[Export] private Vector3 character2Scale = new Vector3(10, 10, 10);
	[Export] private Vector3 character3Scale = new Vector3(10, 10, 10);
	[Export] private Vector3 character4Scale = new Vector3(10, 10, 10);
	[Export] private Vector3 character5Scale = new Vector3(10, 10, 10);
	[Export] private Vector3 character6Scale = new Vector3(10, 10, 10);
	private List<CharacterBody3D> characters = new List<CharacterBody3D>();
	private int currentCharacterIndex = -1;
	private Vector3[,] boardGrid;
	private Vector2[] characterPositions;
	private Camera3D camera;
	
	private const int gridSize = 16;
	private const float squareSize = 3.0f;


	//intializes all characters in a list
	/*int friendlyChars = 6;
	int enemChars = 6;
	bool chiefDead = false;
	bool frienChiefDead = false;
	bool frienTeamDead = false;
	bool enemTeamDead = false;*/
	//[Export] private NodePath wendigoodtoGoPath;
	//[Export] private NodePath goatManPath;
	//[Export] private NodePath tongueMonsterPath;
	//private wendigoodtoGo wendi;
	//private goatMan goat;
	//private tongueMonster skinwalker;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AddCharacter(character1Path, character1Scale);
		AddCharacter(character2Path, character2Scale);
		AddCharacter(character3Path, character3Scale);
		AddCharacter(character4Path, character4Scale);
		AddCharacter(character5Path, character5Scale);
		AddCharacter(character6Path, character6Scale);
		camera = GetNode<Camera3D>(cameraPath);
		camera.Fov = 75.0f;
		
		if(characters.Count == 0)
		{
			GD.PrintErr("No characters found. Please assign character paths.");
			return;
		}
		/*foreach(var character in characters)
		{
			character.Set("isControlled", false);
		}*/
		InitializeBoardGrid();
		InitializeCharacterPositions();
		CreateGridVisual();
		VerifyCharacterSizes();
	}
	
	private void AddCharacter(NodePath path, Vector3 scale)
	{
		if (!string.IsNullOrEmpty(path.ToString()))
		{
			var character = GetNode<CharacterBody3D>(path);
			if (character != null)
			{
				character.Scale = scale;
				characters.Add(character);
			}
			else
			{
				GD.PrintErr($"Character not found at path: {path}");
			}
		}
	}
	private void InitializeBoardGrid()
	{
		boardGrid = new Vector3[gridSize, gridSize];
		for(int x = 0; x < gridSize; x++)
		{
			for(int z = 0; z < gridSize; z++)
			{
				boardGrid[x, z] = new Vector3(x * squareSize, 0, z * squareSize);
			}
		}
	}
	private void InitializeCharacterPositions()
	{
		characterPositions = new Vector2[characters.Count];
		List<Vector2> initialPositions = new List<Vector2>{
			new Vector2(0, 0),
			new Vector2(1, 0),
			new Vector2(2, 0), 
			new Vector2(0, 1),
			new Vector2(1, 1),
			new Vector2(2, 1)
		};
		for(int i = 0; i < characters.Count; i++)
		{
			if(i < initialPositions.Count)
			{
				characterPositions[i] = initialPositions[i];
				Vector3 position = boardGrid[(int)characterPositions[i].X, (int)characterPositions[i].Y];
				characters[i].GlobalTransform = new Transform3D(Basis.Identity, position);
				//characters[i].GlobalTransform = new Transform3D(characters[i].GlobalTransform.Basis, boardGrid[(int)characterPositions[i].X, (int)characterPositions[i].Y]);
				characters[i].Set("isControlled", false);
				GD.Print($"Character {i} initialized at {position}");
			}
			//Vector3 characterWorldPos = characters[i].GlobalTransform.Origin;
			//characterPositions[i] = WorldToGrid(characterWorldPos);
		}
	}
	private void CreateGridVisual(){
		var arrayMesh = new ArrayMesh();
		var vertices = new Vector3[(gridSize + 1) * 4];
		for(int i = 0; i <= gridSize; i++)
		{
			vertices[i * 4] = new Vector3(i * squareSize, 0, 0);
			vertices[i * 4 + 1] = new Vector3(i * squareSize, 0, gridSize * squareSize);
			vertices[i * 4 + 2] = new Vector3(0, 0, i * squareSize);
			vertices[i * 4 + 3] = new Vector3(gridSize * squareSize, 0, i * squareSize);
		}
		var indices = new int[vertices.Length];
		for(int i = 0; i < vertices.Length; i++)
		{
			indices[i] = i;
		}
		var arrays = new Godot.Collections.Array();
		arrays.Resize((int)ArrayMesh.ArrayType.Max);
		arrays[(int)ArrayMesh.ArrayType.Vertex] = vertices;
		arrays[(int)ArrayMesh.ArrayType.Index] = indices;
		
		arrayMesh.AddSurfaceFromArrays(Mesh.PrimitiveType.Lines, arrays);
		var meshInstance = new MeshInstance3D{
			Mesh = arrayMesh,
			Transform = new Transform3D(Basis.Identity, new Vector3(-gridSize * squareSize / 2, 0, -gridSize * squareSize / 2))
		};
		AddChild(meshInstance);
	}
	private Vector2 WorldToGrid(Vector3 worldPos)
	{
		int X =  Mathf.Clamp((int)(worldPos.X / squareSize), 0, gridSize - 1);
		int Z =  Mathf.Clamp((int)(worldPos.Z / squareSize), 0, gridSize - 1);
		return new Vector2(X, Z);
	}

	public override void _Input(InputEvent @event)
	{
		if(@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
		{
			HandleMouseClick(mouseEvent.Position);
		}
		else if(@event.IsActionPressed("ui_right")){
			IterateCharacter(1);
		}
		else if(@event.IsActionPressed("ui_left")){
			IterateCharacter(-1);
		}
		/*used for iterating through the list of characters, uses left and right arrow keys,
		selected characters are highlighted with a blue ring around them
		*/
	}
	private void IterateCharacter(int direction)
	{
		if (characters.Count == 0) return;
		int newIndex = (currentCharacterIndex + direction + characters.Count) % characters.Count;
		GD.Print($"Switching control from {currentCharacterIndex} to {newIndex}");
		SetControl(newIndex);
	}

	private void SetControl(int characterIndex)
	{
		if(characterIndex >= 0 && characterIndex < characters.Count)
		{
			if(currentCharacterIndex != -1)
			{
				characters[currentCharacterIndex].Set("isControlled", false);
				characters[currentCharacterIndex].Visible = true;
			}
			
			currentCharacterIndex = characterIndex;
			characters[currentCharacterIndex].Set("isControlled", true);
			characters[currentCharacterIndex].Visible = true;
			GD.Print($"Character {currentCharacterIndex} is now controlled");
		}
	}
	private void HandleMouseClick(Vector2 mousePosition)
	{
		//Camera3D camera = GetViewport().GetCamera3D();
		if(camera == null) return;
		
		Vector3 from = camera.ProjectRayOrigin(mousePosition);
		Vector3 to = from + camera.ProjectRayNormal(mousePosition) * 1000;
		
		var spaceState = GetWorld3D().DirectSpaceState;
		var query = PhysicsRayQueryParameters3D.Create(from, to);
		query.Exclude = new Godot.Collections.Array<Rid>();
		foreach(var character in characters)
		{
			query.Exclude.Add(character.GetRid());
		}
		var result = spaceState.IntersectRay(query);
		
		if(result.Count > 0 && result.ContainsKey("position"))
		{//Dictionary method uses ContainsKey instead of Contains
			Vector3 clickedPosition = (Vector3)result["position"];
			Vector2 gridPosition = WorldToGrid(clickedPosition);
			
			MoveCharacterToGrid(gridPosition);
		}
	}
	private void MoveCharacterToGrid(Vector2 gridPosition)
	{
		if(currentCharacterIndex == -1) return;
		Vector2 currentPos = characterPositions[currentCharacterIndex];
		
		if(IsValidGridPosition(gridPosition) && IsAdjacent(currentPos, gridPosition))
		{
			characterPositions[currentCharacterIndex] = gridPosition;
			characters[currentCharacterIndex].GlobalTransform = new Transform3D(characters[currentCharacterIndex].GlobalTransform.Basis, boardGrid[(int)gridPosition.X, (int)gridPosition.Y]);
			GD.Print($"Character {currentCharacterIndex} moved to {gridPosition}");	
		}
	}
	private bool IsValidGridPosition(Vector2 pos)
	{
		return pos.X >= 0 && pos.X < gridSize && pos.Y >= 0 && pos.Y < gridSize;
	}
	private bool IsAdjacent(Vector2 currentPos, Vector2 newPos)
	{
		return Mathf.Abs(currentPos.X - newPos.X) + Mathf.Abs(currentPos.Y - newPos.Y) == 1;
	}
	private void VerifyCharacterSizes()
	{
		foreach(var character in characters)
		{
			GD.Print($"Character {character.Name} size: {character.Scale}");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("ui_up"))
		{
			AdjustCharacterScale(1.1f);
		}
		else if (Input.IsActionJustPressed("ui_down"))
		{
			AdjustCharacterScale(0.9f);
		}
	}
	private void AdjustCharacterScale(float scaleMultiplier)
	{
		foreach (var character in characters)
		{
			character.Scale *= scaleMultiplier;
			GD.Print($"Character {character.Name} new size: {character.Scale}");
		}
		
	}
}
