using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroFramework;
using MetroFramework.Forms;
using System.Windows.Forms;


namespace CourseScheduler
{
	public partial class Interface : MetroFramework.Forms.MetroForm
	{
		List<Course> fullCourseList;

		private Stack<Stack<Course>> courses;
		BackgroundWorker bw = new BackgroundWorker();
		string[] courseCodes;

		public Interface()
		{
			InitializeComponent();
		}

		private void CourseAddRemoveButton_Click(object sender, EventArgs e)
		{
			if (SelectedCoursesListBox.Items.Count < 5)
			{
				if (SelectedCoursesListBox.FindStringExact(CourseComboBox.SelectedItem.ToString(), 0) == -1)
				{
					SelectedCoursesListBox.Items.Add(CourseComboBox.SelectedItem.ToString());
				}
				else
				{
					MetroFramework.MetroMessageBox.Show(this, "You cannot add the same course twice!", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			else
			{
				MetroFramework.MetroMessageBox.Show(this, "You cannot have more than 5 courses!", "Max Courses", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void CourseComboBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
			{
				if (SelectedCoursesListBox.Items.Count < 5)
				{
					if (SelectedCoursesListBox.FindStringExact(CourseComboBox.SelectedItem.ToString(), 0) == -1)
					{
						SelectedCoursesListBox.Items.Add(CourseComboBox.SelectedItem.ToString());
					}
					else
					{
						MetroFramework.MetroMessageBox.Show(this, "You cannot add the same course twice!", "Duplicate", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}
				else
				{
					MetroFramework.MetroMessageBox.Show(this, "You cannot have more than 5 courses!", "Max Courses", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SelectedCoursesListBox.Items.RemoveAt(SelectedCoursesListBox.SelectedIndex);
		}

		private void OpenFileButton_Click(object sender, EventArgs e)
		{
			if (SelectedCoursesListBox.Items.Count != 0)
			{
				SelectedCoursesListBox.Items.Clear();
			}
			CourseComboBox.Enabled = false;
			CourseAddRemoveButton.Enabled = false;
			ProcessButton.Enabled = false;
			Stream fileStream = null;
			OpenFileDialog fileDialog = new OpenFileDialog();
			if (fileDialog.ShowDialog() == DialogResult.OK)
			{
				try
				{
					if ((fileStream = fileDialog.OpenFile()) != null)
					{
						using (fileStream)
						{
							StreamReader newFileStream = new StreamReader(fileStream);
							fullCourseList = Utils.parseFile(newFileStream);
							MetroFramework.MetroMessageBox.Show(this, "File successsfully opened", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
							CourseComboBox.Enabled = true;
							CourseAddRemoveButton.Enabled = true;
							ProcessButton.Enabled = true;
							foreach (string courseName in fullCourseList.Select(x => x.courseName).Distinct())
							{
								CourseComboBox.Items.Add(courseName);
							}
							CourseComboBox.SelectedIndex = 0;
						}
					}
				}
				catch (Exception ex)
				{
					string s = String.Concat("Error: ", ex.Message);
					MetroFramework.MetroMessageBox.Show(this, s, "File Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void SelectedCoursesListBox_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				if (SelectedCoursesListBox.SelectedIndex != -1)
				{
					CourseSelectedListBoxContextMenu.Show();
				}
				else
				{
					SelectedCoursesListBox.SelectedIndex = SelectedCoursesListBox.IndexFromPoint(e.X, e.Y);
					CourseSelectedListBoxContextMenu.Show();
				}
			}
		}

		private async void ProcessButton_Click(object sender, EventArgs e)
		{
			if (SelectedCoursesListBox.Items.Count == 0)
			{
				MetroFramework.MetroMessageBox.Show(this, "Cannot process with no courses.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			else if (SelectedCoursesListBox.Items.Count < 5)
			{
				System.Windows.Forms.DialogResult result = MetroFramework.MetroMessageBox.Show(this, "You have less than 5 courses, are you sure you want to continue?", "Are you sure?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
				if (result != DialogResult.Yes)
				{
					return;
				}
			}
			ProcessButton.Enabled = false;
			ProgressSpinner.Visible = true;
			ProgressSpinner.Spinning = true;
			ProgressBar.Visible = true;
			var progressHandler = new Progress<int>(value => 
			{
				ProgressBar.Value = value;
			});
			var progress = progressHandler as IProgress<int>;
			await Task.Run(() =>
			{
				courseCodes = new string[5];
				for (var i = 0; i < 5; i++)
				{
					courseCodes[i] = SelectedCoursesListBox.Items[i].ToString();
				}

				List<Course>[,] selectedCoursesAndSections = new List<Course>[5, 3];
				for (var i = 0; i < 5; i++)
				{
					for (var j = 0; j < 3; j++)
					{
						selectedCoursesAndSections[i, j] = new List<Course>();
						selectedCoursesAndSections[i, j] = fullCourseList.FindAll(delegate(Course classToCheck)
						{
							if (j == 0)
							{
								return (String.Equals(classToCheck.courseName, courseCodes[i]) && (String.Equals(classToCheck.courseType, "LEC")));
							}
							else if (j == 1)
							{
								return (String.Equals(classToCheck.courseName, courseCodes[i]) && (String.Equals(classToCheck.courseType, "TUT")));
							}
							else
							{
								return (String.Equals(classToCheck.courseName, courseCodes[i]) && (String.Equals(classToCheck.courseType, "PRA")));
							}
						});
					}
				}
				courses = Utils.creativelyOrganizeClasses(selectedCoursesAndSections, courseCodes, progress);
			});
			ProgressSpinner.Spinning = false;
			ProgressSpinner.Visible = false;
			ProgressBar.Visible = false;
			ProcessButton.Enabled = true;

			int totalArrangements = courses.Count;
			Stack<Course>[] arrangements = new Stack<Course>[totalArrangements];
			for (var i = 0; i < totalArrangements; i++)
			{
				arrangements[i] = courses.Pop();
			}

			//FIND A WAY TO DISPLAY THING HERE
			if (totalArrangements == 1)
			{//Only one thing, create a single grid
				//Initialize single grid
				DataGridView dataGridView1 = new DataGridView();
				dataGridView1.AllowUserToAddRows = false;
				dataGridView1.AllowUserToDeleteRows = false;
				dataGridView1.AllowUserToResizeColumns = false;
				dataGridView1.AllowUserToResizeRows = false;
				dataGridView1.BackgroundColor = System.Drawing.Color.White;
				dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
				dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
				dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
				dataGridView1.ColumnCount = 5;
				dataGridView1.RowsDefaultCellStyle.WrapMode = DataGridViewTriState.True;
				dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
				foreach (DataGridViewColumn column in dataGridView1.Columns)
				{
					column.SortMode = DataGridViewColumnSortMode.NotSortable;
				}
				dataGridView1.Location = new System.Drawing.Point(294, 50);
				dataGridView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
				dataGridView1.MaximumSize = new System.Drawing.Size(804, 524);
				dataGridView1.Name = "dataGridView1";
				dataGridView1.ReadOnly = true;
				dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
				dataGridView1.RowTemplate.Height = 25;
				dataGridView1.RowTemplate.ReadOnly = true;
				dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
				dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
				dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
				dataGridView1.Size = new System.Drawing.Size(644, 422);
				dataGridView1.RowCount = 10;
				dataGridView1.Rows[0].HeaderCell.Value = "9:00AM";
				dataGridView1.Rows[1].HeaderCell.Value = "10:00AM";
				dataGridView1.Rows[2].HeaderCell.Value = "11:00AM";
				dataGridView1.Rows[3].HeaderCell.Value = "12:00PM";
				dataGridView1.Rows[4].HeaderCell.Value = "1:00PM";
				dataGridView1.Rows[5].HeaderCell.Value = "2:00PM";
				dataGridView1.Rows[6].HeaderCell.Value = "3:00PM";
				dataGridView1.Rows[7].HeaderCell.Value = "4:00PM";
				dataGridView1.Rows[8].HeaderCell.Value = "5:00PM";
				dataGridView1.Rows[9].HeaderCell.Value = "6:00PM";
				Controls.Add(dataGridView1);
				//For each class, add to grid
				foreach (Course course in arrangements[0])
				{
					foreach (Class classTime in course.lectures)
					{
						int row = (int)classTime.startTime - 8;
						int column;
						switch (classTime.day)
						{
							case "Mon":
								column = 0;
								break;
							case "Tue":
								column = 1;
								break;
							case "Wed":
								column = 2;
								break;
							case "Thu":
								column = 3;
								break;
							case "Fri":
								column = 4;
								break;
							default:
								column = 0;
								break;
						}
						if (dataGridView1.Rows[row].Cells[column].Value != null)
						{
							for (var i = 0; i < classTime.duration; i++)
							{
								dataGridView1.Rows[row].Cells[column].Value = dataGridView1.Rows[row].Cells[column].Value + "\n" + classTime.className;
								DataGridViewCellStyle style = new DataGridViewCellStyle();
								style.BackColor = MetroColors.Magenta;
								dataGridView1.Rows[row].Cells[column].Style = style;
								dataGridView1.Rows[row].Height += 9;
								row++;
							}
						}
						else
						{
							for (var i = 0; i < classTime.duration; i++)
							{
								dataGridView1.Rows[row].Cells[column].Value = classTime.className;
								row++;
							}
						}
					}
				}
				string[] daysOfTheWeek = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
				for (var i = 0; i < 5; i++)
				{
					dataGridView1.Columns[i].HeaderText = daysOfTheWeek[i];
				}
			}
			else
			{//Multiple versions, create tabs
				while (true) ;
			}

			MetroFramework.MetroMessageBox.Show(this, "", "Completed", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
