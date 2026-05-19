using System.Globalization;
using Avalonia.Data.Converters;
using FruityScale.Domain.MusicTheory;

namespace FruityScale.Presentation.Converters;

public class NoteNameConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int noteNumber)
        {
            return NoteNameNormalizer.GetNoteName(noteNumber);
        }
        
        return string.Empty;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException("Converting back from note name to number is not supported.");
    }
}