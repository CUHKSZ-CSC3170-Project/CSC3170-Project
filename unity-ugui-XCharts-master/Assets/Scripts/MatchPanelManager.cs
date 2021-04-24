using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MyDbDemo
{

   




    public class MatchPanelManager : MonoBehaviour
    {

        private GameObject content;
        private GameObject SearchInputObject;
        [SerializeField]
        private GameObject AttributePrefab;



        void Awake()
        {
            content = this.gameObject.transform.Find("Scroll View").Find("Viewport").Find("Content").gameObject;
            SearchInputObject = this.gameObject.transform.Find("SearchButton").Find("Text").gameObject;
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

}