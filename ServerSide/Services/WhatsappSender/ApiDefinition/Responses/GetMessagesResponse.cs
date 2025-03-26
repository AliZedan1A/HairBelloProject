namespace ServerSide.Services.WhatsappSender.ApiDefinition.Responses;

public record GetMessagesResponse
{
		public List<Message> Messages { get; set; }
		public bool HasMorePages { get; set; }
}
	public record Message
	{
		public string Id { get; set; }
		public string From { get; set; }
		public string To { get; set; }
		public object Author { get; set; }
		public string Pushname { get; set; }
		public string MessageType { get; set; }
		public string Status { get; set; }
		public string Body { get; set; }
		public string Caption { get; set; }
		public int Forwarded { get; set; }
		public object QuotedMessageId { get; set; }
		public string MentionedIds { get; set; }
		public DateTime Timestamp { get; set; }
	}
