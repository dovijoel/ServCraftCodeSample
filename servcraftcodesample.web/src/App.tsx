import "@mantine/core/styles.css";
import {
  MantineProvider,
  Affix,
  ActionIcon,
  Popover,
  TextInput,
} from "@mantine/core";
import { IconMessageChatbot, IconPacman } from "@tabler/icons-react";
import { useEffect, useState } from "react";
import reactLogo from "./assets/react.svg";
import viteLogo from "/vite.svg";
import "./App.css";
import { ChatBox } from "./components/ChatBox.tsx";
import * as signalR from "@microsoft/signalr";
import { v4 as uuidv4, NIL as NIL_UUID } from "uuid";
import { ChatMessage } from "./model/ChatMessage.tsx";

function App() {
  const [conversationId, setConversationId] = useState(NIL_UUID);
  const [messages, setMessages] = useState<any>([]);

  const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/chat")
    .configureLogging(signalR.LogLevel.Information)
    .withAutomaticReconnect()
    .build();

  // Starts the SignalR connection
  hubConnection.start().then(() => {
    // Once started, invokes the sendConnectionId in our ChatHub inside our ASP.NET Core application.
    if (conversationId === NIL_UUID) {
      hubConnection.invoke("StartConversation");
    }
  });

  hubConnection.on("setConversationId", (response) => {
    setConversationId(response);
    console.log("Conversation ID: ", response);
  });

  hubConnection.on("receiveMessage", (message) => {
    setMessages([...messages, new ChatMessage(message, new Date(), true)]);
  });

  const sendMessage = (message) => {
    setMessages([...messages, new ChatMessage(message, new Date(), false)]);
    hubConnection
      .invoke("ReceiveMessage", conversationId, message)
      .then((response) => {
        setMessages([...messages, new ChatMessage(response, new Date(), true)]);
      })
      .catch((error) => {
        console.error("Error sending message: ", error);
      });
  };
  return (
    <MantineProvider>
      <Affix position={{ bottom: 20, right: 20 }}>
        <Popover width={550} trapFocus position="top" withArrow shadow="md">
          <Popover.Target>
            <ActionIcon size="xl" variant="filled" color="blue">
              <IconMessageChatbot size={24} />
            </ActionIcon>
          </Popover.Target>
          <Popover.Dropdown>
            <ChatBox messages={messages} sendMessage={sendMessage} />
          </Popover.Dropdown>
        </Popover>
      </Affix>
      <div>
        <a href="https://www.github.com/dovijoel" target="_blank">
          <img
            src="https://avatars.githubusercontent.com/u/6263462?v=4"
            className="logo"
            alt="ServCraft logo"
          />
        </a>
        <a href="https://www.servcraft.co.za/" target="_blank">
          <img
            src="https://www.servcraft.co.za/servcraft/servcraft-logo-dark.svg"
            className="logo"
            alt="ServCraft logo"
          />
        </a>
      </div>
      <h1>Dovi + ServCraft</h1>
      <div className="card">
        <p>(Yes, I know it's a bit cheesy)</p>
      </div>
      <p className="read-the-docs">
        Click on the chat button on the lower left corner of the screen to get
        started.
      </p>
    </MantineProvider>
  );
}

export default App;
