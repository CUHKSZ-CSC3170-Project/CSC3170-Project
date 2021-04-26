using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using System.Text.RegularExpressions;

namespace MyDbDemo
{

    public class PlayerInfo
    {
        public string firstName; // 姓
        public string secondName;  // 名
        public string nation; // 国籍
        public string birthDate; // 生日（年-月-日）
        public string teamName; // 队名 
        public double winRate; // 胜率

        public PlayerInfo(string firstName, string secondName, string nation, string birthDate, string teamName, double winRate)
        {
            this.firstName = firstName;
            this.secondName = secondName;
            this.nation = nation;
            this.birthDate = birthDate;
            this.teamName = teamName;
            this.winRate = winRate;
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


        void Awake()
        {
            content = this.gameObject.transform.Find("Scroll View").Find("Viewport").Find("Content").gameObject;
            SearchInputObject = this.gameObject.transform.Find("InputField").gameObject;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        public void LoadData()
        {
            // load data from server
            string SearchInput = SearchInputObject.GetComponent<InputField>().text;
            DataSet ds=MySqlSearchManager.Instance.NameSearch(SearchInput);
        
            List<PlayerInfo> playerList = new List<PlayerInfo>();
            // ....
            for (int i = 0; i < ds.Tables[0].Rows.Count;i++)
            {
                string name = ds.Tables[0].Rows[i][1].ToString();
                string [] names = Regex.Split(name, "\\s+",RegexOptions.IgnoreCase);
                string first_name=names[0];
                string second_name="";
                if (names.Length == 2)
                {
                    second_name = names[1];
                }
                
                playerList.Add(new PlayerInfo(first_name, second_name,
                   ds.Tables[0].Rows[i][4].ToString(), ds.Tables[0].Rows[i][3].ToString(), ds.Tables[0].Rows[i][2].ToString(), 23.333));
                

            }
            
            //playerList.Add(new PlayerInfo("test", "test", "test", "test", "test", 23.333));
            //playerList.Add(new PlayerInfo("test", "test", "test", "test", "test", 23.333));
            //playerList.Add(new PlayerInfo("test", "test", "test", "test", "test", 23.333));
            //playerList.Add(new PlayerInfo("test", "test", "test", "test", "test", 23.333));
            //playerList.Add(new PlayerInfo("test", "test", "test", "test", "test", 23.333));
            //playerList.Add(new PlayerInfo("test", "test", "test", "test", "test", 23.333));
            //playerList.Add(new PlayerInfo("test", "test", "test", "test", "test", 23.333));
            //playerList.Add(new PlayerInfo("test", "test", "test", "test", "test", 23.333));
            //playerList.Add(new PlayerInfo("test", "test", "test", "test", "test", 23.333));
            //playerList.Add(new PlayerInfo("test", "test", "test", "test", "test", 23.333));
            //playerList.Add(new PlayerInfo("test", "test", "test", "test", "test", 23.333));
            //playerList.Add(new PlayerInfo("test", "test", "test", "test", "test", 23.333));
            //playerList.Add(new PlayerInfo("test", "test", "test", "test", "test", 23.333));
            //playerList.Add(new PlayerInfo("test", "test", "test", "test", "test", 23.333));
            //playerList.Add(new PlayerInfo("test", "test", "test", "test", "test", 23.333));
            //playerList.Add(new PlayerInfo("test", "test", "test", "test", "test", 23.333));
            //playerList.Add(new PlayerInfo("test", "test", "test", "test", "test", 23.333));
            //playerList.Add(new PlayerInfo("test", "test", "test", "test", "test", 23.333));



            // data list view
            for (int i = 0; i < playerList.Count; ++i)
            {
                // row organization
                GameObject go = Instantiate(RowPrefab) as GameObject;
                go.transform.parent = content.transform;


                // attribute organization
                GameObject go1 = Instantiate(AttributePrefab) as GameObject;
                go1.transform.Find("Text").gameObject.GetComponent<Text>().text = playerList[i].firstName;
                go1.transform.parent = go.transform;

                GameObject go2 = Instantiate(AttributePrefab) as GameObject;
                go2.transform.Find("Text").gameObject.GetComponent<Text>().text = playerList[i].secondName;
                go2.transform.parent = go.transform;

                GameObject go3 = Instantiate(AttributePrefab) as GameObject;
                go3.transform.Find("Text").gameObject.GetComponent<Text>().text = playerList[i].nation;
                go3.transform.parent = go.transform;

                GameObject go4 = Instantiate(AttributePrefab) as GameObject;
                go4.transform.Find("Text").gameObject.GetComponent<Text>().text = playerList[i].birthDate;
                go4.transform.parent = go.transform;

                GameObject go5 = Instantiate(AttributePrefab) as GameObject;
                go5.transform.Find("Text").gameObject.GetComponent<Text>().text = playerList[i].teamName;
                go5.transform.parent = go.transform;

                GameObject go6 = Instantiate(AttributePrefab) as GameObject;
                go6.transform.Find("Text").gameObject.GetComponent<Text>().text = playerList[i].winRate.ToString();
                go6.transform.parent = go.transform;

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