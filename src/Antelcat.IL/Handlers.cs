namespace Antelcat.IL;

public delegate void SetHandler<TTarget, in TIn>(ref TTarget? target, TIn? value);

public delegate TOut? GetHandler<in TTarget, out TOut>(TTarget? target);

public delegate TReturn? InvokeHandler<in TTarget, out TReturn>(TTarget? target, params object?[]? args);

public delegate T? CtorHandler<out T>(params object?[]? parameters);