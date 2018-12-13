# -*- coding: utf-8 -*-
"""
-------------------------------------------------
   File Name：     recordscore
   Description :   记录成绩action
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

class RecordScoreActioner(CDBManager):

    def Init(self):
        pass

    def CalPos(self, oClient, dData):

        pubdefines.FormatPrint(dData)
        sql = "SELECT EXISTS(SELECT 1 FROM `user` WHERE account='%s' AND password='%s');" % (dData["account"],dData["password"])
        isExitResult = self.Query(sql)
        # 如果账号密码不匹配 不作处理
        # 匹配 成绩入库
        if isExitResult[0][0] == 1:
            sql = "INSERT INTO `score` VALUES ('%s',%s);" % (dData["account"], dData["score"])
            self.ExecSql(sql)
        else:
            pass



def Init():
    if pubglobalmanager.GetGlobalManager("recordscore"):
        return
    oManger = RecordScoreActioner()
    pubglobalmanager.SetGlobalManager("recordscore", oManger)
    oManger.Init()

def Record():
    pubglobalmanager.CallManagerFunc("recordscore", "NewItem")

def OnCommand(oClient, dData):
    try:
        pubglobalmanager.CallManagerFunc("recordscore", "CalPos", oClient, dData)
    except Exception, arg:
        pubdefines.FormatPrint(arg)



