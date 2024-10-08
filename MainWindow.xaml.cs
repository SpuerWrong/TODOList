﻿using System;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using System.Data;
using Newtonsoft.Json;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;
using Newtonsoft.Json;


namespace TODOList
{
   
    public partial class MainWindow : Window
    {
        private ObservableCollection<Taskma> taskma;
        int savebutton = 0;
        int addbutton = 0;
        public MainWindow()
        {
            InitializeComponent();
            taskma = new ObservableCollection<Taskma>();
            listTodo.ItemsSource = taskma;
            DateEnter.SelectedDate = DateTime.Now;
            LoadDataFromJson("..\\tasks.json");
            DataContext = this;
            
        }
        // Window_Loaded 事件处理程序
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // 获取打开动画
            Storyboard openAnimation = (Storyboard)this.Resources["OpenAnimation"];
            openAnimation.Begin(this); // 确保传递当前窗口实例
        }

        private bool isClosing = false;

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (isClosing)
            {
                return; // Already closing, ignore further close requests
            }

            // Start the closing process
            isClosing = true;

            // Get the closing animation
            Storyboard closeAnimation = (Storyboard)this.Resources["CloseAnimation"];
            closeAnimation.Begin(this); // Ensure the current window instance is passed

            // Delay the window closing
            e.Cancel = true;
            closeAnimation.Completed += (s, _) => this.Close();
        }



        private void LoadDataFromJson(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    var deserializedTasks = JsonConvert.DeserializeObject<ObservableCollection<Taskma>>(json);
                    taskma = deserializedTasks ?? new ObservableCollection<Taskma>();
                    listTodo.ItemsSource = taskma;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading tasks: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                taskma = new ObservableCollection<Taskma>();
                listTodo.ItemsSource = taskma;
            }
        }


        private void SaveDataToJson(string filePath)
        {
            try
            {
                string json = JsonConvert.SerializeObject(taskma, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving tasks: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Addbtn_Click(object sender, RoutedEventArgs e)
        {
            if(TaskEntry.Text != "")
            {
                taskma.Add(new Taskma() { TaskName = TaskEntry.Text, Des = TaskDes.Text, DOD = DateEnter.SelectedDate, Priority = priorityM.Text, priin = priorityM.SelectedIndex});
                addbutton += 1;
                TaskEntry.Clear();
                TaskDes.Clear();
                DateEnter.SelectedDate = DateTime.Now;
                priorityM.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("First Enter Task into Box to Add","Alert!",MessageBoxButton.OK);
            }
            
        }

        private void updatebtn_Click(object sender, RoutedEventArgs e)
        {
            if(listTodo.SelectedItem != null)
            {
                var ind = listTodo.SelectedItem;
                Taskma uplist = (Taskma)ind;
                TaskEntry.Text = uplist.TaskName;
                TaskDes.Text = uplist.Des;
                DateEnter.SelectedDate = uplist.DOD;
                priorityM.Text = uplist.Priority;
                priorityM.SelectedIndex = uplist.priin;
                taskma.Remove((Taskma)ind);
            }
            else
            {
                MessageBox.Show("First Select Task to update", "Alert!", MessageBoxButton.OK);
            }
            
        }

        private void Updatebtn_Click(object sender, RoutedEventArgs e)
        {
            if (listTodo.SelectedItem != null)
            {
                var selectedTask = listTodo.SelectedItem as Taskma;
                if (selectedTask != null)
                {
                    // Save the current state to allow rollback in case of cancellation
                    string originalTaskName = selectedTask.TaskName;
                    string originalDes = selectedTask.Des;
                    DateTime? originalDOD = selectedTask.DOD;
                    string originalPriority = selectedTask.Priority;
                    int originalPriorityIndex = selectedTask.priin;

                    // Update the fields
                    TaskEntry.Text = selectedTask.TaskName;
                    TaskDes.Text = selectedTask.Des;
                    DateEnter.SelectedDate = selectedTask.DOD;
                    priorityM.Text = selectedTask.Priority;
                    priorityM.SelectedIndex = selectedTask.priin;

                    // Confirm update
                    if (MessageBox.Show("Do you want to update this task?", "Confirm Update", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        // Remove and re-add to trigger the UI update
                        taskma.Remove(selectedTask);
                        taskma.Add(new Taskma()
                        {
                            TaskName = TaskEntry.Text,
                            Des = TaskDes.Text,
                            DOD = DateEnter.SelectedDate,
                            Priority = priorityM.Text,
                            priin = priorityM.SelectedIndex
                        });
                    }
                    else
                    {
                        // Rollback the changes
                        selectedTask.TaskName = originalTaskName;
                        selectedTask.Des = originalDes;
                        selectedTask.DOD = originalDOD;
                        selectedTask.Priority = originalPriority;
                        selectedTask.priin = originalPriorityIndex;
                    }
                }
            }
            else
            {
                MessageBox.Show("First Select Task to update", "Alert!", MessageBoxButton.OK);
            }
        }


        private void SortByDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if(SortByDate.SelectedIndex == 1)
            {
                taskma = new ObservableCollection<Taskma>(taskma.OrderBy(Taskma => Taskma.DOD));
                listTodo.ItemsSource = taskma;
            }
            else if(SortByDate.SelectedIndex == 2)
            {
                taskma = new ObservableCollection<Taskma>(taskma.OrderByDescending(Taskma => Taskma.DOD));
                listTodo.ItemsSource = taskma;
            }

        }

        private void SortByPriority_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if(SortByPriority.SelectedIndex == 1)
            {
                taskma = new ObservableCollection<Taskma>(taskma.OrderBy(Taskma => Taskma.priin));
                listTodo.ItemsSource = taskma;
            }
            else if (SortByPriority.SelectedIndex == 2)
            {
                taskma = new ObservableCollection<Taskma>(taskma.OrderByDescending(Taskma => Taskma.priin));
                listTodo.ItemsSource = taskma;
            }
        }

        private void Clearbtn_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are Sure You want to Clear List", "Alert!", MessageBoxButton.YesNo);
            if(result == MessageBoxResult.Yes)
            {
                taskma.Clear();
                ClearJsonFile("..\\tasks.json");
                addbutton += 1;
            }
        }
        private void Deletebtn_Click(object sender, RoutedEventArgs e)
        {
            // 删除按钮的逻辑
            if (listTodo.SelectedItem != null)
            {
                taskma.Remove((Taskma)listTodo.SelectedItem);
                SaveDataToJson("..\\tasks.json");  // 假设您想在删除后保存更改
            }
        }

        private static void ClearJsonFile(string filePath)
        {
            try
            {
                // Open the file in write mode and truncate its content
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    // No need to write anything, just opening the file in FileMode.Create truncates it
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error clearing JSON file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Resetbtn_Click(object sender, RoutedEventArgs e)
        {
            TaskEntry.Clear();
            TaskDes.Clear();
            DateEnter.SelectedDate = DateTime.Now;
            priorityM.SelectedIndex = 0;
        }
        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            savebutton += 1;
            SaveDataToJson("..\\tasks.json");
            MessageBox.Show("Changes Saved", "Alert!", MessageBoxButton.OK);
        }


        public class Taskma
        {
            public string TaskName { get; set; }
            public string Des { get; set; }
            public DateTime? DOD { get; set; }
            public string Priority { get; set; }
            public int priin { get; set; }
        }

        private void modechange_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (modechange.SelectedIndex == 0)
            {
                lightmode();
            }
            else
            {
                darkmode();
            }
        }

        

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Drag the window when the title bar is clicked
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            // Check if there are unsaved changes by comparing addbutton and savebutton
            if (addbutton != 0 && savebutton == 0)
            {
                var result = MessageBox.Show("You have unsaved changes. Do you want to save them before exiting?", "Confirm Exit", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                {
                    // Save and then close the application
                    SaveDataToJson("..\\tasks.json");
                    Application.Current.Shutdown();  // Close the application
                }
                else if (result == MessageBoxResult.No)
                {
                    Application.Current.Shutdown();  // Close the application without saving
                }
                // If Cancel is selected, do nothing (i.e., do not close the application)
            }
            else
            {
                // No unsaved changes or no changes at all, just close the application
                Application.Current.Shutdown();
            }
        }


        private void max_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Normal;
        }

        private void mini_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void lightmode()
        {
            MainGrid.Background = Brushes.White; //mainwindow
            listTodo.Background = Brushes.White; //listview
            listTodo.Foreground = Brushes.Black;

            //label
            firstLabel.Foreground = Brushes.Black;
            secondLabel.Foreground = Brushes.Black;
            thirdLabel.Foreground = Brushes.Black;
            forthLabel.Foreground = Brushes.Black;
            fifthLabel.Foreground = Brushes.Black;
            mode.Foreground = Brushes.Black;

            //box entry
            TaskEntry.Background = Brushes.White;
            TaskDes.Background = Brushes.White;
            DateEnter.Background = Brushes.White;

            TaskEntry.Foreground = Brushes.Black;
            TaskDes.Foreground = Brushes.Black;


            //buttons
            Addbtn.Foreground = Brushes.Black;
            updatebtn.Foreground = Brushes.Black;
            Deletebtn.Foreground = Brushes.Black;
            Clearbtn.Foreground = Brushes.Black;
            Resetbtn.Foreground = Brushes.Black;
            SaveFile.Foreground = Brushes.Black;
            
            Addbtn.BorderBrush = Brushes.Black;
            updatebtn.BorderBrush = Brushes.Black;
            Deletebtn.BorderBrush = Brushes.Black;
            Clearbtn.BorderBrush = Brushes.Black;
            Resetbtn.BorderBrush = Brushes.Black;
            SaveFile.BorderBrush = Brushes.Black;

            Addbtn.Background = Brushes.White;
            updatebtn.Background = Brushes.White;
            Deletebtn.Background = Brushes.White;
            Clearbtn.Background = Brushes.White;
            Resetbtn.Background = Brushes.White;
            SaveFile.Background = Brushes.White;

            //title
            titlebar.Background = Brushes.White;
            titleName.Foreground = Brushes.Black;

            mini.Background = Brushes.White;
            max.Background = Brushes.White;
            close.Background = Brushes.White;

            mini.BorderBrush = Brushes.White;
            max.BorderBrush = Brushes.White;
            close.BorderBrush = Brushes.White;

            mini.Foreground = Brushes.Black;
            max.Foreground = Brushes.Black;
            close.Foreground = Brushes.Black;
            
            //combobox
            priorityM.Foreground = Brushes.Black;
            ToggleButton toggleButton4 = priorityM.Template.FindName("toggleButton", priorityM) as ToggleButton;
            if (toggleButton4 != null)
            {
                Border border = toggleButton4.Template.FindName("templateRoot", toggleButton4) as Border;
                if (border != null)
                    border.Background = Brushes.White;
            }
            priorityM.Resources.Remove(SystemColors.WindowBrushKey);
            priorityM.Resources.Add(SystemColors.WindowBrushKey, Brushes.White);

            SortByDate.Foreground = Brushes.Black;
            ToggleButton toggleButton1 = SortByDate.Template.FindName("toggleButton", SortByDate) as ToggleButton;
            if (toggleButton1 != null)
            {
                Border border = toggleButton1.Template.FindName("templateRoot", toggleButton1) as Border;
                if (border != null)
                    border.Background = Brushes.White;
            }
            SortByDate.Resources.Remove(SystemColors.WindowBrushKey);
            SortByDate.Resources.Add(SystemColors.WindowBrushKey, Brushes.White);

            SortByPriority.Foreground = Brushes.Black;
            ToggleButton toggleButton2 = SortByPriority.Template.FindName("toggleButton", SortByPriority) as ToggleButton;
            if (toggleButton2 != null)
            {
                Border border = toggleButton2.Template.FindName("templateRoot", toggleButton2) as Border;
                if (border != null)
                    border.Background = Brushes.White;
            }
            SortByPriority.Resources.Remove(SystemColors.WindowBrushKey);
            SortByPriority.Resources.Add(SystemColors.WindowBrushKey, Brushes.White);

            if (modechange.IsInitialized)
            {
                modechange.Foreground = Brushes.Black;
                ToggleButton toggleButton3 = modechange.Template.FindName("toggleButton", modechange) as ToggleButton;
                if (toggleButton3 != null)
                {
                    Border border = toggleButton3.Template.FindName("templateRoot", toggleButton3) as Border;
                    if (border != null)
                        border.Background = Brushes.White;
                }
                modechange.Resources.Remove(SystemColors.WindowBrushKey);
                modechange.Resources.Add(SystemColors.WindowBrushKey, Brushes.White);
            }
        }

        private void darkmode()
        {
            MainGrid.Background = Brushes.Black; //mainwindow
            listTodo.Background = Brushes.Black; //listview
            listTodo.Foreground = Brushes.White;

            //label
            firstLabel.Foreground = Brushes.White;
            secondLabel.Foreground = Brushes.White;
            thirdLabel.Foreground = Brushes.White;
            forthLabel.Foreground = Brushes.White;
            fifthLabel.Foreground = Brushes.White;
            mode.Foreground = Brushes.White;

            //box entry
            TaskEntry.Background = Brushes.Black;
            TaskDes.Background = Brushes.Black;
            DateEnter.Background = Brushes.Black;

            TaskEntry.Foreground = Brushes.White;
            TaskDes.Foreground = Brushes.White;



            //buttons
            Addbtn.Foreground = Brushes.White;
            updatebtn.Foreground = Brushes.White;
            Deletebtn.Foreground = Brushes.White;
            Clearbtn.Foreground = Brushes.White;
            Resetbtn.Foreground = Brushes.White;
            SaveFile.Foreground = Brushes.White;

            Addbtn.BorderBrush = Brushes.White;
            updatebtn.BorderBrush = Brushes.White;
            Deletebtn.BorderBrush = Brushes.White;
            Clearbtn.BorderBrush = Brushes.White;
            Resetbtn.BorderBrush = Brushes.White;
            SaveFile.BorderBrush = Brushes.White;

            Addbtn.Background = Brushes.Black;
            updatebtn.Background = Brushes.Black;
            Deletebtn.Background = Brushes.Black;
            Clearbtn.Background = Brushes.Black;
            Resetbtn.Background = Brushes.Black;
            SaveFile.Background = Brushes.Black;

            //title bar
            titlebar.Background = Brushes.Black;
            titleName.Foreground = Brushes.White;

            mini.Background = Brushes.Black;
            max.Background = Brushes.Black;
            close.Background = Brushes.Black;

            mini.BorderBrush = Brushes.Black;
            max.BorderBrush = Brushes.Black;
            close.BorderBrush = Brushes.Black;

            mini.Foreground = Brushes.White;
            max.Foreground = Brushes.White;
            close.Foreground = Brushes.White;

            //comboBox
            priorityM.Foreground = Brushes.White;
            ToggleButton toggleButton4 = priorityM.Template.FindName("toggleButton", priorityM) as ToggleButton;
            if (toggleButton4 != null)
            {
                Border border = toggleButton4.Template.FindName("templateRoot", toggleButton4) as Border;
                if (border != null)
                    border.Background = Brushes.Black;
            }
            priorityM.Resources.Remove(SystemColors.WindowBrushKey);
            priorityM.Resources.Add(SystemColors.WindowBrushKey, Brushes.Black);

            SortByDate.Foreground = Brushes.White;
            ToggleButton toggleButton1 = SortByDate.Template.FindName("toggleButton", SortByDate) as ToggleButton;
            if (toggleButton1 != null)
            {
                Border border = toggleButton1.Template.FindName("templateRoot", toggleButton1) as Border;
                if (border != null)
                    border.Background = Brushes.Black;
            }
            SortByDate.Resources.Remove(SystemColors.WindowBrushKey);
            SortByDate.Resources.Add(SystemColors.WindowBrushKey, Brushes.Black);

            SortByPriority.Foreground = Brushes.White;
            ToggleButton toggleButton2 = SortByPriority.Template.FindName("toggleButton", SortByPriority) as ToggleButton;
            if (toggleButton2 != null)
            {
                Border border = toggleButton2.Template.FindName("templateRoot", toggleButton2) as Border;
                if (border != null)
                    border.Background = Brushes.Black;
            }
            SortByPriority.Resources.Remove(SystemColors.WindowBrushKey);
            SortByPriority.Resources.Add(SystemColors.WindowBrushKey, Brushes.Black);

            if (modechange.IsInitialized)
            {
                modechange.Foreground = Brushes.White;
                ToggleButton toggleButton3 = modechange.Template.FindName("toggleButton", modechange) as ToggleButton;
                if (toggleButton3 != null)
                {
                    Border border = toggleButton3.Template.FindName("templateRoot", toggleButton3) as Border;
                    if (border != null)
                        border.Background = Brushes.Black;
                }
                modechange.Resources.Remove(SystemColors.WindowBrushKey);
                modechange.Resources.Add(SystemColors.WindowBrushKey, Brushes.Black);
            }
        }
    }
}
