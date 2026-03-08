using WoochiCode.Core.Vo;

namespace WoochiCode.Core.Llm.OpenAI
{
    public class OpenAIApiRequestBody
    {
        //[JsonPropertyName("")]
        public string Model { get; set; } = "local_model";

        public List<Message> Messages { get; set; } = new List<Message>();

        public float Temperature { get; set; } = 0.7f;

        public ResponseFormat? ResponseFormat { get; set; } = null;

        public List<ToolDefinition>? Tools { get; set; } = null;

        public string ToolChoice { get; set; } = "auto";
        public int MaxTokens { get; set; } = 8192;
        public bool Stream { get; set; } = false;
    }

    // =============== Tool Definition =============

    // =============== Tool Definition End =============

    // =============== ResponseFormat =============
    public class ResponseFormat
    {
        public string Type { get; set; } = "json_schema";
        public JsonSchema JsonSchema { get; set; } = new();
    }

    public class JsonSchema
    {
        public string name { get; set; } = "user_info";
        public bool strict { get; set; } = true;
        public ParameterSchema Schema { get; set; } = new();
    }

    public class ParameterSchema
    {
        public string Type { get; set; } = "object";
        public Dictionary<string, PropertyType> Properties { get; set; } = new();
        public List<string> Required { get; set; } = new();
        public bool AdditionalProperties { get; set; } = false;
    }

    public record PropertyType
    (
        string Type = ""
    );

    // =============== ResponseFormat END =============
}
