using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using Krypton.Toolkit;
using Timer = System.Timers.Timer;

namespace DU_Industry_Tool
{
    // Derivates to toggle visibility based on set text
    public class KLabel : KryptonLabel
    {
        private Timer _timer;
        private Timer _fadeTimer;
        private int _timeout;
        private Color _initialColor1;
        private Color _initialColor2;
        private int _fadeStep;

        public KLabel()
        {
            _timer = new Timer();
            _timer.Elapsed += OnTimeout;
            _fadeTimer = new Timer();
            _fadeTimer.Interval = 250; // Fade step duration
            _fadeTimer.Elapsed += OnFade;
            Timeout = 2000; // Default value
            _initialColor1 = Color.Navy;
            _initialColor2 = Color.Navy;
        }

        public int Timeout
        {
            get => _timeout;
            private set
            {
                _timeout = value;
                _timer.Interval = value;
            }
        }

        private void StopTimers()
        {
            _timer.Stop();
            _fadeTimer.Stop();
        }

        public void Stop()
        {
            if (!_timer.Enabled && !_fadeTimer.Enabled) return;
            StopTimers();
            Invoke(new MethodInvoker(() =>
            {
                Visible = false;
            }));
        }

        private void ShowLabel()
        {
            StopTimers();
            Invoke(new MethodInvoker(() =>
            {
                StateNormal.ShortText.Color1 = _initialColor1;
                StateNormal.ShortText.Color2 = _initialColor2;
                StateNormal.ShortText.ColorStyle = PaletteColorStyle.Inherit;
                Visible = true;
            }));
            _timer.Start();
        }

        public void StartTimeout()
        {
            ShowLabel();
        }

        public void FadeOut()
        {
            ShowLabel();
        }
        
        private void OnTimeout(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
            _fadeStep = 0;
            _fadeTimer.Start();
        }

        private void OnFade(object sender, ElapsedEventArgs e)
        {
            if (_fadeStep < 10)
            {
                Invoke(new MethodInvoker(() =>
                {
                    StateNormal.ShortText.Color1 = Color.FromArgb(
                        (_initialColor1.R * (10 - _fadeStep) + BackColor.R * _fadeStep) / 10,
                        (_initialColor1.G * (10 - _fadeStep) + BackColor.G * _fadeStep) / 10,
                        (_initialColor1.B * (10 - _fadeStep) + BackColor.B * _fadeStep) / 10
                    );
                    StateNormal.ShortText.Color2 = Color.FromArgb(
                        (_initialColor2.R * (10 - _fadeStep) + BackColor.R * _fadeStep) / 10,
                        (_initialColor2.G * (10 - _fadeStep) + BackColor.G * _fadeStep) / 10,
                        (_initialColor2.B * (10 - _fadeStep) + BackColor.B * _fadeStep) / 10
                    );
                }));
                _fadeStep++;
            }
            else
            {
                _fadeTimer.Stop();
                Invoke(new MethodInvoker(() =>
                {
                    Visible = false;
                    StateNormal.ShortText.Color1 = _initialColor1;
                    StateNormal.ShortText.Color2 = _initialColor2;
                }));
            }
        }

        public void SetText(string value = null, bool visible = true)
        {
            if (value != null)
            {
                Values.Text = value;
            }
            Visible = visible;
        }

        public void Hide(string value = null)
        {
            StopTimers();
            SetText(value, false);
        }

        public override string Text
        {
            get => Values.Text;
            set => SetText(value);
        }
    }

    public class KLinkLabel : KryptonLinkLabel
    {
        public void SetText(string value = null, bool visible = true)
        {
            if (value != null)
            {
                Values.Text = value;
            }
            Visible = visible;
        }

        public void Hide(string value = null)
        {
            SetText(value, false);
        }

        public override string Text
        {
            get => Values.Text;
            set => SetText(value);
        }
    }
}
