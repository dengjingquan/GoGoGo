# -*- coding: utf-8  -*-
DBCTRL_MANAGER_NAME = "dbctrl"
DATABASE_NAME = "db_demo"


TABLE_GLOBAL = """
CREATE TABLE tbl_global
(
    rl_sName varchar(30) NOT NULL COMMENT '名字',
    rl_dmSaveTime datetime NOT NULL COMMENT '存档时间',
    rl_sData MEDIUMBLOB NOT NULL COMMENT '数据块',
    PRIMARY KEY (rl_sName)
)ENGINE=InnoDB default charset=utf8 comment='全局数据'
"""
# 自定义表
USER = """
CREATE TABLE `user` (
  `account` varchar(10) NOT NULL,
  `password` varchar(10) NOT NULL,
  PRIMARY KEY (`account`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
"""

SCORE = """
CREATE TABLE `score` (
  `account` varchar(10) DEFAULT NULL,
  `usetime` float DEFAULT NULL,
  KEY `account` (`account`),
  CONSTRAINT `account` FOREIGN KEY (`account`) REFERENCES `user` (`account`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
"""

TABLE_ALL = {
    "tbl_global":TABLE_GLOBAL
    # "user":USER
    # "score":SCORE
}