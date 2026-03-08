namespace WoochiCode.Core.Llm.OpenAI
{
    public class Message
    {
        public string Role { get; set; } = "user";

        public string Content { get; set; } = string.Empty;
    }
}
