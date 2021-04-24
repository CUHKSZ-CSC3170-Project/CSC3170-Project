using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamPanelManager : MonoBehaviour
{
    private GameObject content;


    void Awake()
    {
        content = this.gameObject.transform.Find("Scroll View").Find("Viewport").Find("Content").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    #region UI control

    public void ShowPanel()
    {
        this.gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        this.gameObject.SetActive(false);
    }

    #endregion
}
