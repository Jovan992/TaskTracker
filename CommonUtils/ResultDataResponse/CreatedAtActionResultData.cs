namespace CommonUtils.ResultDataResponse;

public class CreatedAtActionResultData<T> : ResultData<T>
{
    public CreatedAtActionResultData(T data)
        : base(data: data)
    {
    }
}
