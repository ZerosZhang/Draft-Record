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

GEM defines a standard implementation of SECS-II for all semiconductor manufacturing equipment. The GEM standard defines a common set of equipment behavior and communications capabilities that provide the functionality and flexibility to support the manufacturing automation programs of semiconductor device manufacturers. Equipment suppliers may provide additional SECS-II functionality not included in GEM as long as the additional functionality does not conflict with any of the behavior or capabilities defined in GEM. Such additions may include SECS-II messages, collection events, alarms, remote command codes, processing states, variable data items (data values, status values or equipment constants), or other functionality that is unique to a class (etchers, steppers, etc.) or specific instance of equipment.

GEM is intended to produce economic benefits for both device manufacturers and equipment suppliers. Equipment suppliers benefit from the ability to develop and market a single SECS-II interface that satisfies most customers. Device manufacturers benefit from the increased functionality and standardization of the SECS-II interface across all manufacturing equipment. This standardization reduces the cost of software development for both equipment suppliers and device manufacturers. By reducing costs and increasing functionality, device manufacturers can automate semiconductor factories more quickly and effectively. The flexibility provided by the GEM standard also enables device manufacturers to implement unique automation solutions within a common industry framework.

The GEM standard is intended to specify the following:

- A model of the behavior to be exhibited by semiconductor manufacturing equipment in a SECS-II communication environment,
- A description of information and control functions needed in a semiconductor manufacturing environment,
- A definition of the basic SECS-II communications capabilities of semiconductor manufacturing equipment,
- A single consistent means of accomplishing an action when SECS-II provides multiple possible methods, and
- Standard message dialogues necessary to achieve useful communications capabilities.

The GEM standard contains two types of requirements:

- fundamental GEM requirements and
- requirements of additional GEM capabilities.

The fundamental GEM requirements form the foundation of the GEM standard. The additional GEM capabilities provide functionality required for some types of factory automation or functionality applicable to specific types of equipment. A detailed list of the fundamental GEM requirements and additional GEM capabilities can be found in Chapter 8, GEM Compliance. Figure 1.2 illustrates the components of the GEM standard.
