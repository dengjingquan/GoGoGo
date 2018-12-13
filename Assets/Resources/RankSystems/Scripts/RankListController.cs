using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;


public class RankListController : MonoBehaviour {
    //特殊排名颜色
    public Color RankFirstBgColor;
    public Color RankFirstMedalColor;
    public Color RankSecondBgColor;
    public Color RankSecondMedalColor;
    public Color RankThirdBgColor;
    public Color RankThirdMedalColor;
    public Text errorText;
    private int maxWaitTime = 10;            // 最大等待时间
    private bool isLoading;                  // 是否在请求数据
    private float maxTime;                      
    private string actionID;
    public GameObject loadingPic;                   // 正在读取图片

    // Use this for initialization
    void Start () {
        loadingPic.gameObject.SetActive(true);
        isLoading = false;
        // 数据请求
        Dictionary<string, object> loginDict = new Dictionary<string, object>();
        string action = "rank";
        loginDict.Add("action", "rank");
        // 动作ID 用于后续异步获取结果,组成 : 主机名 + 时间戳 + action 
        actionID = GameSocket.hostName + Time.time + action;
        loginDict.Add("actionID", actionID);
        string jsonData = JsonConvert.SerializeObject(loginDict);
        // 发送
        GameSocket.Send(jsonData);
        maxTime = Time.time + maxWaitTime;
        isLoading = true;
        
    }
	
	// Update is called once per frame
	void Update () {
		if (isLoading)
        {
            if (maxTime > Time.time)
            {
                // 已接受答复包
                if (GameSocket.responseBuffer.ContainsKey(actionID))
                {
                    // 接受成功
                    isLoading = false;
                    Dictionary<string, object> resultDict = (Dictionary<string, object>)(GameSocket.responseBuffer[actionID]);
                    // 暂时使用字符串解析获取值
                    string accountStr = (resultDict["accounts"]).ToString();
                    string scoreStr = (resultDict["scores"]).ToString();
                    // resultDict["accounts"] 后有特殊字符
                    string[] accounts = accountStr.Substring(1, accountStr.LastIndexOf('"')).Split(',');
                    string[] scores = scoreStr.Substring(1, scoreStr.Length - 2).Split(',');
                    GameObject ListItemPrefab = (GameObject)Resources.Load("RankSystems/Prefabs/RankListItem");
                    for (int i = 0; i < scores.Length; i++)
                    {
                        //实例化预制件
                        GameObject Item = Instantiate(ListItemPrefab);
                        //填充实例数据
                        Item.transform.Find("RankText").GetComponent<Text>().text = (i + 1).ToString();
                        string account = accounts[i];
                        int startIndex = account.IndexOf('"');
                        //Debug.Log(account);
                        Item.transform.Find("UserText").GetComponent<Text>().text = account.Substring(startIndex + 1,account.Length - startIndex - 2);
                 
                        Item.transform.Find("ScoreText").GetComponent<Text>().text = string.Format("{0:F}", float.Parse(scores[i]));
                        //对前三名颜色进行特殊处理
                        if (i < 3)
                        {
                            switch (i)
                            {
                                case 0:
                                    Item.transform.Find("ItemBg").GetComponent<Image>().color = RankFirstBgColor;
                                    Item.transform.Find("MedalImg").GetComponent<Image>().color = RankFirstMedalColor;
                                    break;
                                case 1:
                                    Item.transform.Find("ItemBg").GetComponent<Image>().color = RankSecondBgColor;
                                    Item.transform.Find("MedalImg").GetComponent<Image>().color = RankSecondMedalColor;
                                    break;
                                case 2:
                                    Item.transform.Find("ItemBg").GetComponent<Image>().color = RankThirdBgColor;
                                    Item.transform.Find("MedalImg").GetComponent<Image>().color = RankThirdMedalColor;
                                    break;
                            }
                        }
                        //将实例加载到场景
                        Item.transform.SetParent(this.transform);
                        Item.transform.localScale = new Vector3(1, 1, 1);
                        Item.transform.localPosition = new Vector3(Item.transform.localPosition.x, Item.transform.localPosition.y, 0);

                    }
                    //double dbdata = 0.335333; string str1 = String.Format("{0:F}", dbdata);
                    loadingPic.gameObject.SetActive(false);
                }

            }
            else { 
                errorText.text = "overtime try again later!";
                loadingPic.gameObject.SetActive(false);
                return;
            }
        

            //RankItems.Sort(new RankDataItemComparetor());
            ////加载预制件
            //GameObject ListItemPrefab = (GameObject)Resources.Load("RankSystems/Prefabs/RankListItem");
            ////将成绩前15个添加到排行榜
            //for (int i = 0; i < RankItems.Count&& i < 15; i++)
            //{
            //    //实例化预制件
            //    GameObject Item = Instantiate(ListItemPrefab);
            //    //填充实例数据
            //    Item.transform.Find("RankText").GetComponent<Text>().text = (i+1).ToString();
            //    Item.transform.Find("UserText").GetComponent<Text>().text = ((RankDataItem)(RankItems[i])).User;
            //    Item.transform.Find("ScoreText").GetComponent<Text>().text = ((RankDataItem)(RankItems[i])).Score.ToString("0");
            //    //对前三名颜色进行特殊处理
            //    if(i < 3)
            //    {
            //        switch (i)
            //        {
            //            case 0:
            //                Item.transform.Find("ItemBg").GetComponent<Image>().color = RankFirstBgColor;
            //                Item.transform.Find("MedalImg").GetComponent<Image>().color = RankFirstMedalColor;
            //                break;
            //            case 1:
            //                Item.transform.Find("ItemBg").GetComponent<Image>().color = RankSecondBgColor;
            //                Item.transform.Find("MedalImg").GetComponent<Image>().color = RankSecondMedalColor;
            //                break;
            //            case 2:
            //                Item.transform.Find("ItemBg").GetComponent<Image>().color = RankThirdBgColor;
            //                Item.transform.Find("MedalImg").GetComponent<Image>().color = RankThirdMedalColor;
            //                break;
            //        }
            //    }
            //    //将实例加载到场景
            //    Item.transform.parent = this.transform;
            //    Item.transform.localScale = new Vector3(1, 1, 1);
            //    Item.transform.localPosition=new Vector3(Item.transform.localPosition.x, Item.transform.localPosition.y,0);

            //}
        }
    }
}

// 成绩数据类
class RankDataItem
{
    public float Score;
    public string User;

    public RankDataItem(float Score,string User)
    {
        this.Score = Score;
        this.User = User;
    }
}

class RankDataItemComparetor : IComparer
{
    // 比较器 按完成游戏使用时间升序排序
    int IComparer.Compare(object x, object y)
    {
        RankDataItem NewX = (RankDataItem)x;
        RankDataItem NewY = (RankDataItem)y;
        if(NewX.Score > NewY.Score)
        {
            return -1;
        }
        if (NewX.Score < NewY.Score)
        {
            return 1;
        }
        return 0;
    }
}