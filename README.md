# Sdcb.SparkDesk [![NuGet](https://img.shields.io/nuget/v/Sdcb.SparkDesk.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/Sdcb.SparkDesk/) [![NuGet](https://img.shields.io/nuget/dt/Sdcb.SparkDesk.svg?style=flat-square)](https://www.nuget.org/packages/Sdcb.SparkDesk/) [![GitHub](https://img.shields.io/github/license/sdcb/Sdcb.SparkDesk.svg?style=flat-square&label=license)](https://github.com/sdcb/Sdcb.SparkDesk/blob/master/LICENSE.txt)

`Sdcb.SparkDesk` is an unofficial open-source project that provides a .NET client for SparkDesk WebSocket API(https://console.xfyun.cn/services/cbm). Upstream document: https://www.xfyun.cn/doc/spark/Guide.html

It can be used to build chatbots and virtual assistants that can communicate with users in natural language.

`Sdcb.SparkDesk`��Ѷ���ǻ��ģ��WebSocket API�ķǹٷ�.NET��Դ�ͻ���(https://console.xfyun.cn/services/cbm)��

## Features

- Provides a .NET client for the SparkDesk API
- Supports both synchronous and asynchronous communication
- Implements streaming APIs for real-time communication
- Provides a simple and intuitive API for chatbot development

## Installation

`Sdcb.SparkDesk` can be installed using NuGet. To install the package, run the following command in the Package Manager Console:

```
Install-Package Sdcb.SparkDesk
```

## Usage

To use Sdcb.SparkDesk, you need to create an instance of the `SparkDeskClient` class. You can create the client by passing your SparkDesk API credentials to the constructor:

```csharp
SparkDeskClient client = new SparkDeskClient(appId, apiKey, apiSecret);
```

### Example 1: Chatting with a virtual assistant

The following example shows how to use the `ChatAsync` method to chat with a virtual assistant:

```csharp
SparkDeskClient client = new SparkDeskClient(appId, apiKey, apiSecret);
ChatResponse response = await client.ChatAsync(new ChatMessage[] 
{
    ChatMessage.FromUser("ϵͳ��ʾ�����������һ��5���к������ڽ�ɫҡ���׶�԰��ѧ�������������ģ���һ������ʦ"),
    ChatMessage.FromUser("���С���ѣ���������ʦ��������ѧ��"),
});
Console.WriteLine(response.Text);
```

### Example 2: Chatting with a virtual assistant using streaming API

The following example shows how to use the `ChatAsStreamAsync` method to chat with a virtual assistant using streaming API:

```csharp
SparkDeskClient client = new SparkDeskClient(appId, apiKey, apiSecret);
await foreach (StreamedChatResponse msg in client.ChatAsStreamAsync(new ChatMessage[] { ChatMessage.FromUser("���ϵ�ʡ�����ģ�") }, new ChatRequestParameters
{
    ChatId = "test",
    MaxTokens = 20,
    Temperature = 0.5f,
    TopK = 4,
}))
{
    Console.WriteLine(msg);
}
```

### Example 3: Chatting with a virtual assistant using streaming API and callback

The following example shows how to use the `ChatAsStreamViaCallbackAsync` method to chat with a virtual assistant using streaming API and callback:

```csharp
SparkDeskClient client = new SparkDeskClient(appId, apiKey, apiSecret);
StringBuilder sb = new();
TokensUsage usage = await client.ChatAsStreamAsync(new ChatMessage[] 
{ 
    ChatMessage.FromUser("1+1=?"),
    ChatMessage.FromAssistant("1+1=3"),
    ChatMessage.FromUser("���԰����������룿")
}, s => sb.Append(s), uid: "zhoujie");

string realResponse = sb.ToString();
Console.WriteLine(realResponse);
```

## License

Sdcb.SparkDesk is licensed under the MIT License. See the [LICENSE.txt](LICENSE.txt) file for more information.