using System.Text.Json.Serialization;

namespace ServerSide.Services.WhatsappSender.ApiDefinition.Requests;

public class HasWhatsappRequest: BaseRequest
{
	private HasWhatsappRequest(string phone)
	{
		Phone = phone;
	}
	
	public static HasWhatsappRequest Create(string phone)
	{
		return new HasWhatsappRequest(phone);
	}
	[JsonPropertyName("phone_number")] public string Phone { get; set; }
}