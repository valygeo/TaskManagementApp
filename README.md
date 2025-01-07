# TaskManagementApp  

## Purpose  
This application is designed to help manage tasks within a company.  

### Features  
- Create projects.  
- Assign team members to projects.  
- Organize projects into sprints, tasks, and PBIs.  

This structure ensures efficient task tracking and collaboration across teams.  

---

## Prerequisites  
Before running the application, make sure you have the following installed:  
- **Database:** SQL Server Express and SQL Server Management Studio (SSMS).  
- **Backend:** .NET SDK and Visual Studio.  
- **Frontend:** Node.js, Angular CLI, and Visual Studio Code.  

---

## Installation Instructions  

### 1. Setting up the Database  
1. Install SQL Server Express and SSMS.  
2. Open SSMS and run the scripts located in the following file:  
   [Database Scripts](DatabaseScripts/Database.txt).  

### 2. Setting up the Backend  
1. Install the .NET SDK and Visual Studio.  
2. Open the solution file located at:  
   [TaskManagementApp.sln](Backend/TaskManagementApp/TaskManagementApp.sln).  
3. Build and run the solution in Visual Studio.  

### 3. Setting up the Frontend  
1. Install Node.js, Angular CLI, and Visual Studio Code.  
2. Open the frontend directory in Visual Studio Code:  
   [Frontend Source Code](Frontend/TaskManagementApp/src/).  
3. Open a terminal and run the following command:  

   ```bash
   ng serve

 4. Access the application in your browser at http://localhost:4200.   
