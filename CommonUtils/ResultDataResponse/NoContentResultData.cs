namespace CommonUtils.ResultDataResponse;

public class NoContentResultData<T> : ResultData<T>
{
    public NoContentResultData(string? message = "", T? data = default)
        : base(message, data)
    {
    }
}
