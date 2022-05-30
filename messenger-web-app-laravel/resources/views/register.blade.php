<head>

    <link rel="stylesheet" href="{{ asset(('css/register.css')) }}">
    <title>Register</title>
    <link rel="shortcut icon" href="{{ asset('/icons/messenger-icon2.svg') }}" type="image/svg">
</head>
<body>
    <div class="register">
        <div class="register__messenger-icon"><img src="{{ asset('icons/messenger-icon.svg') }}" alt="icon" /></div>
        <h1 class="register__title">Register your new account</h1>

        @if(count($errors) > 0)
            <div style="margin: 0 auto; display: flex; align-items: center; justify-content: center; margin-bottom: 20px">
                <ul style="color: red">
                    @foreach($errors->all() as $error)
                        <li>{{ $error}}</li>
                    @endforeach
                </ul>
            </div>
        @endif

        <form class="register__form" method="POST" action="{{ route('register') }}" enctype="multipart/form-data">
            @csrf
            <input hidden id="user_image" name="img" type="file" accept="image/*" >
            <label class="register__input-icon" for="user_image"><img class="register__image" src="{{ asset(('icons/profile-icon.svg')) }}" alt="Select user icon"></label>
            <input name="login" required class="register__input" id="register__user-name-input" type="text" maxLength="20" placeholder="Enter your user name" />
            <input name="password" required class="register__input" id="register__password-input" type="password" maxLength="30" placeholder="Enter your password" />
            <input name="password_confirmation" required class="register__input" id="register__password-input" type="password" maxLength="30" placeholder="Repeat your password" />

            <div class="register_form__buttons">
                <button class="register__submit-button" type="submit">Register</button>
                <a href="{{ route('login_page') }}"  class="register___submit-href" type="submit">I already have an account</a>
            </div>
        </form>
    </div>
</body>
