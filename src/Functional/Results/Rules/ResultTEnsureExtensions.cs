using System.Runtime.CompilerServices;

namespace Toarnbeike.Functional.Results.Rules;

public static class ResultEnsureExtensions
{
    extension(Result result)
    {
        public RuleTarget<Option<T>, T> EnsureSome<T>(
            Func<Option<T>> valueSelector,
            string? propertyName = null,
            [CallerArgumentExpression(nameof(valueSelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(valueSelector);
            propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);
            var executor = new RuleExecutor<Option<T>, T>((_, rule, message) =>
            {
                var option = valueSelector();
                if (option.TryGetValue(out var some))
                {
                    var ruleResult = rule(some);
                    return ruleResult.IsValid
                        ? Result.Success()
                        : Result.Failure(new GuardFailure(propertyName, message ?? ruleResult.Message));
                }
                else
                {
                    // If the Option is None, we consider it invalid for this rule.
                    return Result.Failure(new GuardFailure(propertyName, message ?? "Option was None"));
                }
            });
            var asyncExecutor = new AsyncRuleExecutor<Option<T>, T>(async (_, rule, message) =>
            {
                var option = valueSelector();
                if (option.TryGetValue(out var some))
                {
                    var ruleResult = await rule(some);
                    return ruleResult.IsValid
                        ? Result.Success()
                        : Result.Failure(new GuardFailure(propertyName, message ?? ruleResult.Message));
                }
                else
                {
                    // If the Option is None, we consider it invalid for this rule.
                    return Result.Failure(new GuardFailure(propertyName, message ?? "Option was None"));
                }
            });
            return new RuleTarget<Option<T>, T>(result.WithValue(valueSelector()), executor, asyncExecutor);
        }

        public RuleTarget<Option<T>, T> EnsureWhenSome<T>(
            Func<Option<T>> valueSelector,
            string? propertyName = null,
            [CallerArgumentExpression(nameof(valueSelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(valueSelector);
            propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);
            var executor = new RuleExecutor<Option<T>, T>((_, rule, message) =>
            {
                var option = valueSelector();
                if (option.TryGetValue(out var some))
                {
                    var ruleResult = rule(some);
                    return ruleResult.IsValid
                        ? Result.Success()
                        : Result.Failure(new GuardFailure(propertyName, message ?? ruleResult.Message));
                }
                else
                {
                    // If the Option is None, we consider it valid for this rule.
                    return Result.Success();
                }
            });
            var asyncExecutor = new AsyncRuleExecutor<Option<T>, T>(async (_, rule, message) =>
            {
                var option = valueSelector();
                if (option.TryGetValue(out var some))
                {
                    var ruleResult = await rule(some);
                    return ruleResult.IsValid
                        ? Result.Success()
                        : Result.Failure(new GuardFailure(propertyName, message ?? ruleResult.Message));
                }
                else
                {
                    // If the Option is None, we consider it valid for this rule.
                    return Result.Success();
                }
            });
            return new RuleTarget<Option<T>, T>(result.WithValue(valueSelector()), executor, asyncExecutor);
        }
    }

    extension(Task<Result> resultTask)
    {
        public async Task<RuleTarget<Option<T>, T>> EnsureSomeAsync<T>(
            Func<Option<T>> valueSelector,
            string? propertyName = null,
            [CallerArgumentExpression(nameof(valueSelector))] string? expression = null)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.EnsureSome(valueSelector, propertyName, expression);
        }

        public async Task<RuleTarget<Option<T>, T>> EnsureWhenSomeAsync<T>(
            Func<Option<T>> valueSelector,
            string? propertyName = null,
            [CallerArgumentExpression(nameof(valueSelector))] string? expression = null)
        {
            var result = await resultTask.ConfigureAwait(false);
            return result.EnsureWhenSome(valueSelector, propertyName, expression);
        }
    }
}

/// <summary>
/// Provides extension methods for creating guard targets from <see cref="Result{Option{T}}"/>
/// </summary>
public static class ResultTEnsureExtensions
{
    /// <param name="optionResult">The result containing an option to validate.</param>
    /// <typeparam name="T">The type of the value contained in the result.</typeparam>
    extension<T>(Result<Option<T>> optionResult)
    {
        /// <summary>
        /// Creates an ensure target for the current value that verifies that the option has a value.
        /// </summary>
        /// <param name="propertyName">Property name to include in guard failures. If null, a fallback name is extracted.</param>
        /// <returns>An ensure target that evaluates rules against the current value and returns either success or a guard failure.</returns>
        public RuleTarget<Option<T>, T> EnsureSome(string propertyName)
        {
            var executor = new RuleExecutor<Option<T>, T>((value, rule, message) =>
            {
                if (value.TryGetValue(out var some))
                {
                    var ruleResult = rule(some);
                    return ruleResult.IsValid
                        ? Result.Success()
                        : Result.Failure(new GuardFailure(propertyName, message ?? ruleResult.Message));
                }
                else
                {
                    // If the Option is None, we consider it invalid for this rule.
                    return Result.Failure(new GuardFailure(propertyName, message ?? "Option was None"));
                }
            });

            var asyncExecutor = new AsyncRuleExecutor<Option<T>, T>(async (value, rule, message) =>
            {
                if (value.TryGetValue(out var some))
                {
                    var ruleResult = await rule(some);
                    return ruleResult.IsValid
                        ? Result.Success()
                        : Result.Failure(new GuardFailure(propertyName, message ?? ruleResult.Message));
                }
                else
                {
                    // If the Option is None, we consider it invalid for this rule.
                    return Result.Failure(new GuardFailure(propertyName, message ?? "Option was None"));
                }
            });

            return new RuleTarget<Option<T>, T>(optionResult, executor, asyncExecutor);
        }

        /// <summary>
        /// Creates an ensure target for the current value that only verifies when the option has a value.
        /// </summary>
        /// <param name="propertyName">Property name to include in guard failures. If null, a fallback name is extracted.</param>
        /// <returns>An ensure target that evaluates rules against the current value and returns either success or a guard failure.</returns>
        public RuleTarget<Option<T>, T> EnsureWhenSome(string propertyName)
        {
            var executor = new RuleExecutor<Option<T>, T>((value, rule, message) =>
            {
                if (value.TryGetValue(out var some))
                {
                    var ruleResult = rule(some);
                    return ruleResult.IsValid
                        ? Result.Success()
                        : Result.Failure(new GuardFailure(propertyName, message ?? ruleResult.Message));
                }
                else
                {
                    // If the Option is None, we consider it valid for this rule.
                    return Result.Success();
                }
            });

            var asyncExecutor = new AsyncRuleExecutor<Option<T>, T>(async (value, rule, message) =>
            {
                if (value.TryGetValue(out var some))
                {
                    var ruleResult = await rule(some);
                    return ruleResult.IsValid
                        ? Result.Success()
                        : Result.Failure(new GuardFailure(propertyName, message ?? ruleResult.Message));
                }
                else
                {
                    // If the Option is None, we consider it valid for this rule.
                    return Result.Success();
                }
            });

            return new RuleTarget<Option<T>, T>(optionResult, executor, asyncExecutor);
        }
    }

    extension<T>(Result<T> result)
    {
        /// <summary>
        /// Creates a rule target for an optional value and applies rules when the option contains a value. Returns a failure if the option is <c>None</c>.
        /// </summary>
        /// <remarks>If the selected option is <c>None</c>, validation fails for this rule.</remarks>
        /// <typeparam name="TProperty">The type of the value contained in the option.</typeparam>
        /// <param name="valueSelector">Selects the optional value from the current instance.</param>
        /// <param name="propertyName">The property name used in validation failures. If null, the name is inferred from the selector expression.</param>
        /// <param name="expression">The captured argument expression for the selector, used to infer the property name when one is not provided.</param>
        /// <returns>An EnsureTarget that evaluates rules against the contained value.</returns>
        public RuleTarget<T, TProperty> EnsureSome<TProperty>(
            Func<T, Option<TProperty>> valueSelector,
            string? propertyName = null,
            [CallerArgumentExpression(nameof(valueSelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(valueSelector);
            propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);

            var executor = new RuleExecutor<T, TProperty>((value, rule, message) =>
            {
                if (valueSelector(value).TryGetValue(out var some))
                {
                    var ruleResult = rule(some);
                    return ruleResult.IsValid
                        ? Result.Success()
                        : Result.Failure(new GuardFailure(propertyName, message ?? ruleResult.Message));
                }
                else
                {
                    // If the Option is None, we consider it invalid for this rule.
                    return Result.Failure(new GuardFailure(propertyName, message ?? "Option was None"));
                }
            });

            var asyncExecutor = new AsyncRuleExecutor<T, TProperty>(async (value, rule, message) =>
            {
                if (valueSelector(value).TryGetValue(out var some))
                {
                    var ruleResult = await rule(some);
                    return ruleResult.IsValid
                        ? Result.Success()
                        : Result.Failure(new GuardFailure(propertyName, message ?? ruleResult.Message));
                }
                else
                {
                    // If the Option is None, we consider it invalid for this rule.
                    return Result.Failure(new GuardFailure(propertyName, message ?? "Option was None"));
                }
            });

            return new RuleTarget<T, TProperty>(result, executor, asyncExecutor);
        }
       
        /// <summary>
        /// Creates a rule target for an optional value and applies rules when the option contains a value.
        /// Returns a success if the option is <c>None</c>.
        /// </summary>
        /// <remarks>If the selected option is <c>None</c>, validation succeeds for this rule.</remarks>
        /// <typeparam name="TProperty">The type of the value contained in the option.</typeparam>
        /// <param name="valueSelector">Selects the optional value from the current instance.</param>
        /// <param name="propertyName">The property name used in validation failures. If null, the name is inferred from the selector expression.</param>
        /// <param name="expression">The captured argument expression for the selector, used to infer the property name when one is not provided.</param>
        /// <returns>An EnsureTarget that evaluates rules against the contained value.</returns>
        public RuleTarget<T, TProperty> EnsureWhenSome<TProperty>(
                Func<T, Option<TProperty>> valueSelector,
                string? propertyName = null,
                [CallerArgumentExpression(nameof(valueSelector))] string? expression = null)
        {
            ArgumentNullException.ThrowIfNull(valueSelector);
            propertyName ??= ExpressionHelpers.ExtractPropertyName(expression);

            var executor = new RuleExecutor<T, TProperty>((value, rule, message) =>
            {
                if (valueSelector(value).TryGetValue(out var some))
                {
                    var ruleResult = rule(some);
                    return ruleResult.IsValid
                        ? Result.Success()
                        : Result.Failure(new GuardFailure(propertyName, message ?? ruleResult.Message));
                }
                else
                {
                    // If the Option is None, we consider it valid for this rule.
                    return Result.Success();
                }
            });

            var asyncExecutor = new AsyncRuleExecutor<T, TProperty>(async (value, rule, message) =>
            {
                if (valueSelector(value).TryGetValue(out var some))
                {
                    var ruleResult = await rule(some);
                    return ruleResult.IsValid
                        ? Result.Success()
                        : Result.Failure(new GuardFailure(propertyName, message ?? ruleResult.Message));
                }
                else
                {
                    // If the Option is None, we consider it valid for this rule.
                    return Result.Success();
                }
            });

            return new RuleTarget<T, TProperty>(result, executor, asyncExecutor);
        }
    }
}
