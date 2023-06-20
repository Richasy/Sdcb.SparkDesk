using Microsoft.Extensions.Configuration;
using Xunit.Abstractions;

namespace Sdcb.SparkDesk.Tests
{
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
        public async Task Test1()
        {
            SparkDeskClient c = CreateSparkDeskClient();
            await foreach (ChatSingleResponse msg in c.ChatAsync(new ChatMessage[] { ChatMessage.FromUser("���ϵ�ʡ�����ģ�") }))
            {
                _console.WriteLine(msg);
            }
        }
    }
}