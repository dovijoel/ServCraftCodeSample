import { MantineProvider, Affix, ActionIcon, Popover, TextInput, Stack, Flex, Text } from '@mantine/core';
import { IconMessageChatbot, IconPacman, IconSend } from '@tabler/icons-react';
import { useState } from 'react';

export function ChatBox({messages, sendMessage}) {
    const [message, setMessage] = useState("");
    const messagesList = messages.map((message) => <Text key={message.time}>{message.message}</Text>);
    return (<>
    <Stack
      h={300}
      bg="var(--mantine-color-body)"
      align="center"
      justify="flex-start"
      gap="md"
    >
        {messagesList}
        </Stack>
        <Flex
        mih={50}
        gap="md"
        justify="center"
        align="center"
        direction="row"
        wrap="nowrap"
>  
        <TextInput placeholder="Message" value={message} onChange={(event) => setMessage(event.currentTarget.value)} />
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