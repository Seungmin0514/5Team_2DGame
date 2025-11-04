using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameSignals
{
    public static Action<int> OnCoinsChanged;         //코인 변경
    public static Action<string> OnSkinBought;        // skinId 해금 완료
    public static Action<string> OnSkinEquipped;      // skinId 장착 완료
}
