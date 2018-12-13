using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using System.Net;


/*********************************
 @Description   :   游戏通信器
 @Version       :   1.0 
 @Author        :   Dang
 @Date          :   2018.12.10
********************************/

public class GameSocket:MonoBehaviour{
    public static string hostName = Dns.GetHostName();                                                 // 主机名 
    public static string account;                                                                      // 游戏当前用户名
    public static string password;                                                                     // 密码
    public static Dictionary<string, object> responseBuffer = new Dictionary<string, object>();        // 消息缓冲区
    public static bool isOnline = false;
    public static TcpClient client;                                                                    // Tcp连接对象
    public static NetworkStream streamToServer;
    private static int miMaxSize = 1024;                                                              // 单次发送最大字节数
    private static byte[] msData = new byte[0];                                                       // 初始化字节数组


    //初始化TCP连接
    public static void InitTCP()
    {
        if (streamToServer == null)
        {
            client = new TcpClient("172.16.28.227", 12080);
            streamToServer = client.GetStream();
            // 开启对服务器监听
            ThreadStart recvThreadStart = new ThreadStart(Recv);
            Thread recvThread = new Thread(recvThreadStart);
            recvThread.Start();
        }
       
    }
     
    // 发送消息包(仿照服务端Demo)
    public static void Send(string sData)
    {
        //Debug.Log("send:" + sData);
        //string sData = JsonUtility.ToJson(dData);
        //string sData = "{\"action\": \"updatepos\", \"radius1\": 1, \"radius2\": 1, \"pos1\": 1, \"pos2\": 1}";
        //Debug.Log(sData);
        int iSize = sData.Length;
        int iTotalSize = iSize + 4;
        // 将int转化为32位（4字节）字节数组
        byte[] bytes = ConvertIntToByteArray(iTotalSize);
        // 将字节数组转为字符串
        string sSize = Encoding.Default.GetString(bytes);
        // 与发送内容拼接     
        string sTotalStr = sSize + sData;   
        int iCount = (iTotalSize - 1) / miMaxSize + 1;
        // 每次最多发送m_iMaxSize字节

       
        for (int i = 0; i < iCount; i++)
        {
            int iBegin = miMaxSize * i;
            int iEnd = iBegin + miMaxSize;
            // 截取字符串
            String sSubSend;
            if (iEnd <= sTotalStr.Length)
            {
                sSubSend = sTotalStr.Substring(iBegin, iEnd - iBegin);
            }
            else
            {
                sSubSend = sTotalStr.Substring(iBegin);
            }
            // Debug.Log("send:" + sSubSend);
            // 转化为 GBK 码字节序列
            byte[] buffer = Encoding.GetEncoding("gbk").GetBytes(sSubSend);
            
            streamToServer.Write(buffer, 0, buffer.Length);
            streamToServer.Flush();
            //Debug.Log("send finish");
        }  
    }

    // 接受消息包(仿照服务端Demo)
    private static void Recv(){
        while (true)
        {
            //Debug.Log("restart");
            byte[] sData = new byte[miMaxSize * miMaxSize];
            // 读取字节流
            client.GetStream().Read(sData, 0, sData.Length);
            //string sData = Encoding.GetEncoding("gbk").GetString(result);
            //Debug.Log("sData:" + sData.Length);
            msData = mergeArray<byte>(msData,sData);
            //Debug.Log("mData:" + msData.Length);
            while (true)
            {   
                int iLen = msData.Length;
                // 基本格式不齐 剩余不足4个字节
                if (iLen < 4)
                {
                    //Debug.Log("return1");
                    break;
                }
                byte[] sSize = cutArray<byte>(msData, 0, 4);
                // 将4字节数组转化为int32
                
                int iSize = BitConverter.ToInt32(sSize,0);
                //Debug.Log("iSize" + iSize.ToString());
                // 未发完 继续等待
                if (iSize > iLen)
                {
                    
                    //Debug.Log("return2");
                    break;

                }
                // 空包 后续为空字符 重置m_sData
                if (iSize == 0)
                {
                    msData = new byte[0];
                    break;
                }
                // 获取一个数据包
                byte[] sPack = cutArray<byte>(msData,4,iSize);
                //Debug.Log("spack length" + sPack.Length.ToString());
                msData = cutArray<byte>(msData, iSize, msData.Length);
                string dPackData = Encoding.GetEncoding("gbk").GetString(sPack);
                //Debug.Log("dpacklage" + dPackData);
                Dictionary<String, object> dict = JsonConvert.DeserializeObject<Dictionary<String, object>>(dPackData);
                //Debug.Log(dict.ContainsKey("action"));
                //Debug.Log("recv a msg");
                //Debug.Log(dict["actionID"]);
                //Debug.Log((string)dict["actionID"]);
                responseBuffer.Add((string)dict["actionID"], dict);
            }
        }
        
    }
    // 外部获取消息
    public static void GetMsg()
    {

    }

    // 将整数转为长度为4的字节数组(32位)
    public static byte[] ConvertIntToByteArray(Int32 num)
    {
        byte[] bytes = new byte[4];
        bytes[0] = (byte)(num & 0xFF);
        bytes[1] = (byte)((num & 0xFF00) >> 8);
        bytes[2] = (byte)((num & 0xFF0000) >> 16);
        bytes[3] = (byte)((num >> 24) & 0xFF);
        return bytes;
    }

    // 数组切片
    private static T[] cutArray<T>(T[] array,int start,int end)
    {
        T[] newT = new T[end - start];
        for (int i = start,j=0;i < end; i++,j++)
        {
            newT[j] = array[i];
        }
        return newT;
    }
    // 数组合并
    private static T[] mergeArray<T>(T[] array1, T[] array2)
    {
        int length1 = array1.Length;
        int length2 = array2.Length;
        T[] newT = new T[length1 + length2];
        int i = 0;
        while (i < length1)
        {
            newT[i] = array1[i];
            i++;
        }
        int j = 0;
        while(j < length2)
        {
            newT[i++] = array2[j++];
        }
        return newT;
    }

}
