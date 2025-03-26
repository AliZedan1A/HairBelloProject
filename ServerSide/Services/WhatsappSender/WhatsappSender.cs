
using Domain.Models;
using Newtonsoft.Json;
using ServerSide.Services.WhatsappSender.ApiDefinition;
using ServerSide.Services.WhatsappSender.ApiDefinition.Requests;
using ServerSide.Services.WhatsappSender.ApiDefinition.Responses;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ServerSide.Services;

public class WhatsappService 
{
    private readonly Services.WhatsappSender.ApiDefinition.IWhatsappApi _whatsappApi;

    public WhatsappService(IWhatsappApi whatsappApi)
    {
        _whatsappApi = whatsappApi;
    }

    public async Task<bool> IsValidNumber(string number)
    {
        var request = HasWhatsappRequest.Create(number);
        var response = await _whatsappApi.HasWhatsapp(request);
        return response.Status == "valid";
    }

    public async Task<ServiceReturnModel<bool>> Send(string number, string message)
    {
        try
        {
            var request = SendMessageRequest.SendPhoneMessage(number, message);
            var responseJson = await _whatsappApi.SendCode(request);
            const string invalidNumber = "Invalid WhatsApp number or Group ID";
            const string numberDontHaveWhatsApp = "This number doesn't have a WhatsApp account associated to it.";
            var successResponse = JsonSerializer.Deserialize<SendMessageSucces>(responseJson);
            if (successResponse is not null && successResponse.Sent == "true") return new ServiceReturnModel<bool>()
            {
                IsSucceeded = true,


            };
            var errorResponse = JsonConvert.DeserializeObject<SendMessageError>(responseJson);
            if (errorResponse is not null && errorResponse.Message == numberDontHaveWhatsApp)
                return new ServiceReturnModel<bool>()
                {
                    IsSucceeded = false,
                    Comment = numberDontHaveWhatsApp,


                };
            if (errorResponse is not null && errorResponse.Message == invalidNumber)
                return new ServiceReturnModel<bool>()
                {
                    IsSucceeded = false,
                    Comment = invalidNumber,


                };
            return new ServiceReturnModel<bool>()
            {
                IsSucceeded = false,
                Comment = "فشل ارسال الرسالة",


            };
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return new ServiceReturnModel<bool>()
            {
                IsSucceeded = false,
            };
        }
       
    }

   
}