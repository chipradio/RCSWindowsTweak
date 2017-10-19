using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;
using System.Management.Automation;
using System.Net.NetworkInformation;
using System.Collections.ObjectModel;
using System.Resources;
using System.Reflection;
using WindowsTweak.Properties;
using System.DirectoryServices;
using System.Management.Automation.Runspaces;

namespace WindowsTweak
{
    class SystemOptions
    {
        List<NetworkInterface> netAdapters = new List<NetworkInterface>();

        public SystemOptions()
        {

        }

        public bool DisableAutoPlay()
        {
            try
            {
                RegistrySettings.ModifyHKLMRegistryValue(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer",
                    "NoDriveTypeAutorun",
                    "",
                    177,
                    RegistryValueKind.DWord, true);

                RegistrySettings.ModifyHKCURegistryValue(@"Software\Microsoft\Windows\CurrentVersion\Policies\Explorer",
                    "NoDriveTypeAutorun",
                    "",
                    177,
                    RegistryValueKind.DWord, true);

                return true;
            }
            catch (Win32Exception ex)
            {
                Logging.LogErrorMessage("Error Disabling AutoPlay");
                Logging.LogErrorMessage(ex.Message);
            }

            return false;
        }
        public bool EnableRDP()
        {
            try
            {
                return RegistrySettings.ModifyHKLMRegistryValue(Resources.RegistrySettingTerminalServer,
                    "fDenyTSConnections",
                    "",
                    0,
                    RegistryValueKind.DWord, true);
            }
            catch (Exception exc)
            {
                Logging.LogErrorMessage("Error enabling Remote Desktop Protocol");
                Logging.LogErrorMessage(exc.Message);
            }

            return false;
        }
        public bool DisableSystemRestore()
        {
            try
            {
                string osDrive = Path.GetPathRoot(Environment.SystemDirectory);

                ManagementScope scope = new ManagementScope("\\\\localhost\\root\\default");
                ManagementPath path = new ManagementPath("SystemRestore");
                ObjectGetOptions options = new ObjectGetOptions();
                ManagementClass process = new ManagementClass(scope, path, options);
                ManagementBaseObject inParams = process.GetMethodParameters("Disable");
                inParams["Drive"] = osDrive;
                ManagementBaseObject outParams = process.InvokeMethod("Disable", inParams, null);

                return true;
            }
            catch (Exception exc)
            {
                Logging.LogErrorMessage("Error Disabling System Restore");
                Logging.LogErrorMessage(exc.Message);
            }

            return false;
        }

        public bool DisableSleepForPC()
        {
            try
            {
                DisablePowerOptionUsingPowerCfg(Resources.StandbyTimeoutAC);
                DisablePowerOptionUsingPowerCfg(Resources.StandbyTimeoutDC);

                DisablePowerOptionUsingPowerCfg(Resources.HibernateTimeoutAC);
                DisablePowerOptionUsingPowerCfg(Resources.HibernateTimeoutDC);

                DisablePowerOptionUsingPowerCfg(Resources.MonitorTimeoutAC);
                DisablePowerOptionUsingPowerCfg(Resources.MonitorTimeoutDC);

                DisablePowerOptionUsingPowerCfg(Resources.DiskTimeoutAC);
                DisablePowerOptionUsingPowerCfg(Resources.DiskTimeoutDC);

                //set the sleep timeout
                DisablePowerOptionUsingPowerCfg(Resources.SleepTimeoutAC);
                DisablePowerOptionUsingPowerCfg(Resources.SleepTimeoutDC);

                //set the minimum processor state
                DisablePowerOptionUsingPowerCfg(Resources.MonitorTimeoutAC);
                DisablePowerOptionUsingPowerCfg(Resources.MonitorTimeoutDC);

                return true;
            }
            catch (Exception exc)
            {
                Logging.LogErrorMessage("Error Disable Sleep for PC");
                Logging.LogErrorMessage(exc.Message);
            }

            return false;
        }

        public bool DisableSystemSounds()
        {
            try
            {
                var sRegistryFile = Application.StartupPath;

                if (!sRegistryFile.EndsWith(Convert.ToString(Path.DirectorySeparatorChar)))
                {
                    sRegistryFile += Path.DirectorySeparatorChar;
                }

                sRegistryFile += "Install\\";

                var sPSFile = sRegistryFile;
                sPSFile += "DisableSystemSounds.ps1";
                RunScript(sPSFile);

                sRegistryFile += "NoSounds.reg";
                string command = "import " + sRegistryFile;
                return PerformProcess("reg.exe", command);
            }
            catch (Exception exc)
            {
                Logging.LogErrorMessage("Error disabling System Sounds");
                Logging.LogErrorMessage(exc.Message);
            }

            return false;
        }

        /// <summary>
        /// Runs a Powershell script taking it's path and parameters.
        /// </summary>
        /// <param name="scriptFullPath">The full file path for the .ps1 file.</param>
        /// <param name="parameters">The parameters for the script, can be null.</param>
        /// <returns>The output from the Powershell execution.</returns>
        /// http://www.codeproject.com/Articles/18229/How-to-run-PowerShell-scripts-from-C
        /// http://stackoverflow.com/questions/527513/execute-powershell-script-from-c-sharp-with-commandline-arguments
        public static ICollection<PSObject> RunScript(string scriptFullPath, ICollection<CommandParameter> parameters = null)
        {
            var runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();
            var pipeline = runspace.CreatePipeline();
            var cmd = new Command(scriptFullPath);
            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    cmd.Parameters.Add(p);
                }
            }
            pipeline.Commands.Add(cmd);
            var results = pipeline.Invoke();
            pipeline.Dispose();
            runspace.Dispose();
            return results;
        }
        public List<NetworkInterface> GetNetworkAdapters()
        {
            List<NetworkInterface> values = new List<NetworkInterface>();
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if( nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet || nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                {
                    values.Add(nic);
                }
                
            }
            return values;
        }

        public bool DisableTCPIP6OnNics()
        {
            try
            {
                bool bNoError = true;

                if ( !netAdapters.Any()  )
                {
                    netAdapters = GetNetworkAdapters();
                }

                //string _psScript = @"Disable-NetAdapterBinding -Name * -ComponentID ""ms_tcpip6"" ";
                string _psScript = String.Format(Resources.DisableTCPIP6OnNics);
                if (!PerformPowerShell("", "", "", "", "", "", "", _psScript))
                {
                    Logging.LogErrorMessage("Error Disable QoS on Network Adapters - " + _psScript);
                    bNoError = false;
                }

                return bNoError;

            }
            catch (System.Exception ex)
            {
                Logging.LogErrorMessage("Error Disable TCPIP v6 on Network Adapters");
                Logging.LogErrorMessage(ex.Message);
            }

            return false;

        }

        public bool DisableFlowControlOnNics()
        {
            try
            {
                bool bNoError = true;

                if (!netAdapters.Any())
                {
                    netAdapters = GetNetworkAdapters();
                }

                //string _psScript = @"Set-NetAdapterAdvancedProperty -Name " + adapter + @" -DisplayName ""Flow Control"" -DisplayValue ""Disabled"" ";
                string _psScript = String.Format(Resources.DisableFlowControlOnNics);
                if (!PerformPowerShell("", "", "", "", "", "", "", _psScript))
                {
                    Logging.LogErrorMessage("Error Disable Flow Control on Network Adapters - " + _psScript);
                    bNoError = false;
                }

                return bNoError;
            }
            catch (System.Exception ex)
            {
                Logging.LogErrorMessage("Exception - Error Disable Flow Control on Network Adapters");
                Logging.LogErrorMessage(ex.Message);
            }

            return false;

        }

        public bool DisableQOSOnNics()
        {
            try
            {
                bool bNoError = true;

                if (!netAdapters.Any())
                {
                    netAdapters = GetNetworkAdapters();
                }

                foreach (NetworkInterface adapter in netAdapters)
                {
                    if (!adapter.Name.Contains("Axia") && !adapter.Name.Contains("Wheat"))
                    {
                        //string _psScript = @"Disable-NetAdapterBinding -Name " + adapter.Name + " -ComponentID ms_pacer";
                        string _psScript = String.Format(Resources.DisableQOSOnNics, adapter.Name);
                        if (!PerformPowerShell("", "", "", "", "", "", "", _psScript))
                        {
                            Logging.LogErrorMessage("Error Disable QoS on Network Adapters - " + _psScript);
                            bNoError = false;
                        }
                    }
                }

                return bNoError;
            }
            catch (System.Exception ex)
            {
                Logging.LogErrorMessage("Error Disable TCPIP v6 on Network Adapters");
                Logging.LogErrorMessage(ex.Message);
            }

            return false;

        }

        public bool DisablePowerManagementOnNics()
        {
            try
            {
                int nIndex = 0;
                for (nIndex = 0; nIndex < 100; nIndex++)
                {
                    RegistrySettings.ModifyHKLMRegistryValue(@"SYSTEM\CurrentControlSet\Control\Class\{4D36E972-E325-11CE-BFC1-08002BE10318}\" + nIndex.ToString("D4"),
                                                   "PnPCapabilities",
                                                   "",
                                                   24,
                                                   RegistryValueKind.DWord, false);
                }

                return true;

                //bool bNoError = true;

                //if (!netAdapters.Any())
                //{
                //    netAdapters = GetNetworkAdapters();
                //}

                //foreach (NetworkInterface adapter in netAdapters)
                //{
                //    string _psScript = @"Disable-NetAdapterPowerManagement -Name *";
                //    string _psScript = Resources.DisablePowerManagementOnNics + @"""" + adapter.Name + @"""";
                //    if (!PerformPowerShell("", "", "", "", "", "", "", _psScript))
                //    {
                //        Logging.LogErrorMessage("Error Disable Flow Control on Network Adapters");
                //        bNoError = false;
                //    }
                //}

                //return bNoError;
            }
            catch (System.Exception ex)
            {
                Logging.LogErrorMessage("Exception - Error Disable Flow Control on Network Adapters");
                Logging.LogErrorMessage(ex.Message);
            }

            return false;
        }

        public bool DisableDefrag()
        {
            try
            {
                //string _psScript = @"Disable-ScheduledTask -TaskName ""ScheduledDefrag"" -TaskPath ""\Microsoft\Windows\Defrag\"" ";
                string _psScript = Resources.DisableDefrag;
                return PerformPowerShell("", "", "", "", "", "", "", _psScript);
            }
            catch (Exception exc)
            {
                Logging.LogErrorMessage("Error Disabling Scheduled Defrag Task!");
                Logging.LogErrorMessage(exc.Message);
            }

            return false;
        }


        public bool SetFirewallPermissions()
        {
            var sPermissionsFile = Application.StartupPath;

            if (!sPermissionsFile.EndsWith(Convert.ToString(Path.DirectorySeparatorChar)))
            {
                sPermissionsFile += Path.DirectorySeparatorChar;
            }

            sPermissionsFile += "Install\\";

            sPermissionsFile += "Win7NGFirewall.wfw";

            if (!File.Exists(sPermissionsFile))
            {
                Logging.LogErrorMessage("Error Firewall Permissions File does not exist");
                return false;
            }

            string sArgument = "advfirewall import ";
            sArgument += sPermissionsFile;

            if( PerformProcess("netsh.exe", sArgument) )
            {
                sArgument = "advfirewall firewall set rule group=\"Remote Desktop\" new enable=yes";

                return PerformProcess("netsh.exe", sArgument);
            }

            return false;

            
        }

        public bool SetAdvancedFirewall()
        {
            bool returnCode = true;

            string sArgument = "advfirewall set allprofiles state on";

            if (!PerformProcess("netsh.exe", sArgument))
            {
                returnCode = false;
            }

            sArgument = "advfirewall set allprofiles firewallpolicy allowinbound,allowoutbound";

            if (!PerformProcess("netsh.exe", sArgument))
            {
                returnCode = false;
            }

            return returnCode;
        }

        public bool DisablePowerOptionUsingPowerCfg(string sArgument)
        {
            try
            {
                return PerformProcess(Resources.PowerCfg, sArgument);
            }
            catch (Exception exc)
            {
                Logging.LogErrorMessage("Error changing Power Options - " + sArgument);
                Logging.LogErrorMessage(exc.Message);
            }

            return false;
        }

        public bool SetLanManagerAuth()
        {
            try
            {
                return RegistrySettings.ModifyHKLMRegistryValue(Resources.RegistrySettingControlLSA,
                    "lmcompatibilitylevel",
                    "",
                    1,
                    RegistryValueKind.DWord, true);
            }
            catch (Exception exc)
            {
                Logging.LogErrorMessage("Error setting Lan Manager Authentication");
                Logging.LogErrorMessage(exc.Message);
            }

            return false;
        }

        public bool SetListMangerPolicies()
        {
            try
            {
                //SOFTWARE\Policies\Microsoft\Windows NT\CurrentVersion\NetworkList\Signatures\010103000F0000F0010000000F0000F0C967A3643C3AD745950DA7859209176EF5B87C875FA20DF21951640E807D7C24
                return RegistrySettings.ModifyHKLMRegistryValue(Resources.RegistrySettingControlLSA,
                    "lmcompatibilitylevel",
                    "",
                    1,
                    RegistryValueKind.DWord, true);
            }
            catch (Exception exc)
            {
                Logging.LogErrorMessage("Error setting List Manager Policies");
                Logging.LogErrorMessage(exc.Message);
            }

            return false;
        }

        public bool SetFolderOwner()
        {
            try
            {
                string pathIntern = @"C:\";

                FolderPermissions.GrantAdministratorsAccess(pathIntern, FolderPermissions.SE_OBJECT_TYPE.SE_FILE_OBJECT);

                return true;
            }
            catch (System.Exception ex)
            {
                Logging.LogErrorMessage("Error Setting Folder Owner");
                Logging.LogErrorMessage(ex.Message);
            }

            return false;
        }


        public bool DisableSynchronizeTimeFromInternet()
        {
            return RegistrySettings.ModifyHKLMRegistryValue(Resources.DisableSynchronizeTimeFromInternet,
                "Type",
                "NoSync",
                0,
                RegistryValueKind.String, true);
        }

        public bool CreateEnableLinkedConnections()
        {
            try
            {
                return RegistrySettings.ModifyHKLMRegistryValue(Resources.RegistrySettingCurrentVersionPoliciesSystem,
                    "EnableLinkedConnections",
                    "",
                    1,
                    RegistryValueKind.DWord, true);
            }
            catch (Exception exc)
            {
                Logging.LogErrorMessage("Error Setting CreateEnableLinkedConnections in Registry");
                Logging.LogErrorMessage(exc.Message);
            }

            return false;
        }

        public bool DisableConsentPromptBehaviorAdmin()
        {
            try
            {
                return RegistrySettings.ModifyHKLMRegistryValue(Resources.RegistrySettingCurrentVersionPoliciesSystem,
                    "ConsentPromptBehaviorAdmin",
                    "",
                    0,
                    RegistryValueKind.DWord, true);
            }
            catch (Exception exc)
            {
                Logging.LogErrorMessage("Error Setting DisableConsentPromptBehaviorAdmin Registry Setting to 0");
                Logging.LogErrorMessage(exc.Message);
            }

            return false;
        }

        public bool DisableConsentPromptBehaviorUser()
        {
            try
            {
                return RegistrySettings.ModifyHKLMRegistryValue(Resources.RegistrySettingCurrentVersionPoliciesSystem,
                    "ConsentPromptBehaviorUser",
                    "",
                    1,
                    RegistryValueKind.DWord, true);
            }
            catch (Exception exc)
            {
                Logging.LogErrorMessage("Error Setting ConsentPromptBehaviorUser Registry Setting to 0");
                Logging.LogErrorMessage(exc.Message);
            }

            return false;
        }

        public bool SetExeToRunAsAdministrator()
        {
            try
            {
                return RegistrySettings.ModifyHKCURegistryValue(Resources.RegistrySettingRunAsAdministrator,
                    @"C:\HLC\NexGen.exe",
                    "RUNASADMIN",
                    0,
                    RegistryValueKind.String, true);
            }
            catch (Exception exc)
            {
                Logging.LogErrorMessage("Error Setting EXE to Run As Administrator");
                Logging.LogErrorMessage(exc.Message);
            }

            return false;
        }

        public bool CreateRCSUserAndSetToAutoLogon()
        {
            try
            {
                DirectoryEntry hostMachineDirectory = new DirectoryEntry("WinNT://localhost");
                DirectoryEntries entries = hostMachineDirectory.Children;
                bool userExists = false;
                foreach (DirectoryEntry each in entries)
                {
                    userExists = each.Name.Equals("RCS",StringComparison.CurrentCultureIgnoreCase);
                    if (userExists)
                        break;
                }

                if (false == userExists)
                {
                    DirectoryEntry obUser = entries.Add("RCS", "User");
                    obUser.Properties["FullName"].Add("RCS");
                    //obUser.Invoke("SetPassword", "abcdefg12345@");
                    obUser.Invoke("Put", new object[] { "UserFlags", 0x10000 });
                    obUser.CommitChanges();

                    RegistrySettings.ModifyHKLMRegistryValue(Resources.RegistrySettingWinLogon,
                        "AutoAdminLogon", "1", 1, RegistryValueKind.String, true);
                    RegistrySettings.ModifyHKLMRegistryValue(Resources.RegistrySettingWinLogon,
                        "DefaultUserName", "RCS", 0, RegistryValueKind.String, true);

                    return true;
                }
            }
            catch (System.Exception ex)
            {
                Logging.LogErrorMessage("Error Creating RCS User");
                Logging.LogErrorMessage(ex.Message);

            }

            return false;
        }

        public bool SetWindowsUpdatesToDisable()
        {
            try
            {
                //https://support.microsoft.com/en-us/kb/328010
                //https://4sysops.com/archives/disable-windows-10-update-in-the-registry-and-with-powershell/
                //https://msdn.microsoft.com/en-us/library/dd939844(v=ws.10).aspx
                //string _psScript = Set - ItemProperty - Path $AutoUpdatePath - Name NoAutoUpdate - Value 1

                string _psScript = Resources.RegistryPoliciesMSNewWU;
                PerformPowerShell("", "", "", "", "", "", "", _psScript);

                _psScript = Resources.RegistryPoliciesMSNewAU;
                 PerformPowerShell("", "", "", "", "", "", "", _psScript);

                _psScript = Resources.RegistryPoliciesMSAUNoAU;
                PerformPowerShell("", "", "", "", "", "", "", _psScript);

                _psScript = Resources.RegistryCurrentVersionMSNewWU;
                PerformPowerShell("", "", "", "", "", "", "", _psScript);

                _psScript = Resources.RegistryCurrentVersionMSNewAU;
                PerformPowerShell("", "", "", "", "", "", "", _psScript);

                _psScript = Resources.RegistryCurrentVersionMSAUNoAU;
                PerformPowerShell("", "", "", "", "", "", "", _psScript);

                _psScript = Resources.RegistryPoliciesMSNewAUOptions;
                PerformPowerShell("", "", "", "", "", "", "", _psScript);

                return true;
            }
            catch (Exception exc)
            {
                Logging.LogErrorMessage("Error Setting Windows Updates to Disabled!");
                Logging.LogErrorMessage(exc.Message);
            }

            return false;
        }

        public bool DisableSecureTime()
        {
            return RegistrySettings.ModifyHKLMRegistryValue(@"SYSTEM\CurrentControlSet\Services\w32time\Config",
                "UtilizeSslTimeData",
                "",
                0,
                RegistryValueKind.DWord, true);
        }

        public bool DisableActionCenterSidebar()
        {
            //http://www.askvg.com/collection-of-windows-10-hidden-secret-registry-tweaks/

            RegistrySettings.ModifyHKLMRegistryValue(@"Software\Microsoft\Windows\CurrentVersion\ImmersiveShell",
                "UseActionCenterExperience",
                "",
                0,
                RegistryValueKind.DWord, true);

            return true;
        }
        public bool UninstallDefaultApps()
        {
            //http://pureinfotech.com/how-uninstall-apps-powershell-commands-windows-10/
            //http://www.howtogeek.com/224798/how-to-uninstall-windows-10s-built-in-apps-and-how-to-reinstall-them/
            //https://www.techsupportall.com/uninstall-built-apps-windows-10/

            //Uninstall all apps for a user;
            //Get - AppxPackage - User Username | Remove - AppxPackage
            //Replace ‘Username’ with the name of the user you want to remove the app for.

            //Uninstall all apps for all users;
            //Get - AppxPackage - AllUsers | Remove - AppxPackage

            //Uninstall a single app for all users;
            //remove - AppxProvisionedPackage[App Package Name]

            using (PowerShell ps = PowerShell.Create())
            {
                ps.Commands.AddScript("Get-AppxPackage -AllUsers | Remove-AppxPackage");
                //ps.AddCommand("Get-AppxPackage");
                //ps.AddParameter("-AllUsers");
                //ps.AddCommand("Remove-AppxPackage");

                try
                {
                    ps.Invoke();
                    return true;
                }
                catch (System.Exception exc)
                {
                    Logging.LogErrorMessage("Error Performing Uninstall of Default Apps");
                    Logging.LogErrorMessage(exc.Message);

                }
            }

            return false;
        }

        public bool UnpinAppsFromStart()
        {
            //https://www.tenforums.com/customization/21002-how-automatically-cmd-powershell-script-unpin-all-apps-start.html

            return true;
        }

        public static bool PerformPowerShell(string PSCommand, string PSNameArgument, string PSNameValue, string PSComponentID, string PSComponentIDValue,
                               string PSEnabled, string PSEnabledValue, string PSScript)
        {
            try
            {
                using (PowerShell ps = PowerShell.Create())
                {
                    if (!String.IsNullOrEmpty(PSScript))
                    {
                        ps.AddScript(PSScript);
                    }
                    else
                    {

                        ps.AddCommand(PSCommand);
                        ps.AddArgument(PSNameArgument);
                        ps.AddParameter(PSNameValue);
                        ps.AddArgument(PSComponentID);
                        ps.AddParameter(PSComponentIDValue);

                        if (!String.IsNullOrEmpty(PSEnabled))
                        {
                            ps.AddArgument(PSEnabled);
                            ps.AddParameter(PSEnabledValue);
                        }
                    }

                    Collection<PSObject> result = ps.Invoke();
                    if (result.Any())
                    {
                        return true;
                    }

                    if (ps.Streams.Error.Count > 0)
                    {
                        foreach (var errorRecord in ps.Streams.Error)
                        {
                            Logging.LogErrorMessage(errorRecord.ToString());
                        }
                        return false;
                    }

                    return true;
                }
            }
            catch (System.Exception ex)
            {
                if (!String.IsNullOrEmpty(PSScript))
                {
                    Logging.LogErrorMessage("Error Performing PowerShell Script - " + PSScript);
                }
                else
                {
                    Logging.LogErrorMessage("Error Performing PowerShell Command");
                }

                Logging.LogErrorMessage(ex.Message);
            }

            return false;
        }
        public bool PerformProcess(string sExeToStart, string sArgument)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.FileName = sExeToStart;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.Arguments = sArgument;

                try
                {
                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess.WaitForExit();
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    Logging.LogErrorMessage("Error - " + sExeToStart + " - " + sArgument);
                    Logging.LogErrorMessage(ex.Message);
                }
            }
            catch (Exception exc)
            {
                Logging.LogErrorMessage("Error - " + sExeToStart + " - " + sArgument);
                Logging.LogErrorMessage(exc.Message);
            }

            return false;
        }

    }

    internal class NetworkAdapter
    {
        /// <summary> 
        /// The DeviceId of the NetworkAdapter 
        /// </summary> 
        public int DeviceId
        {
            get;
            private set;
        }

        /// <summary> 
        /// The ProductName of the NetworkAdapter 
        /// </summary> 
        public string Name
        {
            get;
            private set;
        }


        /// <summary> 
        /// The NetEnabled status of the NetworkAdapter 
        /// </summary> 
        public int NetEnabled
        {
            get;
            private set;
        }


        public NetworkAdapter(int deviceId,
            string name,
            int netEnabled)
        {
            DeviceId = deviceId;
            Name = name;
            NetEnabled = netEnabled;
        }
    } 
}
