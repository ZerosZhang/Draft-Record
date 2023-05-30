# SEMI E5

## 第一章：Introduction

### 1.1 Intent

The SEMI Equipment Communications Standard Part 2 (SECS-II) defines the details of the interpretation of messages exchanged between intelligent equipment and a host. This specification has been developed in cooperation with the Japan Electronic Industry Development Association Committee 12 on Equipment Communications.

> 半设备通信标准第2部分（==SECS-II==）定义了智能设备(Equipment)和主机(Host)之间交换消息的解释的细节。 该规范是与日本电子行业发展协会关于设备通信的12委员会合作开发的。

#### 1.1.1

It is the intent of this standard to be fully compatible with SEMI Equipment Communications Standard E4 (SECS-I). It is also the intent to allow for compatibility with alternative message transfer protocols. The details of the message transfer protocol requirements are contained in Section 3.

> 该标准的目的是与半设备通信标准E4（==SECS-I==）完全兼容。它也是为了允许与替代消息传输协议兼容的目的。消息传输协议要求的详细信息在第3节中包含。

#### 1.1.2  

It is the intent of this standard to define messages to such a level of detail that some consistent host software may be constructed with only minimal knowledge of individual equipment. The equipment, in turn, may be constructed with only minimal knowledge of the host.

> 本标准的目的是将消息定义到如此详细的程度，以至于一些一致的主机软件可能只需要对单个设备有最少的了解就可以构造出来。反过来，这些设备可能只需要对主机有最少的了解就可以构建。

#### 1.1.3  

The messages defined in the standard support the most typical activities required for IC manufacturing. The standard also provides for the definition of equipment-specific messages to support those activities not covered by the standard messages. While certain activities can be handled by common software in the host, it is expected that equipment-specific host software may be required to support the full capabilities of the equipment.

> 标准中定义的消息支持IC制造所需的最典型的活动。该标准还规定了特定于设备的消息的定义，以支持标准消息未涵盖的活动。虽然某些活动可以由主机中的通用软件处理，但预计可能需要特定于设备的主机软件来支持设备的全部功能。

### 1.2 Overview

SECS-II gives form and meaning to messages exchanged between equipment and host using a message transfer protocol, such as SECS-I.
> SECS-II为使用消息传输协议(如SECS-I)在设备和主机之间交换的消息提供格式和含义。

#### 1.2.1

SECS-II defines the method of conveying information between equipment and host in the form of messages. These messages are organized into categories of activities, called streams, which contain specific messages, called functions. A request for information and the corresponding data transmission is an example of such an activity.
> SECS-II定义了在设备(Equipment)和主机(Host)之间以消息形式传递信息的方法。这些消息被组织成活动类别(称为Stream)，其中包含特定的消息(称为Function)。信息请求和相应的数据传输就是这种活动的一个例子。

#### 1.2.2

SECS-II defines the structure of messages into entities called items and lists of items. This structure allows for a self-describing data format to guarantee proper interpretation of the message.
> SECS-II将消息结构定义为称为==项==(items)和==项列表==(lists of items)的实体。这种结构允许使用自定义的数据格式，以保证消息的正确解析。

#### 1.2.3

The interchange of messages is governed by a set of rules for handling messages called the transaction protocol. The transaction protocol places some minimum requirements on any SECS-II implementation.
> 消息的交换由一组用于处理消息的规则控制，这些规则称为事务协议(transaction protocol)。事务协议对任何SECS-II实现提出了一些最小的要求。

### 1.3 Application

SECS-II applies to equipment and hosts used in the manufacturing of semiconductor devices. Examples of the activities supported by the standard are: transfer of control programs, material movement information, measurement data, summarized test data, and alarms.
> SECS-II适用于半导体器件制造中使用的设备和主机。该标准支持的活动示例有:控制程序、材料运动信息、测量数据、汇总测试数据和警报的传递。

#### 1.3.1

The minimum compliance to this standard involves meeting the few constraints outlined in Section 1. It is expected that a given piece of equipment will require only a subset of the functions described in this standard. The number of functions and the selection of functions will depend upon the equipment capabilities and requirements. For each piece of equipment, the exact format for each function provided must be docu- mented according to the form outlined in Section 7.
> 符合本标准的最低要求包括满足第1节中概述的几个约束条件。预期一个给定的设备只需要本标准中描述的功能的一个子集。功能的数量和功能的选择将取决于设备的能力和要求。对于每一件设备，所提供的每个功能的确切格式必须按照第7节中概述的格式进行记录。

#### 1.3.2  

It is assumed that the equipment will define the messages used in a particular implementation of SECS- II. It is assumed the host will support equipment implementation.
> 假定设备将定义在SECS-II的特定实现中使用的消息。假定主机将支持设备实现。

### 1.4 Applicable Documents

#### 1.4.1  ANSI

X3.4-1977 — Code for Information Interchange (ASCII)

#### 1.4.2  IEEE

754 — Standard for Binary Floating Point Arithmetic

#### 1.4.3  SEMI Standards

SEMI E4 — SEMI Equipment Communications Standard 1 Message Transfer (SECS-I)
SEMI E6 — Facilities Interface Specifications Guideline and Format

#### 1.4.4

The Japan Electronic Industry Development Association (JEIDA) has requested that the SECS-II standard incorporate support for the JIS-8 codes for data exchange. This code would allow support for katakana characters in Japanese implementations of SECS-II.
JIS 8-bit Coded Character Set (JIS-6226) for information Interchange, Japanese Industrial Standards.
NOTE 1: As listed or revised, all documents cited shall be the latest publications of adopted standards.
> 参考文档：略

### 1.5

This standard does not purport to address safety issues, if any, associated with its use. It is the responsibility of the users of this standard to establish appropriate safety and health practices and determine the applicability of regulatory limitations prior to use.
> 本标准并不旨在解决与其使用相关的安全问题。本标准的使用者有责任在使用前建立适当的安全和健康做法，并确定法规限制的适用性。

## 第二章：Selected Definitions

2.1 The following brief definitions refer to sections providing further information.

> 以下简要定义参考了提供进一步信息的章节。

2.1.1 block — a physical division of a message used by the message transfer protocol (see Section 3.3).

> [block] —— 消息传输协议所使用的消息的物理分割

2.1.2 conversation — a sequence of related messages (see Section 5.4).

> [conversation] —— 一系列相关的信息

2.1.3 conversation timeout — an indication that a conversation has not completed properly (see Section 5.4.1).

> [conversation timeout] —— 谈话没有正确结束的指示

2.1.4 device ID — a number between 0 and 32767 used in identifying the particular piece of equipment communicating with a host (see Section 3.4.1).

> [device ID] —— 一个介于0和32767之间的数字，用于识别与主机通信的特定设备

2.1.5 equipment — the intelligent system which communicates with a host.

> [equipment] —— 与主机通信的智能系统。

2.1.6 function — a specific message for a specific activity within a stream (see Section 4.2).

> [function] —— 在stream中用于特定活动的特定消息

2.1.7 host — the intelligent system which communicates with the equipment.

> [host] —— 和设备通讯的智能系统

2.1.8 interpreter — the system that interprets a primary message and generates a reply when requested (see Section 3.2).

> [interpreter] —— 【解释器】用于解析主消息并生成回复的系统

2.1.9 item — a data element within a message (see Section 6.2).

> [item] —— 消息中的数据元素（message其实包含了TCP/IP协议中的各种消息头，与SECS相关的只有item，这个是message的实际数据）

2.1.10 item format — a code used to identify the data type of an item (see Section 6.2).

> [item format] —— 用于标识item的数据类型的代码

2.1.11  list — a group of items (see Section 6.3).

> [list] —— 一组item

2.1.12 message — a complete unit of communication (see Section 3.2).

> [message] —— 一个完整的通讯单元（包含TCP/IP中的各种消息头）

2.1.13 message header — information about the message passed by the message transfer protocol (see Section 3.4).

> [message header] —— 通过消息传输协议传输的消息的相关信息

2.1.14  multi-block message — a message sent in more than one block by the message transfer protocol (see Section 3.3.2).

> [multi-block message] —— 通过消息传输协议在多块中发送的消息

2.1.15  originator — the creator of a primary message (see Section 3.2).

> [originator] —— 主消息的发起者

2.1.16 packet — a physical division of a message used by the message transfer protocol (see Section 3.3).

> [packet] —— 消息传输协议使用的一条消息的物理划分（这个和block是同一个解释）

2.1.17 primary message — an odd numbered message. Also, the first message of a transaction (see Sections 3.2 and 4.2).

> [primary message] —— [主消息]奇数消息，同样也是事务的第一条消息。

2.1.18 reply — the particular secondary message corresponding to a primary message (see Sections 3.2 and 4.2).

> [reply] —— 对应于[主消息]的特定[次消息]

2.1.19 secondary message — an even-numbered message. Also the second message of a transaction (see Sections 3.2 and 4.2).

> [secondary message] —— [次消息]偶数消息，同样也是事务的第二条消息

2.1.20  single-block message — a message sent in one block by the message transfer protocol (see Section 3.3.1).

> [single-block message] —— 在同一个block中发送的消息

2.1.21  stream — a category of messages (see Section 4.1).

> [stream] —— 一个种类的消息

2.1.22 transaction — a primary message and its associated secondary message, if any (see Section 5.2).

> [transaction] —— 主消息和关联的次消息（如果有）

2.1.23 transaction timeout — an indication from the message transfer protocol that a transaction has not completed properly (see Section 3.5).

> [transaction timeout] —— 表示事务没有正确完成


## 第三章：The Message Transfer Protocal 消息传输协议

3.1 Intent — SECS-II is fully compatible with the message transfer protocol defined by SECS-I. It is the intent of this standard to allow for compatibility with alternative message transfer protocols. The purpose of this section is to define the requirements of the interaction between an application using SECS-II and the message transfer protocol. The methods used to implement these requirements are not covered as a part of this standard. The terms used in this standard are those used by SECS-I. Equivalent terms may be different for other message transfer protocols.
> SECS-II和SECS-I定义的消息传输协议完全兼容。本标准的目的是为了与其他消息传输协议的兼容性，本节的目的是定义使用了SECS-II的程序和通讯协议之间的交互需求，实现这些需求的方法在此不赘述了。

3.2  Messages — The message transfer protocol is used to send messages between equipment and host. The message transfer protocol must be capable of sending a primary message, indicating whether a reply is requested; and, if a reply is requested, it must be capable of associating the corresponding secondary message or reply message with the original primary message. The term originator will refer to the creator of the original primary message. The term interpreter will refer to the entity that interprets the primary message at its destination and generates a reply when requested.
> 消息传输协议用于在设备和主机之间发送消息。消息传输协议必须能够发送主消息，表明是否要求回复; 如果要求回复，则必须能够将相应的次消息或应答消息与主消息关联起来。发起者将指代主消息的创建者。解释器指的是在设备上解释主消息并在请求时生成应答的实体。

3.3 Blocking Requirements — The message transfer protocol must support the following SECS-II message blocking requirements.
消息传输协议必须支持以下 SECS-II 消息分块要求。

3.3.1  Single-Block Messages — SECS-II requires that certain messages be sent in a single block or single packet by the message transfer protocol. Those messages defined in this standard as single-block SECS-II messages must be sent in a single-block or packet. The method used by the application software to tell the message transfer protocol that a particular message must be sent as a single block is not covered as part of this standard. For compatibility with SECS-I, the maximum length allowed for a single-block SECS-II message is 244 bytes. The minimum requirement for the message transfer protocol is to be able to send single-block SECS-II messages.
> 单块消息-SECS-II通过消息传输协议在单个块或单个数据包中发送某条消息。本标准中定义为单块SECS-II消息的消息必须在single-block或packet中发送。应用软件用来告诉消息传输协议特定消息必须作为单个块发送的方法不属于本标准的一部分。为了与SECS-I兼容，单个块SECS-II消息允许的最大长度为244字节。消息传输协议的最低要求是能够发送单块SECS-II消息。

3.3.2 Multi-Block Messages — For compatibility with SECS-I, SECS-II messages that are longer than 244 bytes are referred to as multi-block messages. Also, certain SECS-II messages are allowed to be multi-block messages even if they otherwise meet the single-block length requirements. Certain older implementations may impose application-specific requirements on block sizes for certain incoming messages. Beginning with the 1988 revision of the standard, new applications may not impose application-specific requirements on incoming block sizes. Applications implemented before 1988 may impose such requirements.
> 为了与 SECS-I 兼容，长于244字节的 SECS-II 消息称为多块消息。另外，允许某些 SECS-II 消息为多块消息，即使它们满足单块长度要求。某些较旧的实现可能会对某些传入消息的块大小施加特定于应用程序的要求。从1988年修订标准开始，新的申请可能不会对进入的块大小施加特定的申请要求。在1988年以前实施的申请可能会施加这些要求。

3.4  Message Header — The message transfer protocol must provide the following information, called the message header, with every message. Only the content of the message header is defined by this standard. The exact format of the message header passed between the application and the message transfer protocol is not covered as part of this standard.
> 消息传输协议必须为每条消息提供接下来的信息，称为消息头。此标准仅定义消息头的内容。在应用程序和消息传输协议之间传递的消息头的确切格式不作为本标准的一部分。

NOTE 2: In SECS-I, this information is contained in the 10-byte header of each block of a message.
> 在SECS-I中，该information被包含在消息的每个block的10字节的头中

3.4.1 Device ID — The message transfer protocol must be capable of identifying the device ID (0-32767) which indicates the source or destination of a message.
> 消息传输协议必须能够识别Device ID (0-32767)，Device ID表示消息的来源或者目的地。

3.4.2 Stream and Function — The message transfer protocol must be capable of identifying to SECS-II a minimum15-bit message identification code. In SECS- II, messages are identified by a stream code (0-127, 7 bits) and a function code (0-255, 8 bits). Each combination of stream and function represents a distinct message identification.
> 消息传输协议必须能够识别 SECS-II 的最小15位消息识别代码。在 SECS-II 中，消息由一个流代码(0-127,7位)和一个函数代码(0-255,8位)标识。stream和function的每个组合表示一个不同的消息标识。

3.4.3 Reply Requested — The message transfer protocol must be capable of identifying whether a reply is requested to a primary message.
> 消息传输协议必须能够识别主消息是否要求回复。

3.5 Transaction Timeout — It is presumed that the message transfer protocol will notify SECS-II in the event of failure to receive an expected reply message within a specified transaction timeout period.
NOTE 3: In SECS-I, a transaction timeout occurs if either the reply timeout (T3) is exceeded before the first block of a reply message is received or if the inter-block timeout (T4) is exceeded before an expected block of a multi-block message is received.
> 如果在指定的事务超时时间内未能接收到预期的应答消息，则消息传输协议将通知 SECS-II。
注3: 在 SECS-I 中，如果在接收到第一个应答消息块之前超过了应答超时(T3) ，或者在接收到多个块消息的预期块之前超过了块间超时(T4) ，则发生事务超时。

3.6 Multiple Open Transactions — This standard allows, but does not require, the support of more than one concurrent open transaction.
> 该标准允许但不要求支持多个并发打开事务。

## 第四章：Streams and Functions

## 第五章：Transaction and Conversation Protocols

> 第五章：事务和会话消息

### 5.1 Intent

For an implementation to be in compliance with SECS-II, it must meet the minimum transaction requirements outlined in this section. The conversation protocols serve to further define the use and interaction between transactions.

> SECS-II的实现，必须满足本节中概述的最低「Transaction」需求。
> 「Conversation」协议用于进一步定义「Transaction」之间的使用和交互。

### 5.2 Transaction Definition

> 事务「Transaction」的定义

A transaction forms the basis for all information exchanges in SECS-II. A transaction consists of either a primary message for which no reply is requested, or a primary message which requests a reply together with its corresponding secondary message. Secondary messages cannot request a reply.

> 在SECS-II中，「Transaction」构成了所有信息交换的基础。
> 「Transaction」由未请求回复的主消息或请求回复的主消息及其相应的次消息组成。
> 次消息无法请求回复。

### 5.3 Transaction Level Requirements

> 事务等级「Transaction Level」

The following are the requirements to comply with the SECS-II protocol at the transaction level:

> 在SECS-II协议中，「Transaction Level」必须满足以下要求:

1. Respond to S1F1 with S1F2
   > 使用S1F2回复S1F1
2. For any received message that cannot be processed by the equipment, send the appropriate error message on Stream 9. As described in Section 7.13, S9-F1, F3, F5, F7, or F11 are possible.
   > 对于设备无法处理的的消息，在Stream 9上发送相应的错误消息。
3. Format any other supported messages according to Section 7.
   > 根据第七章的内容，处理其他支持的消息
4. Upon detection of a transaction timeout at the equipment, send S9F9 to the host.
   > 在设备上检测到事务超时「Transaction Timeout」时，将S9F9发送到主机。
5. Upon receipt of function 0 as a reply to a primary message, terminate the related transaction. No error message should be sent to the host by the equipment.
   > 在收到Function 0作为对主要消息的回复时，终止相关事务。设备不应向主机发送错误消息。

### 5.4 Conversation Protocols

> 会话「Conversation」协议

A conversation is a series of one or more related transactions used to complete a specific task. A conversation should include all transactions necessary to accomplish the task and leave both the originator and interpreter free of resource commitments at its conclusion.

> 会话「Conversation」是用来完成特定任务的一个或多个相关事务「Transaction」的系列。会话应该包括完成任务所需的所有事务，并在会话结束时让Host和Equipment都释放资源。

#### 5.4.1 Conversation Timeout

A conversation timeout is used to indicate that a conversation has not completed properly. A conversation timeout is application-dependent, and the methods used for detecting conversation timeouts are not covered as part of this standard. A conversation timeout will terminate further action on the conversation, and will allow for the clearing of any committed resources. Upon detection of a conversation timeout at the equipment, S9F13 should be sent to the host.
> 会话超时用于指示会话未正常完成。会话超时取决于应用程序，用于检测会话超时的方法不在本标准的范围内。会话超时将终止对会话的进一步操作，并允许清除任何已提交的资源。在设备上检测到会话超时时，应该将S9F13发送到主机。

#### 5.4.2 Types of Conversations

There are seven types of conversations which characterize all information exchanges in SECS-II:
> SECS-II的所有信息交流有七种类型的对话:

1. A primary message with no reply is the simplest conversation. This message must be a single-block SECS-II message. The originator must assume that the interpreter responds to the message. This conversation is used where the originator can do nothing if the message is rejected.
   > ==不需要答复的主信息==是最简单的对话。此消息必须是单块SECS-II消息。
   > originator必须假设interpreter响应消息。
   > 此对话用于消息被拒绝时originator不能做任何事情的情况。
2. If the interpreter has data that the originator wants, the data are requested with a primary message and the data returned to the originator as a reply message. It is assumed that the originator requesting the data is prepared to receive the amount of data returned. This is the request/data conversation.
   > 如果interpreter拥有originator想要的数据，则使用主消息请求数据，并将数据作为应答消息返回给originator。
   > 假定请求数据的发起者已准备好接收返回的数据量。这是==请求/数据对话==。
3. If the originator wishes to send data in a single-block SECS-II message to the interpreter, then the originator sends the data and expects an acknowledgment from the interpreter. This is the send/acknowledge conversation.
   > 如果originator希望以单块SECS-II消息的形式向interpreter发送数据，那么originator将发送数据并期待interpreter的确认。
   > 这是==发送/确认对话==。
4. If the originator has a multi-block SECS-II message to send for a particular exchange, then the originator must receive permission from the interpreter prior to sending the data. The first transaction requests permission to send, and the interpreter either grants or denies permission. If permission is granted, the originator sends the data and the interpreter replies appropriately. This is the inquire/grant/send/ack- nowledge conversation. Between the inquire and the send, the interpreter may commit some resources in preparation for the data. Consequently, a conversa- tion timeout may be set by the interpreter at a time dependent upon the application, at which time the interpreter will free its resources and send an S9,F13 error message to the originator. Note that under the definition of S9,F13 in this standard, only the equipment should generate an error message to the host under these conditions.
   > 如果originator要为特定的交换发送多块SECS-II消息，那么originator必须在发送数据之前获得interpreter的许可。第一个事务请求发送权限，解释器要么授予许可，要么拒绝许可。如果授予了权限，发起者将发送数据，解释器将适当地回复。这是查询/授予/发送/确认知识对话。在查询和发送之间，解释器可能会提交一些资源以准备数据。因此，解释器可以在依赖于应用程序的时间设置会话超时，此时解释器将释放其资源并向发起者发送S9,F13错误消息。注意，在本标准中S9、F13的定义下，只有设备在这些条件下才应该向主机生成错误消息。
5. There is a conversation related to the transfer of unformatted data sets between equipment and host. This conversation is described in detail in Stream 13. (See Section 7.17)
6. There is a conversation related to the handling of material between equipment. This conversation is described in detail in Stream 4. (See Section 7.8)
7. The originator may request information from the interpreter which requires some time to obtain (e.g., operator input is required). The first transaction requests the information and the interpreter responds in one of three ways: 1) the information is returned, 2) the interpreter indicates that the information cannot or will not be obtained, or 3) the interpreter indicates that the information will be obtained and returned in a subsequent transaction, as specified for this conversation. For case number 3, the interpreter will initiate the subsequent transaction when the information is available.

Case 3 is the request/acknowledge/send/acknowledge transaction.
The originator of the request/acknowledge/send/ acknowledge conversation may commit some resources in anticipation of the send/acknowledge transaction. Consequently, a conversation timeout may be set by the originator at a time dependent on the application. On timeout, the originator will free its resources and restart the conversation with the ‘request', or send an S9,F13 error message. Note that under the definition of S9,F13 in this standard, only the equipment should generate an error message to the host under these conditions.

#### 5.4.3

The key words, request, data, send, acknowledge, inquire, and grant are used in the function names as an aid to understanding the relationship between the messages and the conversation. Single message transactions do not use these words.

## 第六章：Data Structures

### 6.3 List

A list is an ordered set of elements, where an element can be either an item (6.2) or a list. The list header (LH) has the same form as an item header with format type 0. However, the length bytes refer to the number of elements in the list rather than to the number of bytes. The list structure allows grouping items of related information which may have different formats into a useful structure.
> 列表是元素的有序集合，其中元素可以是项(6.2)或列表。列表标题(Lh)具有与格式类型为0的项目标题相同的形式。但是，长度字节指的是列表中的元素数量，而不是字节数。该列表结构允许将可能具有不同格式的相关信息项分组为有用的结构。

#### 6.3.1

A zero-length in the format byte is illegal and produces an error. A zero-length in the list length bytes has a special meaning, which is defined in the detailed message definitions.
> 格式字节中的零长度是非法的，并会产生错误。列表长度字节中的零长度具有特殊含义，这在详细的消息定义中定义。

## 第七章: Message Detail

### 7.3 Message Usage

This section discusses message features and where they may be used.
> 本节讨论消息特性以及它们可以在哪里使用。

#### 7.3.1 Zero Length Items and Lists

> 零长度项和列表

Certain message definitions may use zero length data items and zero length lists as a technique to convey specific information to the receiver of the message. For commands (i.e., “Do Something”) and requests (i.e., “Return Some Data”), it may be used to mean “Use default values for the data item(s) which were not included”. The default may be a specific value or a value chosen by the equipment.
> 某些消息定义可能使用==零长度数据项和零长度列表==作为向消息接收者传递特定信息的技术。对于命令(如“Do Something”)和请求(如“Return Some Data”)，它可以用来表示“==对未包含的数据项使用默认值==”。默认值可以是一个特定的值，也可以是由设备选择的值。

##### 7.3.1.1

For messages reporting data (either responses to requests or asynchronous reports), the technique may be used to indicate that the desired information is not available or not applicable. In some cases, the fact that data is unavailable may indicate success or failure of a command.
> 对于报告数据的消息(对请求的响应或异步报告)，可以使用该技术来指示所需的信息不可用或不适用。在某些情况下，数据不可用的事实可能表明命令的成功或失败。

##### 7.3.1.2

Certain message definitions may define a zero length data item or a zero length list to mean “the information is not supplied.” The receiving party should react to this lack of information as it deems appropriate.
> 某些消息定义可能定义零长度数据项或零长度列表，以表示“不提供信息”。接收方应在其认为适当时对这种缺乏资料的情况作出反应。

### 7.5 Stream 1

This stream provides a means for exchanging information about the status of the equipment, including its current mode, depletion of various consumable items, and the status of transfer operations.
> 这个Stream提供了一种交换设备状态信息的方法，包括它的当前模式，各种消耗品的消耗，以及转移操作的状态。

#### S1F13 Establish Communications Request(CR)

The purpose of this message is to provide a formal means of initializing communications at a logical application level both on power-up and following a break in communications. It should be the following any period where host and Equipment SECS applications are unable to communicate. An attempt to send an Establish Communications Request (S1,F13) should be repeated at programmable intervals until an Establish Communications Acknowledge(S1,F14) is received within the transaction timeout period with an acknowledgement code accepting the establishment.
> 此消息的目的是提供一种在逻辑应用程序级别上初始化通信的正式方法，包括启动时和通信中断后。它应该是在主机和设备SECS应用程序无法通信的任何时间段之后。应以可编程间隔重复尝试发送建立通信请求(S1,F13)，直到在事务超时时间内接收到建立通信确认(S1,F14)，并接收确认代码接受建立。
