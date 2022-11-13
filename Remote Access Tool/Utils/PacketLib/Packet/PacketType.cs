/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace PacketLib.Packet
{
    public enum PacketType : byte
    {
        CLOSE_CLIENT =                          0,
        UNINSTALL_CLOSE_CLIENT =                1,
        CONNECTED =                             2,

        RECOVERY_PASSWORDS =                    3,
        RECOVERY_HISTORY =                      4,
        RECOVERY_AUTOFILL =                     53,
        RECOVERY_KEYWORDS =                     54,
        RECOVERY_ALL =                          66,

        FM_GET_DISK =                           5,
        FM_GET_FILES_AND_DIRS =                 6,
        FM_DOWNLOAD_FILE =                      7,
        FM_DELETE_FILE =                        8,
        FM_START_FILE =                         9,
        FM_RENAME_FILE =                        46,
        FM_SHORTCUT_PATH  =                     51,
        FM_UPLOAD_FILE =                        58,

        PM_GET_PROCESSES =                      10,
        PM_KILL_PROCESS =                       11,
        PM_SUSPEND_PROCESS =                    12,
        PM_RESUME_PROCESS =                     13,
        PM_INJECT_PROCESS =                     33, 

        KEYLOG_ON =                             14,
        KEYLOG_OFF =                            15, 
        KEYLOG_OFFLINE =                        52,

        MEM_EXEC_SHELLCODE =                    16,
        MEM_EXEC_NATIVE_DLL =                   17,
        MEM_EXEC_NATIVE_PE =                    18,
        MEM_EXEC_MANAGED_DLL =                  19,
        MEM_EXEC_MANAGED_PE =                   20,
        MEM_EXEC_CSHARP_CODE =                  64,
        MEM_EXEC_VB_CODE =                      65,

        POWER_SHUTDOWN =                        21,
        POWER_REBOOT =                          22,
        POWER_LOG_OUT =                         23,
        POWER_BSOD =                            24,
        POWER_LOCK_WORKSTATION =                25,
        POWER_SUSPEND =                         26,
        POWER_HIBERNATE =                       27,

        MISC_HIDE_TASKBAR =                     28,
        MISC_SHOW_TASKBAR =                     29,
        MISC_AUDIO_UP =                         30,
        MISC_AUDIO_DOWN =                       31,
        MISC_SET_WALLPAPER =                    32,
        MISC_INFORMATION =                      43,
        MISC_SCREENLOCKER_ON =                  44,
        MISC_SCREENLOCKER_OFF =                 45,
        MISC_HIDE_DESKTOP_ICONS =               49,
        MISC_SHOW_DESKTOP_ICONS =               50,
        MISC_SCREEN_ROTATION =                  61,
        MISC_ASK_ADMIN_RIGHTS =                 67,
        MISC_OPEN_WEBSITE_LINK =                70,

        RM_VIEW_ON =                            34,
        RM_VIEW_OFF =                           35,
        RM_MOUSE =                              59,
        RM_KEYBOARD =                           60,

        RC_GET_CAM =                            36,
        RC_CAPTURE_ON =                         37,
        RC_CAPTURE_OFF =                        38,

        HDW_MS_OFF =                            39,
        HDW_MS_ON =                             40,
        HDW_KB_OFF =                            41,
        HDW_KB_ON =                             42,

        RANSOMWARE_ENCRYPTION =                 47,
        RANSOMWARE_DECRYPTION =                 48,
        RANSOMWARE_ENCRYPTION_CONFIRMATION =    71,
        RANSOMWARE_DECRYPTION_CONFIRMATION =    72,

        AUDIO_GET_DEVICES =                     55,
        AUDIO_RECORD_ON =                       56,
        AUDIO_RECORD_OFF =                      57,

        CHAT_ON =                               62,
        CHAT_OFF =                              63,

        UAC_GET_RESTORE_POINT =                 68,
        UAC_DELETE_RESTORE_POINT =              69,

        SHELL_START =                           73,
        SHELL_COMMAND =                         74,
        SHELL_STOP =                            75
    }
}
