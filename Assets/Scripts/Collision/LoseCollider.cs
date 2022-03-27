/*
* Copyright (c) Kp4ws
*
*/

using UnityEngine;
using BDM.EventManagement;

namespace BDM.Collision {
	public class LoseCollider : MonoBehaviour 
	{
        private EventBus bus;

        private void Awake()
        {
            bus = EventBus.Instance;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            bus.Publish(EventChannel.TakeLife, this, collision);
		}
	}
}