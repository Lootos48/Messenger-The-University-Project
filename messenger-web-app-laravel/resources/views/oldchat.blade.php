<head xmlns="http://www.w3.org/1999/html">
    <link rel="stylesheet" href="{{ asset(('css/mainpage.css')) }}">
</head>
<body>

    <div class="main-page">

        <div class="main-page__chats chats">
            <div class="chats__header">
                <a class="chats__exit-link" href="/"><img src="./img/logout-icon.svg" alt="logout" /></a>
                <h2 class="chats__main-user-name">{userName}</h2>
            </div>

            <div class="chats__list">
                {
                    userChats.map(chatInfo => {
                        return (
                        <button class="chats__item chat" chatid={chatInfo.chatID}>
                            <div class="chat__icon"><img src={chatInfo.chatIcon} alt="icon" /></div>
                            <h3 class="chat__chat-name">{chatInfo.chatName}</h3>
                            <h4 class="chat__last-message">{chatInfo.lastMessage}</h4>
                            <h5 class="chat__date-last-message">{chatInfo.dateLastMessage}</h5>
                        </button>
                        )
                    })
                }
            </div>
        </div>


        <div class="main-page__messages messages">

            <div class="messages__header">
                <div class="messages__chat-icon "><img src="{{ asset(('icons/profile-icon.svg')) }}" alt="icon" /></div>
                <h2 class="messages__chat-name ">{{ $chat['title'] }}</h2>
                <button class="messages__dots"><img src="{{ asset(('icons/three-dots.svg')) }}" alt="additional options" /></button>
            </div>


            <ul id="messages" class="messages__list">
                @foreach($chat['messages'] as $message)
                            <li class="messages__item message">
                                <div class="message__icon"><img src="{{ asset(('icons/profile-icon.svg')) }}" alt="icon" /></div>
                                <div class="message__main">
                                    <h3 class="message__sender-name">{{ $message['username'] }}</h3>
                                    <h4 class="message__text">{{ $message['text'] }}</h4>
                                    <!--<div class="message__image"><img src={message.image} /></div> -->
                                    <h5 class="message__date">{{ $message['sendTime'] }}</h5>
                                </div>
                            </li>
                @endforeach

            </ul>

            <div id="input" class="input">
                <input hidden type="file" name="photo" id="upload-photo" accept="image/png, image/gif, image/jpeg" />

                <label class="input__attach" for="upload-photo"><img src="{{ asset(('icons/attach-icon.svg')) }}" alt="icon" /></label>

                <textarea class=input__text" type="text" maxLength="3000" placeholder="Write your message..."></textarea>
                <button class="input__send"><img src="{{ asset(('icons/send-icon.svg')) }}" alt="icon" /></button>
            </div>

            <script>
                let el = document.getElementById('messages');
                el.scrollIntoView(false);
            </script>
        </div>

    </div>
</body>
