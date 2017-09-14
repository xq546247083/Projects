using System;
using System.Data;
using System.IO;


/// <summary>
/// HdcCache ��ժҪ˵����
/// </summary>
public class HdcCache
{
    /// <summary>
    /// �洢���ݵĶ���
    /// </summary>
    /// <author>xumr</author>
    /// <log date="2005-05-15">����</log>
    private object obj = null;

    /// <summary>
    /// �ļ�·����
    /// </summary>
    /// <author>xumr</author>
    /// <log date="2005-05-15">����</log>
    private string filePath = null;

    /// <summary>
    /// �ļ�������޸����ڡ�
    /// </summary>
    /// <author>xumr</author>
    /// <log date="2005-05-15">����</log>
    private DateTime fileUpdateTime;

    /// <summary>
    /// ˢ��Ƶ�ʡ�
    /// </summary>
    /// <author>xumr</author>
    /// <log date="2005-05-15">����</log>
    private int second = 0;

    /// <summary>
    /// ���ݵĶ�ȡ���ڡ�
    /// </summary>
    /// <author>xumr</author>
    /// <log date="2005-05-15">����</log>
    private DateTime setTime;

    /// <summary>
    /// Ĭ�Ϲ��캯����
    /// </summary>
    /// <author>xumr</author>
    /// <log date="2005-05-15">����</log>
    public HdcCache()
    {
        //do nothing
    }

    /// <summary>
    /// ȡ���ļ����ݡ�
    /// </summary>
    /// <param name="filePath">�ļ�·��</param>
    /// <param name="second">���ݸ���Ƶ��</param>
    public void Init(string filePath, int second)
    {
        this.obj = null;

        this.filePath = filePath;
        if (filePath != null)
        {
            this.fileUpdateTime = File.GetLastWriteTime(filePath);
            this.second = second;
            this.setTime = DateTime.Now;
        }
    }

    public void Init(string filePath)
    {
        this.obj = null;

        this.filePath = filePath;
        if (filePath != null)
        {
            this.fileUpdateTime = File.GetLastWriteTime(filePath);
        }
    }

    public void Init(int second)
    {
        this.obj = null;

        this.second = second;
        if (second <= 0)
        {
            this.setTime = DateTime.Now;
        }
    }

    public object Get()
    {
        if (this.obj == null)
        {
            return null;
        }

        if (this.filePath != null)
        {
            DateTime updateTime = File.GetLastWriteTime(this.filePath);
            if (!updateTime.Equals(this.fileUpdateTime))
            {
                this.obj = null;
                return null;
            }
        }

        if (this.second > 0)
        {
            TimeSpan timeSpan = DateTime.Now - this.setTime;
            if (timeSpan.TotalSeconds > this.second)
            {
                this.obj = null;
                return null;
            }
        }

        return this.obj;
    }

    public void Set(object obj)
    {
        this.obj = obj;

        if (this.second > 0)
        {
            this.setTime = DateTime.Now;
        }

        if (this.filePath != null)
        {
            this.fileUpdateTime = File.GetLastWriteTime(filePath);
        }
    }
}
