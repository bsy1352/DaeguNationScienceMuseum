using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiRobots.EthernetIP
{
    class cifXError
    {
        public const ulong CIFX_NO_ERROR = 0x00000000L;

        /*******************************************************************************
        * Generic Errors
        *******************************************************************************/
        //
        // MessageId: CIFX_INVALID_POINTER
        //
        // MessageText:
        //
        //  Invalid pointer (NULL) passed to driver
        //
        public const ulong CIFX_INVALID_POINTER = 0x800A0001L;

        //
        // MessageId: CIFX_INVALID_BOARD
        //
        // MessageText:
        //
        //  No board with the given name / index available
        //
        public const ulong CIFX_INVALID_BOARD = 0x800A0002L;

        //
        // MessageId: CIFX_INVALID_CHANNEL
        //
        // MessageText:
        //
        //  No channel with the given index available
        //
        public const ulong CIFX_INVALID_CHANNEL = 0x800A0003L;

        //
        // MessageId: CIFX_INVALID_HANDLE
        //
        // MessageText:
        //
        //  Invalid handle passed to driver
        //
        public const ulong CIFX_INVALID_HANDLE = 0x800A0004L;

        //
        // MessageId: CIFX_INVALID_PARAMETER
        //
        // MessageText:
        //
        //  Invalid parameter
        //
        public const ulong CIFX_INVALID_PARAMETER = 0x800A0005L;

        //
        // MessageId: CIFX_INVALID_COMMAND
        //
        // MessageText:
        //
        //  Invalid command
        //
        public const ulong CIFX_INVALID_COMMAND = 0x800A0006L;

        //
        // MessageId: CIFX_INVALID_BUFFERSIZE
        //
        // MessageText:
        //
        //  Invalid buffer size
        //
        public const ulong CIFX_INVALID_BUFFERSIZE = 0x800A0007L;

        //
        // MessageId: CIFX_INVALID_ACCESS_SIZE
        //
        // MessageText:
        //
        //  Invalid access size
        //
        public const ulong CIFX_INVALID_ACCESS_SIZE = 0x800A0008L;

        //
        // MessageId: CIFX_FUNCTION_FAILED
        //
        // MessageText:
        //
        //  Function failed
        //
        public const ulong CIFX_FUNCTION_FAILED = 0x800A0009L;

        //
        // MessageId: CIFX_FILE_OPEN_FAILED
        //
        // MessageText:
        //
        //  File could not be opened
        //
        public const ulong CIFX_FILE_OPEN_FAILED = 0x800A000AL;

        //
        // MessageId: CIFX_FILE_SIZE_ZERO
        //
        // MessageText:
        //
        //  File size is zero
        //
        public const ulong CIFX_FILE_SIZE_ZERO = 0x800A000BL;

        //
        // MessageId: CIFX_FILE_LOAD_INSUFF_MEM
        //
        // MessageText:
        //
        //  Insufficient memory to load file
        //
        public const ulong CIFX_FILE_LOAD_INSUFF_MEM = 0x800A000CL;

        //
        // MessageId: CIFX_FILE_CHECKSUM_ERROR
        //
        // MessageText:
        //
        //  File checksum compare failed
        //
        public const ulong CIFX_FILE_CHECKSUM_ERROR = 0x800A000DL;

        //
        // MessageId: CIFX_FILE_READ_ERROR
        //
        // MessageText:
        //
        //  Error reading from file
        //
        public const ulong CIFX_FILE_READ_ERROR = 0x800A000EL;

        //
        // MessageId: CIFX_FILE_TYPE_INVALID
        //
        // MessageText:
        //
        //  Invalid file type
        //
        public const ulong CIFX_FILE_TYPE_INVALID = 0x800A000FL;

        //
        // MessageId: CIFX_FILE_NAME_INVALID
        //
        // MessageText:
        //
        //  Invalid file name
        //
        public const ulong CIFX_FILE_NAME_INVALID = 0x800A0010L;

        //
        // MessageId: CIFX_FUNCTION_NOT_AVAILABLE
        //
        // MessageText:
        //
        //  Driver function not available
        //
        public const ulong CIFX_FUNCTION_NOT_AVAILABLE = 0x800A0011L;

        //
        // MessageId: CIFX_BUFFER_TOO_SHORT
        //
        // MessageText:
        //
        //  Given buffer is too short
        //
        public const ulong CIFX_BUFFER_TOO_SHORT = 0x800A0012L;

        //
        // MessageId: CIFX_MEMORY_MAPPING_FAILED
        //
        // MessageText:
        //
        //  Failed to map the memory
        //
        public const ulong CIFX_MEMORY_MAPPING_FAILED = 0x800A0013L;

        //
        // MessageId: CIFX_NO_MORE_ENTRIES
        //
        // MessageText:
        //
        //  No more entries available
        //
        public const ulong CIFX_NO_MORE_ENTRIES = 0x800A0014L;

        /*******************************************************************************
        * Generic Driver Errors
        *******************************************************************************/
        //
        // MessageId: CIFX_DRV_NOT_INITIALIZED
        //
        // MessageText:
        //
        //  Driver not initialized
        //
        public const ulong CIFX_DRV_NOT_INITIALIZED = 0x800B0001L;

        //
        // MessageId: CIFX_DRV_INIT_STATE_ERROR
        //
        // MessageText:
        //
        //  Driver init state error
        //
        public const ulong CIFX_DRV_INIT_STATE_ERROR = 0x800B0002L;

        //
        // MessageId: CIFX_DRV_READ_STATE_ERROR
        //
        // MessageText:
        //
        //  Driver read state error
        //
        public const ulong CIFX_DRV_READ_STATE_ERROR = 0x800B0003L;

        //
        // MessageId: CIFX_DRV_CMD_ACTIVE
        //
        // MessageText:
        //
        //  Command is active on device
        //
        public const ulong CIFX_DRV_CMD_ACTIVE = 0x800B0004L;

        //
        // MessageId: CIFX_DRV_DOWNLOAD_FAILED
        //
        // MessageText:
        //
        //  General error during download
        //
        public const ulong CIFX_DRV_DOWNLOAD_FAILED = 0x800B0005L;

        //
        // MessageId: CIFX_DRV_WRONG_DRIVER_VERSION
        //
        // MessageText:
        //
        //  Wrong driver version
        //
        public const ulong CIFX_DRV_WRONG_DRIVER_VERSION = 0x800B0006L;

        //
        // MessageId: CIFX_DRV_DRIVER_NOT_LOADED
        //
        // MessageText:
        //
        //  CIFx driver is not running
        //
        public const ulong CIFX_DRV_DRIVER_NOT_LOADED = 0x800B0030L;

        //
        // MessageId: CIFX_DRV_INIT_ERROR
        //
        // MessageText:
        //
        //  Failed to initialize the device
        //
        public const ulong CIFX_DRV_INIT_ERROR = 0x800B0031L;

        //
        // MessageId: CIFX_DRV_CHANNEL_NOT_INITIALIZED
        //
        // MessageText:
        //
        //  Channel not initialized (xOpenChannel not called)
        //
        public const ulong CIFX_DRV_CHANNEL_NOT_INITIALIZED = 0x800B0032L;

        //
        // MessageId: CIFX_DRV_IO_CONTROL_FAILED
        //
        // MessageText:
        //
        //  IOControl call failed
        //
        public const ulong CIFX_DRV_IO_CONTROL_FAILED = 0x800B0033L;

        //
        // MessageId: CIFX_DRV_NOT_OPENED
        //
        // MessageText:
        //
        //  Driver was not opened
        //
        public const ulong CIFX_DRV_NOT_OPENED = 0x800B0034L;

        /*******************************************************************************
        * Generic Device Errors
        *******************************************************************************/
        //
        // MessageId: CIFX_DEV_DPM_ACCESS_ERROR
        //
        // MessageText:
        //
        //  Dual port memory not accessable (board not found)
        //
        public const ulong CIFX_DEV_DPM_ACCESS_ERROR = 0x800C0010L;

        //
        // MessageId: CIFX_DEV_NOT_READY
        //
        // MessageText:
        //
        //  Device not ready (ready flag failed)
        //
        public const ulong CIFX_DEV_NOT_READY = 0x800C0011L;

        //
        // MessageId: CIFX_DEV_NOT_RUNNING
        //
        // MessageText:
        //
        //  Device not running (running flag failed)
        //
        public const ulong CIFX_DEV_NOT_RUNNING = 0x800C0012L;

        //
        // MessageId: CIFX_DEV_WATCHDOG_FAILED
        //
        // MessageText:
        //
        //  Watchdog test failed
        //
        public const ulong CIFX_DEV_WATCHDOG_FAILED = 0x800C0013L;

        //
        // MessageId: CIFX_DEV_SYSERR
        //
        // MessageText:
        //
        //  Error in handshake flags
        //
        public const ulong CIFX_DEV_SYSERR = 0x800C0015L;

        //
        // MessageId: CIFX_DEV_MAILBOX_FULL
        //
        // MessageText:
        //
        //  Send mailbox is full
        //
        public const ulong CIFX_DEV_MAILBOX_FULL = 0x800C0016L;

        //
        // MessageId: CIFX_DEV_PUT_TIMEOUT
        //
        // MessageText:
        //
        //  Send packet timeout
        //
        public const ulong CIFX_DEV_PUT_TIMEOUT = 0x800C0017L;

        //
        // MessageId: CIFX_DEV_GET_TIMEOUT
        //
        // MessageText:
        //
        //  Receive packet timeout
        //
        public const ulong CIFX_DEV_GET_TIMEOUT = 0x800C0018L;

        //
        // MessageId: CIFX_DEV_GET_NO_PACKET
        //
        // MessageText:
        //
        //  No packet available
        //
        public const ulong CIFX_DEV_GET_NO_PACKET = 0x800C0019L;

        //
        // MessageId: CIFX_DEV_MAILBOX_TOO_SHORT
        //
        // MessageText:
        //
        //  Mailbox too short
        //
        public const ulong CIFX_DEV_MAILBOX_TOO_SHORT = 0x800C001AL;

        //
        // MessageId: CIFX_DEV_RESET_TIMEOUT
        //
        // MessageText:
        //
        //  Reset command timeout
        //
        public const ulong CIFX_DEV_RESET_TIMEOUT = 0x800C0020L;

        //
        // MessageId: CIFX_DEV_NO_COM_FLAG
        //
        // MessageText:
        //
        //  COM-flag not set
        //
        public const ulong CIFX_DEV_NO_COM_FLAG = 0x800C0021L;

        //
        // MessageId: CIFX_DEV_EXCHANGE_FAILED
        //
        // MessageText:
        //
        //  I/O data exchange failed
        //
        public const ulong CIFX_DEV_EXCHANGE_FAILED = 0x800C0022L;

        //
        // MessageId: CIFX_DEV_EXCHANGE_TIMEOUT
        //
        // MessageText:
        //
        //  I/O data exchange timeout
        //
        public const ulong CIFX_DEV_EXCHANGE_TIMEOUT = 0x800C0023L;

        //
        // MessageId: CIFX_DEV_COM_MODE_UNKNOWN
        //
        // MessageText:
        //
        //  Unknown I/O exchange mode 
        //
        public const ulong CIFX_DEV_COM_MODE_UNKNOWN = 0x800C0024L;

        //
        // MessageId: CIFX_DEV_FUNCTION_FAILED
        //
        // MessageText:
        //
        //  Device function failed
        //
        public const ulong CIFX_DEV_FUNCTION_FAILED = 0x800C0025L;

        //
        // MessageId: CIFX_DEV_DPMSIZE_MISMATCH
        //
        // MessageText:
        //
        //  DPM size differs from configuration
        //
        public const ulong CIFX_DEV_DPMSIZE_MISMATCH = 0x800C0026L;

        //
        // MessageId: CIFX_DEV_STATE_MODE_UNKNOWN
        //
        // MessageText:
        //
        //  Unknown state mode
        //
        public const ulong CIFX_DEV_STATE_MODE_UNKNOWN = 0x800C0027L;

        //
        // MessageId: CIFX_DEV_HW_PORT_IS_USED
        //
        // MessageText:
        //
        //  Output port already in use
        //
        public const ulong CIFX_DEV_HW_PORT_IS_USED = 0x800C0028L;

        //
        // MessageId: CIFX_DEV_CONFIG_LOCK_TIMEOUT
        //
        // MessageText:
        //
        //  Configuration locking timeout
        //
        public const ulong CIFX_DEV_CONFIG_LOCK_TIMEOUT = 0x800C0029L;

        //
        // MessageId: CIFX_DEV_CONFIG_UNLOCK_TIMEOUT
        //
        // MessageText:
        //
        //  Configuration unlocking timeout
        //
        public const ulong CIFX_DEV_CONFIG_UNLOCK_TIMEOUT = 0x800C002AL;

        //
        // MessageId: CIFX_DEV_HOST_STATE_SET_TIMEOUT
        //
        // MessageText:
        //
        //  Set HOST state timeout
        //
        public const ulong CIFX_DEV_HOST_STATE_SET_TIMEOUT = 0x800C002BL;

        //
        // MessageId: CIFX_DEV_HOST_STATE_CLEAR_TIMEOUT
        //
        // MessageText:
        //
        //  Clear HOST state timeout
        //
        public const ulong CIFX_DEV_HOST_STATE_CLEAR_TIMEOUT = 0x800C002CL;

        //
        // MessageId: CIFX_DEV_INITIALIZATION_TIMEOUT
        //
        // MessageText:
        //
        //  Timeout during channel initialization
        //
        public const ulong CIFX_DEV_INITIALIZATION_TIMEOUT = 0x800C002DL;

        //
        // MessageId: CIFX_DEV_BUS_STATE_ON_TIMEOUT
        //
        // MessageText:
        //
        //  Set Bus ON Timeout
        //
        public const ulong CIFX_DEV_BUS_STATE_ON_TIMEOUT = 0x800C002EL;

        //
        // MessageId: CIFX_DEV_BUS_STATE_OFF_TIMEOUT
        //
        // MessageText:
        //
        //  Set Bus OFF Timeout
        //
        public const ulong CIFX_DEV_BUS_STATE_OFF_TIMEOUT = 0x800C002FL;
    }
}
