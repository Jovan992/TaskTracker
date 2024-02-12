using Microsoft.AspNetCore.Mvc;

namespace CommonUtils.ResultDataResponse;

public static class ResultDataExtensions
{
    public static ObjectResult ToOkActionResult<T>(this ResultData<T> resultData)
    {
        return new OkObjectResult(resultData.Data);
    }

    public static ObjectResult ToCreatedAtActionResult<T>(this ResultData<T> resultData, string actionName,object routeValues)
    {
        return new CreatedAtActionResult(actionName, null, routeValues, resultData.Data);
    }

    public static ObjectResult ToBadRequestActionResult<T>(this ResultData<T> resultData)
    {
        return new BadRequestObjectResult(resultData.Message);
    }

    public static ObjectResult ToNotFoundActionResult<T>(this ResultData<T> resultData)
    {
        return new NotFoundObjectResult(resultData.Message);
    }


}
