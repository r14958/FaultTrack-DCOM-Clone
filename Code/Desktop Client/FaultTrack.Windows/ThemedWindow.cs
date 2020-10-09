// -----------------------------------------------------------------------------
//  <copyright file="ThemedWindow.cs" company="DCOM Engineering, LLC">
//      Copyright (c) DCOM Engineering, LLC.  All rights reserved.
//  </copyright>
// -----------------------------------------------------------------------------
namespace FaultTrack.Windows
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Interop;

    public class ThemedWindow : Window
    {
        private Point cursorOffset;

        /// <summary>
        /// Value used when the user drags the window by the caption from a Maximized Window State into a Normal Window State.
        /// This value is used to restore the window's Top position near the mouse cursor.
        /// </summary>
        private double restoreTop;

        /// <summary>
        /// Initializes a new instance of the FaultTrack.Windows.ThemedWindow class.
        /// </summary>
        public ThemedWindow()
        {
            SourceInitialized += (sender, e) => RegisterHandle();
            Style = (Style)TryFindResource("ThemedWindowStyle");
        }
        
        private FrameworkElement borderLeft;
        private FrameworkElement borderTopLeft;
        private FrameworkElement borderTop;
        private FrameworkElement borderTopRight;
        private FrameworkElement borderRight;
        private FrameworkElement borderBottomRight;
        private FrameworkElement borderBottom;
        private FrameworkElement borderBottomLeft;
        private FrameworkElement caption;
        private Hyperlink minimizeButton;
        private Hyperlink maximizeButton;
        private Hyperlink closeButton;
        private IntPtr handle;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            RegisterBorders();
            RegisterCaption();
            RegisterMinimizeButton();
            RegisterMaximizeButton();
            RegisterCloseButton();
        }

        private void RegisterBorders()
        {
            borderLeft          = (FrameworkElement)GetTemplateChild("PART_WindowBorderLeft");
            borderTopLeft       = (FrameworkElement)GetTemplateChild("PART_WindowBorderTopLeft");
            borderTop           = (FrameworkElement)GetTemplateChild("PART_WindowBorderTop");
            borderTopRight      = (FrameworkElement)GetTemplateChild("PART_WindowBorderTopRight");
            borderRight         = (FrameworkElement)GetTemplateChild("PART_WindowBorderRight");
            borderBottomRight   = (FrameworkElement)GetTemplateChild("PART_WindowBorderBottomRight");
            borderBottom        = (FrameworkElement)GetTemplateChild("PART_WindowBorderBottom");
            borderBottomLeft    = (FrameworkElement)GetTemplateChild("PART_WindowBorderBottomLeft");

            RegisterBorderEvents(WindowBorderEdge.Left, borderLeft);
            RegisterBorderEvents(WindowBorderEdge.TopLeft, borderTopLeft);
            RegisterBorderEvents(WindowBorderEdge.Top, borderTop);
            RegisterBorderEvents(WindowBorderEdge.TopRight, borderTopRight);
            RegisterBorderEvents(WindowBorderEdge.Right, borderRight);
            RegisterBorderEvents(WindowBorderEdge.BottomRight, borderBottomRight);
            RegisterBorderEvents(WindowBorderEdge.Bottom, borderBottom);
            RegisterBorderEvents(WindowBorderEdge.BottomLeft, borderBottomLeft);
        }

        private void RegisterCaption()
        {
            caption = (FrameworkElement)GetTemplateChild("PART_WindowCaption");

            if (caption != null)
            {
                caption.MouseLeftButtonDown += (sender, e) =>
                {
                    restoreTop = e.GetPosition(this).Y;

                    if (e.ClickCount == 2 && 
                        e.ChangedButton == MouseButton.Left && 
                        (ResizeMode != ResizeMode.CanMinimize && ResizeMode != ResizeMode.NoResize))
                    {
                        if (WindowState != WindowState.Maximized)
                        {
                            WindowState = WindowState.Maximized;
                        }
                        else
                        {
                            WindowState = WindowState.Normal;
                        }

                        return;
                    }
                    
                    DragMove();
                };

                caption.MouseMove += (sender, e) =>
                {
                    if (e.LeftButton == MouseButtonState.Pressed && caption.IsMouseOver)
                    {
                        if (WindowState == WindowState.Maximized)
                        {
                            WindowState = WindowState.Normal;
                            Top = restoreTop - 10;
                            DragMove();
                        }
                    }
                };
            }
        }

        private void RegisterCloseButton()
        {
            closeButton = (Hyperlink)GetTemplateChild("PART_WindowCaptionCloseButton");

            if (closeButton != null)
            {
                closeButton.Click += (sender, e) => Close();
            }
        }

        private void RegisterHandle()
        {
            handle = new WindowInteropHelper(this).Handle;
            
            HwndSource hwndSource = HwndSource.FromHwnd(this.handle);

            if (hwndSource != null)
            {
                hwndSource.AddHook(WndProc);
            }
        }

        private void RegisterMaximizeButton()
        {
            maximizeButton = (Hyperlink)GetTemplateChild("PART_WindowCaptionMaximizeButton");

            if (maximizeButton != null)
            {
                maximizeButton.Click += (sender, e) =>
                {
                    if (WindowState == WindowState.Normal)
                    {
                        WindowState = WindowState.Maximized;
                    }
                    else
                    {
                        WindowState = WindowState.Normal;
                    }
                };
            }
        }

        private void RegisterMinimizeButton()
        {
            minimizeButton = (Hyperlink)GetTemplateChild("PART_WindowCaptionMinimizeButton");

            if (minimizeButton != null)
            {
                minimizeButton.Click += (sender, e) => WindowState = WindowState.Minimized;
            }
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        private void RegisterBorderEvents(WindowBorderEdge borderEdge, FrameworkElement border)
        {
            border.MouseEnter += (sender, e) =>
            {
                if (WindowState == WindowState.Maximized || ResizeMode != ResizeMode.CanResize)
                {
                    border.Cursor = Cursors.Arrow;
                    return;
                }

                switch (borderEdge)
                {
                    case WindowBorderEdge.Left:
                    case WindowBorderEdge.Right:
                        border.Cursor = Cursors.SizeWE;
                        break;
                    case WindowBorderEdge.Top:
                    case WindowBorderEdge.Bottom:
                        border.Cursor = Cursors.SizeNS;
                        break;
                    case WindowBorderEdge.TopLeft:
                    case WindowBorderEdge.BottomRight:
                        border.Cursor = Cursors.SizeNWSE;
                        break;
                    case WindowBorderEdge.TopRight:
                    case WindowBorderEdge.BottomLeft:
                        border.Cursor = Cursors.SizeNESW;
                        break;
                }
            };

            border.MouseLeftButtonDown += (sender, e) =>
            {
                if (WindowState != WindowState.Maximized && ResizeMode == ResizeMode.CanResize)
                {
                    Point cursorLocation = e.GetPosition(this);
                    Point newCursorOffset = new Point();

                    switch (borderEdge)
                    {
                        case WindowBorderEdge.Left:
                            newCursorOffset.X = cursorLocation.X;
                            break;
                        case WindowBorderEdge.TopLeft:
                            newCursorOffset.X = cursorLocation.X;
                            newCursorOffset.Y = cursorLocation.Y;
                            break;
                        case WindowBorderEdge.Top:
                            newCursorOffset.Y = cursorLocation.Y;
                            break;
                        case WindowBorderEdge.TopRight:
                            newCursorOffset.X = (Width - cursorLocation.X);
                            newCursorOffset.Y = cursorLocation.Y;
                            break;
                        case WindowBorderEdge.Right:
                            newCursorOffset.X = (Width - cursorLocation.X);
                            break;
                        case WindowBorderEdge.BottomRight:
                            newCursorOffset.X = (Width - cursorLocation.X);
                            newCursorOffset.Y = (Height - cursorLocation.Y);
                            break;
                        case WindowBorderEdge.Bottom:
                            newCursorOffset.Y = (Height - cursorLocation.Y);
                            break;
                        case WindowBorderEdge.BottomLeft:
                            newCursorOffset.X = cursorLocation.X;
                            newCursorOffset.Y = (Height - cursorLocation.Y);
                            break;
                    }

                    this.cursorOffset = newCursorOffset;

                    border.CaptureMouse();
                }
            };

            border.MouseMove += (sender, e) =>
            {
                if (WindowState != WindowState.Maximized && border.IsMouseCaptured && ResizeMode == ResizeMode.CanResize)
                {
                    Point cursorLocation = e.GetPosition(this);

                    double nHorizontalChange = (cursorLocation.X - cursorOffset.X);
                    double pHorizontalChange = (cursorLocation.X + cursorOffset.X);
                    double nVerticalChange   = (cursorLocation.Y - cursorOffset.Y);
                    double pVerticalChange   = (cursorLocation.Y + cursorOffset.Y);

                    switch (borderEdge)
                    {
                        case WindowBorderEdge.Left:
                            if (Width - nHorizontalChange <= MinWidth) 
                                break;
                            Left   += nHorizontalChange;
                            Width  -= nHorizontalChange;
                            break;
                        case WindowBorderEdge.TopLeft:
                            if (Width - nHorizontalChange <= MinWidth)
                                break;
                            Left   += nHorizontalChange;
                            Width  -= nHorizontalChange;
                            if (Height - nVerticalChange <= MinHeight)
                                break;
                            Top    += nVerticalChange;
                            Height -= nVerticalChange;
                            break;
                        case WindowBorderEdge.Top:
                            if (Height - nVerticalChange <= MinHeight) 
                                break;
                            Top    += nVerticalChange;
                            Height -= nVerticalChange;
                            break;
                        case WindowBorderEdge.TopRight:
                            if (pHorizontalChange <= MinWidth)
                                break;
                            Width  = pHorizontalChange;
                            if (Height - nVerticalChange <= MinHeight)
                                break;
                            Top    += nVerticalChange;
                            Height -= nVerticalChange;
                            break;
                        case WindowBorderEdge.Right:
                            if (pHorizontalChange <= MinWidth)
                                break;
                            Width = pHorizontalChange;
                            break;
                        case WindowBorderEdge.BottomRight:
                            if (pHorizontalChange <= MinWidth)
                                break;
                            Width  = pHorizontalChange;
                            if (pVerticalChange <= MinHeight)
                                break;
                            Height = pVerticalChange;
                            break;
                        case WindowBorderEdge.Bottom:
                            if (pVerticalChange <= MinHeight)
                                break;
                            Height = pVerticalChange;
                            break;
                        case WindowBorderEdge.BottomLeft:
                            if (Width - nHorizontalChange <= MinWidth)
                                break;
                            Left   += nHorizontalChange;
                            Width  -= nHorizontalChange;
                            if (pVerticalChange <= MinHeight)
                                break;
                            Height = pVerticalChange;
                            break;
                    }
                }
            };

            border.MouseLeftButtonUp += (sender, e) => border.ReleaseMouseCapture();
        }

        private static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
            }

            return IntPtr.Zero;
        }

        private static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
        {
            MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));

            const int MONITOR_DEFAULTTONEAREST = 0x00000002;

            IntPtr monitor = NativeMethods.MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);

            if (monitor != IntPtr.Zero)
            {
                MONITORINFO monitorInfo = new MONITORINFO
                                          {
                                              cbSize = Marshal.SizeOf(typeof (MONITORINFO))
                                          };

                NativeMethods.GetMonitorInfo(monitor, ref monitorInfo);

                RECT rcWorkArea = monitorInfo.rcWork;
                RECT rcMonitorArea = monitorInfo.rcMonitor;

                mmi.ptMaxPosition.x = Math.Abs(rcWorkArea.left - rcMonitorArea.left);
                mmi.ptMaxPosition.y = Math.Abs(rcWorkArea.top - rcMonitorArea.top);
                mmi.ptMaxSize.x = Math.Abs(rcWorkArea.right - rcWorkArea.left);
                mmi.ptMaxSize.y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
            }

            Marshal.StructureToPtr(mmi, lParam, true);
        }

        private enum WindowBorderEdge
        {
            Left,
            TopLeft,
            Top,
            TopRight,
            Right,
            BottomRight,
            Bottom,
            BottomLeft
        }
    }
}