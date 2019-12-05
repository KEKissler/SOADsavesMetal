Boss Attack System Guide
There exist N parts of this system
1. the BossSpecificAttackManager
2. the attack initializer
3. the phase initializer
4. the CoroutineRunner

----+
1   | BossSpecificAttackManager
----+

This script exists in the game scene as a Monobehavior. It it responsible for
keeping track of the boss hp% and for the loop that starts attacks. Each time
an attack ends it runs code to determine if the phase changed, if the attack
options changed (player got close to boss, became low hp, on a platform) and
based on the current attack option settings selects the next attack and runs
it.

----+
2   | Attack Initializer
----+

Because every attack in this system is a scriptable object, and because every
scriptable object is part of the asset database and not the scene asset 
database, attacks cannot store live references to anything in any scene.
Attacks have to rely on live references to things in scenes to know
information like where the player is or where the boss is in order to spawn
hitboxes at the right locations (As needed per attack). So in order to get
those live scene references to a place where attacks can see them, there
needs to exist a Monobehavior in the scene that has live scene references to
any and all data an attack needs as well as a list of all scriptable object
attacks. The attack initializer is responsible for collecting all the live
scene data into one object and calling every scriptable object's Initialize
function and using that to pass aong the data. Each attack saves the specific
references that attack needs from the BossSpecificAttackData object and for
the rest of the running of the program that attack has the data it needs to
run.

----+
3   | Phase Initializer
----+

So this is created for the same reason as the attack initializer above, live
scene refs being needed in a scriptable object. In this case, a boss specific
Phase object holds multiple attack options, sets of attacks that the boss
could execute. At attack selection time, the Phase object makes a decision on
which attack option to use and that decision is based on live scene
references. Things like checking distance between boss and player to see if
it should use the close range attack moveset or checking the player's HP
to see if it should instead use less cheap attacks so as to not kill players
in cheesy ways. That sort of thing. Because each phase has the same decisions
to make as any other phase, they all just take the same data, that some
Monobehavior in the scene is responsible for grabbing and passing along.

----+
4   | CoroutineRunner
----+

So attacks as scriptable objects are containers for the details of an attack.
Some attacks do something once and end, while others have scheduled times
when hitboxes need to be at places and animations need to play. In order to
accomodate these different requirements of the attack system, every attack
has three functions OnEnd, OnStart, and Execute. OnEnd and OnStart handle the
former types of attacks, they get called once at the end and start of an
attack and are intended to allow one time attacks to go once, or persistent
attacks to setup and spawn objects needed for the attack and then clean them
up. The execute part of an attack is a Coroutine in order to allow for the
specific time scheduling parts of attacks to work. However scriptable objects
don't run anything on their own. So in order to actually run the execute
coroutine of any attack, a monobehavior has to run it. There did not exist a
startup scene in this project when I was dealing with this so each scene has
its own coroutine runner now. That is a monobehavior singleton that is
statically referenceable so it can run the execute part of attacks. It will
persist across scenes so if in the future you clean things up, you can have
it in just like the main menu or something and all scenes will be able to
find it.