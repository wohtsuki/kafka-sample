using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using testw.avro;

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


var schemaRegistryConfig = new SchemaRegistryConfig
{
    Url = "https://psrc-q2n1d.westus2.azure.confluent.cloud",
    BasicAuthUserInfo = "2FFV7QE4GXDUQTRS:dvdaIlGRy1wKyDWkC3bMrtYSw+aigKippmSt3l4hZwbxZ4zWyZL6tk36uQWcYc7w", //key:secret
    BasicAuthCredentialsSource = AuthCredentialsSource.UserInfo
};

string topic = "test1";


using (var schemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig))
{
    using var consumer = new ConsumerBuilder<string, user>(consumerConfig)
        .SetValueDeserializer(new AvroDeserializer<user>(schemaRegistry).AsSyncOverAsync())
        .Build();
    consumer.Subscribe(topic);

    while (true)
    {
        var consumerResult = consumer.Consume();
        var usr = consumerResult.Message.Value;

        Console.WriteLine($"User received user: {usr.firstname} {usr.lastname}");
    }
}
