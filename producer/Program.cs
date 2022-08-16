using Confluent.Kafka;

var producerConfig = new ProducerConfig
{
    BootstrapServers = "pkc-12pd3.brazilsouth.azure.confluent.cloud:9092",
    SecurityProtocol = SecurityProtocol.SaslSsl,
    SaslMechanism = SaslMechanism.Plain,
    SaslUsername = "OWCJQBDQ2GNOQRAW",
    SaslPassword = "vKizS6BMS3UoB8YW9d65xGsxrj1EEnNkEVvAEOtnP7HIi6uuTK7bbaxE9GizRqR8"
};




var producer = new ProducerBuilder<string, string>(producerConfig).Build();
string topic = "test1";



while (true)
{
    string guidKey = Guid.NewGuid().ToString();
    Message<string, string> msg = new()
    {
        Key = guidKey,
        Value = Faker.Name.FirstName()
    };
    await producer.ProduceAsync(topic, msg);

    Console.WriteLine($"key: {guidKey}, value: {msg.Value}");
    Thread.Sleep(1000);
}
