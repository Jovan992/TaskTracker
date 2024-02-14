namespace CommonUtils.ResultDataResponse;

public class OkResultData<T> : ResultData<T>
{
    public OkResultData(T data)
        : base(data: data)
    {
    }
}
