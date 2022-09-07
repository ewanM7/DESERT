using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TradingArea : MonoBehaviour
{
    public NPC CurrentNPC;
    public TradingUI _TradingUI;

    public void OnPlayerInteract()
    {
        if (CurrentNPC != null)
        {
            StartCoroutine(StartTrading());
        }
    }

    private IEnumerator StartTrading()
    {
        GameManager.Instance._CameraManager.SwitchToTradingCam();

        CurrentNPC.State = NPCState.Trading;

        yield return new WaitForSeconds(2f);

        _TradingUI = GameManager.Instance._MainUI._TradingUI;
        _TradingUI.CurrentNPC = CurrentNPC;

        if (CurrentNPC.IsSelling)
        {
            CurrentNPC.SetResponseForTradeOffer(null);
        }

        _TradingUI.gameObject.SetActive(true);

        
    }
}
