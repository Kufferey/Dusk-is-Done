using static Godot.GD;
using Godot;

public partial class Game : Node3D
{
	[Signal] public delegate void NewSeasonEventHandler();
	[Signal] public delegate void EndSeasonEventHandler();

	[Signal] public delegate void NewDayEventHandler();
	[Signal] public delegate void EndDayEventHandler();

	public static Day DaySave {get; set;}

	public static Difficulty.DifficultyTypes? CurrentDifficulty {get; set;}
	public static ushort CurrentDay {get; set;}
	public static uint CurrentScore {get; set;}
	public static Seasons.SeasonType? CurrentSeason {get; set;}
	public static byte CurrentSection {get; set;}

	public byte daysNextSeason;

	[Export] private Player Player {get; set;}
	[Export] private Table Table {get; set;}

	private byte _maxCherries = 6;
	private byte _currentCherries;

	private bool _hasCalledNewSection;

	public override void _Ready()
	{
		NewDay += OnNewDay;
		EndDay += OnEndDay;

		NewSeason += OnNewSeason;
		EndSeason += OnEndSeason;

		if (IsOnLoadedSave())
		{
			
		}
		else
		{
			DaySave = new Day();

			if (CurrentDifficulty == null || !CurrentDifficulty.HasValue) CurrentDifficulty = Difficulty.DifficultyTypes.Normal;
			if (CurrentSeason == null || !CurrentSeason.HasValue) CurrentSeason = Seasons.SeasonType.Spring;

			daysNextSeason = 31;

			Print("new save");

			SaveGame();
		}
	}


	public override void _Process(double delta)
	{
		// if (IsSectionClear()) NewSection();
	}

	private void ExitGame()
	{
		// Unload all game assets and null variables.
	}

	private bool IsOnLoadedSave() => DaySave == null ? false : true;

	private bool IsSectionClear()
	{
		int cherryAmount = GetNode<Node>("Interactables/Cherry/Cherries").GetChildCount();
		return cherryAmount <= 0 || false;
	}

	private bool IsNewSeason() => CurrentDay >= daysNextSeason || false;

	private async void NewSection()
	{
		if (_hasCalledNewSection) return;

		AddCherries(1);

		CurrentSection++;
		
		_hasCalledNewSection = true;
		await ToSignal(GetTree().CreateTimer(0.5F), "timeout");
		_hasCalledNewSection = false;
	}

	private void OnNewDay()
	{
		
	}

	private void OnEndDay()
	{
		SaveGame();

	}

	private void OnNewSeason()
	{
		byte seasonNumber = (byte)CurrentSeason;
		if (CurrentSeason == Seasons.SeasonType.Winter) seasonNumber = 0;
		else seasonNumber++;
		CurrentSeason = (Seasons.SeasonType)seasonNumber;

		Player.playerHealth = Player.playerMaxHealth;

		switch (CurrentSeason)
		{
			// Season difficulty scale: 0 - 5
			case Seasons.SeasonType.Spring: // 1

				// Code

			break;

			case Seasons.SeasonType.Summer: // 3

				// Code

			break;

			case Seasons.SeasonType.Fall: // 2

				// Code

			break;

			case Seasons.SeasonType.Winter: // 5

				// Code

			break;

			default: break;
		}

		daysNextSeason *= 2;
	}

	private void OnEndSeason()
	{

	}

	private Godot.Collections.Array<Vector3> GetSpawnPoints()
	{
		Godot.Collections.Array<Vector3> positions = new Godot.Collections.Array<Vector3>{};
		Godot.Collections.Array<Node> spawnPoints = GetNode<Node>("Interactables/Cherry/SpawnPoints").GetChildren();

		foreach (Node3D node in spawnPoints) positions.Add(node.Position);

		return positions;
	}

	private Vector3 GetRandomSpawnPoint()
	{
		Godot.Collections.Array<Vector3> spawnPoints = GetSpawnPoints();
		var randomVector = RandRange(0, GetSpawnPoints().Count - 1);
		var randomSpawnPosition = spawnPoints[randomVector];
		Print($"Returned: {randomSpawnPosition}\n RandomIndex: {randomVector}");

		return (Vector3)randomSpawnPosition;
	}

	private void AddCherries(int amount)
	{
		// TODO: GODOT DOES NOT LIKE RANDOMRANGE FOR LOOPS. "randomPosition', the random positions work. NVM Fixed. // TO KEEP NOTE USING GODOT C# - (Skill issue. I forgot to decrement)
		_currentCherries = 0;
		for (int cherries = 0; cherries < amount; cherries++)
		{
			byte randomChance = (byte)RandRange(1, 5);
			if (randomChance < 1) AddInteractableObject(InteractableObject.InteractableObjectType.CherrySpoiled, GetRandomSpawnPoint(), new Vector3(0,0,0), "Interactables/Cherry/Cherries");
			else AddInteractableObject(InteractableObject.InteractableObjectType.Cherry, GetRandomSpawnPoint(), new Vector3(0,0,0), "Interactables/Cherry/Cherries");

			_currentCherries++;
		}
	}

    private void PlayerZoomCamera(float from, float to, Tween.EaseType easeType,
	bool useCameraSens, float duration = 0.3F)
	{
		if (Player.HasMethod("ZoomCamera"))
		{
			Player.Callv(Player.MethodName.ZoomCamera, new Godot.Collections.Array{
				from, to, (int)easeType, useCameraSens, duration
			});	
		}
	}

	private void PlayerZoomAndLockCamera(float from, float to, Vector3 position,
	Tween.EaseType easeType, bool useSens, bool lockCam,
	float duration = 0.3F)
	{
		if (Player.HasMethod("ZoomAndLockCamera"))
		{
			Player.Callv(Player.MethodName.ZoomAndLockCamera, new Godot.Collections.Array{
				from, to, position, (int)easeType, useSens, lockCam, duration
			});	
		}
	}

	private void StoreTableItem(InteractableObject.InteractableObjectType type)
	{
		// if ( > (int)(tableItemsAllowedRow + tableItemsAllowedCollumn)) return;


	}


	private void OnItemHovered(InteractableObject.InteractableObjectType type)
	{

	}

	private void OnItemInteracted(InteractableObject.InteractableObjectType type)
	{
		switch (type)
		{
			/*
					GAMEPLAY RELATED
			*/
			case InteractableObject.InteractableObjectType.None:

				//Code

			break;

			case InteractableObject.InteractableObjectType.CherrySpoiled:
			case InteractableObject.InteractableObjectType.Cherry:

				//Code

			break;

			case InteractableObject.InteractableObjectType.Notebook:

				//Code

			break;

			/*
					HEALTH RELATED:
			*/
			case InteractableObject.InteractableObjectType.Bandage:

				//Code

			break;

			case InteractableObject.InteractableObjectType.Pills:

				//Code

			break;

			case InteractableObject.InteractableObjectType.MedicalPills:

				//Code

			break;

			case InteractableObject.InteractableObjectType.MedicalKit:

				//Code

			break;


			default: break;
		}
	}

	private void OnItemUsed(InteractableObject.InteractableObjectType type)
	{
		switch (type)
		{
			/*
					GAMEPLAY RELATED
			*/
			case InteractableObject.InteractableObjectType.None:

				//Code

			break;

			case InteractableObject.InteractableObjectType.CherrySpoiled:

				//Code

			break;

			case InteractableObject.InteractableObjectType.Cherry:

				//Code

			break;

			case InteractableObject.InteractableObjectType.Notebook:

				//Code

			break;

			/*
					HEALTH RELATED:
			*/
			case InteractableObject.InteractableObjectType.Bandage:

				// Player health goes up by 5-10.

			break;

			case InteractableObject.InteractableObjectType.Pills:

				// Health goes up by 15-20.

			break;

			case InteractableObject.InteractableObjectType.MedicalPills:

				// Health goes up 30-55.

			break;

			case InteractableObject.InteractableObjectType.MedicalKit:

				Player.playerHealth = Player.playerMaxHealth;

			break;


			default: break;
		}
	}

	private void UpdateSave()
	{
		DaySave.saveDay = CurrentDay;
		DaySave.saveScore = CurrentScore;
		DaySave.saveDifficulty = (Difficulty.DifficultyTypes)CurrentDifficulty;
		DaySave.saveDayNextSeason = daysNextSeason;
		DaySave.saveSeason = (Seasons.SeasonType)CurrentSeason;
		DaySave.saveTableItemsCollumn = Table.GetTableItemsCollumn();
		DaySave.saveTableItemsRow = Table.GetTableItemsRow();
		DaySave.saveCustomEvents = Events.GetEvents();
		if (Player.IsPlayerHoldingItem()) DaySave.savePlayerHeldItem = (InteractableObject.InteractableObjectType)Player.playerCurrentHeldItem.ObjectType;
	}

	private void SaveGame()
	{
		UpdateSave();
		// Rework save day system it sucks.
		Globals.SaveDay(DaySave);
	}

	private void AddInteractableObject(InteractableObject.InteractableObjectType type, Vector3 position = default, Vector3 rotation = default,
	string name = "", string nodePath = "Interactables")
	{
		InteractableObject interactableObject = InteractableObjectManager.interactableObjectPrefabs[type].Instantiate<InteractableObject>(PackedScene.GenEditState.Disabled);

		if (name != "" || name != null) interactableObject.Name = name;

		// Item signals
		interactableObject.ItemHovered += OnItemHovered;
		interactableObject.ItemInteracted += OnItemInteracted;
		interactableObject.ItemUsed += OnItemUsed;

		// Item position, and rotation
		interactableObject.Position = (Vector3)position;
		interactableObject.Rotation = (Vector3)rotation;

		GetNode<Node>(nodePath).AddChild(interactableObject);
	}
}