<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Session;

class LogoutController extends Controller
{
    public function index()
    {
        Session::forget('chat_id');
        Session::forget('userId');

        return redirect()->route('login_page');
    }

}
