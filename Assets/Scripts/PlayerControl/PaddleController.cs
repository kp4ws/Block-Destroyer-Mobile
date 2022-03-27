/*
* Copyright (c) Kp4ws
*
*/

using UnityEngine;
using UnityEngine.EventSystems;
using BDM.Config;

namespace BDM.PlayerControl
{
    public class PaddleController : MonoBehaviour
    {
        [SerializeField] private GameConfig configuration;
        private float skinWidth = 1.1875f;

        private Camera gameCamera;
        private bool disableInput;

        private void Start()
        {
            gameCamera = Camera.main;
        }

        private void Update()
        {
            if (disableInput)
                return;

            Vector2 paddlePos = new Vector2(transform.position.x, transform.position.y);

            if (configuration.autoPlay)
            {
                paddlePos.x = Mathf.Clamp(FindObjectOfType<Ball>().transform.position.x, configuration.minX + skinWidth, configuration.maxX - skinWidth);
            }
            else if (configuration.isMobile)
            {
                paddlePos.x = Mathf.Clamp(GetXPos(), configuration.minX + skinWidth, (configuration.maxX + 1) - skinWidth);
            }
            else
            {
                paddlePos.x = Mathf.Clamp(GetXPosMouse(), configuration.minX + skinWidth, (configuration.maxX + 1) - skinWidth);
            }

            transform.position = paddlePos;
        }

        private float GetXPos()
        {
            if (Input.touchCount == 0)
                return transform.position.x;

            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return transform.position.x;
            }

            Touch touch = Input.GetTouch(0);
            return gameCamera.ScreenToWorldPoint(touch.position).x;
        }

        private float GetXPosMouse()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return transform.position.x;
            }

            return gameCamera.ScreenToWorldPoint(Input.mousePosition).x;
        }
    }

}