using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace FDK
{
    public class InvokeClass
    {
        public static IServiceCollection services;
        public static string _functionName="";
        public static Type _functionType;
        public static object _functionInstance;
        private static short Flag = 0;

        public static MethodInfo _userFunction;

        public static ParameterInfo[] _parameters;
        public static ParameterInfo _returnType;
        public static void HandlerFunc(Type functionType, object functionInstance)
        {
            //This is the entry point where the user function will first enter the FDK
            FunctionDissection(functionType,functionInstance);
            Server.CreateHostBuilder(new ContainerEnvironment()).Build().Run();
        }

        //Call this function to get the user function code
        public static object RunUserFunction(IRequestContext ctx, FunctionInput input)
        {
            if(_parameters.Length==0)
            {
                return _userFunction.Invoke(_functionInstance,null);
            }
            else
            {
                object[] listOfParameters = new object[_parameters.Length];
                //The first parameter is the Request context.
                listOfParameters[0]=ctx;
                //The second parameter is the Request data.
                // When the user can inject this Request data by making a POST request using "CURL" or by echo "Value"
                listOfParameters[1]=input;
                return _userFunction.Invoke(_functionInstance,listOfParameters);
            }
        }

        public static void FunctionDissection(Type functionType, object functionInstance)
        {
            _functionType = functionType;
            //Getting an array of methods
            MethodInfo[] methodInfos =  _functionType.GetMethods(BindingFlags.Public | BindingFlags.Static);
            foreach(MethodInfo val in methodInfos)
            {
                if(val.Name !="Equals" && val.Name!="GetHashCode" && val.Name!="GetType" && val.Name!="Tostring")
                {
                    _functionName = val.Name;
                    Flag=1;
                    break;
                }
            }
            if(Flag == 0)
            {
                Console.WriteLine("No user defined public and static function was found");
            }
            if(Flag==0)
            {
                methodInfos =_functionType.GetMethods(BindingFlags.Public | BindingFlags.Instance);
                foreach(MethodInfo val in methodInfos)
                {
                    if(val.Name !="Equals" && val.Name!="GetHashCode" && val.Name!="GetType" && val.Name!="Tostring")
                    {
                        _functionName = val.Name;
                        break;
                    }
                }
            }
            if(String.IsNullOrEmpty(_functionName))
            {
                Console.WriteLine("No user defined function was found");
            }
            _functionInstance = functionInstance;
            //get the user's function
            _userFunction = _functionType.GetMethod(_functionName);
            if(_userFunction != null)
            {
                //get the array of parameters
                _parameters = _userFunction.GetParameters();
                //get the return type of the user's function
                _returnType = _userFunction.ReturnParameter;
            }
            else
            {
                throw new NullReferenceException("_userFunction is empty");
            }
        }
    }
}