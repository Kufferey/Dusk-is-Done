using Godot;
using System;

public partial class Table : Node3D
{
	[Signal] public delegate void TableItemAddedEventHandler(int atPos, InteractableObject interactableObject);
	[Signal] public delegate void TableItemRemovedEventHandler(int atPos, InteractableObject interactableObject);
	
	public Godot.Collections.Array<InteractableObject> tableItemsRow = new Godot.Collections.Array<InteractableObject>{};
	public Godot.Collections.Array<InteractableObject> tableItemsCollumn = new Godot.Collections.Array<InteractableObject>{};
	
	public int tableItemsAllowedRow = 6;
	public int tableItemsAllowedCollumn = 3;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TableItemAdded += OnTableItemAdded;
		TableItemRemoved += OnTableItemRemoved;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public Godot.Collections.Array<InteractableObject.InteractableObjectType> GetTableItemsRow()
	{
		return new Godot.Collections.Array<InteractableObject.InteractableObjectType>{{InteractableObject.InteractableObjectType.None}};
	}

	public Godot.Collections.Array<InteractableObject.InteractableObjectType> GetTableItemsCollumn()
	{
		return new Godot.Collections.Array<InteractableObject.InteractableObjectType>{{InteractableObject.InteractableObjectType.None}};
	}

	public void OnTableItemAdded(int atPos, InteractableObject interactableObject)
	{

	}

	public void OnTableItemRemoved(int atPos, InteractableObject interactableObject)
	{
		
	}
}
