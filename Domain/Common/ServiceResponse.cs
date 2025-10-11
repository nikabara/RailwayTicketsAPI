namespace Domain.Common;

public class ServiceResponse<T>
{
    public T? Data { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public bool IsSuccess { get; set; } = false;
}
