<?php

namespace App\Http\Controllers;

use App\Http\Requests\RegistrationRequest;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Http;
use Illuminate\Support\Facades\Session;

class RegisterController extends Controller
{
    public function index()
    {
        return view('register');
    }

    public function register(RegistrationRequest $request)
    {
        $login = $request['login'];
        $password = $request['password'];

        if(isset($request['img'])) {
            $image = array();

            foreach(str_split(file_get_contents($request['img'])) as $byte){
                array_push($image, ord($byte));}
        } else {
            $image = null;
        }

        $response = Http::post('http://localhost:4000/users/register', [
            'username' => $login,
            'password' => $password,
            'imagebytes' => $image
        ]);

        if(isset($response->json()['error'])) {
            return(redirect('register')->with('message', 'Введено некоректні дані'));
        } else {
            Session::forget('userId');
            Session::put('userId', $response->json());

        }

        return redirect()->route('chat_page');
    }
}
