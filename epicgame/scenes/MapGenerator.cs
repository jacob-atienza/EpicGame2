using Godot;
using System;
using System.Collections.Generic;

public partial class MapGenerator : Node2D
{
	private TileMapLayer grassLayer;
	private TileMapLayer dirtLayer;
	private TileMapLayer dirtBackgroundLayer;
	private FastNoiseLite fastNoiseLite;

	const int width = 25;
	const int height = 25;
	private const float scale = 0.1f;

	public override void _Ready()
	{
		grassLayer = GetNode<TileMapLayer>("Grass");
		dirtLayer = GetNode<TileMapLayer>("Dirt");
		dirtBackgroundLayer = GetNode<TileMapLayer>("DirtBackground");

		fastNoiseLite = new FastNoiseLite();
		GenerateWorld();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void GenerateWorld()
	{
		RandomNumberGenerator rng = new();
		rng.Randomize();
		fastNoiseLite.Seed = rng.RandiRange(0, 500);
		fastNoiseLite.NoiseType = FastNoiseLite.NoiseTypeEnum.Simplex;
		fastNoiseLite.FractalOctaves = 2;
		fastNoiseLite.FractalGain = 0;
		for (int x = 0; x < width; x++)
		{
			for (int y = 5; y < height; y++)
			{
				float noiseValue = fastNoiseLite.GetNoise2D(x, y);
				if (noiseValue > 0.5f)
				{
					grassLayer.SetCell(new Vector2I(x, y),1, new Vector2I(0,0), 0);
					dirtBackgroundLayer.SetCell(new Vector2I(x, y),1, new Vector2I(0,0), 0);
					GD.Print($"Placing Grass at ({x}, {y}) with noise {noiseValue}");
				}
				else
				{
					dirtLayer.SetCell(new Vector2I(x, y),0, new Vector2I(0,0), 0);
					dirtBackgroundLayer.SetCell(new Vector2I(x, y),1, new Vector2I(0,0), 0);
					GD.Print($"Placing Dirt at ({x}, {y}) with noise {noiseValue}");
					if (noiseValue < -0.5f)
					{
						dirtBackgroundLayer.SetCell(new Vector2I(x, y),1, new Vector2I(0,0), 0);
						GD.Print($"Placing Dirt Background at ({x}, {y}) with noise {noiseValue}");
					}
				}
			}
		}
	}
}
