using Castle.DynamicProxy;
using RetrofitFrame.RetrofitAttribute;
using RetrofitFrame.RetrofitCahce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RetrofitFrame
{

    public class ServiceInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            ServiceRquestInfo rquestInfo = new ServiceRquestInfo();
            IList<System.Reflection.CustomAttributeData> customAttributeDatas = invocation.Method.GetCustomAttributesData();
            for (int j = 0; j < customAttributeDatas.Count; j++)
            {
                System.Reflection.CustomAttributeData customAttribute = customAttributeDatas[j];
                if (customAttribute.AttributeType == typeof(POSTAttribute))
                {
                    rquestInfo.HttpMethod = HttpMethod.Post;
                    if (customAttribute.ConstructorArguments[0].Value is string)
                        rquestInfo.Url = customAttribute.ConstructorArguments[0].Value as string;
                }
                else if (customAttribute.AttributeType == typeof(GETAttribute))
                {
                    rquestInfo.HttpMethod = HttpMethod.Get;
                    if (customAttribute.ConstructorArguments[0].Value is string)
                        rquestInfo.Url = customAttribute.ConstructorArguments[0].Value as string;

                }
                else if (customAttribute.AttributeType == typeof(HeaderAttribute))
                {
                    if (customAttribute.ConstructorArguments[0].Value is string)
                        rquestInfo.Header.Add(customAttribute.ConstructorArguments[0].Value as string);
                }
                else if (customAttribute.AttributeType == typeof(UploadFileAttribute))
                {
                    rquestInfo.IsUploadFile = true;
                }

            }

            if (invocation.Arguments.Count() > 0)//这个请求方法有传参
            {
                //获取参数
                ParameterInfo[] parameterInfos = invocation.Method.GetParameters();
                for (int j = 0; j < parameterInfos.Length; j++)
                {
                    ParameterInfo parameter = parameterInfos[j];
                    IEnumerable<Attribute> attributes = parameter.GetCustomAttributes();
                    if (attributes.Count() == 0)//这个参数没有使用我们的特性，过滤掉这个参数值
                        continue;
                    string paramName = parameter.Name;//参数原始名字
                    object paramValue = invocation.Arguments[j];//参数值
                    for (int z = 0; z < attributes.Count(); z++)
                    {
                        Attribute a = attributes.ElementAt(z);
                        //Query属性
                        if (a is QueryAttribute)
                        {
                            if (!(paramValue is string))
                            {
                                throw new Exception("Query的请求非String");
                            }

                            QueryAttribute query = a as QueryAttribute;
                            string cfgParamName = query.ParamName;//在自定义特性中定义的属性名
                            if (string.IsNullOrEmpty(cfgParamName))//如果没有配置Query的参数名，默认使用参数的㡳名称
                            {
                                rquestInfo.Param.Add(paramName, paramValue as string);
                            }
                            else
                            {
                                rquestInfo.Param.Add(cfgParamName, paramValue as string);
                            }

                        }
                        else if (a is BodyAttribute)
                        {
                            BodyAttribute body = a as BodyAttribute;
                            //将这个参数序列化成json
                            string json = Newtonsoft.Json.JsonConvert.SerializeObject(paramValue);
                            rquestInfo.Body = json;

                        }
                        else if (a is DynamicHeaderAttribute)
                        {
                            DynamicHeaderAttribute dynamicHeader = a as DynamicHeaderAttribute;
                            string headername = dynamicHeader.Header;
                            if (string.IsNullOrEmpty(headername))//如果没有配置Header的参数名，默认使用参数的㡳名称
                                rquestInfo.Header.Add(paramName + ":" + paramValue);
                            else
                                rquestInfo.Header.Add(headername + ":" + paramValue);
                        }
                        else if (a is FilePathAttribute)
                        {
                            rquestInfo.FilePath = paramValue as string;
                        }
                    }

                }
            }


            Console.WriteLine("拦截了");
            Call call = new Call();
            call.RquestInfo = rquestInfo;
            call.Msg = "测试";
            call.Client = RetrofitClientCache.Instance.GetClient(invocation.Proxy.GetHashCode());
            invocation.ReturnValue = call;
            //不用实现方法
            //invocation.Proceed();
        }
    }
}
