namespace CommonUtils.ResultDataResponse;

public class NotFoundResultData<T> : ResultData<T>
{
    public NotFoundResultData(string? message)
        : base(message)
    {
    }
}
