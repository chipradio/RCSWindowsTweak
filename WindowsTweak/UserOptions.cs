using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using WindowsTweak.Properties;

namespace WindowsTweak
{
    class UserOptions
    {
        public UserOptions()
        {

        }
        public bool DisableUAC()
        {
            //UAC Group Policy Settings and Registry Key Settings
            //https://technet.microsoft.com/en-us/library/dd835564(v=ws.10).aspx#BKMK_BuiltInAdmin
            //https://4sysops.com/archives/why-the-built-in-administrator-account-cant-open-edge-and-a-lesson-in-uac/
            try
            {
                RegistrySettings.ModifyHKLMRegistryValue(Resources.RegistrySettingCurrentVersionPoliciesSystem,
                        "FilterAdministratorToken",
                        "",
                        0,
                        RegistryValueKind.DWord, true);

                return RegistrySettings.ModifyHKLMRegistryValue(Resources.RegistrySettingCurrentVersionPoliciesSystem,
                        "EnableLUA",
                        "",
                        0,
                        RegistryValueKind.DWord, true);
            }
            catch (Exception exc)
            {
                Logging.LogErrorMessage("Error Disabling User Account Control");
                Logging.LogErrorMessage(exc.Message);
            }

            return false;
        }
        public bool DisableShowSuggestions()
        {
            //http://www.winhelponline.com/blog/how-to-disable-start-menu-ads-or-suggestions-in-windows-10/
            //https://answers.microsoft.com/en-us/windows/forum/windows_10-start/how-can-powershell-be-used-to-automate-turning-off/c10ca270-2ce9-44f3-9cba-712eb9ba6942
            if (!RegistrySettings.ModifyHKCURegistryValue(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager",
                "SystemPaneSuggestionsEnabled",
                "",
                0,
                RegistryValueKind.DWord, true))
            {
            }

            if (!RegistrySettings.ModifyHKCURegistryValue(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager\",
                "SoftLandingEnabled",
                "",
                0,
                RegistryValueKind.DWord, true))
            {
            }

            if (!RegistrySettings.ModifyHKCURegistryValue(@"SOFTWARE\Microsoft\Windows\CurrentVersion\ContentDeliveryManager\",
                "RotatingLockScreenEnabled",
                "",
                0,
                RegistryValueKind.DWord, true))
            {
            }

            return true;
        }
        public bool DisableAppNotifications(bool bWindows10)
        {
            //http://superuser.com/questions/1140134/how-do-i-turn-off-get-notifications-from-apps-and-other-senders-in-windows-10
            //https://www.tenforums.com/tutorials/4111-notifications-apps-turn-off-windows-10-a.html#option2

            //HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\PushNotifications

            //ToastEnabled DWORD

            //0 = Turn off
            //1 = Turn on
            //string _psScript = Resources.RegistryHKCUPushNotifications;
            //SystemOptions.PerformPowerShell("", "", "", "", "", "", "", _psScript);

            //_psScript = Resources.RegistryHKCUToastEnabled;
            //SystemOptions.PerformPowerShell("", "", "", "", "", "", "", _psScript);

            //if( bWindows10 )
            {
                if(!RegistrySettings.ModifyHKCURegistryValue(@"SOFTWARE\Microsoft\Windows\CurrentVersion\PushNotifications",
                    "ToastEnabled",
                    "",
                    0,
                    RegistryValueKind.DWord, true))
                {
                    Logging.LogErrorMessage(@"SOFTWARE\Microsoft\Windows\CurrentVersion\PushNotifications\ToastEnabled");
                }
            }
            //else
            //{
            //    RegistrySettings.ModifyHKCURegistryValue(@"SOFTWARE\Microsoft\Windows\CurrentVersion\PushNotifications",
            //        "NoToastApplicationNotification ",
            //        "",
            //        1,
            //        RegistryValueKind.DWord, true);
            //}

            DisableShowSuggestions();

            return true;

        }

        public bool DisableNotificationCenter()
        {
            //HKEY_CURRENT_USER\SOFTWARE\Policies\Microsoft\Windows\Explorer

            //DisableNotificationCenter DWORD

            string _psScript = Resources.RegistrySettingWindowsExplorer;
            SystemOptions.PerformPowerShell("", "", "", "", "", "", "", _psScript);

            //_psScript = Resources.RegistryHKCUDisableNotificationCenter;
            //SystemOptions.PerformPowerShell("", "", "", "", "", "", "", _psScript);

            //1 = Turn off
            //0 = Turn on
            RegistrySettings.ModifyHKCURegistryValue(@"SOFTWARE\Policies\Microsoft\Windows\Explorer",
                 "DisableNotificationCenter",
                 "",
                 1,
                 RegistryValueKind.DWord, true);

            return true;
        }
        public bool ShowDesktopIcons()
        {
            try
            {
                if (EnableComputerIconFOrUser())
                {
                    if (EnableControlPanelIconForUser())
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Error Enabling Desktop Icons");
                Logging.LogErrorMessage(ex.Message);
            }

            return false;
        }

        private bool EnableComputerIconFOrUser()
        {
            try
            {
                RegistrySettings.ModifyHKCURegistryValue(Resources.RegistrySettingClassicStartMenu,
                                "{20D04FE0-3AEA-1069-A2D8-08002B30309D}",
                                "",
                                0,
                                RegistryValueKind.DWord, true);
                RegistrySettings.ModifyHKCURegistryValue(Resources.RegistrySettingNewStartPanel,
                                    "{20D04FE0-3AEA-1069-A2D8-08002B30309D}",
                                    "",
                                    0,
                                    RegistryValueKind.DWord, true);

                RegistrySettings.ModifyHKLMRegistryValue(Resources.RegistrySettingClassicStartMenu,
                                "{20D04FE0-3AEA-1069-A2D8-08002B30309D}",
                                "",
                                0,
                                RegistryValueKind.DWord, true);
                RegistrySettings.ModifyHKLMRegistryValue(Resources.RegistrySettingNewStartPanel,
                                "{20D04FE0-3AEA-1069-A2D8-08002B30309D}",
                                "",
                                0,
                                RegistryValueKind.DWord, true);

                return true;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Error Enabling Computer Icon");
                Logging.LogErrorMessage(ex.Message);
            }

            return false;
        }

        private bool EnableControlPanelIconForUser()
        {
            try
            {
                RegistrySettings.ModifyHKCURegistryValue(Resources.RegistrySettingClassicStartMenu,
                                "{F02C1A0D-BE21-4350-88B0-7367FC96EF3C}",
                                "",
                                0,
                                RegistryValueKind.DWord, true);
                RegistrySettings.ModifyHKCURegistryValue(Resources.RegistrySettingNewStartPanel,
                                    "{F02C1A0D-BE21-4350-88B0-7367FC96EF3C}",
                                    "",
                                    0,
                                    RegistryValueKind.DWord, true);

                RegistrySettings.ModifyHKLMRegistryValue(Resources.RegistrySettingClassicStartMenu,
                                   "{F02C1A0D-BE21-4350-88B0-7367FC96EF3C}",
                                   "",
                                   0,
                                   RegistryValueKind.DWord, true);

                RegistrySettings.ModifyHKLMRegistryValue(Resources.RegistrySettingNewStartPanel,
                               "{F02C1A0D-BE21-4350-88B0-7367FC96EF3C}",
                               "",
                               0,
                               RegistryValueKind.DWord, true);


                return true;
            }
            catch (Exception ex)
            {
                Logging.LogErrorMessage("Error Enabling Network Icons");
                Logging.LogErrorMessage(ex.Message);
            }

            return false;
        }


        public bool EnableAutoLogin(string UserName, string PassWord, string Domain)
        {
            try
            {
                if (String.IsNullOrEmpty(UserName) || String.IsNullOrEmpty(PassWord))
                {
                    Logging.LogErrorMessage("Error Setting AutoLogin for User");
                    Logging.LogErrorMessage("Username and/or Password fields were blank!");
                    return false;
                }

                string userName = UserName;
                string userPW = PassWord;
                string userDomain = Domain;

                RegistrySettings.ModifyHKLMRegistryValue(Resources.RegistrySettingWinLogon,
                    "AutoAdminLogon", "1", 1, RegistryValueKind.String, true);
                RegistrySettings.ModifyHKLMRegistryValue(Resources.RegistrySettingWinLogon,
                    "DefaultUserName", userName, 0, RegistryValueKind.String, true);
                RegistrySettings.ModifyHKLMRegistryValue(Resources.RegistrySettingWinLogon,
                    "DefaultPassword", userPW, 0, RegistryValueKind.String, true);
                if( !String.IsNullOrEmpty(userDomain))
                {
                    RegistrySettings.ModifyHKLMRegistryValue(Resources.RegistrySettingWinLogon,
                        "DefaultDomainName", userDomain, 0, RegistryValueKind.String, true);

                }
                return true;
            }
            catch (Exception exc)
            {
                Logging.LogErrorMessage("Error Setting AutoLogin for User");
                Logging.LogErrorMessage(exc.Message);
            }

            return false;
        }

    }
}
