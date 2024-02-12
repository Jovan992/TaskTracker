namespace CommonUtils.ResultDataResponse;

public class BadRequestResultData<T> : ResultData<T>
{
    public BadRequestResultData(string message, T? data = default)
        : base(message, data)
    {
    }
}
