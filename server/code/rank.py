# -*- coding: utf-8 -*-
"""
-------------------------------------------------
   File Name：     rank
   Description :   查看排行榜action
   Author :        Dang
   date：          2018/12/11
-------------------------------------------------
   Change Activity:
                   2018/12/11:
-------------------------------------------------
"""
import dbctrl.saveobject
import timecontrol
import pubcore
import pubdefines
import pubglobalmanager
from dbctrl.manager import CDBManager




class RankActioner(CDBManager):

    def Init(self):
        pass

    def CalPos(self, oClient, dData):
        dReturn = {
            "actionID": dData["actionID"]
        }
        sql = "SELECT * FROM score ORDER BY usetime LIMIT 15;"
        result = self.Query(sql)
        accounts = []
        scores = []
        for account, score in result:
            accounts.append(account)
            scores.append(score)
        dReturn["accounts"] = accounts
        dReturn["scores"] = scores
        oClient.Send(dReturn)


def Init():
    if pubglobalmanager.GetGlobalManager("rank"):
        return
    oManger = RankActioner()
    pubglobalmanager.SetGlobalManager("rank", oManger)
    oManger.Init()

def Record():
    pubglobalmanager.CallManagerFunc("rank", "NewItem")

def OnCommand(oClient, dData):
    try:
        pubglobalmanager.CallManagerFunc("rank", "CalPos", oClient, dData)
    except Exception, arg:
        pubdefines.FormatPrint(arg)




