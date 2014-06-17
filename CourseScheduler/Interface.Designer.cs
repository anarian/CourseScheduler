namespace CourseScheduler
{
	partial class Interface
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.CourseComboBox = new MetroFramework.Controls.MetroComboBox();
			this.CourseAddRemoveButton = new MetroFramework.Controls.MetroButton();
			this.CourseSelectedListBoxContextMenu = new MetroFramework.Controls.MetroContextMenu(this.components);
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SelectedCoursesListBox = new System.Windows.Forms.ListBox();
			this.ProcessButton = new MetroFramework.Controls.MetroButton();
			this.OpenFileButton = new MetroFramework.Controls.MetroButton();
			this.ProgressSpinner = new MetroFramework.Controls.MetroProgressSpinner();
			this.ProgressBar = new MetroFramework.Controls.MetroProgressBar();
			this.CourseSelectedListBoxContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// CourseComboBox
			// 
			this.CourseComboBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.CourseComboBox.Cursor = System.Windows.Forms.Cursors.Default;
			this.CourseComboBox.Enabled = false;
			this.CourseComboBox.FormattingEnabled = true;
			this.CourseComboBox.ItemHeight = 23;
			this.CourseComboBox.Location = new System.Drawing.Point(22, 138);
			this.CourseComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.CourseComboBox.Name = "CourseComboBox";
			this.CourseComboBox.Size = new System.Drawing.Size(147, 29);
			this.CourseComboBox.Style = MetroFramework.MetroColorStyle.Blue;
			this.CourseComboBox.TabIndex = 4;
			this.CourseComboBox.Theme = MetroFramework.MetroThemeStyle.Light;
			this.CourseComboBox.UseSelectable = true;
			this.CourseComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CourseComboBox_KeyDown);
			// 
			// CourseAddRemoveButton
			// 
			this.CourseAddRemoveButton.Enabled = false;
			this.CourseAddRemoveButton.Location = new System.Drawing.Point(177, 138);
			this.CourseAddRemoveButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.CourseAddRemoveButton.Name = "CourseAddRemoveButton";
			this.CourseAddRemoveButton.Size = new System.Drawing.Size(89, 29);
			this.CourseAddRemoveButton.Style = MetroFramework.MetroColorStyle.Blue;
			this.CourseAddRemoveButton.TabIndex = 5;
			this.CourseAddRemoveButton.Text = "Add";
			this.CourseAddRemoveButton.Theme = MetroFramework.MetroThemeStyle.Light;
			this.CourseAddRemoveButton.UseSelectable = true;
			this.CourseAddRemoveButton.Click += new System.EventHandler(this.CourseAddRemoveButton_Click);
			// 
			// CourseSelectedListBoxContextMenu
			// 
			this.CourseSelectedListBoxContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteToolStripMenuItem});
			this.CourseSelectedListBoxContextMenu.Name = "CourseSelectedListBoxContextMenu";
			this.CourseSelectedListBoxContextMenu.ShowImageMargin = false;
			this.CourseSelectedListBoxContextMenu.Size = new System.Drawing.Size(83, 26);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(82, 22);
			this.deleteToolStripMenuItem.Text = "Delete";
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
			// 
			// SelectedCoursesListBox
			// 
			this.SelectedCoursesListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			this.SelectedCoursesListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.SelectedCoursesListBox.ContextMenuStrip = this.CourseSelectedListBoxContextMenu;
			this.SelectedCoursesListBox.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SelectedCoursesListBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.SelectedCoursesListBox.FormattingEnabled = true;
			this.SelectedCoursesListBox.ItemHeight = 30;
			this.SelectedCoursesListBox.Location = new System.Drawing.Point(22, 177);
			this.SelectedCoursesListBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.SelectedCoursesListBox.Name = "SelectedCoursesListBox";
			this.SelectedCoursesListBox.Size = new System.Drawing.Size(244, 240);
			this.SelectedCoursesListBox.TabIndex = 12;
			this.SelectedCoursesListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SelectedCoursesListBox_MouseDown);
			// 
			// ProcessButton
			// 
			this.ProcessButton.Enabled = false;
			this.ProcessButton.Location = new System.Drawing.Point(22, 427);
			this.ProcessButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.ProcessButton.Name = "ProcessButton";
			this.ProcessButton.Size = new System.Drawing.Size(244, 45);
			this.ProcessButton.TabIndex = 14;
			this.ProcessButton.Text = "Arrange Courses";
			this.ProcessButton.UseSelectable = true;
			this.ProcessButton.Click += new System.EventHandler(this.ProcessButton_Click);
			// 
			// OpenFileButton
			// 
			this.OpenFileButton.Location = new System.Drawing.Point(22, 83);
			this.OpenFileButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.OpenFileButton.Name = "OpenFileButton";
			this.OpenFileButton.Size = new System.Drawing.Size(244, 45);
			this.OpenFileButton.TabIndex = 15;
			this.OpenFileButton.Text = "Open File";
			this.OpenFileButton.UseSelectable = true;
			this.OpenFileButton.Click += new System.EventHandler(this.OpenFileButton_Click);
			// 
			// ProgressSpinner
			// 
			this.ProgressSpinner.Location = new System.Drawing.Point(552, 226);
			this.ProgressSpinner.Maximum = 100;
			this.ProgressSpinner.Name = "ProgressSpinner";
			this.ProgressSpinner.Size = new System.Drawing.Size(109, 105);
			this.ProgressSpinner.Spinning = false;
			this.ProgressSpinner.TabIndex = 16;
			this.ProgressSpinner.UseSelectable = true;
			this.ProgressSpinner.Value = 50;
			this.ProgressSpinner.Visible = false;
			// 
			// ProgressBar
			// 
			this.ProgressBar.Location = new System.Drawing.Point(294, 427);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(644, 45);
			this.ProgressBar.Step = 1;
			this.ProgressBar.TabIndex = 17;
			this.ProgressBar.Visible = false;
			// 
			// Interface
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(969, 508);
			this.Controls.Add(this.ProgressBar);
			this.Controls.Add(this.ProgressSpinner);
			this.Controls.Add(this.OpenFileButton);
			this.Controls.Add(this.ProcessButton);
			this.Controls.Add(this.SelectedCoursesListBox);
			this.Controls.Add(this.CourseAddRemoveButton);
			this.Controls.Add(this.CourseComboBox);
			this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.MaximizeBox = false;
			this.Name = "Interface";
			this.Padding = new System.Windows.Forms.Padding(27, 92, 27, 31);
			this.ShadowType = MetroFramework.Forms.MetroFormShadowType.SystemShadow;
			this.Text = "Course Scheduler";
			this.CourseSelectedListBoxContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private MetroFramework.Controls.MetroComboBox CourseComboBox;
		private MetroFramework.Controls.MetroButton CourseAddRemoveButton;
		private MetroFramework.Controls.MetroContextMenu CourseSelectedListBoxContextMenu;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.ListBox SelectedCoursesListBox;
		private MetroFramework.Controls.MetroButton ProcessButton;
		private MetroFramework.Controls.MetroButton OpenFileButton;
		private MetroFramework.Controls.MetroProgressSpinner ProgressSpinner;
		private MetroFramework.Controls.MetroProgressBar ProgressBar;
	}
}