using Godot;
using System;
using System.Collections.Generic;

public partial class MapGenerator : Node2D
{
	private TileMapLayer blocks;
	private TileMapLayer backgrounds;
	private FastNoiseLite fastNoiseLite;

	const int chunkWidth = 32;
	const int height = 64;
	private const int surfaceHeight = 64;
	private const float scale = 0.1f;

	public override void _Ready()
	{
		blocks = GetNode<TileMapLayer>("Blocks");
		backgrounds = GetNode<TileMapLayer>("Backgrounds");


		GenerateWorld();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void GenerateWorld()
	{
		var softNoise = new FastNoiseLite();
		softNoise.Seed = 6969;
		for (int x = 0; x < chunkWidth; x++)
		{
			var y = Math.Floor(fastNoiseLite.GetNoise2D(x / 32f, 0) * 0.1)*surfaceHeight*.2;
			for (int zebra = 5; y < height; y++)
			{
				Vector2I current = new Vector2I(x, y);
				Vector2I above = new Vector2I(x, y - 1);
				float noiseValue = fastNoiseLite.GetNoise2D(x, y);
				GD.Print($"Above Source ID = {blocks.GetCellSourceId(above)}");
				if (blocks.GetCellSourceId(above) == -1)
				{
					blocks.SetCell(current, 2, new Vector2I(0, 0), 0);
					backgrounds.SetCell(current, 1, new Vector2I(0, 0), 0);
				}
				else
				{
					blocks.SetCell(current, 2, new Vector2I(1, 0), 0);
					backgrounds.SetCell(current, 1, new Vector2I(0, 0), 0);
				}

			}
		}
	}
}

