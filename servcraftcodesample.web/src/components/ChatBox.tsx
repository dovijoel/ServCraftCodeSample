import { MantineProvider, Affix, ActionIcon, Popover, TextInput, Stack, Flex, Paper, ScrollArea } from '@mantine/core';
import { IconMessageChatbot, IconPacman, IconSend } from '@tabler/icons-react';
import Markdown from 'react-markdown'
import { useState } from 'react';
import './ChatBox.css';

export function ChatBox({messages, sendMessage}) {
    const [message, setMessage] = useState("");
    const leftClass = "bubble left";
    const rightClass = "bubble right";
    const messagesList = messages.map((message, index) => <Paper class={message.isBot ? leftClass : rightClass}  radius="md" shadow="xs" p="sm" withBorder><Markdown key={index}>{message.message}</Markdown></Paper>);
    return (<>
    <ScrollArea h={275}>
    <Stack
      h={300}
      bg="var(--mantine-color-body)"
      align="center"
      justify="flex-start"
      gap="md"
    >
        {messagesList}
        </Stack>
        </ScrollArea>
        <Flex
        
        mih={50}
        gap="md"
        justify="center"
        align="center"
        direction="row"
        wrap="nowrap"
>  
        <TextInput style={{ width: '100%' }}  placeholder="Message" value={message} onChange={(event) => setMessage(event.currentTarget.value)} />
        <ActionIcon size={36} variant="filled" color="blue">
            <IconSend onClick={() => {
                sendMessage(message);
                setMessage("");
            }}  />
        </ActionIcon>
        </Flex>
                </>
    )
}