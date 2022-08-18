using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Data/Dialogue")]
public class Dialogue : ScriptableObject
{
    public bool ExecuteInSequence;
    public string[] Phrases;
}
