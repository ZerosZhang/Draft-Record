# EPJ文件解析

## Events

### COMMON EVENTS

| id | name | description | wellknownname | list_of_data_variables |
|----|----|----|----|----|
|0|ControlStateLocal|Control State Machine switched to local (operator) control.|ControlStateLocal|
|1|ControlStateRemote|Control State Machine switched to remote (host) control.|ControlStateRemote||
|2|EquipmentOffline|Control State Machine switched to the offline state.|EquipmentOffline||
|3|MaterialReceived|Material arrived to a port on the equipment.|MaterialReceived||
|4|MaterialRemoved|Material departed from a port on the equipment.|MaterialRemoved||
|5|MessageRecognition|Machine operator recognized a terminal service message from the host. Triggered when the client application calls RecognizeTerminalMsg().|MessageRecognition||
|6|OperatorCommandIssued|Machine operator issued a control command.|OperatorCommandIssued|L {A OperatorCommand}|
|7|PPChange|A process program (recipe) has been created changed or deleted.|PPChange|L {A PPChangeName} {A PPChangeStatus}|
|8|PPSelected|A new process program (recipe) has been accepted. Either the host or machine operator has selected the recipe.|PPSelected||
|9|ProcessingCompleted|Normal exit of the EXECUTING state as part of the Processing State Machine.|ProcessingCompleted||
|10|ProcessingStarted|Normal entry of the EXECUTING state as part of the Processing State Machine.|ProcessingStarted||
|11|ProcessingStateChange|The state of the Processing State Machine has changed.|ProcessingStateChange||
|12|ProcessingStopped|A previously requested STOP command has been performed.|ProcessingStopped||
|13|SpoolTransmitFailure|A communication failure has occurred while in the TRANSMIT SPOOL state.|SpoolTransmitFailure||
|14|SpoolingActivated|Spooling State Machine has entered the SPOOL ACTIVE state.|SpoolingActivated||
|15|SpoolingDeactivated|Spooling State Machine has entered the SPOOL INACTIVE state.|SpoolingDeactivated||
|16|ECChange|An equipment constant value was changed locally by the operator.|ECChange|L {A ECID} {A ECChangeName} {A ECChangeValue} {A ECPreviousValue}|
|17|TraceTimestampOutOfTolerance|Trace time tolerance set by TraceTimestampTolerance is exceeded|TraceTimestampOutOfTolerance|
|18|HostCommandAccepted|Another host remote command was performed.|HostCommandAccepted|L {A HostCmdName} {A HostCmdHostID}|
|19|HostECChange|Another host changed an equipment constant(EC) value.|HostECChange|L {A HostECID} {A HostECHostID}|
|20|HostPPChange|Another host created deleted or overwrote a Process Program.|HostPPChange|L {A HostPPChangeName} {A HostPPChangeStatus} {A HostPPChangeHostID}|
|21|PPVerificationFailed|Process program (recipe) verification failed.|||
