using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Win32;

namespace WindowsTweak
{
    class RegistrySettings
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        public static bool ModifyHKCURegistryValue(string KeyName, string SettingName, string SettingValue, int Value, RegistryValueKind nValueType, bool CreateKey)
        {
            try
            {
                RegistryKey regKey = Registry.CurrentUser;

                regKey = regKey.OpenSubKey(KeyName, true);
                if (regKey != null)
                {
                    if (nValueType == RegistryValueKind.String)
                    {
                        regKey.SetValue(SettingName, SettingValue, RegistryValueKind.String);
                    }
                    else
                    {
                        regKey.SetValue(SettingName, Value, nValueType);
                    }

                    regKey.Flush();
                    if (regKey != null)
                        regKey.Close();

                    return true;
                }
            }
            catch (Exception exc)
            {
                Logging.LogErrorMessage("Error modifying HKCU registry value - " + KeyName);
                Logging.LogErrorMessage("Error modifying HKCU registry value - " + SettingName);
                Logging.LogErrorMessage(exc.Message);
            }

            return false;
        }

        public static bool ModifyHKCURegistryBinaryValue(string KeyName, string SettingName, byte[] bValue)
        {
            try
            {
                RegistryKey regKey = Registry.CurrentUser;

                regKey = regKey.OpenSubKey(KeyName, true);
                if (regKey != null)
                {
                    regKey.SetValue(SettingName, bValue);

                    regKey.Flush();
                    if (regKey != null)
                        regKey.Close();

                    return true;
                }
            }
            catch (Exception exc)
            {
                Logging.LogErrorMessage("Error modifying HKCU registry value - " + KeyName);
                Logging.LogErrorMessage("Error modifying HKCU registry value - " + SettingName);
                Logging.LogErrorMessage(exc.Message);
            }

            return false;
        }

        public static string FindHKLMRegistrValueString(string KeyName, string SettingName)
        {
            try
            {
                RegistryKey regKey = Registry.LocalMachine;

                regKey = regKey.OpenSubKey(KeyName, false);
                if (regKey != null)
                {
                    string Value = (string)regKey.GetValue(SettingName);

                    regKey.Flush();
                    if (regKey != null)
                        regKey.Close();

                    return Value;
                }
            }
            catch (Exception exc)
            {
                Logging.LogErrorMessage("Error reading HKLM registry value - " + KeyName);
                Logging.LogErrorMessage("Error reading HKLM registry value - " + SettingName);
                Logging.LogErrorMessage(exc.Message);
            }

            return "";
        }

        public static string FindHKLMRegistryValueDWord(string KeyName, string SettingName)
        {
            try
            {
                RegistryKey regKey = Registry.LocalMachine;

                regKey = regKey.OpenSubKey(KeyName, false);
                if (regKey != null)
                {
                    var value = regKey.GetValue(SettingName);

                    regKey.Flush();
                    if (regKey != null)
                        regKey.Close();

                    return value.ToString();
                }
            }
            catch (Exception exc)
            {
                Logging.LogErrorMessage("Error reading HKLM registry value - " + KeyName);
                Logging.LogErrorMessage("Error reading HKLM registry value - " + SettingName);
                Logging.LogErrorMessage(exc.Message);
            }

            return "";
        }

        public static bool ModifyHKLMRegistryValue(string KeyName, string SettingName, string SettingValue, int Value, RegistryValueKind nValueType, bool CreateKey)
        {
            try
            {
                RegistryKey regKey = Registry.LocalMachine;

                regKey = regKey.OpenSubKey(KeyName, true);
                if (regKey == null)
                {
                    regKey = regKey.CreateSubKey(KeyName);
                }
                if (regKey != null)
                {
                    if (nValueType == RegistryValueKind.String)
                    {
                        regKey.SetValue(SettingName, SettingValue, RegistryValueKind.String);
                    }
                    else
                    {
                        regKey.SetValue(SettingName, Value, nValueType);
                    }

                    regKey.Flush();
                    if (regKey != null)
                        regKey.Close();

                    return true;
                }
            }
            catch (Exception exc)
            {
                Logging.LogErrorMessage("Error modifying HKLM registry value - " + KeyName);
                Logging.LogErrorMessage("Error modifying HKLM registry value - " + SettingName);
                Logging.LogErrorMessage(exc.Message);
            }

            return false;
        }

        public static string[] GetHKLMRegistrValueSubKeys(string KeyName)
        {
            try
            {
                RegistryKey regKey = Registry.LocalMachine;

                regKey = regKey.OpenSubKey(KeyName, false);
                if (regKey != null)
                {
                    Logging.LogErrorMessage("GetHKLMRegistrValueSubKeys - Key Found");

                    string[] Keynames = regKey.GetSubKeyNames();

                    regKey.Flush();
                    if (regKey != null)
                        regKey.Close();

                    return Keynames;
                }
            }
            catch (Exception exc)
            {
                Logging.LogErrorMessage("Error Getting SubKeys for HKLM registry value - " + KeyName);
                Logging.LogErrorMessage(exc.Message);
            }

            return null;
        }

    }
}
