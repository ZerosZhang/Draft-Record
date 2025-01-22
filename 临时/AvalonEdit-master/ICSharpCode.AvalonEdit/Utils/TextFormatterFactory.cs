using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;

namespace ICSharpCode.AvalonEdit.Utils
{
    // - TextFormattingMode 是一个用于控制文本呈现方式的属性，它可以影响文本的清晰度和性能。
    // - 在 .NET 4.0 中，TextFormattingMode 有两种模式：Ideal 和 Display。
    // - Ideal 模式提供了更高的清晰度，但可能会降低性能；Display 模式则更注重性能，但可能会牺牲一些清晰度。
    /// <summary>
    /// 创建具有正确 TextFormattingMode 的 TextFormatter 实例（如果在 .NET 4.0 上运行）。
    /// Creates TextFormatter instances that with the correct TextFormattingMode, if running on .NET 4.0.
    /// </summary>
    static class TextFormatterFactory
    {
        /// <summary>
        /// Creates a <see cref="TextFormatter"/> using the formatting mode used by the specified owner object.
        /// </summary>
        // - 根据指定的所有者对象创建一个 TextFormatter 实例。
        // - 这个方法会从所有者对象中获取 TextFormattingMode 属性，并使用该属性创建 TextFormatter。
        public static TextFormatter Create(DependencyObject owner)
        {
            ArgumentNullException.ThrowIfNull(owner);
            return TextFormatter.Create(TextOptions.GetTextFormattingMode(owner));
        }

        /// <summary>
        /// Returns whether the specified dependency property affects the text formatter creation.
        /// Controls should re-create their text formatter for such property changes.
        /// </summary>
        // - 判断指定的依赖属性是否会影响 TextFormatter 的创建。
        // - 如果某个属性的更改会导致 TextFormatter 需要重新创建，那么这个方法会返回 true。
        public static bool PropertyChangeAffectsTextFormatter(DependencyProperty dp)
        {
            return dp == TextOptions.TextFormattingModeProperty;
        }

        /// <summary>
        ///  创建一个格式化的文本对象 FormattedText。
        /// </summary>
        /// <param name="element">The owner element. The text formatter setting are read from this element.</param>
        /// <param name="text">The text.</param>
        /// <param name="typeface">The typeface to use. If this parameter is null, the typeface of the <paramref name="element"/> will be used.</param>
        /// <param name="emSize">The font size. If this parameter is null, the font size of the <paramref name="element"/> will be used.</param>
        /// <param name="foreground">The foreground color. If this parameter is null, the foreground of the <paramref name="element"/> will be used.</param>
        /// <returns>A FormattedText object using the specified settings.</returns>
        // - 这个方法会根据指定的元素、文本、字体、字号和前景色创建一个 FormattedText 对象，
        // - 并使用元素的 TextFormattingMode 属性来设置文本的格式化模式。
        public static FormattedText CreateFormattedText(FrameworkElement element, string text, Typeface typeface, double? emSize, Brush foreground)
        {
            ArgumentNullException.ThrowIfNull(element);
            ArgumentNullException.ThrowIfNull(text);
            typeface ??= element.CreateTypeface();
            emSize ??= TextBlock.GetFontSize(element);
            foreground ??= TextBlock.GetForeground(element);
            return new FormattedText(
                text,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                typeface,
                emSize.Value,
                foreground,
                null,
                TextOptions.GetTextFormattingMode(element),
                VisualTreeHelper.GetDpi(element).PixelsPerDip
            );
        }
    }
}
