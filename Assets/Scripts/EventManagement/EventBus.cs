/*
* Copyright (c) Kp4ws
*
*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace BDM.EventManagement
{
	public class EventBus
	{
		private Dictionary<EventChannel, List<Action<object>>> subscribers = new Dictionary<EventChannel, List<Action<object>>>();
		private static EventBus _instance;
		public static EventBus Instance
		{
			get
			{
				if (_instance == null)
					_instance = new EventBus();
				return _instance;
			}
		}

		public void Publish<T>(EventChannel channel, object sender, T value)
		{
			if (!subscribers.ContainsKey(channel))
				return;

			var eventHandlers = subscribers[channel];
			var payload = new EventObject<T>(sender, value);

			//Sometimes this runs into an error if unsubscribe event happens. ToList() is one way to resolve it
			//TODO figure out how to resolve this without using .ToList()
			foreach (var eventHandler in eventHandlers.ToList())
			{
				eventHandler?.Invoke(payload);
			}
		}

		public void Subscribe(EventChannel channel, Action<object> eventHandler)
		{
			if (subscribers.ContainsKey(channel))
			{
				List<Action<object>> actionList = subscribers[channel];
				if (!actionList.Contains(eventHandler))
				{
					actionList.Add(eventHandler);
				}
			}
			else
			{
				List<Action<object>> actionList = new List<Action<object>> { eventHandler };
				subscribers.Add(channel, actionList);
			}

		}

		public void Unsubscribe(EventChannel channel, Action<object> eventHandler)
		{
			if (!subscribers.ContainsKey(channel))
				return;

			List<Action<object>> actionList = subscribers[channel];
			if (actionList.Contains(eventHandler))
			{
				actionList.Remove(eventHandler);
			}
			if (actionList.Count == 0)
			{
				subscribers.Remove(channel);
			}
		}
	}
}
