using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    public Trait[] Traits;
    public Inventory inventory;
    public NPCState State;

    public GameObject SpeechCanvas;
    public TextMeshProUGUI SpeechText;

    public float SpeechCanvasHorizontalOffset;
    public Vector3 SpeechCanvasVerticalOffset;

    public void SetItems(Item[] items)
    {
        foreach(Item item in items)
        {
            inventory.AddItem(item);
        }
    }

    private void Update()
    {
        if(State == NPCState.Speaking)
        {
            //set world position and rotation of the speech bubble

            Vector3 FromCameraToNPC = SpeechCanvas.transform.position - GameManager.Instance._CameraManager.MainCamera.transform.position;
            FromCameraToNPC.y = 0;
            Vector3 Perpendicular = Vector3.Cross(FromCameraToNPC, Vector3.up);
            Perpendicular.Normalize();

            SpeechCanvas.transform.position = transform.position + SpeechCanvasVerticalOffset + (Perpendicular * SpeechCanvasHorizontalOffset);

            SpeechCanvas.transform.LookAt(GameManager.Instance._CameraManager.MainCamera.transform);
            SpeechCanvas.transform.Rotate(0, 180f, 0);


        }
    }

    public void OnPlayerInteract()
    {

    }

}

public enum Trait
{
    Alcoholic,
    SweetTooth,
    Moral,
    Shady,
    BulkBuyer,
    Collector,
    Haggler,
    Impatient,
    Trusting,
    AnimalLover,
    Gambler,
    HighStandards,
    Modest,
    Vain,
    Cynical,
    Naive,
    LocalTrader,
    Tourist,
}

public enum NPCState
{
    Traveling,
    Trading,
    Speaking,
}