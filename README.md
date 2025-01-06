# How to Run the Application

Follow these steps to run the application and set up the bot integration:

## 1. Launch the Application using Docker Compose

The project already includes docker-compose.yml and Dockerfile. Follow these steps to launch the application:

## 1. Install and Set Up ngrok

To expose your local server to the internet, you need to use ngrok.

### Step 1: Download ngrok
- **Download ngrok** from [official website](https://ngrok.com/download).
- Follow the installation instructions for your operating system.

### Step 2: Open Your Terminal

- Open a terminal or command prompt and navigate to the directory where you downloaded ngrok.

### Step 3: Retrieve the Local Application URL
- In your project, open the file `Properties/launchSettings.json`.
- Find the `"applicationUrl"` property. It should look like this:

  ```json
  "applicationUrl": "http://localhost:5132"
  
### Step 4: Run ngrok

- Open your terminal and enter the following command to start ngrok:

  ```bash
  ngrok http http://localhost:5132
- Copy the forwarding URL shown in the terminal (e.g., https://2ae1-146-120-162-35.ngrok-free.app).

### Step 5: Update the Webhook URL in Telegram
After setting up ngrok, you need to update the webhook URL for the Telegram bot.

## 2. Modify the Webhook URL
- In the terminal, you should have the **forwarding URL** from ngrok (e.g., `https://2ae1-146-120-162-35.ngrok-free.app`).
- Replace `{forwarding_url}` in the following link with the ngrok URL you copied:

  ```plaintext
  https://api.telegram.org/bot7825825837:AAH8Q3DmW7yf9rJWzqJo08ndX619Z25tM9I/setWebhook?url={forwarding_url}/api/bot
  
## 3. Update the Connection String to SQL Server

To connect your application to your own SQL Server database, follow these steps:

### Step 1: Open the `Program.cs` File
- Navigate to the `Program.cs` file in your project directory.

### Step 2: Update the Connection String
- Locate the connection string used for SQL Server in the file.
- Replace the existing connection string with your own, pointing to your database. For example:

  ```csharp
  builder.Services.AddDbContext<ApplicationDbContext>(options =>
      options.UseSqlServer("Your_Connection_String_Here"));
  
## 4. Launch the Application

To run the application, you can use one of the following methods:

### Option 1: Using Visual Studio
- Open the project in **Visual Studio**.
- Press `F5` or click on **Start** to build and launch the application.

### Option 2: Using Visual Studio Code
- Open the project in **Visual Studio Code**.
- Press `Ctrl + Shift + P` and select **.NET: Run** to start the application.

### Option 3: Using the Terminal
- Open a terminal or command prompt.
- Navigate to the project directory.
- Run the application using the following command:

  ```bash
  dotnet run

# How to Run the Application using Docker Compose

Follow these steps to run the application and set up the bot integration:

### Step 1: Build and Start Containers

  - Open a terminal in the project root where the docker-compose.yml file is located.
  - Run the following command:
  ````bash
  docker-compose up --build
````

### Step 2: Verify the Application
   - After the containers are running, the application should be accessible at: http://notifymaster.client:4000/

## 2. Install and Set Up ngrok

To expose your local server to the internet, you need to use ngrok.

### Step 1: Download ngrok
- **Download ngrok** from [official website](https://ngrok.com/download).
- Follow the installation instructions for your operating system.

### Step 2: Open Your Terminal

- Open a terminal or command prompt and navigate to the directory where you downloaded ngrok.

### Step 3: Retrieve the Application URL 
  ```json
  "BaseAddress": "http://notifymaster.webapi:5000/"
```

### Step 4: Run ngrok

- Open your terminal and enter the following command to start ngrok:

  ```bash
  ngrok http http://notifymaster.webapi:5000/
- Copy the forwarding URL shown in the terminal (e.g., https://2ae1-146-120-162-35.ngrok-free.app).


