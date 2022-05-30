<head>
    <link rel="stylesheet" href="{{ asset(('css/login.css')) }}">
    <title>Login</title>
    <link rel="shortcut icon" href="{{ asset('/icons/messenger-icon2.svg') }}" type="image/svg">
</head>
<body>
    <div class="login">
        <div class="login__messenger-icon"><img src="{{ asset('icons/messenger-icon.svg') }}" alt="icon" /></div>
        <h1 class="login__title">Login to your account</h1>

        @if(session('message'))
            <div style="margin: 0 auto; display: flex; align-items: center; justify-content: center; margin-bottom: 20px">
                <ul style="color: red">
                    <li>{{ session('message') }}</li>
                </ul>
            </div>
        @endif


        <form class="login__form" action="{{ route('login') }}" method="POST">
            @csrf
            <input name="login" required class="login__input" id="login__user-name-input"type="text" maxLength="20" placeholder="Enter your user name" />
            <input name="password" required class="login__input" id="login__password-input" type="password" maxLength="30" placeholder="Enter your password" />
            <div class="login_form__buttons">
                <button class="login__submit-button" type="submit">Login</button>
                <a href="{{ route('register_page') }}"class="login__submit-button" type="submit">Register</a>
            </div>

        </form>
    </div>
</body>
