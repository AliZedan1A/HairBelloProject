using Refit;

namespace ServerSide.Services.WhatsappSender.ApiDefinition.Requests;

public class GetMessagesParams:BaseRequest
{
		[AliasAs("pageNumber")]
		public int Page { get; set; } = 1;
}