using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Public Field
    public GameObject PlayerPanel;
    public GameObject TeamPanel;
    public GameObject MatchPanel;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        PlayerPanel.SetActive(false);
        TeamPanel.SetActive(false);
        MatchPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
