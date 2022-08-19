using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TradingArea : MonoBehaviour
{
    public NPC CurrentNPC;

    public void OnPlayerInteract()
    {
        GameManager.Instance._CameraManager.SwitchToTradingCam();
    }
}
