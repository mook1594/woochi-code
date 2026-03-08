namespace WoochiCode.Core.Vo;

public enum HookEvent
{
    OnSessionStart,
    OnSessionEnd,
    OnPromptSubmit,
    OnPreToolUse,
    OnPostToolUse,
    OnPermissionRequest,
    OnPostToolUseFailure,
    //OnNotification, //TBD
    //OnSubagentStart, //TBD
    //OnSubagentStop, //TBD
    OnStop,
    //OnTeammateIdle, // TBD
    //OnTaskCompleted, //TBD
    OnInstructionsLoaded,
    //OnConfigChange, //TBD
    //OnPreCompact //TBD
    HandleCommand,
    HandleHttp,
    HandlePrompt,
    HandleAgent,
}

public record HookContext
{
    public HookEvent Event { get; init; }
    public object? Input { get; init; }
    public ToolResult? Result { get; init; }
}


