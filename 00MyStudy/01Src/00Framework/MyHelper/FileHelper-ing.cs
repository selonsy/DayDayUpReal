using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Devin
{
    public static class FileHelper
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        public static void UpLoad() 
        {
            
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        public static void DownLoad() 
        {
            
        }

        /// <summary>
        /// 使用WriteFile下载文件  
        /// </summary>
        /// <param name="filePath">相对路径</param>
        public static void WriteFile(string filePath)
        {
            try
            {
                //filePath = System.Web.HttpContext.Current.Server.MapPath(filePath);
                if (File.Exists(filePath))
                {
                    FileInfo info = new FileInfo(filePath);
                    long fileSize = info.Length;
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ContentType = "application/octet-stream";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachement;filename=" + System.Web.HttpContext.Current.Server.UrlEncode(info.FullName));
                    //指定文件大小   
                    HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());                    
                    HttpContext.Current.Response.WriteFile(filePath, 0, fileSize);
                    HttpContext.Current.Response.Flush();
                }
            }
            catch(Exception ex)
            {
                LogHelper.WriteError(ex);
            }
            finally
            {
                HttpContext.Current.Response.Close();
            }
        }

        /// <summary>
        /// 使用微软的TransmitFile下载文件
        /// </summary>
        /// <param name="filePath">服务器相对路径</param>
        public static void TransmitFile(string filePath)
        {
            try
            {
                filePath = System.Web.HttpContext.Current.Server.MapPath(filePath);
                if (File.Exists(filePath))
                {
                    FileInfo info = new FileInfo(filePath);
                    long fileSize = info.Length;
                    HttpContext.Current.Response.Clear();

                    //指定Http Mime格式为压缩包
                    HttpContext.Current.Response.ContentType = "application/x-zip-compressed";

                    // Http 协议中有专门的指令来告知浏览器, 本次响应的是一个需要下载的文件. 格式如下:
                    // Content-Disposition: attachment;filename=filename.txt
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpContext.Current.Server.UrlEncode(info.FullName));
                    //不指明Content-Length用Flush的话不会显示下载进度   
                    HttpContext.Current.Response.AddHeader("Content-Length", fileSize.ToString());
                    HttpContext.Current.Response.TransmitFile(filePath, 0, fileSize);
                    HttpContext.Current.Response.Flush();
                }
            }
            catch
            { }
            finally
            {
                HttpContext.Current.Response.Close();
            }
        }

        /// <summary>
        /// 使用OutputStream.Write分块下载文件  
        /// </summary>
        /// <param name="filePath"></param>
        public static void WriteFileBlock(string filePath)
        {
            filePath = System.Web.HttpContext.Current.Server.MapPath(filePath);
            if (!File.Exists(filePath))
            {
                return;
            }
            FileInfo info = new FileInfo(filePath);
            //指定块大小   
            long chunkSize = 4096;
            //建立一个4K的缓冲区   
            byte[] buffer = new byte[chunkSize];
            //剩余的字节数   
            long dataToRead = 0;
            FileStream stream = null;
            try
            {
                //打开文件   
                stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

                dataToRead = stream.Length;

                //添加Http头   
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachement;filename=" + System.Web.HttpContext.Current.Server.UrlEncode(info.FullName));
                HttpContext.Current.Response.AddHeader("Content-Length", dataToRead.ToString());

                while (dataToRead > 0)
                {
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        int length = stream.Read(buffer, 0, Convert.ToInt32(chunkSize));
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.Clear();
                        dataToRead -= length;
                    }
                    else
                    {
                        //防止client失去连接   
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("Error:" + ex.Message);
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
                HttpContext.Current.Response.Close();
            }

        }

        /// <summary>
        /// 上传文件，使用.NET自带控件
        /// </summary>
        public static void UpLoadByControl()
        {
            //if (FileUpload1.HasFile)
            //{
            //    if (FileUpload1.PostedFile.ContentType.Substring(0, 5) == "image")
            //    {
            //        try
            //        {
            //            string serverPath = System.Web.HttpContext.Current.Server.MapPath("upLoad");
            //            if (!System.IO.Directory.Exists(serverPath))
            //            {
            //                System.IO.Directory.CreateDirectory(serverPath);
            //            }
            //            string imgName = FileUpload1.FileName;//获取上传文件的名称
            //            string newPath = serverPath + "\\" + imgName;//设置图片在服务器端的新路径
            //            FileUpload1.SaveAs(newPath);
            //            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('上传成功！');", true);
            //            img.ImageUrl = "upLoad/" + imgName;//显示上传的图片
            //            txtinfo.Text = "";
            //            txtinfo.Text += "路径：" + FileUpload1.PostedFile.FileName + "\n" +
            //                            "大小：" + FileUpload1.PostedFile.ContentLength / 1024 + "KB" + "\n" +
            //                            "类型：" + FileUpload1.PostedFile.ContentType;
            //        }
            //        catch
            //        {
            //            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('上传失败！');", true);
            //        }
            //    }
            //    else
            //    {
            //        ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请选择图片！');", true);
            //    }
            //}
            //else
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请选择要上传的图片！');", true);
            //}
        }
        
        /// <summary>
        /// 输出硬盘文件，提供下载 支持大文件、续传、速度限制、资源占用小
        /// </summary>
        /// <param name="_Request">;Page.Request对象</param>
        /// <param name="_Response">;Page.Response对象</param>
        /// <param name="_fileName">下载文件名</param>
        /// <param name="_fullPath">带文件名下载路径</param>
        /// <param name="_speed">每秒允许下载的字节数</param>
        /// <returns>返回是否成功</returns>
        /// <example>
        /// string FullPath=System.Web.HttpContext.Current.Server.MapPath("count.txt");
        /// ResponseFile(this.Request,this.Response,"count.txt",FullPath,100);
        /// </example>
        public static bool ResponseFile(HttpRequest _Request, HttpResponse _Response, string _fileName, string _fullPath, long _speed)
        {
            try
            {
                FileStream myFile = new FileStream(_fullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                BinaryReader br = new BinaryReader(myFile);
                try
                {
                    _Response.AddHeader("Accept-Ranges", "bytes");
                    _Response.Buffer = false;

                    long fileLength = myFile.Length;
                    long startBytes = 0;
                    int pack = 10240;  //10K bytes
                    int sleep = (int)Math.Floor((double)(1000 * pack / _speed)) + 1;

                    if (_Request.Headers["Range"] != null)
                    {
                        _Response.StatusCode = 206;
                        string[] range = _Request.Headers["Range"].Split(new char[] { '=', '-' });
                        startBytes = Convert.ToInt64(range[1]);
                    }
                    _Response.AddHeader("Content-Length", (fileLength - startBytes).ToString());
                    if (startBytes != 0)
                    {
                        _Response.AddHeader("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));
                    }

                    _Response.AddHeader("Connection", "Keep-Alive");
                    _Response.ContentType = "application/octet-stream";
                    _Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(_fileName, System.Text.Encoding.UTF8));

                    br.BaseStream.Seek(startBytes, SeekOrigin.Begin);
                    int maxCount = (int)Math.Floor((double)((fileLength - startBytes) / pack)) + 1;

                    for (int i = 0; i < maxCount; i++)
                    {
                        if (_Response.IsClientConnected)
                        {
                            _Response.BinaryWrite(br.ReadBytes(pack));
                            Thread.Sleep(sleep);
                        }
                        else
                        {
                            i = maxCount;
                        }
                    }
                }
                catch
                {
                    return false;
                }
                finally
                {
                    br.Close();
                    myFile.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}
