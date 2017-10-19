using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms.VisualStyles;

namespace WindowsTweak
{
    public partial class WindowsTweaksMainWindow : Form
    {
        enum SoftwareApplication
        {
            None,
            NexGen,
            Zetta
        };

        TweakUISettings UISettings = new TweakUISettings();
        SystemOptions SysOptions = new SystemOptions();
        UserOptions UserOptions = new UserOptions();
        InstallOptions Installers = new InstallOptions();

        bool _bPowershellWarningDisplayed = false;
        bool clicked = true;
        public bool _bWindows10 = false;

        public WindowsTweaksMainWindow()
        {
            InitializeComponent();

            if (IsWindows10OrGreater())
            {
                _bWindows10 = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _radioNone.Checked = true;
        }

        private void RefreshListView(SoftwareApplication software)
        {
            //lvWindowsTweaks.Refresh();
            lvWindowsTweaks.Items.Clear();

            ListViewGroup lvGroupDisableSettings = new ListViewGroup("Disable Settings");
            lvWindowsTweaks.Groups.Add(lvGroupDisableSettings);
            ListViewGroup lvGroupEnableSettings = new ListViewGroup("Enable Settings");
            lvWindowsTweaks.Groups.Add(lvGroupEnableSettings);
            ListViewGroup lvGroupMisc = new ListViewGroup("Miscellaneous");
            lvWindowsTweaks.Groups.Add(lvGroupMisc); 

            ListViewItem lvItem = new ListViewItem(lvGroupDisableSettings);
            lvItem.Checked = software == SoftwareApplication.None ? false : true;
            lvItem.Text = "Disable System Restore";
            lvItem.SubItems.Add("System Restore Functionality Will Be Disabled");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.DisableSystemRestore;
            lvWindowsTweaks.Items.Add(lvItem);

            lvItem = new ListViewItem(lvGroupDisableSettings);
            lvItem.Checked = (software == SoftwareApplication.None ? false : (software == SoftwareApplication.NexGen) ? true : false);
            lvItem.Text = "Disable User Account Control (UAC)";
            lvItem.SubItems.Add("User Account Control Will Be Disabled");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.DisableUAC;
            lvWindowsTweaks.Items.Add(lvItem);

            lvItem = new ListViewItem(lvGroupDisableSettings);
            lvItem.Checked = (software == SoftwareApplication.None) ? false : true;
            lvItem.Text = "Disable Scheduled Defrag";
            lvItem.SubItems.Add("Automatic defrag scheduled by Windows Will Be Disabled");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.DisableDefrag;
            lvWindowsTweaks.Items.Add(lvItem);

            lvItem = new ListViewItem(lvGroupDisableSettings);
            lvItem.Checked = (software == SoftwareApplication.None) ? false : true;
            lvItem.Text = "Disable AutoPlay";
            lvItem.SubItems.Add("AutoPlay for All Drives Will Be Disabled");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.DisableAutoPlay;
            lvWindowsTweaks.Items.Add(lvItem);

            lvItem = new ListViewItem(lvGroupDisableSettings);
            lvItem.Checked = (software == SoftwareApplication.None) ? false : true;
            lvItem.Text = "Disable Power Mgmt on Network Adapters";
            lvItem.SubItems.Add("Power Management On NICs Will Be Disabled");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.DisablePowerMgmtOnNics;
            lvWindowsTweaks.Items.Add(lvItem);

            lvItem = new ListViewItem(lvGroupDisableSettings);
            lvItem.Checked = (software == SoftwareApplication.None) ? false : true;
            lvItem.Text = "Disable UI Effects";
            lvItem.SubItems.Add("Fades, Animations, And Other Effects Will Be Disabled");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.DisableUIEffects;
            lvWindowsTweaks.Items.Add(lvItem);

            lvItem = new ListViewItem(lvGroupDisableSettings);
            lvItem.Checked = (software == SoftwareApplication.None) ? false : true;
            lvItem.Text = "Disable Aero Peek";
            lvItem.SubItems.Add("Aero Peek Will Be Disabled");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.SetAeroPeek;
            lvWindowsTweaks.Items.Add(lvItem);

            lvItem = new ListViewItem(lvGroupDisableSettings);
            lvItem.Checked = (software == SoftwareApplication.None) ? false : true;
            lvItem.Text = "Disable Sync Time With Internet";
            lvItem.SubItems.Add("Synchronize Time With Internet Server Will Be Disabled");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.DisableSyncTimeWithInternet;
            lvWindowsTweaks.Items.Add(lvItem);

            lvItem = new ListViewItem(lvGroupDisableSettings);
            lvItem.Checked = (software == SoftwareApplication.None) ? false : true;
            lvItem.Text = "Disable System Sounds";
            lvItem.SubItems.Add("System Sounds Scheme to NONE");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.DisableSystemSounds;
            lvWindowsTweaks.Items.Add(lvItem);

            lvItem = new ListViewItem(lvGroupDisableSettings);
            lvItem.Checked = (software == SoftwareApplication.None ? false : (software == SoftwareApplication.NexGen) ? true : false);
            lvItem.Text = "Disable IPv6";
            lvItem.SubItems.Add("IPv6 On Network Adapters Will Be Disabled");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.DisableIPv6;
            lvWindowsTweaks.Items.Add(lvItem);

            lvItem = new ListViewItem(lvGroupDisableSettings);
            lvItem.Checked = (software == SoftwareApplication.None) ? false : true;
            lvItem.Text = "Disable QoS Packet Scheduler";
            lvItem.SubItems.Add("QoS Packet Scheduler On Network Adapters Will Be Disabled");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.DisableQoS;
            lvWindowsTweaks.Items.Add(lvItem);

            lvItem = new ListViewItem(lvGroupDisableSettings);
            lvItem.Checked = (software == SoftwareApplication.None) ? false : true;
            lvItem.Text = "Disable Flow Control";
            lvItem.SubItems.Add("Flow Control On Network Adapters Will Be Disabled");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.DisableFlowControl;
            lvWindowsTweaks.Items.Add(lvItem);

            lvItem = new ListViewItem(lvGroupDisableSettings);
            lvItem.Checked = (software == SoftwareApplication.None ? false : (software == SoftwareApplication.NexGen) ? true : false);
            lvItem.Text = "Disable ConsentPromptBehaviorAdmin Registry Setting";
            lvItem.SubItems.Add("Registry Setting ConsentPromptBehaviorAdmin Will Be Set To 0");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.DisableConsentPromptBehaviorAdmin;
            lvWindowsTweaks.Items.Add(lvItem);

            lvItem = new ListViewItem(lvGroupDisableSettings);
            lvItem.Checked = (software == SoftwareApplication.None ? false : (software == SoftwareApplication.NexGen) ? true : false);
            lvItem.Text = "Disable ConsentPromptBehaviorUser Registry Setting";
            lvItem.SubItems.Add("Registry Setting ConsentPromptBehaviorUser Will Be Set To 0");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.DisableConsentPromptBehaviorUser;
            lvWindowsTweaks.Items.Add(lvItem);

            lvItem = new ListViewItem(lvGroupDisableSettings);
            lvItem.Checked = (software == SoftwareApplication.None) ? false : true;
            lvItem.Text = "Disable Automatic Windows Updates";
            lvItem.SubItems.Add("Disable the Ability for Windows to Perform Automatic Updates");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.SetWindowsUpdatesToDisable;
            lvWindowsTweaks.Items.Add(lvItem);

            if(_bWindows10 )
            {
                lvItem = new ListViewItem(lvGroupDisableSettings);
                lvItem.Checked = (software == SoftwareApplication.None) ? false : true; ;
                lvItem.Text = "Disable Secure Time";
                lvItem.SubItems.Add("Disable Secure Time on Windows10 to Avoid Time Sync Drifts");
                lvItem.SubItems.Add("");
                lvItem.Tag = TweakConsts.TweakValues.DisableSecureTime;
                lvWindowsTweaks.Items.Add(lvItem);

                lvItem = new ListViewItem(lvGroupDisableSettings);
                lvItem.Checked = (software == SoftwareApplication.None) ? false : true; 
                lvItem.Text = "Disable App Notifications and Suggestions";
                lvItem.SubItems.Add("Disable Notifications, Suggestions, Tips and Tricks, etc.");
                lvItem.SubItems.Add("");
                lvItem.Tag = TweakConsts.TweakValues.DisableShowSuggestions;
                lvWindowsTweaks.Items.Add(lvItem);

                lvItem = new ListViewItem(lvGroupDisableSettings);
                lvItem.Checked = (software == SoftwareApplication.None) ? false : true;
                lvItem.Text = "Disable Action Center Sidebar";
                lvItem.SubItems.Add("Disable the Action Center Sidebar");
                lvItem.SubItems.Add("");
                lvItem.Tag = TweakConsts.TweakValues.DisableActionCenterSidebar;
                lvWindowsTweaks.Items.Add(lvItem);

                lvItem = new ListViewItem(lvGroupDisableSettings);
                lvItem.Checked = (software == SoftwareApplication.None) ? false : true;
                lvItem.Text = "Disable Action Center ";
                lvItem.SubItems.Add("Disable the Action Center (Notifications)");
                lvItem.SubItems.Add("");
                lvItem.Tag = TweakConsts.TweakValues.DisableNotificationCenter;
                lvWindowsTweaks.Items.Add(lvItem);
            }

            lvItem = new ListViewItem(lvGroupEnableSettings);
            lvItem.Checked = (software == SoftwareApplication.None) ? false : true;
            lvItem.Text = "Enable Remote Desktop";
            lvItem.SubItems.Add("Remote Desktop Protocol Will Be Enabled");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.EnableRDP;
            lvWindowsTweaks.Items.Add(lvItem);

            lvItem = new ListViewItem(lvGroupEnableSettings);
            lvItem.Checked = (software == SoftwareApplication.None) ? false : true;
            lvItem.Text = "Enable Security - NT Lan Manager Authentication";
            lvItem.SubItems.Add("LAN Manager Auth Will be Set To \"Send LM & NTLM.\"");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.SetLanManagerAuth;
            lvWindowsTweaks.Items.Add(lvItem);

            lvItem = new ListViewItem(lvGroupEnableSettings);
            lvItem.Checked = (software == SoftwareApplication.None) ? false : true;
            lvItem.Text = "Enable Computer & Network Icons on Desktop";
            lvItem.SubItems.Add("Desktop Icons Will be Shown");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.EnableDesktopIcons;
            lvWindowsTweaks.Items.Add(lvItem);

            lvItem = new ListViewItem(lvGroupEnableSettings);
            lvItem.Checked = (software == SoftwareApplication.None) ? false : true;
            lvItem.Text = "Enable Save Taskbar Thumbnail Previews";
            lvItem.SubItems.Add("Save Taskbar Thumbnail Previews Will Be Enabled");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.SetSaveTaskbarThumbnailPreviews;
            lvWindowsTweaks.Items.Add(lvItem);

            lvItem = new ListViewItem(lvGroupEnableSettings);
            lvItem.Checked = (software == SoftwareApplication.None) ? false : true;
            lvItem.Text = "Enable Firewall Permissions";
            lvItem.SubItems.Add("Sets the Firewall Permissions");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.SetFirewallPermissions;
            lvWindowsTweaks.Items.Add(lvItem);

            lvItem = new ListViewItem(lvGroupEnableSettings);
            lvItem.Checked = (software == SoftwareApplication.None) ? false : true;
            lvItem.Text = "Enable Windows Firewall with Advanced Security Permissions";
            lvItem.SubItems.Add("Set Domain, Private, and Public Profile to On/Allow/Allow");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.SetAdvancedFirewallPermissions;
            lvWindowsTweaks.Items.Add(lvItem);

            lvItem = new ListViewItem(lvGroupEnableSettings);
            lvItem.Checked = (software == SoftwareApplication.None ? false : (software == SoftwareApplication.NexGen) ? true : false);
            lvItem.Text = "Enable EnabledLinkedConnections in Registry";
            lvItem.SubItems.Add("Create New EnabledLinkedConnections DWORD Value in Registry");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.CreateEnableLinkedConnections;
            lvWindowsTweaks.Items.Add(lvItem);

            lvItem = new ListViewItem(lvGroupEnableSettings);
            lvItem.Checked = (software == SoftwareApplication.None ? false : (software == SoftwareApplication.NexGen) ? true : false);
            lvItem.Text = "Enable NexGen To Run As Administrator";
            lvItem.SubItems.Add("Enable Coyote.exe to Run As Administrator");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.SetExeToRunAsAdministrator;
            lvWindowsTweaks.Items.Add(lvItem);

            lvItem = new ListViewItem(lvGroupMisc);
            lvItem.Checked = (software == SoftwareApplication.Zetta) ? true : false;
            lvItem.Text = "Create RCS User and Set to Auto-Logon";
            lvItem.SubItems.Add("Create the user \"RCS\" And Enable To Auto-Logon");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.CreateRCSUserAndSetToAutoLogon;
            lvWindowsTweaks.Items.Add(lvItem);

            lvItem = new ListViewItem(lvGroupMisc);
            lvItem.Checked = (software == SoftwareApplication.None) ? false : true;
            lvItem.Text = "Set Power Option for Monitor to NEVER";
            lvItem.SubItems.Add("Monitor Will Not Be Turned Off");
            lvItem.SubItems.Add(""); lvItem.Tag = TweakConsts.TweakValues.SetPowerOffMonitorToNever;
            lvWindowsTweaks.Items.Add(lvItem);

            lvItem = new ListViewItem(lvGroupMisc);
            lvItem.Checked = (software == SoftwareApplication.None) ? false : true;
            lvItem.Text = "Set Power Option for Turn off Hard Drive to NEVER";
            lvItem.SubItems.Add("Hard Drives Will Not Go to Sleep");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.SetPowerOffHDToNever;
            lvWindowsTweaks.Items.Add(lvItem);

            lvItem = new ListViewItem(lvGroupMisc);
            lvItem.Checked = (software == SoftwareApplication.None) ? false : true;
            lvItem.Text = "Set Power Option for PC to NEVER";
            lvItem.SubItems.Add("PC Will Not Go To Sleep");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.SetPowerOffPCToNever;
            lvWindowsTweaks.Items.Add(lvItem);

            lvItem = new ListViewItem(lvGroupMisc);
            lvItem.Checked = (software == SoftwareApplication.None ? false : (software == SoftwareApplication.NexGen) ? true : false);
            lvItem.Text = "Set Owner for Root of C";
            lvItem.SubItems.Add("Set Administrator Group as Owner");
            lvItem.SubItems.Add("");
            lvItem.Tag = TweakConsts.TweakValues.SetOwnerForC;
            lvWindowsTweaks.Items.Add(lvItem);

            if (_bWindows10)
            {
                lvItem = new ListViewItem(lvGroupMisc);
                lvItem.Checked = (software == SoftwareApplication.None) ? false : true;
                lvItem.Text = "Remove Built-In Applications";
                lvItem.SubItems.Add("Uninstall All Default Apps, such as Candy Crush, etc.");
                lvItem.SubItems.Add("");
                lvItem.Tag = TweakConsts.TweakValues.UninstallDefaultApps;
                lvWindowsTweaks.Items.Add(lvItem);
            }

            ShowHideInstallButtons();
        }

        private void ShowHideInstallButtons()
        {
            try
            {
                if( !DoesInstallFileExists("UltraVNC_X86_Setup.exe", "UltraVNC_X64_Setup.exe", "UltraVNC") ||
                    !DoesInstallFileExists("install_server.inf", "install_server.inf", "UltraVNC"))
                {
                    Logging.LogErrorMessage("UltraVNC installer(s) not found");
                    btnInstallUltraVNC.Enabled = false;
                }

                if (!DoesInstallFileExists("TeamViewerQS.exe", "TeamViewerQS.exe", "RCS TeamViewer QS"))
                {
                    Logging.LogErrorMessage("TeamViewerQS.exe not found");
                    btnInstallTeamViewer.Enabled = false;
                }

                if (!DoesInstallFileExists("TeamViewer_Host_Setup.exe", "TeamViewer_Host_Setup.exe", "RCS TeamViewer Host"))
                {
                    Logging.LogErrorMessage("TeamViewer_Host_Setup-idcnkgx5r6.exe not found");
                    btnInstallTeamViewer.Enabled = false;
                }

                if ( !DoesInstallFileExists("Sentinel System Driver Installer.exe", "Sentinel System Driver Installer.exe", "Sentinel Drivers") )
                {
                    Logging.LogErrorMessage("Sentinel System Driver installer not found");
                    btnInstallSentinelDrivers.Enabled = false;
                }
                if( !DoesInstallFileExists("ASICOMBO.exe", "ASICOMBO64.exe", "ASI Combo Drivers") )
                {
                    Logging.LogErrorMessage("ASICOMBO installer(s) not found");
                    btnInstallASIDrivers.Enabled = false;
                }
                if (!DoesInstallFileExists("processhacker-setup.exe", "processhacker-setup.exe", "Process Hacker"))
                {
                    Logging.LogErrorMessage("Process Hacker installer not found");
                    _btnProcessHacker.Enabled = false;
                }

                if (!_bPowershellWarningDisplayed)
                {
                    if (CheckForMinVersionOfPowerShell(false))
                    {
                        Logging.LogErrorMessage("PowerShell not found");
                        btnInstallPowerShell.Enabled = false;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking for install files!\n\nPlease check the Error Log File for More Details!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logging.LogErrorMessage("Error checking for install files");
                Logging.LogErrorMessage(ex.Message);
            }

        }

        private void btnRunTweaks_Click(object sender, EventArgs e)
        {
            bool bError = false;
            ListView.CheckedListViewItemCollection checkedItems = lvWindowsTweaks.CheckedItems;

            foreach (ListViewItem lvItem in checkedItems)
            {
                if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.DisableSystemRestore)
                {
                    if (SysOptions.DisableSystemRestore())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.EnableRDP)
                {
                    if( SysOptions.EnableRDP())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.DisableSystemSounds)
                {
                    if (SysOptions.DisableSystemSounds())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.DisableDefrag)
                {
                    if (SysOptions.DisableDefrag())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.DisableUAC)
                {
                    if (UserOptions.DisableUAC())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.SetLanManagerAuth)
                {
                    if (SysOptions.SetLanManagerAuth())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.EnableDesktopIcons)
                {
                    if (UserOptions.ShowDesktopIcons())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.DisableIPv6)
                {
                    if (DisableIPv6())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.DisableQoS)
                {
                    if (DisableQOSOnNics())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.DisableFlowControl)
                {
                    if (DisableFlowControlOnNics())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }

                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.DisableAutoPlay)
                {
                    if (SysOptions.DisableAutoPlay())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.DisablePowerMgmtOnNics)
                {
                    if (SysOptions.DisablePowerManagementOnNics())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.SetPowerOffMonitorToNever)
                {
                    if (SysOptions.DisablePowerOptionUsingPowerCfg("-change -monitor-timeout-ac 0"))
                    {
                        SysOptions.DisablePowerOptionUsingPowerCfg("-change -monitor-timeout-dc 0");

                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.SetPowerOffHDToNever)
                {
                    if (SysOptions.DisablePowerOptionUsingPowerCfg("-change -disk-timeout-ac 0"))
                    {
                        SysOptions.DisablePowerOptionUsingPowerCfg("-change -disk-timeout-dc 0");

                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.SetPowerOffPCToNever)
                {
                    if (SysOptions.DisableSleepForPC())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.SetOwnerForC)
                {
                    if (SysOptions.SetFolderOwner())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.DisableUIEffects)
                {
                    TweakUISettings UISettings = new TweakUISettings();

                    if (UISettings.SetDisplayOptions())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.SetAeroPeek)
                {
                    if (UISettings.DisableAeroPeek())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.SetSaveTaskbarThumbnailPreviews)
                {
                    if (UISettings.EnableSaveTaskbarThumbnailPreviews())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.DisableSyncTimeWithInternet)
                {
                    if (SysOptions.DisableSynchronizeTimeFromInternet())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.SetFirewallPermissions)
                {
                    if (SysOptions.SetFirewallPermissions())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.SetAdvancedFirewallPermissions)
                {
                    if (SysOptions.SetAdvancedFirewall())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.CreateEnableLinkedConnections)
                {
                    if (SysOptions.CreateEnableLinkedConnections())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.DisableConsentPromptBehaviorAdmin)
                {
                    if (SysOptions.DisableConsentPromptBehaviorAdmin())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.DisableConsentPromptBehaviorUser)
                {
                    if (SysOptions.DisableConsentPromptBehaviorUser())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.SetExeToRunAsAdministrator)
                {
                    if (SysOptions.SetExeToRunAsAdministrator())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.SetWindowsUpdatesToDisable)
                {
                    if (SysOptions.SetWindowsUpdatesToDisable())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.CreateRCSUserAndSetToAutoLogon)
                {
                    if (SysOptions.CreateRCSUserAndSetToAutoLogon())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.DisableSecureTime)
                {
                    if (SysOptions.DisableSecureTime())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                //else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.DisableShowSuggestions)
                //{
                //    if (UserOptions.DisableShowSuggestions())
                //    {
                //        lvItem.SubItems[2].Text = "Success!";
                //    }
                //    else
                //    {
                //        bError = true;
                //        lvItem.SubItems[2].Text = "Error!";
                //    }
                //}
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.DisableShowSuggestions)
                {
                    if (UserOptions.DisableAppNotifications(_bWindows10))
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.UninstallDefaultApps)
                {
                    if (SysOptions.UninstallDefaultApps())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.DisableActionCenterSidebar)
                {
                    if (SysOptions.DisableActionCenterSidebar())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }
                else if ((TweakConsts.TweakValues)lvItem.Tag == TweakConsts.TweakValues.DisableNotificationCenter)
                {
                    if (UserOptions.DisableNotificationCenter())
                    {
                        lvItem.SubItems[2].Text = "Success!";
                    }
                    else
                    {
                        bError = true;
                        lvItem.SubItems[2].Text = "Error!";
                    }
                }

                lvItem.Checked = true;
            }

            if (bError)
            {
                MessageBox.Show("Processing Completed with Errors!\n\nPlease check error file for more details.", "Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Processing Completed!\n\nPlease Reboot the Computer.");
            }
        }


        private bool DisableIPv6()
        {
            try
            {
                return SysOptions.DisableTCPIP6OnNics();
            }
            catch (Win32Exception ex)
            {
                Logging.LogErrorMessage("Error Disabling IPv6");
                Logging.LogErrorMessage(ex.Message);
            }

            return false;
        }

        private bool DisableQOSOnNics()
        {
            try
            {
                return SysOptions.DisableQOSOnNics();
            }
            catch (Win32Exception ex)
            {
                Logging.LogErrorMessage("Error Disabling IPv6");
                Logging.LogErrorMessage(ex.Message);
            }

            return false;
        }

        private bool DisableFlowControlOnNics()
        {
            try
            {
                return SysOptions.DisableFlowControlOnNics();
            }
            catch (Win32Exception ex)
            {
                Logging.LogErrorMessage("Error Disabling IPv6");
                Logging.LogErrorMessage(ex.Message);
            }

            return false;
        }

        private void btnInstallTeamViewer_Click(object sender, EventArgs e)
        {
            if (Installers.InstallTeamViewer())
            {
                MessageBox.Show("Copy of TeamViewer Completed!", "Success", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Error Encountered During Copy!\n\nPlease Check Error Log File for More Details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInstallUltraVNC_Click(object sender, EventArgs e)
        {
            if (Installers.InstallUltraVNC())
            {
                MessageBox.Show("Install of UltraVNC Completed!", "Success", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Error Encountered During Install!\n\nPlease Check Error Log File for More Details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInstallSentinelDrivers_Click(object sender, EventArgs e)
        {
            if (Installers.InstallSentinelDriver())
            {
                MessageBox.Show("Install of Sentinel Drivers Completed!", "Success", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Error Encountered During Install!\n\nPlease Check Error Log File for More Details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInstallASIDrivers_Click(object sender, EventArgs e)
        {
            if (Installers.InstallASIComboDrivers())
            {
                MessageBox.Show("Install of ASI Combo Drivers Completed!", "Success", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Error Encountered During Install!\n\nPlease Check Error Log File for More Details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSetAutoLogin_Click(object sender, EventArgs e)
        {
            if (UserOptions.EnableAutoLogin(txtUserName.Text, txtPassword.Text, txtDomain.Text))
            {
                MessageBox.Show("Setting of AutoLogin Completed!", "Success", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Error Encountered During Setting of AutoLogin!\n\nPlease Check Error Log File for More Details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool DoesInstallFileExists(string s32BitInstaller, string s64BitInstaller, string sDescription)
        {
            try
            {
                var sInstaller = Application.StartupPath;

                if (!sInstaller.EndsWith(Convert.ToString(Path.DirectorySeparatorChar)))
                {
                    sInstaller += Path.DirectorySeparatorChar;
                }

                sInstaller += "Install\\";

                if (Environment.Is64BitOperatingSystem)
                {
                    sInstaller += s64BitInstaller;
                }
                else
                {
                    sInstaller += s32BitInstaller;
                }

                if (File.Exists(sInstaller))
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking for installer for  " + sDescription + "!\n\nPlease check the Error Log File for More Details!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logging.LogErrorMessage("DoesInstallFileExists - " + sDescription);
                Logging.LogErrorMessage(ex.Message);
            }
            return false;
        }

        private bool CheckForMinVersionOfPowerShell(bool displayError)
        {
            try
            {
                string PSVersion = RegistrySettings.FindHKLMRegistrValueString(@"SOFTWARE\Microsoft\PowerShell\3\PowerShellEngine", "PowerShellVersion");
                if( String.IsNullOrEmpty(PSVersion))
                {
                    PSVersion = RegistrySettings.FindHKLMRegistrValueString(@"SOFTWARE\Microsoft\PowerShell\2\PowerShellEngine", "PowerShellVersion");
                }
                if (String.IsNullOrEmpty(PSVersion))
                {
                    PSVersion = RegistrySettings.FindHKLMRegistrValueString(@"SOFTWARE\Microsoft\PowerShell\1\PowerShellEngine", "PowerShellVersion");
                }

                if (String.IsNullOrEmpty(PSVersion))
                {
                    return false;
                }


                if (_bWindows10)
                {
                    float PSVersionValue = Convert.ToSingle(PSVersion);

                    if (PSVersionValue < 5.0)
                    {
                        if (displayError)
                        {
                            MessageBox.Show("PowerShell 5.0 is Required for Windows8.1/Server 2012!\n\nPlease use the Install PowerShell button below", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            _bPowershellWarningDisplayed = true;
                        }

                        return false;
                    }
                }
                else if ( Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor >= 2)
                {
                    float PSVersionValue = Convert.ToSingle(PSVersion);

                    //if (PSVersionValue < 4.0)
                    //{
                    //    if (displayError)
                    //    {
                    //        MessageBox.Show("PowerShell 4.0 is Required for Windows8.1/Server 2012!\n\nPlease use the Install PowerShell button below", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    }

                    //    return false;
                    //}

                    if( PSVersionValue < 5.0)
                    {
                        MessageBox.Show("PowerShell 5.0 is Available for Windows8.1/Server 2012!\n\nPlease use the Install PowerShell button below", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _bPowershellWarningDisplayed = true;
                    }
                }
                else if (Environment.OSVersion.Version.Major == 6 && Environment.OSVersion.Version.Minor == 1)
                {
                    float PSVersionValue = Convert.ToSingle(PSVersion);

                    //if (PSVersionValue < 3.0)
                    //{
                    //    if (displayError)
                    //        MessageBox.Show("PowerShell 3.0 is Required for Windows7/Server 2008 R2!\n\nPlease use the Install PowerShell button below", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //    return false;
                    //}

                    if (PSVersionValue < 5.0)
                    {
                        MessageBox.Show("PowerShell 5.0 is Available for Windows7.1/Server 2012!\n\nPlease use the Install PowerShell button below", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _bPowershellWarningDisplayed = true;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                if (displayError )
                    MessageBox.Show("Error checking for version of PowerShell!\n\nPlease check the Error Log File for More Details!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logging.LogErrorMessage("CheckForMinVersionOfPowerShell");
                Logging.LogErrorMessage(ex.Message);
            }
            return false;

        }

        public bool IsWindows10OrGreater()
        {
            string OSVersion = RegistrySettings.FindHKLMRegistryValueDWord(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion", "CurrentMajorVersionNumber");
            if (!String.IsNullOrEmpty(OSVersion))
            {
                if (OSVersion.Equals("10"))
                {
                    return true;
                }
            }

            return false;
        }
        private void btnInstallPowerShell_Click(object sender, EventArgs e)
        {
            Installers.InstallPowerShell();
        }

        private void lvWindowsTweaks_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if( 0 == e.Column )
            {
                if (!clicked)
                {
                    clicked = true;

                    foreach (ListViewItem item in lvWindowsTweaks.Items)
                    {
                        item.Checked = true;
                    }

                    Invalidate();
                }
                else
                {
                    clicked = false;
                    Invalidate();

                    foreach (ListViewItem item in lvWindowsTweaks.Items)
                    {
                        item.Checked = false;
                    }
                }
            }
        }

        private void _radioNexGen_CheckedChanged(object sender, EventArgs e)
        {
            if (_radioNexGen.Checked)
            {
                RefreshListView(SoftwareApplication.NexGen);
                _radioZetta.Checked = false;
                _radioNone.Checked = false;
            }
        }

        private void _radioZetta_CheckedChanged(object sender, EventArgs e)
        {
            if (_radioZetta.Checked)
            {
                RefreshListView(SoftwareApplication.Zetta);
                _radioNexGen.Checked = false;
                _radioNone.Checked = false;
            }

        }

        private void _radioNone_CheckedChanged(object sender, EventArgs e)
        {
            if (_radioNone.Checked)
            {
                RefreshListView(SoftwareApplication.None);
                _radioNexGen.Checked = false;
                _radioZetta.Checked = false;
            }
        }

        private void _btnProcessHacker_Click(object sender, EventArgs e)
        {
            Installers.InstallProcessHacker();
        }

        private void _btnSysInternalsSuite_Click(object sender, EventArgs e)
        {
            Installers.InstallSysInternalsSuite();
        }

        private void btnRCSTVHost_Click(object sender, EventArgs e)
        {
            if (Installers.InstallTeamViewerHost())
            {
                MessageBox.Show("Copy of TeamViewer Completed!", "Success", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Error Encountered During Copy!\n\nPlease Check Error Log File for More Details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}