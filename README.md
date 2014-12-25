KCV.ManualCounter
====================
KanColleViewer手动计数插件

使用方法
-------
将KCV.ManualCounter.dll放入KanColleViewer的Plugin目录中

截图
----
![image1](https://raw.githubusercontent.com/Gizeta/KCV.ManualCounter/master/ScreenShots/screenshot1.png)

配置文件
-------
配置文件保存在`%AppData%\grabacr.net\KanColleViewer\KCV.ManualCounter.xml`中，请谨慎修改。

配置文件内容如下：

> * PluginSettings - XML根目录
>   * Counters - 计数器集合
>     * Counter - 计数器实例
>       * Content - 计数器标题
>       * ResetFrequency - 重置频率。可取None, Day, Week, Month, Year值。
>       * CurrentValue - 当前计数值
>       * TotalValue - 计数上限值。0为无限大。
>       * StepValue - 计数步长。**暂时无效**
>       * ResetDate - 上次重置时间。此值自动计算，请勿修改。
>       * ResetAlongWithQuests - 是否随任务刷新时间重置
>       * StepText - 计数按钮显示文本
>       * Script - IronRuby脚本。
>   * CounterWidth - 计数器显示宽度（整数）
>   * CounterHeight - 计数器显示高度（整数）
>   * SettingsVersion - 配置文件版本。仅供程序升级时使用，请勿修改。

IronRuby脚本示例：
```ruby
# 统计补给次数
using_clr_extensions System
using_clr_extensions System::Reactive::Linq

Grabacr07::KanColleWrapper::KanColleProxyExtensions.try_parse(kancolle_proxy.api_req_hokyu_charge).where(lambda { |x| x.is_success }).subscribe(lambda { |s| counter.increase() })
```

使用库
-----
* [KanColleViewer](http://grabacr.net/kancolleviewer) - 提督業も忙しい！ 开源协议文件见 KCV-LICENSE.txt
* [Livet](https://github.com/ugaya40/Livet) - WPF MVVM Infrastructure
* [MetroRadiance](https://github.com/Grabacr07/MetroRadiance) - MetroLikeWindow
* [IronRuby](http://ironruby.net/) - Ruby programming language for the .NET Framework

协议
---
使用MIT开源协议发布。
