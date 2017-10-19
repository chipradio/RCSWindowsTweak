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
using System.IO.Compression;

namespace WindowsTweak
{
	class InstallOptions
	{
		[DllImport( "kernel32.dll" )]
		private static extern bool GetVersionEx( ref OSVERSIONINFOEX osVersionInfo );

		[StructLayout( LayoutKind.Sequential )]
		private struct OSVERSIONINFOEX
		{
			public int dwOSVersionInfoSize;
			public int dwMajorVersion;
			public int dwMinorVersion;
			public int dwBuildNumber;
			public int dwPlatformId;
			[MarshalAs( UnmanagedType.ByValTStr, SizeConst = 128 )]
			public string szCSDVersion;
			public short wServicePackMajor;
			public short wServicePackMinor;
			public short wSuiteMask;
			public byte wProductType;
			public byte wReserved;
		}

		SystemOptions SysOptions = new SystemOptions();

		public InstallOptions()
		{

		}

		public bool InstallUltraVNC()
		{
			try
			{
				var sUltraVNCServerINFFile = Application.StartupPath;
				if (!sUltraVNCServerINFFile.EndsWith(Convert.ToString(Path.DirectorySeparatorChar)))
				{
					sUltraVNCServerINFFile += Path.DirectorySeparatorChar;
				}
				sUltraVNCServerINFFile += "install_server.inf";

				return PerformInstall("UltraVNC_X86_Setup.exe", "UltraVNC_X64_Setup.exe", " /SP- /VERYSILENT /NORESTART /LOADINF=" + sUltraVNCServerINFFile, "UltraVNC");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error installing UltraVNC!\n\nPlease check the Error Log File for More Details!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Logging.LogErrorMessage("Error installing UltraVNC");
				Logging.LogErrorMessage(ex.Message);
			}
			return false;
		}

		public bool InstallSentinelDriver()
		{
			try
			{
				return PerformInstall("Sentinel System Driver Installer.exe", "Sentinel System Driver Installer.exe", "/v\"/q\"", "Sentinel Drivers");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error installing Sentinel Drivers!\n\nPlease check the Error Log File for More Details!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Logging.LogErrorMessage("Error installing Sentinel Drivers");
				Logging.LogErrorMessage(ex.Message);
			}
			return false;
		}

		public bool InstallTeamViewer()
		{
			try
			{
				string sInstaller = Application.StartupPath;

				if (!sInstaller.EndsWith(Convert.ToString(Path.DirectorySeparatorChar)))
				{
					sInstaller += Path.DirectorySeparatorChar;
				}

				sInstaller += "Install\\";

				sInstaller += "TeamViewerQS.exe";

				string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
				if (!desktopPath.EndsWith(Convert.ToString(Path.DirectorySeparatorChar)))
				{
					desktopPath += Path.DirectorySeparatorChar;
				}
				desktopPath  += "TeamViewerQS.exe";

				File.Copy(sInstaller, desktopPath);

				if( File.Exists(desktopPath))
				{
					return true;
				}

				//return PerformInstall("TeamViewer_Setup.exe", "TeamViewer_Setup.exe", "/MSI /S", "TeamViewer");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error installing TeamViewer!\n\nPlease check the Error Log File for More Details!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Logging.LogErrorMessage("Error installing TeamViewer");
				Logging.LogErrorMessage(ex.Message);
			}
			return false;
		}

        public bool InstallTeamViewerHost()
        {
            try
            {
                return PerformInstall("TeamViewer_Host_Setup.exe", "TeamViewer_Host_Setup.exe", "/MSI /S", "RCS TeamViewer Host");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error installing TeamViewer Host!\n\nPlease check the Error Log File for More Details!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logging.LogErrorMessage("Error installing TeamViewer Host");
                Logging.LogErrorMessage(ex.Message);
            }
            return false;
        }
        public bool InstallASIComboDrivers()
		{
			try
			{
				return PerformInstall("ASICOMBO.exe", "ASICOMBO64.exe", "/MSI /S", "ASI Combo Drivers");
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error installing ASI Combo Drivers!\n\nPlease check the Error Log File for More Details!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Logging.LogErrorMessage("Error installing ASI Combo Drivers");
				Logging.LogErrorMessage(ex.Message);
			}
			return false;
		}

        public bool IsWindows8Or2012()
        {
            OperatingSystem OS = Environment.OSVersion;
            OSVERSIONINFOEX osVersionInfo = new OSVERSIONINFOEX();
            osVersionInfo.dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX));
            GetVersionEx(ref osVersionInfo);

            if( osVersionInfo.dwMajorVersion == 6 && osVersionInfo.dwMinorVersion >= 2)
            { 
                return true;
            }

            return false;
        }

        public bool IsWindows7Or2008R2()
        {
            OperatingSystem OS = Environment.OSVersion;
            OSVERSIONINFOEX osVersionInfo = new OSVERSIONINFOEX();
            osVersionInfo.dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX));
            GetVersionEx(ref osVersionInfo);

            if (osVersionInfo.dwMajorVersion == 6 && osVersionInfo.dwMinorVersion == 1)
            {
                return true;
            }

            return false;
        }

        public bool IsWindows10()
        {
            OperatingSystem OS = Environment.OSVersion;
            OSVERSIONINFOEX osVersionInfo = new OSVERSIONINFOEX();
            osVersionInfo.dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX));
            GetVersionEx(ref osVersionInfo);

            if (osVersionInfo.dwMajorVersion == 10)
            {
                return true;
            }

            return false;
        }

        public bool InstallProcessHacker()
        {
            try
            {
                return PerformInstall("processhacker-setup.exe", "processhacker-setup.exe", "/MSI /S", "Process Hacker");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error installing Process Hacker!\n\nPlease check the Error Log File for More Details!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logging.LogErrorMessage("Error installing AProcess Hacker");
                Logging.LogErrorMessage(ex.Message);
            }
            return false;
        }

        public bool InstallPowerShell()
		{
			try
			{
				OperatingSystem OS = Environment.OSVersion;
				OSVERSIONINFOEX osVersionInfo = new OSVERSIONINFOEX();
				osVersionInfo.dwOSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX));
				GetVersionEx(ref osVersionInfo);

				var sInstaller = Application.StartupPath;

				if (!sInstaller.EndsWith(Convert.ToString(Path.DirectorySeparatorChar)))
				{
					sInstaller += Path.DirectorySeparatorChar;
				}

				sInstaller += "Install\\";

                bool bPerformInstall = false;


                //PowerShell 5.0 Installs = https://msdn.microsoft.com/en-us/powershell/wmf/5.0/install
                //OSVERSIONINFOEX struction = https://msdn.microsoft.com/en-us/library/windows/desktop/ms724833(v=vs.85).aspx
                if ((OS.Platform == PlatformID.Win32NT) )
				{
                    if( OS.Version.Major == 6 )
                    {
					    switch (OS.Version.Minor)
					    {
						    case 1:
							    {
								    if (osVersionInfo.wProductType == 1)
								    {
									    if (Environment.Is64BitOperatingSystem)
									    {
                                            sInstaller += "Win7AndW2K8R2-KB3134760-x64.msu";
                                            bPerformInstall = true;
									    }
									    else
									    {
                                            sInstaller += "Win7-KB3134760-x86.msu";
                                            bPerformInstall = true;
                                        }
								    }
								    else if (osVersionInfo.wProductType == 3)
								    {
                                        sInstaller += "Win7AndW2K8R2-KB3134760-x64.msu";
                                        bPerformInstall = true;
								    }
							    }
							    break;
                            case 2:
                                {
                                    if ((osVersionInfo.wProductType == 3) && (Environment.Is64BitOperatingSystem))
                                    {
                                        sInstaller += "W2K12-KB3134759-x64.msu";
                                        bPerformInstall = true;
                                    }
                                }
                                break;

                            case 3:
							    {
                                    if (osVersionInfo.wProductType == 1)
                                    {
                                        if (Environment.Is64BitOperatingSystem)
                                        {
                                            sInstaller += "Win8.1AndW2K12R2-KB3134758-x64.msu";
                                            bPerformInstall = true;
                                        }
                                        else
                                        {
                                            sInstaller += "Win8.1-KB3134758-x86.msu";
                                            bPerformInstall = true;
                                        }
                                    }
                                    else if (osVersionInfo.wProductType == 3)
                                    {
                                        sInstaller += "Win8.1AndW2K12R2-KB3134758-x64.msu";
                                        bPerformInstall = true;
                                    }
                                }
                                break;
                        
						    default:
							    break;
					    }
                    }
                }
                else if( OS.Version.Major == 10)
                {

                }

                if ( !bPerformInstall )
                {
                    return false;
                }

                MessageBox.Show(sInstaller);

                return SysOptions.PerformProcess("wusa.exe", "\"" + sInstaller + "\"" );
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error installing PowerShell!\n\nPlease check the Error Log File for More Details!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Logging.LogErrorMessage("Error installing PowerShell");
				Logging.LogErrorMessage(ex.Message);
			}
			return false;
		}

        public bool InstallSysInternalsSuite()
        {
            try
            {
                var sInstaller = Application.StartupPath;
                if (!sInstaller.EndsWith(Convert.ToString(Path.DirectorySeparatorChar)))
                {
                    sInstaller += Path.DirectorySeparatorChar;
                }

                sInstaller += "Install\\";
                sInstaller += "SysinternalsSuite.zip";

                var extractPath = @"c:\\Install\\sysinternals\\";

                ZipFile.ExtractToDirectory(sInstaller, extractPath);

                using (RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    key.SetValue("BGInfo", @"C:\Install\sysinternals\Bginfo.exe rcs.bgi / silent / timer:0 / nolicpromp");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error unzipping SysInternals Suite!\n\nPlease check the Error Log File for More Details!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logging.LogErrorMessage("Error unzipping SysInternals Suite");
                Logging.LogErrorMessage(ex.Message);
            }
            return false;
        }

        private bool PerformInstall(string s32BitInstaller, string s64BitInstaller, string sArguments, string sDescription)
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

				if (!File.Exists(sInstaller))
				{
					string message = "Install file for " + sDescription + " was not found!";
					MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return false;
				}

				SysOptions.PerformProcess(sInstaller, sArguments);

				return true;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error installing " + sDescription + "!\n\nPlease check the Error Log File for More Details!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Logging.LogErrorMessage("Error installing ASI Combo Drivers");
				Logging.LogErrorMessage(ex.Message);
			}
			return false;
		}

		public bool DoesInstallFileExists(string s32BitInstaller, string s64BitInstaller, string sDescription)
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

	}
}
