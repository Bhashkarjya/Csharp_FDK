using System.Runtime.InteropServices;
using System;
using FDK.Exceptions;

namespace FDK
{
    public static class ContainerEnvironmentValidator
    {
        public static void Validate(IContainerEnvironment containerEnvironment)
        {
            if(containerEnvironment.FN_FORMAT != "http-stream")
            {
                throw new InvalidEnvironmentException("http-stream is the only supported format");
            }
            if(containerEnvironment.SOCKET_TYPE.ToLower() != "unix")
            {
                throw new InvalidEnvironmentException("FDK is compliant with only Unix Domain Sockets");
            }
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                throw new InvalidEnvironmentException("Unix domain sockets are not supported on Windows");
            }
        }
    }
}