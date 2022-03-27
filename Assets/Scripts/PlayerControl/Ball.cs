/*
* Copyright (c) Kp4ws
*
*/

using UnityEngine;
using BDM.Stats;
using BDM.Config;
using BDM.EventManagement;
using UnityEngine.EventSystems;
using System.Collections;

namespace BDM.PlayerControl
{
	public class Ball : MonoBehaviour
	{
		//Config
		[SerializeField] private PaddleController paddle;
		[SerializeField] private AudioClip[] ballSounds;
		[SerializeField][Range(-10,10)] private float randomFactor = -1.2f;
		[SerializeField] private float spawnDelay = 0.2f;
		[SerializeField] private GameConfig configuration;

		private const string PLAYER = "Player";
		private const string WALL = "Wall";
		private const string BLOCK = "Breakable";

		//State
		private float xVelocity;
		private float yVelocity;
		Vector2 paddleToBallDistance;
		Vector2 originalPos;

		//Cached references
		private Rigidbody2D rb;
		private AudioSource audioSource;

		private bool ballLaunched = false;
		private bool disableInput = false;

		//Prevent loop
		private float oldYPosition;
		private int loopHitCheck;
		private float loopRange = 1.2f;
		private int maxLoopHit = 2;

		private EventBus bus;

		private void Awake()
        {
			bus = EventBus.Instance;
		}

        private void OnEnable()
		{
			bus.Subscribe(EventChannel.Countdown, ResetBallImmediate);
			bus.Subscribe(EventChannel.TakeLife, ResetBall);
			bus.Subscribe(EventChannel.GameOver, DisableInput);
		}

		private void OnDisable()
		{
			bus.Unsubscribe(EventChannel.Countdown, ResetBallImmediate);
			bus.Unsubscribe(EventChannel.TakeLife, ResetBall);
			bus.Unsubscribe(EventChannel.GameOver, DisableInput);
		}

		private void Start()
		{
			rb = GetComponent<Rigidbody2D>();
			audioSource = GetComponent<AudioSource>();
			paddleToBallDistance = transform.position - paddle.transform.position;

			audioSource.volume = PlayerPrefsController.GetMasterVolume();

			xVelocity = InitializeVelocityX();
			yVelocity = InitializeVelocityY();

			originalPos = GetPaddlePosition();
			oldYPosition = transform.position.y;
		}

		private float InitializeVelocityX()
        {
			//Could be modified in future iteration
			return 2;
        }
		
		private float InitializeVelocityY()
		{
			switch (PlayerPrefsController.GetDifficulty())
			{
				//Easy
				case 0:
					return configuration.easySpeed;
				//Normal
				case 1:
					return configuration.normalSpeed;
				//Hard
				case 2:
					return configuration.hardSpeed;

				case 3:
					return configuration.intenseSpeed;

				default:
					Debug.LogError("Invalid Difficulty");
					return 0;
			}
		}

		private void Update()
		{
			if (ballLaunched)
				return;

			LockBallToPaddle();

            if (configuration.isMobile)
            {
                LaunchBall();
            }
            else
            {
                LaunchBallDesktop();
            }
        }

		private void LateUpdate()
        {
			//Keep ball at constant y
			yVelocity = Mathf.Clamp(yVelocity, InitializeVelocityY(), InitializeVelocityY());
		}

		private void LockBallToPaddle()
		{
			Vector2 paddlePos = GetPaddlePosition();
			transform.position = paddlePos + paddleToBallDistance;
		}

		private Vector2 GetPaddlePosition()
        {
			return new Vector2(paddle.transform.position.x, paddle.transform.position.y);
		}

		private void LaunchBallDesktop()
		{
			if (disableInput) 
				return;
			if (EventSystem.current.IsPointerOverGameObject())
				return;

			if (Input.GetMouseButtonDown(0))
			{
				rb.velocity = new Vector2(xVelocity, yVelocity);
				ballLaunched = true;
			}
		}

		private void LaunchBall()
		{
			if (Input.touchCount == 0) 
				return;
			if (disableInput)
				return;

			if (Input.GetTouch(0).phase == TouchPhase.Ended && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
			{
				rb.velocity = new Vector2(xVelocity, yVelocity);
				ballLaunched = true;
			}
		}

		public void ResetBall(object e)
		{
			StartCoroutine(PerformReset());
		}

		public void ResetBallImmediate(object e)
        {
			transform.position = GetPaddlePosition();
			ballLaunched = false;
		}

		private IEnumerator PerformReset()
        {
			yield return new WaitForSeconds(spawnDelay);
			transform.position = GetPaddlePosition();
			ballLaunched = false;
		}

		private void OnCollisionEnter2D(Collision2D collision)
		{
			if (!ballLaunched) 
				return;

			AudioClip audioClip = ballSounds[Random.Range(0, ballSounds.Length)];
			audioSource.PlayOneShot(audioClip);

			if (collision.gameObject.tag == PLAYER)
			{
				float x = (transform.position.x - paddle.transform.position.x) / collision.collider.bounds.size.x;
				Vector2 dir = new Vector2(x, 1).normalized;
				rb.velocity = dir * yVelocity;
			}
			
			if(collision.gameObject.tag == WALL)
			{
				loopHitCheck++;

				if (loopHitCheck >= maxLoopHit)
				{
					LoopPrevention();
				}
				//if (Mathf.Abs(transform.position.y - oldYPosition) < loopRange)
				//{
				//	loopHitCheck++;
				//}
				//else
				//            {
				//	loopHitCheck = 0;
				//            }

				//if (loopHitCheck >= maxLoopHit)
				//{
				//	LoopPrevention();
				//}
			}
			else 
            {
				loopHitCheck = 0;
			}

			oldYPosition = transform.position.y;
		}

		//Loop prevention has not been thoroughly tested
		private void LoopPrevention()
		{
			float velocityTweak = Random.Range(0, randomFactor);
			rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + velocityTweak);			
		}

		public void DisableInput(object e)
        {
			disableInput = true;
		}

	}

}