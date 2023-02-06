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

SECS-II gives fo rm and meaning to messages exchanged between equipment and host using a message transfer protocol, such as SECS-I.
> SECS-II为使用消息传输协议(如SECS-I)在设备和主机之间交换的消息提供格式和含义。

#### 1.2.1

SECS-II defines the method of conveying information between equipment and host in the form of messages. These messages are organized into categories of activities, called streams, which contain specific messages, called functions. A request for information and the corresponding data transmission is an example of such an activity.
> SECS-II定义了在设备(Equipment)和主机(Host)之间以消息形式传递信息的方法。这些消息被组织成活动类别(称为Stream)，其中包含特定的消息(称为Function)。信息请求和相应的数据传输就是这种活动的一个例子。

#### 1.2.2

SECS-II defines the structure o f messages into entities called items and lists of items. This structure allows for a self-describing data format to guarantee proper interpretation of the message.
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

## 第三章：The Message Transfer Protocal

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
