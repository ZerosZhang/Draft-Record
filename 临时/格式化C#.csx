public class BooleanConverter<T> : IValueConverter
{
    public T TrueValue { get; set; }

    public T FalseValue { get; set; }

    protected BooleanConverter(T trueValue, T falseValue)
    {
        TrueValue = trueValue;
        FalseValue = falseValue;
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return BooleanConverter<T>.ConvertBooleanToType(value, TrueValue, FalseValue);
    }

    internal static T ConvertBooleanToType(object value, T trueValue, T falseValue)
    {
        bool flag = ValidateArgument.NotNullOrEmptyCast<bool>(value, "value");
        return flag ? trueValue : falseValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value is T flag && EqualityComparer<T>.Default.Equals(flag, TrueValue);
    }
}