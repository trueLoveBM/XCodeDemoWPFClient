using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pis.Entity
{
    public class CommonResponse<T>
    {
        public int StatusCode { get; set; }


        public string Message { get; set; }


        public T Data { get; set; }

        /// <summary>
        /// 生成服务器校验失败消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static CommonResponse<T> ForbiddenResponse(string msg)
        {
            return new CommonResponse<T>()
            {
                StatusCode = HttpStatusCode.Forbidden,
                Message = msg
            };
        }

        /// <summary>
        /// 执行成功
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static CommonResponse<T> OKResponse(string msg, T t)
        {
            return new CommonResponse<T>()
            {
                StatusCode = HttpStatusCode.OK,
                Message = msg,
                Data = t,
            };
        }


        /// <summary>
        /// 服务器内部错误
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static CommonResponse<T> ServerErrorResponse(string msg)
        {
            return new CommonResponse<T>()
            {
                StatusCode = HttpStatusCode.Internal_Server_Error,
                Message = msg
            };
        }
    }


    public sealed class HttpStatusCode
    {
        /// <summary>
        /// 执行成功
        /// </summary>
        public const int OK = 200;


        /// <summary>
        /// 服务器已经理解请求，但是拒绝执行它
        /// </summary>
        public const int Forbidden = 403;


        /// <summary>
        /// 服务器内部错误
        /// </summary>
        public const int Internal_Server_Error = 500;


    }
}
