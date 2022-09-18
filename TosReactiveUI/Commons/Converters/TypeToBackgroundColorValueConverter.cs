using System;
using System.Globalization;

namespace TosReactiveUI.Commons.Converters;

public class TypeToBackgroundColorValueConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string type)
        {
            switch (type)
            {
                case "fire":
                    return Color.FromHex("FC7D25");
                case "grass":
                    return Color.FromHex("9BCC50");
                case "poison":
                    return Color.FromHex("B87FC8");
                case "flying":
                    return Color.FromHex("BDB9B8");
                case "water":
                    return Color.FromHex("4692C3");
                case "bug":
                    return Color.FromHex("739F3E");
                case "normal":
                    return Color.FromHex("A3ACAF");
                case "electric":
                    return Color.FromHex("EED535");
                case "ground":
                    return Color.FromHex("AB9841");
                case "fairy":
                    return Color.FromHex("FDB9E9");
                case "fighting":
                    return Color.FromHex("D56624");
                case "psychic":
                    return Color.FromHex("F366B9");
                case "rock":
                    return Color.FromHex("A38C22");
                case "steel":
                    return Color.FromHex("A3ACAF");
                case "ice":
                    return Color.FromHex("51C4E7");
                case "ghost":
                    return Color.FromHex("A3ACAF");
                case "dragon":
                    return Color.FromHex("F16E56");
                case "dark":
                    return Color.FromHex("707070");
            }
        }
        return Color.FromHex("000000");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

