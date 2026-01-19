using SonicRetro.SonLVL.API;
using System;
using System.IO;
using System.Windows.Forms;

namespace SonicRetro.SonLVL.GUI
{
	public partial class NewModDialog : Form
	{
		public NewModDialog()
		{
			InitializeComponent();
			switch (LevelData.Game.RSDKVer)
			{
				case EngineVersion.V3:
					checkBox1.Text = "Disable Save INI Override";
					checkBox1.Visible = true;
					forceSonic1.Visible = false;
					break;
				case EngineVersion.V4:
					checkBox1.Text = "Skip Start Menu";
					checkBox1.Visible = true;
					forceSonic1.Visible = true;
					break;
				default:
					checkBox1.Visible = false;
					forceSonic1.Visible = false;
					break;
			}
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			string moddir = Path.Combine(Path.Combine(LevelData.EXEFolder, "mods"), ValidateFilename(textModName.Text));

			if (textModName.Text.Length <= 0)
			{
				MessageBox.Show("You can't have a mod without a name.", "Invalid mod name", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			try
			{
				if (Directory.Exists(moddir))
				{
					MessageBox.Show("A mod with that name already exists."
					                + "\nPlease choose a different name or rename the existing one.", "Mod already exists",
						MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					return;
				}

				Directory.CreateDirectory(moddir);

				ModInfo newMod = new ModInfo
				{
					Name              = textModName.Text,
					Author            = textModAuthor.Text,
					Description       = textModDescription.Text,
					Version           = textVersion.Text,
					TxtScripts        = loadTextScripts.Checked,
					DisableFocusPause = disableFocusPause.Checked ? "1" : "0",
					RedirectSaveRAM   = redirectSave.Checked
				};

				switch (LevelData.Game.RSDKVer)
				{
					case EngineVersion.V3:
						newMod.DisableSaveIniOverride = checkBox1.Checked;
						newMod.TargetVersion = "3";
						break;
					case EngineVersion.V4:
						newMod.SkipStartMenu = checkBox1.Checked;
						newMod.ForceSonic1 = forceSonic1.Checked;
						newMod.TargetVersion = "4";
						break;
				}

				ModFile = Path.Combine(moddir, "mod.ini");
				IniSerializer.Serialize(newMod, ModFile);

				Directory.CreateDirectory(Path.Combine(moddir, "Data"));

				if (useGameXml.Checked)
				{
					Directory.CreateDirectory(Path.Combine(moddir, "Data/Game"));
					new GameXML().Save(Path.Combine(moddir, "Data/Game/game.xml"));
				}

				if (checkOpenFolder.Checked)
				{
					System.Diagnostics.Process.Start(moddir);
				}

				DialogResult = DialogResult.OK;
				Close();
			}
			catch (Exception error)
			{
				MessageBox.Show(this, error.Message, "Mod Creation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public string ModFile { get; private set; }

		static string ValidateFilename(string filename)
		{
			string result = filename;

			foreach (char c in Path.GetInvalidFileNameChars())
				result = result.Replace(c, '_');

			return result;
		}
	}
}
