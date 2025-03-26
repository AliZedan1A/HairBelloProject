using Refit;

namespace ServerSide.Services.WhatsappSender.ApiDefinition.Requests;

public class BaseRequest
{
	[AliasAs("token")]
	public string Token { get; init; } = "Nq0ucUJx9Sr4reGXSUwoa34fKYDqR/jVpSuVjbhQqZHYEfMFG6my4maGVj2rnpNdV3nDaqEY6tlg2P9IS/r7aK2jMY07XNKgc26P8L0/4R4=";
}