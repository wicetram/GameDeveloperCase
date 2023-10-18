using GameDeveloperEntity.Dto;

namespace GameDeveloperCore.Util
{
    public static class ProcessResultHandler
    {
        public static ProcessResult ErrorHandler(string message = "")
        {
            return new ProcessResult
            {
                Result = false,
                ResultCode = -1,
                ResultMessage = message == "" ? "Failed" : message
            };
        }

        public static ProcessResult SuccessHandler(string message = "")
        {
            return new ProcessResult
            {
                Result = true,
                ResultCode = 1,
                ResultMessage = message == "" ? "Success" : message
            };
        }

        public static ProcessResult ExceptionHandler(Exception ex)
        {
            return new ProcessResult
            {
                Result = false,
                ResultCode = -99,
                ResultMessage = $"Exception: {ex.Message}"
            };
        }
    }
}
