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
		private int waveMultiplier = 2;
		private int spawnValue = 0;

		private int breakableBlocks;

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
			List<Vector2> spawnPositions = new List<Vector2>();
			int blocksToGenerate = GetBlockAmount();

			while(breakableBlocks < blocksToGenerate) 
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
			int maxBlocks = (configuration.maxX+1) * ((configuration.maxY - configuration.minY)+1); //+1 because its zero indexed
			spawnValue += Mathf.RoundToInt(Random.Range(1, waveMultiplier));

			if (spawnValue == 0)
            {
				Debug.LogError("Invalid spawn value, returning 1 block");
                return 1;
            }
            else if (spawnValue < maxBlocks)
            {
                return spawnValue;
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