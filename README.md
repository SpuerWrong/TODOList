
# To-Do List
## 项目简介 | Project Overview
这是一个使用C#和WPF（Windows Presentation Foundation）开发的简单待办事项管理应用。用户可以通过该应用轻松管理任务，支持添加、编辑、删除等基本操作。

This is a simple To-Do List application developed in C# using Windows Presentation Foundation (WPF). It allows users to manage their tasks with features like adding, editing, and deleting tasks.

## 功能特点 | Features
- 添加任务：输入任务名称、描述、截止日期及优先级。

- 编辑任务：修改任务的详细信息，包括描述和截止日期。

- 删除任务：删除已完成或不再需要的任务。

- 任务排序：按截止日期或优先级排序任务。

- 保存任务：任务数据会保存到本地文件，关闭应用后再次打开时数据仍然存在。

- 模式切换：支持浅色模式和深色模式之间的切换。
---
- Add Task: Add a new task with name, description, due date, and priority.

- Edit Task: Modify task details, such as description and due date.

- Delete Task: Remove completed or unnecessary tasks.

- Task Sorting: Sort tasks by due date or priority.

- Save Tasks: Tasks are saved locally and persist after closing the application.

- Mode Switching: Toggle between Light and Dark mode.
---
## 使用技术 | Technologies Used
C#: 应用程序的主要编程语言。
C#: The primary programming language used for application logic.

WPF (Windows Presentation Foundation): 用于构建用户界面。
WPF: Used for building the user interface.

XAML: 用于UI布局设计。
XAML: Used for designing the UI layout.

Visual Studio: 开发和调试的集成开发环境。
Visual Studio: The integrated development environment for building and debugging the application.

##安装与运行 | Installation and Usage
前提条件 | Prerequisites
.NET Framework 4.7.2 或更高版本
.NET Framework 4.7.2 or higher
Visual Studio 2019/2022
步骤 | Steps
克隆仓库 | Clone the repository:
```bash
git clone https://github.com/your-username/TODOList.git
```
打开项目 | Open the project: 在Visual Studio中打开项目文件（.sln）。
Open the .sln file in Visual Studio.

安装依赖 | Install dependencies: 使用NuGet管理器安装缺失的包（例如Newtonsoft.Json）。
Install missing NuGet packages such as Newtonsoft.Json.

编译与运行 | Build and Run: 按F5或点击Start按钮来运行应用程序。
Press F5 or click Start to run the application.

使用说明 | Usage
添加任务：输入任务信息，点击“添加”按钮。

编辑任务：选择任务后点击“更新”按钮，修改任务后保存。

删除任务：选择任务后点击“删除”按钮。

保存数据：点击“保存”按钮保存任务列表。

模式切换：在浅色模式与深色模式之间切换。

Add Task: Enter task details and click the "Add" button.

Edit Task: Select a task, click "Update", modify, and save.

Delete Task: Select a task and click "Delete".

Save Data: Click the "Save" button to store tasks.

Mode Switching: Switch between Light and Dark modes.

## 文件结构 | File Structure
MainWindow.xaml：定义UI布局。
MainWindow.xaml: Defines the UI layout.

MainWindow.xaml.cs：包含应用逻辑和事件处理。
MainWindow.xaml.cs: Contains the application logic and event handling.

Taskma.cs：任务类，定义任务的属性。
Taskma.cs: The task class defining properties such as task name, description, due date, and priority.

tasks.json：存储任务数据的文件。
tasks.json: The file where tasks are stored in JSON format.

## 自定义 | Customization
扩展字段：你可以在Taskma类中添加更多字段（如标签、分类等）。
Add Additional Fields: Add more fields to the Taskma class like tags or categories.

数据库存储：将JSON存储替换为数据库以支持更多数据。
Database Storage: Replace JSON storage with a database for larger datasets.

任务提醒：增加提醒功能，通知用户任务即将到期。
Task Notifications: Add notifications to alert users of upcoming tasks.

## 贡献 | Contributing
欢迎贡献！请提交Pull Request。
Contributions are welcome! Please submit a pull request.

## 许可证 | License
本项目使用MIT许可证。
This project is licensed under the MIT License.

With this dual-language README, you can now better understand the project in both Chinese and English!

