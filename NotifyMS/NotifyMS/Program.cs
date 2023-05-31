// See https://aka.ms/new-console-template for more information

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var factory = new ConnectionFactory
{
    Uri = new Uri(""),
    ClientProvidedName="Notify app"
};

var con= factory.CreateConnection();
var model= con.CreateModel();
string exhange = "order-exchange";
string queue = "order-queue";
string routingKey = "order-routingKey";
model.ExchangeDeclare(exhange, ExchangeType.Direct);
model.QueueDeclare(queue);
model.QueueBind(queue,exhange,routingKey);
model.BasicQos(0, 1, false);

var consumer = new EventingBasicConsumer(model);

consumer.Received += (sender, e) =>
{
    var msgInBinary = e.Body.ToArray();
    var msgStrg = Encoding.UTF8.GetString(msgInBinary);
    Console.WriteLine(msgStrg);
    model.BasicAck(e.DeliveryTag, true);
};
var consumerTag= model.BasicConsume(queue, false, consumer);
model.BasicCancel(consumerTag);

model.Dispose();
con.Dispose();


Console.ReadLine();
