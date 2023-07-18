using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MousePointer
{
    public class MouseSettings
    {
        /// <summary>
        /// Main Function is SystemparamtersInfo so i added in entrypoint and given custom name for calling and understanding
        /// </summary>
       [DllImport("user32.dll", SetLastError = true,EntryPoint = "SystemParametersInfo")]
        static extern bool ApplyKBMouseSetting(int uiAction, int uiParam, int pvParam, int fWinIni);

        /// <summary>
        /// written this function for handling Cursor Blinking rate in Keyboard
        /// </summary>
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetCaretBlinkTime(int uMSeconds);

        /// <summary>
        /// written this function for handling the installation of the Keyboard langauge pack 
        /// </summary>        
        [DllImport("Input.dll", SetLastError = true,EntryPoint = "InstallLayoutOrTip")]
        public static extern bool InstallLanguages([MarshalAsAttribute(UnmanagedType.LPWStr)] string lang, int flag);

        /// <summary>
        /// written this function for handling the Setting the Default Keyboard langauge pack 
        /// </summary>        
        [DllImport("Input.dll", SetLastError = true, EntryPoint = "SetDefaultLayoutOrTip")]
        public static extern bool SetDefaultKBLanguages([MarshalAsAttribute(UnmanagedType.LPWStr)] string lang, int flag);

        /// <summary>
        /// This is for fetching language name from Code
        /// </summary>        
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int LCIDToLocaleName(uint Locale,StringBuilder lpName, int cchName, int dwFlags);

       
        public enum KBMouseSet : int
        {
            //Mouse Settings
            SPI_SETMOUSEBUTTONSWAP= 0x0021,
            SPIF_UPDATEINIFILE= 0x0001,
            SPI_SETDOUBLECLICKTIME= 0x0020,
            SPI_SETMOUSETRAILS = 0x005D,
            SPI_SETMOUSECLICKLOCK= 0x101F,
            SPI_SETMOUSECLICKLOCKTIME= 0x2009,
            SPI_SETMOUSEVANISH= 0x1021,
            SPI_SETMOUSESONAR= 0x101D,
            SPI_SETSNAPTODEFBUTTON = 0x0060,
            SPI_SETWHEELSCROLLLINES= 0x0069,
            SPI_SETMOUSESPEED= 0x0071,
            //Keyboard Settings 
            SPI_SETKEYBOARDCUES = 0x100B,
            SPI_SETKEYBOARDDELAY= 0x0017,
            SPI_SETKEYBOARDPREF = 0x0045,
            SPI_SETKEYBOARDSPEED = 0x000B,
            //Keyboard Language Handler
            ILOT_CLEANINSTALL = 0x00000040,
            ILOT_DEFPROFILE = 0x00000002,
            ILOT_UNINSTALL = 0x00000001
        }

        public static bool SetKBMousePoiter(string MouseOption,int Value)
        {
            bool isResult = false;
            switch (MouseOption)
            {
                case "MouseSwape":
                    ApplyKBMouseSetting((int)KBMouseSet.SPI_SETMOUSEBUTTONSWAP, Value,0, (int)KBMouseSet.SPIF_UPDATEINIFILE);
                    break;
                case "MouseDoubleClickTime":
                    ApplyKBMouseSetting((int)KBMouseSet.SPI_SETDOUBLECLICKTIME, Value, 0, (int)KBMouseSet.SPIF_UPDATEINIFILE);
                    break;
                case "MousePointerTrail":
                    ApplyKBMouseSetting((int)KBMouseSet.SPI_SETMOUSETRAILS, Value, 0, (int)KBMouseSet.SPIF_UPDATEINIFILE);
                    break;
                case "MouseLock":
                    ApplyKBMouseSetting((int)KBMouseSet.SPI_SETMOUSECLICKLOCK, 0, Value, (int)KBMouseSet.SPIF_UPDATEINIFILE);
                    ApplyKBMouseSetting((int)KBMouseSet.SPI_SETMOUSECLICKLOCKTIME, 0, Value, (int)KBMouseSet.SPIF_UPDATEINIFILE);
                    break;
                case "MouseHide":
                    ApplyKBMouseSetting((int)KBMouseSet.SPI_SETMOUSEVANISH, 0, Value, (int)KBMouseSet.SPIF_UPDATEINIFILE);
                    break;
                case "MouseFind":
                    ApplyKBMouseSetting((int)KBMouseSet.SPI_SETMOUSESONAR, 0, Value, (int)KBMouseSet.SPIF_UPDATEINIFILE);
                    break;
                case "MouseSnapButton":
                    ApplyKBMouseSetting((int)KBMouseSet.SPI_SETSNAPTODEFBUTTON, Value, 0, (int)KBMouseSet.SPIF_UPDATEINIFILE);
                    break;
                case "MouseScroll":
                    ApplyKBMouseSetting((int)KBMouseSet.SPI_SETWHEELSCROLLLINES, Value, 0, (int)KBMouseSet.SPIF_UPDATEINIFILE);
                    break;
                case "MouseSpeed":
                    ApplyKBMouseSetting((int)KBMouseSet.SPI_SETMOUSESPEED, 0, Value, (int)KBMouseSet.SPIF_UPDATEINIFILE);
                    break;
                case "KBMenuAccess":
                    ApplyKBMouseSetting((int)KBMouseSet.SPI_SETKEYBOARDCUES, 0, Value, (int)KBMouseSet.SPIF_UPDATEINIFILE);
                    break;
                case "KBrepeatDelay":
                    ApplyKBMouseSetting((int)KBMouseSet.SPI_SETKEYBOARDDELAY, Value,0, (int)KBMouseSet.SPIF_UPDATEINIFILE);
                    break;
                case "KBpref":
                    ApplyKBMouseSetting((int)KBMouseSet.SPI_SETKEYBOARDPREF, Value, 0, (int)KBMouseSet.SPIF_UPDATEINIFILE);
                    break;
                case "KBRepeateRate":
                    ApplyKBMouseSetting((int)KBMouseSet.SPI_SETKEYBOARDSPEED, Value, 0, (int)KBMouseSet.SPIF_UPDATEINIFILE);
                    break;
                case "KBBlinkrate":
                    SetCaretBlinkTime(Value);
                    break;               
                default:
                    break;
            }
            return isResult;
        }

        public static bool ApplyLanguageSettings(string langCode,string strinstall)
        {
            bool iSuccess = false;
            //1)  Call Null Check
            //2)  Install the Language Pack
            iSuccess=strinstall=="Install"? InstallLanguages(langCode, (int)KBMouseSet.ILOT_CLEANINSTALL) : InstallLanguages(langCode, (int)KBMouseSet.ILOT_UNINSTALL);
            return iSuccess;
        }
        public static bool ApplyDefaultDisplaylanguage(string langCode)
        {
            bool iSuccess = false;
            //1)  Call Null Check
            //2)  Install the Language Pack
            iSuccess = InstallLanguages(langCode, (int)KBMouseSet.ILOT_DEFPROFILE);
            return iSuccess;
        }

        public static string KB_CPL_INTER_REG_PATH = "Control Panel\\International";
        public static string KB_CPL_DESK_REG_PATH = "Control Panel\\Desktop";

        /// <summary>
        /// this function is for checking defaultDisplayLanguage is already there or not if same is there then it retunr true other wise false 
        /// </summary>
        /// <param name="DispLang"></param>
        /// <returns></returns>
        public static bool CompareCurrentDefaultMethod(string DispLang)
        {
            bool isdefDispLang = false;
            RegistryKey key = Registry.CurrentUser.OpenSubKey(KB_CPL_INTER_REG_PATH);
            string regValue= key.GetValue("Locale").ToString();
            return isdefDispLang = regValue == DispLang ? true : false;
        }
    }
}
