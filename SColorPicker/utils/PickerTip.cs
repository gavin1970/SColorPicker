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
        const int m_defaultWaitCount = 20;
        const int m_waitStopThread = 5000;

        ManualResetEvent m_shutDownEvent;                               //could be shut down event or new message event
        private List<string> m_pickerTips = new List<string>();
        private int m_tipId = 0;
        private Thread m_tipThread;
        private bool m_disposedValue;

        public delegate void PickerTipEventHandler(object sender, PickerTipEventArgs e);
        public event PickerTipEventHandler PickerTipEvent;

        internal PickerTip(int msTipTimer)
        {
            LoadTips();
            m_shutDownEvent = new ManualResetEvent(false);

            m_tipThread = new Thread(() => {
                ShowTips(msTipTimer);
                return;
            });
            m_tipThread.Start();
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
            if (!m_disposedValue)
            {
                if (disposing)
                {
                    if (!StopThread(m_waitStopThread))
                        m_tipThread.Abort();
                }
                m_disposedValue = true;
            }
        }
        private bool StopThread(int msToWait)
        {
            //fires event to stop thread.
            m_shutDownEvent?.Set();
            //stay out of infinite loop
            DateTime dateTime = DateTime.UtcNow.AddMilliseconds(msToWait);

            while (m_tipThread != null && m_tipThread.IsAlive && DateTime.UtcNow < dateTime)
                Thread.Sleep(10);

            return m_tipThread == null || !m_tipThread.IsAlive;
        }
        private void LoadTips()
        {
            m_pickerTips.Add("Pick Color, Use R-Click and Scroll to Zoom.");
            m_pickerTips.Add("While Zoom, Escape or R-Click exits Zoom.");
            m_pickerTips.Add("Pick Color, Enter or L-Click selects crosshair Color.");
            m_pickerTips.Add("While Zoom, ArrowKeys will move Cursor 1px.");
        }
        private void ShowTips(int msTipTimer)
        {
            int timer, waitCount = m_defaultWaitCount;

            do
            {
                string tip = m_pickerTips[m_tipId];

                if (PickerTipEvent == null && waitCount > 0)
                {
                    timer = 100;   //make timeout less, since no event setup from form yet.
                    waitCount--;
                }
                else
                {
                    waitCount = m_defaultWaitCount;
                    timer = msTipTimer;

                    PickerTipEvent?.Invoke(this, new PickerTipEventArgs
                    {
                        TipId = m_tipId,
                        Tip = tip
                    });

                    m_tipId = m_tipId == m_pickerTips.Count - 1 ? 0 : m_tipId = m_tipId + 1;
                }
            } while (!m_shutDownEvent.WaitOne(timer));
        }
    }
    internal class PickerTipEventArgs : EventArgs
    {
        public int TipId { get; set; }
        public string Tip { get; set; }
    }
}
