
# Function Development Kit to support C# in Fn Project(FDK for C#)
The Function Development Kit(FDK) for C# provides a higher level abstraction and shields the function developer from having to write code to deal with Fn Formats. The C# FDK
enables a developer to easily build and deploy C# functions on the Fn Project platform.

##What does an FDK do?
FDK handles the protocol framing so that developers could focus on writing their functions. Fn Project already supports Java, Kotlin, Python, NodeJs, Ruby and Go FDK. The work of FDK could be divided into three broad parts - 
- Deserializing the request body into the request context and the request data.
- Executing the user's function with the request context and the request data.
- Rendering the response of the user function into a well defined response body.






