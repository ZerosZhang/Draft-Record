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

The following sections contain state models for semiconductor manufacturing equipment. These state models describe the behavior of the equipment from a host perspective in a compact and easy to understand format. State models for different equipment will be identical in some areas (e.g., communications), but may vary in other areas (e.g., processing). It is desirable to divide the equipment into parallel components that can be modeled separately and then combined. An example of a component overview of an equipment is provided as Figure 3.0.

> 以下各节包含半导体设备的状态模型。这些状态模型从主机的角度描述设备的行为。不同设备的状态模型在某些方面（如通信）是相同的，在某些方面（如处理）是不同的。将设备划分为并联的部件，这些部件可以单独建模，然后组合。

Equipment manufacturers must document the operational behavior of their equipment using state model methodology. State models are discussed in Sections 3.1 and A.5 and in a referenced article. Documentation of a state model shall include the following three elements:

> 设备制造商必须用状态模型方法记录其设备的操作行为。状态模型具体参考3.1节和A.5节。状态模型的文档应包含以下三个要素：

- A state diagram showing the possible states of the system or components of a system and all of the possible transitions from one state to another. The states and transitions must each be labeled. Use of the Harel notation (see A.5) is recommended.
  > 状态图显示了系统状态或者系统中组件的状态，和他们从一种状态到另一种状态所有可能的转换。状态和转换必须分别标记，建议使用Harel符号（参考A.5）
- A transition table listing each transition, the beginning and end states, what stimulus triggers the transition, and any actions taken as a result of the transition.
  > 一个转换表，用于列出每个转换开始和结束的状态，触发转换的条件，以及转换后的行为。
- A definition of each state specifying system behavior when that state is active.
  > 每个状态的定义，指定该状态激活时的系统行为

Examples of the above elements are provided in Section A.5.

> 上述举例参考A.5

The benefits of providing state models are:

> 提供状态模型的好处在于

1. State machine models are a useful specification tool,

   > 状态机是一种有用的规范工具

2. A host system can anticipate machine behavior based upon the state model,

   > host可以根据状态模型预测equipment的行为

3. End-users and equipment programmers have a common description of machine behavior from which to work,

   > 终端用户和设备程序员对设备的行为有一个共同的描述

4. “Legal” operations can be defined pertaining to any machine state,

   > 合法操作可以被定义为属于任何状态

5. External event notifications can be related to internal state transitions,

   > 外部事件通知可以与内部状态转换相关

6. External commands can be related to state transitions,

   > 外部命令可以与状态转换相关

7. State model components describing different aspects of machine control can be related to one another (example: processing state model with material transport state model; processing state model with internal machine safety systems).

   > 描述设备控制不同方面的状态模型组件可以相互关联。

### 3.1 State Model Methodology、

> 状态模型方法

To document the expected functionality of the various capabilities described in this document, the “Statechart” notation developed by David Harel has been adopted. An article by Harel is listed in Section 1.5 and should be considered “must” reading for a full understanding of the notation. The convention used in this and following sections is to describe the dynamic functionality of a capability with three items: a textual description of each state or substate defined, a table that describes the possible transitions from one state to another, and a graphical figure that uses the symbols defined by Harel to illustrate the relationships of the states and transitions. The combination of these items define the state model for a system or component. A summary of the Harel notation and a more detailed description of the text, table, and figure used to define behavior with this methodology is contained in the Application Note A.5.

> 为了记录本文中描述的各种功能的预期功能，我们采用了David Harel开发的StateChart标记法。本章和后面的章节使用的习惯是用以下三点来描述功能：
>
> - 定义每个状态或者子状态的文本描述
> - 描述从一个状态到另一个状态可能转换的表格
> - 用Harel符号来说明状态和转换关系的图形
>
> 这些项的组合定义了系统或组件的状态模型。

The basic unit of a state model is the state. A state is a static set of conditions. If the conditions are met, the state is current. These conditions might involve sensor readings, switch positions, time of day, etc. Also part of a state definition is a description of reactions to specific stimuli (e.g., if message Sx,Fy is received, generate reply message Sx,Fy + 1). Stimuli may be quite varied but for semiconductor equipment would include received SECS messages, expired timers, operator input at an equipment terminal, and changes in sensor readings.

> 状态模型的基本单位是状态。状态是一组静态的条件，如果满足这些条件，则进入该状态。这些状态可能涉及到传感器的读数，开关的状态，一天的时间等。状态定义的另一部分是对特定事件的反应（比如收到信息SxFy，回复信息SxFy+1）。这些事件多种多样，半导体设备主要包含收到的SECS信息，定时器，操作员在设备终端的输入，以及传感器读数的变化。

To help clarify the interpretation of this document and the state models described herein, it is useful to distin- guish between a state and an event and the relationship of one to the other. An event is dynamic rather than static. It represents a change in conditions, or more specifically, the awareness of such a change. An event might involve a sensor reading exceeding a limit, a switch changing position, or a time limit exceeded.

> 为了帮助解释本文档描述的状态模型，有必要区分状态、事件和他们之间的关系。事件是动态的而不是静态的，他代表着条件的变化，或者更具体的说，是检测到这些变化。事件可能涉及到传感器读数超过阈值，开关位置改变或者超时。

A change to a new active state (state transition) must always be prompted by a change in conditions, and thus an event. In addition, a state transition may itself be termed an event. In fact, there are many events that may occur on an equipment, so it is important to classify events based on whether they can be detected and whether they are of interest. In this document, the term event has been more narrowly defined as a detectable occurrence that is significant to the equipment.

> 状态转换必须由事件触发。此外状态转换本身也可以称之为事件。事实上，设备上可能发生许多事件，因此根据是否可以检测到事件以及对事件的分类很重要。在本文中，事件被定义为对设备有意义且可以检测到的事件。

A further narrowing of the definition of event is repre- sented by the term “collection event,” which is an event (or group of related events) on the equipment that is considered significant to the host. It is these events that (if enabled) are reported to the host. By this definition, the list of collection events for an equipment would typ- ically be only a subset of total events. The state models in this document are intended to be limited to the level of detail in which the host is interested. Thus, all state transitions defined in this standard, unless otherwise specified, shall correspond to collection events.
> 事件的定义进一步缩小为术语`Collection Event`。`Collection Event`是在设备上被认为对主机有意义的事件，这些事件可以被报告给主机。本文档中的状态模型被限制在对主机有意义的情况下，除非另有声明，否则本标准中所有的状态转换都对应于`Collection Event`

### 3.2 Communications State Model

> 通讯状态模型

The Communications State Model defines the behavior of the equipment in relation to the existence or absence of a communications link with the host. Section 4.1 expands on this section by defining the Establish Communications capability. This model pertains to a logical connection between equipment and host rather than a physical connection.

> 通信状态模型定义了和主机之间是否存在通讯链路的设备行为。4.1节通过定义建立通讯能力对本节内容进行了扩展。这个模型适用于设备和主机之间的逻辑连接，而非物理连接。

3.2.1 Terminology

> 术语

The terms communication failure, connection transaction failure, and communication link are defined for use within this document only and should not be confused with the same or similar terms used elsewhere.

> 术语`communication failure`,`connection transaction failure`,`communication link`的定义只在本文内有效，不要和其他地方使用的类似的术语混淆。

- See SEMI E4 (SECS-I) or SEMI E37 (HSMS) for a protocol specific definitions of communications failure.
  > 参考SEMI E4（SECS-I）或者SEMI E37（HSMS）了解通信故障的协议定义。
- A connection transaction failure occurs when attempting to establish communications and is caused by

  - a communication failure,
  - the failure to receive an S1,F14 reply within a reply timeout limit, or
  - receipt of S1,F14 that has been improperly formatted or with COMMACK2  not set to 0.

  > 尝试建立通信时发生连接事务失败，可能的原因如下所示：
  >
  > - 通信故障
  > - 未能在回复超时前收到S1F14回复
  > - 收到格式不正确或者COMMACK2非零的S1F14

- A reply timeout period begins after the successful transmission of a complete primary message for which a reply is expected. (See SEMI E4 (SECS-I) or SEMI E37 (HSMS) for a protocol-specific definition of reply timeout.)
  > 回复超时倒计时从成功传输需要回复的主消息之后开始（具体参考E37 HSMS）
- A communication link is established following the first successful completion of any one S1,F13/F14 transaction with an acknowledgement of “accept”. The establishment of this link is logical rather than physical.
  > 在首次完成S1F13/F14事务并确认接受后，就会建立通讯联系。这种联系的建立是逻辑上的，而不是物理上的。
- Implementations may have mechanisms which allow outgoing messages to be stored temporarily prior to being sent. The noun queue is used to cover such stored messages. They are queued when placed within the queue and are dequeued by removing them from this storage.
  > 用队列来实现允许在发送消息之前临时存储发出消息的机制
- Send includes “queue to send” or “begin the process of attempting to send” a message. It does not imply the successful completion of sending a message.
  > 发送包括"发送队列""开始尝试发送"并非成功完成了发送消息
- The host may attempt to establish communications with equipment at any time due to the initialization of the host or by independent detection of a communications failure by the host. Thus, the host may initiate an S1,F13/F14 transaction at any time.
  > 主机可能随时和设备建立通信事务，因此主机可以在任何时候启动S1F13/F14

3.2.2 CommDelay Timer

> 通讯延时计时器

The CommDelay timer represents an internal timer used to measure the interval between attempts to send S1,F13. The length of this interval is equal to the value in the Establish Communications Timeout. The CommDelay timer is not directly visible to the host.

> 通讯延时计时器是一个内部计时器，用于规定多次发送S1F13之间的间隔，这个时间间隔为EstablishCommunicationsTimeout的值。对主机来说，该超时不可见。

EstablishCommunicationsTimeout is the user-configurable equipment constant that defines the delay, in seconds, between attempts to send S1,F13. This value is used to initialize the CommDelay timer.

> EstablishCommunicationsTimeout是用户可以配置的设备常量（EC），用于定义多次发送S1F13之间的间隔。该值用于初始化CommDelay计时器

The CommDelay timer is initialized to begin timing. The CommDelay timer is initialized only when the state WAIT DELAY is entered.

> 初始化CommDelay计时器后开始计时，只有在进入状态WAITDELAY时，才会初始化CommDelay计时器

The CommDelay timer is expired when it “times out,” and the time remaining in the interval between attempts to send is zero. When the timer expires during the state WAIT DELAY, it triggers a new attempt to send S1,F13 and the transition to the state WAIT CRA3 .

> CommDelay计时器在超时时结束，并且可以再次尝试发送消息。当计时器在WAITDELAY状态下超时时，会尝试发送一次S1F13并且进入WAIT CRA3状态

3.2.3  Conventions

- The attempt to send S1,F13 is made only upon transit into the state WAIT CRA. The CommDelay Timer should be set to “expired” at this time.
  > 发送S1F13的尝试只在进入WAIT CRA状态时才进行，此时要将CommDelay计时器设置为超时。
- The CommDelay timer is initialized only upon transit into the state WAIT DELAY. A next attempt to send S1,F13 shall occur only upon a transit to the state WAIT CRA.
  > 只有在状态进入WAIT DELAY时，才会初始化CommDelay计时器。下次发送S1F13只会发生在WAIT CRA状态的转换过程中。

3.2.4 Communication States

> 通讯模型

There are two major states of SECS communication, DISABLED and ENABLED. The system default state must be user- configurable at the equipment (e.g., via a jumper setting or non-volatile memory variable).

> SECS通信主要有两种状态：禁用和启用。系统的默认状态对于用户应该是可配置的。

Once system initialization has been achieved, the opera tor shall be able to change the communication stat selection at any time via equipment terminal function or momentary switch. A two-position type switch mus not be used due to possible conflict with the syste default.

> 一旦系统初始化完成，操作员可以随时通过设备终端功能或者瞬时开关改变通信状态。由于可能和系统默认设置发生冲突，因此不能使用双位置类型开关。

The ENABLED state has two substates, NOT COMMUNICATING and COMMUNICATING, described below. The equipment must inform the operator of the current communication state via continuous display at the equipment, including the NOT COMMUNICATING and COMMUNICATING sub-states.

> ENABLE有两个状态，NOT COMMUNICATING和COMMUNICATING。设备必须在设备上显示当前的通信状态。

In the event of a connection transaction failure, a user-configurable equipment constant EstablishCommunica- tionsTimeout is used to establish the interval between attempts to send an S1,F13 (Establish Communications Request) while in the NOT COMMUNICATING sub- state.

> 在连接事务失败的情况下，通过CommunicationTimeout来建立NOT COMMUNICATION子状态下发送S1F13的时间间隔。

Figure 3.2.1 shows the relationship between the superstates and substates of the Communications State Model. A description of the events triggering state transitions and the actions taken is given in Table 3.2.

> 图3.2.1显示了通信状态模型。表3.2给出了触发状态转换事件和采取操作的描述

![1686114517058](image/SEMIE30文档记录/1686114517058.png)
Figure 3.2.1
Communications State Diagram

The states of the Communications State Model are defined as follows:

> 通信状态模式被定义如下：

DISABLED
In this state SECS-II communication with a host computer is non-existent. If the operator switches from ENABLED to DISABLED, all SECS-II communications must cease immediately. Any messages queued to send shall be discarded, and all further action on any open transactions and conversations shall be terminated.4 Handling of messages currently being transmitted is an issue for lower level message transfer protocols and is not addressed in this standard. The DISABLED state is a possible system default.

> DISABLE
> 在这种状态下，和主机之间不存在SECS-II通信。如果操作员从启用切换到禁用，所有的SECS-II通信必须立即停止。任何等待发送的消息都会被丢弃，任何打开的事务和会话都会立即中止。处理正在发送的消息不在本标准的范围内。

ENABLED
ENABLED has two substates, COMMUNICA- TING and NOT COMMUNICATING. Whenever communications are enabled, either during system initialization or through operator selection, the substate of NOT COMMUNICATING is active until communications are formally established. Lower-level protocols (such as SECS-I) are assumed to be functioning normally in that they are capable of supporting the communication of SECS- II syntax.
The ENABLED state is a possible system default.
> ENABLE
> ENABLE有两个子状态，COMMUNICATING和NOT COMMUNICATING。无论何时启用通讯，无论是系统初始化期间还是通过操作员的操作，默认激活的都是NOT COMMUNICATING状态，直到通信正式建立。

ENABLED/NOT COMMUNICATING
No messages other than S1,F13, S1,F14, and S9,Fx shall be sent while this substate is active. The equipment shall discard any messages received from the host other than S1,F13 or S1,F14 (Establish Communications Acknowledge). It shall also periodically attempt to establish communication with a host computer by issuing an S1,F13 until communications are successfully established. However, only one equipment-initiated S1,F13 transaction may be open at any time.
> Enable/Not Communicating
> 当这个子状态激活时，除了S1F13，S1F14和S9Fy之外，不能发送任何消息。除了S1F13和S1F14外，设备丢弃接受到的其他消息。设备定期发送S1F13命令与主机进行通信，直到通信成功建立。

The NOT COMMUNICATING state has two AND substates, HOST-INITIATED CONNECT and EQUIPMENT-INITIATED CONNECT, both of which are active whenever the equipment is NOT COMMUNICATING. These two substates clarify the behavior of the equipment in the event that both the equipment and the host attempt to establish communications during the same period of time5 .
> Not Communicating状态包含两个子状态，Host initiated connect和Equipment initiated connect【主机启动连接】和【设备启动连接】。当设备处于Not Communicating状态时，两个子状态都处于激活状态。这两个子状态阐明和主机在同一时间段内试图建立通信时的设备行为。

NOT COMMUNICATING/EQUIPMENT-INITIATED CONNECT
This state has two substates, WAIT CRA and WAIT DELAY. Upon any entry to the NOT COMMUNICATING state, whenever EQUIPMENT-INITIATED CONNECT first becomes active, a transition to WAIT CRA occurs, the CommDelay timer is set to “expired,” and an immediate attempt to send S1,F13 is made.
> Not Communicating/Equipment initiated connect
> Equipment initiated connect包含两个分支，wait cra和wait delay。在Not Communicating状态下，只要Equipment initiated connect第一次激活，就会发生wait cra状态的转换。CommDelay计时器被设置为超时，并立即尝试发送S1F13

NOT COMMUNICATING/EQUIPMENT-INITIATED CONNECT/WAIT CRA
An Establish Communications Request has been sent. The equipment waits for the host to acknowledge the request.
> Not Communicating/Equipment initiated connect/wait cra
> 建立通信请求已经发送，设备等待主机确认请求。

NOT COMMUNICATING/EQUIPMENT-INITIATED CONNECT/WAIT DELAY
A connection transaction failure has occurred. The CommDelay timer has been initialized. The equipment waits for the timer to expire.
> Not Communicating/Equipment initiated connect/wait delay
> 发生了连接事务失败。CommDelay计时器已被初始化，设备等待CommDelay计时器超时。

NOT COMMUNICATING/HOST-INITIATED CONNECT
This state describes the behavior of the equipment in response to a host-initiated S1,F13 while NOT COMMUNICATING is active.
> Not Communicating/Host Initiated Connect
> 该状态描述了在Not Communicating状态下，设备对主机发起的S1F13的响应行为

NOT COMMUNICATING/HOST-INITIATED CONNECT/WAIT CR FROM HOST
The equipment waits for an S1,F13 from the host. If an S1,F13 is received, the equipment attempts to send an S1,F14 with COMMACK = 0.
> Not Communicating/Host initiated connect/wait cr from host
> 设备等待来自主机的S1F13。如果收到了S1F13，设备发送CommAck=0的S1F14

ENABLED/COMMUNICATING
Communications have been established. The equipment may receive any message from the host, including S1,F13. When the equipment is COMMUNICATING, SECS communications with a host computer must be maintained. This state remains active until communications are disabled or a communication failure occurs. If the equipment receives S1,F13 from the host while in the COMMUNICATING substate, it should respond with S1,F14 with COMMACK set to zero. If the equipment receives S1,F14 from a previously sent S1,F13, no action is required.
> Enable/Communicating
> 通信已经建立。设备可以接受来自主机的任何信息，包括S1F13。当设备处于Communicating状态时，必须保持与主机的SECS通信。该状态一直处于激活状态，直到通信被禁止或者发生通信故障。如果设备在Communicating状态下收到了来自主机的S1F13，应该用CommAck=0的S1F14消息响应。如果设备从之前发送的S1F13中收到了S1F14，则不需要任何动作。

In the event of communication failure, the equipment shall return to the NOT COMMUNICATING substate and attempt to re- establish communications with the host.
> 在communication failure的情况下，设备应返回到Not Communicating子状态，并尝试与主机重新建立通信

It is possible that the equipment may be waiting for an S1,F14 from the host in EQUIPMENT- INITIATED CONNECT/WAIT CRA at the time an S1,F13 is received from the host in HOST- INITIATED CONNECT/WAIT CR FROM HOST. When this situation occurs, both equipment and host have an open S1,F13/S1,F14 transaction. Since communications are successfully established on the successful completion of any S1,F13/S1,F14 transaction, either of these two transactions may be the first to complete successfully and to cause the transition from NOT COMMUNICATING state to COMMUNICATING. In this event, the other transaction shall remain open regardless of the transition to COMMUNICATING until it is closed in a normal manner.
> 设备在Equipment initiated connect/wait cra状态下等待主机的S1F14消息时，有可能在Host initiated connect/wait cr from host状态下收到来自主机的S1F13。在这种情况发生时，设备和主机都会存在一个打开的S1F13/S1F14事务。由于通信是在任何S1F13/S1F14事务成功完成后建立的，这两个事务中的任何一个都可能第一个成功且使状态从Not Communicating过渡到Communicating。在这种情况下，另一个事务保持开放，直到收到S1F14后关闭事务，此时无状态转换。

If the equipment has not yet sent6 an S1,F14 to a previously received S1,F13 at the time when COMMUNICATING becomes active, the S1,F14 response shall be sent in a normal manner. A failure to send the S1,F14 is then treated as any other communication failure.
> 如果在Communicating激活时，设备没有对之前的收到的S1F13响应S1F14，则应以正常方式发送S1F14响应。没有成功发送S1F14的情况会触发communication failure。

If the equipment-initiated S1,F13/S1,F14 is still open when the transition to COMMUNICATING occurs,  subsequent  failure to receive a reply from the host is considered a communication fault by equipment. An S9,F9 should be sent when a transaction timer timeout occurs7. (See Section 4.9 for definitions of communication faults and message faults, as well as detail on Stream 9 Error Messages.)
> 如果在过渡到Communicating状态时，设备发起的S1F13/S1F14事务还是开放的，且随后没有能收到主机的回复，会被认为是设备的通信故障。当发生事务计时器超时时，应发送S9F9，详细参考4.9

3.2.5 State Transitions

> 设备转换

Table 3.2 contains a full description of the state transitions depicted in Figure 3.2.1.
> 表3.2包含了对图3.2.1中描述的状态转换的完整描述

When the operator switches from the DISABLED state to the ENABLED state, no collection event shall occur, since no messages can be sent until communications have been established. The process of establishing communications serves to notify the host that communications are ENABLED. No other collection events are defined for the Communications State Model.
> 当操作员从Disable切换到Enable状态时，不会触发任何collection event，因为在建立通信之前不能发送任何消息。建立通信的过程的目的是为了通知主机，通信是enable的。

![1686114794550](image/SEMIE30文档记录/1686114794550.png)

### 3.3 Control State Model

> Control状态模型

The CONTROL state model defines the level of cooperation between the host and equipment. It also specifies how the operator may interact at the different levels of host control. While the COMMUNICATIONS state model addresses the ability for the host and equipment to exchange messages, the CONTROL model addresses the equipment’s responsibility to act upon messages that it receives.

> Control状态模型定义了主机和设备之间的控制等级。他还规定了操作员如何在主机控制的不同等级上进行交互。通信状态模型解决了主机和设备交换信息的能力，而控制状态模型解决了设备对他所收到信息而采取的行动。

The CONTROL model provides the host with three basic levels of control. In the highest level (REMOTE), the host may control the equipment to the full extent possible. The middle level (LOCAL) allows the host full access to information, but places some limits on how the host can affect equipment operation. In the lowest level (OFF-LINE), the equipment allows no host control and only very limited information.

> 控制模式为主机提供了三个基本的控制级别。
>
> 1. 最高控制级别为remote，主机可以在最大程度上控制设备。
> 2. 中间级别为local，允许主机完全访问信息，但对主机如何影响设备的运行有一些限制
> 3. 最低级别为off-line，设备不允许主机控制，只允许非常有限的信息

The control model and communications model (when implemented) do not interact directly. That is, no action or state of one model directly causes a change in behavior of the other. It is true, however, that when the communication state is NOT COMMUNICATING then most message transaction are not functional. When messages cannot be transmitted, the control capabilities and all other GEM capabilities are affected.

> 控制模型和通信模型是相互独立的，一个模型的动作/状态并不会影响另一个模型。然而当通信状态为Not Communicating时，大多数的事务都不起作用。当消息无法发送时，控制能力和其他Gem功能都会受到影响。

Refer to Figure 3.3 as the CONTROL substates and state transitions are defined.

> Control状态和转换的定义参考图3.3，

OFF-LINE
When the OFF-LINE state is active, operation of the equipment is performed by the operator at the operator console. While the equipment is OFF-LINE, message transfer is possible. However the use of messaging for any automation purpose is severely restricted. While the OFF-LINE state is active, the equipment will only respond to those messages used for the establishment of communications or a host request to activate the ON- LINE state.
> Off-line
> 当Off-line状态激活时，设备的操作由操作员在设备上进行。当设备处于Off-Line状态时，可以进行信息传输。但是任何自动化的使用信息传递都会受到严格限制。当Off-Line状态激活时，设备只对那些用于建立通信的信息或者主机要求激活On-Line状态的信息做出反应。

While OFF-LINE, the equipment will respond with an Sx,F0 to any primary message from the host other than S1,F13 or S1,F17. It will process and respond to S1,F13 and S1,F17. S1,F17 is used by the host to request the equipment to transition to the ON-LINE state. The equipment will accept this request and send a positive response only when the HOST OFF-LINE state is active (see transition 11 definition below).
> 在off-line状态下，设备将以SxF0响应主机发送的除S1F13或S1F17以外的任何主消息，仅响应和处理S1F13和S1F17。S1F17被主机用来请求设备过渡到On-Line状态。只有当Host-OffLine状态激活时，设备才会接受这个请求，并发送一个响应，参考转换11

While the OFF-LINE state is active, the equipment shall attempt to send no primary message other than S1,F13,10 S9,Fx,11 and S1,F1 (see ATTEMPT ON-LINE substate). If the equipment receives a reply message from the host other than S1,F14 or S1,F2, this message is discarded.
> 当Off-Line状态激活时，虽然设备可以尝试发送除了S1F13，S9Fy，和S1F1以外的任何主消息，但是设备受到主机发出的S1F14或者S1F2以外的任何次消息，都不会进行响应。

No messages enter the spool when the system is OFF- LINE. Spooling may be active when the Communications State of NOT COMMUNICATING is active. This might occur during OFF-LINE, but since the equipment will not attempt to send messages except as mentioned in the previous paragraph12, no messages will enter the spool.

> 当系统处于Off-Line状态时，没有信息进入spool。当通信状态为Not Communicating时，spooling也可能激活。由于在Off-Line状态下，设备不会试图发送消息，没有信息会进入spool

OFF-LINE has three substates: EQUIPMENT OFF-LINE, ATTEMPT ON-LINE, and HOST OFF-LINE.

> Off-Line有三个子状态，Equipment OffLine，Attempt OnLine，Host OffLine

OFF-LINE/EQUIPMENT OFF-LINE
While this state is active, the system maintains the OFF-LINE state. It awaits operator instructions to attempt to go ON-LINE.
> OffLine/Equipment OffLine
> 当这个状态被激活时，系统会一直保持OffLine状态，直到操作员尝试进入OnLine状态

OFF-LINE/ATTEMPT ON-LINE
While the ATTEMPT ON-LINE state is active, the equipment has responded to an operator instruction to attempt to go to the ON-LINE state. Upon activating this state, the equipment attempts to send an S1,F1 to the host.
> OffLine/Attempt OnLine
> 当这个状态被激活时，表示设备已经响应了操作员的指令，正在尝试进入OnLine状态。激活该状态后，设备会向主机发送S1F1

Note that when this state is active, the system does not respond to operator actuation of either the ON-LINE or the OFF-LINE switch.
> 注意，当这个状态被激活时，系统不会响应操作员对OnLine和OffLine开关的操作。

![1686557580047](image/SEMIE30文档记录/1686557580047.png)

OFF-LINE/HOST OFF-LINE
While the HOST OFF-LINE state is active, the operator’s intent is that the equipment be ON-LINE. However, the host has not agreed. Entry to this state may be due to a failed attempt to go ON-LINE or to the host’s request that the equipment go OFF-LINE from ON-LINE (see the transition table for more detail). While this state is active, the equipment shall positively respond to any host’s request to go ON-LINE (S1,F17). Such a request shall be denied when the HOST OFF- LINE state is not active.

> OffLine/Host OffLine
> 当该状态被激活的时候，表示设备的状态是OnLine，但是主机并没有同意。进入该状态可能是由于尝试进入OnLine状态失败，或者是由于主机要求设备从OnLine状态进入OffLine状态。当此状态激活时，设备要响应任何主机的OnLine请求S1F17。当Host OffLine状态未激活时，S1F17会被拒绝。

ON-LINE
While the ON-LINE state is active, SECS-II messages may be exchanged and acted upon. Capabilities that may be available to the host should be similar to those available from the operator console wherever practical.

> 在OnLine状态下，可以交换SECS-II信息，并对其采取行动。在可行的情况下，主机具备的功能应与设备提供的功能类似。

The use of Sx,F0 messages is not required while the ON-LINE state is active. Their use is discouraged in this case. The only allowed use is to close open transactions in conjunction with message faults.

> 在OnLine状态下，不需要使用SxF0信息。唯一允许的使用是和message fault一起关闭开放的事务

ON-LINE/LOCAL
Operation of the equipment is implemented by direct action of an operator. All operation commands shall be available for input at the local operator console of the equipment.

> OnLine/Local
> 设备的操作是由操作员直接操作的，所有的操作都可以在设备上实现

The host shall have the following capabilities and restrictions when the LOCAL state is active:

> 当local状态激活时，主机应具备以下能力：

- The host shall be prohibited from the use of remote commands that cause physical movement or which initiate processing. During processing, the host shall be prohibited from the use of any remote command that affects that process.
  > 主机被禁止使用引起物理运动或者启动处理的远程命令。在设备运动过程中，禁止主机使用任何影响该运动的远程命令
- During processing, the host shall be prohibited from modifying any equipment constants that affect that process. Other equipment constants shall be changeable during processing. The host shall be able to modify all available equipment constants when no processing is in progress.
  > 在设备处理过程时，禁止主机修改任何影响该过程的设备常量。其他设备常量在处理过程中应该是可以改变的。在没有进行处理过程的时候，主机可以修改所有可用的设备常量。
- The host shall be capable of initiating the upload and download of recipes to/from the recipe storage area on the equipment. The host shall be capable of selecting recipes for execution so long as this action does not affect any currently executing recipe.
  > 主机能够从设备的配方存储区上传和下载配方。主机能够选择要执行的配方，只要该行动不影响任何当前执行的配方
- The host shall be able to configure automatic data reporting capabilities including alarms, event reporting, and trace data reporting. The host shall receive all such reports at the appropriate times.
  > 主机能配置自动数据报告功能，包括报警，事件报告和跟踪数据报告。主机应能在适当的时候接受这些报告
- The host shall be able to inquire for data from the equipment, including status data, equipment constants, event reports, process program directories, and alarms.
  > 主机能从设备中查询数据，包括状态数据，设备常量，事件报告，过程程序目录和报警
- The equipment shall be able to perform Terminal Services as defined in GEM.
  > 设备能执行GEM中定义的终端服务

The host shall be allowed any other capabilities that were not specifically restricted in the above items as long as the LOCAL state is active.

> 只要Local状态被激活，就应该允许主机具有上述能力

NOTE 2: Capabilities mentioned above which are not implemented on a specific equipment may be ignored in this context.

> 注意：上面提到的能力如果没有在具体设备上实现，在此情况下可以忽略

ON-LINE/REMOTE
For equipment which supports the GEM capability of remote control (see Section 4.4), while the REMOTE state is active, the host shall have access, through the communications interface, to the necessary commands to operate the equipment through the full process cycle in an automated manner. The equipment does not restrict any host capabilities when REMOTE is active. The degree of control executed by the host may vary from factory to factory. In some cases, the operator maybe required to interact during remotely controlled processes. This interaction may involve set-up operations, operator assist situations, and others. This state is intended to be flexible enough to accommodate these different situations.

> OnLine-Remote
> 对于支持GEM远程控制能力的设备（见4.4节）当remote状态激活时，主机应该能通过通信接口获得必要的命令，以自动方式操作设备。当Remote处于激活状态时，设备不限制任何主机能力。主机执行的控制程度可能因工厂而异。在某些情况下，操作员可能需要在远程控制过程中进行互动。这种互动可能涉及设置操作，操作者协助等情况。这种状态的目的是为了灵活的适应这些不同的情况。

To support the different factory automation policies and procedures, it shall be possible to configure the equipment to restrict the operator in specific non-emergency procedures. These restrictions shall be configurable so that the equipment may be set up to allow the operator to perform necessary functions without contention with the host. The categories for configuration shall include (but are not limited to):

> 为了支持不同的工厂自动化政策和程序，应能对设备进行配置，以便在特定的非紧急程序中限制操作员。这些限制应是可配置的，以便设备可以被设置未允许操作员执行必要的功能，而不与主机发生争执。
> 配置的类别应包括但不限于：

- change equipment constants (process-related),
- change equipment constants (non-process-related),
- initiate process program download,
- select process program,
- start process program,
- pause/resume process program,
- operator assist,
- material movement to/from equipment,
- equipment-specific commands (on a command-by-command basis if needed).

> - 改变与工艺有关的设备常量
> - 改变与工艺无关的设备常量
> - 工艺程序下载
> - 选择工艺程序
> - 启动工艺程序
> - 暂停/恢复工艺程序
> - 操作员协助
> - 设备特定的命令

NOTE 3: Capabilities mentioned above which are not implemented on a specific equipment may be ignored in this context.

> 上面提到的能力如果没有在设备上实现，可以忽略

No capabilities that are available to the operator when the LOCAL state is active should be unconditionally restricted when the REMOTE state is active. The supplier may provide for configurable restriction of operator capabilities not included in the list above if desired. No configurability is necessary for any operator functions not available to the host.

> 当本地状态激活时，操作员可用的任何能力都不在远程状态激活时被无条件限制。如果需要，供应商可以对不包括上述列表中的操作员能力进行可配置的限制。对于主机无法使用的操作员功能，没有必要进行配置

The control functions must be shared to some degree between the host and the local operator. At the very least, the operator must have the capability to change the CONTROL state, actuate an Emergency Stop, and interrupt processing (e.g., STOP, ABORT, or PAUSE).

> 控制功能必须在一定程序上由主机和本地操作者共享。至少，操作者必须有能力改变控制状态，执行紧急停止以及中断处理。

All of these capabilities except Emergency Stop may be access-limited.13

> 除了紧急停止外，所有这些功能都可以限制访问

The host software should be designed to be compatible with the capabilities allotted to the operator.

> 主机软件的设计应和分配给操作员的能力兼容
