using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MultiRobots.EthernetIP
{
    public class cifXUser
    {
        /* ------------------------------------------------------------------------------------ */
        /*  Properties                                                                  */
        /* ------------------------------------------------------------------------------------ */
        #region properties
        private static UInt32 _hDriver;
        public static UInt32 hDriver
        { get { return _hDriver; } }

        private static UInt32 _hSysdevice;
        public static UInt32 hSysDevice
        { get { return _hSysdevice; } }

        private static UInt32 _hChannel;
        public static UInt32 hChannel
        { get { return _hChannel; } set { _hChannel = value; } }

        private static string _ActiveBoard;
        public static string ActiveBoard
        { get { return _ActiveBoard; } set { _ActiveBoard = value; } }

        private static int _ActivChannel;
        public static int ActiveChannel
        { get { return _ActivChannel; } set { _ActivChannel = value; } }

        private static DRIVER_INFORMATIONtag _DriverInformation;
        public static DRIVER_INFORMATIONtag DriverInformation
        { get { return _DriverInformation; } }

        private static CIFX_DIRECTORYENTRYtag _CifXDirectoryEntry;
        public static CIFX_DIRECTORYENTRYtag CifXdirectoryEntry
        { get { return _CifXDirectoryEntry; } }

        private static SYSTEM_CHANNEL_INFORMATIONtag _SystemChannelInformation;
        public static SYSTEM_CHANNEL_INFORMATIONtag SystemChannelInformation
        { get { return _SystemChannelInformation; } }

        private static SYSTEM_CHANNEL_INFO_BLOCKtag _SystemChannelInfoBlock;
        public static SYSTEM_CHANNEL_INFO_BLOCKtag SystemchannelSystemInfoBlock
        { get { return _SystemChannelInfoBlock; } }

        private static SYSTEM_CHANNEL_SYSTEM_CONTROL_BLOCKtag _SystemChannelSystemControlBlock;
        public static SYSTEM_CHANNEL_SYSTEM_CONTROL_BLOCKtag SystemChannelSystemControlBlock
        { get { return _SystemChannelSystemControlBlock; } }

        private static SYSTEM_CHANNEL_SYSTEM_STATUS_BLOCKtag _SystemChannelSystemStatusBlock;
        public static SYSTEM_CHANNEL_SYSTEM_STATUS_BLOCKtag SystemChannelSystemStatusBlock
        { get { return _SystemChannelSystemStatusBlock; } }

        private static BOARD_INFORMATIONtag _BoardInformation;
        public static BOARD_INFORMATIONtag BoardInformation
        { get { return _BoardInformation; } }

        private static CHANNEL_INFORMATIONtag _ChannelInformation;
        public static CHANNEL_INFORMATIONtag ChannelInformation
        { get { return _ChannelInformation; } }

        private static CHANNEL_IO_INFORMATIONtag _ChannelIOInformation;
        public static CHANNEL_IO_INFORMATIONtag ChannelIOInformation
        { get { return _ChannelIOInformation; } }

        #endregion
        /* ------------------------------------------------------------------------------------ */
        /*  global definitions                                                                  */
        /* ------------------------------------------------------------------------------------ */
        #region global definitions
        public UInt32 CIFXHANDLE;
        public UInt32 CHANNELHANDLE;

        /* DPM memory validation */
        public ulong CIFX_DPM_NO_MEMORY_ASSIGNED = 0x0BAD0BADUL;
        public ulong CIFX_DPM_INVALID_CONTENT = 0xFFFFFFFFUL;

        /* CIFx global timeouts in milliseconds */
        public ulong CIFX_TO_WAIT_HW_RESET_ACTIVE = 2000UL;
        public ulong CIFX_TO_WAIT_HW = 2000UL;
        public ulong CIFX_TO_WAIT_COS_CMD = 10UL;
        public ulong CIFX_TO_WAIT_COS_ACK = 10UL;
        public ulong CIFX_TO_SEND_PACKET = 5000UL;
        public ulong CIFX_TO_1ST_PACKET = 1000UL;
        public ulong CIFX_TO_CONT_PACKET = 1000UL;
        public ulong CIFX_TO_LAST_PACKET = 1000UL;
        public ulong CIFX_TO_FIRMWARE_START = 10000UL;

        /* Maximum channel number */
        public const int CIFX_MAX_NUMBER_OF_CHANNEL_DEFINITION = 8;
        public const UInt32 CIFX_MAX_NUMBER_OF_CHANNELS = 6;
        public const UInt32 CIFX_NO_CHANNEL = 0xFFFFFFFF;

        /* Maximum file name length */
        public UInt32 CIFX_MAX_FILE_NAME_LENGTH = 260;
        public UInt32 CIFX_MIN_FILE_NAME_LENGTH = 5;

        /* The system device port number */
        public UInt32 CIFX_SYSTEM_DEVICE = 0xFFFFFFFF;

        /* Information commands */
        public const UInt32 CIFX_INFO_CMD_SYSTEM_INFORMATION = 1;
        public const UInt32 CIFX_INFO_CMD_SYSTEM_INFO_BLOCK = 2;
        public const UInt32 CIFX_INFO_CMD_SYSTEM_CHANNEL_BLOCK = 3;
        public const UInt32 CIFX_INFO_CMD_SYSTEM_CONTROL_BLOCK = 4;
        public const UInt32 CIFX_INFO_CMD_SYSTEM_STATUS_BLOCK = 5;

        /* General commands */
        public UInt32 CIFX_CMD_READ_DATA = 1;
        public UInt32 CIFX_CMD_WRITE_DATA = 2;

        /* HOST mode definition */
        public UInt32 CIFX_HOST_STATE_NOT_READY = 0;
        public UInt32 CIFX_HOST_STATE_READY = 1;
        public UInt32 CIFX_HOST_STATE_READ = 2;

        /* WATCHDOG commands*/
        public UInt32 CIFX_WATCHDOG_STOP = 0;
        public UInt32 CIFX_WATCHDOG_START = 1;

        /* Configuration Lock commands*/
        public UInt32 CIFX_CONFIGURATION_UNLOCK = 0;
        public UInt32 CIFX_CONFIGURATION_LOCK = 1;
        public UInt32 CIFX_CONFIGURATION_GETLOCKSTATE = 2;

        /* BUS state commands*/
        public UInt32 CIFX_BUS_STATE_OFF = 0;
        public UInt32 CIFX_BUS_STATE_ON = 1;
        public UInt32 CIFX_BUS_STATE_GETSTATE = 2;

        /* Memory pointer commands*/
        public UInt32 CIFX_MEM_PTR_OPEN = 1;
        public UInt32 CIFX_MEM_PTR_OPEN_USR = 2;
        public UInt32 CIFX_MEM_PTR_CLOSE = 3;

        /* I/O area definition */
        public UInt32 CIFX_IO_INPUT_AREA = 1;
        public UInt32 CIFX_IO_OUTPUT_AREA = 2;

        /* Reset definitions */
        public UInt32 CIFX_SYSTEMSTART = 1;
        public UInt32 CIFX_CHANNELINIT = 2;
        #endregion

        /*****************************************************************************/
        /*! Structure definitions                                                    */
        /*****************************************************************************/
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct DRIVER_INFORMATIONtag
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
            public byte[] abDriverVersion;                              /*!< Driver version                 */
            public UInt32 ulBoardCnt;                                   /*!< Number of available Boards     */
        }

        public const int CIFx_MAX_INFO_NAME_LENTH = 16;

        /*****************************************************************************/
        /*! Directory Information structure for enumerating directories              */
        /*****************************************************************************/
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CIFX_DIRECTORYENTRYtag
        {
            public UInt32 hList;                                /*!< Handle from Enumeration function, do not touch */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CIFx_MAX_INFO_NAME_LENTH)]
            public byte[] szFilename;                           /*!< Returned file name. */
            public byte bFiletype;                            /*!< Returned file type. */
            public UInt32 ulFilesize;                           /*!< Returned file size. */
        }

        /*****************************************************************************/
        /*! System Channel Information structure                                     */
        /*****************************************************************************/
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SYSTEM_CHANNEL_INFORMATIONtag
        {
            public UInt32 ulSystemError;                                /*!< Global system error            */
            public UInt32 ulDpmTotalSize;                               /*!< Total size dual-port memory in bytes */
            public UInt32 ulMBXSize;                                    /*!< System mailbox data size [Byte]*/
            public UInt32 ulDeviceNumber;                               /*!< Global device number           */
            public UInt32 ulSerialNumber;                               /*!< Global serial number           */
            public UInt32 ulOpenCnt;                                    /*!< Channel open counter           */
        }

        /* System Channel: System Information Block */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SYSTEM_CHANNEL_INFO_BLOCKtag
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] abCookie;                                      /*!< 0x00 "netX" cookie */
            public UInt32 ulDpmTotalSize;                                /*!< 0x04 Total Size of the whole dual-port memory in bytes */
            public UInt32 ulDeviceNumber;                                /*!< 0x08 Device number */
            public UInt32 ulSerialNumber;                                /*!< 0x0C Serial number */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public UInt16[] ausHwOptions;                                  /*!< 0x10 Hardware options, xC port 0..3 */
            public UInt16 usManufacturer;                                /*!< 0x18 Manufacturer Location */
            public UInt16 usProductionDate;                              /*!< 0x1A Date of production */
            public UInt32 ulLicenseFlags1;                               /*!< 0x1C License code flags 1 */
            public UInt32 ulLicenseFlags2;                               /*!< 0x20 License code flags 2 */
            public UInt16 usNetxLicenseID;                               /*!< 0x24 netX license identification */
            public UInt16 usNetxLicenseFlags;                            /*!< 0x26 netX license flags */
            public UInt16 usDeviceClass;                                 /*!< 0x28 netX device class */
            public byte bHwRevision;                                   /*!< 0x2A Hardware revision index */
            public byte bHwCompatibility;                              /*!< 0x2B Hardware compatibility index */
            public byte bDevIdNumber;                                  /*!< Device Identification number (rotary switch) */
            public byte bReserved;                                     /*!< unused/reserved */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public UInt16[] ausReserved;                                   /*!< 0x2C:0x2F Reserved */
        }

        /* System Channel: Channel Information Block */
        public const int CIFX_SYSTEM_CHANNEL_DEFAULT_INFO_BLOCK_SIZE = 16;
        public byte[,] abInfoBlock = new byte[CIFX_MAX_NUMBER_OF_CHANNEL_DEFINITION, CIFX_SYSTEM_CHANNEL_DEFAULT_INFO_BLOCK_SIZE];


        /* System Channel: System Control Block */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SYSTEM_CHANNEL_SYSTEM_CONTROL_BLOCKtag
        {
            public UInt32 ulSystemCommandCOS;                            /*!< 0x00 System channel change of state command */
            public UInt32 ulReserved;                                    /*!< 0x04 Reserved */
        }

        /* System Channel: System Status Block */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SYSTEM_CHANNEL_SYSTEM_STATUS_BLOCKtag
        {
            public UInt32 ulSystemCOS;                                   /*!< 0x00 System channel change of state acknowledge */
            public UInt32 ulSystemStatus;                                /*!< 0x04 Actual system state */
            public UInt32 ulSystemError;                                 /*!< 0x08 Actual system error */
            public UInt32 ulReserved1;                                   /*!< 0x0C reserved */
            public UInt32 ulTimeSinceStart;                              /*!< 0x10 time since start in seconds */
            public UInt16 usCpuLoad;                                     /*!< 0x14 cpu load in 0,01% units (10000 => 100%) */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 42)]
            public byte[] abReserved;                                    /*!< 0x16:3F Reserved */
        }

        /*****************************************************************************/
        /*! Board Information structure                                              */
        /*****************************************************************************/
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BOARD_INFORMATIONtag
        {
            public UInt32 lBoardError;                                /*!< Global Board error. Set when device specific data must not be used */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CIFx_MAX_INFO_NAME_LENTH)]
            public byte[] abBoardName;                                /*!< Global board name              */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CIFx_MAX_INFO_NAME_LENTH)]
            public byte[] abBoardAlias;                               /*!< Global board alias name        */
            public UInt32 ulBoardID;                                  /*!< Unique board ID, driver created*/
            public UInt32 ulSystemError;                              /*!< System error                   */
            public UInt32 ulPhysicalAddress;                          /*!< Physical memory address        */
            public UInt32 ulIrqNumber;                                /*!< Hardware interrupt number      */
            public byte bIrqEnabled;                                /*!< Hardware interrupt enable flag */
            public UInt32 ulChannelCnt;                               /*!< Number of available channels   */
            public UInt32 ulDpmTotalSize;                             /*!< Dual-Port memory size in bytes */
            public SYSTEM_CHANNEL_INFO_BLOCKtag tSystemInfo;               /*!< System information             */
        }

        /*****************************************************************************/
        /*! Channel Information structure                                            */
        /*****************************************************************************/
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CHANNEL_INFORMATIONtag
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CIFx_MAX_INFO_NAME_LENTH)]
            public byte[] abBoardName;        /*!< Global board name              */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CIFx_MAX_INFO_NAME_LENTH)]
            public byte[] abBoardAlias;       /*!< Global board alias name        */
            public UInt32 ulDeviceNumber;                               /*!< Global board device number     */
            public UInt32 ulSerialNumber;                               /*!< Global board serial number     */

            public UInt16 usFWMajor;                                    /*!< Major Version of Channel Firmware  */
            public UInt16 usFWMinor;                                    /*!< Minor Version of Channel Firmware  */
            public UInt16 usFWBuild;                                    /*!< Build number of Channel Firmware   */
            public UInt16 usFWRevision;                                 /*!< Revision of Channel Firmware       */
            public byte bFWNameLength;                                /*!< Length  of FW Name                 */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 63)]
            public byte[] abFWName;                                 /*!< Firmware Name                      */
            public UInt16 usFWYear;                                     /*!< Build Year of Firmware             */
            public byte bFWMonth;                                     /*!< Build Month of Firmware (1..12)    */
            public byte bFWDay;                                       /*!< Build Day of Firmware (1..31)      */

            public UInt32 ulChannelError;                               /*!< Channel error                  */
            public UInt32 ulOpenCnt;                                    /*!< Channel open counter           */
            public UInt32 ulPutPacketCnt;                               /*!< Number of put packet commands  */
            public UInt32 ulGetPacketCnt;                               /*!< Number of get packet commands  */
            public UInt32 ulMailboxSize;                                /*!< Mailbox packet size in bytes   */
            public UInt32 ulIOInAreaCnt;                                /*!< Number of IO IN areas          */
            public UInt32 ulIOOutAreaCnt;                               /*!< Number of IO OUT areas         */
            public UInt32 ulHskSize;                                    /*!< Size of the handshake cells    */
            public UInt32 ulNetxFlags;                                  /*!< Actual netX state flags        */
            public UInt32 ulHostFlags;                                  /*!< Actual Host flags              */
            public UInt32 ulHostCOSFlags;                               /*!< Actual Host COS flags          */
            public UInt32 ulDeviceCOSFlags;                             /*!< Actual Device COS flags        */
        }

        /*****************************************************************************/
        /*! IO Area Information structure                                            */
        /*****************************************************************************/
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CHANNEL_IO_INFORMATIONtag
        {
            public UInt32 ulTotalSize;                  /*!< Total IO area size in byte */
            public UInt32 ulUsedSize;                   /*!< Used IO area size in byte */
            public UInt32 ulIOMode;                     /*!< Exchange mode */
        }

        /*****************************************************************************/
        /*! Memory Information structure                                             */
        /*****************************************************************************/

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public unsafe struct MEMORY_INFORMATION
        {
            [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)]
            public UInt32 pvMemoryID;                  /*!< Identification of the memory area      */
            public UInt32* ppvMemoryPtr;         /*!< Memory pointer                         */
            public UInt32* pulMemorySize;         /*!< Complete size of the mapped memory     */
            public UInt32 ulChannel;             /*!< Requested channel number               */
            public UInt32* pulChannelStartOffset; /*!< Start offset of the requested channel  */
            public UInt32* pulChannelSize;        /*!< Memory size of the requested channel   */
        }

        /*****************************************************************************/
        /*! PLC Memory Information structure                                         */
        /*****************************************************************************/
        /*
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PLC_MEMORY_INFORMATION
        {
          void*                 pvMemoryID;           /*!< Identification of the memory area      
          void**                ppvMemoryPtr;         /*!< Memory pointer                         
          unsigned long         ulAreaDefinition;     /*!< Input/output area                      
          unsigned long         ulAreaNumber;         /*!< Area number                            
          unsigned long*        pulIOAreaStartOffset; /*!< Start offset                           
          unsigned long*        pulIOAreaSize;        /*!< Memory size                            
        } __CIFx_PACKED_POST PLC_MEMORY_INFORMATION;
        */

        /***************************************************************************/
        /* Driver dependent information */

        public const int CIFX_MAX_PACKET_SIZE = 1600;                  /*!< Maximum size of the RCX packet in bytes */
        public const int CIFX_PACKET_HEADER_SIZE = 40;                    /*!< Maximum size of the RCX packet header in bytes */
        public const int CIFX_MAX_DATA_SIZE = CIFX_MAX_PACKET_SIZE - CIFX_PACKET_HEADER_SIZE; /*!< Maximum RCX packet data size */
        public UInt32 CIFX_MSK_PACKET_ANSWER = 0x00000001;            /*!< Packet answer bit */

        /*****************************************************************************/
        /*! Packet header                                                            */
        /*****************************************************************************/
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CIFX_PACKET_HEADERtag
        {
            public UInt32 ulDest;   /*!< destination of packet, process queue */
            public UInt32 ulSrc;    /*!< source of packet, process queue */
            public UInt32 ulDestId; /*!< destination reference of packet */
            public UInt32 ulSrcId;  /*!< source reference of packet */
            public UInt32 ulLen;    /*!< length of packet data without header */
            public UInt32 ulId;     /*!< identification handle of sender */
            public UInt32 ulState;  /*!< status code of operation */
            public UInt32 ulCmd;    /*!< packet command defined in TLR_Commands.h */
            public UInt32 ulExt;    /*!< extension */
            public UInt32 ulRout;   /*!< router */
        }

        /*****************************************************************************/
        /*! Definition of the rcX Packet                                             */
        /*****************************************************************************/
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct CIFX_PACKETtag
        {
            public CIFX_PACKET_HEADERtag tHeader;                   /**! */
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CIFX_MAX_DATA_SIZE)]
            public byte[] abData;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PFN_PROGRESS_CALLBACK
        {
            UInt32 ulStep;
            UInt32 ulMaxStep;
            UInt32 pvUser;
            char bFinished;
            long lError;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PFN_RECV_PKT_CALLBACK
        {
            [MarshalAs(UnmanagedType.U4)]
            CIFX_PACKETtag ptRecvPkt;
            UInt32 pvUser;
        }
        //typedef void(*PFN_PROGRESS_CALLBACK)(unsigned long ulStep, unsigned long ulMaxStep, void* pvUser, char bFinished, long lError);
        //typedef void(*PFN_RECV_PKT_CALLBACK)(CIFX_PACKET* ptRecvPkt, void* pvUser);

        public UInt32 CIFX_CALLBACK_ACTIVE = 0;
        public UInt32 CIFX_CALLBACK_FINISHED = 1;

        public UInt32 DOWNLOAD_MODE_FIRMWARE = 1;
        public UInt32 DOWNLOAD_MODE_CONFIG = 2;
        public UInt32 DOWNLOAD_MODE_FILE = 3;
        public UInt32 DOWNLOAD_MODE_BOOTLOADER = 4; /*!< Download bootloader update to target. */
        public UInt32 DOWNLOAD_MODE_LICENSECODE = 5; /*!< License update code.                  */


        /***************************************************************************
        * API Functions
        ***************************************************************************/

        /* Global driver functions */
        [DllImport("cifx32dll.dll", EntryPoint = "xDriverOpen")]
        private static extern UInt32 _xDriverOpen([  MarshalAs(UnmanagedType.U4)]
                                                     ref UInt32 CIFXHANDLE);

        [DllImport("cifx32dll.dll", EntryPoint = "xDriverClose")]
        private static extern UInt32 _xDriverClose(UInt32 CIFXHANDLE);

        [DllImport("cifx32dll.dll", EntryPoint = "xDriverGetInformation")]
        private static extern UInt32 _xDriverGetInformation(UInt32 CIFXHANDLE,
                                                                UInt32 ulSize,
                                                                ref DRIVER_INFORMATIONtag pvDriverInfo);

        [DllImport("cifx32dll.dll", EntryPoint = "xDriverGetErrorDescription")]
        private static extern UInt32 _xDriverGetErrorDescription(UInt32 lError,
                                                                    [Out, MarshalAs(UnmanagedType.LPArray)] byte[] szBuffer,
                                                                    UInt32 ulBufferLen);

        [DllImport("cifx32dll.dll", EntryPoint = "xDriverEnumBoards")]
        private static extern UInt32 _xDriverEnumBoards(UInt32 CIFXHANDLE,
                                                            UInt32 ulBoard,
                                                            UInt32 ulSize,
                                                            ref BOARD_INFORMATIONtag pvBoardInfo);

        [DllImport("cifx32dll.dll", EntryPoint = "xDriverEnumChannels")]
        private static extern UInt32 _xDriverEnumChannels(UInt32 CIFXHANDLE,
                                                            UInt32 ulBoard,
                                                            UInt32 ulChannel,
                                                            UInt32 ulSize,
                                                            ref CHANNEL_INFORMATIONtag pvChannelInfo);
        [DllImport("cifx32dll.dll", EntryPoint = "xDriverMemoryPointer")]
        private static extern UInt32 _xDriverMemoryPointer(UInt32 hDriver,
                                                            UInt32 ulBoard,
                                                            UInt32 ulCmd,
                                                            ref MEMORY_INFORMATION pvMemoryInfo);

        [DllImport("cifx32dll.dll", EntryPoint = "xDriverRestartDevice")]
        private static extern UInt32 _xDriverRestartDevice(UInt32 hDriver,
                                                            [MarshalAs(UnmanagedType.LPStr)]ref string szBoardName,
                                                            string pvData);

        /* System device depending functions*/
        [DllImport("cifx32dll.dll", EntryPoint = "xSysdeviceOpen")]
        private static extern UInt32 _xSysdeviceOpen(UInt32 hDriver,
                                                        string szBoard,
                                                        [MarshalAs(UnmanagedType.U4)] ref UInt32 phSysdevice);

        [DllImport("cifx32dll.dll", EntryPoint = "xSysdeviceClose")]
        private static extern UInt32 _xSysdeviceClose(UInt32 hSysdevice);

        [DllImport("cifx32dll.dll", EntryPoint = "xSysdeviceGetMBXState")]
        private static extern UInt32 _xSysdeviceGetMBXState(UInt32 hSysdevice,
                                                      ref UInt32 pulRecvPktCount,
                                                      ref UInt32 pulSendPktCount);

        [DllImport("cifx32dll.dll", EntryPoint = "xSysdevicePutPacket")]
        private static extern UInt32 _xSysdevicePutPacket(UInt32 hSysdevice,
                                                           ref CIFX_PACKETtag ptSendPkt,
                                                           UInt32 ulTimeout);

        [DllImport("cifx32dll.dll", EntryPoint = "xSysdeviceGetPacket")]
        private static extern UInt32 _xSysdeviceGetPacket(UInt32 hSysdevice,
                                                          UInt32 ulSize,
                                                          ref CIFX_PACKETtag ptRecvPkt,
                                                          UInt32 ulTimeout);

        [DllImport("cifx32dll.dll", EntryPoint = "xSysdeviceInfo")]
        private static extern UInt32 _xSysdeviceInfo(UInt32 hSysdevice,
                                                      UInt32 ulCmd,
                                                      UInt32 ulSize,
                                                      [Out, MarshalAs(UnmanagedType.LPArray)] byte[] pvzData);

        [DllImport("cifx32dll.dll", EntryPoint = "xSysdeviceFindFirstFile")]
        private static extern UInt32 _xSysdeviceFindFirstFile(UInt32 hSysdevice,
                                                        UInt32 ulChannel,
                                                        ref CIFX_DIRECTORYENTRYtag ptDirectoryInfo,
                                                        string PFN1,
                                                        ref UInt32 pvUser);

        [DllImport("cifx32dll.dll", EntryPoint = "xSysdeviceFindNextFile")]
        private static extern UInt32 _xSysdeviceFindNextFile(UInt32 hSysdevice,
                                                              UInt32 ulChannel,
                                                              ref CIFX_DIRECTORYENTRYtag ptDirectoryInfo,
                                                              string PFN1,
                                                              ref UInt32 pvUser);

        [DllImport("cifx32dll.dll", EntryPoint = "xSysdeviceDownload")]
        private static extern UInt32 _xSysdeviceDownload(UInt32 hSysdevice,
                                                            UInt32 ulChannel,
                                                            UInt32 ulMode,
                                                           [MarshalAs(UnmanagedType.LPStr)] string pszFileName,
                                                            byte[] pabFileData,
                                                            UInt32 ulFileSize,
                                                            string PFN1,
                                                            string PFN2,
                                                            ref UInt32 pvUser);

        [DllImport("cifx32dll.dll", EntryPoint = "xSysdeviceUpload")]
        private static extern UInt32 _xSysdeviceUpload(UInt32 hSysdevice,
                                                            UInt32 ulChannel,
                                                            UInt32 ulMode,
                                                           [MarshalAs(UnmanagedType.LPStr)] string pszFileName,
                                                            Byte[] pabFileData,
                                                            UInt32 pulFileSize,
                                                            string PFN1,
                                                            string PFN2,
                                                            ref UInt32 pvUser);

        [DllImport("cifx32dll.dll", EntryPoint = "xSysdeviceReset")]
        private static extern UInt32 _xSysdeviceReset(UInt32 hSysdevice, UInt32 ulTimeout);

        /* Channel depending functions */
        [DllImport("cifx32dll.dll", EntryPoint = "xChannelOpen")]
        private static extern UInt32 _xChannelOpen(UInt32 CIFXHANDLE,
                                                           [MarshalAs(UnmanagedType.LPStr)] string szBoard,
                                                            UInt32 ulChannel,
                                                           [MarshalAs(UnmanagedType.U4)]ref UInt32 phChannel);

        [DllImport("cifx32dll.dll", EntryPoint = "xChannelClose")]
        private static extern UInt32 _xChannelClose(UInt32 hChannel);

        [DllImport("cifx32dll.dll", EntryPoint = "xChannelFindFirstFile")]
        private static extern UInt32 _xChannelFindFirstFile(UInt32 hChannel,
                                                            ref CIFX_DIRECTORYENTRYtag ptDirectoryInfo,
                                                            string PFN1,
                                                            ref UInt32 pvUser);

        [DllImport("cifx32dll.dll", EntryPoint = "xChannelFindNextFile")]
        private static extern UInt32 _xChannelFindNextFile(UInt32 hChannel,
                                                            ref CIFX_DIRECTORYENTRYtag ptDirectoryInfo,
                                                            string PFN1,
                                                            ref UInt32 pvUser);

        [DllImport("cifx32dll.dll", EntryPoint = "xChannelDownload")]
        private static extern UInt32 _xChannelDownload(UInt32 hChannel,
                                                            UInt32 ulMode,
                                                            [MarshalAs(UnmanagedType.LPStr)] string sFileName,
                                                            Byte[] pabFileData,
                                                            UInt32 ulFileSize,
                                                            string PFN1,
                                                            string PFN2,
                                                            ref UInt32 pvUser);

        [DllImport("cifx32dll.dll", EntryPoint = "xChannelUpload")]
        private static extern UInt32 _xChannelUpload(UInt32 hChannel,
                                                            UInt32 ulMode,
                                                           [MarshalAs(UnmanagedType.LPStr)] string pszFileName,
                                                            Byte[] pabFileData,
                                                            UInt32 pulFileSize,
                                                            string PFN1,
                                                            string PFN2,
                                                            ref UInt32 pvUser);

        [DllImport("cifx32dll.dll", EntryPoint = "xChannelGetMBXState")]
        private static extern UInt32 _xChannelGetMBXState(UInt32 hChannel,
                                                           ref UInt32 pulRecvPktCount,
                                                           ref UInt32 pulSendPktCount);

        [DllImport("cifx32dll.dll", EntryPoint = "xChannelPutPacket")]
        private static extern UInt32 _xChannelPutPacket(UInt32 hChannel,
                                                           CIFX_PACKETtag ptSendPkt,
                                                           UInt32 ulTimeout);

        [DllImport("cifx32dll.dll", EntryPoint = "xChannelGetPacket")]
        private static extern UInt32 _xChannelGetPacket(UInt32 hChannel,
                                                           UInt32 ulSize,
                                                           ref CIFX_PACKETtag ptRecvPkt,
                                                           UInt32 ulTimeout);

        [DllImport("cifx32dll.dll", EntryPoint = "xChannelGetSendPacket")]
        private static extern UInt32 _xChannelGetSendPacket(UInt32 hChannel,
                                                            UInt32 ulSize,
                                                            ref CIFX_PACKETtag ptRecvPkt);

        [DllImport("cifx32dll.dll", EntryPoint = "xChannelConfigLock")]
        private static extern UInt32 _xChannelConfigLock(UInt32 hChannel,
                                                          UInt32 ulCmd,
                                                          ref UInt32 pulState,
                                                          UInt32 ulTimeout);

        [DllImport("cifx32dll.dll", EntryPoint = "xChannelReset")]
        private static extern UInt32 _xChannelReset(UInt32 hChannel,
                                                        UInt32 ulResetMode,
                                                        UInt32 ulTimeout);

        [DllImport("cifx32dll.dll", EntryPoint = "xChannelInfo")]
        private static extern UInt32 _xChannelInfo(UInt32 CHANNELHANDLE,
                                                    UInt32 ulSize,
                                                    ref CHANNEL_INFORMATIONtag pvChannelInfo);

        [DllImport("cifx32dll.dll", EntryPoint = "xChannelWatchdog")]
        private static extern UInt32 _xChannelWatchdog(UInt32 hChannel,
                                                        UInt32 ulCmd,
                                                        ref UInt32 pulTrigger);

        [DllImport("cifx32dll.dll", EntryPoint = "xChannelHostState")]
        private static extern UInt32 _xChannelHostState(UInt32 hChannel,
                                                    UInt32 ulCmd,
                                                    ref UInt32 pulState,
                                                    UInt32 ulTimeout);

        [DllImport("cifx32dll.dll", EntryPoint = "xChannelBusState")]
        private static extern UInt32 _xChannelBusState(UInt32 hChannel,
                                                        UInt32 ulCmd,
                                                        ref UInt32 pulState,
                                                        UInt32 ulTimeout);

        [DllImport("cifx32dll.dll", EntryPoint = "xChannelIOInfo")]
        private static extern UInt32 _xChannelIOInfo(UInt32 hChannel,
                                                      UInt32 ulCmd,
                                                      UInt32 ulAreaNumber,
                                                      UInt32 ulSize,
                                                      ref CHANNEL_IO_INFORMATIONtag pvData);

        [DllImport("cifx32dll.dll", EntryPoint = "xChannelIORead")]
        private static extern UInt32 _xChannelIORead(UInt32 hChannel,
                                               UInt32 ulAreaNumber,
                                               UInt32 ulOffset,
                                               UInt32 ulDataLen,
                                               [In, MarshalAs(UnmanagedType.LPArray)] byte[] pvData,
                                               UInt32 ulTimeout);

        [DllImport("cifx32dll.dll", EntryPoint = "xChannelIOWrite")]
        private static extern UInt32 _xChannelIOWrite(UInt32 hChannel,
                                                UInt32 ulAreaNumber,
                                                UInt32 ulOffset,
                                                UInt32 ulDataLen,
                                                [Out, MarshalAs(UnmanagedType.LPArray)] byte[] pvData,
                                                UInt32 ulTimeout);
        /*
                [DllImport("cifx32dll.dll", EntryPoint="xChannelIOReadSendData")]
                private static UInt32 _xChannelIOReadSendData(UInt32 hChannel, 
                                                              UInt32 ulAreaNumber, 
                                                              UInt32 ulOffset,     
                                                              UInt32 ulDataLen, 
                                                              [MarshalAs(UnmanagedType.LPStr)]string pvData);

                [DllImport("cifx32dll.dll", EntryPoint="xChannelControlBlock")]
                private static UInt32 _xChannelControlBlock(UInt32 hChannel, 
                                                            UInt32 ulCmd, 
                                                            UInt32 ulOffset, 
                                                            UInt32 ulDataLen, 
                                                            [MarshalAs(UnmanagedType.LPStr)]string pvData);

                [DllImport("cifx32dll.dll", EntryPoint="xChannelCommonStatusBlock")]
                private static UInt32 _xChannelCommonStatusBlock(UInt32 hChannel, 
                                                                 UInt32 ulCmd, 
                                                                 UInt32 ulOffset, 
                                                                 UInt32 ulDataLen, 
                                                                 [MarshalAs(UnmanagedType.LPStr)] string pvData);

                [DllImport("cifx32dll.dll", EntryPoint="xChannelExtendedStatusBlock")]
                private static UInt32 _xChannelExtendedStatusBlock(UInt32 hChannel, 
                                                                   UInt32 ulCmd, 
                                                                   UInt32 ulOffset, 
                                                                   UInt32 ulDataLen, 
                                                                   [MarshalAs(UnmanagedType.LPStr)]string pvData);

                [DllImport("cifx32dll.dll", EntryPoint="xChannelUserBlock")]
                private static UInt32 _xChannelUserBlock(UInt32 hChannel, 
                                                         UInt32 ulAreaNumber, 
                                                         UInt32 ulCmd, 
                                                         UInt32 ulOffset, 
                                                         UInt32 ulDataLen, 
                                                         [MarshalAs(UnmanagedType.LPStr)]string pvData);

                [DllImport("cifx32dll.dll", EntryPoint="xChannelPLCMemoryPtr")]
                private static UInt32 _xChannelPLCMemoryPtr(UInt32 hChannel, 
                                                            UInt32 ulCmd,        
                                                            ref MEMORY_INFORMATION pvMemoryInfo);

                [DllImport("cifx32dll.dll", EntryPoint="xChannelPLCIsReadReady")]
                private static UInt32 _xChannelPLCIsReadReady(UInt32 hChannel, 
                                                              UInt32 ulAreaNumber, 
                                                              ref UInt32 pulReadState);

                [DllImport("cifx32dll.dll", EntryPoint="xChannelPLCIsWriteReady")]
                private static UInt32 _xChannelPLCIsWriteReady(UInt32 hChannel, 
                                                               UInt32 ulAreaNumber, 
                                                               ref UInt32 pulWriteState);

                [DllImport("cifx32dll.dll", EntryPoint="xChannelPLCActivateWrite")]
                private static UInt32 _xChannelPLCActivateWrite(UInt32 hChannel, 
                                                                UInt32 ulAreaNumber);

                [DllImport("cifx32dll.dll", EntryPoint="xChannelPLCActivateRead")]
                private static UInt32 _xChannelPLCActivateRead(UInt32 hChannel, 
                                                               UInt32 ulAreaNumber);
        */
        #region public functions
        public UInt32 lReturn = 0;

        #region driver specific functions
        public UInt32 xDriverOpen()
        {
            try
            {
                lReturn = _xDriverOpen(ref _hDriver);
                return lReturn;
            }
            catch (Exception)
            {
                
                return 99;
            }
        }
        public UInt32 xDriverEnumBoards(UInt32 ulBoard)
        {
            try
            {
                lReturn = _xDriverEnumBoards(hDriver,
                                                ulBoard,
                                                (UInt32)Marshal.SizeOf(_BoardInformation),
                                                ref _BoardInformation);
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }
        public UInt32 xDriverEnumChannels(UInt32 ulBoard,
                                            UInt32 ulChannel)
        {
            try
            {
                lReturn = _xDriverEnumChannels(hDriver,
                                                ulBoard,
                                                ulChannel,
                                                (UInt32)Marshal.SizeOf(_ChannelInformation),
                                                ref _ChannelInformation);
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }
        public UInt32 xDriverGetInformation()
        {
            try
            {
                lReturn = _xDriverGetInformation(hDriver,
                                                    (UInt32)Marshal.SizeOf(_DriverInformation),
                                                    ref _DriverInformation);
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }
        public UInt32 xDriverClose()
        {
            try
            {
                lReturn = _xDriverClose(_hDriver);
                _hDriver = 0;
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }

        public UInt32 xDriverGetErrorDescription(UInt32 lError, ref byte[] szBuffer, UInt32 ulSize)
        {
            try
            {
                lReturn = _xDriverGetErrorDescription(lError, szBuffer, ulSize);
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }

        #endregion

        #region Sysdevice specific functions

        public UInt32 xSysdeviceOpen(string szBoard)
        {
            try
            {
                lReturn = _xSysdeviceOpen(_hDriver, szBoard, ref _hSysdevice);
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }

        public UInt32 xSysdeviceClose()
        {
            try
            {
                lReturn = _xSysdeviceClose(_hSysdevice);
                _hSysdevice = 0;
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }

        public UInt32 xSysdeviceReset(UInt32 ulTimeout)
        {
            try
            {
                lReturn = _xSysdeviceReset(_hSysdevice, ulTimeout);
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }

        public UInt32 xSysdeviceInfo(UInt32 ulCmd)
        {
            UInt32 ulSize = 0;

            switch (ulCmd)
            {
                case CIFX_INFO_CMD_SYSTEM_INFORMATION:
                    ulSize = (UInt32)Marshal.SizeOf(_SystemChannelInformation);
                    break;

                case CIFX_INFO_CMD_SYSTEM_INFO_BLOCK:
                    ulSize = (UInt32)Marshal.SizeOf(_SystemChannelInfoBlock);
                    break;

                case CIFX_INFO_CMD_SYSTEM_CHANNEL_BLOCK:
                    ulSize = (UInt32)CIFX_MAX_NUMBER_OF_CHANNEL_DEFINITION * CIFX_SYSTEM_CHANNEL_DEFAULT_INFO_BLOCK_SIZE;
                    break;

                case CIFX_INFO_CMD_SYSTEM_CONTROL_BLOCK:
                    ulSize = (UInt32)Marshal.SizeOf(_SystemChannelSystemControlBlock);
                    break;

                case CIFX_INFO_CMD_SYSTEM_STATUS_BLOCK:
                    ulSize = (UInt32)Marshal.SizeOf(_SystemChannelSystemStatusBlock);
                    break;
            }
            try
            {
                byte[] abData = new byte[ulSize];

                lReturn = _xSysdeviceInfo(_hSysdevice, ulCmd, ulSize, abData);

                if (ulCmd == CIFX_INFO_CMD_SYSTEM_CHANNEL_BLOCK)
                {
                    SplitDataArrayFromDriver(abData);
                    return (UInt32)cifXError.CIFX_NO_ERROR;
                }
                IntPtr pData = Marshal.AllocHGlobal(abData.Length);
                GCHandle pinnedData = GCHandle.Alloc(abData, GCHandleType.Pinned);

                switch (ulCmd)
                {
                    case CIFX_INFO_CMD_SYSTEM_INFORMATION:
                        _SystemChannelInformation = (SYSTEM_CHANNEL_INFORMATIONtag)Marshal.PtrToStructure(
                                                     pinnedData.AddrOfPinnedObject(), typeof(SYSTEM_CHANNEL_INFORMATIONtag));
                        pinnedData.Free();
                        break;

                    case CIFX_INFO_CMD_SYSTEM_INFO_BLOCK:
                        _SystemChannelInfoBlock = (SYSTEM_CHANNEL_INFO_BLOCKtag)Marshal.PtrToStructure(
                                                     pinnedData.AddrOfPinnedObject(), typeof(SYSTEM_CHANNEL_INFO_BLOCKtag));
                        pinnedData.Free();
                        break;

                    case CIFX_INFO_CMD_SYSTEM_CONTROL_BLOCK:
                        _SystemChannelSystemControlBlock = (SYSTEM_CHANNEL_SYSTEM_CONTROL_BLOCKtag)Marshal.PtrToStructure(
                                                     pinnedData.AddrOfPinnedObject(), typeof(SYSTEM_CHANNEL_SYSTEM_CONTROL_BLOCKtag));
                        pinnedData.Free();
                        break;

                    case CIFX_INFO_CMD_SYSTEM_STATUS_BLOCK:
                        _SystemChannelSystemStatusBlock = (SYSTEM_CHANNEL_SYSTEM_STATUS_BLOCKtag)Marshal.PtrToStructure(
                                                                        pinnedData.AddrOfPinnedObject(), typeof(SYSTEM_CHANNEL_SYSTEM_STATUS_BLOCKtag));
                        pinnedData.Free();
                        break;

                }
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }

        }

        private void SplitDataArrayFromDriver(byte[] abData)
        {
            int iIndex = 0;
            for (int iChannelDefinition = 0; iChannelDefinition <= CIFX_MAX_NUMBER_OF_CHANNEL_DEFINITION - 1; iChannelDefinition++)
            {
                for (int iDefaultInfoBlockSize = 0; iDefaultInfoBlockSize <= CIFX_SYSTEM_CHANNEL_DEFAULT_INFO_BLOCK_SIZE - 1; iDefaultInfoBlockSize++)
                {
                    abInfoBlock[iChannelDefinition, iDefaultInfoBlockSize] = abData[iIndex];
                    iIndex++;
                }
            }
        }

        public UInt32 xSysdeviceDownload(UInt32 ulChannel, UInt32 ulMode, string szFileName, byte[] abFileData, ref UInt32 pvUserParam)
        {
            try
            {
                lReturn = _xSysdeviceDownload(_hSysdevice,
                                                hChannel,
                                                ulMode,
                                                szFileName,
                                                abFileData,
                                                (UInt32)abFileData.Length,
                                                null,
                                                null,
                                                ref pvUserParam);
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }

        public UInt32 xSysdeviceFindFirstFile(UInt32 ulChannel, ref UInt32 pvUser)
        {
            try
            {
                //_CifXDirectoryEntry = new CIFX_DIRECTORYENTRYtag();
                _CifXDirectoryEntry.hList = 0;
                _CifXDirectoryEntry.szFilename = null;
                lReturn = _xSysdeviceFindFirstFile(_hSysdevice, ulChannel, ref _CifXDirectoryEntry, null, ref pvUser);
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }

        public UInt32 xSysdeviceFindNextFile(UInt32 ulChannel, ref UInt32 pvUser)
        {
            try
            {
                lReturn = _xSysdeviceFindNextFile(_hSysdevice, ulChannel, ref _CifXDirectoryEntry, null, ref pvUser);
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }

        public UInt32 xSysdeviceGetMBXState(ref UInt32 pulRecvPktCount, ref UInt32 pulSendPktCount)
        {
            try
            {
                lReturn = _xSysdeviceGetMBXState(_hSysdevice, ref pulRecvPktCount, ref pulSendPktCount);
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }

        public UInt32 xSysdevicePutPacket(CIFX_PACKETtag tPacket)
        {
            try
            {
                lReturn = _xSysdevicePutPacket(_hSysdevice, ref tPacket, 10);
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }

        public UInt32 xSysdeviceGetPacket(ref CIFX_PACKETtag tPacket)
        {
            try
            {
                UInt32 ulLen = 1560;
                lReturn = _xSysdeviceGetPacket(_hSysdevice, ulLen, ref tPacket, 10);
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }

        #endregion

        #region channel specific functions

        public UInt32 xChannelInfo()
        {
            try
            {
                lReturn = _xChannelInfo(hChannel,
                                            (UInt32)Marshal.SizeOf(_ChannelInformation),
                                            ref _ChannelInformation);
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }

        public UInt32 xChannelOpen(string szBoard, int iChannel)
        {
            try
            {
                lReturn = _xChannelOpen(hDriver,
                                            szBoard,
                                            (UInt32)iChannel,
                                            ref _hChannel);
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }

        public UInt32 xChannelClose()
        {
            try
            {
                lReturn = _xChannelClose(_hChannel);
                _hChannel = 0;
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }

        public UInt32 xChannelReset(UInt32 ulTimeout)
        {
            try
            {
                lReturn = _xChannelReset(_hChannel, CIFX_CHANNELINIT, ulTimeout);
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }

        public UInt32 xChannelDownload(UInt32 ulMode, string szFileName, byte[] abFileData, ref UInt32 pvUserParam)
        {
            try
            {
                lReturn = _xChannelDownload(hChannel,
                                            ulMode,
                                            szFileName,
                                            abFileData,
                                            (UInt32)abFileData.Length,
                                            null,
                                            null,
                                            ref pvUserParam);
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }

        public UInt32 xChannelHostState(UInt32 ulCmd, ref UInt32 pulState, UInt32 ulTimeout)
        {
            try
            {
                lReturn = _xChannelHostState(_hChannel, ulCmd, ref pulState, ulTimeout);
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }

        public UInt32 xChannelBusState(UInt32 ulCmd, ref UInt32 pulState, UInt32 ulTimeout)
        {
            try
            {
                lReturn = _xChannelBusState(_hChannel, ulCmd, ref pulState, ulTimeout);
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }

        public UInt32 xChannelConfigLock(UInt32 ulCmd, ref UInt32 pulState, UInt32 ulTimeout)
        {
            try
            {
                lReturn = _xChannelConfigLock(_hChannel, ulCmd, ref pulState, ulTimeout);
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }

        public UInt32 xChannelWatchdog(UInt32 ulCmd, ref UInt32 pulTrigger)
        {
            try
            {
                lReturn = _xChannelWatchdog(_hChannel, ulCmd, ref pulTrigger);
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }

        public UInt32 xChannelFindFirstFile(ref UInt32 pvUser)
        {
            try
            {
                _CifXDirectoryEntry = new CIFX_DIRECTORYENTRYtag();
                lReturn = _xChannelFindFirstFile(_hChannel, ref _CifXDirectoryEntry, null, ref pvUser);
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }

        public UInt32 xChannelFindNextFile(ref UInt32 pvUser)
        {
            try
            {
                lReturn = _xChannelFindNextFile(_hChannel, ref _CifXDirectoryEntry, null, ref pvUser);
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }

        private readonly object objRead = new object();
        private readonly object objWrite = new object();

        public UInt32 xChannelIORead(UInt32 ulAreaNumber, UInt32 ulOffset, UInt32 ulDataLen, ref byte[] pvData)
        {
            try
            {
                //lock (objRead)
                //{
                //    lReturn = _xChannelIORead(_hChannel, ulAreaNumber, ulOffset, ulDataLen, pvData, 100);
                //}
                lReturn = _xChannelIORead(_hChannel, ulAreaNumber, ulOffset, ulDataLen, pvData, 100);

                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }

        public UInt32 xChannelIOWrite(UInt32 ulAreaNumber, UInt32 ulOffset, UInt32 ulDataLen, ref byte[] pvData)
        {
            try
            {
                //lock (objWrite)
                //{
                //    lReturn = _xChannelIOWrite(_hChannel, ulAreaNumber, ulOffset, ulDataLen, pvData, 100);
                //}
                lReturn = _xChannelIOWrite(_hChannel, ulAreaNumber, ulOffset, ulDataLen, pvData, 100);

                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }

        public UInt32 xChannelIOInfo(UInt32 ulCmd, UInt32 ulAereaNumber)
        {
            try
            {
                lReturn = _xChannelIOInfo(_hChannel, ulCmd, ulAereaNumber, (UInt32)Marshal.SizeOf(_ChannelIOInformation), ref _ChannelIOInformation);
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }

        public UInt32 xChannelPutPacket(CIFX_PACKETtag tPacket)
        {
            try
            {
                lReturn = _xChannelPutPacket(_hChannel, tPacket, 10);
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }

        public UInt32 xChannelGetPacket(ref CIFX_PACKETtag tPacket)
        {
            try
            {
                lReturn = _xChannelGetPacket(_hChannel, (UInt32)Marshal.SizeOf(tPacket), ref tPacket, 10);
                return lReturn;
            }
            catch (Exception)
            {
                
                return lReturn;
            }
        }
        #endregion

        #endregion
    }
}
