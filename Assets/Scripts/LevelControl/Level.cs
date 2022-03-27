/*
* Copyright (c) Kp4ws
*
*/
using UnityEngine;
using System.Collections.Generic;
using BDM.EventManagement;
using BDM.Config;

namespace BDM.LevelControl
{
	public class Level : MonoBehaviour
	{
		[SerializeField] private GameObject[] blockTypes;
		[SerializeField] private GameConfig configuration;

		private int waveNumber = 1;
		private int waveMultiplier = 3;
		private int currentSpawnValue = 0;

		private int breakableBlocks;
		private List<Vector2> spawnPositions = new List<Vector2>();

		private EventBus bus;

        private void Awake()
        {
			bus = EventBus.Instance;
		}

        private void OnEnable()
		{
			bus.Subscribe(EventChannel.BlockDestroyed, DecrementBlocks);
			bus.Subscribe(EventChannel.NewWave, GenerateBlocks);
		}

        private void OnDisable()
        {
			bus.Unsubscribe(EventChannel.BlockDestroyed, DecrementBlocks);
			bus.Unsubscribe(EventChannel.NewWave, GenerateBlocks);
		}

        private void Start()
		{
			GenerateBlocks(null);
		}

		public void GenerateBlocks(object e)
		{
			int blocksToGenerate = GetBlockAmount();
			for (int i = 0; i < blocksToGenerate; i++)
			{
				int selectedBlock = Random.Range(0, blockTypes.Length);
				Vector2 spawnPos = new Vector2(Random.Range(configuration.minX, configuration.maxX + 1), Random.Range(configuration.minY, configuration.maxY + 1));

				if (!spawnPositions.Contains(spawnPos))
				{
					spawnPositions.Add(spawnPos);
					GameObject block = Instantiate(blockTypes[selectedBlock], spawnPos, Quaternion.identity);
					block.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.8f, 1f), Random.Range(0.8f, 1f), Random.Range(0.8f, 1f), 1f);
					breakableBlocks++;
				}
			}
		}

        private int GetBlockAmount()
		{
			int maxBlocks = configuration.maxX * configuration.maxY;
			
			currentSpawnValue = Mathf.RoundToInt(Random.Range(currentSpawnValue, waveMultiplier+1)) + 1;
			waveMultiplier += currentSpawnValue;

			if (currentSpawnValue == 0)
			{
				Debug.LogError("Invalid spawn value");
				currentSpawnValue++;
				return 1;
			}
			else if (currentSpawnValue < maxBlocks)
			{
				return currentSpawnValue;
			}
			else
            {
				return maxBlocks;
            }
		}

		public void DecrementBlocks(object e)
		{
			breakableBlocks--;

			if (breakableBlocks <= 0)
			{
				waveNumber++;
				bus.Publish(EventChannel.Countdown, this, "New Wave");
			}
		}
    }
}