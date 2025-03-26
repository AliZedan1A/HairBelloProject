using System.Text.Json.Serialization;

namespace ServerSide.Services.WhatsappSender.ApiDefinition.Responses;
public record SendMessageError
{
	[JsonPropertyName("sent")] public string Sent { get; set; } = string.Empty;
	[JsonPropertyName("message")] public string Message = string.Empty;
}

public record SendMessageSucces
{
	[JsonPropertyName("sent")] public string Sent { get; set; } = string.Empty;
	[JsonPropertyName("from")] public string From { get; set; } = string.Empty;
	[JsonPropertyName("message_type")] public string MessageType { get; set; } = string.Empty;

	[JsonPropertyName("messageId")] public string MessageId { get; set; } = string.Empty;
}