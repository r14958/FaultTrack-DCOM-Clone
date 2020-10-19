// -----------------------------------------------------------------------------
//  <copyright file="NativeMethods.cs" company="DCOM Engineering, LLC">
//      Copyright (c) DCOM Engineering, LLC.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------

namespace FaultTrack.Shell.Windows
{
    using System;
    using System.Runtime.InteropServices;

    // ReSharper disable InconsistentNaming
    // ReSharper disable NonReadonlyFieldInGetHashCode

    internal static class NativeMethods
    {
        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, ref MONITORINFO lpmi);

        [DllImport("user32")]
        internal static extern IntPtr MonitorFromWindow(IntPtr hwnd, int flags);
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct POINT : IEquatable<POINT>
    {
        public int x;
        public int y;

        public bool Equals(POINT other)
        {
            return (x == other.x) && (y == other.y);
        }

        public override bool Equals(object obj)
        {
            if (obj is POINT)
                return Equals((POINT)obj);

            return false;
        }

        public override int GetHashCode()
        {
            return x ^ y;
        }

        public static bool operator ==(POINT left, POINT right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(POINT left, POINT right)
        {
            return !left.Equals(right);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct MINMAXINFO : IEquatable<MINMAXINFO>
    {
        public POINT ptReserved;
        public POINT ptMaxSize;
        public POINT ptMaxPosition;
        public POINT ptMinTrackSize;
        public POINT ptMaxTrackSize;

        public bool Equals(MINMAXINFO other)
        {
            return (ptReserved     == other.ptReserved) &&
                   (ptMaxSize      == other.ptMaxSize) &&
                   (ptMaxPosition  == other.ptMaxPosition) &&
                   (ptMinTrackSize == other.ptMinTrackSize) &&
                   (ptMaxTrackSize == other.ptMaxTrackSize);
        }

        public override bool Equals(object obj)
        {
            if (obj is MINMAXINFO)
                return Equals((MINMAXINFO)obj);

            return false;
        }

        public override int GetHashCode()
        {
            return ptReserved.GetHashCode() ^
                   ptMaxSize.GetHashCode() ^
                   ptMaxPosition.GetHashCode() ^
                   ptMinTrackSize.GetHashCode() ^
                   ptMaxTrackSize.GetHashCode();
        }

        public static bool operator ==(MINMAXINFO left, MINMAXINFO right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(MINMAXINFO left, MINMAXINFO right)
        {
            return !left.Equals(right);
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    internal struct MONITORINFO : IEquatable<MONITORINFO>
    {
        public int cbSize;//     = Marshal.SizeOf(typeof(MINMAXINFO));
        public RECT rcMonitor;// = new RECT();
        public RECT rcWork;//    = new RECT();
        public uint dwFlags;//   = 0;

        public bool Equals(MONITORINFO other)
        {
            return (cbSize == other.cbSize) &&
                   (rcMonitor == other.rcMonitor) &&
                   (rcWork == other.rcWork) &&
                   (dwFlags == other.dwFlags);
        }

        public override bool Equals(object obj)
        {
            if (obj is MONITORINFO)
                return Equals((MONITORINFO)obj);

            return false;
        }

        public override int GetHashCode()
        {
            return cbSize.GetHashCode() ^
                   rcMonitor.GetHashCode() ^
                   rcWork.GetHashCode() ^
                   dwFlags.GetHashCode();
        }

        public static bool operator ==(MONITORINFO left, MONITORINFO right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(MONITORINFO left, MONITORINFO right)
        {
            return !left.Equals(right);
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0)]
    internal struct RECT : IEquatable<RECT>
    {
        public int left;
        public int top;
        public int right;
        public int bottom;

        public bool Equals(RECT other)
        {
            return (left == other.left) &&
                   (top == other.top) &&
                   (right == other.right) &&
                   (bottom == other.bottom);
        }

        public override bool Equals(object obj)
        {
            if (obj is RECT)
                return Equals((RECT)obj);

            return false;
        }

        public override int GetHashCode()
        {
            return left ^ top ^ right ^ bottom;
        }

        public static bool operator ==(RECT left, RECT right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(RECT left, RECT right)
        {
            return !left.Equals(right);
        }
    }
}