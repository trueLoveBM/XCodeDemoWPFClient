using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RetrofitFrame
{
    public class Call
    {

        public Retrofit Client { get; set; }

        public ServiceRquestInfo RquestInfo { get; set; }

        public string Msg { get; set; }

        /// <summary>
        /// 异步调取接口，返回结果
        /// </summary>
        public void Enqueue()
        {

        }

        /// <summary>
        /// 文件上传接口
        /// 由于服务器接口定义的不同,这里没有意义
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [Obsolete]
        private Respone<T> uploadFileApiExecute<T>()
        {
            if (string.IsNullOrEmpty(RquestInfo.FilePath))
                throw new Exception("未指定文件路径，请在接口调用中使用");

            if (File.Exists(RquestInfo.FilePath))
                throw new Exception("未找到文件，请使用正确的文件路径");

            Respone<T> t = new Respone<T>();
            //边界
            string boundary = DateTime.Now.Ticks.ToString("x");
            //准备url地址
            StringBuilder sb = new StringBuilder();
            sb.Append(Client.Url);
            sb.Append(RquestInfo.Url);
            //创建uploadfile对象
            HttpWebRequest uploadRequest = (HttpWebRequest)WebRequest.Create(sb.ToString());//url为上传的地址
            if (RquestInfo.HttpMethod == RetrofitAttribute.HttpMethod.Get)
                throw new Exception("文件上传必须使用POST请求");
            uploadRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            uploadRequest.Method = RquestInfo.HttpMethod.ToString();
            uploadRequest.Accept = "*/*";
            uploadRequest.KeepAlive = true;
            uploadRequest.Credentials = System.Net.CredentialCache.DefaultCredentials;

            //此服务器/retrofit 的header固定添加
            if (Client.Header?.Count > 0)
            {
                foreach (var item in Client.Header)
                {
                    uploadRequest.Headers.Add(item);
                }
            }

            //方法内固定header和动态header添加
            if (RquestInfo.Header.Count > 0)
            {
                foreach (var item in RquestInfo.Header)
                {
                    uploadRequest.Headers.Add(item);
                }
            }

            //创建一个内存流
            Stream memStream = new MemoryStream();
            boundary = "--" + boundary;

            //添加上传文件参数格式边界
            string paramFormat = boundary + "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}\r\n";
            NameValueCollection param = new NameValueCollection();
            param.Add("fname", Guid.NewGuid().ToString() + Path.GetExtension(RquestInfo.FilePath));

            //写上参数
            foreach (string key in param.Keys)
            {
                string formitem = string.Format(paramFormat, key, param[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                memStream.Write(formitembytes, 0, formitembytes.Length);
            }

            //添加上传文件数据格式边界
            string dataFormat = boundary + "\r\nContent-Disposition: form-data; name=\"{0}\";filename=\"{1}\"\r\nContent-Type:application/octet-stream\r\n\r\n";
            string header = string.Format(dataFormat, "Filedata", Path.GetFileName(RquestInfo.FilePath));
            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
            memStream.Write(headerbytes, 0, headerbytes.Length);


            //获取文件内容
            FileStream fileStream = new FileStream(RquestInfo.FilePath, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[1024];
            int bytesRead = 0;


            //将文件内容写进内存流
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                memStream.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            //添加文件结束边界
            byte[] boundarybytes = System.Text.Encoding.UTF8.GetBytes("\r\n\n" + boundary + "\r\nContent-Disposition: form-data; name=\"Upload\"\r\n\nSubmit Query\r\n" + boundary + "--");
            memStream.Write(boundarybytes, 0, boundarybytes.Length);


            //设置请求长度
            uploadRequest.ContentLength = memStream.Length;
            //获取请求写入流
            Stream requestStream = uploadRequest.GetRequestStream();

            //将内存流数据读取位置归零
            memStream.Position = 0;
            byte[] tempBuffer = new byte[memStream.Length];
            memStream.Read(tempBuffer, 0, tempBuffer.Length);
            memStream.Close();

            //将内存流中的buffer写入到请求写入流
            requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            requestStream.Close();

            //获取到上传请求的响应
            HttpWebResponse response = (HttpWebResponse)uploadRequest.GetResponse();

            return t;

        }


        /// <summary>
        /// 同步调取接口，返回结果
        /// </summary>
        public Respone<T> Execute<T>()
        {
            //if (RquestInfo.IsUploadFile)
            //{
            //    return uploadFileApiExecute<T>();
            //}
            //else
            //{
            return commonApiExecute<T>();
            //}
        }

        private Respone<T> commonApiExecute<T>()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Client.Url);
            sb.Append(RquestInfo.Url);

            if (RquestInfo.Param.Keys.Count > 0)
            {
                sb.Append("?");
                for (int i = 0; i < RquestInfo.Param.Keys.Count; i++)
                {
                    var paramName = RquestInfo.Param.Keys.ElementAt(i);
                    var paramValue = RquestInfo.Param[paramName];
                    sb.Append(paramName);
                    sb.Append("=");
                    sb.Append(paramValue);
                    if (i != RquestInfo.Param.Keys.Count-1)
                    {
                        sb.Append("&");
                    }
                }
            }
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sb.ToString());
            request.Method = RquestInfo.HttpMethod.ToString();
            request.Accept = Client.Accept;
            request.ContentType = Client.ContentType;

            //此服务器/retrofit 的header固定添加
            if (Client.Header?.Count > 0)
            {
                foreach (var item in Client.Header)
                {
                    string[] info = item.Split(':');
                    request.Headers.Add(info[0], info[1]);
                }
            }

            //方法内固定header和动态header添加
            if (RquestInfo.Header.Count > 0)
            {
                foreach (var item in RquestInfo.Header)
                {
                    request.Headers.Add(item);
                }
            }


            if (!string.IsNullOrEmpty(RquestInfo.Body))
            {
                byte[] buffer = encoding.GetBytes(RquestInfo.Body);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
            }
            else
            {
                request.ContentLength = 0;
            }


            Respone<T> respone = new Respone<T>();

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {

                    respone.Code = response.StatusCode;
                    respone.IsSuccess = true;
                    respone.Msg = "接口调取成功";
                    string dataStr = reader.ReadToEnd();
                    respone.Data = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(dataStr);
                }


            }
            catch (WebException ex)
            {
                respone.IsSuccess = false;
                HttpWebResponse response = (HttpWebResponse)ex.Response;
                if (respone != null)
                {
                    respone.Code = response.StatusCode;
                    respone.Msg = ex.Message + "\t" + response.StatusDescription;
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        try
                        {
                            string dataStr = reader.ReadToEnd();
                            respone.Data = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(dataStr);
                        }
                        catch (Exception ex2)
                        {
                            respone.Msg += "\t" + ex2.Message;
                        }
                    }
                }
                else
                {
                    respone.Code = HttpStatusCode.NotFound;
                    respone.Msg = "未得到服务器的相应";
                }

            }
            return respone;
        }
    }
}
