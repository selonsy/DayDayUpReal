using System;
using System.IO;
using System.Web;
using System.Threading;
using System.Xml;
using System.Data;

namespace Devin
{
    #region 目录

    //1、文件上传与下载
    //2、JavaScript客户端脚本输出
    //3、XML
    //4、JSON
    //5、Date
    //6、Excel
    //7、字符串

    #endregion

    #region 内容

    #region XML-N

    /// <summary>
    /// Xml的操作公共类
    /// </summary>    
    //public class XmlHelper
    //{
    //    #region 字段定义
    //    /// <summary>
    //    /// XML文件的物理路径
    //    /// </summary>
    //    private string _filePath = string.Empty;
    //    /// <summary>
    //    /// Xml文档
    //    /// </summary>
    //    private XmlDocument _xml;
    //    /// <summary>
    //    /// XML的根节点
    //    /// </summary>
    //    private XmlElement _element;
    //    #endregion

    //    #region 构造方法
    //    /// <summary>
    //    /// 实例化XmlHelper对象
    //    /// </summary>
    //    /// <param name="xmlFilePath">Xml文件的相对路径</param>
    //    public XmlHelper(string xmlFilePath)
    //    {
    //        //获取XML文件的绝对路径
    //        _filePath=HttpContext.Current.Server.MapPath(xmlFilePath);
    //    }
    //    #endregion

    //    #region 创建XML的根节点
    //    /// <summary>
    //    /// 创建XML的根节点
    //    /// </summary>
    //    private void CreateXMLElement()
    //    {

    //        //创建一个XML对象
    //        _xml = new XmlDocument();

    //        if (File.Exists(_filePath))
    //        {
    //            //加载XML文件
    //            _xml.Load(this._filePath);
    //        }

    //        //为XML的根节点赋值
    //        _element = _xml.DocumentElement;
    //    }
    //    #endregion

    //    #region 获取指定XPath表达式的节点对象
    //    /// <summary>
    //    /// 获取指定XPath表达式的节点对象
    //    /// </summary>        
    //    /// <param name="xPath">XPath表达式
    //    /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
    //    /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
    //    /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
    //    /// </param>
    //    public XmlNode GetNode(string xPath)
    //    {
    //        //创建XML的根节点
    //        CreateXMLElement();

    //        //返回XPath节点
    //        return _element.SelectSingleNode(xPath);
    //    }
    //    #endregion

    //    #region 获取指定XPath表达式节点的值
    //    /// <summary>
    //    /// 获取指定XPath表达式节点的值
    //    /// </summary>
    //    /// <param name="xPath">XPath表达式
    //    /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
    //    /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
    //    /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
    //    /// </param>
    //    public string GetValue(string xPath)
    //    {
    //        //创建XML的根节点
    //        CreateXMLElement();

    //        //返回XPath节点的值
    //        return _element.SelectSingleNode(xPath).InnerText;
    //    }
    //    #endregion

    //    #region 获取指定XPath表达式节点的属性值
    //    /// <summary>
    //    /// 获取指定XPath表达式节点的属性值
    //    /// </summary>
    //    /// <param name="xPath">XPath表达式
    //    /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
    //    /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
    //    /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
    //    /// </param>
    //    /// <param name="attributeName">属性名</param>
    //    public string GetAttributeValue(string xPath, string attributeName)
    //    {
    //        //创建XML的根节点
    //        CreateXMLElement();

    //        //返回XPath节点的属性值
    //        return _element.SelectSingleNode(xPath).Attributes[attributeName].Value;
    //    }
    //    #endregion

    //    #region 新增节点
    //    /// <summary>
    //    /// 功能：新增节点。
    //    /// 使用条件：将任意节点插入到当前Xml文件中。
    //    /// </summary>        
    //    /// <param name="xmlNode">要插入的Xml节点</param>
    //    public void AppendNode(XmlNode xmlNode)
    //    {
    //        //创建XML的根节点
    //        CreateXMLElement();

    //        //导入节点
    //        XmlNode node = _xml.ImportNode(xmlNode, true);

    //        //将节点插入到根节点下
    //        _element.AppendChild(node);
    //    }

    //    /// <summary>
    //    /// 功能：新增节点。
    //    /// 使用条件：将DataSet中的第一条记录插入Xml文件中。
    //    /// </summary>        
    //    /// <param name="ds">DataSet的实例，该DataSet中应该只有一条记录</param>
    //    public void AppendNode(DataSet ds)
    //    {
    //        //创建XmlDataDocument对象
    //        XmlDataDocument xmlDataDocument = new XmlDataDocument(ds);

    //        //导入节点
    //        XmlNode node = xmlDataDocument.DocumentElement.FirstChild;

    //        //将节点插入到根节点下
    //        AppendNode(node);
    //    }
    //    #endregion

    //    #region 删除节点
    //    /// <summary>
    //    /// 删除指定XPath表达式的节点
    //    /// </summary>        
    //    /// <param name="xPath">XPath表达式,
    //    /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
    //    /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
    //    /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
    //    /// </param>
    //    public void RemoveNode(string xPath)
    //    {
    //        //创建XML的根节点
    //        CreateXMLElement();

    //        //获取要删除的节点
    //        XmlNode node = _xml.SelectSingleNode(xPath);

    //        //删除节点
    //        _element.RemoveChild(node);
    //    }
    //    #endregion //删除节点

    //    #region 保存XML文件
    //    /// <summary>
    //    /// 保存XML文件
    //    /// </summary>        
    //    public void Save()
    //    {
    //        //创建XML的根节点
    //        CreateXMLElement();

    //        //保存XML文件
    //        _xml.Save(this._filePath);
    //    }
    //    #endregion //保存XML文件

    //    #region 静态方法

    //    #region 创建根节点对象
    //    /// <summary>
    //    /// 创建根节点对象
    //    /// </summary>
    //    /// <param name="xmlFilePath">Xml文件的相对路径</param>        
    //    private static XmlElement CreateRootElement(string xmlFilePath)
    //    {
    //        //定义变量，表示XML文件的绝对路径
    //        string filePath = "";

    //        //获取XML文件的绝对路径
    //        filePath = HttpContext.Current.Server.MapPath(xmlFilePath);

    //        //创建XmlDocument对象
    //        XmlDocument xmlDocument = new XmlDocument();
    //        //加载XML文件
    //        xmlDocument.Load(filePath);

    //        //返回根节点
    //        return xmlDocument.DocumentElement;
    //    }
    //    #endregion

    //    #region 获取指定XPath表达式节点的值
    //    /// <summary>
    //    /// 获取指定XPath表达式节点的值
    //    /// </summary>
    //    /// <param name="xmlFilePath">Xml文件的相对路径</param>
    //    /// <param name="xPath">XPath表达式,
    //    /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
    //    /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
    //    /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
    //    /// </param>
    //    public static string GetValue(string xmlFilePath, string xPath)
    //    {
    //        //创建根对象
    //        XmlElement rootElement = CreateRootElement(xmlFilePath);

    //        //返回XPath节点的值
    //        return rootElement.SelectSingleNode(xPath).InnerText;
    //    }
    //    #endregion

    //    #region 获取指定XPath表达式节点的属性值
    //    /// <summary>
    //    /// 获取指定XPath表达式节点的属性值
    //    /// </summary>
    //    /// <param name="xmlFilePath">Xml文件的相对路径</param>
    //    /// <param name="xPath">XPath表达式,
    //    /// 范例1: @"Skill/First/SkillItem", 等效于 @"//Skill/First/SkillItem"
    //    /// 范例2: @"Table[USERNAME='a']" , []表示筛选,USERNAME是Table下的一个子节点.
    //    /// 范例3: @"ApplyPost/Item[@itemName='岗位编号']",@itemName是Item节点的属性.
    //    /// </param>
    //    /// <param name="attributeName">属性名</param>
    //    public static string GetAttributeValue(string xmlFilePath, string xPath, string attributeName)
    //    {
    //        //创建根对象
    //        XmlElement rootElement = CreateRootElement(xmlFilePath);

    //        //返回XPath节点的属性值
    //        return rootElement.SelectSingleNode(xPath).Attributes[attributeName].Value;
    //    }
    //    #endregion

    //    #endregion

    //}

    //public class XMLProcess
    //{
    //    #region 构造函数
    //    public XMLProcess()
    //    { }

    //    public XMLProcess(string strPath)
    //    {
    //        this._XMLPath = strPath;
    //    }
    //    #endregion

    //    #region 公有属性
    //    private string _XMLPath;
    //    public string XMLPath
    //    {
    //        get { return this._XMLPath; }
    //    }
    //    #endregion

    //    #region 私有方法
    //    /// <summary>
    //    /// 导入XML文件
    //    /// </summary>
    //    /// <param name="XMLPath">XML文件路径</param>
    //    private XmlDocument XMLLoad()
    //    {
    //        string XMLFile = XMLPath;
    //        XmlDocument xmldoc = new XmlDocument();
    //        try
    //        {
    //            string filename = AppDomain.CurrentDomain.BaseDirectory.ToString() + XMLFile;
    //            if (File.Exists(filename)) xmldoc.Load(filename);
    //        }
    //        catch (Exception e)
    //        { }
    //        return xmldoc;
    //    }

    //    /// <summary>
    //    /// 导入XML文件
    //    /// </summary>
    //    /// <param name="XMLPath">XML文件路径</param>
    //    private static XmlDocument XMLLoad(string strPath)
    //    {
    //        XmlDocument xmldoc = new XmlDocument();
    //        try
    //        {
    //            string filename = AppDomain.CurrentDomain.BaseDirectory.ToString() + strPath;
    //            if (File.Exists(filename)) xmldoc.Load(filename);
    //        }
    //        catch (Exception e)
    //        { }
    //        return xmldoc;
    //    }

    //    /// <summary>
    //    /// 返回完整路径
    //    /// </summary>
    //    /// <param name="strPath">Xml的路径</param>
    //    private static string GetXmlFullPath(string strPath)
    //    {
    //        if (strPath.IndexOf(":") > 0)
    //        {
    //            return strPath;
    //        }
    //        else
    //        {
    //            return System.Web.HttpContext.Current.Server.MapPath(strPath);
    //        }
    //    }
    //    #endregion

    //    #region 读取数据
    //    /// <summary>
    //    /// 读取指定节点的数据
    //    /// </summary>
    //    /// <param name="node">节点</param>
    //    /// 使用示列:
    //    /// XMLProsess.Read("/Node", "")
    //    /// XMLProsess.Read("/Node/Element[@Attribute='Name']")
    //    public string Read(string node)
    //    {
    //        string value = "";
    //        try
    //        {
    //            XmlDocument doc = XMLLoad();
    //            XmlNode xn = doc.SelectSingleNode(node);
    //            value = xn.InnerText;
    //        }
    //        catch { }
    //        return value;
    //    }

    //    /// <summary>
    //    /// 读取指定路径和节点的串联值
    //    /// </summary>
    //    /// <param name="path">路径</param>
    //    /// <param name="node">节点</param>
    //    /// <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
    //    /// 使用示列:
    //    /// XMLProsess.Read(path, "/Node", "")
    //    /// XMLProsess.Read(path, "/Node/Element[@Attribute='Name']")
    //    public static string Read(string path, string node)
    //    {
    //        string value = "";
    //        try
    //        {
    //            XmlDocument doc = XMLLoad(path);
    //            XmlNode xn = doc.SelectSingleNode(node);
    //            value = xn.InnerText;
    //        }
    //        catch { }
    //        return value;
    //    }

    //    /// <summary>
    //    /// 读取指定路径和节点的属性值
    //    /// </summary>
    //    /// <param name="path">路径</param>
    //    /// <param name="node">节点</param>
    //    /// <param name="attribute">属性名，非空时返回该属性值，否则返回串联值</param>
    //    /// 使用示列:
    //    /// XMLProsess.Read(path, "/Node", "")
    //    /// XMLProsess.Read(path, "/Node/Element[@Attribute='Name']", "Attribute")
    //    public static string Read(string path, string node, string attribute)
    //    {
    //        string value = "";
    //        try
    //        {
    //            XmlDocument doc = XMLLoad(path);
    //            XmlNode xn = doc.SelectSingleNode(node);
    //            value = (attribute.Equals("") ? xn.InnerText : xn.Attributes[attribute].Value);
    //        }
    //        catch { }
    //        return value;
    //    }

    //    /// <summary>
    //    /// 获取某一节点的所有孩子节点的值
    //    /// </summary>
    //    /// <param name="node">要查询的节点</param>
    //    public string[] ReadAllChildallValue(string node)
    //    {
    //        int i = 0;
    //        string[] str = { };
    //        XmlDocument doc = XMLLoad();
    //        XmlNode xn = doc.SelectSingleNode(node);
    //        XmlNodeList nodelist = xn.ChildNodes;  //得到该节点的子节点
    //        if (nodelist.Count > 0)
    //        {
    //            str = new string[nodelist.Count];
    //            foreach (XmlElement el in nodelist)//读元素值
    //            {
    //                str[i] = el.Value;
    //                i++;
    //            }
    //        }
    //        return str;
    //    }

    //    /// <summary>
    //    /// 获取某一节点的所有孩子节点的值
    //    /// </summary>
    //    /// <param name="node">要查询的节点</param>
    //    public XmlNodeList ReadAllChild(string node)
    //    {
    //        XmlDocument doc = XMLLoad();
    //        XmlNode xn = doc.SelectSingleNode(node);
    //        XmlNodeList nodelist = xn.ChildNodes;  //得到该节点的子节点
    //        return nodelist;
    //    }

    //    /// <summary> 
    //    /// 读取XML返回经排序或筛选后的DataView
    //    /// </summary>
    //    /// <param name="strWhere">筛选条件，如:"name='kgdiwss'"</param>
    //    /// <param name="strSort"> 排序条件，如:"Id desc"</param>
    //    public DataView GetDataViewByXml(string strWhere, string strSort)
    //    {
    //        try
    //        {
    //            string XMLFile = this.XMLPath;
    //            string filename = AppDomain.CurrentDomain.BaseDirectory.ToString() + XMLFile;
    //            DataSet ds = new DataSet();
    //            ds.ReadXml(filename);
    //            DataView dv = new DataView(ds.Tables[0]); //创建DataView来完成排序或筛选操作	
    //            if (strSort != null)
    //            {
    //                dv.Sort = strSort; //对DataView中的记录进行排序
    //            }
    //            if (strWhere != null)
    //            {
    //                dv.RowFilter = strWhere; //对DataView中的记录进行筛选，找到我们想要的记录
    //            }
    //            return dv;
    //        }
    //        catch (Exception)
    //        {
    //            return null;
    //        }
    //    }

    //    /// <summary>
    //    /// 读取XML返回DataSet
    //    /// </summary>
    //    /// <param name="strXmlPath">XML文件相对路径</param>
    //    public DataSet GetDataSetByXml(string strXmlPath)
    //    {
    //        try
    //        {
    //            DataSet ds = new DataSet();
    //            ds.ReadXml(GetXmlFullPath(strXmlPath));
    //            if (ds.Tables.Count > 0)
    //            {
    //                return ds;
    //            }
    //            return null;
    //        }
    //        catch (Exception)
    //        {
    //            return null;
    //        }
    //    }
    //    #endregion

    //    #region 插入数据
    //    /// <summary>
    //    /// 插入数据
    //    /// </summary>
    //    /// <param name="path">路径</param>
    //    /// <param name="node">节点</param>
    //    /// <param name="element">元素名，非空时插入新元素，否则在该元素中插入属性</param>
    //    /// <param name="attribute">属性名，非空时插入该元素属性值，否则插入元素值</param>
    //    /// <param name="value">值</param>
    //    /// 使用示列:
    //    /// XMLProsess.Insert(path, "/Node", "Element", "", "Value")
    //    /// XMLProsess.Insert(path, "/Node", "Element", "Attribute", "Value")
    //    /// XMLProsess.Insert(path, "/Node", "", "Attribute", "Value")
    //    public static void Insert(string path, string node, string element, string attribute, string value)
    //    {
    //        try
    //        {
    //            XmlDocument doc = new XmlDocument();
    //            doc.Load(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);
    //            XmlNode xn = doc.SelectSingleNode(node);
    //            if (element.Equals(""))
    //            {
    //                if (!attribute.Equals(""))
    //                {
    //                    XmlElement xe = (XmlElement)xn;
    //                    xe.SetAttribute(attribute, value);
    //                }
    //            }
    //            else
    //            {
    //                XmlElement xe = doc.CreateElement(element);
    //                if (attribute.Equals(""))
    //                    xe.InnerText = value;
    //                else
    //                    xe.SetAttribute(attribute, value);
    //                xn.AppendChild(xe);
    //            }
    //            doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);
    //        }
    //        catch { }
    //    }

    //    /// <summary>
    //    /// 插入数据
    //    /// </summary>
    //    /// <param name="path">路径</param>
    //    /// <param name="node">节点</param>
    //    /// <param name="element">元素名，非空时插入新元素，否则在该元素中插入属性</param>
    //    /// <param name="strList">由XML属性名和值组成的二维数组</param>
    //    public static void Insert(string path, string node, string element, string[][] strList)
    //    {
    //        try
    //        {
    //            XmlDocument doc = new XmlDocument();
    //            doc.Load(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);
    //            XmlNode xn = doc.SelectSingleNode(node);
    //            XmlElement xe = doc.CreateElement(element);
    //            string strAttribute = "";
    //            string strValue = "";
    //            for (int i = 0; i < strList.Length; i++)
    //            {
    //                for (int j = 0; j < strList[i].Length; j++)
    //                {
    //                    if (j == 0)
    //                        strAttribute = strList[i][j];
    //                    else
    //                        strValue = strList[i][j];
    //                }
    //                if (strAttribute.Equals(""))
    //                    xe.InnerText = strValue;
    //                else
    //                    xe.SetAttribute(strAttribute, strValue);
    //            }
    //            xn.AppendChild(xe);
    //            doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);
    //        }
    //        catch { }
    //    }

    //    /// <summary>
    //    /// 插入一行数据
    //    /// </summary>
    //    /// <param name="strXmlPath">XML文件相对路径</param>
    //    /// <param name="Columns">要插入行的列名数组，如：string[] Columns = {"name","IsMarried"};</param>
    //    /// <param name="ColumnValue">要插入行每列的值数组，如：string[] ColumnValue={"XML大全","false"};</param>
    //    /// <returns>成功返回true,否则返回false</returns>
    //    public static bool WriteXmlByDataSet(string strXmlPath, string[] Columns, string[] ColumnValue)
    //    {
    //        try
    //        {
    //            //根据传入的XML路径得到.XSD的路径，两个文件放在同一个目录下
    //            string strXsdPath = strXmlPath.Substring(0, strXmlPath.IndexOf(".")) + ".xsd";
    //            DataSet ds = new DataSet();
    //            ds.ReadXmlSchema(GetXmlFullPath(strXsdPath)); //读XML架构，关系到列的数据类型
    //            ds.ReadXml(GetXmlFullPath(strXmlPath));
    //            DataTable dt = ds.Tables[0];
    //            DataRow newRow = dt.NewRow();                 //在原来的表格基础上创建新行
    //            for (int i = 0; i < Columns.Length; i++)      //循环给一行中的各个列赋值
    //            {
    //                newRow[Columns[i]] = ColumnValue[i];
    //            }
    //            dt.Rows.Add(newRow);
    //            dt.AcceptChanges();
    //            ds.AcceptChanges();
    //            ds.WriteXml(GetXmlFullPath(strXmlPath));
    //            return true;
    //        }
    //        catch (Exception)
    //        {
    //            return false;
    //        }
    //    }
    //    #endregion

    //    #region 修改数据
    //    /// <summary>
    //    /// 修改指定节点的数据
    //    /// </summary>
    //    /// <param name="node">节点</param>
    //    /// <param name="value">值</param>
    //    public void Update(string node, string value)
    //    {
    //        try
    //        {
    //            XmlDocument doc = XMLLoad();
    //            XmlNode xn = doc.SelectSingleNode(node);
    //            xn.InnerText = value;
    //            doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + XMLPath);
    //        }
    //        catch { }
    //    }

    //    /// <summary>
    //    /// 修改指定节点的数据
    //    /// </summary>
    //    /// <param name="path">路径</param>
    //    /// <param name="node">节点</param>
    //    /// <param name="value">值</param>
    //    /// 使用示列:
    //    /// XMLProsess.Insert(path, "/Node","Value")
    //    /// XMLProsess.Insert(path, "/Node","Value")
    //    public static void Update(string path, string node, string value)
    //    {
    //        try
    //        {
    //            XmlDocument doc = XMLLoad(path);
    //            XmlNode xn = doc.SelectSingleNode(node);
    //            xn.InnerText = value;
    //            doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);
    //        }
    //        catch { }
    //    }

    //    /// <summary>
    //    /// 修改指定节点的属性值(静态)
    //    /// </summary>
    //    /// <param name="path">路径</param>
    //    /// <param name="node">节点</param>
    //    /// <param name="attribute">属性名，非空时修改该节点属性值，否则修改节点值</param>
    //    /// <param name="value">值</param>
    //    /// 使用示列:
    //    /// XMLProsess.Insert(path, "/Node", "", "Value")
    //    /// XMLProsess.Insert(path, "/Node", "Attribute", "Value")
    //    public static void Update(string path, string node, string attribute, string value)
    //    {
    //        try
    //        {
    //            XmlDocument doc = XMLLoad(path);
    //            XmlNode xn = doc.SelectSingleNode(node);
    //            XmlElement xe = (XmlElement)xn;
    //            if (attribute.Equals(""))
    //                xe.InnerText = value;
    //            else
    //                xe.SetAttribute(attribute, value);
    //            doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);
    //        }
    //        catch { }
    //    }

    //    /// <summary>
    //    /// 更改符合条件的一条记录
    //    /// </summary>
    //    /// <param name="strXmlPath">XML文件路径</param>
    //    /// <param name="Columns">列名数组</param>
    //    /// <param name="ColumnValue">列值数组</param>
    //    /// <param name="strWhereColumnName">条件列名</param>
    //    /// <param name="strWhereColumnValue">条件列值</param>
    //    public static bool UpdateXmlRow(string strXmlPath, string[] Columns, string[] ColumnValue, string strWhereColumnName, string strWhereColumnValue)
    //    {
    //        try
    //        {
    //            string strXsdPath = strXmlPath.Substring(0, strXmlPath.IndexOf(".")) + ".xsd";
    //            DataSet ds = new DataSet();
    //            ds.ReadXmlSchema(GetXmlFullPath(strXsdPath));//读XML架构，关系到列的数据类型
    //            ds.ReadXml(GetXmlFullPath(strXmlPath));

    //            //先判断行数
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //                {
    //                    //如果当前记录为符合Where条件的记录
    //                    if (ds.Tables[0].Rows[i][strWhereColumnName].ToString().Trim().Equals(strWhereColumnValue))
    //                    {
    //                        //循环给找到行的各列赋新值
    //                        for (int j = 0; j < Columns.Length; j++)
    //                        {
    //                            ds.Tables[0].Rows[i][Columns[j]] = ColumnValue[j];
    //                        }
    //                        ds.AcceptChanges();                     //更新DataSet
    //                        ds.WriteXml(GetXmlFullPath(strXmlPath));//重新写入XML文件
    //                        return true;
    //                    }
    //                }

    //            }
    //            return false;
    //        }
    //        catch (Exception)
    //        {
    //            return false;
    //        }
    //    }
    //    #endregion

    //    #region 删除数据
    //    /// <summary>
    //    /// 删除节点值
    //    /// </summary>
    //    /// <param name="path">路径</param>
    //    /// <param name="node">节点</param>
    //    /// <param name="attribute">属性名，非空时删除该节点属性值，否则删除节点值</param>
    //    /// <param name="value">值</param>
    //    /// 使用示列:
    //    /// XMLProsess.Delete(path, "/Node", "")
    //    /// XMLProsess.Delete(path, "/Node", "Attribute")
    //    public static void Delete(string path, string node)
    //    {
    //        try
    //        {
    //            XmlDocument doc = XMLLoad(path);
    //            XmlNode xn = doc.SelectSingleNode(node);
    //            xn.ParentNode.RemoveChild(xn);
    //            doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);
    //        }
    //        catch { }
    //    }

    //    /// <summary>
    //    /// 删除数据
    //    /// </summary>
    //    /// <param name="path">路径</param>
    //    /// <param name="node">节点</param>
    //    /// <param name="attribute">属性名，非空时删除该节点属性值，否则删除节点值</param>
    //    /// <param name="value">值</param>
    //    /// 使用示列:
    //    /// XMLProsess.Delete(path, "/Node", "")
    //    /// XMLProsess.Delete(path, "/Node", "Attribute")
    //    public static void Delete(string path, string node, string attribute)
    //    {
    //        try
    //        {
    //            XmlDocument doc = XMLLoad(path);
    //            XmlNode xn = doc.SelectSingleNode(node);
    //            XmlElement xe = (XmlElement)xn;
    //            if (attribute.Equals(""))
    //                xn.ParentNode.RemoveChild(xn);
    //            else
    //                xe.RemoveAttribute(attribute);
    //            doc.Save(AppDomain.CurrentDomain.BaseDirectory.ToString() + path);
    //        }
    //        catch { }
    //    }

    //    /// <summary>
    //    /// 删除所有行
    //    /// </summary>
    //    /// <param name="strXmlPath">XML路径</param>
    //    public static bool DeleteXmlAllRows(string strXmlPath)
    //    {
    //        try
    //        {
    //            DataSet ds = new DataSet();
    //            ds.ReadXml(GetXmlFullPath(strXmlPath));
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                ds.Tables[0].Rows.Clear();
    //            }
    //            ds.WriteXml(GetXmlFullPath(strXmlPath));
    //            return true;
    //        }
    //        catch (Exception)
    //        {
    //            return false;
    //        }
    //    }

    //    /// <summary>
    //    /// 通过删除DataSet中指定索引行，重写XML以实现删除指定行
    //    /// </summary>
    //    /// <param name="iDeleteRow">要删除的行在DataSet中的Index值</param>
    //    public static bool DeleteXmlRowByIndex(string strXmlPath, int iDeleteRow)
    //    {
    //        try
    //        {
    //            DataSet ds = new DataSet();
    //            ds.ReadXml(GetXmlFullPath(strXmlPath));
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                ds.Tables[0].Rows[iDeleteRow].Delete();
    //            }
    //            ds.WriteXml(GetXmlFullPath(strXmlPath));
    //            return true;
    //        }
    //        catch (Exception)
    //        {
    //            return false;
    //        }
    //    }

    //    /// <summary>
    //    /// 删除指定列中指定值的行
    //    /// </summary>
    //    /// <param name="strXmlPath">XML相对路径</param>
    //    /// <param name="strColumn">列名</param>
    //    /// <param name="ColumnValue">指定值</param>
    //    public static bool DeleteXmlRows(string strXmlPath, string strColumn, string[] ColumnValue)
    //    {
    //        try
    //        {
    //            DataSet ds = new DataSet();
    //            ds.ReadXml(GetXmlFullPath(strXmlPath));
    //            if (ds.Tables[0].Rows.Count > 0)
    //            {
    //                //判断行多还是删除的值多，多的for循环放在里面
    //                if (ColumnValue.Length > ds.Tables[0].Rows.Count)
    //                {
    //                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //                    {
    //                        for (int j = 0; j < ColumnValue.Length; j++)
    //                        {
    //                            if (ds.Tables[0].Rows[i][strColumn].ToString().Trim().Equals(ColumnValue[j]))
    //                            {
    //                                ds.Tables[0].Rows[i].Delete();
    //                            }
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    for (int j = 0; j < ColumnValue.Length; j++)
    //                    {
    //                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //                        {
    //                            if (ds.Tables[0].Rows[i][strColumn].ToString().Trim().Equals(ColumnValue[j]))
    //                            {
    //                                ds.Tables[0].Rows[i].Delete();
    //                            }
    //                        }
    //                    }
    //                }
    //                ds.WriteXml(GetXmlFullPath(strXmlPath));
    //            }
    //            return true;
    //        }
    //        catch (Exception)
    //        {
    //            return false;
    //        }
    //    }
    //    #endregion
    //}

    #endregion

    #region 文件上传与下载-N
    ///// <summary>
    ///// 文件下载帮助类
    ///// </summary>
    //public class DownLoadHelper
    //{
    //    /// <summary>
    //    ///  输出硬盘文件，提供下载 支持大文件、续传、速度限制、资源占用小
    //    /// </summary>
    //    /// <param name="_Request">Page.Request对象</param>
    //    /// <param name="_Response">Page.Response对象</param>
    //    /// <param name="_fileName">下载文件名</param>
    //    /// <param name="_fullPath">带文件名下载路径</param>
    //    /// <param name="_speed">每秒允许下载的字节数</param>
    //    /// <returns>返回是否成功</returns>
    //    public static bool ResponseFile(HttpRequest _Request, HttpResponse _Response, string _fileName, string _fullPath, long _speed) 
    //    {
    //        try
    //        {
    //            FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
    //            BinaryReader br = new BinaryReader(myFile);
    //            try
    //            {
    //                _Response.AddHeader("Accept-Ranges", "bytes");
    //                _Response.Buffer = false;
    //                long fileLength = myFile.Length;
    //                long startBytes = 0;

    //                int pack = 10240; //10K bytes
    //                //int sleep = 200;   //每秒5次   即5*10K bytes每秒
    //                int sleep = (int)Math.Floor((double)(1000 * pack / _speed)) + 1;
    //                if (_Request.Headers["Range"] != null)
    //                {
    //                    _Response.StatusCode = 206;
    //                    string[] range = _Request.Headers["Range"].Split(new char[] { '=', '-' });
    //                    startBytes = Convert.ToInt64(range[1]);
    //                }
    //                _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
    //                if (startBytes != 0)
    //                {
    //                    _Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
    //                }
    //                _Response.AddHeader("Connection", "Keep-Alive");
    //                _Response.ContentType = "application/octet-stream";
    //                _Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName, System.Text.Encoding.UTF8));

    //                br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
    //                int maxCount = (int)Math.Floor((double)((fileLength - startBytes) / pack)) + 1;

    //                for (int i = 0; i < maxCount; i++)
    //                {
    //                    if (_Response.IsClientConnected)
    //                    {
    //                        _Response.BinaryWrite(br.ReadBytes(pack));
    //                        Thread.Sleep(sleep);
    //                    }
    //                    else
    //                    {
    //                        i = maxCount;
    //                    }
    //                }
    //            }
    //            catch
    //            {
    //                return false;
    //            }
    //            finally
    //            {
    //                br.Close();
    //                myFile.Close();
    //            }
    //        }
    //        catch
    //        {
    //            return false;
    //        }
    //        return true;
    //    }
    //}

    ///// <summary>
    ///// 文件下载类
    ///// </summary>
    //public class FileDown
    //{
    //    public FileDown()
    //    { }

    //    /// <summary>
    //    /// 参数为虚拟路径
    //    /// </summary>
    //    public static string FileNameExtension(string FileName)
    //    {
    //        return Path.GetExtension(MapPathFile(FileName));
    //    }

    //    /// <summary>
    //    /// 获取物理地址
    //    /// </summary>
    //    public static string MapPathFile(string FileName)
    //    {
    //        return HttpContext.Current.Server.MapPath(FileName);
    //    }

    //    /// <summary>
    //    /// 普通下载
    //    /// </summary>
    //    /// <param name="FileName">文件虚拟路径</param>
    //    public static void DownLoadold(string FileName)
    //    {
    //        string destFileName = MapPathFile(FileName);
    //        if (File.Exists(destFileName))
    //        {
    //            FileInfo fi = new FileInfo(destFileName);
    //            HttpContext.Current.Response.Clear();
    //            HttpContext.Current.Response.ClearHeaders();
    //            HttpContext.Current.Response.Buffer = false;
    //            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(Path.GetFileName(destFileName), System.Text.Encoding.UTF8));
    //            HttpContext.Current.Response.AppendHeader("Content-Length", fi.Length.ToString());
    //            HttpContext.Current.Response.ContentType = "application/octet-stream";
    //            HttpContext.Current.Response.WriteFile(destFileName);
    //            HttpContext.Current.Response.Flush();
    //            HttpContext.Current.Response.End();
    //        }
    //    }

    //    /// <summary>
    //    /// 分块下载
    //    /// </summary>
    //    /// <param name="FileName">文件虚拟路径</param>
    //    public static void DownLoad(string FileName)
    //    {
    //        string filePath = MapPathFile(FileName);
    //        long chunkSize = 204800;             //指定块大小 
    //        byte[] buffer = new byte[chunkSize]; //建立一个200K的缓冲区 
    //        long dataToRead = 0;                 //已读的字节数   
    //        FileStream stream = null;
    //        try
    //        {
    //            //打开文件   
    //            stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
    //            dataToRead = stream.Length;

    //            //添加Http头   
    //            HttpContext.Current.Response.ContentType = "application/octet-stream";
    //            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachement;filename=" + HttpUtility.UrlEncode(Path.GetFileName(filePath)));
    //            HttpContext.Current.Response.AddHeader("Content-Length", dataToRead.ToString());

    //            while (dataToRead > 0)
    //            {
    //                if (HttpContext.Current.Response.IsClientConnected)
    //                {
    //                    int length = stream.Read(buffer, 0, Convert.ToInt32(chunkSize));
    //                    HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
    //                    HttpContext.Current.Response.Flush();
    //                    HttpContext.Current.Response.Clear();
    //                    dataToRead -= length;
    //                }
    //                else
    //                {
    //                    dataToRead = -1; //防止client失去连接 
    //                }
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            HttpContext.Current.Response.Write("Error:" + ex.Message);
    //        }
    //        finally
    //        {
    //            if (stream != null) stream.Close();
    //            HttpContext.Current.Response.Close();
    //        }
    //    }

    //    /// <summary>
    //    ///  输出硬盘文件，提供下载 支持大文件、续传、速度限制、资源占用小
    //    /// </summary>
    //    /// <param name="_Request">Page.Request对象</param>
    //    /// <param name="_Response">Page.Response对象</param>
    //    /// <param name="_fileName">下载文件名</param>
    //    /// <param name="_fullPath">带文件名下载路径</param>
    //    /// <param name="_speed">每秒允许下载的字节数</param>
    //    /// <returns>返回是否成功</returns>
    //    //---------------------------------------------------------------------
    //    //调用：
    //    // string FullPath=Server.MapPath("count.txt");
    //    // ResponseFile(this.Request,this.Response,"count.txt",FullPath,100);
    //    //---------------------------------------------------------------------
    //    public static bool ResponseFile(HttpRequest _Request, HttpResponse _Response, string _fileName, string _fullPath, long _speed)
    //    {
    //        try
    //        {
    //            FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
    //            BinaryReader br = new BinaryReader(myFile);
    //            try
    //            {
    //                _Response.AddHeader("Accept-Ranges", "bytes");
    //                _Response.Buffer = false;

    //                long fileLength = myFile.Length;
    //                long startBytes = 0;
    //                int pack = 10240;  //10K bytes
    //                int sleep = (int)Math.Floor((double)(1000 * pack / _speed)) + 1;

    //                if (_Request.Headers["Range"] != null)
    //                {
    //                    _Response.StatusCode = 206;
    //                    string[] range = _Request.Headers["Range"].Split(new char[] { '=', '-' });
    //                    startBytes = Convert.ToInt64(range[1]);
    //                }
    //                _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
    //                if (startBytes != 0)
    //                {
    //                    _Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
    //                }

    //                _Response.AddHeader("Connection", "Keep-Alive");
    //                _Response.ContentType = "application/octet-stream";
    //                _Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName, System.Text.Encoding.UTF8));

    //                br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
    //                int maxCount = (int)Math.Floor((double)((fileLength - startBytes) / pack)) + 1;

    //                for (int i = 0; i < maxCount; i++)
    //                {
    //                    if (_Response.IsClientConnected)
    //                    {
    //                        _Response.BinaryWrite(br.ReadBytes(pack));
    //                        Thread.Sleep(sleep);
    //                    }
    //                    else
    //                    {
    //                        i = maxCount;
    //                    }
    //                }
    //            }
    //            catch
    //            {
    //                return false;
    //            }
    //            finally
    //            {
    //                br.Close();
    //                myFile.Close();
    //            }
    //        }
    //        catch
    //        {
    //            return false;
    //        }
    //        return true;
    //    }
    //}

    ///// <summary>
    ///// 文件上传类
    ///// </summary>
    //public class FileUp
    //{
    //    public FileUp()
    //    { }

    //    /// <summary>
    //    /// 转换为字节数组
    //    /// </summary>
    //    /// <param name="filename">文件名</param>
    //    /// <returns>字节数组</returns>
    //    public byte[] GetBinaryFile(string filename)
    //    {
    //        if (File.Exists(filename))
    //        {
    //            FileStream Fsm = null;
    //            try
    //            {
    //                Fsm = File.OpenRead(filename);
    //                return this.ConvertStreamToByteBuffer(Fsm);
    //            }
    //            catch
    //            {
    //                return new byte[0];
    //            }
    //            finally
    //            {
    //                Fsm.Close();
    //            }
    //        }
    //        else
    //        {
    //            return new byte[0];
    //        }
    //    }

    //    /// <summary>
    //    /// 流转化为字节数组
    //    /// </summary>
    //    /// <param name="theStream">流</param>
    //    /// <returns>字节数组</returns>
    //    public byte[] ConvertStreamToByteBuffer(System.IO.Stream theStream)
    //    {
    //        int bi;
    //        MemoryStream tempStream = new System.IO.MemoryStream();
    //        try
    //        {
    //            while ((bi = theStream.ReadByte()) != -1)
    //            {
    //                tempStream.WriteByte(((byte)bi));
    //            }
    //            return tempStream.ToArray();
    //        }
    //        catch
    //        {
    //            return new byte[0];
    //        }
    //        finally
    //        {
    //            tempStream.Close();
    //        }
    //    }

    //    /// <summary>
    //    /// 上传文件
    //    /// </summary>
    //    /// <param name="PosPhotoUpload">控件</param>
    //    /// <param name="saveFileName">保存的文件名</param>
    //    /// <param name="imagePath">保存的文件路径</param>
    //    public string FileSc(FileUpload PosPhotoUpload, string saveFileName, string imagePath)
    //    {
    //        string state = "";
    //        if (PosPhotoUpload.HasFile)
    //        {
    //            if (PosPhotoUpload.PostedFile.ContentLength / 1024 < 10240)
    //            {
    //                string MimeType = PosPhotoUpload.PostedFile.ContentType;
    //                if (String.Equals(MimeType, "image/gif") || String.Equals(MimeType, "image/pjpeg"))
    //                {
    //                    string extFileString = System.IO.Path.GetExtension(PosPhotoUpload.PostedFile.FileName);
    //                    PosPhotoUpload.PostedFile.SaveAs(HttpContext.Current.Server.MapPath(imagePath));
    //                }
    //                else
    //                {
    //                    state = "上传文件类型不正确";
    //                }
    //            }
    //            else
    //            {
    //                state = "上传文件不能大于10M";
    //            }
    //        }
    //        else
    //        {
    //            state = "没有上传文件";
    //        }
    //        return state;
    //    }

    //    /// <summary>
    //    /// 上传文件
    //    /// </summary>
    //    /// <param name="binData">字节数组</param>
    //    /// <param name="fileName">文件名</param>
    //    /// <param name="fileType">文件类型</param>
    //    //-------------------调用----------------------
    //    //byte[] by = GetBinaryFile("E:\\Hello.txt");
    //    //this.SaveFile(by,"Hello",".txt");
    //    //---------------------------------------------
    //    public void SaveFile(byte[] binData, string fileName, string fileType)
    //    {
    //        FileStream fileStream = null;
    //        MemoryStream m = new MemoryStream(binData);
    //        try
    //        {
    //            string savePath = HttpContext.Current.Server.MapPath("~/File/");
    //            if (!Directory.Exists(savePath))
    //            {
    //                Directory.CreateDirectory(savePath);
    //            }
    //            string File = savePath + fileName + fileType;
    //            fileStream = new FileStream(File, FileMode.Create);
    //            m.WriteTo(fileStream);
    //        }
    //        finally
    //        {
    //            m.Close();
    //            fileStream.Close();
    //        }
    //    }
    //}

    ///// <summary>
    ///// UpLoadFiles 的摘要说明
    ///// </summary>
    //public class UpLoadFiles : System.Web.UI.Page
    //{
    //    public UpLoadFiles()
    //    {
    //        //
    //        // TODO: 在此处添加构造函数逻辑
    //        //
    //    }

    //    public string UploadFile(string filePath, int maxSize, string[] fileType, System.Web.UI.HtmlControls.HtmlInputFile TargetFile)
    //    {
    //        string Result = "UnDefine";
    //        bool typeFlag = false;
    //        string FilePath = filePath;
    //        int MaxSize = maxSize;
    //        string strFileName, strNewName, strFilePath;
    //        if (TargetFile.PostedFile.FileName == "")
    //        {
    //            return "FILE_ERR";
    //        }
    //        strFileName = TargetFile.PostedFile.FileName;
    //        TargetFile.Accept = "*/*";
    //        strFilePath = FilePath;
    //        if (Directory.Exists(strFilePath) == false)
    //        {
    //            Directory.CreateDirectory(strFilePath);
    //        }
    //        FileInfo myInfo = new FileInfo(strFileName);
    //        string strOldName = myInfo.Name;
    //        strNewName = strOldName.Substring(strOldName.LastIndexOf("."));
    //        strNewName = strNewName.ToLower();
    //        if (TargetFile.PostedFile.ContentLength <= MaxSize)
    //        {
    //            for (int i = 0; i <= fileType.GetUpperBound(0); i++)
    //            {
    //                if (strNewName.ToLower() == fileType[i].ToString()) { typeFlag = true; break; }
    //            }
    //            if (typeFlag)
    //            {
    //                string strFileNameTemp = GetUploadFileName();
    //                string strFilePathTemp = strFilePath;
    //                float strFileSize = TargetFile.PostedFile.ContentLength;
    //                strOldName = strFileNameTemp + strNewName;
    //                strFilePath = strFilePath + "\\" + strOldName;
    //                TargetFile.PostedFile.SaveAs(strFilePath);
    //                Result = strOldName + "|" + strFileSize;
    //                TargetFile.Dispose();
    //            }
    //            else
    //            {
    //                return "TYPE_ERR";
    //            }
    //        }
    //        else
    //        {
    //            return "SIZE_ERR";
    //        }
    //        return (Result);
    //    }

    //    /// <summary>
    //    /// 上传文件
    //    /// </summary>
    //    /// <param name="filePath">保存文件地址</param>
    //    /// <param name="maxSize">文件最大大小</param>
    //    /// <param name="fileType">文件后缀类型</param>
    //    /// <param name="TargetFile">控件名</param>
    //    /// <param name="saveFileName">保存后的文件名和地址</param>
    //    /// <param name="fileSize">文件大小</param>
    //    /// <returns></returns>
    //    public string UploadFile(string filePath, int maxSize, string[] fileType, System.Web.UI.HtmlControls.HtmlInputFile TargetFile, out string saveFileName, out int fileSize)
    //    {
    //        saveFileName = "";
    //        fileSize = 0;

    //        string Result = "";
    //        bool typeFlag = false;
    //        string FilePath = filePath;
    //        int MaxSize = maxSize;
    //        string strFileName, strNewName, strFilePath;
    //        if (TargetFile.PostedFile.FileName == "")
    //        {
    //            return "请选择上传的文件";
    //        }
    //        strFileName = TargetFile.PostedFile.FileName;
    //        TargetFile.Accept = "*/*";
    //        strFilePath = FilePath;
    //        if (Directory.Exists(strFilePath) == false)
    //        {
    //            Directory.CreateDirectory(strFilePath);
    //        }
    //        FileInfo myInfo = new FileInfo(strFileName);
    //        string strOldName = myInfo.Name;
    //        strNewName = strOldName.Substring(strOldName.LastIndexOf("."));
    //        strNewName = strNewName.ToLower();
    //        if (TargetFile.PostedFile.ContentLength <= MaxSize)
    //        {
    //            string strFileNameTemp = GetUploadFileName();
    //            string strFilePathTemp = strFilePath;
    //            strOldName = strFileNameTemp + strNewName;
    //            strFilePath = strFilePath + "\\" + strOldName;

    //            fileSize = TargetFile.PostedFile.ContentLength / 1024;
    //            saveFileName = strFilePath.Substring(strFilePath.IndexOf("FileUpload\\"));
    //            TargetFile.PostedFile.SaveAs(strFilePath);
    //            TargetFile.Dispose();
    //        }
    //        else
    //        {
    //            return "上传文件超出指定的大小";
    //        }
    //        return (Result);
    //    }

    //    public string UploadFile(string filePath, int maxSize, string[] fileType, string filename, System.Web.UI.HtmlControls.HtmlInputFile TargetFile)
    //    {
    //        string Result = "UnDefine";
    //        bool typeFlag = false;
    //        string FilePath = filePath;
    //        int MaxSize = maxSize;
    //        string strFileName, strNewName, strFilePath;
    //        if (TargetFile.PostedFile.FileName == "")
    //        {
    //            return "FILE_ERR";
    //        }
    //        strFileName = TargetFile.PostedFile.FileName;
    //        TargetFile.Accept = "*/*";
    //        strFilePath = FilePath;
    //        if (Directory.Exists(strFilePath) == false)
    //        {
    //            Directory.CreateDirectory(strFilePath);
    //        }
    //        FileInfo myInfo = new FileInfo(strFileName);
    //        string strOldName = myInfo.Name;
    //        strNewName = strOldName.Substring(strOldName.Length - 3, 3);
    //        strNewName = strNewName.ToLower();
    //        if (TargetFile.PostedFile.ContentLength <= MaxSize)
    //        {
    //            for (int i = 0; i <= fileType.GetUpperBound(0); i++)
    //            {
    //                if (strNewName.ToLower() == fileType[i].ToString()) { typeFlag = true; break; }
    //            }
    //            if (typeFlag)
    //            {
    //                string strFileNameTemp = filename;
    //                string strFilePathTemp = strFilePath;
    //                strOldName = strFileNameTemp + "." + strNewName;
    //                strFilePath = strFilePath + "\\" + strOldName;
    //                TargetFile.PostedFile.SaveAs(strFilePath);
    //                Result = strOldName;
    //                TargetFile.Dispose();
    //            }
    //            else
    //            {
    //                return "TYPE_ERR";
    //            }
    //        }
    //        else
    //        {
    //            return "SIZE_ERR";
    //        }
    //        return (Result);
    //    }

    //    public string GetUploadFileName()
    //    {
    //        string Result = "";
    //        DateTime time = DateTime.Now;
    //        Result += time.Year.ToString() + FormatNum(time.Month.ToString(), 2) + FormatNum(time.Day.ToString(), 2) + FormatNum(time.Hour.ToString(), 2) + FormatNum(time.Minute.ToString(), 2) + FormatNum(time.Second.ToString(), 2) + FormatNum(time.Millisecond.ToString(), 3);
    //        return (Result);
    //    }

    //    public string FormatNum(string Num, int Bit)
    //    {
    //        int L;
    //        L = Num.Length;
    //        for (int i = L; i < Bit; i++)
    //        {
    //            Num = "0" + Num;
    //        }
    //        return (Num);
    //    }

    //}
    #endregion

    #region JavaScript客户端脚本输出-N
    /// <summary>
    /// 客户端脚本输出
    /// </summary>
    //public class JsHelper
    //{
    //    /// <summary>
    //    /// 弹出信息,并跳转指定页面。
    //    /// </summary>
    //    public static void AlertAndRedirect(string message, string toURL)
    //    {
    //        string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
    //        HttpContext.Current.Response.Write(string.Format(js, message, toURL));
    //        HttpContext.Current.Response.End();
    //    }

    //    /// <summary>
    //    /// 弹出信息,并返回历史页面
    //    /// </summary>
    //    public static void AlertAndGoHistory(string message, int value)
    //    {
    //        string js = @"<Script language='JavaScript'>alert('{0}');history.go({1});</Script>";
    //        HttpContext.Current.Response.Write(string.Format(js, message, value));
    //        HttpContext.Current.Response.End();
    //    }

    //    /// <summary>
    //    /// 直接跳转到指定的页面
    //    /// </summary>
    //    public static void Redirect(string toUrl)
    //    {
    //        string js = @"<script language=javascript>window.location.replace('{0}')</script>";
    //        HttpContext.Current.Response.Write(string.Format(js, toUrl));
    //    }

    //    /// <summary>
    //    /// 弹出信息 并指定到父窗口
    //    /// </summary>
    //    public static void AlertAndParentUrl(string message, string toURL)
    //    {
    //        string js = "<script language=javascript>alert('{0}');window.top.location.replace('{1}')</script>";
    //        HttpContext.Current.Response.Write(string.Format(js, message, toURL));
    //    }

    //    /// <summary>
    //    /// 返回到父窗口
    //    /// </summary>
    //    public static void ParentRedirect(string ToUrl)
    //    {
    //        string js = "<script language=javascript>window.top.location.replace('{0}')</script>";
    //        HttpContext.Current.Response.Write(string.Format(js, ToUrl));
    //    }

    //    /// <summary>
    //    /// 返回历史页面
    //    /// </summary>
    //    public static void BackHistory(int value)
    //    {
    //        string js = @"<Script language='JavaScript'>history.go({0});</Script>";
    //        HttpContext.Current.Response.Write(string.Format(js, value));
    //        HttpContext.Current.Response.End();
    //    }

    //    /// <summary>
    //    /// 弹出信息
    //    /// </summary>
    //    public static void Alert(string message)
    //    {
    //        string js = "<script language=javascript>alert('{0}');</script>";
    //        HttpContext.Current.Response.Write(string.Format(js, message));
    //    }

    //    /// <summary>
    //    /// 注册脚本块
    //    /// </summary>
    //    public static void RegisterScriptBlock(System.Web.UI.Page page, string _ScriptString)
    //    {
    //        page.ClientScript.RegisterStartupScript(page.GetType(), "scriptblock", "<script type='text/javascript'>" + _ScriptString + "</script>");
    //    }
    //}
    #endregion

    #region JSON-N

    /// <summary>
    /// JSON转换类
    /// </summary>
    //public class ConvertJson
    //{
    //    #region 私有方法
    //    /// <summary>
    //    /// 过滤特殊字符
    //    /// </summary>
    //    private static string String2Json(String s)
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        for (int i = 0; i < s.Length; i++)
    //        {
    //            char c = s.ToCharArray()[i];
    //            switch (c)
    //            {
    //                case '\"':
    //                    sb.Append("\\\""); break;
    //                case '\\':
    //                    sb.Append("\\\\"); break;
    //                case '/':
    //                    sb.Append("\\/"); break;
    //                case '\b':
    //                    sb.Append("\\b"); break;
    //                case '\f':
    //                    sb.Append("\\f"); break;
    //                case '\n':
    //                    sb.Append("\\n"); break;
    //                case '\r':
    //                    sb.Append("\\r"); break;
    //                case '\t':
    //                    sb.Append("\\t"); break;
    //                default:
    //                    sb.Append(c); break;
    //            }
    //        }
    //        return sb.ToString();
    //    }

    //    /// <summary>
    //    /// 格式化字符型、日期型、布尔型
    //    /// </summary>
    //    private static string StringFormat(string str, Type type)
    //    {
    //        if (type == typeof(string))
    //        {
    //            str = String2Json(str);
    //            str = "\"" + str + "\"";
    //        }
    //        else if (type == typeof(DateTime))
    //        {
    //            str = "\"" + str + "\"";
    //        }
    //        else if (type == typeof(bool))
    //        {
    //            str = str.ToLower();
    //        }
    //        else if (type != typeof(string) && string.IsNullOrEmpty(str))
    //        {
    //            str = "\"" + str + "\"";
    //        }
    //        return str;
    //    }
    //    #endregion

    //    #region List转换成Json
    //    /// <summary>
    //    /// List转换成Json
    //    /// </summary>
    //    public static string ListToJson<T>(IList<T> list)
    //    {
    //        object obj = list[0];
    //        return ListToJson<T>(list, obj.GetType().Name);
    //    }

    //    /// <summary>
    //    /// List转换成Json 
    //    /// </summary>
    //    public static string ListToJson<T>(IList<T> list, string jsonName)
    //    {
    //        StringBuilder Json = new StringBuilder();
    //        if (string.IsNullOrEmpty(jsonName)) jsonName = list[0].GetType().Name;
    //        Json.Append("{\"" + jsonName + "\":[");
    //        if (list.Count > 0)
    //        {
    //            for (int i = 0; i < list.Count; i++)
    //            {
    //                T obj = Activator.CreateInstance<T>();
    //                PropertyInfo[] pi = obj.GetType().GetProperties();
    //                Json.Append("{");
    //                for (int j = 0; j < pi.Length; j++)
    //                {
    //                    Type type = pi[j].GetValue(list[i], null).GetType();
    //                    Json.Append("\"" + pi[j].Name.ToString() + "\":" + StringFormat(pi[j].GetValue(list[i], null).ToString(), type));

    //                    if (j < pi.Length - 1)
    //                    {
    //                        Json.Append(",");
    //                    }
    //                }
    //                Json.Append("}");
    //                if (i < list.Count - 1)
    //                {
    //                    Json.Append(",");
    //                }
    //            }
    //        }
    //        Json.Append("]}");
    //        return Json.ToString();
    //    }
    //    #endregion

    //    #region 对象转换为Json
    //    /// <summary> 
    //    /// 对象转换为Json 
    //    /// </summary> 
    //    /// <param name="jsonObject">对象</param> 
    //    /// <returns>Json字符串</returns> 
    //    public static string ToJson(object jsonObject)
    //    {
    //        string jsonString = "{";
    //        PropertyInfo[] propertyInfo = jsonObject.GetType().GetProperties();
    //        for (int i = 0; i < propertyInfo.Length; i++)
    //        {
    //            object objectValue = propertyInfo[i].GetGetMethod().Invoke(jsonObject, null);
    //            string value = string.Empty;
    //            if (objectValue is DateTime || objectValue is Guid || objectValue is TimeSpan)
    //            {
    //                value = "'" + objectValue.ToString() + "'";
    //            }
    //            else if (objectValue is string)
    //            {
    //                value = "'" + ToJson(objectValue.ToString()) + "'";
    //            }
    //            else if (objectValue is IEnumerable)
    //            {
    //                value = ToJson((IEnumerable)objectValue);
    //            }
    //            else
    //            {
    //                value = ToJson(objectValue.ToString());
    //            }
    //            jsonString += "\"" + ToJson(propertyInfo[i].Name) + "\":" + value + ",";
    //        }
    //        jsonString.Remove(jsonString.Length - 1, jsonString.Length);
    //        return jsonString + "}";
    //    }
    //    #endregion

    //    #region 对象集合转换Json
    //    /// <summary> 
    //    /// 对象集合转换Json 
    //    /// </summary> 
    //    /// <param name="array">集合对象</param> 
    //    /// <returns>Json字符串</returns> 
    //    public static string ToJson(IEnumerable array)
    //    {
    //        string jsonString = "[";
    //        foreach (object item in array)
    //        {
    //            jsonString += ToJson(item) + ",";
    //        }
    //        jsonString.Remove(jsonString.Length - 1, jsonString.Length);
    //        return jsonString + "]";
    //    }
    //    #endregion

    //    #region 普通集合转换Json
    //    /// <summary> 
    //    /// 普通集合转换Json 
    //    /// </summary> 
    //    /// <param name="array">集合对象</param> 
    //    /// <returns>Json字符串</returns> 
    //    public static string ToArrayString(IEnumerable array)
    //    {
    //        string jsonString = "[";
    //        foreach (object item in array)
    //        {
    //            jsonString = ToJson(item.ToString()) + ",";
    //        }
    //        jsonString.Remove(jsonString.Length - 1, jsonString.Length);
    //        return jsonString + "]";
    //    }
    //    #endregion

    //    #region  DataSet转换为Json
    //    /// <summary> 
    //    /// DataSet转换为Json 
    //    /// </summary> 
    //    /// <param name="dataSet">DataSet对象</param> 
    //    /// <returns>Json字符串</returns> 
    //    public static string ToJson(DataSet dataSet)
    //    {
    //        string jsonString = "{";
    //        foreach (DataTable table in dataSet.Tables)
    //        {
    //            jsonString += "\"" + table.TableName + "\":" + ToJson(table) + ",";
    //        }
    //        jsonString = jsonString.TrimEnd(',');
    //        return jsonString + "}";
    //    }
    //    #endregion

    //    #region Datatable转换为Json
    //    /// <summary> 
    //    /// Datatable转换为Json 
    //    /// </summary> 
    //    /// <param name="table">Datatable对象</param> 
    //    /// <returns>Json字符串</returns> 
    //    public static string ToJson(DataTable dt)
    //    {
    //        StringBuilder jsonString = new StringBuilder();
    //        jsonString.Append("[");
    //        DataRowCollection drc = dt.Rows;
    //        for (int i = 0; i < drc.Count; i++)
    //        {
    //            jsonString.Append("{");
    //            for (int j = 0; j < dt.Columns.Count; j++)
    //            {
    //                string strKey = dt.Columns[j].ColumnName;
    //                string strValue = drc[i][j].ToString();
    //                Type type = dt.Columns[j].DataType;
    //                jsonString.Append("\"" + strKey + "\":");
    //                strValue = StringFormat(strValue, type);
    //                if (j < dt.Columns.Count - 1)
    //                {
    //                    jsonString.Append(strValue + ",");
    //                }
    //                else
    //                {
    //                    jsonString.Append(strValue);
    //                }
    //            }
    //            jsonString.Append("},");
    //        }
    //        jsonString.Remove(jsonString.Length - 1, 1);
    //        jsonString.Append("]");
    //        return jsonString.ToString();
    //    }

    //    /// <summary>
    //    /// DataTable转换为Json 
    //    /// </summary>
    //    public static string ToJson(DataTable dt, string jsonName)
    //    {
    //        StringBuilder Json = new StringBuilder();
    //        if (string.IsNullOrEmpty(jsonName)) jsonName = dt.TableName;
    //        Json.Append("{\"" + jsonName + "\":[");
    //        if (dt.Rows.Count > 0)
    //        {
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                Json.Append("{");
    //                for (int j = 0; j < dt.Columns.Count; j++)
    //                {
    //                    Type type = dt.Rows[i][j].GetType();
    //                    Json.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + StringFormat(dt.Rows[i][j].ToString(), type));
    //                    if (j < dt.Columns.Count - 1)
    //                    {
    //                        Json.Append(",");
    //                    }
    //                }
    //                Json.Append("}");
    //                if (i < dt.Rows.Count - 1)
    //                {
    //                    Json.Append(",");
    //                }
    //            }
    //        }
    //        Json.Append("]}");
    //        return Json.ToString();
    //    }
    //    #endregion

    //    #region DataReader转换为Json
    //    /// <summary> 
    //    /// DataReader转换为Json 
    //    /// </summary> 
    //    /// <param name="dataReader">DataReader对象</param> 
    //    /// <returns>Json字符串</returns> 
    //    public static string ToJson(DbDataReader dataReader)
    //    {
    //        StringBuilder jsonString = new StringBuilder();
    //        jsonString.Append("[");
    //        while (dataReader.Read())
    //        {
    //            jsonString.Append("{");
    //            for (int i = 0; i < dataReader.FieldCount; i++)
    //            {
    //                Type type = dataReader.GetFieldType(i);
    //                string strKey = dataReader.GetName(i);
    //                string strValue = dataReader[i].ToString();
    //                jsonString.Append("\"" + strKey + "\":");
    //                strValue = StringFormat(strValue, type);
    //                if (i < dataReader.FieldCount - 1)
    //                {
    //                    jsonString.Append(strValue + ",");
    //                }
    //                else
    //                {
    //                    jsonString.Append(strValue);
    //                }
    //            }
    //            jsonString.Append("},");
    //        }
    //        dataReader.Close();
    //        jsonString.Remove(jsonString.Length - 1, 1);
    //        jsonString.Append("]");
    //        return jsonString.ToString();
    //    }
    //    #endregion
    //}
    #endregion

    #region Date-N

    /// <summary>
    /// 时间操作
    /// </summary>
    //public class DateFormat
    //{
    //    //返回每月的第一天和最后一天
    //    public static void ReturnDateFormat(int month, out string firstDay, out string lastDay)
    //    {
    //        int year = DateTime.Now.Year + month / 12;
    //        if (month != 12)
    //        {
    //            month = month % 12;
    //        }
    //        switch (month)
    //        {
    //            case 1:
    //                firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
    //                lastDay = DateTime.Now.ToString(year + "-0" + month + "-31");
    //                break;
    //            case 2:
    //                firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
    //                if (DateTime.IsLeapYear(DateTime.Now.Year))
    //                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-29");
    //                else
    //                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-28");
    //                break;
    //            case 3:
    //                firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
    //                lastDay = DateTime.Now.ToString("yyyy-0" + month + "-31");
    //                break;
    //            case 4:
    //                firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
    //                lastDay = DateTime.Now.ToString(year + "-0" + month + "-30");
    //                break;
    //            case 5:
    //                firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
    //                lastDay = DateTime.Now.ToString(year + "-0" + month + "-31");
    //                break;
    //            case 6:
    //                firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
    //                lastDay = DateTime.Now.ToString(year + "-0" + month + "-30");
    //                break;
    //            case 7:
    //                firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
    //                lastDay = DateTime.Now.ToString(year + "-0" + month + "-31");
    //                break;
    //            case 8:
    //                firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
    //                lastDay = DateTime.Now.ToString(year + "-0" + month + "-31");
    //                break;
    //            case 9:
    //                firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
    //                lastDay = DateTime.Now.ToString(year + "-0" + month + "-30");
    //                break;
    //            case 10:
    //                firstDay = DateTime.Now.ToString(year + "-" + month + "-01");
    //                lastDay = DateTime.Now.ToString(year + "-" + month + "-31");
    //                break;
    //            case 11:
    //                firstDay = DateTime.Now.ToString(year + "-" + month + "-01");
    //                lastDay = DateTime.Now.ToString(year + "-" + month + "-30");
    //                break;
    //            default:
    //                firstDay = DateTime.Now.ToString(year + "-" + month + "-01");
    //                lastDay = DateTime.Now.ToString(year + "-" + month + "-31");
    //                break;
    //        }
    //    }
    //}

    /// <summary>
    /// 时间类
    /// 1、SecondToMinute(int Second) 把秒转换成分钟
    /// </summary>
    public class TimeHelper
    {
        /// <summary>
        /// 将时间格式化成 年月日 的形式,如果时间为null，返回当前系统时间
        /// </summary>
        /// <param name="dt">年月日分隔符</param>
        /// <param name="Separator"></param>
        /// <returns></returns>
        public string GetFormatDate(DateTime dt, char Separator)
        {
            if (dt != null && !dt.Equals(DBNull.Value))
            {
                string tem = string.Format("yyyy{0}MM{1}dd", Separator, Separator);
                return dt.ToString(tem);
            }
            else
            {
                return GetFormatDate(DateTime.Now, Separator);
            }
        }
        /// <summary>
        /// 将时间格式化成 时分秒 的形式,如果时间为null，返回当前系统时间
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Separator"></param>
        /// <returns></returns>
        public string GetFormatTime(DateTime dt, char Separator)
        {
            if (dt != null && !dt.Equals(DBNull.Value))
            {
                string tem = string.Format("hh{0}mm{1}ss", Separator, Separator);
                return dt.ToString(tem);
            }
            else
            {
                return GetFormatDate(DateTime.Now, Separator);
            }
        }
        /// <summary>
        /// 把秒转换成分钟
        /// </summary>
        /// <returns></returns>
        public static int SecondToMinute(int Second)
        {
            decimal mm = (decimal)((decimal)Second / (decimal)60);
            return Convert.ToInt32(Math.Ceiling(mm));
        }

        #region 返回某年某月最后一天
        /// <summary>
        /// 返回某年某月最后一天
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>日</returns>
        public static int GetMonthLastDate(int year, int month)
        {
            DateTime lastDay = new DateTime(year, month, new System.Globalization.GregorianCalendar().GetDaysInMonth(year, month));
            int Day = lastDay.Day;
            return Day;
        }
        #endregion

        #region 返回时间差
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            try
            {
                //TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                //TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                //TimeSpan ts = ts1.Subtract(ts2).Duration();
                TimeSpan ts = DateTime2 - DateTime1;
                if (ts.Days >= 1)
                {
                    dateDiff = DateTime1.Month.ToString() + "月" + DateTime1.Day.ToString() + "日";
                }
                else
                {
                    if (ts.Hours > 1)
                    {
                        dateDiff = ts.Hours.ToString() + "小时前";
                    }
                    else
                    {
                        dateDiff = ts.Minutes.ToString() + "分钟前";
                    }
                }
            }
            catch
            { }
            return dateDiff;
        }
        #endregion

        #region 获得两个日期的间隔
        /// <summary>
        /// 获得两个日期的间隔
        /// </summary>
        /// <param name="DateTime1">日期一。</param>
        /// <param name="DateTime2">日期二。</param>
        /// <returns>日期间隔TimeSpan。</returns>
        public static TimeSpan DateDiff2(DateTime DateTime1, DateTime DateTime2)
        {
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts;
        }
        #endregion

        #region 格式化日期时间
        /// <summary>
        /// 格式化日期时间
        /// </summary>
        /// <param name="dateTime1">日期时间</param>
        /// <param name="dateMode">显示模式</param>
        /// <returns>0-9种模式的日期</returns>
        public static string FormatDate(DateTime dateTime1, string dateMode)
        {
            switch (dateMode)
            {
                case "0":
                    return dateTime1.ToString("yyyy-MM-dd");
                case "1":
                    return dateTime1.ToString("yyyy-MM-dd HH:mm:ss");
                case "2":
                    return dateTime1.ToString("yyyy/MM/dd");
                case "3":
                    return dateTime1.ToString("yyyy年MM月dd日");
                case "4":
                    return dateTime1.ToString("MM-dd");
                case "5":
                    return dateTime1.ToString("MM/dd");
                case "6":
                    return dateTime1.ToString("MM月dd日");
                case "7":
                    return dateTime1.ToString("yyyy-MM");
                case "8":
                    return dateTime1.ToString("yyyy/MM");
                case "9":
                    return dateTime1.ToString("yyyy年MM月");
                default:
                    return dateTime1.ToString();
            }
        }
        #endregion

        #region 得到随机日期
        /// <summary>
        /// 得到随机日期
        /// </summary>
        /// <param name="time1">起始日期</param>
        /// <param name="time2">结束日期</param>
        /// <returns>间隔日期之间的 随机日期</returns>
        public static DateTime GetRandomTime(DateTime time1, DateTime time2)
        {
            Random random = new Random();
            DateTime minTime = new DateTime();
            DateTime maxTime = new DateTime();

            System.TimeSpan ts = new System.TimeSpan(time1.Ticks - time2.Ticks);

            // 获取两个时间相隔的秒数
            double dTotalSecontds = ts.TotalSeconds;
            int iTotalSecontds = 0;

            if (dTotalSecontds > System.Int32.MaxValue)
            {
                iTotalSecontds = System.Int32.MaxValue;
            }
            else if (dTotalSecontds < System.Int32.MinValue)
            {
                iTotalSecontds = System.Int32.MinValue;
            }
            else
            {
                iTotalSecontds = (int)dTotalSecontds;
            }


            if (iTotalSecontds > 0)
            {
                minTime = time2;
                maxTime = time1;
            }
            else if (iTotalSecontds < 0)
            {
                minTime = time1;
                maxTime = time2;
            }
            else
            {
                return time1;
            }

            int maxValue = iTotalSecontds;

            if (iTotalSecontds <= System.Int32.MinValue)
                maxValue = System.Int32.MinValue + 1;

            int i = random.Next(System.Math.Abs(maxValue));

            return minTime.AddSeconds(i);
        }
        #endregion
    }

    public class TimeParser
    {
        /// <summary>
        /// 把秒转换成分钟
        /// </summary>
        /// <returns></returns>
        public static int SecondToMinute(int Second)
        {
            decimal mm = (decimal)((decimal)Second / (decimal)60);
            return Convert.ToInt32(Math.Ceiling(mm));
        }

        #region 返回某年某月最后一天
        /// <summary>
        /// 返回某年某月最后一天
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>日</returns>
        public static int GetMonthLastDate(int year, int month)
        {
            DateTime lastDay = new DateTime(year, month, new System.Globalization.GregorianCalendar().GetDaysInMonth(year, month));
            int Day = lastDay.Day;
            return Day;
        }
        #endregion

        #region 返回时间差
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            try
            {
                //TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
                //TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
                //TimeSpan ts = ts1.Subtract(ts2).Duration();
                TimeSpan ts = DateTime2 - DateTime1;
                if (ts.Days >= 1)
                {
                    dateDiff = DateTime1.Month.ToString() + "月" + DateTime1.Day.ToString() + "日";
                }
                else
                {
                    if (ts.Hours > 1)
                    {
                        dateDiff = ts.Hours.ToString() + "小时前";
                    }
                    else
                    {
                        dateDiff = ts.Minutes.ToString() + "分钟前";
                    }
                }
            }
            catch
            { }
            return dateDiff;
        }
        #endregion
    }
    #endregion

    #region Excel-N
    /// <summary>
    /// 操作EXCEL导出数据报表的类
    /// </summary>
    //public class DataToExcel
    //{
    //    public DataToExcel()
    //    {
    //    }

    //    #region 操作EXCEL的一个类(需要Excel.dll支持)

    //    private int titleColorindex = 15;
    //    /// <summary>
    //    /// 标题背景色
    //    /// </summary>
    //    public int TitleColorIndex
    //    {
    //        set { titleColorindex = value; }
    //        get { return titleColorindex; }
    //    }

    //    private DateTime beforeTime;			//Excel启动之前时间
    //    private DateTime afterTime;				//Excel启动之后时间

    //    #region 创建一个Excel示例
    //    /// <summary>
    //    /// 创建一个Excel示例
    //    /// </summary>
    //    public void CreateExcel()
    //    {
    //        //Excel.Application excel = new Excel.Application();
    //        //excel.Application.Workbooks.Add(true);
    //        //excel.Cells[1, 1] = "第1行第1列";
    //        //excel.Cells[1, 2] = "第1行第2列";
    //        //excel.Cells[2, 1] = "第2行第1列";
    //        //excel.Cells[2, 2] = "第2行第2列";
    //        //excel.Cells[3, 1] = "第3行第1列";
    //        //excel.Cells[3, 2] = "第3行第2列";

    //        ////保存
    //        //excel.ActiveWorkbook.SaveAs("./tt.xls", XlFileFormat.xlExcel9795, null, null, false, false, Excel.XlSaveAsAccessMode.xlNoChange, null, null, null, null, null);
    //        ////打开显示
    //        //excel.Visible = true;
    //        ////			excel.Quit();
    //        ////			excel=null;            
    //        ////			GC.Collect();//垃圾回收
    //    }
    //    #endregion

    //    #region 将DataTable的数据导出显示为报表
    //    /// <summary>
    //    /// 将DataTable的数据导出显示为报表
    //    /// </summary>
    //    /// <param name="dt">要导出的数据</param>
    //    /// <param name="strTitle">导出报表的标题</param>
    //    /// <param name="FilePath">保存文件的路径</param>
    //    /// <returns></returns>
    //    //public string OutputExcel(System.Data.DataTable dt, string strTitle, string FilePath)
    //    //{
    //    //    beforeTime = DateTime.Now;

    //    //    Excel.Application excel;
    //    //    Excel._Workbook xBk;
    //    //    Excel._Worksheet xSt;

    //    //    int rowIndex = 4;
    //    //    int colIndex = 1;

    //    //    excel = new Excel.ApplicationClass();
    //    //    xBk = excel.Workbooks.Add(true);
    //    //    xSt = (Excel._Worksheet)xBk.ActiveSheet;

    //    //    //取得列标题			
    //    //    foreach (DataColumn col in dt.Columns)
    //    //    {
    //    //        colIndex++;
    //    //        excel.Cells[4, colIndex] = col.ColumnName;

    //    //        //设置标题格式为居中对齐
    //    //        xSt.get_Range(excel.Cells[4, colIndex], excel.Cells[4, colIndex]).Font.Bold = true;
    //    //        xSt.get_Range(excel.Cells[4, colIndex], excel.Cells[4, colIndex]).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;
    //    //        xSt.get_Range(excel.Cells[4, colIndex], excel.Cells[4, colIndex]).Select();
    //    //        xSt.get_Range(excel.Cells[4, colIndex], excel.Cells[4, colIndex]).Interior.ColorIndex = titleColorindex;//19;//设置为浅黄色，共计有56种
    //    //    }


    //    //    //取得表格中的数据			
    //    //    foreach (DataRow row in dt.Rows)
    //    //    {
    //    //        rowIndex++;
    //    //        colIndex = 1;
    //    //        foreach (DataColumn col in dt.Columns)
    //    //        {
    //    //            colIndex++;
    //    //            if (col.DataType == System.Type.GetType("System.DateTime"))
    //    //            {
    //    //                excel.Cells[rowIndex, colIndex] = (Convert.ToDateTime(row[col.ColumnName].ToString())).ToString("yyyy-MM-dd");
    //    //                xSt.get_Range(excel.Cells[rowIndex, colIndex], excel.Cells[rowIndex, colIndex]).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;//设置日期型的字段格式为居中对齐
    //    //            }
    //    //            else
    //    //                if (col.DataType == System.Type.GetType("System.String"))
    //    //                {
    //    //                    excel.Cells[rowIndex, colIndex] = "'" + row[col.ColumnName].ToString();
    //    //                    xSt.get_Range(excel.Cells[rowIndex, colIndex], excel.Cells[rowIndex, colIndex]).HorizontalAlignment = Excel.XlVAlign.xlVAlignCenter;//设置字符型的字段格式为居中对齐
    //    //                }
    //    //                else
    //    //                {
    //    //                    excel.Cells[rowIndex, colIndex] = row[col.ColumnName].ToString();
    //    //                }
    //    //        }
    //    //    }

    //    //    //加载一个合计行			
    //    //    int rowSum = rowIndex + 1;
    //    //    int colSum = 2;
    //    //    excel.Cells[rowSum, 2] = "合计";
    //    //    xSt.get_Range(excel.Cells[rowSum, 2], excel.Cells[rowSum, 2]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
    //    //    //设置选中的部分的颜色			
    //    //    xSt.get_Range(excel.Cells[rowSum, colSum], excel.Cells[rowSum, colIndex]).Select();
    //    //    //xSt.get_Range(excel.Cells[rowSum,colSum],excel.Cells[rowSum,colIndex]).Interior.ColorIndex =Assistant.GetConfigInt("ColorIndex");// 1;//设置为浅黄色，共计有56种

    //    //    //取得整个报表的标题			
    //    //    excel.Cells[2, 2] = strTitle;

    //    //    //设置整个报表的标题格式			
    //    //    xSt.get_Range(excel.Cells[2, 2], excel.Cells[2, 2]).Font.Bold = true;
    //    //    xSt.get_Range(excel.Cells[2, 2], excel.Cells[2, 2]).Font.Size = 22;

    //    //    //设置报表表格为最适应宽度			
    //    //    xSt.get_Range(excel.Cells[4, 2], excel.Cells[rowSum, colIndex]).Select();
    //    //    xSt.get_Range(excel.Cells[4, 2], excel.Cells[rowSum, colIndex]).Columns.AutoFit();

    //    //    //设置整个报表的标题为跨列居中			
    //    //    xSt.get_Range(excel.Cells[2, 2], excel.Cells[2, colIndex]).Select();
    //    //    xSt.get_Range(excel.Cells[2, 2], excel.Cells[2, colIndex]).HorizontalAlignment = Excel.XlHAlign.xlHAlignCenterAcrossSelection;

    //    //    //绘制边框			
    //    //    xSt.get_Range(excel.Cells[4, 2], excel.Cells[rowSum, colIndex]).Borders.LineStyle = 1;
    //    //    xSt.get_Range(excel.Cells[4, 2], excel.Cells[rowSum, 2]).Borders[Excel.XlBordersIndex.xlEdgeLeft].Weight = Excel.XlBorderWeight.xlThick;//设置左边线加粗
    //    //    xSt.get_Range(excel.Cells[4, 2], excel.Cells[4, colIndex]).Borders[Excel.XlBordersIndex.xlEdgeTop].Weight = Excel.XlBorderWeight.xlThick;//设置上边线加粗
    //    //    xSt.get_Range(excel.Cells[4, colIndex], excel.Cells[rowSum, colIndex]).Borders[Excel.XlBordersIndex.xlEdgeRight].Weight = Excel.XlBorderWeight.xlThick;//设置右边线加粗
    //    //    xSt.get_Range(excel.Cells[rowSum, 2], excel.Cells[rowSum, colIndex]).Borders[Excel.XlBordersIndex.xlEdgeBottom].Weight = Excel.XlBorderWeight.xlThick;//设置下边线加粗



    //    //    afterTime = DateTime.Now;

    //    //    //显示效果			
    //    //    //excel.Visible=true;			
    //    //    //excel.Sheets[0] = "sss";

    //    //    ClearFile(FilePath);
    //    //    string filename = DateTime.Now.ToString("yyyyMMddHHmmssff") + ".xls";
    //    //    excel.ActiveWorkbook.SaveAs(FilePath + filename, Excel.XlFileFormat.xlExcel9795, null, null, false, false, Excel.XlSaveAsAccessMode.xlNoChange, null, null, null, null, null);

    //    //    //wkbNew.SaveAs strBookName;
    //    //    //excel.Save(strExcelFileName);

    //    //    #region  结束Excel进程

    //    //    //需要对Excel的DCOM对象进行配置:dcomcnfg


    //    //    //excel.Quit();
    //    //    //excel=null;            

    //    //    xBk.Close(null, null, null);
    //    //    excel.Workbooks.Close();
    //    //    excel.Quit();


    //    //    //注意：这里用到的所有Excel对象都要执行这个操作，否则结束不了Excel进程
    //    //    //			if(rng != null)
    //    //    //			{
    //    //    //				System.Runtime.InteropServices.Marshal.ReleaseComObject(rng);
    //    //    //				rng = null;
    //    //    //			}
    //    //    //			if(tb != null)
    //    //    //			{
    //    //    //				System.Runtime.InteropServices.Marshal.ReleaseComObject(tb);
    //    //    //				tb = null;
    //    //    //			}
    //    //    if (xSt != null)
    //    //    {
    //    //        System.Runtime.InteropServices.Marshal.ReleaseComObject(xSt);
    //    //        xSt = null;
    //    //    }
    //    //    if (xBk != null)
    //    //    {
    //    //        System.Runtime.InteropServices.Marshal.ReleaseComObject(xBk);
    //    //        xBk = null;
    //    //    }
    //    //    if (excel != null)
    //    //    {
    //    //        System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);
    //    //        excel = null;
    //    //    }
    //    //    GC.Collect();//垃圾回收
    //    //    #endregion

    //    //    return filename;

    //    //}
    //    #endregion

    //    #region Kill Excel进程

    //    /// <summary>
    //    /// 结束Excel进程
    //    /// </summary>
    //    public void KillExcelProcess()
    //    {
    //        Process[] myProcesses;
    //        DateTime startTime;
    //        myProcesses = Process.GetProcessesByName("Excel");

    //        //得不到Excel进程ID，暂时只能判断进程启动时间
    //        foreach (Process myProcess in myProcesses)
    //        {
    //            startTime = myProcess.StartTime;
    //            if (startTime > beforeTime && startTime < afterTime)
    //            {
    //                myProcess.Kill();
    //            }
    //        }
    //    }
    //    #endregion

    //    #endregion

    //    #region 将DataTable的数据导出显示为报表(不使用Excel对象，使用COM.Excel)

    //    #region 使用示例
    //    /*使用示例：
    //     * DataSet ds=(DataSet)Session["AdBrowseHitDayList"];
    //        string ExcelFolder=Assistant.GetConfigString("ExcelFolder");
    //        string FilePath=Server.MapPath(".")+"\\"+ExcelFolder+"\\";
			
    //        //生成列的中文对应表
    //        Hashtable nameList = new Hashtable();
    //        nameList.Add("ADID", "广告编码");
    //        nameList.Add("ADName", "广告名称");
    //        nameList.Add("year", "年");
    //        nameList.Add("month", "月");
    //        nameList.Add("browsum", "显示数");
    //        nameList.Add("hitsum", "点击数");
    //        nameList.Add("BrowsinglIP", "独立IP显示");
    //        nameList.Add("HitsinglIP", "独立IP点击");
    //        //利用excel对象
    //        DataToExcel dte=new DataToExcel();
    //        string filename="";
    //        try
    //        {			
    //            if(ds.Tables[0].Rows.Count>0)
    //            {
    //                filename=dte.DataExcel(ds.Tables[0],"标题",FilePath,nameList);
    //            }
    //        }
    //        catch
    //        {
    //            //dte.KillExcelProcess();
    //        }
			
    //        if(filename!="")
    //        {
    //            Response.Redirect(ExcelFolder+"\\"+filename,true);
    //        }
    //     * 
    //     * */

    //    #endregion

    //    /// <summary>
    //    /// 将DataTable的数据导出显示为报表(不使用Excel对象)
    //    /// </summary>
    //    /// <param name="dt">数据DataTable</param>
    //    /// <param name="strTitle">标题</param>
    //    /// <param name="FilePath">生成文件的路径</param>
    //    /// <param name="nameList"></param>
    //    /// <returns></returns>
    //    //public string DataExcel(System.Data.DataTable dt, string strTitle, string FilePath, Hashtable nameList)
    //    //{
    //    //    COM.Excel.cExcelFile excel = new COM.Excel.cExcelFile();
    //    //    ClearFile(FilePath);
    //    //    string filename = DateTime.Now.ToString("yyyyMMddHHmmssff") + ".xls";
    //    //    excel.CreateFile(FilePath + filename);
    //    //    excel.PrintGridLines = false;

    //    //    COM.Excel.cExcelFile.MarginTypes mt1 = COM.Excel.cExcelFile.MarginTypes.xlsTopMargin;
    //    //    COM.Excel.cExcelFile.MarginTypes mt2 = COM.Excel.cExcelFile.MarginTypes.xlsLeftMargin;
    //    //    COM.Excel.cExcelFile.MarginTypes mt3 = COM.Excel.cExcelFile.MarginTypes.xlsRightMargin;
    //    //    COM.Excel.cExcelFile.MarginTypes mt4 = COM.Excel.cExcelFile.MarginTypes.xlsBottomMargin;

    //    //    double height = 1.5;
    //    //    excel.SetMargin(ref mt1, ref height);
    //    //    excel.SetMargin(ref mt2, ref height);
    //    //    excel.SetMargin(ref mt3, ref height);
    //    //    excel.SetMargin(ref mt4, ref height);

    //    //    COM.Excel.cExcelFile.FontFormatting ff = COM.Excel.cExcelFile.FontFormatting.xlsNoFormat;
    //    //    string font = "宋体";
    //    //    short fontsize = 9;
    //    //    excel.SetFont(ref font, ref fontsize, ref ff);

    //    //    byte b1 = 1,
    //    //        b2 = 12;
    //    //    short s3 = 12;
    //    //    excel.SetColumnWidth(ref b1, ref b2, ref s3);

    //    //    string header = "页眉";
    //    //    string footer = "页脚";
    //    //    excel.SetHeader(ref header);
    //    //    excel.SetFooter(ref footer);


    //    //    COM.Excel.cExcelFile.ValueTypes vt = COM.Excel.cExcelFile.ValueTypes.xlsText;
    //    //    COM.Excel.cExcelFile.CellFont cf = COM.Excel.cExcelFile.CellFont.xlsFont0;
    //    //    COM.Excel.cExcelFile.CellAlignment ca = COM.Excel.cExcelFile.CellAlignment.xlsCentreAlign;
    //    //    COM.Excel.cExcelFile.CellHiddenLocked chl = COM.Excel.cExcelFile.CellHiddenLocked.xlsNormal;

    //    //    // 报表标题
    //    //    int cellformat = 1;
    //    //    //			int rowindex = 1,colindex = 3;					
    //    //    //			object title = (object)strTitle;
    //    //    //			excel.WriteValue(ref vt, ref cf, ref ca, ref chl,ref rowindex,ref colindex,ref title,ref cellformat);

    //    //    int rowIndex = 1;//起始行
    //    //    int colIndex = 0;



    //    //    //取得列标题				
    //    //    foreach (DataColumn colhead in dt.Columns)
    //    //    {
    //    //        colIndex++;
    //    //        string name = colhead.ColumnName.Trim();
    //    //        object namestr = (object)name;
    //    //        IDictionaryEnumerator Enum = nameList.GetEnumerator();
    //    //        while (Enum.MoveNext())
    //    //        {
    //    //            if (Enum.Key.ToString().Trim() == name)
    //    //            {
    //    //                namestr = Enum.Value;
    //    //            }
    //    //        }
    //    //        excel.WriteValue(ref vt, ref cf, ref ca, ref chl, ref rowIndex, ref colIndex, ref namestr, ref cellformat);
    //    //    }

    //    //    //取得表格中的数据			
    //    //    foreach (DataRow row in dt.Rows)
    //    //    {
    //    //        rowIndex++;
    //    //        colIndex = 0;
    //    //        foreach (DataColumn col in dt.Columns)
    //    //        {
    //    //            colIndex++;
    //    //            if (col.DataType == System.Type.GetType("System.DateTime"))
    //    //            {
    //    //                object str = (object)(Convert.ToDateTime(row[col.ColumnName].ToString())).ToString("yyyy-MM-dd"); ;
    //    //                excel.WriteValue(ref vt, ref cf, ref ca, ref chl, ref rowIndex, ref colIndex, ref str, ref cellformat);
    //    //            }
    //    //            else
    //    //            {
    //    //                object str = (object)row[col.ColumnName].ToString();
    //    //                excel.WriteValue(ref vt, ref cf, ref ca, ref chl, ref rowIndex, ref colIndex, ref str, ref cellformat);
    //    //            }
    //    //        }
    //    //    }
    //    //    int ret = excel.CloseFile();

    //    //    //			if(ret!=0)
    //    //    //			{
    //    //    //				//MessageBox.Show(this,"Error!");
    //    //    //			}
    //    //    //			else
    //    //    //			{
    //    //    //				//MessageBox.Show(this,"请打开文件c:\\test.xls!");
    //    //    //			}
    //    //    return filename;

    //    //}

    //    #endregion

    //    #region  清理过时的Excel文件

    //    private void ClearFile(string FilePath)
    //    {
    //        String[] Files = System.IO.Directory.GetFiles(FilePath);
    //        if (Files.Length > 10)
    //        {
    //            for (int i = 0; i < 10; i++)
    //            {
    //                try
    //                {
    //                    System.IO.File.Delete(Files[i]);
    //                }
    //                catch
    //                {
    //                }

    //            }
    //        }
    //    }
    //    #endregion

    //}

    /// <summary>
    /// Excel操作类
    /// </summary>
    /// Microsoft Excel 11.0 Object Library
    //public class ExcelHelper
    //{
    //    #region 数据导出至Excel文件
    //    /// </summary> 
    //    /// 导出Excel文件，自动返回可下载的文件流 
    //    /// </summary> 
    //    public static void DataTable1Excel(System.Data.DataTable dtData)
    //    {
    //        GridView gvExport = null;
    //        HttpContext curContext = HttpContext.Current;
    //        StringWriter strWriter = null;
    //        HtmlTextWriter htmlWriter = null;
    //        if (dtData != null)
    //        {
    //            curContext.Response.ContentType = "application/vnd.ms-excel";
    //            curContext.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
    //            curContext.Response.Charset = "utf-8";
    //            strWriter = new StringWriter();
    //            htmlWriter = new HtmlTextWriter(strWriter);
    //            gvExport = new GridView();
    //            gvExport.DataSource = dtData.DefaultView;
    //            gvExport.AllowPaging = false;
    //            gvExport.DataBind();
    //            gvExport.RenderControl(htmlWriter);
    //            curContext.Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html;charset=gb2312\"/>" + strWriter.ToString());
    //            curContext.Response.End();
    //        }
    //    }

    //    /// <summary>
    //    /// 导出Excel文件，转换为可读模式
    //    /// </summary>
    //    public static void DataTable2Excel(System.Data.DataTable dtData)
    //    {
    //        DataGrid dgExport = null;
    //        HttpContext curContext = HttpContext.Current;
    //        StringWriter strWriter = null;
    //        HtmlTextWriter htmlWriter = null;

    //        if (dtData != null)
    //        {
    //            curContext.Response.ContentType = "application/vnd.ms-excel";
    //            curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
    //            curContext.Response.Charset = "";
    //            strWriter = new StringWriter();
    //            htmlWriter = new HtmlTextWriter(strWriter);
    //            dgExport = new DataGrid();
    //            dgExport.DataSource = dtData.DefaultView;
    //            dgExport.AllowPaging = false;
    //            dgExport.DataBind();
    //            dgExport.RenderControl(htmlWriter);
    //            curContext.Response.Write(strWriter.ToString());
    //            curContext.Response.End();
    //        }
    //    }

    //    /// <summary>
    //    /// 导出Excel文件，并自定义文件名
    //    /// </summary>
    //    public static void DataTable3Excel(System.Data.DataTable dtData, String FileName)
    //    {
    //        GridView dgExport = null;
    //        HttpContext curContext = HttpContext.Current;
    //        StringWriter strWriter = null;
    //        HtmlTextWriter htmlWriter = null;

    //        if (dtData != null)
    //        {
    //            HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8);
    //            curContext.Response.AddHeader("content-disposition", "attachment;filename=" + HttpUtility.UrlEncode(FileName, System.Text.Encoding.UTF8) + ".xls");
    //            curContext.Response.ContentType = "application nd.ms-excel";
    //            curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
    //            curContext.Response.Charset = "GB2312";
    //            strWriter = new StringWriter();
    //            htmlWriter = new HtmlTextWriter(strWriter);
    //            dgExport = new GridView();
    //            dgExport.DataSource = dtData.DefaultView;
    //            dgExport.AllowPaging = false;
    //            dgExport.DataBind();
    //            dgExport.RenderControl(htmlWriter);
    //            curContext.Response.Write(strWriter.ToString());
    //            curContext.Response.End();
    //        }
    //    }

    //    /// <summary>
    //    /// 将数据导出至Excel文件
    //    /// </summary>
    //    /// <param name="Table">DataTable对象</param>
    //    /// <param name="ExcelFilePath">Excel文件路径</param>
    //    public static bool OutputToExcel(DataTable Table, string ExcelFilePath)
    //    {
    //        if (File.Exists(ExcelFilePath))
    //        {
    //            throw new Exception("该文件已经存在！");
    //        }

    //        if ((Table.TableName.Trim().Length == 0) || (Table.TableName.ToLower() == "table"))
    //        {
    //            Table.TableName = "Sheet1";
    //        }

    //        //数据表的列数
    //        int ColCount = Table.Columns.Count;

    //        //用于记数，实例化参数时的序号
    //        int i = 0;

    //        //创建参数
    //        OleDbParameter[] para = new OleDbParameter[ColCount];

    //        //创建表结构的SQL语句
    //        string TableStructStr = @"Create Table " + Table.TableName + "(";

    //        //连接字符串
    //        string connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelFilePath + ";Extended Properties=Excel 8.0;";
    //        OleDbConnection objConn = new OleDbConnection(connString);

    //        //创建表结构
    //        OleDbCommand objCmd = new OleDbCommand();

    //        //数据类型集合
    //        ArrayList DataTypeList = new ArrayList();
    //        DataTypeList.Add("System.Decimal");
    //        DataTypeList.Add("System.Double");
    //        DataTypeList.Add("System.Int16");
    //        DataTypeList.Add("System.Int32");
    //        DataTypeList.Add("System.Int64");
    //        DataTypeList.Add("System.Single");

    //        //遍历数据表的所有列，用于创建表结构
    //        foreach (DataColumn col in Table.Columns)
    //        {
    //            //如果列属于数字列，则设置该列的数据类型为double
    //            if (DataTypeList.IndexOf(col.DataType.ToString()) >= 0)
    //            {
    //                para[i] = new OleDbParameter("@" + col.ColumnName, OleDbType.Double);
    //                objCmd.Parameters.Add(para[i]);

    //                //如果是最后一列
    //                if (i + 1 == ColCount)
    //                {
    //                    TableStructStr += col.ColumnName + " double)";
    //                }
    //                else
    //                {
    //                    TableStructStr += col.ColumnName + " double,";
    //                }
    //            }
    //            else
    //            {
    //                para[i] = new OleDbParameter("@" + col.ColumnName, OleDbType.VarChar);
    //                objCmd.Parameters.Add(para[i]);

    //                //如果是最后一列
    //                if (i + 1 == ColCount)
    //                {
    //                    TableStructStr += col.ColumnName + " varchar)";
    //                }
    //                else
    //                {
    //                    TableStructStr += col.ColumnName + " varchar,";
    //                }
    //            }
    //            i++;
    //        }

    //        //创建Excel文件及文件结构
    //        try
    //        {
    //            objCmd.Connection = objConn;
    //            objCmd.CommandText = TableStructStr;

    //            if (objConn.State == ConnectionState.Closed)
    //            {
    //                objConn.Open();
    //            }
    //            objCmd.ExecuteNonQuery();
    //        }
    //        catch (Exception exp)
    //        {
    //            throw exp;
    //        }

    //        //插入记录的SQL语句
    //        string InsertSql_1 = "Insert into " + Table.TableName + " (";
    //        string InsertSql_2 = " Values (";
    //        string InsertSql = "";

    //        //遍历所有列，用于插入记录，在此创建插入记录的SQL语句
    //        for (int colID = 0; colID < ColCount; colID++)
    //        {
    //            if (colID + 1 == ColCount)  //最后一列
    //            {
    //                InsertSql_1 += Table.Columns[colID].ColumnName + ")";
    //                InsertSql_2 += "@" + Table.Columns[colID].ColumnName + ")";
    //            }
    //            else
    //            {
    //                InsertSql_1 += Table.Columns[colID].ColumnName + ",";
    //                InsertSql_2 += "@" + Table.Columns[colID].ColumnName + ",";
    //            }
    //        }

    //        InsertSql = InsertSql_1 + InsertSql_2;

    //        //遍历数据表的所有数据行
    //        for (int rowID = 0; rowID < Table.Rows.Count; rowID++)
    //        {
    //            for (int colID = 0; colID < ColCount; colID++)
    //            {
    //                if (para[colID].DbType == DbType.Double && Table.Rows[rowID][colID].ToString().Trim() == "")
    //                {
    //                    para[colID].Value = 0;
    //                }
    //                else
    //                {
    //                    para[colID].Value = Table.Rows[rowID][colID].ToString().Trim();
    //                }
    //            }
    //            try
    //            {
    //                objCmd.CommandText = InsertSql;
    //                objCmd.ExecuteNonQuery();
    //            }
    //            catch (Exception exp)
    //            {
    //                string str = exp.Message;
    //            }
    //        }
    //        try
    //        {
    //            if (objConn.State == ConnectionState.Open)
    //            {
    //                objConn.Close();
    //            }
    //        }
    //        catch (Exception exp)
    //        {
    //            throw exp;
    //        }
    //        return true;
    //    }

    //    /// <summary>
    //    /// 将数据导出至Excel文件
    //    /// </summary>
    //    /// <param name="Table">DataTable对象</param>
    //    /// <param name="Columns">要导出的数据列集合</param>
    //    /// <param name="ExcelFilePath">Excel文件路径</param>
    //    public static bool OutputToExcel(DataTable Table, ArrayList Columns, string ExcelFilePath)
    //    {
    //        if (File.Exists(ExcelFilePath))
    //        {
    //            throw new Exception("该文件已经存在！");
    //        }

    //        //如果数据列数大于表的列数，取数据表的所有列
    //        if (Columns.Count > Table.Columns.Count)
    //        {
    //            for (int s = Table.Columns.Count + 1; s <= Columns.Count; s++)
    //            {
    //                Columns.RemoveAt(s);   //移除数据表列数后的所有列
    //            }
    //        }

    //        //遍历所有的数据列，如果有数据列的数据类型不是 DataColumn，则将它移除
    //        DataColumn column = new DataColumn();
    //        for (int j = 0; j < Columns.Count; j++)
    //        {
    //            try
    //            {
    //                column = (DataColumn)Columns[j];
    //            }
    //            catch (Exception)
    //            {
    //                Columns.RemoveAt(j);
    //            }
    //        }
    //        if ((Table.TableName.Trim().Length == 0) || (Table.TableName.ToLower() == "table"))
    //        {
    //            Table.TableName = "Sheet1";
    //        }

    //        //数据表的列数
    //        int ColCount = Columns.Count;

    //        //创建参数
    //        OleDbParameter[] para = new OleDbParameter[ColCount];

    //        //创建表结构的SQL语句
    //        string TableStructStr = @"Create Table " + Table.TableName + "(";

    //        //连接字符串
    //        string connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelFilePath + ";Extended Properties=Excel 8.0;";
    //        OleDbConnection objConn = new OleDbConnection(connString);

    //        //创建表结构
    //        OleDbCommand objCmd = new OleDbCommand();

    //        //数据类型集合
    //        ArrayList DataTypeList = new ArrayList();
    //        DataTypeList.Add("System.Decimal");
    //        DataTypeList.Add("System.Double");
    //        DataTypeList.Add("System.Int16");
    //        DataTypeList.Add("System.Int32");
    //        DataTypeList.Add("System.Int64");
    //        DataTypeList.Add("System.Single");

    //        DataColumn col = new DataColumn();

    //        //遍历数据表的所有列，用于创建表结构
    //        for (int k = 0; k < ColCount; k++)
    //        {
    //            col = (DataColumn)Columns[k];

    //            //列的数据类型是数字型
    //            if (DataTypeList.IndexOf(col.DataType.ToString().Trim()) >= 0)
    //            {
    //                para[k] = new OleDbParameter("@" + col.Caption.Trim(), OleDbType.Double);
    //                objCmd.Parameters.Add(para[k]);

    //                //如果是最后一列
    //                if (k + 1 == ColCount)
    //                {
    //                    TableStructStr += col.Caption.Trim() + " Double)";
    //                }
    //                else
    //                {
    //                    TableStructStr += col.Caption.Trim() + " Double,";
    //                }
    //            }
    //            else
    //            {
    //                para[k] = new OleDbParameter("@" + col.Caption.Trim(), OleDbType.VarChar);
    //                objCmd.Parameters.Add(para[k]);

    //                //如果是最后一列
    //                if (k + 1 == ColCount)
    //                {
    //                    TableStructStr += col.Caption.Trim() + " VarChar)";
    //                }
    //                else
    //                {
    //                    TableStructStr += col.Caption.Trim() + " VarChar,";
    //                }
    //            }
    //        }

    //        //创建Excel文件及文件结构
    //        try
    //        {
    //            objCmd.Connection = objConn;
    //            objCmd.CommandText = TableStructStr;

    //            if (objConn.State == ConnectionState.Closed)
    //            {
    //                objConn.Open();
    //            }
    //            objCmd.ExecuteNonQuery();
    //        }
    //        catch (Exception exp)
    //        {
    //            throw exp;
    //        }

    //        //插入记录的SQL语句
    //        string InsertSql_1 = "Insert into " + Table.TableName + " (";
    //        string InsertSql_2 = " Values (";
    //        string InsertSql = "";

    //        //遍历所有列，用于插入记录，在此创建插入记录的SQL语句
    //        for (int colID = 0; colID < ColCount; colID++)
    //        {
    //            if (colID + 1 == ColCount)  //最后一列
    //            {
    //                InsertSql_1 += Columns[colID].ToString().Trim() + ")";
    //                InsertSql_2 += "@" + Columns[colID].ToString().Trim() + ")";
    //            }
    //            else
    //            {
    //                InsertSql_1 += Columns[colID].ToString().Trim() + ",";
    //                InsertSql_2 += "@" + Columns[colID].ToString().Trim() + ",";
    //            }
    //        }

    //        InsertSql = InsertSql_1 + InsertSql_2;

    //        //遍历数据表的所有数据行
    //        DataColumn DataCol = new DataColumn();
    //        for (int rowID = 0; rowID < Table.Rows.Count; rowID++)
    //        {
    //            for (int colID = 0; colID < ColCount; colID++)
    //            {
    //                //因为列不连续，所以在取得单元格时不能用行列编号，列需得用列的名称
    //                DataCol = (DataColumn)Columns[colID];
    //                if (para[colID].DbType == DbType.Double && Table.Rows[rowID][DataCol.Caption].ToString().Trim() == "")
    //                {
    //                    para[colID].Value = 0;
    //                }
    //                else
    //                {
    //                    para[colID].Value = Table.Rows[rowID][DataCol.Caption].ToString().Trim();
    //                }
    //            }
    //            try
    //            {
    //                objCmd.CommandText = InsertSql;
    //                objCmd.ExecuteNonQuery();
    //            }
    //            catch (Exception exp)
    //            {
    //                string str = exp.Message;
    //            }
    //        }
    //        try
    //        {
    //            if (objConn.State == ConnectionState.Open)
    //            {
    //                objConn.Close();
    //            }
    //        }
    //        catch (Exception exp)
    //        {
    //            throw exp;
    //        }
    //        return true;
    //    }
    //    #endregion

    //    /// <summary>
    //    /// 获取Excel文件数据表列表
    //    /// </summary>
    //    public static ArrayList GetExcelTables(string ExcelFileName)
    //    {
    //        DataTable dt = new DataTable();
    //        ArrayList TablesList = new ArrayList();
    //        if (File.Exists(ExcelFileName))
    //        {
    //            using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" + ExcelFileName))
    //            {
    //                try
    //                {
    //                    conn.Open();
    //                    dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
    //                }
    //                catch (Exception exp)
    //                {
    //                    throw exp;
    //                }

    //                //获取数据表个数
    //                int tablecount = dt.Rows.Count;
    //                for (int i = 0; i < tablecount; i++)
    //                {
    //                    string tablename = dt.Rows[i][2].ToString().Trim().TrimEnd('$');
    //                    if (TablesList.IndexOf(tablename) < 0)
    //                    {
    //                        TablesList.Add(tablename);
    //                    }
    //                }
    //            }
    //        }
    //        return TablesList;
    //    }

    //    /// <summary>
    //    /// 将Excel文件导出至DataTable(第一行作为表头)
    //    /// </summary>
    //    /// <param name="ExcelFilePath">Excel文件路径</param>
    //    /// <param name="TableName">数据表名，如果数据表名错误，默认为第一个数据表名</param>
    //    public static DataTable InputFromExcel(string ExcelFilePath, string TableName)
    //    {
    //        if (!File.Exists(ExcelFilePath))
    //        {
    //            throw new Exception("Excel文件不存在！");
    //        }

    //        //如果数据表名不存在，则数据表名为Excel文件的第一个数据表
    //        ArrayList TableList = new ArrayList();
    //        TableList = GetExcelTables(ExcelFilePath);

    //        if (TableName.IndexOf(TableName) < 0)
    //        {
    //            TableName = TableList[0].ToString().Trim();
    //        }

    //        DataTable table = new DataTable();
    //        OleDbConnection dbcon = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelFilePath + ";Extended Properties=Excel 8.0");
    //        OleDbCommand cmd = new OleDbCommand("select * from [" + TableName + "$]", dbcon);
    //        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

    //        try
    //        {
    //            if (dbcon.State == ConnectionState.Closed)
    //            {
    //                dbcon.Open();
    //            }
    //            adapter.Fill(table);
    //        }
    //        catch (Exception exp)
    //        {
    //            throw exp;
    //        }
    //        finally
    //        {
    //            if (dbcon.State == ConnectionState.Open)
    //            {
    //                dbcon.Close();
    //            }
    //        }
    //        return table;
    //    }

    //    /// <summary>
    //    /// 获取Excel文件指定数据表的数据列表
    //    /// </summary>
    //    /// <param name="ExcelFileName">Excel文件名</param>
    //    /// <param name="TableName">数据表名</param>
    //    public static ArrayList GetExcelTableColumns(string ExcelFileName, string TableName)
    //    {
    //        DataTable dt = new DataTable();
    //        ArrayList ColsList = new ArrayList();
    //        if (File.Exists(ExcelFileName))
    //        {
    //            using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" + ExcelFileName))
    //            {
    //                conn.Open();
    //                dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, TableName, null });

    //                //获取列个数
    //                int colcount = dt.Rows.Count;
    //                for (int i = 0; i < colcount; i++)
    //                {
    //                    string colname = dt.Rows[i]["Column_Name"].ToString().Trim();
    //                    ColsList.Add(colname);
    //                }
    //            }
    //        }
    //        return ColsList;
    //    }
    //}

    //public class ExportExcel
    //{

    //    protected void ExportData(string strContent, string FileName)
    //    {

    //        FileName = FileName + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

    //        HttpContext.Current.Response.Clear();
    //        HttpContext.Current.Response.Charset = "gb2312";
    //        HttpContext.Current.Response.ContentType = "application/ms-excel";
    //        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
    //        //this.Page.EnableViewState = false; 
    //        // 添加头信息，为"文件下载/另存为"对话框指定默认文件名 
    //        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName + ".xls");
    //        // 把文件流发送到客户端 
    //        HttpContext.Current.Response.Write("<html><head><meta http-equiv=Content-Type content=\"text/html; charset=utf-8\">");
    //        HttpContext.Current.Response.Write(strContent);
    //        HttpContext.Current.Response.Write("</body></html>");
    //        // 停止页面的执行 
    //        //Response.End();
    //    }

    //    /// <summary>
    //    /// 导出Excel
    //    /// </summary>
    //    /// <param name="obj"></param>
    //    public void ExportData(GridView obj)
    //    {
    //        try
    //        {
    //            string style = "";
    //            if (obj.Rows.Count > 0)
    //            {
    //                style = @"<style> .text { mso-number-format:\@; } </script> ";
    //            }
    //            else
    //            {
    //                style = "no data.";
    //            }

    //            HttpContext.Current.Response.ClearContent();
    //            DateTime dt = DateTime.Now;
    //            string filename = dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();
    //            HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=ExportData" + filename + ".xls");
    //            HttpContext.Current.Response.ContentType = "application/ms-excel";
    //            HttpContext.Current.Response.Charset = "GB2312";
    //            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
    //            StringWriter sw = new StringWriter();
    //            HtmlTextWriter htw = new HtmlTextWriter(sw);
    //            obj.RenderControl(htw);
    //            HttpContext.Current.Response.Write(style);
    //            HttpContext.Current.Response.Write(sw.ToString());
    //            HttpContext.Current.Response.End();
    //        }
    //        catch
    //        {
    //        }
    //    }
    //}

    /// <summary>
    /// Summary description for GridViewExport
    /// </summary>
    //public class GridViewExport
    //{
    //    public GridViewExport()
    //    {
    //        //
    //        // TODO: Add constructor logic here
    //        //
    //    }

    //    public static void Export(string fileName, GridView gv)
    //    {
    //        HttpContext.Current.Response.Clear();
    //        HttpContext.Current.Response.AddHeader(
    //            "content-disposition", string.Format("attachment; filename={0}", fileName));
    //        HttpContext.Current.Response.ContentType = "application/ms-excel";
    //        //HttpContext.Current.Response.Charset = "utf-8";


    //        using (StringWriter sw = new StringWriter())
    //        {
    //            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
    //            {
    //                //  Create a form to contain the grid
    //                Table table = new Table();
    //                table.GridLines = GridLines.Both;  //单元格之间添加实线

    //                //  add the header row to the table
    //                if (gv.HeaderRow != null)
    //                {
    //                    PrepareControlForExport(gv.HeaderRow);
    //                    table.Rows.Add(gv.HeaderRow);
    //                }

    //                //  add each of the data rows to the table
    //                foreach (GridViewRow row in gv.Rows)
    //                {
    //                    PrepareControlForExport(row);
    //                    table.Rows.Add(row);
    //                }

    //                //  add the footer row to the table
    //                if (gv.FooterRow != null)
    //                {
    //                    PrepareControlForExport(gv.FooterRow);
    //                    table.Rows.Add(gv.FooterRow);
    //                }

    //                //  render the table into the htmlwriter
    //                table.RenderControl(htw);

    //                //  render the htmlwriter into the response
    //                HttpContext.Current.Response.Write(sw.ToString());
    //                HttpContext.Current.Response.End();
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// Replace any of the contained controls with literals
    //    /// </summary>
    //    /// <param name="control"></param>
    //    private static void PrepareControlForExport(Control control)
    //    {
    //        for (int i = 0; i < control.Controls.Count; i++)
    //        {
    //            Control current = control.Controls[i];
    //            if (current is LinkButton)
    //            {
    //                control.Controls.Remove(current);
    //                control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
    //            }
    //            else if (current is ImageButton)
    //            {
    //                control.Controls.Remove(current);
    //                control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
    //            }
    //            else if (current is HyperLink)
    //            {
    //                control.Controls.Remove(current);
    //                control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
    //            }
    //            else if (current is DropDownList)
    //            {
    //                control.Controls.Remove(current);
    //                control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
    //            }
    //            else if (current is CheckBox)
    //            {
    //                control.Controls.Remove(current);
    //                control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
    //            }

    //            if (current.HasControls())
    //            {
    //                PrepareControlForExport(current);
    //            }
    //        }
    //    }


    //    /// <summary>
    //    /// 导出Grid的数据(全部)到Excel
    //    /// 字段全部为BoundField类型时可用
    //    /// 要是字段为TemplateField模板型时就取不到数据
    //    /// </summary>
    //    /// <param name="grid">grid的ID</param>
    //    /// <param name="dt">数据源</param>
    //    /// <param name="excelFileName">要导出Excel的文件名</param>
    //    public static void OutputExcel(GridView grid, DataTable dt, string excelFileName)
    //    {
    //        Page page = (Page)HttpContext.Current.Handler;
    //        page.Response.Clear();
    //        string fileName = System.Web.HttpUtility.UrlEncode(System.Text.Encoding.UTF8.GetBytes(excelFileName));
    //        page.Response.AddHeader("Content-Disposition", "attachment:filename=" + fileName + ".xls");
    //        page.Response.ContentType = "application/vnd.ms-excel";
    //        page.Response.Charset = "utf-8";

    //        StringBuilder s = new StringBuilder();
    //        s.Append("<HTML><HEAD><TITLE>" + fileName + "</TITLE><META http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"></head><body>");

    //        int count = grid.Columns.Count;

    //        s.Append("<table border=1>");
    //        s.AppendLine("<tr>");
    //        for (int i = 0; i < count; i++)
    //        {

    //            if (grid.Columns[i].GetType() == typeof(BoundField))
    //                s.Append("<td>" + grid.Columns[i].HeaderText + "</td>");

    //            //s.Append("<td>" + grid.Columns[i].HeaderText + "</td>");

    //        }
    //        s.Append("</tr>");

    //        foreach (DataRow dr in dt.Rows)
    //        {
    //            s.AppendLine("<tr>");
    //            for (int n = 0; n < count; n++)
    //            {
    //                if (grid.Columns[n].Visible && grid.Columns[n].GetType() == typeof(BoundField))
    //                    s.Append("<td>" + dr[((BoundField)grid.Columns[n]).DataField].ToString() + "</td>");

    //            }
    //            s.AppendLine("</tr>");
    //        }

    //        s.Append("</table>");
    //        s.Append("</body></html>");

    //        page.Response.BinaryWrite(System.Text.Encoding.GetEncoding("utf-8").GetBytes(s.ToString()));
    //        page.Response.End();
    //    }

    //}
    #endregion

    #region 字符串-N

    /// <summary>
    /// 字符串操作类
    /// 1、GetStrArray(string str, char speater, bool toLower)  把字符串按照分隔符转换成 List
    /// 2、GetStrArray(string str) 把字符串转 按照, 分割 换为数据
    /// 3、GetArrayStr(List list, string speater) 把 List 按照分隔符组装成 string
    /// 4、GetArrayStr(List list)  得到数组列表以逗号分隔的字符串
    /// 5、GetArrayValueStr(Dictionary<int, int> list)得到数组列表以逗号分隔的字符串
    /// 6、DelLastComma(string str)删除最后结尾的一个逗号
    /// 7、DelLastChar(string str, string strchar)删除最后结尾的指定字符后的字符
    /// 8、ToSBC(string input)转全角的函数(SBC case)
    /// 9、ToDBC(string input)转半角的函数(SBC case)
    /// 10、GetSubStringList(string o_str, char sepeater)把字符串按照指定分隔符装成 List 去除重复
    /// 11、GetCleanStyle(string StrList, string SplitString)将字符串样式转换为纯字符串
    /// 12、GetNewStyle(string StrList, string NewStyle, string SplitString, out string Error)将字符串转换为新样式
    /// 13、SplitMulti(string str, string splitstr)分割字符串
    /// 14、SqlSafeString(string String, bool IsDel)
    /// </summary>
    
    //public class StringHelper
    //{
    //    /// <summary>
    //    /// 把字符串按照分隔符转换成 List
    //    /// </summary>
    //    /// <param name="str">源字符串</param>
    //    /// <param name="speater">分隔符</param>
    //    /// <param name="toLower">是否转换为小写</param>
    //    /// <returns></returns>
    //    public static List<string> GetStrArray(string str, char speater, bool toLower)
    //    {
    //        List<string> list = new List<string>();
    //        string[] ss = str.Split(speater);
    //        foreach (string s in ss)
    //        {
    //            if (!string.IsNullOrEmpty(s) && s != speater.ToString())
    //            {
    //                string strVal = s;
    //                if (toLower)
    //                {
    //                    strVal = s.ToLower();
    //                }
    //                list.Add(strVal);
    //            }
    //        }
    //        return list;
    //    }
    //    /// <summary>
    //    /// 把字符串转 按照, 分割 换为数据
    //    /// </summary>
    //    /// <param name="str"></param>
    //    /// <returns></returns>
    //    public static string[] GetStrArray(string str)
    //    {
    //        return str.Split(new Char[] { ',' });
    //    }
    //    /// <summary>
    //    /// 把 List<string> 按照分隔符组装成 string
    //    /// </summary>
    //    /// <param name="list"></param>
    //    /// <param name="speater"></param>
    //    /// <returns></returns>
    //    public static string GetArrayStr(List<string> list, string speater)
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        for (int i = 0; i < list.Count; i++)
    //        {
    //            if (i == list.Count - 1)
    //            {
    //                sb.Append(list[i]);
    //            }
    //            else
    //            {
    //                sb.Append(list[i]);
    //                sb.Append(speater);
    //            }
    //        }
    //        return sb.ToString();
    //    }
    //    /// <summary>
    //    /// 得到数组列表以逗号分隔的字符串
    //    /// </summary>
    //    /// <param name="list"></param>
    //    /// <returns></returns>
    //    public static string GetArrayStr(List<int> list)
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        for (int i = 0; i < list.Count; i++)
    //        {
    //            if (i == list.Count - 1)
    //            {
    //                sb.Append(list[i].ToString());
    //            }
    //            else
    //            {
    //                sb.Append(list[i]);
    //                sb.Append(",");
    //            }
    //        }
    //        return sb.ToString();
    //    }
    //    /// <summary>
    //    /// 得到数组列表以逗号分隔的字符串
    //    /// </summary>
    //    /// <param name="list"></param>
    //    /// <returns></returns>
    //    public static string GetArrayValueStr(Dictionary<int, int> list)
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        foreach (KeyValuePair<int, int> kvp in list)
    //        {
    //            sb.Append(kvp.Value + ",");
    //        }
    //        if (list.Count > 0)
    //        {
    //            return DelLastComma(sb.ToString());
    //        }
    //        else
    //        {
    //            return "";
    //        }
    //    }


    //    #region 删除最后一个字符之后的字符

    //    /// <summary>
    //    /// 删除最后结尾的一个逗号
    //    /// </summary>
    //    public static string DelLastComma(string str)
    //    {
    //        return str.Substring(0, str.LastIndexOf(","));
    //    }

    //    /// <summary>
    //    /// 删除最后结尾的指定字符后的字符
    //    /// </summary>
    //    public static string DelLastChar(string str, string strchar)
    //    {
    //        return str.Substring(0, str.LastIndexOf(strchar));
    //    }

    //    #endregion




    //    /// <summary>
    //    /// 转全角的函数(SBC case)
    //    /// </summary>
    //    /// <param name="input"></param>
    //    /// <returns></returns>
    //    public static string ToSBC(string input)
    //    {
    //        //半角转全角：
    //        char[] c = input.ToCharArray();
    //        for (int i = 0; i < c.Length; i++)
    //        {
    //            if (c[i] == 32)
    //            {
    //                c[i] = (char)12288;
    //                continue;
    //            }
    //            if (c[i] < 127)
    //                c[i] = (char)(c[i] + 65248);
    //        }
    //        return new string(c);
    //    }

    //    /// <summary>
    //    ///  转半角的函数(SBC case)
    //    /// </summary>
    //    /// <param name="input">输入</param>
    //    /// <returns></returns>
    //    public static string ToDBC(string input)
    //    {
    //        char[] c = input.ToCharArray();
    //        for (int i = 0; i < c.Length; i++)
    //        {
    //            if (c[i] == 12288)
    //            {
    //                c[i] = (char)32;
    //                continue;
    //            }
    //            if (c[i] > 65280 && c[i] < 65375)
    //                c[i] = (char)(c[i] - 65248);
    //        }
    //        return new string(c);
    //    }

    //    /// <summary>
    //    /// 把字符串按照指定分隔符装成 List 去除重复
    //    /// </summary>
    //    /// <param name="o_str"></param>
    //    /// <param name="sepeater"></param>
    //    /// <returns></returns>
    //    public static List<string> GetSubStringList(string o_str, char sepeater)
    //    {
    //        List<string> list = new List<string>();
    //        string[] ss = o_str.Split(sepeater);
    //        foreach (string s in ss)
    //        {
    //            if (!string.IsNullOrEmpty(s) && s != sepeater.ToString())
    //            {
    //                list.Add(s);
    //            }
    //        }
    //        return list;
    //    }


    //    #region 将字符串样式转换为纯字符串
    //    /// <summary>
    //    ///  将字符串样式转换为纯字符串
    //    /// </summary>
    //    /// <param name="StrList"></param>
    //    /// <param name="SplitString"></param>
    //    /// <returns></returns>
    //    public static string GetCleanStyle(string StrList, string SplitString)
    //    {
    //        string RetrunValue = "";
    //        //如果为空，返回空值
    //        if (StrList == null)
    //        {
    //            RetrunValue = "";
    //        }
    //        else
    //        {
    //            //返回去掉分隔符
    //            string NewString = "";
    //            NewString = StrList.Replace(SplitString, "");
    //            RetrunValue = NewString;
    //        }
    //        return RetrunValue;
    //    }
    //    #endregion

    //    #region 将字符串转换为新样式
    //    /// <summary>
    //    /// 将字符串转换为新样式
    //    /// </summary>
    //    /// <param name="StrList"></param>
    //    /// <param name="NewStyle"></param>
    //    /// <param name="SplitString"></param>
    //    /// <param name="Error"></param>
    //    /// <returns></returns>
    //    public static string GetNewStyle(string StrList, string NewStyle, string SplitString, out string Error)
    //    {
    //        string ReturnValue = "";
    //        //如果输入空值，返回空，并给出错误提示
    //        if (StrList == null)
    //        {
    //            ReturnValue = "";
    //            Error = "请输入需要划分格式的字符串";
    //        }
    //        else
    //        {
    //            //检查传入的字符串长度和样式是否匹配,如果不匹配，则说明使用错误。给出错误信息并返回空值
    //            int strListLength = StrList.Length;
    //            int NewStyleLength = GetCleanStyle(NewStyle, SplitString).Length;
    //            if (strListLength != NewStyleLength)
    //            {
    //                ReturnValue = "";
    //                Error = "样式格式的长度与输入的字符长度不符，请重新输入";
    //            }
    //            else
    //            {
    //                //检查新样式中分隔符的位置
    //                string Lengstr = "";
    //                for (int i = 0; i < NewStyle.Length; i++)
    //                {
    //                    if (NewStyle.Substring(i, 1) == SplitString)
    //                    {
    //                        Lengstr = Lengstr + "," + i;
    //                    }
    //                }
    //                if (Lengstr != "")
    //                {
    //                    Lengstr = Lengstr.Substring(1);
    //                }
    //                //将分隔符放在新样式中的位置
    //                string[] str = Lengstr.Split(',');
    //                foreach (string bb in str)
    //                {
    //                    StrList = StrList.Insert(int.Parse(bb), SplitString);
    //                }
    //                //给出最后的结果
    //                ReturnValue = StrList;
    //                //因为是正常的输出，没有错误
    //                Error = "";
    //            }
    //        }
    //        return ReturnValue;
    //    }
    //    #endregion

    //    /// <summary>
    //    /// 分割字符串
    //    /// </summary>
    //    /// <param name="str"></param>
    //    /// <param name="splitstr"></param>
    //    /// <returns></returns>
    //    public static string[] SplitMulti(string str, string splitstr)
    //    {
    //        string[] strArray = null;
    //        if ((str != null) && (str != ""))
    //        {
    //            strArray = new Regex(splitstr).Split(str);
    //        }
    //        return strArray;
    //    }
    //    public static string SqlSafeString(string String, bool IsDel)
    //    {
    //        if (IsDel)
    //        {
    //            String = String.Replace("'", "");
    //            String = String.Replace("\"", "");
    //            return String;
    //        }
    //        String = String.Replace("'", "&#39;");
    //        String = String.Replace("\"", "&#34;");
    //        return String;
    //    }

    //    #region 获取正确的Id，如果不是正整数，返回0
    //    /// <summary>
    //    /// 获取正确的Id，如果不是正整数，返回0
    //    /// </summary>
    //    /// <param name="_value"></param>
    //    /// <returns>返回正确的整数ID，失败返回0</returns>
    //    public static int StrToId(string _value)
    //    {
    //        if (IsNumberId(_value))
    //            return int.Parse(_value);
    //        else
    //            return 0;
    //    }
    //    #endregion
    //    #region 检查一个字符串是否是纯数字构成的，一般用于查询字符串参数的有效性验证。
    //    /// <summary>
    //    /// 检查一个字符串是否是纯数字构成的，一般用于查询字符串参数的有效性验证。(0除外)
    //    /// </summary>
    //    /// <param name="_value">需验证的字符串。。</param>
    //    /// <returns>是否合法的bool值。</returns>
    //    public static bool IsNumberId(string _value)
    //    {
    //        return QuickValidate("^[1-9]*[0-9]*$", _value);
    //    }
    //    #endregion
    //    #region 快速验证一个字符串是否符合指定的正则表达式。
    //    /// <summary>
    //    /// 快速验证一个字符串是否符合指定的正则表达式。
    //    /// </summary>
    //    /// <param name="_express">正则表达式的内容。</param>
    //    /// <param name="_value">需验证的字符串。</param>
    //    /// <returns>是否合法的bool值。</returns>
    //    public static bool QuickValidate(string _express, string _value)
    //    {
    //        if (_value == null) return false;
    //        Regex myRegex = new Regex(_express);
    //        if (_value.Length == 0)
    //        {
    //            return false;
    //        }
    //        return myRegex.IsMatch(_value);
    //    }
    //    #endregion


    //    #region 根据配置对指定字符串进行 MD5 加密
    //    /// <summary>
    //    /// 根据配置对指定字符串进行 MD5 加密
    //    /// </summary>
    //    /// <param name="s"></param>
    //    /// <returns></returns>
    //    public static string GetMD5(string s)
    //    {
    //        //md5加密
    //        s = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();

    //        return s.ToLower().Substring(8, 16);
    //    }
    //    #endregion

    //    #region 得到字符串长度，一个汉字长度为2
    //    /// <summary>
    //    /// 得到字符串长度，一个汉字长度为2
    //    /// </summary>
    //    /// <param name="inputString">参数字符串</param>
    //    /// <returns></returns>
    //    public static int StrLength(string inputString)
    //    {
    //        System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
    //        int tempLen = 0;
    //        byte[] s = ascii.GetBytes(inputString);
    //        for (int i = 0; i < s.Length; i++)
    //        {
    //            if ((int)s[i] == 63)
    //                tempLen += 2;
    //            else
    //                tempLen += 1;
    //        }
    //        return tempLen;
    //    }
    //    #endregion

    //    #region 截取指定长度字符串
    //    /// <summary>
    //    /// 截取指定长度字符串
    //    /// </summary>
    //    /// <param name="inputString">要处理的字符串</param>
    //    /// <param name="len">指定长度</param>
    //    /// <returns>返回处理后的字符串</returns>
    //    public static string ClipString(string inputString, int len)
    //    {
    //        bool isShowFix = false;
    //        if (len % 2 == 1)
    //        {
    //            isShowFix = true;
    //            len--;
    //        }
    //        System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
    //        int tempLen = 0;
    //        string tempString = "";
    //        byte[] s = ascii.GetBytes(inputString);
    //        for (int i = 0; i < s.Length; i++)
    //        {
    //            if ((int)s[i] == 63)
    //                tempLen += 2;
    //            else
    //                tempLen += 1;

    //            try
    //            {
    //                tempString += inputString.Substring(i, 1);
    //            }
    //            catch
    //            {
    //                break;
    //            }

    //            if (tempLen > len)
    //                break;
    //        }

    //        byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
    //        if (isShowFix && mybyte.Length > len)
    //            tempString += "…";
    //        return tempString;
    //    }
    //    #endregion



    //    #region HTML转行成TEXT
    //    /// <summary>
    //    /// HTML转行成TEXT
    //    /// </summary>
    //    /// <param name="strHtml"></param>
    //    /// <returns></returns>
    //    public static string HtmlToTxt(string strHtml)
    //    {
    //        string[] aryReg ={
    //        @"<script[^>]*?>.*?</script>",
    //        @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
    //        @"([\r\n])[\s]+",
    //        @"&(quot|#34);",
    //        @"&(amp|#38);",
    //        @"&(lt|#60);",
    //        @"&(gt|#62);", 
    //        @"&(nbsp|#160);", 
    //        @"&(iexcl|#161);",
    //        @"&(cent|#162);",
    //        @"&(pound|#163);",
    //        @"&(copy|#169);",
    //        @"&#(\d+);",
    //        @"-->",
    //        @"<!--.*\n"
    //        };

    //        string newReg = aryReg[0];
    //        string strOutput = strHtml;
    //        for (int i = 0; i < aryReg.Length; i++)
    //        {
    //            Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
    //            strOutput = regex.Replace(strOutput, string.Empty);
    //        }

    //        strOutput.Replace("<", "");
    //        strOutput.Replace(">", "");
    //        strOutput.Replace("\r\n", "");


    //        return strOutput;
    //    }
    //    #endregion

    //    #region 判断对象是否为空
    //    /// <summary>
    //    /// 判断对象是否为空，为空返回true
    //    /// </summary>
    //    /// <typeparam name="T">要验证的对象的类型</typeparam>
    //    /// <param name="data">要验证的对象</param>        
    //    public static bool IsNullOrEmpty<T>(T data)
    //    {
    //        //如果为null
    //        if (data == null)
    //        {
    //            return true;
    //        }

    //        //如果为""
    //        if (data.GetType() == typeof(String))
    //        {
    //            if (string.IsNullOrEmpty(data.ToString().Trim()))
    //            {
    //                return true;
    //            }
    //        }

    //        //如果为DBNull
    //        if (data.GetType() == typeof(DBNull))
    //        {
    //            return true;
    //        }

    //        //不为空
    //        return false;
    //    }

    //    /// <summary>
    //    /// 判断对象是否为空，为空返回true
    //    /// </summary>
    //    /// <param name="data">要验证的对象</param>
    //    public static bool IsNullOrEmpty(object data)
    //    {
    //        //如果为null
    //        if (data == null)
    //        {
    //            return true;
    //        }

    //        //如果为""
    //        if (data.GetType() == typeof(String))
    //        {
    //            if (string.IsNullOrEmpty(data.ToString().Trim()))
    //            {
    //                return true;
    //            }
    //        }

    //        //如果为DBNull
    //        if (data.GetType() == typeof(DBNull))
    //        {
    //            return true;
    //        }

    //        //不为空
    //        return false;
    //    }
    //    #endregion
    //}

    #endregion

    #region 文件操作-N

    /// <summary>
    /// 文件夹操作类
    /// </summary>
    //public static class DirFile
    //{
    //    #region 检测指定目录是否存在
    //    /// <summary>
    //    /// 检测指定目录是否存在
    //    /// </summary>
    //    /// <param name="directoryPath">目录的绝对路径</param>
    //    /// <returns></returns>
    //    public static bool IsExistDirectory(string directoryPath)
    //    {
    //        return Directory.Exists(directoryPath);
    //    }
    //    #endregion

    //    #region 检测指定文件是否存在,如果存在返回true
    //    /// <summary>
    //    /// 检测指定文件是否存在,如果存在则返回true。
    //    /// </summary>
    //    /// <param name="filePath">文件的绝对路径</param>        
    //    public static bool IsExistFile(string filePath)
    //    {
    //        return File.Exists(filePath);
    //    }
    //    #endregion

    //    #region 获取指定目录中的文件列表
    //    /// <summary>
    //    /// 获取指定目录中所有文件列表
    //    /// </summary>
    //    /// <param name="directoryPath">指定目录的绝对路径</param>        
    //    public static string[] GetFileNames(string directoryPath)
    //    {
    //        //如果目录不存在，则抛出异常
    //        if (!IsExistDirectory(directoryPath))
    //        {
    //            throw new FileNotFoundException();
    //        }

    //        //获取文件列表
    //        return Directory.GetFiles(directoryPath);
    //    }
    //    #endregion

    //    #region 获取指定目录中所有子目录列表,若要搜索嵌套的子目录列表,请使用重载方法.
    //    /// <summary>
    //    /// 获取指定目录中所有子目录列表,若要搜索嵌套的子目录列表,请使用重载方法.
    //    /// </summary>
    //    /// <param name="directoryPath">指定目录的绝对路径</param>        
    //    public static string[] GetDirectories(string directoryPath)
    //    {
    //        try
    //        {
    //            return Directory.GetDirectories(directoryPath);
    //        }
    //        catch (IOException ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //    #endregion

    //    #region 获取指定目录及子目录中所有文件列表
    //    /// <summary>
    //    /// 获取指定目录及子目录中所有文件列表
    //    /// </summary>
    //    /// <param name="directoryPath">指定目录的绝对路径</param>
    //    /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
    //    /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
    //    /// <param name="isSearchChild">是否搜索子目录</param>
    //    public static string[] GetFileNames(string directoryPath, string searchPattern, bool isSearchChild)
    //    {
    //        //如果目录不存在，则抛出异常
    //        if (!IsExistDirectory(directoryPath))
    //        {
    //            throw new FileNotFoundException();
    //        }

    //        try
    //        {
    //            if (isSearchChild)
    //            {
    //                return Directory.GetFiles(directoryPath, searchPattern, SearchOption.AllDirectories);
    //            }
    //            else
    //            {
    //                return Directory.GetFiles(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
    //            }
    //        }
    //        catch (IOException ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //    #endregion

    //    #region 检测指定目录是否为空
    //    /// <summary>
    //    /// 检测指定目录是否为空
    //    /// </summary>
    //    /// <param name="directoryPath">指定目录的绝对路径</param>        
    //    public static bool IsEmptyDirectory(string directoryPath)
    //    {
    //        try
    //        {
    //            //判断是否存在文件
    //            string[] fileNames = GetFileNames(directoryPath);
    //            if (fileNames.Length > 0)
    //            {
    //                return false;
    //            }

    //            //判断是否存在文件夹
    //            string[] directoryNames = GetDirectories(directoryPath);
    //            if (directoryNames.Length > 0)
    //            {
    //                return false;
    //            }

    //            return true;
    //        }
    //        catch
    //        {
    //            //这里记录日志
    //            //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
    //            return true;
    //        }
    //    }
    //    #endregion

    //    #region 检测指定目录中是否存在指定的文件
    //    /// <summary>
    //    /// 检测指定目录中是否存在指定的文件,若要搜索子目录请使用重载方法.
    //    /// </summary>
    //    /// <param name="directoryPath">指定目录的绝对路径</param>
    //    /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
    //    /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>        
    //    public static bool Contains(string directoryPath, string searchPattern)
    //    {
    //        try
    //        {
    //            //获取指定的文件列表
    //            string[] fileNames = GetFileNames(directoryPath, searchPattern, false);

    //            //判断指定文件是否存在
    //            if (fileNames.Length == 0)
    //            {
    //                return false;
    //            }
    //            else
    //            {
    //                return true;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception(ex.Message);
    //            //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
    //        }
    //    }

    //    /// <summary>
    //    /// 检测指定目录中是否存在指定的文件
    //    /// </summary>
    //    /// <param name="directoryPath">指定目录的绝对路径</param>
    //    /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
    //    /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param> 
    //    /// <param name="isSearchChild">是否搜索子目录</param>
    //    public static bool Contains(string directoryPath, string searchPattern, bool isSearchChild)
    //    {
    //        try
    //        {
    //            //获取指定的文件列表
    //            string[] fileNames = GetFileNames(directoryPath, searchPattern, true);

    //            //判断指定文件是否存在
    //            if (fileNames.Length == 0)
    //            {
    //                return false;
    //            }
    //            else
    //            {
    //                return true;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception(ex.Message);
    //            //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
    //        }
    //    }
    //    #endregion

    //    #region 创建目录
    //    /// <summary>
    //    /// 创建目录
    //    /// </summary>
    //    /// <param name="dir">要创建的目录路径包括目录名</param>
    //    public static void CreateDir(string dir)
    //    {
    //        if (dir.Length == 0) return;
    //        if (!Directory.Exists(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir))
    //            Directory.CreateDirectory(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir);
    //    }
    //    #endregion

    //    #region 删除目录
    //    /// <summary>
    //    /// 删除目录
    //    /// </summary>
    //    /// <param name="dir">要删除的目录路径和名称</param>
    //    public static void DeleteDir(string dir)
    //    {
    //        if (dir.Length == 0) return;
    //        if (Directory.Exists(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir))
    //            Directory.Delete(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir);
    //    }
    //    #endregion

    //    #region 删除文件
    //    /// <summary>
    //    /// 删除文件
    //    /// </summary>
    //    /// <param name="file">要删除的文件路径和名称</param>
    //    public static void DeleteFile(string file)
    //    {
    //        if (File.Exists(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + file))
    //            File.Delete(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + file);
    //    }
    //    #endregion

    //    #region 创建文件
    //    /// <summary>
    //    /// 创建文件
    //    /// </summary>
    //    /// <param name="dir">带后缀的文件名</param>
    //    /// <param name="pagestr">文件内容</param>
    //    public static void CreateFile(string dir, string pagestr)
    //    {
    //        dir = dir.Replace("/", "\\");
    //        if (dir.IndexOf("\\") > -1)
    //            CreateDir(dir.Substring(0, dir.LastIndexOf("\\")));
    //        System.IO.StreamWriter sw = new System.IO.StreamWriter(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir, false, System.Text.Encoding.GetEncoding("GB2312"));
    //        sw.Write(pagestr);
    //        sw.Close();
    //    }
    //    #endregion

    //    #region 移动文件(剪贴--粘贴)
    //    /// <summary>
    //    /// 移动文件(剪贴--粘贴)
    //    /// </summary>
    //    /// <param name="dir1">要移动的文件的路径及全名(包括后缀)</param>
    //    /// <param name="dir2">文件移动到新的位置,并指定新的文件名</param>
    //    public static void MoveFile(string dir1, string dir2)
    //    {
    //        dir1 = dir1.Replace("/", "\\");
    //        dir2 = dir2.Replace("/", "\\");
    //        if (File.Exists(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir1))
    //            File.Move(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir1, System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir2);
    //    }
    //    #endregion

    //    #region 复制文件
    //    /// <summary>
    //    /// 复制文件
    //    /// </summary>
    //    /// <param name="dir1">要复制的文件的路径已经全名(包括后缀)</param>
    //    /// <param name="dir2">目标位置,并指定新的文件名</param>
    //    public static void CopyFile(string dir1, string dir2)
    //    {
    //        dir1 = dir1.Replace("/", "\\");
    //        dir2 = dir2.Replace("/", "\\");
    //        if (File.Exists(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir1))
    //        {
    //            File.Copy(System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir1, System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\" + dir2, true);
    //        }
    //    }
    //    #endregion

    //    #region 根据时间得到目录名 / 格式:yyyyMMdd 或者 HHmmssff
    //    /// <summary>
    //    /// 根据时间得到目录名yyyyMMdd
    //    /// </summary>
    //    /// <returns></returns>
    //    public static string GetDateDir()
    //    {
    //        return DateTime.Now.ToString("yyyyMMdd");
    //    }
    //    /// <summary>
    //    /// 根据时间得到文件名HHmmssff
    //    /// </summary>
    //    /// <returns></returns>
    //    public static string GetDateFile()
    //    {
    //        return DateTime.Now.ToString("HHmmssff");
    //    }
    //    #endregion

    //    #region 复制文件夹
    //    /// <summary>
    //    /// 复制文件夹(递归)
    //    /// </summary>
    //    /// <param name="varFromDirectory">源文件夹路径</param>
    //    /// <param name="varToDirectory">目标文件夹路径</param>
    //    public static void CopyFolder(string varFromDirectory, string varToDirectory)
    //    {
    //        Directory.CreateDirectory(varToDirectory);

    //        if (!Directory.Exists(varFromDirectory)) return;

    //        string[] directories = Directory.GetDirectories(varFromDirectory);

    //        if (directories.Length > 0)
    //        {
    //            foreach (string d in directories)
    //            {
    //                CopyFolder(d, varToDirectory + d.Substring(d.LastIndexOf("\\")));
    //            }
    //        }
    //        string[] files = Directory.GetFiles(varFromDirectory);
    //        if (files.Length > 0)
    //        {
    //            foreach (string s in files)
    //            {
    //                File.Copy(s, varToDirectory + s.Substring(s.LastIndexOf("\\")), true);
    //            }
    //        }
    //    }
    //    #endregion

    //    #region 检查文件,如果文件不存在则创建
    //    /// <summary>
    //    /// 检查文件,如果文件不存在则创建  
    //    /// </summary>
    //    /// <param name="FilePath">路径,包括文件名</param>
    //    public static void ExistsFile(string FilePath)
    //    {
    //        //if(!File.Exists(FilePath))    
    //        //File.Create(FilePath);    
    //        //以上写法会报错,详细解释请看下文.........   
    //        if (!File.Exists(FilePath))
    //        {
    //            FileStream fs = File.Create(FilePath);
    //            fs.Close();
    //        }
    //    }
    //    #endregion

    //    #region 删除指定文件夹对应其他文件夹里的文件
    //    /// <summary>
    //    /// 删除指定文件夹对应其他文件夹里的文件
    //    /// </summary>
    //    /// <param name="varFromDirectory">指定文件夹路径</param>
    //    /// <param name="varToDirectory">对应其他文件夹路径</param>
    //    public static void DeleteFolderFiles(string varFromDirectory, string varToDirectory)
    //    {
    //        Directory.CreateDirectory(varToDirectory);

    //        if (!Directory.Exists(varFromDirectory)) return;

    //        string[] directories = Directory.GetDirectories(varFromDirectory);

    //        if (directories.Length > 0)
    //        {
    //            foreach (string d in directories)
    //            {
    //                DeleteFolderFiles(d, varToDirectory + d.Substring(d.LastIndexOf("\\")));
    //            }
    //        }


    //        string[] files = Directory.GetFiles(varFromDirectory);

    //        if (files.Length > 0)
    //        {
    //            foreach (string s in files)
    //            {
    //                File.Delete(varToDirectory + s.Substring(s.LastIndexOf("\\")));
    //            }
    //        }
    //    }
    //    #endregion

    //    #region 从文件的绝对路径中获取文件名( 包含扩展名 )
    //    /// <summary>
    //    /// 从文件的绝对路径中获取文件名( 包含扩展名 )
    //    /// </summary>
    //    /// <param name="filePath">文件的绝对路径</param>        
    //    public static string GetFileName(string filePath)
    //    {
    //        //获取文件的名称
    //        FileInfo fi = new FileInfo(filePath);
    //        return fi.Name;
    //    }
    //    #endregion

    //    /// <summary>
    //    /// 复制文件参考方法,页面中引用
    //    /// </summary>
    //    /// <param name="cDir">新路径</param>
    //    /// <param name="TempId">模板引擎替换编号</param>
    //    public static void CopyFiles(string cDir, string TempId)
    //    {
    //        //if (Directory.Exists(Request.PhysicalApplicationPath + "\\Controls"))
    //        //{
    //        //    string TempStr = string.Empty;
    //        //    StreamWriter sw;
    //        //    if (File.Exists(Request.PhysicalApplicationPath + "\\Controls\\Default.aspx"))
    //        //    {
    //        //        TempStr = File.ReadAllText(Request.PhysicalApplicationPath + "\\Controls\\Default.aspx");
    //        //        TempStr = TempStr.Replace("{$ChannelId$}", TempId);

    //        //        sw = new StreamWriter(Request.PhysicalApplicationPath + "\\" + cDir + "\\Default.aspx", false, System.Text.Encoding.GetEncoding("GB2312"));
    //        //        sw.Write(TempStr);
    //        //        sw.Close();
    //        //    }
    //        //    if (File.Exists(Request.PhysicalApplicationPath + "\\Controls\\Column.aspx"))
    //        //    {
    //        //        TempStr = File.ReadAllText(Request.PhysicalApplicationPath + "\\Controls\\Column.aspx");
    //        //        TempStr = TempStr.Replace("{$ChannelId$}", TempId);

    //        //        sw = new StreamWriter(Request.PhysicalApplicationPath + "\\" + cDir + "\\List.aspx", false, System.Text.Encoding.GetEncoding("GB2312"));
    //        //        sw.Write(TempStr);
    //        //        sw.Close();
    //        //    }
    //        //    if (File.Exists(Request.PhysicalApplicationPath + "\\Controls\\Content.aspx"))
    //        //    {
    //        //        TempStr = File.ReadAllText(Request.PhysicalApplicationPath + "\\Controls\\Content.aspx");
    //        //        TempStr = TempStr.Replace("{$ChannelId$}", TempId);

    //        //        sw = new StreamWriter(Request.PhysicalApplicationPath + "\\" + cDir + "\\View.aspx", false, System.Text.Encoding.GetEncoding("GB2312"));
    //        //        sw.Write(TempStr);
    //        //        sw.Close();
    //        //    }
    //        //    if (File.Exists(Request.PhysicalApplicationPath + "\\Controls\\MoreDiss.aspx"))
    //        //    {
    //        //        TempStr = File.ReadAllText(Request.PhysicalApplicationPath + "\\Controls\\MoreDiss.aspx");
    //        //        TempStr = TempStr.Replace("{$ChannelId$}", TempId);

    //        //        sw = new StreamWriter(Request.PhysicalApplicationPath + "\\" + cDir + "\\DissList.aspx", false, System.Text.Encoding.GetEncoding("GB2312"));
    //        //        sw.Write(TempStr);
    //        //        sw.Close();
    //        //    }
    //        //    if (File.Exists(Request.PhysicalApplicationPath + "\\Controls\\ShowDiss.aspx"))
    //        //    {
    //        //        TempStr = File.ReadAllText(Request.PhysicalApplicationPath + "\\Controls\\ShowDiss.aspx");
    //        //        TempStr = TempStr.Replace("{$ChannelId$}", TempId);

    //        //        sw = new StreamWriter(Request.PhysicalApplicationPath + "\\" + cDir + "\\Diss.aspx", false, System.Text.Encoding.GetEncoding("GB2312"));
    //        //        sw.Write(TempStr);
    //        //        sw.Close();
    //        //    }
    //        //    if (File.Exists(Request.PhysicalApplicationPath + "\\Controls\\Review.aspx"))
    //        //    {
    //        //        TempStr = File.ReadAllText(Request.PhysicalApplicationPath + "\\Controls\\Review.aspx");
    //        //        TempStr = TempStr.Replace("{$ChannelId$}", TempId);

    //        //        sw = new StreamWriter(Request.PhysicalApplicationPath + "\\" + cDir + "\\Review.aspx", false, System.Text.Encoding.GetEncoding("GB2312"));
    //        //        sw.Write(TempStr);
    //        //        sw.Close();
    //        //    }
    //        //    if (File.Exists(Request.PhysicalApplicationPath + "\\Controls\\Search.aspx"))
    //        //    {
    //        //        TempStr = File.ReadAllText(Request.PhysicalApplicationPath + "\\Controls\\Search.aspx");
    //        //        TempStr = TempStr.Replace("{$ChannelId$}", TempId);

    //        //        sw = new StreamWriter(Request.PhysicalApplicationPath + "\\" + cDir + "\\Search.aspx", false, System.Text.Encoding.GetEncoding("GB2312"));
    //        //        sw.Write(TempStr);
    //        //        sw.Close();
    //        //    }
    //        //}
    //    }


    //    #region 创建一个目录
    //    /// <summary>
    //    /// 创建一个目录
    //    /// </summary>
    //    /// <param name="directoryPath">目录的绝对路径</param>
    //    public static void CreateDirectory(string directoryPath)
    //    {
    //        //如果目录不存在则创建该目录
    //        if (!IsExistDirectory(directoryPath))
    //        {
    //            Directory.CreateDirectory(directoryPath);
    //        }
    //    }
    //    #endregion

    //    #region 创建一个文件
    //    /// <summary>
    //    /// 创建一个文件。
    //    /// </summary>
    //    /// <param name="filePath">文件的绝对路径</param>
    //    public static void CreateFile(string filePath)
    //    {
    //        try
    //        {
    //            //如果文件不存在则创建该文件
    //            if (!IsExistFile(filePath))
    //            {
    //                //创建一个FileInfo对象
    //                FileInfo file = new FileInfo(filePath);

    //                //创建文件
    //                FileStream fs = file.Create();

    //                //关闭文件流
    //                fs.Close();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
    //            throw ex;
    //        }
    //    }

    //    /// <summary>
    //    /// 创建一个文件,并将字节流写入文件。
    //    /// </summary>
    //    /// <param name="filePath">文件的绝对路径</param>
    //    /// <param name="buffer">二进制流数据</param>
    //    public static void CreateFile(string filePath, byte[] buffer)
    //    {
    //        try
    //        {
    //            //如果文件不存在则创建该文件
    //            if (!IsExistFile(filePath))
    //            {
    //                //创建一个FileInfo对象
    //                FileInfo file = new FileInfo(filePath);

    //                //创建文件
    //                FileStream fs = file.Create();

    //                //写入二进制流
    //                fs.Write(buffer, 0, buffer.Length);

    //                //关闭文件流
    //                fs.Close();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            //LogHelper.WriteTraceLog(TraceLogLevel.Error, ex.Message);
    //            throw ex;
    //        }
    //    }
    //    #endregion

    //    #region 获取文本文件的行数
    //    /// <summary>
    //    /// 获取文本文件的行数
    //    /// </summary>
    //    /// <param name="filePath">文件的绝对路径</param>        
    //    public static int GetLineCount(string filePath)
    //    {
    //        //将文本文件的各行读到一个字符串数组中
    //        string[] rows = File.ReadAllLines(filePath);

    //        //返回行数
    //        return rows.Length;
    //    }
    //    #endregion

    //    #region 获取一个文件的长度
    //    /// <summary>
    //    /// 获取一个文件的长度,单位为Byte
    //    /// </summary>
    //    /// <param name="filePath">文件的绝对路径</param>        
    //    public static int GetFileSize(string filePath)
    //    {
    //        //创建一个文件对象
    //        FileInfo fi = new FileInfo(filePath);

    //        //获取文件的大小
    //        return (int)fi.Length;
    //    }
    //    #endregion

    //    #region 获取指定目录中的子目录列表
    //    /// <summary>
    //    /// 获取指定目录及子目录中所有子目录列表
    //    /// </summary>
    //    /// <param name="directoryPath">指定目录的绝对路径</param>
    //    /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
    //    /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
    //    /// <param name="isSearchChild">是否搜索子目录</param>
    //    public static string[] GetDirectories(string directoryPath, string searchPattern, bool isSearchChild)
    //    {
    //        try
    //        {
    //            if (isSearchChild)
    //            {
    //                return Directory.GetDirectories(directoryPath, searchPattern, SearchOption.AllDirectories);
    //            }
    //            else
    //            {
    //                return Directory.GetDirectories(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
    //            }
    //        }
    //        catch (IOException ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //    #endregion

    //    #region 向文本文件写入内容

    //    /// <summary>
    //    /// 向文本文件中写入内容
    //    /// </summary>
    //    /// <param name="filePath">文件的绝对路径</param>
    //    /// <param name="text">写入的内容</param>
    //    /// <param name="encoding">编码</param>
    //    public static void WriteText(string filePath, string text, Encoding encoding)
    //    {
    //        //向文件写入内容
    //        File.WriteAllText(filePath, text, encoding);
    //    }
    //    #endregion

    //    #region 向文本文件的尾部追加内容
    //    /// <summary>
    //    /// 向文本文件的尾部追加内容
    //    /// </summary>
    //    /// <param name="filePath">文件的绝对路径</param>
    //    /// <param name="content">写入的内容</param>
    //    public static void AppendText(string filePath, string content)
    //    {
    //        File.AppendAllText(filePath, content);
    //    }
    //    #endregion

    //    #region 将现有文件的内容复制到新文件中
    //    /// <summary>
    //    /// 将源文件的内容复制到目标文件中
    //    /// </summary>
    //    /// <param name="sourceFilePath">源文件的绝对路径</param>
    //    /// <param name="destFilePath">目标文件的绝对路径</param>
    //    public static void Copy(string sourceFilePath, string destFilePath)
    //    {
    //        File.Copy(sourceFilePath, destFilePath, true);
    //    }
    //    #endregion

    //    #region 将文件移动到指定目录
    //    /// <summary>
    //    /// 将文件移动到指定目录
    //    /// </summary>
    //    /// <param name="sourceFilePath">需要移动的源文件的绝对路径</param>
    //    /// <param name="descDirectoryPath">移动到的目录的绝对路径</param>
    //    public static void Move(string sourceFilePath, string descDirectoryPath)
    //    {
    //        //获取源文件的名称
    //        string sourceFileName = GetFileName(sourceFilePath);

    //        if (IsExistDirectory(descDirectoryPath))
    //        {
    //            //如果目标中存在同名文件,则删除
    //            if (IsExistFile(descDirectoryPath + "\\" + sourceFileName))
    //            {
    //                DeleteFile(descDirectoryPath + "\\" + sourceFileName);
    //            }
    //            //将文件移动到指定目录
    //            File.Move(sourceFilePath, descDirectoryPath + "\\" + sourceFileName);
    //        }
    //    }
    //    #endregion


    //    #region 从文件的绝对路径中获取文件名( 不包含扩展名 )
    //    /// <summary>
    //    /// 从文件的绝对路径中获取文件名( 不包含扩展名 )
    //    /// </summary>
    //    /// <param name="filePath">文件的绝对路径</param>        
    //    public static string GetFileNameNoExtension(string filePath)
    //    {
    //        //获取文件的名称
    //        FileInfo fi = new FileInfo(filePath);
    //        return fi.Name.Split('.')[0];
    //    }
    //    #endregion

    //    #region 从文件的绝对路径中获取扩展名
    //    /// <summary>
    //    /// 从文件的绝对路径中获取扩展名
    //    /// </summary>
    //    /// <param name="filePath">文件的绝对路径</param>        
    //    public static string GetExtension(string filePath)
    //    {
    //        //获取文件的名称
    //        FileInfo fi = new FileInfo(filePath);
    //        return fi.Extension;
    //    }
    //    #endregion

    //    #region 清空指定目录
    //    /// <summary>
    //    /// 清空指定目录下所有文件及子目录,但该目录依然保存.
    //    /// </summary>
    //    /// <param name="directoryPath">指定目录的绝对路径</param>
    //    public static void ClearDirectory(string directoryPath)
    //    {
    //        if (IsExistDirectory(directoryPath))
    //        {
    //            //删除目录中所有的文件
    //            string[] fileNames = GetFileNames(directoryPath);
    //            for (int i = 0; i < fileNames.Length; i++)
    //            {
    //                DeleteFile(fileNames[i]);
    //            }

    //            //删除目录中所有的子目录
    //            string[] directoryNames = GetDirectories(directoryPath);
    //            for (int i = 0; i < directoryNames.Length; i++)
    //            {
    //                DeleteDirectory(directoryNames[i]);
    //            }
    //        }
    //    }
    //    #endregion

    //    #region 清空文件内容
    //    /// <summary>
    //    /// 清空文件内容
    //    /// </summary>
    //    /// <param name="filePath">文件的绝对路径</param>
    //    public static void ClearFile(string filePath)
    //    {
    //        //删除文件
    //        File.Delete(filePath);

    //        //重新创建该文件
    //        CreateFile(filePath);
    //    }
    //    #endregion

    //    #region 删除指定目录
    //    /// <summary>
    //    /// 删除指定目录及其所有子目录
    //    /// </summary>
    //    /// <param name="directoryPath">指定目录的绝对路径</param>
    //    public static void DeleteDirectory(string directoryPath)
    //    {
    //        if (IsExistDirectory(directoryPath))
    //        {
    //            Directory.Delete(directoryPath, true);
    //        }
    //    }
    //    #endregion
    //}

    /// <summary>
    /// INI文件读写类
    /// </summary>
    //public class INIFile
    //{
    //    public string path;

    //    public INIFile(string INIPath)
    //    {
    //        path = INIPath;
    //    }

    //    [DllImport("kernel32")]
    //    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

    //    [DllImport("kernel32")]
    //    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);


    //    [DllImport("kernel32")]
    //    private static extern int GetPrivateProfileString(string section, string key, string defVal, Byte[] retVal, int size, string filePath);


    //    /// <summary>
    //    /// 写INI文件
    //    /// </summary>
    //    /// <param name="Section"></param>
    //    /// <param name="Key"></param>
    //    /// <param name="Value"></param>
    //    public void IniWriteValue(string Section, string Key, string Value)
    //    {
    //        WritePrivateProfileString(Section, Key, Value, this.path);
    //    }

    //    /// <summary>
    //    /// 读取INI文件
    //    /// </summary>
    //    /// <param name="Section"></param>
    //    /// <param name="Key"></param>
    //    /// <returns></returns>
    //    public string IniReadValue(string Section, string Key)
    //    {
    //        StringBuilder temp = new StringBuilder(255);
    //        int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
    //        return temp.ToString();
    //    }
    //    public byte[] IniReadValues(string section, string key)
    //    {
    //        byte[] temp = new byte[255];
    //        int i = GetPrivateProfileString(section, key, "", temp, 255, this.path);
    //        return temp;

    //    }


    //    /// <summary>
    //    /// 删除ini文件下所有段落
    //    /// </summary>
    //    public void ClearAllSection()
    //    {
    //        IniWriteValue(null, null, null);
    //    }
    //    /// <summary>
    //    /// 删除ini文件下personal段落下的所有键
    //    /// </summary>
    //    /// <param name="Section"></param>
    //    public void ClearSection(string Section)
    //    {
    //        IniWriteValue(Section, null, null);
    //    }

    //}

    /// <summary>
    /// 文件操作类
    /// </summary>
    //public class FileOperate
    //{
    //    #region 写文件
    //    protected void Write_Txt(string FileName, string Content)
    //    {
    //        Encoding code = Encoding.GetEncoding("gb2312");
    //        string htmlfilename = HttpContext.Current.Server.MapPath("Precious\\" + FileName + ".txt");　//保存文件的路径
    //        string str = Content;
    //        StreamWriter sw = null;
    //        {
    //            try
    //            {
    //                sw = new StreamWriter(htmlfilename, false, code);
    //                sw.Write(str);
    //                sw.Flush();
    //            }
    //            catch { }
    //        }
    //        sw.Close();
    //        sw.Dispose();

    //    }
    //    #endregion

    //    #region 读文件
    //    protected string Read_Txt(string filename)
    //    {

    //        Encoding code = Encoding.GetEncoding("gb2312");
    //        string temp = HttpContext.Current.Server.MapPath("Precious\\" + filename + ".txt");
    //        string str = "";
    //        if (File.Exists(temp))
    //        {
    //            StreamReader sr = null;
    //            try
    //            {
    //                sr = new StreamReader(temp, code);
    //                str = sr.ReadToEnd(); // 读取文件
    //            }
    //            catch { }
    //            sr.Close();
    //            sr.Dispose();
    //        }
    //        else
    //        {
    //            str = "";
    //        }


    //        return str;
    //    }
    //    #endregion

    //    #region 取得文件后缀名
    //    /****************************************
    //     * 函数名称：GetPostfixStr
    //     * 功能说明：取得文件后缀名
    //     * 参    数：filename:文件名称
    //     * 调用示列：
    //     *           string filename = "aaa.aspx";        
    //     *           string s = DotNet.Utilities.FileOperate.GetPostfixStr(filename);         
    //    *****************************************/
    //    /// <summary>
    //    /// 取后缀名
    //    /// </summary>
    //    /// <param name="filename">文件名</param>
    //    /// <returns>.gif|.html格式</returns>
    //    public static string GetPostfixStr(string filename)
    //    {
    //        int start = filename.LastIndexOf(".");
    //        int length = filename.Length;
    //        string postfix = filename.Substring(start, length - start);
    //        return postfix;
    //    }
    //    #endregion

    //    #region 写文件
    //    /****************************************
    //     * 函数名称：WriteFile
    //     * 功能说明：当文件不存时，则创建文件，并追加文件
    //     * 参    数：Path:文件路径,Strings:文本内容
    //     * 调用示列：
    //     *           string Path = Server.MapPath("Default2.aspx");       
    //     *           string Strings = "这是我写的内容啊";
    //     *           DotNet.Utilities.FileOperate.WriteFile(Path,Strings);
    //    *****************************************/
    //    /// <summary>
    //    /// 写文件
    //    /// </summary>
    //    /// <param name="Path">文件路径</param>
    //    /// <param name="Strings">文件内容</param>
    //    public static void WriteFile(string Path, string Strings)
    //    {

    //        if (!System.IO.File.Exists(Path))
    //        {
    //            System.IO.FileStream f = System.IO.File.Create(Path);
    //            f.Close();
    //            f.Dispose();
    //        }
    //        System.IO.StreamWriter f2 = new System.IO.StreamWriter(Path, true, System.Text.Encoding.UTF8);
    //        f2.WriteLine(Strings);
    //        f2.Close();
    //        f2.Dispose();


    //    }
    //    #endregion

    //    #region 读文件
    //    /****************************************
    //     * 函数名称：ReadFile
    //     * 功能说明：读取文本内容
    //     * 参    数：Path:文件路径
    //     * 调用示列：
    //     *           string Path = Server.MapPath("Default2.aspx");       
    //     *           string s = DotNet.Utilities.FileOperate.ReadFile(Path);
    //    *****************************************/
    //    /// <summary>
    //    /// 读文件
    //    /// </summary>
    //    /// <param name="Path">文件路径</param>
    //    /// <returns></returns>
    //    public static string ReadFile(string Path)
    //    {
    //        string s = "";
    //        if (!System.IO.File.Exists(Path))
    //            s = "不存在相应的目录";
    //        else
    //        {
    //            StreamReader f2 = new StreamReader(Path, System.Text.Encoding.GetEncoding("gb2312"));
    //            s = f2.ReadToEnd();
    //            f2.Close();
    //            f2.Dispose();
    //        }

    //        return s;
    //    }
    //    #endregion

    //    #region 追加文件
    //    /****************************************
    //     * 函数名称：FileAdd
    //     * 功能说明：追加文件内容
    //     * 参    数：Path:文件路径,strings:内容
    //     * 调用示列：
    //     *           string Path = Server.MapPath("Default2.aspx");     
    //     *           string Strings = "新追加内容";
    //     *           DotNet.Utilities.FileOperate.FileAdd(Path, Strings);
    //    *****************************************/
    //    /// <summary>
    //    /// 追加文件
    //    /// </summary>
    //    /// <param name="Path">文件路径</param>
    //    /// <param name="strings">内容</param>
    //    public static void FileAdd(string Path, string strings)
    //    {
    //        StreamWriter sw = File.AppendText(Path);
    //        sw.Write(strings);
    //        sw.Flush();
    //        sw.Close();
    //        sw.Dispose();
    //    }
    //    #endregion

    //    #region 拷贝文件
    //    /****************************************
    //     * 函数名称：FileCoppy
    //     * 功能说明：拷贝文件
    //     * 参    数：OrignFile:原始文件,NewFile:新文件路径
    //     * 调用示列：
    //     *           string OrignFile = Server.MapPath("Default2.aspx");     
    //     *           string NewFile = Server.MapPath("Default3.aspx");
    //     *           DotNet.Utilities.FileOperate.FileCoppy(OrignFile, NewFile);
    //    *****************************************/
    //    /// <summary>
    //    /// 拷贝文件
    //    /// </summary>
    //    /// <param name="OrignFile">原始文件</param>
    //    /// <param name="NewFile">新文件路径</param>
    //    public static void FileCoppy(string OrignFile, string NewFile)
    //    {
    //        File.Copy(OrignFile, NewFile, true);
    //    }

    //    #endregion

    //    #region 删除文件
    //    /****************************************
    //     * 函数名称：FileDel
    //     * 功能说明：删除文件
    //     * 参    数：Path:文件路径
    //     * 调用示列：
    //     *           string Path = Server.MapPath("Default3.aspx");    
    //     *           DotNet.Utilities.FileOperate.FileDel(Path);
    //    *****************************************/
    //    /// <summary>
    //    /// 删除文件
    //    /// </summary>
    //    /// <param name="Path">路径</param>
    //    public static void FileDel(string Path)
    //    {
    //        File.Delete(Path);
    //    }
    //    #endregion

    //    #region 移动文件
    //    /****************************************
    //     * 函数名称：FileMove
    //     * 功能说明：移动文件
    //     * 参    数：OrignFile:原始路径,NewFile:新文件路径
    //     * 调用示列：
    //     *            string OrignFile = Server.MapPath("../说明.txt");    
    //     *            string NewFile = Server.MapPath("../../说明.txt");
    //     *            DotNet.Utilities.FileOperate.FileMove(OrignFile, NewFile);
    //    *****************************************/
    //    /// <summary>
    //    /// 移动文件
    //    /// </summary>
    //    /// <param name="OrignFile">原始路径</param>
    //    /// <param name="NewFile">新路径</param>
    //    public static void FileMove(string OrignFile, string NewFile)
    //    {
    //        File.Move(OrignFile, NewFile);
    //    }
    //    #endregion

    //    #region 在当前目录下创建目录
    //    /****************************************
    //     * 函数名称：FolderCreate
    //     * 功能说明：在当前目录下创建目录
    //     * 参    数：OrignFolder:当前目录,NewFloder:新目录
    //     * 调用示列：
    //     *           string OrignFolder = Server.MapPath("test/");    
    //     *           string NewFloder = "new";
    //     *           DotNet.Utilities.FileOperate.FolderCreate(OrignFolder, NewFloder); 
    //    *****************************************/
    //    /// <summary>
    //    /// 在当前目录下创建目录
    //    /// </summary>
    //    /// <param name="OrignFolder">当前目录</param>
    //    /// <param name="NewFloder">新目录</param>
    //    public static void FolderCreate(string OrignFolder, string NewFloder)
    //    {
    //        Directory.SetCurrentDirectory(OrignFolder);
    //        Directory.CreateDirectory(NewFloder);
    //    }

    //    /// <summary>
    //    /// 创建文件夹
    //    /// </summary>
    //    /// <param name="Path"></param>
    //    public static void FolderCreate(string Path)
    //    {
    //        // 判断目标目录是否存在如果不存在则新建之
    //        if (!Directory.Exists(Path))
    //            Directory.CreateDirectory(Path);
    //    }

    //    #endregion

    //    #region 创建目录
    //    public static void FileCreate(string Path)
    //    {
    //        FileInfo CreateFile = new FileInfo(Path); //创建文件 
    //        if (!CreateFile.Exists)
    //        {
    //            FileStream FS = CreateFile.Create();
    //            FS.Close();
    //        }
    //    }
    //    #endregion

    //    #region 递归删除文件夹目录及文件
    //    /****************************************
    //     * 函数名称：DeleteFolder
    //     * 功能说明：递归删除文件夹目录及文件
    //     * 参    数：dir:文件夹路径
    //     * 调用示列：
    //     *           string dir = Server.MapPath("test/");  
    //     *           DotNet.Utilities.FileOperate.DeleteFolder(dir);       
    //    *****************************************/
    //    /// <summary>
    //    /// 递归删除文件夹目录及文件
    //    /// </summary>
    //    /// <param name="dir"></param>  
    //    /// <returns></returns>
    //    public static void DeleteFolder(string dir)
    //    {
    //        if (Directory.Exists(dir)) //如果存在这个文件夹删除之 
    //        {
    //            foreach (string d in Directory.GetFileSystemEntries(dir))
    //            {
    //                if (File.Exists(d))
    //                    File.Delete(d); //直接删除其中的文件                        
    //                else
    //                    DeleteFolder(d); //递归删除子文件夹 
    //            }
    //            Directory.Delete(dir, true); //删除已空文件夹                 
    //        }
    //    }

    //    #endregion

    //    #region 将指定文件夹下面的所有内容copy到目标文件夹下面 果目标文件夹为只读属性就会报错。
    //    /****************************************
    //     * 函数名称：CopyDir
    //     * 功能说明：将指定文件夹下面的所有内容copy到目标文件夹下面 果目标文件夹为只读属性就会报错。
    //     * 参    数：srcPath:原始路径,aimPath:目标文件夹
    //     * 调用示列：
    //     *           string srcPath = Server.MapPath("test/");  
    //     *           string aimPath = Server.MapPath("test1/");
    //     *           DotNet.Utilities.FileOperate.CopyDir(srcPath,aimPath);   
    //    *****************************************/
    //    /// <summary>
    //    /// 指定文件夹下面的所有内容copy到目标文件夹下面
    //    /// </summary>
    //    /// <param name="srcPath">原始路径</param>
    //    /// <param name="aimPath">目标文件夹</param>
    //    public static void CopyDir(string srcPath, string aimPath)
    //    {
    //        try
    //        {
    //            // 检查目标目录是否以目录分割字符结束如果不是则添加之
    //            if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
    //                aimPath += Path.DirectorySeparatorChar;
    //            // 判断目标目录是否存在如果不存在则新建之
    //            if (!Directory.Exists(aimPath))
    //                Directory.CreateDirectory(aimPath);
    //            // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
    //            //如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
    //            //string[] fileList = Directory.GetFiles(srcPath);
    //            string[] fileList = Directory.GetFileSystemEntries(srcPath);
    //            //遍历所有的文件和目录
    //            foreach (string file in fileList)
    //            {
    //                //先当作目录处理如果存在这个目录就递归Copy该目录下面的文件

    //                if (Directory.Exists(file))
    //                    CopyDir(file, aimPath + Path.GetFileName(file));
    //                //否则直接Copy文件
    //                else
    //                    File.Copy(file, aimPath + Path.GetFileName(file), true);
    //            }
    //        }
    //        catch (Exception ee)
    //        {
    //            throw new Exception(ee.ToString());
    //        }
    //    }
    //    #endregion

    //    #region 获取指定文件夹下所有子目录及文件(树形)
    //    /****************************************
    //     * 函数名称：GetFoldAll(string Path)
    //     * 功能说明：获取指定文件夹下所有子目录及文件(树形)
    //     * 参    数：Path:详细路径
    //     * 调用示列：
    //     *           string strDirlist = Server.MapPath("templates");       
    //     *           this.Literal1.Text = DotNet.Utilities.FileOperate.GetFoldAll(strDirlist);  
    //    *****************************************/
    //    /// <summary>
    //    /// 获取指定文件夹下所有子目录及文件
    //    /// </summary>
    //    /// <param name="Path">详细路径</param>
    //    public static string GetFoldAll(string Path)
    //    {

    //        string str = "";
    //        DirectoryInfo thisOne = new DirectoryInfo(Path);
    //        str = ListTreeShow(thisOne, 0, str);
    //        return str;

    //    }

    //    /// <summary>
    //    /// 获取指定文件夹下所有子目录及文件函数
    //    /// </summary>
    //    /// <param name="theDir">指定目录</param>
    //    /// <param name="nLevel">默认起始值,调用时,一般为0</param>
    //    /// <param name="Rn">用于迭加的传入值,一般为空</param>
    //    /// <returns></returns>
    //    public static string ListTreeShow(DirectoryInfo theDir, int nLevel, string Rn)//递归目录 文件
    //    {
    //        DirectoryInfo[] subDirectories = theDir.GetDirectories();//获得目录
    //        foreach (DirectoryInfo dirinfo in subDirectories)
    //        {

    //            if (nLevel == 0)
    //            {
    //                Rn += "├";
    //            }
    //            else
    //            {
    //                string _s = "";
    //                for (int i = 1; i <= nLevel; i++)
    //                {
    //                    _s += "│&nbsp;";
    //                }
    //                Rn += _s + "├";
    //            }
    //            Rn += "<b>" + dirinfo.Name.ToString() + "</b><br />";
    //            FileInfo[] fileInfo = dirinfo.GetFiles();   //目录下的文件
    //            foreach (FileInfo fInfo in fileInfo)
    //            {
    //                if (nLevel == 0)
    //                {
    //                    Rn += "│&nbsp;├";
    //                }
    //                else
    //                {
    //                    string _f = "";
    //                    for (int i = 1; i <= nLevel; i++)
    //                    {
    //                        _f += "│&nbsp;";
    //                    }
    //                    Rn += _f + "│&nbsp;├";
    //                }
    //                Rn += fInfo.Name.ToString() + " <br />";
    //            }
    //            Rn = ListTreeShow(dirinfo, nLevel + 1, Rn);


    //        }
    //        return Rn;
    //    }



    //    /****************************************
    //     * 函数名称：GetFoldAll(string Path)
    //     * 功能说明：获取指定文件夹下所有子目录及文件(下拉框形)
    //     * 参    数：Path:详细路径
    //     * 调用示列：
    //     *            string strDirlist = Server.MapPath("templates");      
    //     *            this.Literal2.Text = DotNet.Utilities.FileOperate.GetFoldAll(strDirlist,"tpl","");
    //    *****************************************/
    //    /// <summary>
    //    /// 获取指定文件夹下所有子目录及文件(下拉框形)
    //    /// </summary>
    //    /// <param name="Path">详细路径</param>
    //    ///<param name="DropName">下拉列表名称</param>
    //    ///<param name="tplPath">默认选择模板名称</param>
    //    public static string GetFoldAll(string Path, string DropName, string tplPath)
    //    {
    //        string strDrop = "<select name=\"" + DropName + "\" id=\"" + DropName + "\"><option value=\"\">--请选择详细模板--</option>";
    //        string str = "";
    //        DirectoryInfo thisOne = new DirectoryInfo(Path);
    //        str = ListTreeShow(thisOne, 0, str, tplPath);
    //        return strDrop + str + "</select>";

    //    }

    //    /// <summary>
    //    /// 获取指定文件夹下所有子目录及文件函数
    //    /// </summary>
    //    /// <param name="theDir">指定目录</param>
    //    /// <param name="nLevel">默认起始值,调用时,一般为0</param>
    //    /// <param name="Rn">用于迭加的传入值,一般为空</param>
    //    /// <param name="tplPath">默认选择模板名称</param>
    //    /// <returns></returns>
    //    public static string ListTreeShow(DirectoryInfo theDir, int nLevel, string Rn, string tplPath)//递归目录 文件
    //    {
    //        DirectoryInfo[] subDirectories = theDir.GetDirectories();//获得目录

    //        foreach (DirectoryInfo dirinfo in subDirectories)
    //        {

    //            Rn += "<option value=\"" + dirinfo.Name.ToString() + "\"";
    //            if (tplPath.ToLower() == dirinfo.Name.ToString().ToLower())
    //            {
    //                Rn += " selected ";
    //            }
    //            Rn += ">";

    //            if (nLevel == 0)
    //            {
    //                Rn += "┣";
    //            }
    //            else
    //            {
    //                string _s = "";
    //                for (int i = 1; i <= nLevel; i++)
    //                {
    //                    _s += "│&nbsp;";
    //                }
    //                Rn += _s + "┣";
    //            }
    //            Rn += "" + dirinfo.Name.ToString() + "</option>";


    //            FileInfo[] fileInfo = dirinfo.GetFiles();   //目录下的文件
    //            foreach (FileInfo fInfo in fileInfo)
    //            {
    //                Rn += "<option value=\"" + dirinfo.Name.ToString() + "/" + fInfo.Name.ToString() + "\"";
    //                if (tplPath.ToLower() == fInfo.Name.ToString().ToLower())
    //                {
    //                    Rn += " selected ";
    //                }
    //                Rn += ">";

    //                if (nLevel == 0)
    //                {
    //                    Rn += "│&nbsp;├";
    //                }
    //                else
    //                {
    //                    string _f = "";
    //                    for (int i = 1; i <= nLevel; i++)
    //                    {
    //                        _f += "│&nbsp;";
    //                    }
    //                    Rn += _f + "│&nbsp;├";
    //                }
    //                Rn += fInfo.Name.ToString() + "</option>";
    //            }
    //            Rn = ListTreeShow(dirinfo, nLevel + 1, Rn, tplPath);


    //        }
    //        return Rn;
    //    }
    //    #endregion

    //    #region 获取文件夹大小
    //    /****************************************
    //     * 函数名称：GetDirectoryLength(string dirPath)
    //     * 功能说明：获取文件夹大小
    //     * 参    数：dirPath:文件夹详细路径
    //     * 调用示列：
    //     *           string Path = Server.MapPath("templates"); 
    //     *           Response.Write(DotNet.Utilities.FileOperate.GetDirectoryLength(Path));       
    //    *****************************************/
    //    /// <summary>
    //    /// 获取文件夹大小
    //    /// </summary>
    //    /// <param name="dirPath">文件夹路径</param>
    //    /// <returns></returns>
    //    public static long GetDirectoryLength(string dirPath)
    //    {
    //        if (!Directory.Exists(dirPath))
    //            return 0;
    //        long len = 0;
    //        DirectoryInfo di = new DirectoryInfo(dirPath);
    //        foreach (FileInfo fi in di.GetFiles())
    //        {
    //            len += fi.Length;
    //        }
    //        DirectoryInfo[] dis = di.GetDirectories();
    //        if (dis.Length > 0)
    //        {
    //            for (int i = 0; i < dis.Length; i++)
    //            {
    //                len += GetDirectoryLength(dis[i].FullName);
    //            }
    //        }
    //        return len;
    //    }
    //    #endregion

    //    #region 获取指定文件详细属性
    //    /****************************************
    //     * 函数名称：GetFileAttibe(string filePath)
    //     * 功能说明：获取指定文件详细属性
    //     * 参    数：filePath:文件详细路径
    //     * 调用示列：
    //     *           string file = Server.MapPath("robots.txt");  
    //     *            Response.Write(DotNet.Utilities.FileOperate.GetFileAttibe(file));         
    //    *****************************************/
    //    /// <summary>
    //    /// 获取指定文件详细属性
    //    /// </summary>
    //    /// <param name="filePath">文件详细路径</param>
    //    /// <returns></returns>
    //    public static string GetFileAttibe(string filePath)
    //    {
    //        string str = "";
    //        System.IO.FileInfo objFI = new System.IO.FileInfo(filePath);
    //        str += "详细路径:" + objFI.FullName + "<br>文件名称:" + objFI.Name + "<br>文件长度:" + objFI.Length.ToString() + "字节<br>创建时间" + objFI.CreationTime.ToString() + "<br>最后访问时间:" + objFI.LastAccessTime.ToString() + "<br>修改时间:" + objFI.LastWriteTime.ToString() + "<br>所在目录:" + objFI.DirectoryName + "<br>扩展名:" + objFI.Extension;
    //        return str;
    //    }
    //    #endregion

    //}

    #endregion

    #endregion
}


