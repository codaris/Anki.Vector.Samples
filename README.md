# Anki Vector .NET SDK Sample applications

This solution contains 2 applications and 15 tutorial applications for the Anki Vector SDK.

## Getting Started

### Documentation

* [Anki Vector .NET SDK Documentation](https://codaris.github.io/Anki.Vector.SDK/)

* [Anki Vector .NET SDK GitHub Project](https://github.com/codaris/Anki.Vector.SDK)


### Vector SDK Configuration and Authentication

In order to run these samples, you will need authenticate with the robot and create a configuration file
that is stored in your user profile.  This SDK uses the same configuration file as the Python SDK
and the [Vector Explorer](https://www.weekendrobot.com/vectorexplorer) application.

The easiest way to get setup with Vector on you Windows PC is to install [Vector Explorer](https://www.weekendrobot.com/vectorexplorer) and configure your robot through that application.  However, you 
can also use the `VectorConfigure` command line tool located in this solution. 

## Sample Projects

### `VectorConfigure`

This is a command line tool for configuring and authenticating the SDK with your Vector robot.  You will need to run this
command to create a configuration file in your profile to run the remaining SDK sample applications in this solution.

You will be prompted for your robot’s name, ip address and serial number. You will also be asked for your Anki login and password. Make sure to use the same account that was used to set up your Vector.  These credentials give full access to your robot, including camera stream, audio stream and data. *Do not share these credentials*.

### `ReserveControl`

Command line tool for reserving control of Vector.  Reserving control will keep Vector still during development so he doesn't drive off in-between runs of your code. It's also useful to keep Vector still when you need some peace and quiet.  

### `Tutorial_01_HelloWorld`

Everyone's first project, Vector will speak "Hello World".

### `Tutorial_02_DriveSquare`

Make Vector drive in a square by going forward and turning left 4 times in a row.

### `Tutorial_03_Motors`

Drive Vector's wheels, lift and head motors directly.  This is an example of how you can also have low-level control of Vector's motors (wheels, lift and head) for fine-grained control and ease of controlling multiple things at once.

### `Tutorial_04_Animation`

Play a few of Vector's animations.  Play an animation using a trigger, and then another animation by name.

### `Tutorial_05_DriveOnOffCharger`

Tell Vector to return to his charger and then drive off.

### `Tutorial_06_FaceImage`

Display an JPEG image on Vector's face.

### `Tutorial_07_DockWithCube`

Tell Vector to drive up to a seen cube.  This example demonstrates Vector driving to and docking with a cube, without picking it up.  Vector will line his arm hooks up with the cube so that they are inserted into the cube's corners.  You must place a cube in front of Vector so that he can see it.

### `Tutorial_08_DownloadPhoto`

Downloads all the photos stored in Vector. Before running this script, please make sure you have successfully had Vector take a photo by saying, "Hey Vector! Take a photo."

### `Tutorial_09_EyeColor`

Set Vector's eye color. Note that Vector's eye color will return to normal when the connection terminates.

### `Tutorial_10_PlayAudio`

Play audio files through Vector's speaker.  This will play an embedded MP3 music file through Vector's speakers.

### `Tutorial_11_DriveToCliffAndBackUp`

Make Vector drive to a cliff and back up. Place the robot about a foot from a "cliff" (such as a tabletop edge), then run this script.

### `Tutorial_12_ControlPriorityLevel`

Vector maintains SDK behavior control after being picked up. During normal operation, SDK programs cannot maintain control over Vector when he is at a cliff, stuck on an edge or obstacle, tilted or inclined, picked up, in darkness, etc.   This script demonstrates how to use the highest level SDK behavior control to make Vector perform actions that normally take priority over the SDK.

### `Tutorial_13_UserIntent`

Return information about a voice commands given to Vector. The user_intent event is only dispatched when the SDK program has requested behavior control and Vector gets a voice command. After the robot hears "Hey Vector! ..." and a valid voice command is given for example "...what time is it?") the event will be dispatched and displayed.
 
### `Tutorial_14_FaceEvent`

Wait for Vector to see a face, and then print output to the console. This script demonstrates how to set up a listener for an event. It subscribes to event `ObservedFace`.  When that event is dispatched, the lambda is called, which prints text to the console.  Vector will also say "I see a face" one time, and the program will exit when he finishes speaking.

### `Tutorial_15_FaceFollower`

Make Vector turn toward a face.  This script shows off the turn towards face behavior.  It will wait for a face and then constantly turn towards it to keep it in frame.

## Getting Help

There are numerous places to get help with programming Vector using the .NET SDK:

* **Official Anki developer forums**: https://forums.anki.com/

* **Anki Vector developer subreddit**: https://www.reddit.com/r/ankivectordevelopers

* **Anki robots Discord chat**: https://discord.gg/FT8EYwu

 


