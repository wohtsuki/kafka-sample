using Confluent.Kafka;

var consumerConfig = new ConsumerConfig
{
    BootstrapServers = "pkc-12pd3.brazilsouth.azure.confluent.cloud:9092",
    SecurityProtocol = SecurityProtocol.SaslSsl,
    SaslMechanism = SaslMechanism.Plain,
    SaslUsername = "OWCJQBDQ2GNOQRAW",
    SaslPassword = "vKizS6BMS3UoB8YW9d65xGsxrj1EEnNkEVvAEOtnP7HIi6uuTK7bbaxE9GizRqR8",
    GroupId = "quantico.example",
    AutoOffsetReset = AutoOffsetReset.Earliest,
};

string topic = "test1";

using (var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build())
{
    consumer.Subscribe(topic);

    while(true)
    {
        var consumerResult = consumer.Consume();
        var value = consumerResult.Message.Value;
        Console.WriteLine($"Received user: {value}");
        Thread.Sleep(500);
    }
}
    

