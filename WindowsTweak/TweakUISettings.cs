using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Win32;

namespace WindowsTweak
{
    class TweakUISettings
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct ANIMATIONINFO
        {
            /// <summary>
            /// Creates an AMINMATIONINFO structure.
            /// </summary>
            /// <param name="iMinAnimate">If non-zero and SPI_SETANIMATION is specified, enables minimize/restore animation.</param>
            public ANIMATIONINFO(System.Int32 iMinAnimate)
            {
                this.cbSize = (System.UInt32)Marshal.SizeOf(typeof(ANIMATIONINFO));
                this.iMinAnimate = iMinAnimate;
            }

            /// <summary>
            /// Always must be set to (System.UInt32)Marshal.SizeOf(typeof(ANIMATIONINFO)).
            /// </summary>
            public System.UInt32 cbSize;

            /// <summary>
            /// If non-zero, minimize/restore animation is enabled, otherwise disabled.
            /// </summary>
            public System.Int32 iMinAnimate;
        }

        private const uint SPIF_UPDATEINIFILE = 0x01;
        private const uint SPIF_SENDWININICHANGE = 0x02;
        private const uint SPI_SETANIMATION = 0x0049;
        //private const uint SPI_SETUIEFFECTS = 0x103F;
        private const uint SPI_SETMENUANIMATION = 0x1003;
        private const uint SPI_SETCOMBOBOXANIMATION = 0x1005;
        private const uint SPI_SETLISTBOXSMOOTHSCROLLING = 0x1007;
        private const uint SPI_SETMENUFADE = 0x1013;
        private const uint SPI_SETSELECTIONFADE = 0x1015;
        private const uint SPI_SETTOOLTIPANIMATION = 0x1017;
        private const uint SPI_SETTOOLTIPFADE = 0x1019;
        private const uint SPI_SETCURSORSHADOW = 0x101B;
        private const uint SPI_SETDROPSHADOW = 0x1025;
        private const uint SPI_SETCLIENTAREAANIMATION = 0x1043;

        private const uint SPI_GETUIEFFECTS = 0x103E;
        private const uint SPI_GETTOOLTIPANIMATION = 0x1016;
        
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SystemParametersInfo(uint uiAction, uint uiParam, bool pvParam, uint fWinIni);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SystemParametersInfo(uint uiAction, uint uiParam, ref ANIMATIONINFO pvParam, uint fWinIni);

        public TweakUISettings()
        {

        }
        public bool SetDisplayOptions()
        {
            bool bSuccess = true;

            if (!SetUISettingsToCustom(@"Software\Microsoft\Windows\CurrentVersion\Explorer\VisualEffects"))
            {
                return false;
            }

            if (!SetAnimationControlsAndElements())
            {
                bSuccess = false;
            }

            if (!SetAnimateWindowsWhenMinMax())
            {
                bSuccess = false;
            }

            if (!SetDropShadow())
            {
                bSuccess = false;
            }

            if (!SetTaskbarAnimation())
            {
                bSuccess = false;
            }

            //if( !SetUserPreferencesMask())
            //{
            //    bSuccess = false;
            //}

            return bSuccess;
        }

        //private bool SetUserPreferencesMask()
        //{
        //    //List of bits for the User Preferences Mask - http://technet.microsoft.com/en-us/library/cc957204.aspx
            
        //    return RegistrySettings.ModifyHKCURegistryBinaryValue(@"Control Panel\Desktop", "UserPreferencesMask", new byte[] { 0x90, 0x22, 0x07, 0x00, 0x10, 0x00, 0x00, 0x00 });
        //}

        private bool SetUISettingsToCustom(string regKey)
        {
            //Setting this value to 3 will ensure the UI settings are set to "Custom", allowing for changes
            return RegistrySettings.ModifyHKCURegistryValue(regKey,
                                                    "VisualFXSetting",
                                                    "",
                                                    3,
                                                    RegistryValueKind.DWord,
                                                    true);
        }

        private bool SetAnimationControlsAndElements()
        {
            bool enabled = false;

            ANIMATIONINFO animInfo = new ANIMATIONINFO(0);

            bool bSuccess = true;

            //Disable options
            if( !SystemParametersInfo(SPI_SETCLIENTAREAANIMATION, 0, enabled, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE) )
            {
                bSuccess = false;
            }
            
            if( !SystemParametersInfo(SPI_SETCOMBOBOXANIMATION, 0, enabled, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE) )
            {
                bSuccess = false;
            }

            if( !SystemParametersInfo(SPI_SETLISTBOXSMOOTHSCROLLING, 0, enabled, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE) )
            {
                bSuccess = false;
            }

            if( !SystemParametersInfo(SPI_SETMENUANIMATION, 0, enabled, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE) )
            {
                bSuccess = false;
            }

            if( !SystemParametersInfo(SPI_SETSELECTIONFADE, 0, enabled, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE) )
            {
                bSuccess = false;
            }

            if( !SystemParametersInfo(SPI_SETTOOLTIPANIMATION, 0, enabled, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE) )
            {
                bSuccess = false;
            }

            if (!SystemParametersInfo(SPI_SETTOOLTIPFADE, 0, enabled, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE))
            {
                bSuccess = false;
            }

            if( !SystemParametersInfo(SPI_SETANIMATION, (System.UInt32)Marshal.SizeOf(typeof(ANIMATIONINFO)), ref animInfo, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE))
            {
                bSuccess = false;
            }

            //Enable options
            enabled = true;
            if( !SystemParametersInfo(SPI_SETCURSORSHADOW, 0, enabled, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE) )
            {
                bSuccess = false;
            }
            
            if( !SystemParametersInfo(SPI_SETDROPSHADOW, 0, enabled, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE))
            {
                bSuccess = false;
            }

            return bSuccess;

        }

        private bool SetAnimateWindowsWhenMinMax()
        {
            return RegistrySettings.ModifyHKCURegistryValue(@"Control Panel\Desktop\WindowMetrics",
                "MinAnimate",
                "",
                0,
                RegistryValueKind.DWord, true);
        }

        private bool SetDropShadow()
        {
            return RegistrySettings.ModifyHKCURegistryValue(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
                "ListviewShadow",
                "",
                0,
                RegistryValueKind.DWord, true);
        }
        private bool SetTaskbarAnimation()
        {
            return RegistrySettings.ModifyHKCURegistryValue(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced",
                "TaskbarAnimations",
                "",
                0,
                RegistryValueKind.DWord, true);


        }

        public bool DisableAeroPeek()
        {
            return RegistrySettings.ModifyHKCURegistryValue(@"Software\Microsoft\Windows\DWM",
                                            "EnableAeroPeek",
                                            "",
                                            0,
                                            RegistryValueKind.DWord, true);
        }

        public bool EnableSaveTaskbarThumbnailPreviews()
        {
            return RegistrySettings.ModifyHKCURegistryValue(@"Software\Microsoft\Windows\DWM",
                                            "AlwaysHibernateThumbnails",
                                            "",
                                            1,
                                            RegistryValueKind.DWord, true);

        }
    }
}
