
# Function Development Kit to support C# in Fn Project(FDK for C#)
The Function Development Kit(FDK) for C# provides a higher level abstraction and shields the function developer from having to write code to deal with Fn Formats. The C# FDK
enables a developer to easily build and deploy C# functions on the Fn Project platform.

## What does an FDK do?
FDK handles the protocol framing so that developers could focus on writing their functions. Fn Project already supports Java, Kotlin, Python, NodeJs, Ruby and Go FDK. The work of FDK could be divided into three broad parts - 
- Deserializing the request body into the request context and the request data.
- Executing the user's function with the request context and the request data.
- Rendering the response of the user function into a well defined response body.

## Features of the C# FDK
- Parsing the input in raw data or Json format.
- Rendering the output in raw data or Json format.
- Using Unix Domain Sockets to handle requests.
- Using [Xunit](https://xunit.net/) and [NSubstitute](https://nsubstitute.github.io/help/getting-started/) Nuget Packages to do unit testing.

## Learn about the Fn Project
If you are new to Fn Project, you should start out with the [official documentation](https://fnproject.io/tutorials/).

## Using the C# FDK
  ### Install Docker and Fn
  You need to have Docker and Fn installed in your device. Visit [Install Fn](https://fnproject.io/tutorials/install/) to learn how to install Fn and start an Fn server.
  ### Start the Fn Server
  Type the following command in the terminal. This will start the fn server.
  ```
  fn start
  ```
  If you have correctly followed the installation procedure, your terminal will look like this-
  
  ### Clone the repository
  - Clone the [Csharp_FDK repository](https://github.com/Bhashkarjya/Csharp_FDK) using the git clone command
  ``` 
  git clone https://github.com/Bhashkarjya/Csharp_FDK.git 
  ```
  - Move inside the Csharp_FDK directory. The tree structure of the directory would look like this - 
  ```
  cd Csharp_FDK
  ls
  ```
  - Again navigate inside the FDK sub directory. The tree structure would look like this - 
  ```
  cd FDK
  ls
  ```
  
  ### Check your context
  Check your context - Make sure your context is set to default and you are using a demo user. Use the fn list contexts command to check.If your context is not configured,       
  please see the [context installation instructions](https://github.com/fnproject/tutorials/blob/master/install/README.md#configure-your-context) before proceeding.
  ``` 
  fn list contexts 
  ```
  
  ### Create an App
  Create an app - Apps are the first class citizens in Fn Project. All the functions are grouped inside an application. To create an app type the following - 
  ``` 
  fn create app dotnet_fdk
  ```
  This creates an app with the name "dotnet_fdk"
  A confirmation is returned:
  //Insert image
  
  ### Deploy your function to your app
  - Deploy your Function to your App - Deploying your function is how you publish your function and make it accessible to other users and systems. Type this command -
  ``` 
  fn deploy --verbose --app dotnet_fdk --local
  ```
  - The ```--verbose``` option allows you to see all the steps of deploying the function.
  - The ```--local``` option indicates that you are deploying the function locally in your device and not in the Docker hub.
  - Specifying ``` --app dotnet_fdk``` explicitly puts your function inside the dotnet_fdk app.
  - To check if your function resides within the dotnet_fdk app, type this command in the terminal
  ```
  fn list functions dotnet_fdk
  ```
  ### Invoke your deployed function
  There are two ways of invoking your deployed function.
  #### Invoke with the CLI
  Type the following command-
  ```
  fn invoke dotnet_fdk fdk
  ```
  which returns - 
  //Insert image
  On running this command the fn server will look inside the dotnet_fdk application and then look for the Docker image bound to the "fdk" function and execute the code.
  You can also pass raw stream and Json input in your function. We will talk about that later.
  
  #### Invoke using curl
  Functions are exposed by using triggers. To list the functions included inside "dotnet_fdk" we can use
  ```
  fn list triggers dotnet_fdk
  ```
  This shows that "fdk" function can be invoked using this url
  Use curl to invoke the function 
  ```
  curl http://localhost:8080/t/dotnet_fdk/fdk-trigger
  ```
  which returns - 
   //Insert image

## Exploring functions and the different data formats
  Navigate inside the UserFunction subdirectory. It contains a bunch of example user function files and a Program.cs file. The three structure looks 
  similar to this.
  ``` 
  cd UserFunction 
  ```
  ### Raw data as input
  Open the Example.cs file. The input is given as a raw string. To differentiate between different parameters, use the defined delimiters-
  " ", ",", "'", "(",")". 
  In order to access "Kevin", use GetValue("arg1"), similarly to access "18", use GetValue("arg2") and so on.
  ```
  name = input.GetValue("arg1");
  age = input.GetValue("arg2");
  ```
  Inside the Program.cs file - 
  //Insert the image
  To execute this Example.cs file change the parameter inside executingAssembly.GetType("") to the full name of the user-defined class.
  ```
  Type functionType = executingAssembly.GetType("UserFunction.Example");
  ```
  Invoking the deployed function with Raw data-
  ```
  echo "Kevin 18 India" | fn invoke dotnet_fdk fdk
  ```
   
  ### JSON as input
  In order to pass JSON as an input to your function and to return a JSON object as a response, we need to make the following changes to our code.
  //Insert Image
   
  Inside the Programm.cs file - 
  To execute this JsonExample.cs file change the parameter inside executingAssembly.GetType("") to the full name of the user-defined class.
  ```
  Type functionType = executingAssembly.GetType("UserFunction.JsonExample");
  ```
  Invoking the deployed function with JSON input
  ```
    echo '{"Name":"Daniel","Age":18,"Subjects":["OS","CN"]}' | fn invoke dotnetFDK fdk 
  ```

