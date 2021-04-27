using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System.Text.RegularExpressions;
using System;
using TMPro;

namespace MyDbDemo
{

    // 搜球员ID：返回ID，姓名，队名，出生年，国籍，位置（门将，前锋等，胜率
    public class PlayerInfo
    {
        public string ID;
        public string Name;  // 姓名
        public string nation; // 国籍
        public string birthDate; // 生日（年-月-日）
        public string teamName; // 队名
        public string position; // 位置
        public string winRate; // 胜率

        public PlayerInfo(string ID, string Name, string nation, string birthDate, string teamName, string position, string winRate)
        {
            this.ID = ID;
            this.Name = Name;
            this.nation = nation;
            this.birthDate = birthDate;
            this.teamName = teamName;
            this.position = position;
            this.winRate = winRate;
        }

    }

    // 搜球员姓名（或部分姓名，比如只输入first name):返回球员姓名列表,如果只有一个名字，返回详细信息
    public class NameInfo
    {
        public string ID;
        public string Name;  // 姓名
        public NameInfo(string ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    public class PlayerPanelManager : MonoBehaviour
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

        void PlayerDataSetDisplay(DataSet ds)
        {
            List<PlayerInfo> PlayerList = new List<PlayerInfo>();
            PlayerList.Add(new PlayerInfo("ID", "Name", "Nation", "Birth Year", "Team Name", "Position", "Win Rate"));

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string name = ds.Tables[0].Rows[i][1].ToString();

                PlayerList.Add(new PlayerInfo(ds.Tables[0].Rows[i][0].ToString(), name,
                    ds.Tables[0].Rows[i][4].ToString(), ds.Tables[0].Rows[i][3].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][5].ToString(), ds.Tables[0].Rows[i][7].ToString()));
            }

            #region data list view
            for (int i = 0; i < PlayerList.Count; ++i)
            {
                // row organization
                GameObject rowInstance = Instantiate(RowPrefab) as GameObject;
                rowInstance.transform.parent = content.transform;


                #region attribute organization
                GameObject AttributeInstance;

                AttributeInstance = Instantiate(AttributePrefab) as GameObject;
                AttributeInstance.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = PlayerList[i].ID;
                AttributeInstance.transform.parent = rowInstance.transform;

                AttributeInstance = Instantiate(AttributePrefab) as GameObject;
                AttributeInstance.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = PlayerList[i].Name;
                AttributeInstance.transform.parent = rowInstance.transform;

                AttributeInstance = Instantiate(AttributePrefab) as GameObject;
                AttributeInstance.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = PlayerList[i].nation;
                AttributeInstance.transform.parent = rowInstance.transform;

                AttributeInstance = Instantiate(AttributePrefab) as GameObject;
                AttributeInstance.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = PlayerList[i].birthDate;
                AttributeInstance.transform.parent = rowInstance.transform;

                AttributeInstance = Instantiate(AttributePrefab) as GameObject;
                AttributeInstance.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = PlayerList[i].teamName;
                AttributeInstance.transform.parent = rowInstance.transform;

                AttributeInstance = Instantiate(AttributePrefab) as GameObject;
                AttributeInstance.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = PlayerList[i].position;
                AttributeInstance.transform.parent = rowInstance.transform;

                AttributeInstance = Instantiate(AttributePrefab) as GameObject;
                AttributeInstance.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = PlayerList[i].winRate;
                AttributeInstance.transform.parent = rowInstance.transform;

                #endregion
            }
            #endregion
        }

        public void LoadData()
        {
            // Distroy data
            for(int i=0;i<content.transform.childCount;++i)
            {
                Destroy(content.transform.GetChild(i).gameObject);
            }
            // load data from server
            string SearchInput = SearchInputObject.GetComponent<InputField>().text;
            if(char.IsLetter(SearchInput[0])) // 搜球员姓名（或部分姓名，比如只输入first name):返回球员姓名列表,如果只有一个名字，返回详细信息
            {
                DataSet ds = MySqlSearchManager.Instance.NameSearch(SearchInput);
                if (ds.Tables[0].Rows.Count == 0) return;

                if (ds.Tables[0].Rows.Count > 1) // 多个结果 返回姓名列表
                {
                    List<NameInfo> NameList = new List<NameInfo>();
                    NameList.Add(new NameInfo("ID","Name")); // table title

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string name = ds.Tables[0].Rows[i][1].ToString();
                        NameList.Add(new NameInfo(ds.Tables[0].Rows[i][0].ToString(), name));
                    }

                    #region data list view
                    for (int i = 0; i < NameList.Count; ++i)
                    {
                        // row organization
                        GameObject rowInstance = Instantiate(RowPrefab) as GameObject;
                        rowInstance.transform.parent = content.transform;


                        #region attribute organization
                        GameObject AttributeInstance;

                        AttributeInstance = Instantiate(AttributePrefab) as GameObject;
                        AttributeInstance.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = NameList[i].ID;
                        AttributeInstance.transform.parent = rowInstance.transform;

                        AttributeInstance = Instantiate(AttributePrefab) as GameObject;
                        AttributeInstance.transform.Find("Text").gameObject.GetComponent<TextMeshProUGUI>().text = NameList[i].Name;
                        AttributeInstance.transform.parent = rowInstance.transform;
                        #endregion
                    }
                    #endregion
                }
                else // 单个结果 返回详细值
                {
                    PlayerDataSetDisplay(ds);
                }
            }
            else // 搜球员ID：返回ID，姓名，队名，出生年，国籍，位置（门将，前锋等，胜率
            {
                DataSet ds = MySqlSearchManager.Instance.PlayerIDSearch(SearchInput);
                



                PlayerDataSetDisplay(ds);
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