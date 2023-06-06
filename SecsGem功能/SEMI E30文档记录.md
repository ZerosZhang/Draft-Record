# SEMI E30-1000 Gem

GENERIC MODEL FOR COMMUNICATIONS AND CONTROL OF MANUFACTURING EQUIPMENT (GEM)
> 制造设备通信与控制通用模型

## 1. Introduction

> 介绍

### 1.1 Revision History

> 修订历史

This is the first release of the GEM standard.

> 这是GEM标准发行的第一版

### 1.2 Scope

> 范围

The scope of the GEM standard is limited to defining the behavior of semiconductor equipment as viewed through a communications link. The SEMI E5 (SECS-II) standard provides the definition of messages and related data items exchanged between host and equipment. The GEM standard defines which SECS-II messages should be used, in what situations, and what the resulting activity should be. Figure 1.1 illustrates the relationship of GEM, SECS-II and other communications alternatives.

> GEM标准的范围仅限于定义半导体设备之间的通信行为。SEMI E5 SECS-II标准提供了host和equipment之间消息交换和相关数据项的定义。GEM标准解释应该使用那些SECS-II消息，在什么情况下使用，以及应该使用触发哪些活动。

The GEM standard does NOT attempt to define the behavior of the host computer in the communications link. The host computer may initiate any GEM message scenario at any time and the equipment shall respond as described in the GEM standard. When a GEM message scenario is initiated by either the host or equipment, the equipment shall behave in the manner described in the GEM standard when the host uses the appropriate GEM messages.

> GEM标准并不定义在链路通信中host的行为。host可以随时发起GEM消息，equipment必须按照GEM标准进行响应。

The capabilities described in this standard are specifically designed to be independent of lower-level communications protocols and connection schemes (e.g., SECS-I, SMS, point-to-point, connection-oriented or connectionless). Use of those types of standards is not required or precluded by this standard.

> 本标准中描述的功能独立于低版本的通信协议和连接方案（如SECS-I，SMS，点对点连接等）。本标准不使用这些标准。

This standard does not purport to address safety issues, if any, associated with its use.  It is the responsibility of the users of this standard to establish appropriate safety and health practices and determine the applicability of regulatory limitations prior to use.

> 本标准并不旨在解决使用过程中相关的安全问题。本标准的使用者应当在使用之前制定适当的安全规范。

### 1.3 Intent

> 目的

GEM defines a standard implementation of SECS-II for all semiconductor manufacturing equipment. The GEM standard defines a common set of equipment behavior and communications capabilities that provide the functionality and flexibility to support the manufacturing automation programs of semiconductor device manufacturers. Equipment suppliers may provide additional SECS-II functionality not included in GEM as long as the additional functionality does not conflict with any of the behavior or capabilities defined in GEM. Such additions may include SECS-II messages, collection events, alarms, remote command codes, processing states, variable data items (data values, status values or equipment constants), or other functionality that is unique to a class (etchers, steppers, etc.) or specific instance of equipment.

> GEM为所有半导体设备定义了SECS-II的使用标准。GEM标准定义了一组通用的设备行为和通信能力，提供了支持半导体制造商的自动化程序的功能和灵活性。设备供应商可以提供GEM中不包含的额外SECS-II功能，只要该功能不与GEM中定义的功能冲突。

GEM is intended to produce economic benefits for both device manufacturers and equipment suppliers. Equipment suppliers benefit from the ability to develop and market a single SECS-II interface that satisfies most customers. Device manufacturers benefit from the increased functionality and standardization of the SECS-II interface across all manufacturing equipment. This standardization reduces the cost of software development for both equipment suppliers and device manufacturers. By reducing costs and increasing functionality, device manufacturers can automate semiconductor factories more quickly and effectively. The flexibility provided by the GEM standard also enables device manufacturers to implement unique automation solutions within a common industry framework.

> GEM协议旨在为设备制造商和设备供应商带来经济效益。设备供应商使用该标准可以满足大多数客户要求的SECS-II接口的能力。设备制造商可以针对设备的SECS-II接口提供功能增强和标准化。这种标准降低了设备供应商和设备制造商的软件开发成本。通过降低成本和增加功能，半导体工厂可以更快更有效的实现半导体工厂的自动化。GEM标准提供的灵活性还使得设备制造商能够在行业的通用框架下实现独有的自动化解决方案。

The GEM standard is intended to specify the following:

> GEM标准旨在规范以下内容

- A model of the behavior to be exhibited by semiconductor manufacturing equipment in a SECS-II communication environment
  > 半导体制造设备在SECS-II通信环境中的行为模型
- A description of information and control functions needed in a semiconductor manufacturing environment,
  > 半导体制造环境中所需的信息和功能的描述
- A definition of the basic SECS-II communications capabilities of semiconductor manufacturing equipment,
  > 半导体制造设备基于SECS-II通信能力的定义
- A single consistent means of accomplishing an action when SECS-II provides multiple possible methods, and
  > 当SECS-II提供了多种可能的方法时，完成某一行为的同一方式
- Standard message dialogues necessary to achieve useful communications capabilities.
  > 实现常用通信能力必须的标准消息会话

The GEM standard contains two types of requirements:

> GEM标准包含两类要求：

- fundamental GEM requirements and
  > GEM的基本功能
- requirements of additional GEM capabilities.
  > GEM的额外功能

The fundamental GEM requirements form the foundation of the GEM standard. The additional GEM capabilities provide functionality required for some types of factory automation or functionality applicable to specific types of equipment. A detailed list of the fundamental GEM requirements and additional GEM capabilities can be found in Chapter 8, GEM Compliance. Figure 1.2 illustrates the components of the GEM standard.

> GEM的基本功能是GEM标准的基础。GEM的额外功能提供了一些半导体工厂所需要的功能或者特定类型的设备的功能。GEM的基本功能和额外功能可以参考第八章。
