using Microsoft.Extensions.Configuration;
using System.Text;
using Xunit.Abstractions;

namespace Sdcb.SparkDesk.Tests;

public class UnitTest1
{
    private readonly ITestOutputHelper _console;

    static UnitTest1()
    {
        Config = new ConfigurationBuilder()
            .AddUserSecrets<UnitTest1>()
            .AddEnvironmentVariables()
            .Build();
    }

    public static IConfigurationRoot Config { get; }

    static SparkDeskClient CreateSparkDeskClient()
    {
        string? appId = Config["SparkConfig:AppId"];
        string? apiKey = Config["SparkConfig:ApiKey"];
        string? apiSecret = Config["SparkConfig:ApiSecret"];
#pragma warning disable CS8604 // �������Ͳ�������Ϊ null��
        return new SparkDeskClient(appId, apiKey, apiSecret);
#pragma warning restore CS8604 // �������Ͳ�������Ϊ null��
    }

    public UnitTest1(ITestOutputHelper console)
    {
        _console = console;
    }

    [Fact]
    public async Task ChatAsStreamAsyncTest()
    {
        SparkDeskClient c = CreateSparkDeskClient();
        await foreach (StreamedChatResponse msg in c.ChatAsStreamAsync(new ChatMessage[] { ChatMessage.FromUser("���ϵ�ʡ�����ģ�") }, new ChatRequestParameters
        {
            ChatId = "test",
            MaxTokens = 20,
            Temperature = 0.5f,
            TopK = 4,
        }))
        {
            _console.WriteLine(msg);
        }
    }

    [Fact]
    public async Task ChatAsyncTest()
    {
        SparkDeskClient c = CreateSparkDeskClient();
        ChatResponse msg = await c.ChatAsync(new ChatMessage[] 
        {
            ChatMessage.FromUser("ϵͳ��ʾ�����������һ��5���к������ڽ�ɫҡ���׶�԰��ѧ�������������ģ���һ������ʦ"),
            ChatMessage.FromUser("���С���ѣ���������ʦ��������ѧ��"),
        });
        _console.WriteLine(msg.Text);
        Assert.Contains("��ɫҡ���׶�԰", msg.Text);
    }

    [Fact]
    public async Task ChatAsStreamTest()
    {
        SparkDeskClient c = CreateSparkDeskClient();
        StringBuilder sb = new();
        TokensUsage usage = await c.ChatAsStreamAsync(new ChatMessage[] 
        { 
            ChatMessage.FromUser("1+1=?"),
            ChatMessage.FromAssistant("1+1=3"),
            ChatMessage.FromUser("���԰����������룿")
        }, s => sb.Append(s), uid: "zhoujie");

        string realResponse = sb.ToString();
        _console.WriteLine(realResponse);
        Assert.Contains("2", realResponse);
        Assert.Contains("��Ǹ", realResponse);
    }
}