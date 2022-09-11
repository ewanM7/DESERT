using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tooltip : MonoBehaviour
{
    public RectTransform rt;
    public TextMeshProUGUI ItemNameText;
    public TextMeshProUGUI ItemValueText;

    private void OnEnable()
    {
        Vector2 pos = GameManager.Instance.MousePos;
        rt.position = new Vector3(pos.x, pos.y, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = GameManager.Instance.MousePos;
        rt.position = new Vector3(pos.x, pos.y, 0f);
    }

    public void ReflectItem(Item item)
    {
        ItemNameText.text = item.Name;
        ItemValueText.text = item.Value.ToString();
    }
}
