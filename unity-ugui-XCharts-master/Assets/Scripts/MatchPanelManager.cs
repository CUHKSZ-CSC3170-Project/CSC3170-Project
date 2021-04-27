using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System.Text.RegularExpressions;
using TMPro;

namespace MyDbDemo
{

    // 搜比赛ID：返回比赛双方队名以及出场球员，日期，开始时间，地点，比分
    public class MatchInfo
    {
        public string ID;
        public string homeTeamName;
        public string homeTeamPlayer;
        public string awayTeamName;
        public string awayTeamPlayer;
        public string date;
        public string startTime;
        public string revenue;
        public string score;

        public MatchInfo(string ID,string homeTeamName,string homeTeamPlayer,string awayTeamName,string awayTeamPlayer,string date,string startTime,string revenue,string score)
        {
            this.ID = ID;
            this.homeTeamName = homeTeamName;
            this.homeTeamPlayer = homeTeamPlayer;
            this.awayTeamName = awayTeamName;
            this.awayTeamPlayer = awayTeamPlayer;
            this.date = date;
            this.startTime = startTime;
            this.revenue = revenue;
            this.score=score;
        }

    }

    public class MatchPanelManager : MonoBehaviour
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


        // 搜比赛ID：返回比赛双方队名以及出场球员，日期，开始时间，地点，比分
        public void SearchMatchIDDisplay(DataSet ds)
        {
            List<MatchInfo> MatchList = new List<MatchInfo>();
            MatchList.Add(new MatchInfo("ID","Home Team Name", "Home Team Player" , "Away Team Name", "Away Team Player","Date","Start Time", "Revenue", "Score"));

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string date = ds.Tables[0].Rows[i][3].ToString();

                string matchID = ds.Tables[0].Rows[i][0].ToString();
                string homeTeamName = ds.Tables[0].Rows[i][1].ToString();
                string awayTeamName = ds.Tables[0].Rows[i][2].ToString();

                DataSet d1 = MySqlSearchManager.Instance.MatchSearch_TeamPosition(matchID,homeTeamName);
                DataSet d2 = MySqlSearchManager.Instance.MatchSearch_TeamPosition(matchID, awayTeamName);
                Debug.Log("Success");

                string homeConfig="Config  ", awayConfig="Config  ";
                for (int j = 0; j < d1.Tables[0].Rows.Count; j++)
                {
                    homeConfig += "\n"+d1.Tables[0].Rows[j][1].ToString() +" ["+ d1.Tables[0].Rows[j][3].ToString() + "]";
                }
                for (int j = 0; j < d2.Tables[0].Rows.Count; j++)
                {
                    awayConfig += "\n"+d2.Tables[0].Rows[j][1].ToString() + " [" + d2.Tables[0].Rows[j][3].ToString() + "]";
                }


                MatchList.Add(new MatchInfo(matchID,homeTeamName, homeConfig,awayTeamName,awayConfig,date.Substring(0,date.IndexOf(' ')) , ds.Tables[0].Rows[i][4].ToString(), ds.Tables[0].Rows[i][5].ToString(), ds.Tables[0].Rows[i][6].ToString()+" : "+ ds.Tables[0].Rows[i][7].ToString()));
            }

            #region data list view
            for (int i = 0; i < MatchList.Count; ++i)
            {
                // row organization
                GameObject rowInstance = Instantiate(RowPrefab) as GameObject;
                rowInstance.transform.parent = content.transform;


                #region attribute organization
                GameObject AttributeInstance;

                AttributeInstance = Instantiate(AttributePrefab) as GameObject;
                AttributeInstance.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = MatchList[i].ID;
                AttributeInstance.transform.parent = rowInstance.transform;

                AttributeInstance = Instantiate(AttributePrefab) as GameObject;
                AttributeInstance.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = MatchList[i].homeTeamName;
                AttributeInstance.transform.parent = rowInstance.transform;

                AttributeInstance = Instantiate(AttributePrefab) as GameObject;
                AttributeInstance.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = MatchList[i].homeTeamPlayer;
                AttributeInstance.transform.parent = rowInstance.transform;

                AttributeInstance = Instantiate(AttributePrefab) as GameObject;
                AttributeInstance.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = MatchList[i].awayTeamName;
                AttributeInstance.transform.parent = rowInstance.transform;

                AttributeInstance = Instantiate(AttributePrefab) as GameObject;
                AttributeInstance.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = MatchList[i].awayTeamPlayer;
                AttributeInstance.transform.parent = rowInstance.transform;

                AttributeInstance = Instantiate(AttributePrefab) as GameObject;
                AttributeInstance.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = MatchList[i].date;
                AttributeInstance.transform.parent = rowInstance.transform;

                AttributeInstance = Instantiate(AttributePrefab) as GameObject;
                AttributeInstance.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = MatchList[i].startTime;
                AttributeInstance.transform.parent = rowInstance.transform;

                AttributeInstance = Instantiate(AttributePrefab) as GameObject;
                AttributeInstance.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = MatchList[i].revenue;
                AttributeInstance.transform.parent = rowInstance.transform;

                AttributeInstance = Instantiate(AttributePrefab) as GameObject;
                AttributeInstance.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = MatchList[i].score;
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
            // 搜比赛时间：返回这个时间段所有比赛的ID
            if(SearchInput[0]=='#') // 规定时间段以#开始
            {
                DataSet ds = MySqlSearchManager.Instance.MatchDateSearch(SearchInput.Substring(1));
                if (ds.Tables[0].Rows.Count == 0) return;
                SearchMatchIDDisplay(ds);
            }
            else // 搜比赛ID：返回比赛双方队名以及出场球员，日期，开始时间，地点，比分
            {
                DataSet ds = MySqlSearchManager.Instance.MatchIDSearch(SearchInput);
                if (ds.Tables[0].Rows.Count == 0) return;
                SearchMatchIDDisplay(ds);
            }

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