namespace CommonUtils.ResultDataResponse;

public abstract class ResultData<T>
{
    public ResultData(string message = "", T data = default!)
    {
        Message = message;
        Data = data;
    }
    public string? Message { get; set; }
    public T? Data { get; set; }
}
