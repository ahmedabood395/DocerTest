namespace FAQ.Infrastructure.Helpers
{
    public interface IResponseHelper
    {
        ResponseDTO SavedSuccessfully(string message);
        ResponseDTO SavedSuccessfully(dynamic result, string message);
        ResponseDTO Exception();
        ResponseDTO FailedToSave(string message);
        ResponseDTO NotFound(string message);
        ResponseDTO AlreadyExists(string message);
        ResponseDTO RetrievedSuccessfully(dynamic result, string message);
        ResponseDTO RetrievedSuccessfully(dynamic result, string message, ref ResponseDTO responseDto);
    }
}