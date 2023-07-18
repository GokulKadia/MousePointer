using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MousePointer
{
    internal class Program
    {
       
        static void Main(string[] args)
        {
            #region Mouse Settings
            // ApplyMouseSwapButton 
            //MouseSettings.SetKBMousePoiter("MouseSwape",0); // This is for settings the MouseSwape  (Left-handed Mouse)
            //MouseSettings.SetKBMousePoiter("MouseDoubleClickTime", 300); // This is for settings the MouseDoubleSpeed (Double Click Speed)
            //MouseSettings.SetKBMousePoiter("MousePointerTrail", 1); // This is for settings the MouseTrails (Pointer Trail Length)
            //MouseSettings.SetKBMousePoiter("MouseLock", 1); // This is for settings the MouseLock (Click Lock)
            //MouseSettings.SetKBMousePoiter("MouseHide", 1); // This is for settings the Hide Mouse Pointer while typing (Hide Mouse Pointer)
            //MouseSettings.SetKBMousePoiter("MouseFind", 1); // This is for settings the Show Mouse pointer Localtion while pressing Ctrl (Find Mouse Pointer)
            //MouseSettings.SetKBMousePoiter("MouseSnapButton", 1); // This is for settings the Autometically move mouse pointer to default button dialogue box  (Snap Mouse Pointer)
            //MouseSettings.SetKBMousePoiter("MouseSpeed", 10); // This is for settings the MouseSpeed (Mouse Speed)
            //MouseSettings.SetKBMousePoiter("MouseScroll", 5); // This is for settings the MouseScroll (Scroll Lines)
            #endregion

            #region Keyboard
            //MouseSettings.SetKBMousePoiter("KBMenuAccess", 1); // This is for Menu Access
            //MouseSettings.SetKBMousePoiter("KBrepeatDelay", 5); // This is for KBrepeatDelay (Control Keyboard in Win+R)
            //MouseSettings.SetKBMousePoiter("KBpref", 1); // This is for KBpref
            //MouseSettings.SetKBMousePoiter("KBRepeateRate", 20); // This is for RepeateRate (Control Keyboard in Win+R)
            //MouseSettings.SetKBMousePoiter("KBBlinkrate", 500); // This is for KBBlinkrate (Control Keyboard in Win+R)


            /* 
             Installing Language 
            1) First fetch the distinct lang code from Payload 
            2) Use Install function for installing Language Pack - PASSING FLAG FOR HANDLING INSTALL AND UNINSTALL
            3) before installing display lang we check if it is already there or not if install lang is proper installed then Configure Display language for current Profile
            4) after that we fetch the Name from Code of the Language for setting the registry
            5) last step set the registry values
            */

            StringBuilder strLangName = new StringBuilder(85);
            // int ss= MouseSettings.LCIDToLocaleName(0x0C0A, str,85,0); // Here in Argument we have to pass Lang code which is first in the Payload like 0409:00000409 so we need to pass 0409

            string KB_CPL_DESK_REG_PATH = "Control Panel\\Desktop";
            string KB_CPL_INTER_REG_PATH = "Control Panel\\International";
            string KB_CPL_INTER_USR_PROFILE_REG_PATH = "Control Panel\\International\\User Profile";

            if (MouseSettings.ApplyLanguageSettings("0416:00000416;0409:00000409;0C0A:0001040A;0409:00011009;0409:00004009;0414:0001043B;0414:00011809;", "UnInstall"))
            {
                if (MouseSettings.CompareCurrentDefaultMethod("0416:00000416") == false)
                {
                    if (MouseSettings.ApplyDefaultDisplaylanguage("0416:00000416;"))
                    {
                        if (MouseSettings.LCIDToLocaleName(0x0416, strLangName, 85, 0) != 0) // Here in Argument we have to pass Lang code which is first in the Payload like 0409:00000409 so we need to pass 0409
                        {
                            //Set registry 
                            RegistryKey key = Registry.CurrentUser.OpenSubKey(KB_CPL_DESK_REG_PATH);
                            key.SetValue("PreferredUILanguages", strLangName.ToString(),RegistryValueKind.MultiString);
                            key.SetValue("PreferredUILanguagesPending", strLangName.ToString(), RegistryValueKind.MultiString);
                            key = Registry.CurrentUser.OpenSubKey(KB_CPL_INTER_REG_PATH);
                            key.SetValue("Locale", "00000416");
                            key.SetValue("LocaleName", strLangName.ToString());
                        }
                    }
                }
            }

            ////Configure Default Keyboard Layout
            //if (MouseSettings.SetDefaultKBLanguages("0409:00004009;", 1))
            //{
            //    //Set tht Registry Path 
            //    //KB_CPL_INTER_USR_PROFILE_REG_PATH with key- InputMethodOverride and Value -  "0409:00004009;" (Default Keyboard Layout)
            //}


            // Only Now MSGINA keyboard layou we need to check  if not req then no need

            #endregion
        }
    }
}
