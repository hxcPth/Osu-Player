﻿using OsuPlayer.Data.Models;

namespace OsuPlayer.Data;

public class KeyComparer : IEqualityComparer<KeyValuePair<string, PlayItemDetail>>
{
    private KeyComparer()
    {
    }

    public static KeyComparer Instance { get; } = new();

    public bool Equals(KeyValuePair<string, PlayItemDetail> x, KeyValuePair<string, PlayItemDetail> y)
    {
        return x.Key == y.Key;
    }

    public int GetHashCode(KeyValuePair<string, PlayItemDetail> obj)
    {
        return obj.Key?.GetHashCode() ?? 0;
    }
}