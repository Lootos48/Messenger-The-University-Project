<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Http;
use Illuminate\Support\Facades\Session;

class TestController extends Controller
{
    public function index()
    {

        //$response = Http::get('http://localhost:4000/users/1');

//        $response = Http::post('http://localhost:4000/users/login', [
//            'username' => 'Denys',
//            'password' => '12345678'
//        ]);

//        $response = Http::post('http://localhost:4000/chats/create', [
//            'title' => 'Test chat2',
//            'creatorId' => '1'
//        ]);

//        $response = Http::post('http://localhost:4000/messages/create', [
//            'text' => 'Test7 message',
//            'userId'=> '2',
//            'chatId' => '2',
//        ]);

        //$response = Http::get('http://localhost:4000/users/1/chats');

        //$response = Http::get('http://localhost:4000/users/Test');

//        $response = Http::get('http://localhost:4000/users/'. Session::get('userId')['userId']);
//        dd($response->json());

        $response = Http::get('http://localhost:4000/users/' . Session::get('userId')['userId'] . '/chats');
        dd($response->json());


        //return(view('welcome', ['data' => $image64]));
    }

    public function index2(Request $request)
    {
        dd($request);
    }
}
