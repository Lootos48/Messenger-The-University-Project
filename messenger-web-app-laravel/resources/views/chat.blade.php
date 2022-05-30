<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="{{ asset('css/main.css') }}">
    <title>Chats</title>
    <link rel="shortcut icon" href="{{ asset('/icons/messenger-icon2.svg') }}" type="image/svg">

</head>
<body>
<header class="header" id="header">
    <div class="container">
        <div class="main">

            <form id="create-chat" class="create-chat" method="post" action="{{ route('create_chat') }}">
                @csrf
                <input name="title" class="create-chat__input" type="text" placeholder="Enter chat title">
                <input class="create-chat__button" type="submit" value="Create">
                <button class="create-chat__button" type="button" onclick="hideChatCreate()">Cancel</button>
            </form>

            <div class="history">
                <div class="user">
                    <a href="{{ route('logout') }}" class="logout" >
                        <img class="logout__image" src="{{ asset(('icons/exitIcon.png')) }}" alt="logout-icon">
                    </a>
                    <div class="user__icon">
                        @if($user_avatar === null)
                            <img src="{{ asset(('icons/profile-icon.svg')) }}" alt="profile-icon">
                        @else
                            <img class="message__user-avatar" src="data:image/png;base64, {{ $user_avatar }}">
                        @endif
                    </div>
                    <h1 class="user__title">
                        {{ $user_name }}
                    </h1>
                    <button type="button" class="gear" onclick="redirectToEditUser()">
                        <i class="fa-solid fa-gear"></i>
                    </button>
                </div>
                <div class="chats-list">
                    <button id="add-chat-button" class="add-chat-button" onclick="displayChatCreate()">
                        <img class="add-chat-button_image" src="{{ asset('icons/addIcon.png') }}">
                    </button>

                @foreach($chat_list as $el)
                    @if($el['id'] != Session::get('chat_id'))
                        <form class="friend__form" action="{{ route('get_chat') }}" method="POST">
                            @csrf
                            <input name="chatId" value="{{ $el['id'] }}" style="display: none">
                            <button type="submit" class="friend btn">
                                <div class="friend__icon icon">
                                    <img class="message__user-avatar" src="{{ asset('icons/chatIcon.png') }}">
                                </div>
                                <div class="friend__chat">
                                    <h3 class="friend__title">
                                        {{ $el['title'] }}
                                    </h3>
                                    <p class="friend__text">
                                        @if($el['messages']) {{ end($el['messages'])['text'] }}@endif
                                    </p>
                                </div>
                            </button>
                        </form>
                    @else
                        <form class="friend__form" action="{{ route('get_chat') }}" method="POST">
                            @csrf
                            <input name="chatId" value="{{ $el['id'] }}" style="display: none">
                            <button type="submit" class="friend btn-selected">
                                <div class="friend__icon icon">
                                    <img class="message__user-avatar" src="{{ asset('icons/chatIcon.png') }}">
                                </div>
                                <div class="friend__chat">
                                    <h3 class="friend__title">
                                        {{ $el['title'] }}
                                    </h3>
                                    <p class="friend__text">
                                        @if($el['messages']){{ end($el['messages'])['text'] }}@endif
                                    </p>
                                </div>
                            </button>
                        </form>
                    @endif
                @endforeach

                </div>
            </div>

            <!--========================================-->
        @if(!isset($chat['id']))

            <div class="chat">
                <div class="head">
                    <div class="head__people">
                        <h2>
                            Виберіть чат
                        </h2>
                    </div>
                </div>
                <div class="unknown">
                    <div class="wait">
                        <h2 class="wait__title">
                            Тут поки що пусто...
                        </h2>
                        <p class="wait__text">
                            Виберіть чат для перегляду інформації
                        </p>
                    </div>
                </div>
            </div>
            <!--========================================-->


        @else
            <div class="chat">
                <div class="head">
                    <div class="head__people">
                        <h2>
                            {{ $chat['title'] }}
                        </h2>
                    </div>
                    <div class="dots" onclick="displayDots()">
                        <i class="fa-solid fa-ellipsis-vertical"></i>
                    </div>
                    <div class="dots__block" id="dots__block">
                        <ul class="dots__menu" >
                            <li><a onclick="displayChangeChatName()">Change the chat title</a></li>
                            <li><a onclick="displayAddUser()">Add user to chat</a></li>
                            <li><a onclick="displayChatUsers()">Chat users</a></li>
                            <li><a href="{{ route('leave_chat') }}">Leave the chat</a></li>
                            <li><a href="{{ route('close_chat') }}">Close chat</a></li>
                        </ul>
                    </div>
                    <div class="dots__change-chat-name" id="dots__change-chat-name">
                        <form method="POST" action="{{ route('change_chat_name') }}">
                            @csrf
                            <input name="title" class="dots__change-chat-name__input" type="text" max="100" required><button class="dots__change-chat-name__button" type="submit">Save</button>
                        </form>
                    </div>
                    <div class="dots__add-user" id="dots__add-user">
                        <form method="POST" action="{{ route('add_user_to_chat') }}">
                            @csrf
                            <input name="userName" class="dots__add-user__input" type="text" max="100" required><button class="dots__add-user__button" type="submit">Add</button>
                        </form>
                    </div>

                    <div class="dots__user-list" id="dots__user-list">
                        <ul>
                            @foreach($chat['users'] as $user)
                            <li>{{ $user['username'] }}</li>
                            @endforeach
                        </ul>
                    </div>
                </div>




                <div id="window" class="window">


               @if(isset($chat['messages']))
            @foreach($chat['messages'] as $message)
                @if($message['userId'] != Session::get('userId')['userId'])
                    <div class="message left">
                        <div class="message__icon icon">
                            @if($message['userAvatar'] == null)
                                <img src="{{ asset(('icons/profile-icon.svg')) }}" alt="profile-icon">
                            @else
                                <img class="message__user-avatar" src="data:image/png;base64, {{ $message['userAvatar'] }}">
                            @endif
                        </div>
                        <div class="message__block">
                            <h3 class="message__title">
                                {{ $message['username'] }}
                            </h3>
                            @if(isset($message['image']))
                            <div class="message__img">
                                <img src="data:image/png;base64, {{ $message['image'] }}">
                            </div>
                            @endif
                            <p class="message__text">
                                {{ $message['text'] }}
                            </p>
                            <div class="message__time">
                                <time datetime="2015-12-02">{{ $message['sendDate'] }}</time>
                                <time datetime="20:00:00">{{ $message['sendTime'] }}</time>
                            </div>
                        </div>
                    </div>
                @else
                    <div class="message right">
                        <div class="message__settings">
                            <div class="edit">
                                <button class="trash__button" type="button" onclick="editMessage({{ $message['id'] }})"><img class="edit_icon" src="{{ asset('icons/editIcon.png') }}"></button>
                            </div>
                            <form class="trash" action="{{ route('delete_message') }}" method="POST">
                                @csrf
                                <input name="message_id"  value="{{ $message['id'] }}" hidden>
                                <button class="trash__button" type="submit"><img src="{{ asset('icons/deleteIcon.png') }}"></button>
                            </form>
                        </div>
                        <div class="message__block-right">
                            <h3 class="message__title">
                                {{ $message['username'] }} (Я)
                            </h3>
                            @if(isset($message['image']))
                                <div class="message__img">
                                    <img src="data:image/png;base64, {{ $message['image'] }}">
                                </div>
                            @endif
                            <p id="{{ 'message' . $message['id'] }}" class="message__text">{{ $message['text'] }}</p>
                            <div class="message__time">
                                <time datetime="2015-12-02">{{ $message['sendDate'] }}</time>
                                <time datetime="20:00:00">{{ $message['sendTime'] }}</time>
                            </div>
                        </div>
                        <div class="message__icon icon">
                            @if($message['userAvatar'] == null)
                                <img src="{{ asset(('icons/profile-icon.svg')) }}" alt="profile-icon">
                            @else
                                <img class="message__user-avatar" src="data:image/png;base64, {{ $message['userAvatar'] }}">
                            @endif
                        </div>
                    </div>
                @endif
            @endforeach
            @endif
                </div>


                <form id="sendMessage" action="{{ route('sendMessage') }}" class="form" method="POST" enctype="multipart/form-data">
                    @csrf
                    <input hidden id="upload_image" name="img" type="file" accept="image/*">
                    <label class="about" for="upload_image"><img src="{{ asset(('icons/attach-icon.svg')) }}" alt="attach-icon"></label>
                    <input name="userId" value="{{ Session::get('userId')['userId'] }}" hidden>
                    <input name="chatId" type="text" value="{{ $chat['id'] }}" hidden>
                    <input class="form__input-text" type="text" placeholder="Write your message..." name="text"></input>
                    <button type="submit" class="send">
                        <img src="{{ asset(('icons/send-icon.svg')) }}" alt="send-icon">
                    </button>
                </form>

                <form id="editMessage" action="{{ route('edit_message') }}" class="form" method="POST" enctype="multipart/form-data" style="display: none">
                    @csrf
                    <input hidden id="edit_upload_image" name="img" type="file" accept="image/*">
                    <label class="about" for="upload_image"><img src="{{ asset(('icons/attach-icon.svg')) }}" alt="attach-icon"></label>
                    <input id="messageId" name="message_id" hidden>
                    <input class="form__input-text" type="text" id="edit_text" placeholder="Edit your message..." name="text" required></input>
                    <button type="submit" class="send">
                        <img src="{{ asset(('icons/send-icon.svg')) }}" alt="send-icon">
                    </button>
                </form>


            </div>
        @endif
        </div>
    </div>

</header>

<script>
    var objDiv = document.getElementById("window");
    objDiv.scrollTop = objDiv.scrollHeight;

    function displayDots()
    {
        document.getElementById('dots__change-chat-name').style.display = 'none';
        document.getElementById('dots__add-user').style.display = 'none';
        document.getElementById('dots__user-list').style.display = 'none';


        let $el = document.getElementById('dots__block');

        if($el.style.display === 'block') $el.style.display = 'none';
        else $el.style.display = 'block';
    }

    function displayChangeChatName()
    {
        document.getElementById('dots__add-user').style.display = 'none';
        document.getElementById('dots__user-list').style.display = 'none';

        let $el = document.getElementById('dots__change-chat-name');
        if($el.style.display === 'block') $el.style.display = 'none';
        else $el.style.display = 'block';
    }

    function displayAddUser()
    {
        document.getElementById('dots__change-chat-name').style.display = 'none';
        document.getElementById('dots__user-list').style.display = 'none';

        let $el = document.getElementById('dots__add-user');
        if($el.style.display === 'block') $el.style.display = 'none';
        else $el.style.display = 'block';
    }

    function displayChatUsers()
    {
        document.getElementById('dots__change-chat-name').style.display = 'none';
        document.getElementById('dots__add-user').style.display = 'none';

        let $el = document.getElementById('dots__user-list');
        if($el.style.display === 'block') $el.style.display = 'none';
        else $el.style.display = 'block';
    }

    function displayChatCreate()
    {
        document.getElementById('add-chat-button').style.display = 'none';
        document.getElementById('create-chat').style.display = 'flex';
    }


    function hideChatCreate()
    {
        document.getElementById('add-chat-button').style.display = 'block';
        document.getElementById('create-chat').style.display = 'none';
    }

    function editMessage($id)
    {
        document.getElementById('editMessage').style.display = 'flex';
        document.getElementById('sendMessage').style.display = 'none';

        document.getElementById('edit_text').value = document.getElementById('message'+ $id).innerHTML;
        document.getElementById('messageId').value = $id;
    }

    function redirectToEditUser()
    {
        window.location.href = '{{ route('edit_user_page') }}';
    }



</script>
    <script src="https://kit.fontawesome.com/b882084224.js" crossorigin="anonymous"></script>
</body>
</html>
