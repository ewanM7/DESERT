using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dice
{
    public static int Roll(Die die)
    {
        switch (die)
        {
            case Die.d4:
                return Random.Range(1, 5);

            case Die.d6:
                return Random.Range(1, 7);

            case Die.d8:
                return Random.Range(1, 9);

            case Die.d10:
                return Random.Range(1, 11);

            case Die.d12:
                return Random.Range(1, 13);

            case Die.d20:
                return Random.Range(1, 21);

            default:
                return Random.Range(1, 7);
        }
    }
}

public enum Die
{
    d4,
    d6,
    d8,
    d10,
    d12,
    d20
}