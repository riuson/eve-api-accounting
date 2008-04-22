#define animation

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace Accounting
{
    public partial class StartPageMenuCommandItem : UserControl, IStartPageMenuItem
    {
        public StartPageMenuCommandItem()
        {
            InitializeComponent();
            mTitleFont = new Font(this.Font, FontStyle.Bold);
            mDescriptionFont = this.Font;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            mBackDraw = new Bitmap(this.Width, this.Height);

#if animation
            timerAsyncDebugRun.Interval = 500;
            timerAsyncDebugRun.Tick += new EventHandler(timerAsyncDebugRun_Tick);
#endif
        }
        public StartPageMenuCommandItem(Image icon, string title, string description, string command)
            : this()
        {
            mIcon = icon;
            mTitle = title;
            mDescription = description;
            mCommand = command;
        }
        //private bool mMouseOver;
        private bool mMouseDown;
        private string mCommand;
        private Image mIcon;
        private string mTitle;
        private string mDescription;
        private Font mTitleFont;
        private Font mDescriptionFont;
        private Bitmap mBackDraw;

        public Image Icon
        {
            get { return mIcon; }
            set { mIcon = value; }
        }
        public string Title
        {
            get { return mTitle; }
            set { mTitle = value; }
        }
        public string Description
        {
            get { return mDescription; }
            set { mDescription = value; }
        }
        public Font DescriptionFont
        {
            get { return mDescriptionFont; }
            set { mDescriptionFont = value; }
        }


        static ColorMatrix mColorMatrixBW;
        static ColorMatrix mColorMatrixUnhover;
        static ColorMatrix mColorMatrixFullColor;
        public static ColorMatrix ColorMatrixBW
        {
            get
            {
                if (mColorMatrixBW == null)
                {
                    float[][] matrixItemsBW ={ 
                        new float[] {0.299f, 0.299f, 0.299f, 0, 0},
                        new float[] {0.587f, 0.587f, 0.587f, 0, 0},
                        new float[] {0.114f, 0.114f, 0.114f, 0, 0},
                        new float[] {0, 0, 0, 1, 0}, 
                        new float[] {0, 0, 0, 0, 1}};
                    mColorMatrixBW = new ColorMatrix(matrixItemsBW);
                }
                return mColorMatrixBW;
            }
        }
        public static ColorMatrix ColorMatrixUnhover
        {
            get
            {
                if (mColorMatrixBW == null)
                {
                    float[][] matrixItemsUnhover ={ 
                        new float[] {1, 0, 0, 0, 0},
                        new float[] {0, 1, 0, 0, 0},
                        new float[] {0, 0, 1, 0, 0},
                        new float[] {0, 0, 0, 0.7f, 0}, 
                        new float[] {0, 0, 0, 0, 1}};
                    mColorMatrixUnhover = new ColorMatrix(matrixItemsUnhover);
                }
                return mColorMatrixUnhover;
            }
        }
        public static ColorMatrix ColorMatrixFullColor
        {
            get
            {
                if (mColorMatrixBW == null)
                {
                    float[][] matrixItemsFullColor ={ 
                        new float[] {1, 0, 0, 0, 0},
                        new float[] {0, 1, 0, 0, 0},
                        new float[] {0, 0, 1, 0, 0},
                        new float[] {0, 0, 0, 1, 0}, 
                        new float[] {0, 0, 0, 0, 1}};
                    mColorMatrixFullColor = new ColorMatrix(matrixItemsFullColor);
                }
                return mColorMatrixFullColor;
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Pen oPen = null;
            List<PointF> poly = new List<PointF>();

            Rectangle r = this.ClientRectangle;
            if (mBackDraw.Width != this.Width ||
                mBackDraw.Height != this.Height)
            {
                mBackDraw.Dispose();
                mBackDraw = new Bitmap(this.Width, this.Height);
            }
            //Graphics gr = Graphics.FromImage(mBackDraw);
            Graphics gr = pevent.Graphics;
            gr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            gr.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            Brush oBrush = null;

            r.Inflate(-1, -1);
            ColorMatrix colorMatrix = ColorMatrixUnhover;
            //возможные состояния:
            //кнопка не нажата, без фокуса, без мыши над ней
            //кнопка нажата, остальное не важно
            //мышь над кнопкой
            //кнопка с фокусом
            //ничего нигде нет
            {
                oPen = new Pen(Color.FromArgb(238, 238, 238));
                oBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    ClientRectangle,
                    this.BackColor, Color.FromArgb(246, 246, 246),
                    System.Drawing.Drawing2D.LinearGradientMode.Vertical);

            }
            //кнопка с фокусом
            if (Focused)
            {
                oPen = new Pen(Color.FromArgb(200, 200, 200));
                oBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                        ClientRectangle,
                        this.BackColor, Color.FromArgb(236, 236, 236),
                        System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            }
            //мышь над кнопкой
            if (MouseOver)
            {
                oPen = new Pen(Color.FromArgb(198, 198, 198));
                //the mouse is over is, draw the hover border
                //oPen = new Pen(Color.FromArgb(198, 198, 198));
                ////oPen = new Pen(Color.Orange, 1.2f);
                //oBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                //    ClientRectangle,
                //    this.BackColor, Color.FromArgb(220, 220, 220),
                //    System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                colorMatrix = ColorMatrixFullColor;
#if animation
                colorMatrix.Matrix33 = mAnimationStep;
#endif
            }
            //кнопка нажата мышью или клавишей
            if (mMouseDown)
            {
                oPen = new Pen(Color.FromArgb(164, 164, 164));
                oBrush = new System.Drawing.Drawing2D.LinearGradientBrush(
                    ClientRectangle,
                    this.BackColor, Color.FromArgb(220, 220, 220),
                    System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            }
            {
                //вывод фона и рамки
                poly.Add(new PointF(r.X + 2, r.Y));
                poly.Add(new PointF(r.X + r.Width - 2, r.Y));
                poly.Add(new PointF(r.X + r.Width, r.Y + 2));
                poly.Add(new PointF(r.X + r.Width, r.Y + r.Height - 2));
                poly.Add(new PointF(r.X + r.Width - 2, r.Y + r.Height));
                poly.Add(new PointF(r.X + 2, r.Y + r.Height));
                poly.Add(new PointF(r.X, r.Y + r.Height - 2));
                poly.Add(new PointF(r.X, r.Y + 2));
                poly.Add(new PointF(r.X + 2, r.Y));

                gr.FillRectangle(Brushes.White, this.ClientRectangle);
                if (oBrush == null)
                {
                    gr.FillPolygon(new SolidBrush(BackColor), poly.ToArray());
                }
                else
                {
                    gr.FillPolygon(oBrush, poly.ToArray());
                    oBrush.Dispose();
                }
                if (oPen != null)
                {
                    oPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;

                    gr.DrawLines(oPen, poly.ToArray());
                    oPen.Dispose();
                }
            }
            //вывод текста
            Rectangle area = ClientRectangle;
            area.Inflate(-2, -2);
            int dx, dy;
            int padding = 5;
            area = Rectangle.FromLTRB(area.Left + padding, area.Top + padding, area.Right - padding, area.Bottom - padding);
            if (mMouseDown && MouseOver)
                area.Offset(1, 1);
            Point imagePoint = Point.Empty;
            if (this.Icon != null)
            {
                Image im = mIcon;// Bitmap.FromHicon(mIcon.Handle);
                dx = (area.Width - this.Icon.Width) / 2;
                dy = (area.Height - this.Icon.Height) / 2;
                {
                    ImageAttributes imageAtt = new ImageAttributes();
                    imageAtt.SetColorMatrix(
                       colorMatrix,
                       ColorMatrixFlag.Default,
                       ColorAdjustType.Bitmap);

                    // Now draw the semitransparent bitmap image.
                    int iWidth = im.Width;
                    int iHeight = im.Height;
#if animation
                    mAnimationRect = new Rectangle(area.Left, area.Top + dy / 2, iWidth, iHeight);
#endif
                    gr.DrawImage(
                       im,
                       new Rectangle(area.Left, area.Top + dy / 2, iWidth, iHeight),  // destination rectangle
                       0.0f,                          // source rectangle x 
                       0.0f,                          // source rectangle y
                       iWidth,                        // source rectangle width
                       iHeight,                       // source rectangle height
                       GraphicsUnit.Pixel,
                       imageAtt);
                }
            }
            {
                RectangleF rectText = RectangleF.FromLTRB(
                        area.Left + padding + mIcon.Width,
                        area.Top,
                        area.Right - padding,
                        area.Top + 20);

                if (MouseOver)
                {
                    if (!mTitleFont.Underline)
                        mTitleFont = new Font(mTitleFont, FontStyle.Underline | FontStyle.Bold);
                }
                else
                {
                    if (mTitleFont.Underline)
                        mTitleFont = new Font(mTitleFont, FontStyle.Bold);
                }
                if (this.mTitle != null && mTitle != "")
                {
                    SizeF size = pevent.Graphics.MeasureString(mTitle, mTitleFont, (int)rectText.Width);
                    rectText.Height = size.Height;
                    gr.DrawString(
                        mTitle,
                        mTitleFont,
                        Brushes.Navy,
                        rectText);
                }
                if (this.mDescription != null && mDescription != "")
                {
                    rectText.Y = rectText.Bottom;// +_padding;
                    rectText.Height = area.Bottom - padding - rectText.Y;
                    gr.DrawString(
                        mDescription,
                        mDescriptionFont,
                        Brushes.DarkGray,
                        rectText);
                }
                //gr.DrawString(
                //        mAnimationStep.ToString(),
                //        mDescriptionFont,
                //        Brushes.Silver,
                //        0.0f, 0.0f);
            }
        }
        public bool MouseOver
        {
            get
            {
                if (!this.IsDisposed)
                {
                    return this.RectangleToScreen(this.ClientRectangle).Contains(MousePosition);
                }
                else
                {
                    return false;
                }
            }
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Invalidate();
#if animation
            if (!mIsAnimationStarted)
                RunAnimationAsync();
#endif
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Invalidate();
        }
        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
                mMouseDown = true;
            base.OnMouseDown(mevent);
            Invalidate();
        }
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
                mMouseDown = false;
            base.OnMouseUp(mevent);
            Invalidate();
        }
        protected override void OnKeyDown(KeyEventArgs kevent)
        {
            if (kevent.KeyCode == Keys.Space)
            {
                mMouseDown = true;
                Invalidate();
            }
            base.OnKeyDown(kevent);
        }
        protected override void OnKeyUp(KeyEventArgs kevent)
        {
            if (kevent.KeyCode == Keys.Space)
            {
                mMouseDown = false;
                Invalidate();
            }
            base.OnKeyUp(kevent);
        }
        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            Invalidate();
        }
        protected override void OnEnter(EventArgs e)
        {
            base.OnLeave(e);
            Invalidate();
        }

        #region IStartPageMenuItem Members

        /// <summary>
        /// текст команды
        /// </summary>
        public string Command
        {
            get { return mCommand; }
        }
        public StartPageMenuItemType Type
        {
            get { return StartPageMenuItemType.Command; }
        }
        #endregion

        private void OnMouseClick(object sender, EventArgs e)
        {
            this.OnClick(e);
        }

#if animation
        #region AsyncRedraw
        System.Windows.Forms.Timer timerAsyncDebugRun = new System.Windows.Forms.Timer();
        void timerAsyncDebugRun_Tick(object sender, EventArgs e)
        {
            timerAsyncDebugRun.Stop();
            FunctionAnimation();
            AnimationCompleted();
        }

        delegate void FunctionAnimationDelegate();
        float mAnimationStep = 0.25f;
        Rectangle mAnimationRect;
        bool mIsAnimationStarted;
        bool mAnimationDirectionUp = true;

        void FunctionAnimation()
        {
            if (mAnimationDirectionUp)
            {
                mAnimationStep += 0.1f;
                if (mAnimationStep >= 0.9f)
                    mAnimationDirectionUp = false;
            }
            else
            {
                mAnimationStep -= 0.1f;
                if (mAnimationStep < 0.5f)
                    mAnimationDirectionUp = true;
            }
            Thread.Sleep(50);
        }

        void RunAnimationAsync()
        {
#if DEBUG
            timerAsyncDebugRun.Start();
#else
            FunctionAnimationDelegate f = this.FunctionAnimation;
            f.BeginInvoke(new AsyncCallback(AnimationCompletedCallBack), this);
#endif
            mIsAnimationStarted = true;
        }

        void AnimationCompletedCallBack(IAsyncResult result)
        {
            try
            {
                AsyncResult r = (AsyncResult)result;
                FunctionAnimationDelegate command = (FunctionAnimationDelegate)r.AsyncDelegate;
                command.EndInvoke(result);
                AnimationCompleted();
            }
            catch// (Exception exc)
            {
            }
        }

        void AnimationCompleted()
        {
            if (this.InvokeRequired)
            {
                this.Invoke((EventHandler)delegate
                {
                    AnimationCompleted();
                });
            }
            else
            {
                //if (mAnimationRect != null)
                //    Invalidate(mAnimationRect);
                Invalidate();
                mIsAnimationStarted = false;

                if (MouseOver)
                    RunAnimationAsync();
            }
        }
        #endregion
#endif
    }
}
