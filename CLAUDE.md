# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**QRcodeHelper (3GCR)** — 与基恩士 SR-X100 扫码枪通过以太网 TCP/IP 通信的 WinForms (.NET Framework 4.6.1) 扫码数据管理工具。接收扫描到的二维码数据，按质量等级判定是否合格（A/B=通过），存储到本地 SQLite 数据库，并支持查询、漏码/重码检测、Excel 导出、三色灯控制。

## Build & Run

- **Solution**: `QRcodeHelper.sln` — Visual Studio 2017 solution, 单项目
- **Project**: `QRcodeHelper/QRcodeHelper.csproj` — .NET Framework 4.6.1 WinForms
- **Target**: x86 (Debug/Release); Assembly output = `3GCR.exe`
- **编译**: `msbuild QRcodeHelper.sln /p:Configuration=Debug /p:Platform=x86`
- **运行**: `QRcodeHelper/bin/x86/Debug/3GCR.exe`
- **NuGet 还原**: 自动在 VS 中完成；命令行需 `nuget restore QRcodeHelper.sln`
- **无测试项目**

## 项目结构

```
QRcodeHelper/
├── Program.cs                          # 入口 — 单例互斥锁 + 全局异常处理
├── MainForm.cs                         # 主界面 — 扫码枪/三色灯/漏码重码检测/查询导出
├── MainForm.Designer.cs                # WinForms 设计器布局
├── ConfigFrm.cs / .Designer.cs         # 配置窗口（串口/报警参数/密码）
├── InfoFrm.cs / .Designer.cs           # 不合格品弹窗（30秒倒计时自动关闭）
├── Enums.cs                            # IsPassed + AlertType 枚举
├── ExcelFileGenerator.cs               # 泛型 NPOI Excel 导出
├── XmlHelper.cs / NLogHelper.cs        # 辅助工具
├── App.config / packages.config / NLog.config
├── Models/
│   └── QRCodeRecord.cs                 # 数据模型（含 AlertType/AlertTypeDisplay）
├── Services/
│   ├── DatabaseService.cs              # SQLite 建库/迁移/测试数据
│   ├── AppConfig.cs                    # XML 配置读写（串口/报警参数/密码）
│   └── Activation.cs                   # 激活码生成/校验（默认禁用）
├── Properties/ / dll/ / bin/
```

## 数据库

- SQLite 文件: `./MyDb.sqlite`
- 表 `QRCodeRecords` 字段: Id, QRCode, Level, CreationTime, IsPassed, AlertType
- AlertType: 0=正常, 1=重码, 2=漏码, 3=跳号
- 首次启动自动建表；`MigrateDB()` 为旧库添加 AlertType 列

## 二维码数据格式

```
1006200XEN38CGCOJ250917A0001:A
└──────────┬──────────┘└─┬┘
      QRCode本体        等级 (A/B=通过)
```

- 流号固定**最后 4 位**（0001−9999）
- 层级识别流号 = 截前N-4字符作为批次键 + 后4位作为流号
- 漏码检测：同一批次键下，表中最新的流号 +1 < 当前流号
- 重码检测：同一批次键下，表中最新的流号 == 当前流号

## 新功能（开发中）

| 功能 | 状态 | 说明 |
|---|---|---|
| 漏码/重码检测 | ✅ 完成 | `ReceivedDataWrite` 中实时检测 |
| AlertType 字段 | ✅ 完成 | 库+过滤+导出全链路 |
| 测试数据 | ✅ 完成 | 点[测试]按钮生成带漏码/重码/跳号的数据 |
| 跳号按钮 | ✅ 完成 | 插入 AlertType=3 的记录，Level="-" |
| 消警按钮 | ✅ 完成 | 关闭当前报警状态 |
| 配置窗口 | ✅ 完成 | 串口/报警参数/密码，需密码进入（默认1234） |
| 激活码功能 | ✅ 完成 | `Activation.cs`，默认禁用 |
| 三色灯硬件控制 | ⏳ 待做 | 等 LCUS-4 模块到货，`RelayController.cs` 未创建 |
| 蜂鸣器控制 | ⏳ 待做 | 跟随三色灯 |
| 软件闪烁 | ⏳ 待做 | 基于 `AppConfig.FlashIntervalMs` |

## 三色灯方案（待实施）

- **硬件**: LCUS-4 USB 继电器模块（CH340 芯片，虚拟 COM 口）
- **IO分配**: CH1=红灯(漏码), CH2=黄灯(重码), CH3=绿灯(正常), CH4=蜂鸣器
- **电源**: USB 5V 供模块；外接 24V 供三色灯（通过继电器触点）
- **接线**: 
  - 三色灯灰色公共线 → 24V+
  - 继电器 COM1~COM4 → 全部短接→24V-
  - 红/黄/绿/蜂鸣线 → CH1~CH4 NO 端子
- **通信协议**: 9600,8N1；开 CH1: `A0 01 01 A2`；关 CH1: `A0 01 00 A1`
- **待创建**: `Services/RelayController.cs`（SerialPort 封装 + 闪灯定时器）

## 扫码枪硬件配置

- **型号**: 基恩士 SR-X100（无屏幕/按钮）
- **IP**: `192.168.100.100`，电脑: `192.168.100.200`（静态）
- **Web 配置**: `http://192.168.100.100`（调曝光/增益）
- **首次配 IP**: 需 USB + AutoID Network Navigator

## 已修复的 Bug

1. ✅ **搜索状态未恢复**：`SearchListUp` 成功分支补了 `cbNics.Enabled = true; SchBtn.Enabled = true;`
2. ✅ **无网卡闪退**：`MainForm_Load` 中加 `m_nicList.Count > 0` 保护，无网卡时禁用搜索

## 代码中值得注意的模式

- `ExcelFileGenerator<T>` — 反射 + NPOI 导出；`Dictionary<string,string>` 映射属性名→列名
- SQL 查询中仍有字符串插值 `like '%{txtCode.Text}%'` — SQL 注入风险未修
- `IsPassed` 是**只读计算属性**，由 Level 决定（A/B=通过）
- `AlertTypeDisplay` 是**只读计算属性**，由 AlertType(int) 决定显示文本
- `btnC_Click` — 测试按钮，现在调用 `DatabaseService.TestData()` 生成数据
- `AppConfig.Load()` 在 `MainForm_Load` 中调用
- 激活码功能默认关闭（`Activation.Enabled = false`）
