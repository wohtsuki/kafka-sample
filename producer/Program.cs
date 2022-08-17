using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using testw.avro;

var producerConfig = new ProducerConfig
{
    BootstrapServers = "pkc-12pd3.brazilsouth.azure.confluent.cloud:9092",
    SecurityProtocol = SecurityProtocol.SaslSsl,
    SaslMechanism = SaslMechanism.Plain,
    SaslUsername = "OWCJQBDQ2GNOQRAW",
    SaslPassword = "vKizS6BMS3UoB8YW9d65xGsxrj1EEnNkEVvAEOtnP7HIi6uuTK7bbaxE9GizRqR8"
};

var schemaRegistryConfig = new SchemaRegistryConfig
{
    Url = "https://psrc-q2n1d.westus2.azure.confluent.cloud",
    BasicAuthUserInfo = "2FFV7QE4GXDUQTRS:dvdaIlGRy1wKyDWkC3bMrtYSw+aigKippmSt3l4hZwbxZ4zWyZL6tk36uQWcYc7w", //key:secret
    BasicAuthCredentialsSource = AuthCredentialsSource.UserInfo
};

var shemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);


var producer = new ProducerBuilder<string, user>(producerConfig)
    .SetValueSerializer(new AvroSerializer<user>(shemaRegistry))
    .Build();
string topic = "test1";



while (true)
{
    user usr = new()
    {
        firstname = Faker.Name.FirstName(),
        lastname = Faker.Name.LastName(),   
    };

    Message<string, user> msg = new()
    {
        Key = Guid.NewGuid().ToString(),
        Value = usr
    };
    await producer.ProduceAsync(topic, msg);

    Console.WriteLine($"User created key: {msg.Key}, {usr.firstname} {usr.lastname}");
    Thread.Sleep(1000);
}
