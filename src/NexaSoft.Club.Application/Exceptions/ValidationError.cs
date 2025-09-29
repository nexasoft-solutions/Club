namespace NexaSoft.Club.Application.Exceptions;

public sealed record ValidationError
(
    string PropertyName,
    string ErrorMessage
);