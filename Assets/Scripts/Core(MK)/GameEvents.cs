using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameEvents
{
    public static Action<int> OnCoinsGained;
    public static Action<string> OnItemBought;
}
