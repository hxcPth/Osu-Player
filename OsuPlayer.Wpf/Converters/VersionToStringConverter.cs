﻿using System;
using System.Globalization;
using System.Windows.Data;
using Coosu.Beatmap.Sections.GamePlay;

namespace Milky.OsuPlayer.Converters
{
    class VersionToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var sb = (GameMode?)value;
            if (sb == null) return "";
            switch (sb)
            {
                case GameMode.Circle:
                    return "圈";
                case GameMode.Taiko:
                    return "鼓";
                case GameMode.Catch:
                    return "果";
                case GameMode.Mania:
                    return "键";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
