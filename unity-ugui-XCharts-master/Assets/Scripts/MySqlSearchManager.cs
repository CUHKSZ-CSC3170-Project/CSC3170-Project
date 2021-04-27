using UnityEngine;
using System.Data;

public class MySqlSearchManager : MonoBehaviour
{
    private static MySqlSearchManager instance;
    public static MySqlSearchManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MySqlSearchManager();
            }
            return instance;
        }
    }

    //public InputField playerNameInput;

    //提示用户登录信息
    //public Text loginMessage;

    //IP地址
    public string host;
    //端口号
    public string port;
    //用户名
    public string userName;
    //密码
    public string password;
    //数据库名称
    public string databaseName;
    //封装好的数据库类
    MySqlAccess mysql;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        mysql = new MySqlAccess(host, port, userName, password, databaseName);
    }


    /// <summary>
    /// 按下登录按钮
    /// 返回值的类型是DataSet（类似一张表格）
    // DataSet name_ds=mysql.Select(sql);
    // for (int i = 0; i < name_ds.Tables[0].Columns.Count; i++)
    //     {
    //            Debug.Log(name_ds.Tables[0].Rows[0][i]);
    //     }
    /// UI 填充的时候，用for循环就能拿到每个表的每个信息
    /// </summary>
    public DataSet NameSearch(string input)
    {
        mysql.OpenSql();
        string sql = "SELECT * FROM player Left Join player_winrate on player.ID=player_winrate.playerID WHERE NAME LIKE '%" + input+"%' ORDER BY ID;";
        Debug.Log("sql:" + sql);
        DataSet name_ds=mysql.Select(sql);
        if (name_ds != null)
        {
            Debug.Log("结果不为空");
            for (int i = 0; i < name_ds.Tables[0].Columns.Count; i++)
            {
                Debug.Log(name_ds.Tables[0].Rows[0][i]);
            }
        }
        else
        {
            Debug.Log("结果为空");
        }
        return name_ds;
    }

    public DataSet PlayerIDSearch(string input)
    {
        mysql.OpenSql();
        string sql = "SELECT * FROM player Left Join player_winrate on player.ID=player_winrate.playerID WHERE ID=" + input + " ORDER BY ID;";
        Debug.Log("sql:" + sql);
        DataSet playerID_ds = mysql.Select(sql);
        if (playerID_ds != null)
        {
            Debug.Log("结果不为空");
            for (int i = 0; i < playerID_ds.Tables[0].Columns.Count; i++)
            {
                Debug.Log(playerID_ds.Tables[0].Rows[0][i]);
            }
        }
        else
        {
            Debug.Log("结果为空");
        }
        return playerID_ds;
    }

    public DataSet TeamSearch(string input)
    {
        mysql.OpenSql();
        string sql = "SELECT * FROM team left join host_winrate on team.name=host_winrate.host_team left join away_winrate on team.name=away_winrate.away_team WHERE name LIKE '%" + input + "%' ORDER BY found_year;";
        Debug.Log("sql:" + sql);
        DataSet team_ds = mysql.Select(sql);
        if (team_ds != null)
        {
            Debug.Log("结果不为空");
            for (int i = 0; i < team_ds.Tables[0].Columns.Count; i++)
            {
                Debug.Log(team_ds.Tables[0].Rows[0][i]);
            }
        }
        else
        {
            Debug.Log("结果为空");
        }
        return team_ds;
    }

    public DataSet MatchIDSearch(string input)
    {
        mysql.OpenSql();
        string sql = "SELECT * FROM soccer_game.match WHERE matchID=" + input + " ORDER BY matchID;";
        Debug.Log("sql:" + sql);
        DataSet match_ds = mysql.Select(sql);
        if (match_ds != null)
        {
            Debug.Log("结果不为空");
            for (int i = 0; i < match_ds.Tables[0].Columns.Count; i++)
            {
                Debug.Log(match_ds.Tables[0].Rows[0][i]);
            }
        }
        else
        {
            Debug.Log("结果为空");
        }
        return match_ds;
    }

    public DataSet MatchDateSearch(string input)
    {
        mysql.OpenSql();
        string sql = "SELECT * FROM soccer_game.match WHERE date LIKE '%" + input + "%' ORDER BY matchID;";
        Debug.Log("sql:" + sql);
        DataSet match_ds = mysql.Select(sql);
        if (match_ds != null)
        {
            Debug.Log("结果不为空");
            for (int i = 0; i < match_ds.Tables[0].Columns.Count; i++)
            {
                Debug.Log(match_ds.Tables[0].Rows[0][i]);
            }
        }
        else
        {
            Debug.Log("结果为空");
        }
        return match_ds;
    }

    public DataSet MatchSearch_TeamPosition(string matchID,string teamName)
    {
        mysql.OpenSql();
        string sql = "select soccer_game.match.matchID,player.name,player.team_name,player.position from participate left join soccer_game.match on participate.matchID=soccer_game.match.matchID left join player on player.ID=participate.playerID where soccer_game.match.matchID=" + matchID+" and player.team_name='"+teamName +"';";
        Debug.Log("sql:" + sql);
        DataSet match_ds = mysql.Select(sql);
        if (match_ds != null)
        {
            Debug.Log("结果不为空");
            for (int i = 0; i < match_ds.Tables[0].Columns.Count; i++)
            {
                Debug.Log(match_ds.Tables[0].Rows[0][i]);
            }
        }
        else
        {
            Debug.Log("结果为空");
        }
        return match_ds;
    }
}
