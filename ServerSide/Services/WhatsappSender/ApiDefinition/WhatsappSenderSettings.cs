namespace ServerSide.Services.WhatsappSender.ApiDefinition;

public class WhatsappSenderSettings
{
	public static string SectionName=> "WhatsappSender";
	public string ApiToken { get; set; } = null!;
	public string RapidKey { get; set; } = null!;
	public string RapidHost { get; set; } = null!; 
	public string BaseUrl { get; set; } = null!;
}