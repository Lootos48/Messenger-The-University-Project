<?php

namespace App\Http\Controllers;

use App\Http\Requests\loginRequest;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Http;
use Illuminate\Support\Facades\Session;

class LoginController extends Controller
{
    public function index()
    {
        return view('login');
    }

    public function login(loginRequest $request)
    {
        $login = $request['login'];
        $password = $request['password'];

        $response = Http::post('http://localhost:4000/users/login', [
            'username' => $login,
            'password' => $password
        ]);

        if(isset($response->json()['error'])) {
            return(redirect('login')->with('message', 'there is no user with such data'));
        } else {
            Session::put('userId', $response->json());
        }

        return redirect()->route('chat_page');
    }
}
