namespace Api;

public class ApiResponse<T>
{
    public int CodRetorno { get; set; } = 0;
    public string? Mensagem { get; set; }
    public T? Data { get; set; }

    public static ApiResponse<T> Success(T data)
    {
        return new ApiResponse<T> { Data = data };
    }

    public static ApiResponse<object> Error(string message)
    {
        return new ApiResponse<object> { CodRetorno = 1, Mensagem = message, Data = null };
    }
}