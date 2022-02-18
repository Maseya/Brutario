namespace Brutario
{
    using Brutario.Smb1;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public partial class ObjectListWindow : Form
    {
        public ObjectListWindow()
        {
            InitializeComponent();
            CurrentPage = 0;
        }

        public MainForm MainForm
        {
            get
            {
                return Owner as MainForm;
            }
        }

        private int CurrentPage
        {
            get;
            set;
        }

        public void Add(AreaObjectCommand command)
        {
            var hex = $"{command.Value1:X2} {command.Value2:X2}";
            if (command.Size == 3)
            {
                hex += $" {command.Value3:X2}";
            }

            if (command.ScreenFlag)
            {
                CurrentPage++;
            }

            var position = $"{command.X:X1},{command.Y:X1}";
            var type = command.Command.ToString();

            _ = lvwObjects.Items.Add(new ListViewItem(new string[] {
                hex,
                CurrentPage.ToString(),
                position,
                type}));
        }

        public void AddRange(IEnumerable<AreaObjectCommand> commands)
        {
            var items = new List<ListViewItem>();
            foreach (var command in commands)
            {
                var hex = $"{command.Value1:X2} {command.Value2:X2}";
                if (command.Size == 3)
                {
                    hex += $" {command.Value3:X2}";
                }

                if (command.ScreenFlag)
                {
                    CurrentPage++;
                }

                if (command.Code == AreaObjectCode.ScreenSkip)
                {
                    CurrentPage = command.BaseCommand;
                }

                var position = $"{command.X:X1},{command.Y:X1}";
                var type = command.Code.ToString();

                items.Add(new ListViewItem(new string[] {
                    hex,
                    CurrentPage.ToString(),
                    position,
                    type}));
            }

            lvwObjects.Items.AddRange(items.ToArray());
        }

        public void Clear()
        {
            CurrentPage = 0;
            lvwObjects.Items.Clear();
        }

        private void lvwObjects_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvwObjects.SelectedItems.Count != 1)
            {
                return;
            }

            using var dialog = new ObjectEditorForm();
            dialog.AreaObjectCommand = MainForm.Smb1GameData.CurrentObjectData[
                lvwObjects.SelectedIndices[0]];
            dialog.UseManual = true;
            if (dialog.ShowDialog(owner: this) == DialogResult.OK)
            {
                MainForm.Smb1GameData.CurrentObjectData[
                lvwObjects.SelectedIndices[0]] = dialog.AreaObjectCommand;
                MainForm.Smb1GameData.RenderAreaTilemap();
                MainForm.LoadBG1();
            }
        }
    }
}
