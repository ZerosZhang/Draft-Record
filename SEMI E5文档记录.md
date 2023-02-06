# SEMI E5

## 第五章：Transaction and Conversation Protocols
>
> 第五章：事务和会话消息
>
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
