using System;
using System.Windows.Input;

namespace Scch.Common.Windows.Input;

public class EquatableKeyGesture : KeyGesture, IEquatable<EquatableKeyGesture>
{
    public EquatableKeyGesture(KeyGesture gesture)
        : base(gesture.Key, gesture.Modifiers, gesture.DisplayString)
    {
    }

    public EquatableKeyGesture(Key key, ModifierKeys modifiers)
        : base(key, modifiers)
    {
    }

    public bool Equals(EquatableKeyGesture other)
    {
        return (other != null && Key == other.Key && Modifiers == other.Modifiers);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(this, obj))
            return true;

        if (obj == null || obj.GetType() != GetType())
            return false;

        return Equals((EquatableKeyGesture)obj);
    }

    public override int GetHashCode()
    {
        return Key.GetHashCode() ^ Modifiers.GetHashCode();
    }

    public static bool IsValid(Key key, ModifierKeys modifiers)
    {
        if (key is (< Key.F1 or > Key.F24) and (< Key.NumPad0 or > Key.Divide))
        {
            if ((modifiers & (ModifierKeys.Windows | ModifierKeys.Control | ModifierKeys.Alt)) != ModifierKeys.None)
            {
                return key switch
                {
                    Key.LWin or Key.RWin or Key.LeftCtrl or Key.RightCtrl or Key.LeftAlt or Key.RightAlt => false,
                    _ => true,
                };
            }
            if (key is >= Key.D0 and <= Key.D9 or >= Key.A and <= Key.Z)
            {
                return false;
            }
        }
        return true;
    }
}
