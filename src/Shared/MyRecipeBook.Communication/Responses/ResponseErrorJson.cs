using System.Net;

namespace MyRecipeBook.Communication.Responses;

public class ResponseErrorJson
{

    public List<string> Errors { get; private set; }

    public ResponseErrorJson(List<string> errorMessage)
    {
        Errors = errorMessage;
    }

    public ResponseErrorJson(string errorMessage) 
    {
        Errors = new List<string>();

        Errors.Add(errorMessage);

    }
}
