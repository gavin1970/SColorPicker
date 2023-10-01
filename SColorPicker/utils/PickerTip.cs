using System;
using System.Collections.Generic;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SColorPicker.utils
{
    internal class PickerTip : IDisposable
    {
        protected const int EVENT_SHUTDOWN = 0;
        protected const int EVENT_TIMEOUT = 258;
        const int defaultWaitCount = 20;
        const int waitStopThread = 5000;

        ManualResetEvent shutDownEvent;                               //could be shut down event or new message event
        private List<string> pickerTips = new List<string>();
        private int tipID = 0;
        private Thread tipThread;
        private bool disposedValue;

        public delegate void PickerTipEventHandler(object sender, PickerTipEventArgs e);
        public event PickerTipEventHandler PickerTipEvent;

        internal PickerTip(int msTipTimer)
        {
            LoadTips();
            shutDownEvent = new ManualResetEvent(false);

            tipThread = new Thread(() => {
                ShowTips(msTipTimer);
                return;
            });
            tipThread.Start();
        }
        ~PickerTip()
        {
            Dispose(disposing: false);
        }
        void IDisposable.Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (!StopThread(waitStopThread))
                        tipThread.Abort();
                }
                disposedValue = true;
            }
        }
        private bool StopThread(int msToWait)
        {
            //fires event to stop thread.
            shutDownEvent?.Set();
            //stay out of infinite loop
            DateTime dateTime = DateTime.UtcNow.AddMilliseconds(msToWait);

            while (tipThread != null && tipThread.IsAlive && DateTime.UtcNow < dateTime)
                Thread.Sleep(10);

            return tipThread == null || !tipThread.IsAlive;
        }
        private void LoadTips()
        {
            pickerTips.Add("Pick Color, Use R-Click and Scroll to Zoom.");
            pickerTips.Add("While Zoom, Escape or R-Click exits Zoom.");
            pickerTips.Add("Pick Color, Enter or L-Click selects crosshair Color.");
            pickerTips.Add("While Zoom, ArrowKeys will move Cursor 1px.");
        }
        private void ShowTips(int msTipTimer)
        {
            int timer, waitCount = defaultWaitCount;

            do
            {
                string tip = pickerTips[tipID];

                if (PickerTipEvent == null && waitCount > 0)
                {
                    timer = 100;   //make timeout less, since no event setup from form yet.
                    waitCount--;
                }
                else
                {
                    waitCount = defaultWaitCount;
                    timer = msTipTimer;

                    PickerTipEvent?.Invoke(this, new PickerTipEventArgs
                    {
                        TipId = tipID,
                        Tip = tip
                    });

                    tipID = tipID == pickerTips.Count - 1 ? 0 : tipID = tipID + 1;
                }
            } while (!shutDownEvent.WaitOne(timer));
        }
    }
    public class PickerTipEventArgs : EventArgs
    {
        public int TipId { get; set; }
        public string Tip { get; set; }
    }
}
