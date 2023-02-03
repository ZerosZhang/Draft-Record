# SEMI文档记录

This section deﬁnes the detailed format of the messages used by the procedures in the previous section.

> 本节定义了前一节中过程使用的消息的详细格式。

An item is an information packet which has a length and format defined by the first 2, 3, or 4 bytes of the item. These first bytes are called the item header (IH).

> 一个Item是一个信息包，它的长度和格式由条目的前2、3或4个字节定义。这些字节称为item header(IH)。

The item header consists of the format byte and the length byte(s) as shown in Figure 2.

> item header由格式字节和长度字节组成，如图2所示。

Bits one and two of the item header tell how many of the following bytes refer to the length of the item.
item header的第1位和第2位告诉下面的字节中有多少是指项的长度。

The item length refers to the number of bytes following the item header, called the item body (IB), which is the actual data of the item.

> item length是指项头后面的字节数，称为item body(IB)，它是项的实际数据。

The item length refers only to the item body not including the item header, so the actual number of bytes in the message for one item is the item length plus 2, 3, or 4 bytes for the item header.

> 项长度仅指项体，不包括项标题，因此每个项的消息中的实际字节数为项长度加上项标题的2、3或4字节。

All bytes in the item body are in the format specified in the format byte.

> item body中的所有字节都是在格式字节中指定的格式。

## Message Length

Message Length is a four byte unsigned integer value which specifies the length in bytes of the Message Header plus the Message Text. Message Length is transmitted most significant byte (MSB) first and least significant byte (LSB) last.

> 消息长度-消息长度是一个四字节无符号整型值，以字节为单位指定消息头加上消息文本的长度。消息长度是最重要字节(MSB)先传输，最不重要字节(LSB)最后传输。

The minimum possible Message Length is 10 (Header only). The maximum possible Message Length is implementation-speciﬁc.

> 最小可能的消息长度是10(仅头)。可能的最大消息长度取决于具体实现。

## Message Header

The Message Header is a ten-byte field. The bytes in the header are numbered from byte 0 (first byte transmitted) to byte 9 (last byte transmitted). The format of the Message Header is as follows:

> Message Header是一个10字节的字段。头中的字节编号从字节0(传输的第一个字节)到字节9(传输的最后一个字节)。Message Header的格式如下:

The physical byte order is designed to correspond as closely as possible to the SECS-I header.

> 物理字节顺序被设计成尽可能与SECS-I报头相对应。

### Session ID

Session ID is a 16-bit unsigned integer value, which occupies bytes 0 and 1 of the header (byte 0 is MSB, 1 is LSB). Its purpose is to provide an association by reference between control messages (particularly Select and Deselect) and subsequent data messages.  It is the role of HSMS sub- sidiary standards to specify this association further.

> Session ID为16位无符号整型值，占用表头的0和1字节(0字节为MSB, 1字节为LSB)。它的目的是通过引用控制消息(特别是选择和取消选择)和后续数据消息之间的关联。HSMS附属标准的作用是进一步指定这种关联。

### Header Byte 2

This header byte is used in different ways for different HSMS messages. For Control Messages (see SType, below) it contains zero or a status code. For a Data Message whose PType (see below) = 0, it contains the W-Bit and SECS Stream. For a Data Message with PType not equal to 0, see "Special Considerations."

> 这个头字节以不同的方式用于不同的HSMS消息。对于控制消息(请参阅下面的SType)，它包含零或状态码。对于PType = 0的数据消息，它包含W-Bit和SECS流。对于PType不等于0的数据消息，请参阅“特殊注意事项”。

### Header Byte 3

This header byte is used in different ways for different HSMS messages. For Control Messages, it contains zero or a status code. For a Data Message whose PType (see below) = 0, it contains the SECS Function. For a Data Message with PType not equal to 0, see "Special Considerations."
这个头字节以不同的方式用于不同的HSMS消息。对于控制消息，它包含零或状态代码。对于PType = 0的数据消息，它包含SECS函数。对于PType不等于0的数据消息，请参阅“特殊注意事项”。

### PType

PType (Presentation Type) is an 8-bit unsigned integer value which occupies byte 4 of the header.
PType is intended as an enumerated type defining the presentation layer message type:  how the Message Header and Message Text are encoded.
Only PType = 0 is defined by HSMS to mean SECS-II message encoding. For non-zero PType values, see "Special Considerations."
> PType(表示类型)是一个8位无符号整型值，占用头文件的第4字节。PType是定义表示层消息类型的枚举类型:消息头和消息文本是如何编码的。只有PType = 0被HSMS定义为表示SECS-II消息编码。对于非零PType值，请参见“特殊注意事项”。

### SType

SType (Session Type) is a one-byte unsigned integer value which occupies header byte 5. SType is an enumerated type identifying whether this message is an HSMS Data Message (value = 0) or one of the HSMS Control Messages (other). Those values not explicitly deﬁned in the table are addressed in "Special Considerations."
> SType(会话类型)是一个单字节无符号整型值，占用头字节5。SType是一个枚举类型，用于标识该消息是HSMS数据消息(值= 0)还是HSMS控制消息之一(其他)。那些在表中没有明确定义的值在“特殊考虑”中处理。

### System Bytes

System Bytes is a four-byte field occupying header bytes 6-9. System Bytes is used to identify a transaction uniquely among the set of open transactions.
> System Bytes是一个占据头字节6-9的4字节字段。System Bytes用于在一组打开的事务中惟一地标识一个事务。

- Uniqueness
  The System Bytes of a Primary Data Message, Select.req, Deselect.req, or Linktest.req message must be unique from those of all other currently open transactions initiated from the same end of the connection. They must also be unique from those of the most recently completed transaction.
  > 主数据消息的System Bytes中，Select.req, Deselect.req, Linktest.req 必须与从连接的同一端发起的所有其他当前打开事务的事务惟一。。它们还必须与最近完成的事务的那些惟一。
- Reply Message
  The System Bytes of a Reply Data Message must be the same as those of the corresponding Primary Message.
  The System Bytes of a Select.rsp, Deselect.rsp, or Linktest.rsp must be the same as those of the respective ".req" message.
  > 应答数据电文的系统字节数必须与对应的主电文的系统字节数相同。Select.rsp, Deselect.rsp, or Linktest.rsp必须与相应的”相同。请求”消息。

# HSMS Message Formats by Type

The specific interpretation of the header bytes in an HSMS message is dependent on the specific HSMS message type as defined by the value of the SType field. The complete set of messages defined is summarized in the table below, shown for PType = 0 (SECS-II message format).
> HSMS消息中报头字节的具体解释取决于SType字段值定义的特定HSMS消息类型。下表总结了定义的完整消息集，显示为PType = 0 (SECS-II消息格式)。

## SType=0: Data Message

An HSMS message with SType = 0 is used by the HSMS Data procedure to send a Data message, either Primary or Reply. The message format is as follows:
> SType = 0的HSMS消息由HSMS Data过程用于发送数据消息(Primary或Reply)。消息格式如下:
HSMS Message Length is always 10 (the length of the header alone) or greater.
> HSMS消息长度总是10(单是消息头的长度)或更大。
The HSMS Message Header is as follows:

- Session ID
- Header Byte2 对于PType值= 0 (SECS-II)的消息，头字节2的格式如下所示

The most signiﬁcant bit (bit 7) of Header Byte 2 is the W-Bit. In a Primary Message, the W-Bit indicates whether the Primary Message expects a Reply message. A Primary Message which expects a Reply should set the W-Bit to 1. A Primary Message which does not expect a Reply should set the W-Bit to 0. A Reply Message should always set the W-Bit to 0. The low-order 7 bits (bits 6-0) of Header Byte 2 contain the SECS Stream for the message. The Stream is a 7-bit unsigned integer value, which identiﬁes a major topic of the message, and its use is deﬁned within SEMI E5 (SECS-II).
> 头字节2的最高位(位7)是w位。在主消息中，W-Bit表示主消息是否需要回复消息。希望收到回复的主消息应将w位设置为1。不需要回复的主消息应该将w位设置为0。回复消息应该总是将w位设置为0。头字节2的低阶7位(比特6-0)包含消息的SECS流。Stream是一个7位无符号整数值，用于标识消息的主要主题，它的使用在SEMI E5 (SECS-II)中定义。

- Header Byte 3
  For messages whose PType value=0, header Byte 3 contains the SECS Function for the message. The Function is an 8-bit unsigned integer value which identiﬁes a minor topic of the message (within the Stream), and its use is deﬁned within SEMI E5 (SECS-II). The least signiﬁcant bit (bit 0) of the Function deﬁnes whether the Data Message is Primary or Reply; the value 1 indicates Primary and the value 0 indicates Reply.
  > 对于PType值=0的消息，头字节3包含该消息的SECS函数。Function是一个8位无符号整数值，用于标识消息的次要主题(在流中)，它的使用在SEMI E5 (SECS-II)中定义。函数的最低有效位(比特0)定义数据消息是主消息还是应答消息;1表示Primary, 0表示Reply。

Communication Established. Select was successfully completed.

Communication Already Active. A previous select has already established communications to the entity being selected in this select.
Connection Not Ready. The Connection is not yet ready to accept select requests.
Connect Exhaust. The connection was accepted, but the entity is already servicing a separate TCP/IP connection and is unable to service more than one at any given time.

The SessionID must match the value of the SessionID of a previously sent Select.req to indicate the particular HSMS session that is ending. Subject to further speciﬁcation by subsidiary standards.

Communication Busy. The session is still in use by the responding entity and so it cannot yet relinquish it gracefully. In this case, if the original requester must terminate communications, the separate procedure should be used as a last resort.

### Reject.req

An HSMS message with SType 7 is used in response to any valid HSMS message received which is not supported by the receiver of the message or which is not valid at the time. It is intended for dealing with attempts to use subsidiary standards or user-defined extensions which are not supported by the receiver (for example, SType equal to any value not defined in this standard). It must be used when an entity receives a control message which is a response (even numbered SType) for which there was no corresponding open transaction.

> SType=7的HSMS消息用于响应收到的任何有效的HSMS消息，表示该消息是不支持或者现在无效。
> 它用于处理接收方不支持的附属标准或用户定义的扩展(例如，SType等于本标准中未定义的任何值)。
> 当一个实体接收到一个没有对应的打开事务的响应(偶数SType)控制消息时，必须使用它。

The HSMS Message Header is as follows:

- SessionID — equal to the value of the Session ID in the message being rejected.
- Header Byte 2 — For ReasonCode = PType Not Supported, equal to the PType in the message being rejected. Otherwise equal to the value of the SType in the message being rejected.
  > Header Byte 2 ： 对于【拒绝理由】=PType Not Supported时，Header Byte 2 = 被拒绝消息的PType。否则等于被拒绝消息的SType
- Header Byte 3 — reason code (always non-zero)
  SType Not Supported. A message was received whose SType value not deﬁned in the HSMS standard or the particular subsidiary standard(s) supported by the entity.

### Separate.req

An HSMS message with SType = 9 is used to terminate HSMS communications immediately. With the exception of the SType value, it is identical to the Deselect.req message. Its purpose is to end HSMS communications immediately and without exception. No response is defined.
SType = 9的HSMS消息用于立即终止HSMS通信。除SType值不同外，它与取消选择相同。请求消息。其目的是立即毫无例外地终止HSMS通信。没有定义响应。

# 第九章 Special Considerations

## 9.1   General Considerations

### 9.1.1   Communications Failures

If a communications failure is detected, the entity should terminate the TCP/IP connection. Upon termination of the connection, the entity may, at this point, attempt to reestablish communications.
> 如果检测到通信失败，entity终止TCP/IP连接。连接终止后，entity此时可以尝试重新建立通信。

## 9.2   TCP/IP Considerations  

### 9.2.1   Connect Separation Time (T5)

The connect procedures initiate some network activity.
> 连接过程启动一些网络活动。
Frequent use of the active mode connect procedure to the IP Address and Port Number of an entity not yet ready to accept connections can be hostile to TCP/IP operations.
> 对于还没有准备好的entity，如果频繁的用主动模式连接该entity的IP地址和端口号， 对网络会产生不好的影响
The passive mode does not generate network activity and is not considered hostile to the network, although it may affect local application performance.
> 被动模式不会触发网络事件，并且对网络无害，虽然他可能会影响本地应用程序的性能。
An Entity initiating a connection in the active mode should limit its use of the connect procedure in a manner that is equivalent to the procedure described here.
> 正如这里所说，在主动模式下发起连接的实体应该限制其对连接过程的使用

After an active connect procedure terminates by any means (successfully or unsuccessfully), the Entity should not initiate another active connect procedure (for the same Remote Entity) until the T5 Connect Separation Time has elapsed.
> 当主动连接过程以任何方式(成功或不成功)终止之后，该实体不应该发起另一个主动连接过程(对于同一远程实体),直到T5 Connect Separation Time过去。
The separation of connect operations will be the sum of the T5 Connect Separation Time interval, plus the duration of the connect operation itself.
> 连接过程的总时间将是T5 Connect Separation Time interval加上连接操作本身的持续时间。

### 9.2.2   NOT SELECTED Timeout (T7)

Entry into the NOT SELECTED state is achieved either by state transition #2 (establishment of a TCP/IP connection). There is a time limit on how long an entity is required to remain in the NOT SELECTED state before either entering the SELECTED state or by returning to the NOT CONNECTED state.
> 进入NOT SELECTED状态是通过建立TCP/IP连接实现的。在进入SELECTED状态或返回NOT CONNECTED状态之前，Entity停在在NOT SELECTED状态是有时间限制的。
Some entities, particularly those unable to accept more than a single TCP/IP connection, may be impaired in their operation by remaining in their NOT SELECTED state as they will be unavailable for communications with other entities. Such entities shall disconnect the TCP/IP connection (State Transition Event #3) if communication remains in the NOT SELECTED state for longer than the T7 timeout period.
> 一些entities，尤其是那些不能接受多于一个TCP/IP连接的实体，可能由于保持在它们的NOT SELECTED状态的操作中受到损害，因为它们将不可用于与其他实体的通信。如果通信保持在未选择状态的时间超过T7超时周期，则这些实体应断开TCP/IP连接(状态转换事件#3)。

### 9.2.3   Network Intercharacter Timeout (T8)

Because TCP/IP is a stream rather than a message pro- tocol, it is possible that bytes which are all part of a single HSMS message may be transmitted in separate TCP/IP messages without any violation of the TCP/IP protocol. Since it is possible that these separate mes- sages may be separated by a substantial period of time, the Network Intercharacter Timeout (T8) is defined.
> 因为TCP/IP是一个流，而不是一个消息协议，所以可以在不违反TCP/IP协议的情况下，在单独的TCP/IP消息中传输属于单个HSMS消息一部分的字节。由于这些单独的消息可能被相当长的时间间隔分开，因此定义了网络字符超时(T8)。

T8 is similar in purpose to the SECS-I T1 timer except that the communications issues which necessitate T8 are not entirely in the control of the sender of the mes- sage. Therefore, it is deﬁned only in terms of the receiver of the message. In particular, if after receipt of a partial message, the T8 timeout period expires prior to receipt of the complete message, the receiving entity shall consider such case as a communications failure, as deﬁned above.
> T8在目的上类似于SECS-1 T1定时器，除了需要T8的通信问题不完全在消息发送者的控制之下。因此，它仅根据消息的接收者来定义。特别是，如果在收到部分消息后，T8超时周期在收到完整消息前到期，则接收实体应将这种情况视为通信失败，如上所述。

### 9.2.4   Multiple Connection Requests Directed to a Single Published Port
>
> 多个连接请求被定向到一个发布的端口
Once a passive entity has accepted a connection on its published port, TCP/IP permits (though does not require) the entity to listen for and accept additional connections directed to the same published port.
> 一旦被动实体接受了其公布端口上的连接，TCP/IP就允许(尽管不要求)该实体监听并接受指向同一公布端口的附加连接。
HSMS permits (though does not require) entities to operate in this manner. However, for the purposes of HSMS compliance, each connection so formed must exhibit the behavior deﬁned in the HSMS state dia- gram as if it were completely independent of any other connection to the same published port.
> HSMS允许(尽管不要求)实体以这种方式运作。然而，出于HSMS合规性的目的，如此形成的每个连接必须表现出HSMS 状态中定义的行为，就好像它完全独立于到相同公布端口的任何其他连接一样。
>
#### 9.2.4.1   Rejection of Additional Connection Requests by a Passive Mode Entity

A passive mode entity unable to service more than a single TCP/IP connection for HSMS communications will follow one of these three procedures with respect to additional con- nection requests.
> 不能服务于超过1个的TCP/IP连接的被动模式的entity，遵循以下3个原则：
a. Accept the connection, but always respond to any subsequent HSMS select procedures with the Com- munication Already Active response code. For the purpose of the HSMS State Diagram, the connect procedure terminates successfully (enters CONNECTED state), but HSMS communications are never established (remain in NOT SELECTED substate). This is the preferred option in that it can provide the most information to the remote entity as to why the connection is refused (see HSMS Select Procedure), but places an addition imple- mentation requirement on the local entity.
> 接受连接，但始终使用“通信已激活”响应代码响应任何后续的HSMS选择过程。对于HSMS状态图，连接过程成功终止(进入CONNECTED状态)，但HSMS通信从未建立(保持在NOT SELECTED子状态)。这是首选选项，因为它可以向远程实体提供关于为什么拒绝连接的大部分信息(参见HSMS选择过程)，但对本地实体提出了额外的实现要求。
