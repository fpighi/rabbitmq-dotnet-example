using System;
using System.Collections.Generic;
using System.Threading;

namespace Publisher
{
    class Program
    {
        public static void Main(string[] args) {
            const int intervalInSeconds = 2;
            var possibleMessages = new List<Message> {
                                                         new Message { Topic = "hello", Body = "hello rabbit!" },
                                                         new Message { Topic = "example.hello", Body = "hello" },
                                                         new Message { Topic = "example.goodbye", Body = "goodbye" },
                                                         new Message { Topic = "example.badger", Body = "badger" },
                                                         new Message { Topic = "example.elephant", Body = "elephant" },
                                                     };

            Console.WriteLine(" [*] Publishing random messages. To exit press CTRL+C.");

            using (var publisher = new Publisher())
            {
                while (true) {
                    var message = PickRandomMessage(possibleMessages);
                    publisher.DeclareExchangeAndPutMessage(message.Topic, message.Body);
                    Thread.Sleep(intervalInSeconds * 1000);
                }
            }
        }

        private static Message PickRandomMessage(IList<Message> possibleMessages) {
            var index = new Random().Next(possibleMessages.Count - 1);
            return possibleMessages[index];
        }
    }

    public class Message
    {
        public string Topic { get; set; }
        public string Body { get; set; }
    }
}
