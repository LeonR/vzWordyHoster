vzWordyHoster: A Windows app for hosting word games in VZones

Author: Spud

Goals:
To implement a modern Windows application that allows a VZones user to
host games such as Trivia, Devil's Dictionary and Word Scramble.

My agenda:
The majority of games being hosted in VZones today are games of
chance such as bingo. Those don't interest me at all. Several years ago I
used to enjoy playing, in VZones, games that required some thought, which
were therefore more rewarding, such as trivia quizzes and word games like
"Devil's Dictionary" (the host gives a word's dictionary definition, then
incrementally reveals letters in the word until someone guesses it) and
"Word Scramble" (solve the anagram). I want to reverse the trend and make
games like these more popular again in VZones. If such games become more
popular, VZones (which has a dwindling population) might become a more
attractive place for those who are not happy to while away hours clicking
a bingo card.

My experience is that the existing VZones game-hosting apps are buggy and ugly.
I'm not aware that anyone (as of May 2013) is creating new apps that might
offer a modern-looking user interface and be more robust. I want to create
an app that is capable of hosting games such as these, through a common
interface.

At the same time, this might encourage others to make their own VZones apps.
On the internet there are a few bits of VB6 source code that demonstrate how
to use wadapi.dll, but nothing modern. This will be a C# app, developed in
SharpDevelop, a freely available IDE.

Roadmap:
vzWordyHoster will first implement a trivia hoster, simply because a trivia
game is the simplest, functionally, of the three games that I have mentioned.
Thereafter I intend to add Devil's Dictionary and Word Scramble. After those,
I'm open to suggestions!

Warning!
I am not a professional coder, but I can get things done if I clutch my
forehead and think long and hard. This is also my first C# project. If you
spot defects, inelegance or bad style in my code, please clone the project,
make the code better, then send me a git pull request -- or just send me a
message with your suggestions.