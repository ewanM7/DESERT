using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemDragWidget : MonoBehaviour
{
    public RectTransform rt;
    public TextMeshProUGUI ItemNameText;
    public Image ItemUISprite;

    private void Awake()
    {
        Vector2 pos = GameManager.Instance.MousePos;
        rt.position = new Vector3(pos.x, pos.y, 0f);
    }

    private void Update()
    {
        Vector2 pos = GameManager.Instance.MousePos;
        rt.position = new Vector3(pos.x, pos.y, 0f);
    }

    public void ReflectItem(Item item)
    {
        ItemNameText.text = item.Name;
    }
}
