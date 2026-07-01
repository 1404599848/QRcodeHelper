using System;
using System.IO.Ports;
using System.Timers;

namespace QRcodeHelper.Services
{
    /// <summary>
    /// LCUS-4 USB 继电器模块控制（三色灯+蜂鸣器）
    /// IO分配: CH1=红灯(漏码), CH2=黄灯(重码), CH3=绿灯(正常), CH4=蜂鸣器
    /// 协议: 9600,8N1；开 CH1: A0 01 01 A2；关 CH1: A0 01 00 A1
    /// </summary>
    public class RelayController : IDisposable
    {
        private SerialPort _port;
        private Timer _flashTimer;
        private Timer _durationTimer;
        private bool _flashState;
        private int _currentFlashChannel;

        public const int CH_RED = 1;
        public const int CH_YELLOW = 2;
        public const int CH_GREEN = 3;
        public const int CH_BUZZER = 4;

        public bool IsConnected => _port?.IsOpen ?? false;

        public RelayController()
        {
            _flashTimer = new Timer();
            _flashTimer.Elapsed += FlashTimer_Elapsed;
            _flashTimer.AutoReset = true;

            _durationTimer = new Timer();
            _durationTimer.Elapsed += DurationTimer_Elapsed;
            _durationTimer.AutoReset = false;
        }

        /// <summary>
        /// 连接到串口继电器模块
        /// </summary>
        public bool Connect(string portName, int baudRate = 9600)
        {
            try
            {
                if (_port != null && _port.IsOpen)
                {
                    if (_port.PortName == portName && _port.BaudRate == baudRate)
                        return true;
                    _port.Close();
                }

                _port = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
                _port.Open();
                AllOff();
                NLogHelper.Info($"RelayController connected on {portName} @ {baudRate}");
                return true;
            }
            catch (Exception ex)
            {
                NLogHelper.Error($"RelayController Connect({portName}) failed: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 断开串口连接
        /// </summary>
        public void Disconnect()
        {
            StopAll();
            try
            {
                if (_port != null && _port.IsOpen)
                    _port.Close();
            }
            catch (Exception ex)
            {
                NLogHelper.Error($"RelayController Disconnect failed: {ex.Message}");
            }
            _port = null;
        }

        /// <summary>
        /// 从 AppConfig 读取串口配置并连接
        /// </summary>
        public bool ConnectFromConfig()
        {
            var port = AppConfig.SerialPort;
            if (string.IsNullOrWhiteSpace(port))
                return false;
            return Connect(port, AppConfig.BaudRate);
        }

        // ---- 底层指令 ----

        private byte[] BuildCommand(int channel, bool turnOn)
        {
            byte cmd = turnOn ? (byte)0x01 : (byte)0x00;
            byte[] buf = new byte[4];
            buf[0] = 0xA0;
            buf[1] = (byte)channel;
            buf[2] = cmd;
            buf[3] = (byte)(0xA0 + channel + cmd); // 校验和
            return buf;
        }

        private void Write(byte[] data)
        {
            if (!IsConnected) return;
            try
            {
                _port.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                NLogHelper.Error($"RelayController write failed: {ex.Message}");
            }
        }

        /// <summary>打开指定通道</summary>
        public void ChannelOn(int channel)
        {
            Write(BuildCommand(channel, true));
        }

        /// <summary>关闭指定通道</summary>
        public void ChannelOff(int channel)
        {
            Write(BuildCommand(channel, false));
        }

        /// <summary>关闭所有通道</summary>
        public void AllOff()
        {
            ChannelOff(CH_RED);
            ChannelOff(CH_YELLOW);
            ChannelOff(CH_GREEN);
            ChannelOff(CH_BUZZER);
        }

        // ---- 业务模式 ----

        /// <summary>正常通过 → 绿灯常亮</summary>
        public void SetNormal()
        {
            StopAll();
            if (!AppConfig.AlertEnabled) return;
            ChannelOn(CH_GREEN);
            StartDurationTimer();
        }

        /// <summary>重码警报 → 黄灯闪烁 + 蜂鸣器</summary>
        public void SetDuplicateAlert()
        {
            StopAll();
            if (!AppConfig.AlertEnabled) return;
            StartFlash(CH_YELLOW);
            ChannelOn(CH_BUZZER);
            StartDurationTimer();
        }

        /// <summary>漏码警报 → 红灯闪烁 + 蜂鸣器</summary>
        public void SetMissingAlert()
        {
            StopAll();
            if (!AppConfig.AlertEnabled) return;
            StartFlash(CH_RED);
            ChannelOn(CH_BUZZER);
            StartDurationTimer();
        }

        /// <summary>跳号 → 黄灯短暂常亮</summary>
        public void SetSkipAlert()
        {
            StopAll();
            if (!AppConfig.AlertEnabled) return;
            ChannelOn(CH_YELLOW);
            StartDurationTimer();
        }

        /// <summary>解除所有警报</summary>
        public void DismissAlert()
        {
            StopAll();
        }

        // ---- 内部控制 ----

        private void StartFlash(int channel)
        {
            _currentFlashChannel = channel;
            _flashState = true;
            ChannelOn(channel);
            _flashTimer.Interval = AppConfig.FlashIntervalMs;
            _flashTimer.Start();
        }

        private void StartDurationTimer()
        {
            _durationTimer.Interval = AppConfig.AlertDurationSec * 1000;
            _durationTimer.Start();
        }

        private void FlashTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _flashState = !_flashState;
            if (_flashState)
                ChannelOn(_currentFlashChannel);
            else
                ChannelOff(_currentFlashChannel);
        }

        private void DurationTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            StopAll();
        }

        /// <summary>停止所有定时器并关闭所有通道</summary>
        public void StopAll()
        {
            _flashTimer.Stop();
            _durationTimer.Stop();
            _flashState = false;
            AllOff();
        }

        // ---- IDisposable ----

        public void Dispose()
        {
            StopAll();
            _flashTimer.Dispose();
            _durationTimer.Dispose();
            Disconnect();
        }
    }
}
