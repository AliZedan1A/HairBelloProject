using System.Text.Json.Serialization;

namespace ServerSide.Services.WhatsappSender.ApiDefinition.Requests;

public class SendMessageRequest : BaseRequest
{

	private SendMessageRequest(string phoneNumberOrGroupId,
		bool isGroup,
		string message,
		string mentionIds,
		string quotedMessagesIds)
	{
		PhoneNumberOrGroupId = phoneNumberOrGroupId;
		IsGroup = isGroup;
		Message = message;
		MentionIds = mentionIds;
		QuotedMessagesIds = quotedMessagesIds;
	}
	public static SendMessageRequest SendPhoneMessage(string phone ,string msg)
	{
		return new SendMessageRequest(phone,false,msg,"","");
	}
	[JsonPropertyName("phone_number_or_group_id")] public string PhoneNumberOrGroupId { get; set; }
	[JsonPropertyName("is_group")] public bool IsGroup { get; set; }
	[JsonPropertyName("message")] public string Message { get; set; }
	[JsonPropertyName("mentioned_ids")] public string MentionIds { get; set; }
	[JsonPropertyName("quoted_message_id")] public string QuotedMessagesIds { get; set; }
}