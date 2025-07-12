# 📋 planEdge - Task Manager Application

**planEdge** is a full-stack Task Manager application built with **Angular**, **ASP.NET Core Web API**, and **MongoDB**. It helps users manage tasks, reminders, and deadlines efficiently.

---

## 🚀 Features

- ✅ Create, edit, complete, and delete tasks
- 📅 Set due dates and reminder times
- 🏷️ Add priorities, categories, and tags
- 🔔 Email notifications for reminders
- 🎤 Voice input using Web Speech API
- 📄 Export tasks to CSV and PDF
- 🌙 Toggle between Light/Dark mode
- 📱 Simulate mobile view
- 📬 User authentication with JWT

---

## 🛠️ Tech Stack

| Layer     | Technology              |
|-----------|--------------------------|
| Frontend  | Angular 17, TypeScript   |
| Backend   | ASP.NET Core Web API     |
| Database  | MongoDB                  |
| Other     | Toastr, jsPDF, XLSX, Bootstrap, FileSaver, SMTP email |

---

## 📁 Project Structure

TaskManagerSolution/
├── TaskManager.API/ # ASP.NET Core API
├── taskmanager-frontend/ # Angular UI
├── docker-compose.yml # Optional Docker setup
├── TaskManagerSolution.sln # Solution file
└── README.md # This file



---

## ▶️ Getting Started

### 🔧 Backend Setup

```bash
cd TaskManager.API
dotnet restore
dotnet run

Update appsettings.json with your MongoDB connection string and SMTP settings.

🌐 Frontend Setup
bash
Copy
Edit
cd taskmanager-frontend
npm install
ng serve
📬 Email Reminder Setup
Email reminders are sent using SMTP (e.g., Gmail, SendGrid). Configure your credentials in:

bash
Copy
Edit
TaskManager.API/appsettings.json
json
Copy
Edit
"SmtpSettings": {
  "Host": "smtp.gmail.com",
  "Port": 587,
  "EnableSSL": true,
  "UserName": "your_email@gmail.com",
  "Password": "your_app_password"
}
🧾 Export Formats
CSV: Excel-compatible list

PDF: Printable task summary

🙋‍♂️ Author
Deepu T John
📧 deeputjohn@gmail.com
🔗 GitHub Profile

📃 License
This project is licensed under the MIT License.
