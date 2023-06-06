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

Equipment suppliers should work with their customers to determine which additional GEM capabilities should be implemented for a specific type of equipment. Because the capabilities defined in the GEM standard were specifically developed to meet the factory automation requirements of semiconductor manufacturers, it is anticipated that most device manufacturers will require most of the GEM capabilities that apply to a particular type of equipment. Some device manufacturers may not require all the GEM capabilities due to differences in their factory automation strategies.

> 设备供应商应该和他的客户合作，已确定特定的设备需要哪些额外的GEM功能。由于GEM标准中定义的功能是专门为了满足半导体工厂的自动化要求开发的，预计大多数设备制造商都可以使用大部分GEM功能。由于半导体自动化测量的差异，一些设备制造商可能不需要所有的GEM功能。

### 1.4 Overview

> 概述

The GEM standard is divided into sections as described below.

> GEM标准分为以下几个部分：

Section 1 — Introduction

> 第一章 介绍

This section provides the revision history, scope and intent of the GEM standard. It also provides an overview of the structure of the document and a list of related documents.

> 本章提供了GEM标准的修订历史，范围和目的，还概述了文档的结构

Section 2 — Definitions

> 第二章 定义

This section provides definitions of terms used throughout the document.

> 本章提供了文档中使用的术语的定义

Section 3 — State Models

> 第三章 状态模型

This section describes the conventions used throughout this document to depict state models. It also describes the basic state models that apply to all semiconductor manufacturing equipment and that pertain to more than a single capability. State models describe the behavior of the equipment from a host perspective.

> 本章介绍了文档中状态模型的定义。他还描述了适用于半导体制造设备的基本状态模型，这些模型涉及到多个功能。状态模型从主机的角度描述设备的行为。

Section 4 — Capabilities and Scenarios

> 第四章 能力和场景

This section provides a detailed description of the communications capabilities defined for semiconductor manufacturing equipment. The description of each capability includes the purpose, definitions, requirements, and scenarios that shall be supported.

> 本章详细的描述了半导体设备的通信能力。每种能力的描述包含目的，定义，需求和场景。

Section 5 — Data Definitions

> 第五章  数据定义

This section provides a reference to the Data Item Dictionary and Variable Item Dictionary found in SEMI Standard E5. The first subsection shows those data items from SECS-II which have been restricted in their use (i.e., allowed formats). The second subsection lists variable data items that are available to the host for data collection and shows any restrictions on their SECS-II definitions.

> 本章参考了SEMI E5标准中的数据项字典和变量项字典。第一小节显示了SECS-II在使用方面受到限制的数据项。第二小节列出了主机可用于数据收集的可变数据项，并显示了SECS-II对其定义的限制。

Section 6 — Collection Events

> 第六章 收集事件

This section provides a list of required collection events and their associated data.

> 本章提供了一系列要求的收集事件和相关的数据

Section 7 — SECS Message Subset

> 第七章 SECS消息子集

This section provides a composite list of the SECS-II messages required to implement all capabilities defined in the GEM standard.

> 本节提实现GEM标准中定义所有功能需要的SECS-II消息的列表

Section 8 — GEM Compliance

> GEM合规性

This section describes the fundamental GEM requirements and additional GEM capabilities and provides references to other sections of the standard where detailed requirements are located. This section also defines standard terminology and documentation that can be used by equipment suppliers and device manufacturers to describe compliance with this standard.

> 本节描述了GEM的基本功能和额外功能，并且提供了详细功能所在章节的参考。本节还定义了设备供应商和设备制造商可用于描述符合本标准的术语和为文件。

Section A — Application Notes

> 应用笔记

These sections provide additional explanatory information and examples.

> 本节提供了额外的解释性信息和示例

Section A.1 — Factory Operational Script

> A.1 工厂操作脚本

This section provides an overview of how the required SECS capabilities may be used in the context of a typical factory operation sequence. This section is organized according to the sequence in which actions are typically performed.

> 本节概述了如何在典型工厂操作序列中使用所需的SECS功能。本节根据通常操作的顺序进行解释。

Section A.2 — Equipment Front Panel

> A.2 设备前面板

This section provides guidance in implementing the required front panel buttons, indicators, and switches as defined in this document. A summary of the front panel requirements is provided.

> 本节提供了实现文档中定义功能所需要的面板按钮，指示灯和开关的指南。提供了前面板要求的摘要。

Section A.3 — Examples of Equipment Alarms

> A.3 设备报警的例子

This section provides examples of alarms related to various equipment configurations.

> 本节提供了与各种设备配置相关的报警示例

Section A.4 — Trace Data Collection Example

> A.4 跟踪数据收集示例

This section provides an example of trace initialization by the host and the periodic trace data messages that might be sent by the equipment.

> 本节提供了主机进行跟踪数据，以及设备可能定期发送数据消息的示例

Section A.5 — Harel Notation

> A.5 Harel符号

This section explains David Harel’s “Statechart” notation that is used throughout this document to depict state models.

> 本节解释David Harel的状态图的表示方法，该表示方法用于在文档中描述状态模型

Section A.6 — Example Control Model Application

> A.6 控制模型应用示例

This section provides one example of a host’s interaction with an equipment’s control model.

> 本节提供了主机与设备控制模型交互的一个示例

Section A.7 — Examples of Limits Monitoring

> A.7 极限值监控的示例

This section contains four limits monitoring examples to help clarify the use of limits and to illustrate typical applications.
> 本节包含4个极限检测示例，以帮助理解极限值的使用。

## 2. Definitions

> 第二章 定义

### 2.1 alarm

> 报警

An alarm is related to any abnormal situation on the equipment that may endanger people, equipment, or material being processed. Such abnormal situations are defined by the equipment manufacturer based on physical safety limitations. Equipment activities potentially impacted by the presence of an alarm shall be inhibited.

> 报警与设备的非正常情况有关，这些非正常情况可能导致人，机器或者材料的损伤。类似的异常情况由设备制造商根据物理安全限制定义。受警报影响的设备活动应该停止。

2.1.1 Note that exceeding control limits associated with process tolerance does not constitute an alarm nor do normal equipment events such as the start or completion of processing.

> 请注意，工艺公差相关的超限控制并不构成警报，也不构成正常设备事件，如工艺的开始或完成。

## 2.2 capabilities

> 功能

Capabilities are operations performed by semiconductor manufacturing equipment. These operations are initiated through the communications interface using sequences of SECS-II messages (or scenarios). An example of a capability is the setting and clearing of alarms.

> 功能是由半导体设备执行的操作，这些操作通过使用通信接口发起，这些通信使用的是SECS-II消息。如设置或者清除报警。

## 2.3 collection event

> 收集事件

A collection event is an event (or grouping of related events) on the equipment that is considered to be significant to the host.

> 收集事件是equipment上被认为对host有重要意义的事件

## 2.4  communication failure

> 通信失败

A communication failure is said to occur when an established communications link is broken. Such failures are protocol specific. Refer to the appropriate protocol standard (e.g., SEMI E4 or SEMI E37) for a protocol-specific definition of communication failure.

> 当已建立的通信链路中断时，就会发生通信失败，这部分非本协议的内容，具体参考SEMI E4或者SEMI E37。

## 2.5 communication fault

> 通信错误

A communication fault occurs when the equipment does not receive an expected message, or when either a transaction timer or a conversation timer expires.

> 当设备没有接受到预期的消息，或者事务/会话计时器超时时，就会发生通信错误

## 2.6 control

> 控制

To control is to exercise directing influence.

> 控制是运动的的直接影响

## 2.7 equipment model

> 设备模型

An equipment model is a definition based on capabilities, scenarios, and SECS-II messages that manufacturing equipment should perform to support an automated manufacturing environment. (See also Generic Equipment Model.)

> 设备模型是基于设备功能，场景和SECS-II消息的定义，制造设备应执行这些信息以支持自动化制造环境。

## 2.8 event

> 事件

An event is a detectable occurrence significant to the equipment.

> 事件指的是对设备有意义且可以检测到的事件

## 2.9  GEM compliance

> 符合GEM标准

The term “GEM Compliance” is defined with respect to individual GEM capabilities to indicate adherence to the GEM standard for a specific capability. Section 8 includes more detail on GEM Compliance.

> 术语"GEM Compliance"是针对个别GEM功能定义的，以表明这些功能符合GEM标准。

## 2.10 Generic Equipment Model

> 通用设备模型

The Generic Equipment Model is used as a reference model for any type of equipment. It contains functionality that can apply to most equipment, but does not address unique requirements of specific equipment.

> 通用设备模型是任何类型设备的参考模型，包含适用于绝大多数设备的功能，但不涉及特定设备的特殊要求。

## 2.11 host

> 主机

The SEMI E4 and E5 standards define Host as “the intelligent system that communicates with the equipment.”

> SEMI E4和E5标准将主机定义为与设备通信的智能系统

## 2.12  message fault

> 消息错误

A message fault occurs when the equipment receives a message that it cannot process because of a defect in the message.

> 当设备收到的消息由于信息的缺陷而无法处理时，就会发生信息故障

## 2.13 operational script

> 操作脚本

An operational script is a collection of scenarios arranged in a sequence typical of actual factory operations. Example sequences are system initialization powerup, machine setup, and processing.

> 操作脚本是一个场景的集合，按照实际工厂操作的典型顺序排列。例如：系统初始化开机 ，机器设置和加工等序列。

## 2.14  operator

> 操作员

A human who operates the equipment to perform its intended function (e.g., processing). The operator typically interacts with the equipment via the equipment supplied operator console.

> 操作设备以执行其预定功能的人。操作员通常通过设备提供的操作台与设备进行互动。

## 2.15 process unit

> 加工单元

A process unit refers to the material that is typically processed as a unit via single run command, process program, etc. Common process units are wafers, cassettes, magazines, and boats.

> 加工单元是指通常通过单次运行命令，加工程序等作为一个单元进行加工的材料。常见的加工单元有晶圆，料带，料盒等。

## 2.16 processing cycle

> 加工周期

A processing cycle is a sequence where in all of the material contained in a typical process unit is processed. This is often used as a measure of action or time.

> 一个加工周期是一个序列，在这个序列中，一个加工单元中包含的所有材料都被加工。这通常被用于作为CT的标准。

## 2.17 scenario

> 场景

A scenario is a group of SECS-II messages arranged in a sequence to perform a capability. Other information may also be included in a scenario for clarity.

> 场景是一组按顺序排列的SECS-II信息，用于执行某一个功能。

## 2.18 SECS-I

> SECS-I

SEMI Equipment Communications Standard 1 (SEMI E4). This standard specifies a method for a message transfer protocol with electrical signal levels based upon EIA RS232-C.

> SECS-I（SEMI E4）规定了一种基于RS232串口的信息传输协议。

## 2.19 SECS-II

> SECS-II

SEMI Equipment Communications Standard 2 (SEMI E5). This standard specifies a group of messages and the respective syntax and semantics for those messages relating to semiconductor manufacturing equipment control.

> SECS-II（SEMI E5）规定了与半导体设备控制有关消息的语法和语义。

## 2.20  SMS

> SMS

SECS Message Service. An alternative to SECS-I to be used when sending SECS-II formatted messages over a network.

> SECS消息服务。在网络上发送SECS-II格式的信息，是SECS-I的替代方案。

## 2.21 state model

> 状态模型

A State Model is a collection of states and state transitions that combine to describe the behavior of a system. This model includes definition of the conditions that delineate a state, the actions/reactions possible within a state, the events that trigger transitions to other states, and the process of transitioning between states.

> 状态模型是一个状态和状态转换的集合，结合起来描述一个系统的行为。这个模型包括对进入一个状态条件的定义，在一个状态下可能的操作，触发向其他状态过渡的事件，以及状态间的转换过程。

## 2.22 system default

> 系统默认

Refers to state(s) in the equipment behavioral model that are expected to be active at the end of system initialization. It also refers to the value(s) that specified equipment variables are expected to contain at the end of system initialization.

> 指设备行为模型中预计在系统初始化结束时的状态。这也是在系统初始化结束时，指定的设备变量应包含的值。

## 2.23 system initialization

> 系统初始化

The process that an equipment performs at power-up, system activation, and/or system reset. This process is expected to prepare the equipment to operate properly and according to the equipment behavioral models.

> 设备在开机，系统复位时执行的过程。这个过程预计会使设备做好准备，按照设备预定的操作正常运行。

## 2.24 user

> 使用者

A human or humans who represent the factory and enforce the factory operation model. A user is considered to be responsible for many setup and configuration activities that cause the equipment to best conform to factory operations practices.

> 工厂里执行工厂事务的人。用户负责设备的设置和调试，使设备符合工厂运营管理。

## 3. State Models
