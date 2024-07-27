public struct PlayerStateInfo
{
	public string Name;
	public PlayerModelVariations Variation;
	public PlayerStateInfo(string name, PlayerModelVariations variation)
	{
		Name = name;
		Variation = variation;
	}

	public bool IsValid() => !string.IsNullOrEmpty(Name);
}