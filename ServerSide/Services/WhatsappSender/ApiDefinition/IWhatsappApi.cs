using Refit;
using ServerSide.Services.WhatsappSender.ApiDefinition.Requests;
using ServerSide.Services.WhatsappSender.ApiDefinition.Responses;

namespace ServerSide.Services.WhatsappSender.ApiDefinition;

public interface IWhatsappApi
{
	[Get("/WhatsappGetMessages")]
	Task<GetMessagesResponse> GetMessages( GetMessagesParams param);

	[Post("/WhatsappSendMessage")]
	Task<string> SendCode([Body]SendMessageRequest request);
	[Post("/WhatsappNumberHasIt")]
	Task<HasWhatsappResponse> HasWhatsapp([Body]HasWhatsappRequest request);
	

}






