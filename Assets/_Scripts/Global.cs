using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public enum TGameItem
    {
        Clothes,
        Weapon,
        Consumable
    }

    public enum TLookDirection
    {
        Left,
        Rigth
    }

    public enum TState
    {
        FreeRoam,
        Dialog
    }

    public enum TEquip
    {
        Helmet = 0,
        Armor = 1,
        Shoulder = 2,
        Glove = 3,
        Boots = 4,
        Weapon = 5
    }
}
