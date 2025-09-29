using NexaSoft.Club.Domain.Abstractions;

public class Result
{
    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None)
            throw new InvalidOperationException("Un resultado exitoso no debe tener error.");

        if (!isSuccess && error == Error.None)
            throw new InvalidOperationException("Un resultado fallido debe tener error.");

        IsSuccess = isSuccess;
        Error = error;
    }

    public bool IsSuccess { get; }
    public Error Error { get; }

    public bool IsFailure => !IsSuccess;

    public static Result Success() => new(true, Error.None);

    public static Result Failure(Error error) => new(false, error);

    public static Result<TValue> Success<TValue>(TValue value)
        => new Result<TValue>(value, true, Error.None);

    public static Result<TValue> Failure<TValue>(Error error)
        => new Result<TValue>(default!, false, error);

    public static Result<TValue> Create<TValue>(TValue value)
        => value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
}

public class Result<Tvalue> : Result
{
    public Result(Tvalue value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        Value = value;
    }

    public Tvalue Value { get; }

    public static implicit operator Result<Tvalue>(Tvalue value) => Create(value);

    // Método Match que devuelve un resultado (por ejemplo, IActionResult en el controlador)
    public TResult Match<TResult>(Func<Tvalue, TResult> onSuccess, Func<Error, TResult> onFailure)
    {
        return IsSuccess ? onSuccess(Value) : onFailure(Error);
    }
}

