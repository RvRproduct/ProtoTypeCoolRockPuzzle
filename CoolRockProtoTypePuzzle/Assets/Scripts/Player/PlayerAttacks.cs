using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Attacks
{
    None,
    GuitarNormal,
    GuitarSpecial,
    DrumNormal,
    DrumSpecial,
    KeyboardNormal,
    KeyboardSpecial,
    Scream,
    Pitch,
    Phase,
    Laser
}

public static class PlayerAttacks
{
    public static readonly Dictionary<Attacks, string> AttackNames = new Dictionary<Attacks, string>()
    {
        { Attacks.None, "None"},
        { Attacks.Scream, "Scream"},
        { Attacks.Pitch, "Pitch"},
        { Attacks.Phase, "Phase"},
        { Attacks.Laser, "Laser" },
        { Attacks.DrumSpecial, "DrumSpecial" }
    };

    public static string GetAttackName(Attacks attack)
    {
        if (AttackNames.TryGetValue(attack, out string attackName))
        {
            return attackName;
        }

        return "Unknown Attack";
    }
}