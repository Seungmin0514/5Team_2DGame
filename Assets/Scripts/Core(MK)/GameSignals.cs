using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class GameSignals
{
        //상태 (UI가 자동 갱신할 때 씀)
        public static Action<int> OnCoinsChanged;         // newCoins

        //행동 보고 (퀘스트 진행 등 로직 트리거)
        public static Action<int> OnCoinsGained;       // 러너/정산: 코인 N개 획득
        public static Action<string> OnItemBought;        // 상점: 특정 itemId 구매 완료
    
}
