#!/usr/bin/python
#coding=utf-8
"""
description: modify RSRP counter 
"""
# 提供给需求时，需要修改的地方
"""
1、修改文件的获取路径
2、修改压缩后文件的存放路径
3、修改文件入口
"""

import csv
import os
import re
import shutil
import time
import gzip
import zipfile

OSS_MR_DIR = "/opt/MR/data/northbound/ready/"
# OSS_MR_DIR = r"E:\data\ailixin\mrTool"
testPath = r"D:\myCode\pyCode\MRS-RSRP\test"

class Main(object): 
    def __init__(self):
        self.bpPath = os.path.join(os.getcwd(), "temp")
        self.rsrpFlag = False
        self.parseCsv()

    def getCurrTime(self):
        def get_rop_info(time_list):
            y = time_list[0]
            m = time_list[1]
            d = time_list[2]
            h = time_list[3]
            min = int(time_list[4])
            start_rop = ""
            end_rop = ""
            if min == 17:
                print "*****",time.asctime()
                start_rop = "%02d45" % (int(h) - 1)
                end_rop = "%02d00" % (int(h))
            elif min == 32:
                print "*****", time.asctime()
                start_rop = "%02d00" % (int(h))
                end_rop = "%02d15" % (int(h))
            elif min == 47:
                print "*****", time.asctime()
                start_rop = "%02d15" % (int(h))
                end_rop = "%02d30" % (int(h))
            elif min == 02:
                print "*****", time.asctime()
                start_rop = "%02d30" % (int(h) - 1)
                end_rop = "%02d45" % (int(h) - 1)
            else:
                # print "incorrect recording time: %s, ignored" % time.asctime()
                return False
            return y, m, d, start_rop, end_rop

        time_list = time.strftime('%Y %m %d %H %M %S', time.localtime()).split(" ")
        return get_rop_info(time_list)

    def uzipFile(self, xmlName):
        if not os.path.exists(self.bpPath):
            os.mkdir(self.bpPath)
        xml = os.path.basename(xmlName)
        xmlTemp = os.path.join(self.bpPath, xml)
        shutil.copyfile(xmlName, xmlTemp)
        f = zipfile.ZipFile(xmlTemp, 'r')
        for file in f.namelist():
            f.extract(file, self.bpPath)
        # os.system("gzip -d %s" % xmlTemp)
        # os.remove(xmlTemp)
        return os.path.join(self.bpPath, xml.replace(".zip",""))

    def parseCsv(self):
        # 获取需要修改的指标的对应的值
        self.counterDir = {}
        csv_reader = csv.reader(open('counter.csv'))
        for row in csv_reader:
            if row[0] == "counter":
                continue
            counter, index, factor = row
            # 以指标分类存入字典中
            self.counterDir.setdefault(counter,[]).append((index,factor))

    def zipFile(self,xmlFile,xml):
        zipFile = zipfile.ZipFile(xml, 'w')
        zipFile.write(xmlFile, os.path.basename(xmlFile), zipfile.ZIP_DEFLATED)
        zipFile.close()

    def parseXml(self, xmlFile, xml):
        xmlName = xmlFile
        xmlNameNew = os.path.join(self.bpPath, os.path.basename(xmlFile) + ".new")
        xmlFileNew = open(xmlNameNew, "w")
        xmlFile = open(xmlFile, "r")

        rsrpMeasure = ""
        counter = ""
        for xmlLine in xmlFile.readlines():
            for counterTemp in self.counterDir:
                rsrpMeasureTemp = '<measurement mrName="MR.%s">' % counterTemp
                if rsrpMeasureTemp in xmlLine:
                    counter = counterTemp
                    rsrpMeasure = rsrpMeasureTemp
                    break
            if self.rsrpFlag == False and rsrpMeasure!="":
                self.rsrpFlag = True
                xmlFileNew.write(xmlLine)
            elif self.rsrpFlag and "<v>" in xmlLine:
                rsrpV = re.compile("(\s+)<v>(.+)</v>")
                rsrpBlack, rsrpValue = re.findall(rsrpV,xmlLine)[0]
                if rsrpValue:
                    rsrpValueLst = rsrpValue.split(" ")
                    for (index, factor) in self.counterDir[counter]:
                        rsrpValueLst[int(index)] = str(int(eval(rsrpValueLst[int(index)])*eval(factor)))
                    xmlFileNew.write(rsrpBlack+"<v>"+" ".join(rsrpValueLst)+"</v>\n")
            elif self.rsrpFlag and "</measurement>" in xmlLine:
                self.rsrpFlag = False
                rsrpMeasure = ""
                counter = ""
                xmlFileNew.write(xmlLine)
            else:
                xmlFileNew.write(xmlLine)
        xmlFileNew.close()
        xmlFile.close()
        os.remove(xmlName)
        os.rename(xmlNameNew, xmlName)
        self.zipFile(xmlName,xml)

    def getModFile(self,*args):
        (cYear, cMon, cDay, start_rop, end_rop) = args[0]
        timeFileName = os.path.join(OSS_MR_DIR, cYear+cMon+cDay)
        fpNameTemplate = "FD-LTE_MRS_ERICSSON_OMC1_%s_%s%s%s%s00.xml.zip"

        for eachNodeb in os.listdir(timeFileName):
            nodebFileName = os.path.join(timeFileName, eachNodeb)
            xmlName = os.path.join(nodebFileName, fpNameTemplate % (eachNodeb,cYear,cMon,cDay,start_rop))
            if fpNameTemplate % (eachNodeb,cYear,cMon,cDay,start_rop) not in os.listdir(nodebFileName):
                continue
            uzipFile = self.uzipFile(xmlName)
            self.parseXml(uzipFile,xmlName)
        if os.path.exists(self.bpPath):
            shutil.rmtree(self.bpPath)

    def test(self):
        fileDir = r"E:\data\ailixin\mrTool\MRS"
        fpNameTemplate = "FD-LTE_MRS_ERICSSON_OMC1_%s_%s%s%s%s00"
        for each in os.listdir(fileDir):
            if each.endswith(".zip"):
                uzipFile = self.uzipFile(os.path.join(fileDir,each))
                self.parseXml(uzipFile)
        shutil.rmtree(self.bpPath)

    def startProcess(self):
        while 1:
            # func
            # 获取当前时间，固定的时间点修改固定的文件
            if self.getCurrTime() == False:
                continue
            # cYear, cMon, cDay, start_rop, end_rop = self.getCurrTime()
            # 扫描得到需要修改的文件
            self.getModFile(self.getCurrTime())
            time.sleep(60)
            # 要修改的文件先进行解压
            # self.uzipFile()
            # # 解析需要修改的文件，并对指标RSRP的值进行修改
            # self.parseXml()
            # # 修改后的文件压缩后，放回到原始的位置
            # self.zipFile()

if __name__ == "__main__":
    Main().startProcess()
    # RsrpUpdator().test()
