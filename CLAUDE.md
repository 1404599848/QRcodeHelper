# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**QRcodeHelper (3GCR)** — 与基恩士 SR-X100 扫码枪通过以太网 TCP/IP 通信的 WinForms (.NET Framework 4.6.1) 扫码数据管理工具。接收扫描到的二维码数据，按质量等级判定是否合格（A/B=通过），存储到本地 SQLite 数据库，并支持查询与 Excel 导出。

## Build & Run

- **Solution**: `QRcodeHelper.sln` — Visual Studio 2017 solution, 单项目
- **Project**: `QRcodeHelper/QRcodeHelper.csproj` — .NET Framework 4.6.1 WinForms
- **Target**: x86 (Debug/Release); Assembly output = `3GCR.exe`
- **NuGet 还原**: `nuget restore QRcodeHelper.sln` 或 Visual Studio 直接编译
- **编译**: `msbuild QRcodeHelper.sln /p:Configuration=Debug /p:Platform=x86`
- **运行**: `QRcodeHelper/bin/x86/Debug/3GCR.exe`
- **无测试项目**

## 技术栈

| 领域 | 技术 | 用途 |
|---|---|---|
| 扫码枪通信 | Keyence.AutoID.SDK (本地 DLL) | ReaderAccessor + ReaderSearcher，TCP/IP 通信 |
| 数据库 | SQLite (Microsoft.Data.Sqlite 2.2.6 + Dapper 2.0.123) | 扫码记录存储与查询 |
| Excel 导出 | NPOI 2.5.6 | .xls 文件导出 |
| 日志 | NLog 5.1.1 | 文件 + 控制台日志 |
| 串口通信 | Communication.dll (本地 DLL) | 已引用但未使用 |
| 其他 | VncClientControlCommon.dll / VncClientControlCommonLib.dll | VNC 控件（已引用） |

本地 DLL 均位于 `QRcodeHelper/dll/`。

## 项目结构

```
QRcodeHelper/
├── Program.cs                          # 入口 — 单例互斥锁 + 全局异常处理
├── MainForm.cs                         # 主界面 — 扫码枪连接/触发/断开、数据接收入库、查询导出
├── MainForm.Designer.cs                # WinForms 设计器布局
├── InfoFrm.cs                          # 不合格品弹窗（30秒倒计时自动关闭）
├── InfoFrm.Designer.cs
├── Enums.cs                            # IsPassed 枚举（通过=0, 未通过=1）
├── XmlHelper.cs                        # App.config 运行时读写
├── NLogHelper.cs                       # NLog 静态封装（Debug/Info/Warn/Error/Fatal）
├── NLog.config                         # 日志配置：console, debugger, error_file, info 文件
├── ExcelFileGenerator.cs               # 泛型 NPOI Excel 导出（Dictionary 属性名→列名映射）
├── App.config                          # 仅保存 LastIndex（上次选择的网卡索引）
├── packages.config                     # NuGet 依赖列表
├── Models/
│   └── QRCodeRecord.cs                 # 数据模型 — QRCode, Level, CreationTime, 计算属性 IsPassed
├── Services/
│   └── DatabaseService.cs              # SQLite 自动建库建表 + 连接工厂
├── Properties/                         # Assembly info, settings, resources
├── dll/                                # 本地非托管 DLL
└── bin/x86/Debug/                      # 编译输出、日志、SQLite 数据库
```

## 扫码枪硬件配置

- **型号**: 基恩士 SR-X100（无屏幕、无按钮、体积小）
- **IP**: `192.168.100.100`，电脑 IP: `192.168.100.200`
- **通信方式**: 以太网直连 TCP/IP，需设置静态 IP
- **数据格式约定**: `二维码内容:等级`（如 `PROD-001:A`）
- **Web 配置界面**: `http://192.168.100.100`（调曝光/增益等参数）
- **首次配 IP**: 需 USB 连接 + AutoID Network Navigator（基恩士官方软件）

## 数据流

```
扫码枪扫描二维码
      ↓
ReaderSearcher 发现设备 → ReaderAccessor TCP/IP 连接
      ↓
ReceivedDataWrite() 解析 "QRCode:Level" 格式
      ↓
QRCodeRecord.IsPassed 计算属性判定（Level A/B = 通过，其他 = 未通过）
      ↓
Dapper INSERT 到 SQLite QRCodeRecords 表
      ↓
通过 → MessageBeep(ok)  |  未通过 → 双蜂鸣 + InfoFrm 弹窗（30s 倒计时）
      ↓
用户可通过查询面板筛选记录 → DataGridView 展示 或 NPOI 导出 .xls
```

## 设备连接流程

1. 选择网卡（cbNics）→ 点击"搜索" → `ReaderSearcher` 发现网络中的基恩士读码器
2. 选择读码器（cbReaders）→ 点击"连接设备" → `ReaderAccessor` TCP/IP 连接 + `LiveviewForm` 实时画面
3. "触发"（发送 `LON`）/ "触发结束"（发送 `LOFF`）

## 窗体生命周期

- 点击关闭按钮 → 隐藏到系统托盘（notifyIcon），并非真正退出
- 托盘右键菜单：「显示主界面」/「退出」
- 单实例运行 → 通过命名 `Mutex` 实现（`QRcodeHelper`）
- SQLite 数据库：首次启动自动在 `./MyDb.sqlite` 创建 `QRCodeRecords` 表
- 日志目录：`./Logs/info/`（Info 级别）、`./Logs/error/`（Error 级别），保留 30 天
- 导出目录：`./Exports/`，文件命名 `二维码记录_yyyyMMddHHmmss.xls`

## 已发现的 Bug / 待修复

1. **搜索状态未恢复**：搜索成功后 `SchBtn` 和 `cbNics` 未恢复可用状态（`SearchListUp` 函数缺少两行恢复代码，仅在搜索结果为空的 else 分支中恢复了）
2. **无网卡闪退**：无可用网卡时 `cbNics.SelectedIndex` 为 -1，点击搜索直接 `IndexOutOfRangeException`（`MainForm_Load` 中未保护判断）
3. **Liveview 画质粗糙**：`LiveviewForm` 硬编码为 `OneQuarter` 缩放 + `ImageQuality=5`，缺乏可配置性
4. **扫码枪参数未调好时日志框显示 "error"**：过曝等问题需进 Web 界面 `http://192.168.100.100` 调曝光/增益

## 代码中值得注意的模式

- `ExcelFileGenerator<T>` — 泛型类，通过反射 + NPOI 导出任意类型列表为 Excel；接受 `Dictionary<string, string>` 映射属性名→中文列名
- SQL 查询中使用字符串插值 `like '%{txtCode.Text}%'` — **SQL 注入风险**，此处的参数未使用 Dapper 参数化（其他查询参数已使用 Dapper 匿名对象参数）
- `IsPassed` 枚举值在 SQL 过滤时**倒置**：通过 = `AND IsPassed = 0`，未通过 = `AND IsPassed = 1`
- `DatabaseService.TestData()` — 生成 100 条测试数据的方法，已注释未调用
- `btnC_Click` — 测试按钮，Level 硬编码为 C（未通过），插入随机测试数据并弹窗

## 可能的扩展方向

- 增加 RS-232 串口通信支持（`Communication.dll` 已存在于项目中但未使用）
- SQLite 数据库加密
- 软件 License 验证机制
- Liveview 画质参数可配置化
