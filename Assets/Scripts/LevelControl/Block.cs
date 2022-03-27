/*
* Copyright (c) Kp4ws
*
*/

using UnityEngine;
using BDM.Stats;
using BDM.EventManagement;
using System.Collections;

namespace BDM.LevelControl
{
	public class Block : MonoBehaviour
	{
		private const string BREAKABLE = "Breakable";

		[SerializeField] private AudioClip breakSound;
		[SerializeField] private GameObject blockVFX;
		[SerializeField] private Sprite[] hitSprites;
		[SerializeField] private int blockPointsPerDestroyed; //TODO could dynamically set this to random value each time?

		private int timesHit;
		private float delayVFX = 1f;
		
		private float moveDelay;
		private int maxPosition = 3;
		private bool canMove = true;

		private EventBus bus;

        private void Awake()
        {
			bus = EventBus.Instance;
		}

        private void Start()
		{
			GetComponent<AudioSource>().volume = PlayerPrefsController.GetMasterVolume(); //This didn't seem to work when volume was set to 0

			//moveDelay = GetMoveDelay();
			//StartCoroutine(MovingBlocks());
		}

        //private float GetMoveDelay()
        //      {
        //	switch (PlayerPrefsController.GetDifficulty())
        //	{
        //		//Easy
        //		case 0:
        //			return 20f;
        //		//Normal
        //		case 1:
        //			return 15f;
        //		//Hard
        //		case 2:
        //			return 10f;
        //		//Intense
        //		case 3:
        //			return 5f;

        //		default:
        //			Debug.LogError("Invalid Difficulty");
        //			return 0;
        //	}
        //}

        //private IEnumerator MovingBlocks()
        //      {
        //	int layerMask = LayerMask.GetMask("Block");
        //          while (canMove)
        //          {
        //		RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + 0.5f, transform.position.y), transform.TransformDirection(Vector2.down), 1f, layerMask);
        //		Debug.Log(hit.collider);

        //		if (transform.position.y <= maxPosition || hit.collider != null)
        //		{
        //			canMove = false;
        //			break;
        //		}

        //		yield return new WaitForSeconds(1);
        //		transform.position = new Vector2(transform.position.x, transform.position.y - 1);
        //          }
        //      }

        private void OnCollisionEnter2D(Collision2D collision)
		{
			if (tag == BREAKABLE)
			{
				HandleHit();
			}
		}

		private void HandleHit()
		{
			timesHit++;
			int maxHits = hitSprites.Length + 1;

			if (timesHit >= maxHits)
			{
				DestroyBlock();
			}
			else
			{
				ShowNextHitSprite();
			}
		}

		private void ShowNextHitSprite()
		{
			int spriteIndex = timesHit - 1;
			if (hitSprites[spriteIndex] != null)
			{
				GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
			}
			else
			{
				Debug.LogError("Block sprite is missing from array" + gameObject.name);
			}
		}

		private void DestroyBlock()
		{
			PlayBlockDestroyedSFX();
			Destroy(gameObject);
			bus.Publish(EventChannel.BlockDestroyed, this, blockPointsPerDestroyed);
		}

		private void PlayBlockDestroyedSFX()
		{
			if (breakSound == null)
			{
				Debug.LogError("Break sound is not set " + gameObject.name);
				return;
			}

			if (blockVFX == null)
			{
				Debug.LogError("Block VFX is not set");
				return;
			}

			AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position, PlayerPrefsController.GetMasterVolume());
			GameObject particles = Instantiate(blockVFX, transform.position, Quaternion.identity);
			Destroy(particles, delayVFX);
		}
    }

}