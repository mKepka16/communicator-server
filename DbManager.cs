using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using communicator_server.DataTypes;
using MySql.Data.MySqlClient;

namespace communicator_server
{
    class DbManager
    {
        static string host = "localhost";
        static string username = "root";
        static string password = "";
        static string database = "kemik";

        MySqlConnection connection = new MySqlConnection(); 
        MySqlCommand command = new MySqlCommand();
        DataTable dataTable = new DataTable();
        MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
        MySqlDataReader dataReader;
        DataSet dataSet = new DataSet();

        public DbManager()
        {
            connection.ConnectionString = $"server={host};user id={username};password={password};database={database}";
        }

        public void Connect()
        {
            dataTable = new DataTable();
            command = new MySqlCommand();
            connection.Open();
            command.Connection = connection;
        }

        public bool IsExist(string nick)
        {
            Connect();

            command.CommandText = $"SELECT id FROM users WHERE nick=\"{nick}\"";
            dataReader = command.ExecuteReader();
            dataTable.Load(dataReader);
            bool isExist = dataTable.Rows.Count > 0;

            CloseConnetion();
            return isExist; 
        }

        public void InsertUser(RegisterData registerData)
        {
            Connect();

            command.CommandText = $"INSERT INTO users (nick, password) VALUES (\"{registerData.nick}\", \"{registerData.password}\")";
            dataReader = command.ExecuteReader();

            CloseConnetion();
        }

        public bool IsExist(string nick, string password, out int id)
        {
            Connect();

            command.CommandText = $"SELECT id FROM users WHERE nick=\"{nick}\" AND password=\"{password}\"";
            dataReader = command.ExecuteReader();
            dataTable.Load(dataReader);
            bool isExist = dataTable.Rows.Count > 0;
            id = 0;
            if(isExist)
            {
                id = int.Parse(dataTable.Rows[0]["id"].ToString());
            }

            CloseConnetion();
            return isExist; 

        }

        public bool IsExist(NewChatData newChatData)
        {
            string chatName = $@"'{newChatData.ChatName}'";
            if(newChatData.Nicks.Count < 3)
            {
                string nick1 = newChatData.Nicks[0].Nick;
                string nick2 = newChatData.Nicks[1].Nick;
                chatName = $@"'{nick1},{nick2}', '{nick2},{nick1}'";
            }

            Connect();

            command.CommandText = $@"SELECT id FROM chats WHERE name IN ({chatName})";
            dataReader = command.ExecuteReader();
            dataTable.Load(dataReader);
            bool isExist = dataTable.Rows.Count > 0;

            CloseConnetion();
            return isExist;
        }

        public List<ChatListItemData> GetUserChats(UserData user)
        {
            Connect();
            Console.WriteLine("Connection started");

            List<ChatListItemData> chats = new List<ChatListItemData>();

            command.CommandText = $"SELECT c.id, c.name, members.numberOfMembers FROM chats c INNER JOIN chat_user cu ON c.id = cu.id_chat INNER JOIN(SELECT COUNT(id_chat) as 'numberOfMembers', id_chat FROM chat_user GROUP BY id_chat) members ON c.id = members.id_chat WHERE cu.id_user = \'{user.id}\'";
            dataReader = command.ExecuteReader();
            dataTable.Load(dataReader);
            
            foreach (DataRow row in dataTable.Rows)
            {
                int id = int.Parse(row["id"].ToString());
                string name = row["name"].ToString();
                int numberOfMembers = int.Parse(row["numberOfMembers"].ToString());
                Console.WriteLine(new ChatListItemData(id, name, numberOfMembers, user.nick).ToString());

                chats.Add(new ChatListItemData(id, name, numberOfMembers, user.nick));
            }

            CloseConnetion();
            return chats;
        }

        public ChatHistoryData GetChatHistory(int chatId, string chatName, UserData user)
        {
            Connect();

            List<ChatMessageData> messagesList = new List<ChatMessageData>();
            
            command.CommandText = $"SELECT u.nick, m.content FROM messages m INNER JOIN users u ON m.id_user = u.id WHERE m.id_chat = \'{chatId}\' ORDER BY m.message_order";
            dataReader = command.ExecuteReader();
            dataTable.Load(dataReader);
            
            foreach (DataRow row in dataTable.Rows)
            {
                string nick = row["nick"].ToString();
                string content = row["content"].ToString();

                messagesList.Add(new ChatMessageData(content, nick, user.nick));
            }

            ChatHistoryData chatHistory = new ChatHistoryData(messagesList, chatName);

            CloseConnetion();
            return chatHistory;
        }

        public List<string> GetNicksInChat(int chatId, UserData user)
        {
            Connect();
            List<string> nicks = new List<string>();

            command.CommandText = $"SELECT u.nick FROM users u INNER JOIN chat_user cu ON u.id = cu.id_user WHERE cu.id_chat = \'{chatId}\' AND u.nick != \'{user.nick}\'";
            dataReader = command.ExecuteReader();
            dataTable.Load(dataReader);
            
            foreach (DataRow row in dataTable.Rows)
            {
                string nick = row["nick"].ToString();
                nicks.Add(nick);
            }
            
            CloseConnetion();
            return nicks;
        }

        public void AddMessageToChat(MessageSendData message, UserData user)
        {
            Connect();
            command.CommandText = $"SELECT MAX(m.message_order) + 1 as 'nextOrderNumber' FROM messages m WHERE m.id_chat = \'{message.ChatId}\' GROUP BY m.id_chat";
            dataReader = command.ExecuteReader();
            dataTable.Load(dataReader);
            string maxId = "1";
            if(dataTable.Rows.Count > 0)
            {
                maxId = dataTable.Rows[0]["nextOrderNumber"].ToString();
            }

            CloseConnetion();

            Connect();
            command.CommandText = $@"INSERT INTO messages (id_chat, id_user, message_order, content)
                VALUES(
                    '{message.ChatId}',
                    '{user.id}',
                    '{maxId}',
                    '{message.Content}'
                )";
            dataReader = command.ExecuteReader();

            CloseConnetion();
        }

        public void CreateNewChat(NewChatData newChatData)
        {
            Connect();
            command.CommandText = $"INSERT INTO chats (name) VALUES (\"{newChatData.ChatName}\")";
            dataReader = command.ExecuteReader();
            CloseConnetion();

            Connect();
            command.CommandText = $"SELECT id FROM chats WHERE name = \"{newChatData.ChatName}\"";
            dataReader = command.ExecuteReader();
            dataTable.Load(dataReader);
            CloseConnetion();

            int chatId = int.Parse(dataTable.Rows[0]["id"].ToString());
            Console.WriteLine($"new chat id: {chatId}");

            foreach(NickData nick in newChatData.Nicks)
            {
                Connect();
                command.CommandText = $"SELECT id FROM users WHERE nick = \"{nick.Nick}\"";
                dataReader = command.ExecuteReader();
                dataTable.Load(dataReader);
                CloseConnetion();

                int userId = int.Parse(dataTable.Rows[0][0].ToString());
                Console.WriteLine($"new user in group id: {userId}");


                Connect();
                command.CommandText = $"INSERT INTO chat_user (id_chat, id_user) VALUES (\"{chatId}\", \"{userId}\")";
                dataReader = command.ExecuteReader();
                CloseConnetion();
            }
        }

        public void CloseConnetion()
        {
            connection.Close();
        }
    }
}
