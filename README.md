# Asteroids Clone

**Asteroids Clone** is a clone of the classic "Asteroids" arcade game written in C# which runs in the Windows command prompt (aka: console). The goal of this project was to re-create a classic game purely from scratch without any 3rd party frameworks/libraries (i.e. MonoGame, Pygame, etc.). In order to achieve this goal I wrote my own minimal game framework for the console in which the game was programmed! This minimal game framework (name not yet decided) supports: loading and rendering multiple sprites (made up of ASCII art), checking if two sprites are colliding, entering and exiting different game scenes, running at a target FPS, and a few other things.

My next goal is to start making a more fully fushed-out version of the game framework I created for myself and others to use! The framework will be as streight forward and as simple as possible so that newer game devs (like me!) can dive right into learning game programming concepts (such as world generation) before moving to more complex frameworks or full on game engines. Alternatively it could be used to just experiment and have fun!

### Did I use AI in this project?

On a few occasions I did ask an AI for help understanding how to implement a feature that I wasn't 100% sure how to do (such as setting a target FPS for the game). Here's what I did and didn't do:
| I did       | I didn't    |
| ----------- | ----------- |
| Ask AI how to implement a feature | Ask AI to generate any code for me |
| Go through answers I received to make sure I understood and agreed with them (if I used the answer given to me) | Blindly copy/paste/trust the code/concepts from answers |

Note: in the case of implementing AABB collision detection I did copy the AI's code. But this was only because the AABB formula is well known and for some reason I couldn't find a cleanly readable version online that wasn't buried in a ton of other code. So it was just faster to ask AI. Even in this case I didn't blindly trust the answer. I took time to pick apart the code and made sure I understood how and why it worked.
