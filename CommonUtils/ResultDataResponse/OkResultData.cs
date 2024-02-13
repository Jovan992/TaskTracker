namespace CommonUtils.ResultDataResponse;

public class OkResultData<T> : ResultData<T>
{
    public OkResultData(T data, string message = "")
        : base(message, data)
    {
    }
}
