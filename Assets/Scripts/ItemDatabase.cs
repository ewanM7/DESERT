using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Data/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> AllItems;

    public AnimationCurve QualityValueCurve;
}




