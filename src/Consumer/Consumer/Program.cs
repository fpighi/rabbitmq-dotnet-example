namespace Consumer_step_1
{
    class Program
    {
        static void Main(string[] args) {
            using (var messageReader = new MessageReader()) {
                var routingKey = "hello";
                
                if (args.Length > 0) {
                    routingKey = args[0];
                }

                messageReader.ConsumeMessages(routingKey);
            }
        }
    }
}
