using Godot;

public partial class GameUi : CanvasLayer
{
	public Godot.Collections.Array<CompressedTexture2D> interactionIcons = new Godot.Collections.Array<CompressedTexture2D>
	{
		{null},
		{ResourceLoader.Load<CompressedTexture2D>("res://assets/images/icons/interaction/interactionicon-pregrab.png")}
	};

	public enum InteractionIconsEnum
	{
		None,
		Normal,
	}

	public static InteractionIconsEnum interactionIcon = InteractionIconsEnum.None;

	[Export] public Control InteractionUiContainer {get; set;}

	[Export] public Label TextBox {get; set;}
	[Export] public TextureRect IconBox {get; set;}

	[Export] public TextureProgressBar HealthBar {get; set;}

	private Color healthBarDefaultColor; // HEX: 57ff60

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (HealthBar.Value != Player.playerHealth)
		{
			healthBarDefaultColor = new Color(0.341F, 1F, 0.376F, (float)((HealthBar.Value - HealthBar.MinValue) / (HealthBar.MaxValue - HealthBar.MinValue)));
			HealthBar.TintProgress = healthBarDefaultColor;
			HealthBar.Value = Mathf.Lerp(HealthBar.Value, Player.playerHealth, 0.1F);
		}
	}

	public void ChangeUi(InteractionIconsEnum interactionIcon, string text)
	{
		IconBox.Texture = interactionIcons[(int)interactionIcon];
		TextBox.Text = text;
	}
}
