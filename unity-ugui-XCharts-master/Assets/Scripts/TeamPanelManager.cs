using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System.Text.RegularExpressions;
using TMPro;

namespace MyDbDemo
{

    // 搜球队名：返回队名，教练名，球员及其位置，成立年份，胜率
    public class TeamInfo
    {
        public string teamName; // 队名
        public string coachName;
        public string BirthYear;
        public string hostWinRate; // 胜率
        public string awayWinRate;

        public TeamInfo(string teamName, string coachName, string BirthYear, string hostWinRate, string awayWinRate)
        {
            this.teamName = teamName;
            this.coachName = coachName;
            this.BirthYear = BirthYear;
            this.hostWinRate = hostWinRate;
            this.awayWinRate = awayWinRate;
        }

    }

    public class TeamPanelManager : MonoBehaviour
    {
        private GameObject content;
        private GameObject SearchInputObject;
        [SerializeField]
        private GameObject AttributePrefab;
        [SerializeField]
        private GameObject RowPrefab;
        [SerializeField]
        private GameObject DetailedInfoBoard;


        void Awake()
        {
            content = this.gameObject.transform.Find("Scroll View").Find("Viewport").Find("Content").gameObject;
            SearchInputObject = this.gameObject.transform.Find("InputField").gameObject;
            AttributePrefab.GetComponent<AttributeManager>().DetailInfoBoard = DetailedInfoBoard;
        }


        // 搜球队名：返回队名，教练名，球员及其位置，成立年份，胜率
        public void SearchTeamNameDisplay(DataSet ds)
        {

            List<TeamInfo> TeamList = new List<TeamInfo>();
            TeamList.Add(new TeamInfo("Team Name", "Coach Name",  "Found Year", "Host Win Rate","Away Win Rate"));

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                TeamList.Add(new TeamInfo(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][5].ToString(), ds.Tables[0].Rows[i][8].ToString()));
            }


            #region data list view
            for (int i = 0; i < TeamList.Count; ++i)
            {
                // row organization
                GameObject rowInstance = Instantiate(RowPrefab) as GameObject;
                rowInstance.transform.parent = content.transform;


                #region attribute organization
                GameObject AttributeInstance;

                AttributeInstance = Instantiate(AttributePrefab) as GameObject;
                AttributeInstance.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = TeamList[i].teamName;
                AttributeInstance.transform.parent = rowInstance.transform;

                AttributeInstance = Instantiate(AttributePrefab) as GameObject;
                AttributeInstance.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = TeamList[i].coachName;
                AttributeInstance.transform.parent = rowInstance.transform;


                AttributeInstance = Instantiate(AttributePrefab) as GameObject;
                AttributeInstance.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = TeamList[i].BirthYear;
                AttributeInstance.transform.parent = rowInstance.transform;

                AttributeInstance = Instantiate(AttributePrefab) as GameObject;
                AttributeInstance.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = TeamList[i].hostWinRate;
                AttributeInstance.transform.parent = rowInstance.transform;

                AttributeInstance = Instantiate(AttributePrefab) as GameObject;
                AttributeInstance.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = TeamList[i].awayWinRate;
                AttributeInstance.transform.parent = rowInstance.transform;

                #endregion
            }
            #endregion

        }


        public void LoadData()
        {
            for (int i = 0; i < content.transform.childCount; ++i)
            {
                Destroy(content.transform.GetChild(i).gameObject);
            }
            string SearchInput = SearchInputObject.GetComponent<InputField>().text;
            DataSet ds = MySqlSearchManager.Instance.TeamSearch(SearchInput);
            if (ds.Tables[0].Rows.Count == 0) return;
            SearchTeamNameDisplay(ds);
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