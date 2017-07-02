// *
// * Copyright (C) 2008 Roger Alsing : http://www.RogerAlsing.com
// *
// * This library is free software; you can redistribute it and/or modify it
// * under the terms of the GNU Lesser General Public License 2.1 or later, as
// * published by the Free Software Foundation. See the included license.txt
// * or http://www.gnu.org/copyleft/lesser.html for details.
// *
// *

using Alsing.Windows;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Alsing.Drawing.GDI
{
    public class GDISurface : GDIObject
    {
        private WeakReference _Control;
        protected IntPtr _OldBmp = IntPtr.Zero;
        protected IntPtr _OldBrush = IntPtr.Zero;
        protected IntPtr _OldFont = IntPtr.Zero;
        protected IntPtr _OldPen = IntPtr.Zero;
        protected IntPtr mhBMP;
        protected IntPtr mhDC;
        protected int mHeight;
        protected int mTabSize = 4;
        protected int mWidth;

        public GDISurface(IntPtr hDC)
        {
            mhDC = hDC;
        }

        public GDISurface(int width, int height, IntPtr hdc)
        {
            Init(width, height, hdc);
            Create();
        }

        public GDISurface(int width, int height, GDISurface surface)
        {
            Init(width, height, surface.hDC);
            Create();
        }

        public GDISurface(int width, int height, Control CompatibleControl, bool BindControl)
        {
            IntPtr hDCControk = Windows.NativeMethods.ControlDC(CompatibleControl);
            Init(width, height, hDCControk);
            Windows.NativeMethods.ReleaseDC(CompatibleControl.Handle, hDCControk);

            if (BindControl)
            {
                Control = CompatibleControl;
            }

            Create();
        }

        private Control Control
        {
            get
            {
                if (_Control != null)
                    return (Control)_Control.Target;
                return null;
            }
            set { _Control = new WeakReference(value); }
        }

        public IntPtr hDC
        {
            get { return mhDC; }
        }

        public IntPtr hBMP
        {
            get { return mhBMP; }
        }

        public Color TextForeColor
        {
            //map get,settextcolor
            get { return Windows.NativeMethods.IntToColor(Windows.NativeMethods.GetTextColor(mhDC)); }
            set { Windows.NativeMethods.SetTextColor(mhDC, Windows.NativeMethods.ColorToInt(value)); }
        }

        public Color TextBackColor
        {
            //map get,setbkcolor
            get { return Windows.NativeMethods.IntToColor(Windows.NativeMethods.GetBkColor(mhDC)); }
            set { Windows.NativeMethods.SetBkColor(mhDC, Windows.NativeMethods.ColorToInt(value)); }
        }

        public bool FontTransparent
        {
            //map get,setbkmode
            //1=transparent , 2=solid
            get { return Windows.NativeMethods.GetBkMode(mhDC) < 2; }
            set { Windows.NativeMethods.SetBkMode(mhDC, value ? 1 : 2); }
        }

        public GDIFont Font
        {
            get
            {
                var tm = new GDITextMetric();
                var fontname = new string(' ', 48);

                Windows.NativeMethods.GetTextMetrics(mhDC, ref tm);
                Windows.NativeMethods.GetTextFace(mhDC, 79, fontname);

                var gf = new GDIFont
                {
                    FontName = fontname,
                    Bold = (tm.tmWeight > 400),
                    Italic = (tm.tmItalic != 0),
                    Underline = (tm.tmUnderlined != 0),
                    Strikethrough = (tm.tmStruckOut != 0),
                    Size = ((int)(((tm.tmMemoryHeight) / (double)tm.tmDigitizedAspectY) * 72))
                };

                return gf;
            }
            set
            {
                IntPtr res = Windows.NativeMethods.SelectObject(mhDC, value.hFont);
                if (_OldFont == IntPtr.Zero)
                    _OldFont = res;
            }
        }

        protected void Init(int width, int height, IntPtr hdc)
        {
            mWidth = width;
            mHeight = height;
            mhDC = Windows.NativeMethods.CreateCompatibleDC(hdc);

            mhBMP = Windows.NativeMethods.CreateCompatibleBitmap(hdc, width, height);

            IntPtr ret = Windows.NativeMethods.SelectObject(mhDC, mhBMP);
            _OldBmp = ret;

            if (mhDC == (IntPtr)0)
                throw new OutOfMemoryException("hDC creation FAILED!!");

            if (mhDC == (IntPtr)0)
                throw new OutOfMemoryException("hBMP creation FAILED!!");
        }

        public Size MeasureString(string Text)
        {
            //map GetTabbedTextExtent
            //to be implemented
            return new Size(0, 0);
        }

        public Size MeasureTabbedString(string Text, int tabsize)
        {
            int ret = Windows.NativeMethods.GetTabbedTextExtent(mhDC, Text, Text.Length, 1, ref tabsize);
            return new Size(ret & 0xFFFF, (ret >> 16) & 0xFFFF);
        }

        public void DrawString(string Text, int x, int y, int width, int height)
        {
            //to be implemented
            //map DrawText
        }

        public Size DrawTabbedString(string Text, int x, int y, int taborigin, int tabsize)
        {
            int ret = Windows.NativeMethods.TabbedTextOut(mhDC, x, y, Text, Text.Length, 1, ref tabsize, taborigin);
            return new Size(ret & 0xFFFF, (ret >> 16) & 0xFFFF);
        }

        //---------------------------------------
        //render methods ,
        //render to dc ,
        //render to control
        //render to gdisurface

        public void RenderTo(IntPtr hdc, int x, int y)
        {
            //map bitblt
            Windows.NativeMethods.BitBlt(hdc, x, y, mWidth, mHeight, mhDC, 0, 0, (int)GDIRop.SrcCopy);
        }

        public void RenderTo(GDISurface target, int x, int y)
        {
            RenderTo(target.hDC, x, y);
        }

        public void RenderTo(GDISurface target, int SourceX, int SourceY, int Width, int Height, int DestX, int DestY)
        {
            Windows.NativeMethods.BitBlt(target.hDC, DestX, DestY, Width, Height, hDC, SourceX, SourceY, (int)GDIRop.SrcCopy);
        }

        public void RenderToControl(int x, int y)
        {
            IntPtr hdc = Windows.NativeMethods.ControlDC(Control);

            RenderTo(hdc, x, y);
            Windows.NativeMethods.ReleaseDC(Control.Handle, hdc);
        }

        //---------------------------------------

        public Graphics CreateGraphics()
        {
            return Graphics.FromHdc(mhDC);
        }

        //---------------------------------------

        public void FillRect(GDIBrush brush, int x, int y, int width, int height)
        {
            APIRect gr;
            gr.top = y;
            gr.left = x;
            gr.right = width + x;
            gr.bottom = height + y;

            Windows.NativeMethods.FillRect(mhDC, ref gr, brush.hBrush);
        }

        public void DrawFocusRect(int x, int y, int width, int height)
        {
            APIRect gr;
            gr.top = y;
            gr.left = x;
            gr.right = width + x;
            gr.bottom = height + y;

            Windows.NativeMethods.DrawFocusRect(mhDC, ref gr);
        }

        public void FillRect(Color color, int x, int y, int width, int height)
        {
            var b = new GDIBrush(color);
            FillRect(b, x, y, width, height);
            b.Dispose();
        }

        public void InvertRect(int x, int y, int width, int height)
        {
            APIRect gr;
            gr.top = y;
            gr.left = x;
            gr.right = width + x;
            gr.bottom = height + y;

            Windows.NativeMethods.InvertRect(mhDC, ref gr);
        }

        public void DrawLine(GDIPen pen, Point p1, Point p2)
        {
            IntPtr oldpen = Windows.NativeMethods.SelectObject(mhDC, pen.hPen);
            APIPoint gp;
            gp.x = 0;
            gp.y = 0;
            Windows.NativeMethods.MoveToEx(mhDC, p1.X, p1.Y, ref gp);
            Windows.NativeMethods.LineTo(mhDC, p2.X, p2.Y);
            Windows.NativeMethods.SelectObject(mhDC, oldpen);
        }

        public void DrawLine(Color color, Point p1, Point p2)
        {
            var p = new GDIPen(color, 1);
            DrawLine(p, p1, p2);
            p.Dispose();
        }

        public void DrawRect(Color color, int left, int top, int width, int height)
        {
            var p = new GDIPen(color, 1);
            DrawRect(p, left, top, width, height);
            p.Dispose();
        }

        public void DrawRect(GDIPen pen, int left, int top, int width, int height)
        {
            DrawLine(pen, new Point(left, top), new Point(left + width, top));
            DrawLine(pen, new Point(left, top + height), new Point(left + width, top + height));
            DrawLine(pen, new Point(left, top), new Point(left, top + height));
            DrawLine(pen, new Point(left + width, top), new Point(left + width, top + height + 1));
        }

        public void Clear(Color color)
        {
            var b = new GDIBrush(color);
            Clear(b);
            b.Dispose();
        }

        public void Clear(GDIBrush brush)
        {
            FillRect(brush, 0, 0, mWidth, mHeight);
        }

        public void Flush()
        {
            Windows.NativeMethods.GdiFlush();
        }

        protected override void Destroy()
        {
            if (_OldBmp != IntPtr.Zero)
                Windows.NativeMethods.SelectObject(hDC, _OldBmp);

            if (_OldFont != IntPtr.Zero)
                Windows.NativeMethods.SelectObject(hDC, _OldFont);

            if (_OldPen != IntPtr.Zero)
                Windows.NativeMethods.SelectObject(hDC, _OldPen);

            if (_OldBrush != IntPtr.Zero)
                Windows.NativeMethods.SelectObject(hDC, _OldBrush);

            if (mhBMP != (IntPtr)0)
                Windows.NativeMethods.DeleteObject(mhBMP);

            if (mhDC != (IntPtr)0)
                Windows.NativeMethods.DeleteDC(mhDC);

            mhBMP = (IntPtr)0;
            mhDC = (IntPtr)0;

            base.Destroy();
        }

        public void SetBrushOrg(int x, int y)
        {
            APIPoint p;
            p.x = 0;
            p.y = 0;
            Windows.NativeMethods.SetBrushOrgEx(mhDC, x, y, ref p);
        }
    }
}