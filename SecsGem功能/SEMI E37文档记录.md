# SEMI E37-0298 HIGH-SPEED SECS MESSAGE SERVICES (HSMS) GENERIC SERVICES

## 1. Purpose 目的

HSMS provides a means for independent manufacturers to produce implementations which can be connected and interoperate without requiring speciﬁc knowledge of one another.

> HSMS 为独立制造商提供了一种生产实现的手段，这些实现可以相互连接和互操作，而不需要彼此之间的特定知识。

HSMS is intended as an alternative to SEMI E4 (SECS-I) for applications where higher speed communication is needed or when a simple point-to-point topology is insufﬁcient. SEMI E4 (SECS-I) can still be used in applications where these and other attributes of HSMS are not required.

> HSMS 是 SEMI E4(SECS-I)的替代品，用于需要更高速通信或简单点对点拓扑不足的应用。SEMI E4(SECS-I)仍然可以用于不需要 HSMS 的这些属性和其他属性的应用程序中。

HSMS is also intended as an alternative to SEMI E13 (SECS Message Services) for applications where TCP/ IP is preferred over OSI.

> HSMS 还可以作为 SEMI E13(SECS 消息服务)的替代品，用于 TCP/IP 优于 OSI 的应用程序。

It is intended that HSMS be supplemented by subsidiary standards which further specify details of its use or impose restrictions on its use in particular application domains.

> 目的是通过附属标准对 HSMS 进行补充，进一步详细说明其使用情况，或对其在特定应用领域的使用施加限制。

## 2. Scope 范围

High-Speed SECS Message Services (HSMS) deﬁnes a communication interface suitable for the exchange of messages between computers in a semiconductor factory.

> 高速SECS消息服务(HSMS)定义了一种适合于半导体工厂中计算机之间交换消息的通信接口。

## 3. Referenced Documents

SEMI标准参考了
SEMI E4  — SEMI Equipment Communication Standard 1 — Message Transport (SECS-I)
SEMI E5  — SEMI Equipment Communication Standard 2 — Message Content (SECS-II)

## 4. Terminology 术语

API — Application Program Interface. In the case of TCP/IP, a set of programming conventions used by an application program to prepare for or invoke TCP/IP capabilities.

> API：应用程序接口。在TCP/IP下，应用程序使用的一组编程约定，用于调试TCP/IP功能

communication failure — A failure in the communication link resulting from a transition to the NOT CONNECTED state from the SELECTED state. (See Section 9.)

> communication failure：通讯故障，特指从SELECTED状态转换到NOT CONNECTED状态而导致的通讯故障。

conﬁrmed service (HSMS) — An HSMS service requested by sending a message from the initiator to the responding entity which requires that completion of the service be indicated by a response message from the responding entity to the initiator.

> confirmed service(HSMS)：确认服务，表示通过从originator向interpreter发送request消息的HSMS服务，该消息要求interpreter向originator发送响应的消息后才算结束

connection — A logical linkage established on a TCP/ IP LAN between two entities for the purposes of exchanging messages.

> connection：在TCP/IP局域网上两个实体为交换消息而建立的一种逻辑链路。

control message — An HSMS message used for the management of HSMS sessions between two entities.

> control message：一种用于管理两个实体之间的HSMS会话的HSMS消息。

data message — An HSMS message used for commu- nication of application-speciﬁc data within an HSMS session. A Data Message can be a Primary Message or a Reply Message.

> data message：用于在HSMS会话中通信应用程序特定数据的HSMS消息。数据消息可以是主消息或回复消息。

entity — An application program associated with an endpoint of a TCP/IP connection.

> entity：通过TCP/IP连接的应用程序

header — A 10-byte data element preceding every HSMS message.

> header：在每个HSMS消息之前的一个10字节的数据单元。

initiator (HSMS) — The entity requesting an HSMS service. The initiator requests the service by sending an appropriate HSMS message.

> initiator：请求HSMS服务的应用程序

IP Address — Internet Protocol Address. A logical address which uniquely identiﬁes a particular attach- ment to a TCP/IP network.

> IP Address：Internet协议地址。唯一标识TCP/IP网络特定连接的逻辑地址。

local entity — Relative to a particular end point of a connection, the local entity is that entity associated with that endpoint.
local entity：本地实体，相对于远程实体。

message — A complete unit of communication in one direction.  An HSMS Message consists of the Message Length, Message Header, and the Message Text. An HSMS Message can be a Data Message or a Control Message.

message length — A 4-byte unsigned integer ﬁeld specifying the length of a message in bytes.

open transaction — A transaction in progress.

port — An endpoint of a TCP/IP connection whose complete network address is speciﬁed by an IP Address and TCP/IP Port number.

port number — (or TCP port number).  The address of a port within an attachment to a TCP/IP network which can serve as an endpoint of a TCP/IP connection.

primary message — An HSMS Data Message with an odd numbered Function.  Also, the ﬁrst message of a data transaction.

published port — A TCP/IP IP Address and Port num- ber associated with a particular entity (server) which that entity intends to use for receiving TCP/IP connec- tion requests. An entity's published port must be known by remote entities intending to initiate connec- tions.

receiver — The HSMS Entity receiving a message.

remote entity — Relative to a particular endpoint of a connection, the remote entity is the entity associated with the opposite endpoint of the connection.

reply — An HSMS Data Message with an even-num- bered function. Also, the appropriate response to a Pri- mary HSMS Data Message.

responding entity (HSMS) — The provider of an HSMS service. The responding entity receives a mes- sage from an initiator requesting the service. In the event of a conﬁrmed service, the responding entity indicates completion of the requested service by send- ing an appropriate HSMS response message to the ini- tiator of the request. In an unconﬁrmed service, the responding entity does not send a response message.

session — A relationship established between two entities for the purpose of exchanging HSMS messages.

session entity — An entity participating in an HSMS session.

session ID — A 16-bit unsigned integer which identi- ﬁes a particular session between particular session entities.

stream (TCP/IP) — A sequence of bytes presented at one end of a TCP/IP connection for delivery to the other end. TCP/IP guarantees that the delivered sequence of bytes matches the presented stream. HSMS subdivides a stream into blocks of contiguous bytes - messages.

T3 — Reply timeout in the HSMS protocol.

T5 — Connect Separation Timeout in the HSMS protocol used to prevent excessive TCP/IP connect activity by providing a minimum time between the breaking, by an entity, of a TCP/IP connection or a failed attempt to establish one, and the attempt, by that same entity, to initiate a new TCP/IP connection.

T6 — Control Timeout in the HSMS protocol which deﬁnes the maximum time an HSMS control transac- tion can remain open before a communications failure is considered to have occurred. A transaction is consid- ered open from the time the initiator sends the required request message until the response message is received.

T7 — Connection Idle Timeout in the HSMS protocol which deﬁnes the maximum amount of time which may transpire between the formation of a TCP/IP con- nection and the use of that connection for HSMS com- munications before a communications failure is considered to have occurred.

T8 —  Network Intercharacter Timeout in the HSMS protocol which deﬁnes the maximum amount of time which may transpire between the receipt of any two successive bytes of a complete HSMS message before a communications failure is considered to have occurred.

TCP/IP — Transmission Control Protocol/Internet Protocol. A method of communications which provides reliable, connection-oriented message exchange between computers within a network.

TLI — Transport Level Interface. One particular API provided by certain implementations of TCP/IP which provides a transport protocol and operating system independent deﬁnition of the use of any Transport Level protocol.

transaction — A Primary Message and its associated Reply message, if required. Also, an HSMS Control Message of the request (.req) type, and its response Control Message (.rsp), if required.

unconﬁrmed service (HSMS) — An HSMS service requested by sending a message from the initiator to the responding entity which requires no indication of completion from the responding entity.

## 5. HSMS Overview and State Diagram HSMS概述和状态图

High-Speed SECS Message Services (HSMS) deﬁnes a communication interface suitable for the exchange of messages between computers in a semiconductor fac- tory using a TCP/IP environment. HSMS uses TCP/IP stream support, which provides reliable two way simultaneous transmission of streams of contiguous bytes. It can be used as a replacement for SECS-I communication as well as other more advanced communi- cations environments.

> HSMS定义了一种通信接口，基于TCP/IP通讯，用于半导体工厂计算机之间交换信息。

The procedure for HSMS communications parallels the more familiar SECS-I communications it replaces. The following steps are followed for any communica- tions (HSMS or otherwise):

> HSMS通信程序与SECS-I通讯程序类似，按照如下步骤进行通讯：

1. Obtain a communications link between two entities. In SECS-I, this is the RS232 wire physically connecting host and equipment. In HSMS, the link is a TCP/IP connection obtained by the standard TCP/IP connect procedure. Note that the abstract term "entity" is used instead of "host" or "equip- ment." This is because, while HSMS is used for SECS-I replacement, it has more general applica- tions as well. In a SECS-I replacement application, the "host" is an "entity" and the "equipment" is an "entity."

   > 建立两个entity之间的通信链路（局域网）。这里之所以使用术语entity而非equipment或者host，是因为HSMS有更广泛的应用，本质上equipment和host就是一个entity
   >
2. Establish the application protocol conventions to be used for exchanging data messages between two entities. For SECS-I, this step is implicit in the fact that semiconductor equipment is physically con- nected on the two ends of the wire: the protocol is SECS-II. In the case of HSMS, the communications link is a dynamically established TCP/IP connection on a physical link which may be shared with many other TCP/IP connections using protocols other than HSMS or connections using non TCP/IP protocols. HSMS adds a message exchange (called the Select procedure) which is used to conﬁrm to both entities that the particular TCP/IP connection is to be used exlusively for HSMS communications.

   > 建立用于在两个entity之间的Socket通讯。除了建立Socket通讯，HSMS还增加了了一个消息交换(称为Select过程) ，用于向两个entity确认特定的socket连接将专门用于 HSMS 通信。
   >
3. Exchange Data. This is the normal intended purpose of the communications link. In both SECS-I and HSMS, the procedure is to exchange SECS-II encoded messages for the control of semiconductor equipment and/or processes. Data exchange nor- mally continues until one or both of the entities are taken off-line for equipment-speciﬁc purposes, such as maintenance.

   > 交换数据，这也是建立通信连接的目的。在 SECS-I 和 HSMS 中，程序是交换 SECS-II 编码的消息以控制半导体设备和/或工艺。数据交换正常情况下继续进行，直到一个或两个实体为特定设备目的(如维护)脱机。
   >
4. Formally end communications. In SECS-I, there is no formal requirement here; the equipment to be taken off-line stops communicating. In HSMS, a message exchange (either the “bilat eral” Deselect procedure or the “unilateral” separate procedure) is used for both parties to conﬁrm that the TCP/IP connection is no longer needed for HSMS communications.

   > 正式结束通讯。在 SECS-I 中，这里没有正式的要求; 要离线的设备停止通信。在 HSMS 中，双方使用消息交换(“双边”取消选择程序或“单边”分离速率程序)来确认 HSMS 通信不再需要 TCP/IP 连接。
   >
5. Break the communications link. In SECS-I, this is done by physically unplugging the host or equip- ment from the communications cable, which only occurs during repair or physical reconﬁguration of the factory network environment. In HSMS, since it uses the dynamic connection environment of TCP/IP, the TCP/IP connection is logically broken via a release or a disconnect procedure without any physical disconnect from the network medium.

   > 切断通讯。在 SECS-I 中，这是通过将主机或设备从通信电缆中物理拔出来完成的，这只在工厂网络环境的维修或物理重新配置期间才会发生。在 HSMS 中，由于使用 TCP/IP 的动态连接环境，所以 TCP/IP 连接通过释放或断开过程在逻辑上中断，而不与网络介质发生任何物理断开。
   >

Two additional procedures, of a diagnostic nature, are supported in HSMS, which are generally not required by a simple SECS-I link or a SECS-I direct replacement. These follow:

> HSMS 支持另外两个诊断性质的程序，这些程序通常不需要简单的 SECS-I 链接或 SECS-I 直接替换。以下是:

1. Linktest. This procedure provides a simple conﬁrmation of connection integrity.

   > 过程提供连接完整性的简单确认。
   >
2. Reject. Because HSMS is intended to be extended to protocols other than just SECS-II (by means of subsidiary standards), it is possible that two entities can be connected (due to a conﬁguration error) which use incompatible subsidiary standards. Also, during initial implementation, incorrect message types may be sent, or they may be sent out of order due to software bugs. The reject procedure is used to indicate such an occurrence.

   > 因为 HSMS 的目的是扩展到不仅仅是 SECS-II (通过附属标准)的协议，所以有可能连接两个使用不兼容附属标准的实体(由于配置错误)。此外，在最初的实现过程中，可能会发送不正确的消息类型，或者由于软件错误而发送错误的消息类型。拒绝过程用于指示这种情况。
   >

### 5.1 HSMS Connection State Diagram

The HSMS state machine is illustrated in the diagram below. The behavior described in this diagram defines the basic requirements of HSMS: subsidiary standards may further extend these or other states.

> HSMS 状态机如下图所示。此图中描述的行为定义了 HSMS 的基本需求: 附属标准可以进一步扩展这些或其他状态。

![1685514398652](image/SEMIE37文档记录/1685514398652.png)

### 5.2 State Descriptions

5.2.1   NOT CONNECTED — The entity is ready to listen for or initiate TCP/IP connections but either has not yet established any connections or all previously established TCP/IP connections have been terminated.

5.2.2 CONNECTED — A TCP/IP connection has been established. This state has two substates, NOT SELECTED and SELECTED.

5.2.2.1   NOT SELECTED — A substate of CONNECTED in which no HSMS session has been established or any previously established HSMS session has ended.

5.2.2.2   SELECTED — A substate of CONNECTED in which at least one HSMS session has been established. This is the normal "operating" state of HSMS: data messages may be exchanged in this state. It is highlighted by a heavy outline in the state diagram.
