using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTipGenerator
{
    public enum Boss {
        Agas,
        Nhang,
        Tsovinar,
        Sandaramet
    }

    public enum Character {
        John,
        Shavo,
        Daron,
        Serj
    }

    public static string GenerateDeathTip(Boss boss, string bandMember, int phase)
    {
        List<string> tipPool = new List<string>(generalTips);

        Character character;
        switch(bandMember)
        {
            case "John":
                character = Character.John;
                break;
            case "Shavo":
                character = Character.Shavo;
                break;
            case "Daron":
                character = Character.Daron;
                break;
            case "Serj":
                character = Character.Serj;
                break;
            default:
                character = Character.John;
                break;
        }

        switch(boss) {
            case Boss.Agas:
                for(int i = 0; i <= phase; ++i)
                {
                    tipPool.AddRange(agasTips[i]);
                }
                break;
            case Boss.Nhang:
                for(int i = 0; i <= phase; ++i)
                {
                    tipPool.AddRange(nhangTips[i]);
                }
                break;
            case Boss.Tsovinar:
                for(int i = 0; i <= phase; ++i)
                {
                    tipPool.AddRange(tsovinarTips[i]);
                }
                break;
            case Boss.Sandaramet:
                for(int i = 0; i <= phase; ++i)
                {
                    tipPool.AddRange(sandarametTips[i]);
                }
                break;
        }

        switch(character) {
            case Character.John:
                tipPool.AddRange(johnTips);
                break;
            case Character.Daron:
                tipPool.AddRange(daronTips);
                break;
            case Character.Shavo:
                tipPool.AddRange(shavoTips);
                break;
            case Character.Serj:
                tipPool.AddRange(serjTips);
                break;
        }

        return tipPool[Random.Range(0, tipPool.Count)];
    }

    private static readonly string[][] agasTips = {
        new string[] {
            "Agas will light her candles periodically, but they can be extinguished with a melee attack. Put them out before they become an issue!",
        },
        new string[] {
            "Agas has a few different projectile attacks. Learning their patterns will make them easier to avoid!",
        },
        new string[] {
            "Agas can hurt you while she thrashes across the screen, but she’s also vulnerable to attacks. Capitalize on her weaknesses to maximize your damage!",
        },
        new string[] {
        }
    };

    private static readonly string[][] nhangTips = {
        new string[] {
            "The Nhang’s serpent doesn’t deal damage, but it can knock you into other attacks. Keep an eye out for the strike!"
        },
        new string[] {
            "The Nhang’s hands can make it more difficult to avoid attacks. Try to stay off the ground!",
            "When The Nhang raises her hand from the ground, it acts like a platform. You can even drop through it by double tapping the crouch button!",
        },
        new string[] {
            "The Nhang’s blood rain attack can be difficult to avoid, but the droplets stop when they hit a platform. Be sure to keep an eye on the sky!"
        },
        new string[] {
        }
    };

    private static readonly string[][] tsovinarTips = {
        new string[] {
            "Tsovinar is invulnerable while in her static form. Hitting the antennas with a melee attack can end it early!"
        },
        new string[] {
            "The arrows on Tsovinar’s beam attack tell you which direction you need to be moving at the moment the beam crosses your body. Keep an eye out for them!"
        },
        new string[] {
            "The electricity on the floor makes the ground dangerous, but it always comes from the right side of the screen. Perfecting your double jump move is crucial!"
        },
        new string[] {
        }
    };

    private static readonly string[][] sandarametTips = {
        new string[] {
            "Sandaramet’s minions can be frustrating, but they can be killed in a single hit. Don’t let her summon too many!"
        },
        new string[] {
            "Sandaramet’s fire beam and blood sphere attacks can be difficult to avoid. Use the platforms to your advantage!"
        },
        new string[] {
        },
        new string[] {
            "Watch out for the stalactites that can appear on the ceiling, they’ll fall when they reach your position!",
            "Sandaramet’s hand can crush you if you stay on it too long. Jump off or drop through it before it’s too late!"
        },
        new string[] {
            "Sandaramet’s flame pillar attack can block access to part of the arena. Watch for the smoke and plan accordingly!",
            "Sandaramet is on the retreat—chase her down and finish this thing!",
        },
        new string[] {
            "You’re so close to saving Metal, don’t give up now! Give it everything you’ve got!"
        }
    };

    private static readonly string[] generalTips = {
        "A new band member is unlocked after defeating each boss. Try them all out!",
        "Each band member has a different melee attack, ranged attack, Super, and double jump. Choose wisely!",
        "Double tapping the crouch button will let you drop through platforms. Use this to avoid attacks!",
        "A new music track is unlocked after defeating each boss. You can mix and match music with levels however you want!"
    };

    private static readonly string[] johnTips = {
        "Don’t underestimate the power of melee attacks—they deal more damage, charge your Super faster, and most are able to block projectiles!",
        "John’s Super has the potential to deal a lot of damage, but leaves you vulnerable for the duration. Time it wisely!",
        "John’s second jump can provide you with an extra height boost when it really matters. Don’t forget about it!"
    };

    private static readonly string[] shavoTips = {
        "Don’t underestimate the power of melee attacks—they deal more damage, charge your Super faster, and most are able to block projectiles!",
        "Shavo’s Super can be hard to aim, but it makes you invulnerable for the duration and deals a lot of damage if it hits!",
        "Shavo’s double jump is an angled dash. Use it to dodge your enemies’ attacks!"
    };

    private static readonly string[] daronTips = {
        "Daron’s melee attacks don’t block projectiles, but they do a lot more damage and charge your Super even faster!",
        "Daron’s Super doesn’t do as much damage as the other band members, but it’s easier to aim and makes you temporarily invulnerable.",
        "Daron’s double jump doesn’t actually affect his trajectory, instead it destroys projectiles you come into contact with. Time it wisely!"
    };

    private static readonly string[] serjTips = {
        "Don’t underestimate the power of melee attacks—they deal more damage, charge your Super faster, and most are able to block projectiles!",
        "Serj’s Super hits the entire screen, and makes you invulnerable for the duration. Use it to your advantage!",
        "Serj’s double jump grants you full aerial control—mastering it can change the tide of battle!"
    };
}
