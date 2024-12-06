# How to Run the Application

Follow these steps to run the application and set up the bot integration:

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

Run ngrok by entering the following command in the terminal:
ngrok http http://localhost:5132
Copy the forwarding URL shown in the terminal (e.g., https://2ae1-146-120-162-35.ngrok-free.app).
2. Update the Webhook URL in Telegram
Modify the webhook URL by replacing {forwarding_url} in the following link with the URL you copied in the previous step:
plaintext
Copy code
https://api.telegram.org/bot7825825837:AAH8Q3DmW7yf9rJWzqJo08ndX619Z25tM9I/setWebhook?url={forwarding_url}/api/bot
Example:
plaintext
Copy code
https://api.telegram.org/bot7825825837:AAH8Q3DmW7yf9rJWzqJo08ndX619Z25tM9I/setWebhook?url=https://2ae1-146-120-162-35.ngrok-free.app/api/bot
3. Update the Connection String to SQL Server
Open the Program.cs file in the project.
Update the SQL Server connection string to point to your own database.
4. Launch the Application
Run the application using your preferred method (e.g., Visual Studio, Visual Studio Code, or via the terminal with dotnet run).
5. Interact with the Telegram Bot
Go to Telegram and search for the bot by its username: @nfymaster_bot.
Start the bot and interact with it.
