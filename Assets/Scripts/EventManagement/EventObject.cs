/*
* Copyright (c) Kp4ws
*
*/

namespace BDM.EventManagement
{
	public class EventObject<T>
	{
		public readonly object sender;
		public readonly T value;

		public EventObject(object sender, T value)
		{
			this.sender = sender;
			this.value = value;
		}
	}
}
