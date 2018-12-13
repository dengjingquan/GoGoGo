# -*- coding: utf-8 -*-
"""
-------------------------------------------------
   File Name：     login
   Description :   登陆验证action
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
import math

class LoginActioner(CDBManager):

    def Init(self):
        pass

    def CalPos(self, oClient, dData):
        # 处理登陆消息
        dReturn = {
            "actionID": dData["actionID"],
            "action": "login",
            "result": "fail",
        }
        sql = "SELECT EXISTS(SELECT 1 FROM `user` WHERE account='%s');"% dData["account"]
        isExitResult = self.Query(sql)
        # 如果账号不存在 创建新账号
        if isExitResult[0][0] == 0:
            sql = "INSERT INTO `user` VALUES ('%s','%s');"%(dData["account"],dData["password"])
            self.ExecSql(sql)
            dReturn["result"] = "success"
        # 如果存在 匹配密码
        else:
            sql = "SELECT password FROM `user` WHERE account='%s';"% dData["account"]
            passwordResult = self.Query(sql)
            if passwordResult[0][0] == dData["password"]:
               dReturn["result"] = "success"
        oClient.Send(dReturn)

def Init():
    if pubglobalmanager.GetGlobalManager("login"):
        return
    oManger = LoginActioner()
    pubglobalmanager.SetGlobalManager("login", oManger)
    oManger.Init()

def Record():
    pubglobalmanager.CallManagerFunc("login", "NewItem")

def OnCommand(oClient, dData):
    try:
        pubglobalmanager.CallManagerFunc("login", "CalPos", oClient, dData)
    except Exception, arg:
        pubdefines.FormatPrint(arg)



