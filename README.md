# Block-Destroyer-Mobile
## About ##
- A mobile remastered version of my original Block Destroyer game.
- Destroy blokcs by bouncing the ball of the paddle.

## Touch Controls ##
- Press/Release to launch the ball
- Slide to move paddle across screen

## Game Mechanics ##
- Each level is procedurally generated so that the blocks appear random each time, thus increasing the replay value.
- When all the blocks are destroyed, a count down timer will start signalling the next wave of blocks to generate.

## Custom Event System ##
- I created my own event system based on the Publisher/Subscriber pattern.
- The publishers will publish an event to a specific channel
- The subscribers will subscribe to one or more of the channels and their appropriate method(s) will be invoked when an event is published to that channel.
- I’m using an enum for the channels. Whenever a new event channel is needed, I simply append the name of the channel, such as START, into my enum.
- For more detail, here's a link within the code to my event system: https://github.com/kp4ws/Block-Destroyer-Mobile/tree/main/Assets/Scripts/EventManagement

## Link ##
- I've released this game on the Google Play Store here:  
https://play.google.com/store/apps/details?id=com.Kp4wsGames.BlockDestroyer
