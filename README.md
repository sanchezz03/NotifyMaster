How to Run the Application
Follow these steps to run the application and set up the bot integration:

1. Install and Set Up ngrok
Download ngrok from https://ngrok.com/download.
Open your terminal and navigate to the folder where ngrok is located.
Retrieve the application URL from your launchSettings.json file:
json
Copy code
"applicationUrl": "http://localhost:5132"
Run ngrok by entering the following command in the terminal:
bash
Copy code
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
