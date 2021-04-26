using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AttributeManager : MonoBehaviour
{
    public GameObject DetailInfoBoard;

    // Start is called before the first frame update
    private void Start()
    {
        Button btn = this.gameObject.GetComponent<Button>();
        btn.onClick.AddListener(DisplayDetailAttribute);
    }

    void DisplayDetailAttribute()
    {
        DetailInfoBoard.transform.GetChild(0).gameObject.GetComponent<Text>().text = this.gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text;
    }
}
