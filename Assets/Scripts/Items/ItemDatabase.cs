using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Data/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    public List<BaseItemData> AllItems;

    public AnimationCurve QualityValueCurve;

}

[System.Serializable]
public class MaterialMultiplier
{
    [SerializeField]
    public float Multiplier;
    [SerializeField]
    public ItemDescriptor Material;
}


