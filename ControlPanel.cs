using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace pkStreamAssist
{
    public partial class ControlPanel : Form
    {
        public ControlPanel()
        {
            InitializeComponent();
            TSpecies = new[] {CB_TSpecies1, CB_TSpecies2, CB_TSpecies3, CB_TSpecies4, CB_TSpecies5, CB_TSpecies6};
            BSpecies = new[] {CB_BSpecies1, CB_BSpecies2, CB_BSpecies3, CB_BSpecies4, CB_BSpecies5, CB_BSpecies6};
            TForms = new[] {CB_TForm1, CB_TForm2, CB_TForm3, CB_TForm4, CB_TForm5, CB_TForm6};
            BForms = new[] {CB_BForm1, CB_BForm2, CB_BForm3, CB_BForm4, CB_BForm5, CB_BForm6};
            TUsed = new[] {CHK_TU1, CHK_TU2, CHK_TU3, CHK_TU4, CHK_TU5, CHK_TU6};
            BUsed = new[] {CHK_BU1, CHK_BU2, CHK_BU3, CHK_BU4, CHK_BU5, CHK_BU6};
            TFNT = new[] {CHK_TX1, CHK_TX2, CHK_TX3, CHK_TX4, CHK_TX5, CHK_TX6};
            BFNT = new[] {CHK_BX1, CHK_BX2, CHK_BX3, CHK_BX4, CHK_BX5, CHK_BX6};
            TStatus = new[] {CB_TStatus1, CB_TStatus2, CB_TStatus3, CB_TStatus4, CB_TStatus5, CB_TStatus6};
            BStatus = new[] {CB_BStatus1, CB_BStatus2, CB_BStatus3, CB_BStatus4, CB_BStatus5, CB_BStatus6};

            TPKM = new[] {PB_T1, PB_T2, PB_T3, PB_T4, PB_T5, PB_T6};
            BPKM = new[] {PB_B1, PB_B2, PB_B3, PB_B4, PB_B5, PB_B6};
            TRCount = new[] {L_TScore, L_BScore};

            defaultControlWhite = CB_TSpecies1.BackColor;

            foreach (var c in TSpecies) { c.DisplayMember = "Text"; c.ValueMember = "Value"; }
            foreach (var c in BSpecies) { c.DisplayMember = "Text"; c.ValueMember = "Value"; }
            CB_Lang.DataSource = new[]
            {
                "English", // ENG
                "日本語", // JPN
                "Français", // FRE
                "Italiano", // ITA
                "Deutsch", // GER
                "Español", // SPA
                "한국어", // KOR
            };
            CB_PKMPerBattle.SelectedIndex = 4;
            Setup = true;
            CB_Lang.SelectedIndex = 0; // triggers team wipe
        }

        private readonly TeamView viewer = new TeamView();
        private readonly string[] lang_val = { "en", "ja", "fr", "it", "de", "es", "ko", };
        private readonly string[] gendersymbols = { "♂", "♀", "-" };
        private readonly Color defaultControlWhite;
        private readonly ComboBox[] TSpecies;
        private readonly ComboBox[] BSpecies;
        private readonly ComboBox[] TForms, BForms;
        private readonly CheckBox[] TUsed, BUsed;
        private readonly CheckBox[] TFNT, BFNT;
        private readonly ComboBox[] TStatus, BStatus;
        private readonly PictureBox[] TPKM, BPKM;
        private readonly Label[] TRCount;
        private readonly bool Setup;

        private bool resetting;
        private string[] specieslist, types, forms = { };
        private List<Util.cbItem> SpeciesDataSource;
        private string curlanguage = "en";

        private void clickMenu(object sender, EventArgs e)
        {
            var parent = (sender as ToolStripItem)?.GetCurrentParent();
            bool top = (parent as ContextMenuStrip)?.SourceControl == GB_Top;
            bool soft = sender == mnu_Reset; // don't clear species

            resetTeam(top, soft);
        }
        private void resetTeam(bool top, bool soft)
        {
            if (top)
                resetTeam(soft, TB_TTrainer, TSpecies, TForms, TUsed, TFNT, TStatus);
            else
                resetTeam(soft, TB_BTrainer, BSpecies, BForms, BUsed, BFNT, BStatus);
            
            TRCount[top ? 0 : 1].Text = "";
        }
        private void resetTeam(bool soft, TextBox Trainer, ComboBox[] Species, ComboBox[] Formes, CheckBox[] Used, CheckBox[] Faint, ComboBox[] Status)
        {
            resetting = true;

            if (!soft)
                Trainer.Text = "";
            for (int i = 0; i < 6; i++)
            {
                if (!soft)
                    Species[i].SelectedIndex = 0;
                int species = Util.getIndex(Species[i]);

                if (Legal.getIsBattleForm(species))
                    Formes[i].SelectedIndex = 0;

                Used[i].Checked = false;
                Faint[i].Checked = false;
                Status[i].SelectedIndex = 0;
            }

            resetting = false;
        }
        private void validateComboBox(object sender, EventArgs e)
        {
            if (!(sender is ComboBox))
                return;

            ComboBox cb = (ComboBox)sender;
            cb.SelectionLength = 0;
            cb.BackColor = cb.SelectedValue == null ? Color.DarkSalmon : defaultControlWhite;
        }
        private void changeLanguage(object sender, EventArgs e)
        {
            if (CB_Lang.SelectedIndex < 8)
                curlanguage = lang_val[CB_Lang.SelectedIndex];

            string l = curlanguage;
            forms = Util.getStringList("Forms", l);
            types = Util.getStringList("Types", l);

            // Update Species
            specieslist = Util.getStringList("Species", l);
            specieslist[0] = "---";
            SpeciesDataSource = Util.getCBList(specieslist, null);

            foreach (var c in TSpecies)
                c.DataSource = new BindingSource(SpeciesDataSource, null);
            foreach (var c in BSpecies)
                c.DataSource = new BindingSource(SpeciesDataSource, null);
            
            // Update Fields
            resetTeam(soft: false, top: true);
            resetTeam(soft: false, top: false);
        }
        private void changeUpdate(object sender, EventArgs e)
        {
            if (!CHK_Update.Checked)
                return;

            for (int i = 0; i < 6; i++)
            { updatePB(i, true); updatePB(i, false); }
            changeTRText(TB_TTrainer, e);
            changeTRText(TB_BTrainer, e);
            changePKMPerBattle(null, null);
        }
        private void changeOpacity(object sender, EventArgs e)
        {
            changeUpdate(null, null);
        }
        private void changePKMPerBattle(object sender, EventArgs e)
        {
            if (!CHK_Update.Checked)
                return;
            TRCount[0].Text = (CB_PKMPerBattle.SelectedIndex - TFNT.Count(c => c.Checked)).ToString();
            TRCount[1].Text = (CB_PKMPerBattle.SelectedIndex - BFNT.Count(c => c.Checked)).ToString();
        }
        private void changeTRText(object sender, EventArgs e)
        {
            if (!Setup)
                return;
            bool top = sender as TextBox == TB_TTrainer;
            string text = (sender as TextBox).Text;
            string fn = (top ? "top" : "bot") + "Trainer.txt";
            new Thread(() =>
            {
                int ctr = 0; 
                trywrite:
                try { File.WriteAllText(fn, text, Encoding.UTF8); }
                catch { ctr++; if (ctr < 10) { Thread.Sleep(100); goto trywrite; } }
            }).Start();
        }
        private void changeScore(object sender, EventArgs e)
        {
            if (!Setup)
                return;
            bool top = sender as Label == L_TScore;
            string text = (sender as Label).Text;
            string fn = (top ? "top" : "bot") + "Score.txt";
            new Thread(() =>
            {
                int ctr = 0;
                trywrite:
                try { File.WriteAllText(fn, text, Encoding.UTF8); }
                catch { ctr++; if (ctr < 10) { Thread.Sleep(100); goto trywrite; } }
            }).Start();
        }

        private void updateSpecies(object sender, EventArgs e)
        {
            bool top = TSpecies.Contains(sender as ComboBox);
            int index = Array.IndexOf(top ? TSpecies : BSpecies, sender as ComboBox);
            int species = Util.getIndex((top ? TSpecies : BSpecies)[index]);
            (top ? TForms : BForms)[index].DataSource = PKX.getFormList(species, types, forms, gendersymbols, 7).ToList();
            updateControls(index, top);
            updatePB(Array.IndexOf(top ? TSpecies : BSpecies, sender as ComboBox), top);
        }

        private void updateForm(object sender, EventArgs e)
        {
            bool top = TForms.Contains(sender as ComboBox);
            updatePB(Array.IndexOf(top ? TForms : BForms, sender as ComboBox), top);
        }
        private void updateUsed(object sender, EventArgs e)
        {
            bool top = TUsed.Contains(sender as CheckBox);
            int index = Array.IndexOf(top ? TUsed : BUsed, sender as CheckBox);
            updateControls(index, top);
            updatePB(Array.IndexOf(top ? TUsed : BUsed, sender as CheckBox), top);
        }
        private void updateFNT(object sender, EventArgs e)
        {
            bool top = TFNT.Contains(sender as CheckBox);
            int index = Array.IndexOf(top ? TFNT : BFNT, sender as CheckBox);
            changePKMPerBattle(null, null);
            updateControls(index, top);
            updatePB(Array.IndexOf(top ? TFNT : BFNT, sender as CheckBox), top);
        }
        private void updateStatus(object sender, EventArgs e)
        {
            bool top = TStatus.Contains(sender as ComboBox);
            updatePB(Array.IndexOf(top ? TStatus : BStatus, sender as ComboBox), top);
        }

        private void updateControls(int index, bool top)
        {
            int species = Util.getIndex((top ? TSpecies : BSpecies)[index]);
            (top ? TForms : BForms)[index].Enabled = (top ? TForms : BForms)[index].Items.Count > 1;
            if (species < 1)
            {
                (top ? TUsed : BUsed)[index].Checked = false;
                (top ? TFNT : BFNT)[index].Checked = false;
                (top ? TStatus : BStatus)[index].SelectedIndex = 0;
            }
            (top ? TUsed : BUsed)[index].Enabled = species > 0;
            if (!(top ? TUsed : BUsed)[index].Checked)
            {
                (top ? TFNT : BFNT)[index].Checked = false;
            }
            (top ? TFNT : BFNT)[index].Enabled = species > 0 && (top ? TUsed : BUsed)[index].Checked;
            (top ? TStatus : BStatus)[index].Enabled = species > 0 && (top ? TUsed : BUsed)[index].Checked && !(top ? TFNT : BFNT)[index].Checked;
        }
        private void updatePB(int slot, bool top)
        {
            if (!CHK_Update.Checked)
                return;
            if (resetting)
            { (top ? TPKM : BPKM)[slot].Image = null; goto save; }

            int species = Util.getIndex((top ? TSpecies : BSpecies)[slot]);
            int form = (top ? TForms : BForms)[slot].SelectedIndex;

            bool used = (top ? TUsed : BUsed)[slot].Checked;
            bool faint = (top ? TFNT : BFNT)[slot].Checked;
            int status = (top ? TStatus : BStatus)[slot].SelectedIndex;

            (top ? TPKM : BPKM)[slot].Image = getSlotImage(species, form, used, faint, status, top);
            save:
            if (!Setup)
                return;
            // Save Image
            using (MemoryStream ms = new MemoryStream())
            {
                //error will throw from here
                ((top ? TPKM : BPKM)[slot].Image ?? Properties.Resources.nb).Save(ms, ImageFormat.Png);
                byte[] data = ms.ToArray();
                string fn = (top ? "top" : "bot") + (slot + 1) + ".png";
                new Thread(() =>
                {
                    int ctr = 0;
                    trywrite:
                    try { File.WriteAllBytes(fn, data); }
                    catch { ctr++; if (ctr < 10) { Thread.Sleep(100); goto trywrite; } }
                }).Start();
            }

            // Update Viewer
            updateViewer();
        }
        private Bitmap getSlotImage(int species, int form, bool used, bool faint, int status, bool top)
        {
            Bitmap img = PKX.getSprite(species, form);

            if (!used && species > 0)
                img = Util.ChangeOpacity(img, (float)NUD_PCT.Value / 100);

            Bitmap finalimg = new Bitmap(50, 40);
            using (Graphics gr = Graphics.FromImage(finalimg))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                gr.DrawImage(img, new Point(5, 4));

                var p = new Pen(top ? Color.Red : Color.Blue, 3);
                var f = new Pen(top ? Color.FromArgb(200, 255, 0, 0) : Color.FromArgb(200, 0, 0, 255), 2);

                if (used)
                    gr.DrawRectangle(p, 1, 1, 50 - p.Width / 2 - 1.5f, 40 - p.Width / 2 - 1.5f);
                if (faint)
                {
                    gr.DrawLine(f, 0, 0, 50, 40);
                    gr.DrawLine(f, 0, 40, 50, 0);
                }
            }
            if (status > 0 && !faint)
            {
                Image sprite = (Image)Properties.Resources.ResourceManager.GetObject("s" + status + "_" + curlanguage);
                finalimg = Util.LayerImage(finalimg, sprite, 0, 30, 1);
            }
            return finalimg;
        }
        private void B_View_Click(object sender, EventArgs e)
        {
            viewer.Show();
            viewer.BringToFront();
        }
        private void updateViewer()
        {
            // Concat all images
            Bitmap finalimg = new Bitmap(50*6, 50*2);
            using (Graphics gr = Graphics.FromImage(finalimg))
            {
                for (int i = 0; i < 6; i++)
                {
                    gr.DrawImage(TPKM[i].Image ?? Properties.Resources._0, 0.5f + i * 50, 5);
                    gr.DrawImage(BPKM[i].Image ?? Properties.Resources._0, 0.5f + i * 50, 55);
                }
                for (int i = 1; i <= 5; i++)
                {
                    gr.DrawLine(new Pen(Color.DarkGray, 2), 0.125f + i* (float)finalimg.Width/6, 0, 0.125f + i*(float)finalimg.Width/6, finalimg.Height);
                }
                gr.DrawLine(new Pen(Color.Black, 4), 0, (float)finalimg.Height / 2, finalimg.Width, (float)finalimg.Height / 2);
            }
            viewer.PB_Teams.Image = TeamView.ResizeImage(viewer.img = finalimg, new Size(viewer.PB_Teams.Width - 2, viewer.PB_Teams.Height - 2));
        }
    }
}
