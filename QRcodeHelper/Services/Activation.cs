using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QRcodeHelper.Services
{
    public static class Activation
    {
        private static string licenseFile = "./License.key";

        /// <summary>
        /// 是否启用激活验证（默认false，可开关）
        /// </summary>
        public static bool Enabled { get; set; } = false;

        /// <summary>
        /// 当前是否已激活
        /// </summary>
        public static bool IsActivated { get; private set; } = false;

        /// <summary>
        /// 获取本机机器码
        /// </summary>
        public static string GetMachineCode()
        {
            try
            {
                var mac = "";
                var disk = "";

                using (var mc = new ManagementClass("Win32_NetworkAdapterConfiguration"))
                {
                    using (var instances = mc.GetInstances())
                    {
                        foreach (var mo in instances)
                        {
                            if ((bool)mo["IPEnabled"])
                            {
                                mac = mo["MacAddress"]?.ToString() ?? "";
                                break;
                            }
                        }
                    }
                }

                using (var mc = new ManagementClass("Win32_DiskDrive"))
                {
                    using (var instances = mc.GetInstances())
                    {
                        foreach (var mo in instances)
                        {
                            disk = mo["SerialNumber"]?.ToString()?.Trim() ?? "";
                            break;
                        }
                    }
                }

                var raw = $"{mac}-{disk}";
                using (var sha = SHA256.Create())
                {
                    var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(raw));
                    return BitConverter.ToString(hash).Replace("-", "").Substring(0, 16).ToUpper();
                }
            }
            catch
            {
                return "ERROR";
            }
        }

        /// <summary>
        /// 验证激活码
        /// </summary>
        public static bool Verify(string code)
        {
            try
            {
                var machineCode = GetMachineCode();
                var expected = GenerateCode(machineCode);
                return string.Equals(code?.Trim(), expected?.Trim(), StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 根据机器码生成激活码
        /// </summary>
        public static string GenerateCode(string machineCode)
        {
            var raw = machineCode + "@QRCH#2025";
            using (var sha = SHA256.Create())
            {
                var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(raw));
                return BitConverter.ToString(hash).Replace("-", "").Substring(0, 20).ToUpper();
            }
        }

        /// <summary>
        /// 保存激活状态
        /// </summary>
        public static void SaveActivation(string code)
        {
            System.IO.File.WriteAllText(licenseFile, code ?? "");
            IsActivated = true;
        }

        /// <summary>
        /// 加载激活状态
        /// </summary>
        public static void LoadActivation()
        {
            try
            {
                if (System.IO.File.Exists(licenseFile))
                {
                    var code = System.IO.File.ReadAllText(licenseFile);
                    IsActivated = Verify(code);
                }
                else
                {
                    IsActivated = false;
                }
            }
            catch
            {
                IsActivated = false;
            }
        }
    }
}
