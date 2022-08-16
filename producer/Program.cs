using Confluent.Kafka;
using producer;
using System.Text.Json;

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
    User user = new()
    {
        Id = Guid.NewGuid().ToString(),
        Firstname = Faker.Name.FirstName(),
        Lastname = Faker.Name.LastName(),   
    };

    Message<string, string> msg = new()
    {
        Key = user.Id,
        Value = JsonSerializer.Serialize(user)
    };
    await producer.ProduceAsync(topic, msg);

    Console.WriteLine($"User created key: {user.Id}, {user.Firstname} {user.Lastname}");
    Thread.Sleep(1000);
}
