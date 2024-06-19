public static void Range(bool checkedExpression, string parameterName, string message)
{
    if (!checkedExpression)
    {
        throw new ArgumentOutOfRangeException(parameterName, message);
    }
}

EqualityComparer<T>.Default.Equals(flag, TrueValue)