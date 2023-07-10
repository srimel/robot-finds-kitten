# Robot Find Kitten by Stuart Rimel

This is a C# implementation of Robot Finds Kitten. The game is an ascii character based game where the player is a robot that tries to find the kitten among several objects strewn about the world. The player moves around with the arrow keys and as they come across objects the player will see a text description of that object. The player wins when they find the kitten. 

## Build and Run

### Requirements

This project requires the .NET Core SDK to be installed. The SDK can be downloaded from [here](https://dotnet.microsoft.com/download). Once the SDK is installed, the project can be built and run.
And ideally, you should have Visual Studio which can be downloaded from [here](https://visualstudio.microsoft.com/downloads/).

This project also requires NCurses to be installed. It is always installed on OSX and usually on Linux. For linux, consult with your distro's package manager to install NCurses if it's missing. On windows, you can find a mingw version of NCurses [here](https://invisible-island.net/ncurses/#download_mingw) 

### To Run 
Open up the project's `.sln` file within visual studio and build the project. Once the project is built, open up a terminal and navigate to the project directory and run the game with `dotnet run`. The game does not run correctly on Visual Studio's integrated terminal. The game must be run on a separate terminal window. 

---

## Assignment Questions

### Question 1
The player experience I'm going for is a whimsical feeling. I want the player to be exploring the game field and the text for the objects that are found are all written from a first person perspective such that it feels like it is the player's thoughts as they explore the game area. 

### Question 2
I think one enhancement would be to add color to the game objects. They are currently one color but adding different colors could enhance the visual style of gameplay. 

### Question 3
I think one simple enhancement to the gameplay mechanics might be to add a counter that will keep track of how many game objects the player has found that are not the kitten. This would allow for players to get a "score" for their game session with the lower score being better.

### Question 4
I'm not too confident that the game is bug free. I think there are some bugs that could be found with more testing. I think the game is pretty stable but I think there are some edge cases that could be found with more testing.

### Question 5
I got my friend to play a few times and they said it was alright. They had never played a Robot Finds Kitten game before so they didn't really know what to expect. They said it was a little confusing at first but once they got the hang of it, it was pretty fun. They said they would play it again (they might have just been saying that to be nice though). They didn't really get the whimsical vibe I was going for, but that might have been because they didn't know what to expect.

### Question 6
I read in the book that what is true of my experience may not be true for others. I think when I got my friend to playtest my game this really hit home for me. I thought I got near a whimsical experience for the player, but my friend did not appear to mirror that sentiment on his playtests. He thought the game was alright, but he didn't really get the whimsical vibe I was going for. I think this is because he didn't know what to expect from the game. He had never played a Robot Finds Kitten game before so he didn't really know what to expect.

### Question 7
I think a AAA version of this game could be kind of cool. I imagine a 3D world where the player is a nicely modeled Robot that explores the world looking for the kitten. Along they way there might be clues that the robot can use to help hone in on the location of the kitten. The robot might also have some extra features that can be used to help find the kitten like scanning the surrounding for clues that pop out. 