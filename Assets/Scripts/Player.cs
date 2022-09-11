using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Money;
    public Inventory inventory;

    public const int MAX_HEALTH = 100;

    public int Health;
    public int Hunger;
    public int Thirst;

    public PlayerInteractHitbox InteractHitbox;

    private void Awake()
    {
        inventory = new Inventory(10);
    }

    public void OnPlayerInteract()
    {
        InteractHitbox.OnPlayerInteract();
    }

}
