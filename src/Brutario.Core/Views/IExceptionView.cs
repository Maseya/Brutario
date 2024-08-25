namespace Brutario.Core;

using System;

public interface IExceptionView
{
    void Show(Exception ex);

    void Show(string? message);

    bool ShowAndPromptRetry(Exception ex);

    bool ShowAndPromptRetry(string? message);
}
