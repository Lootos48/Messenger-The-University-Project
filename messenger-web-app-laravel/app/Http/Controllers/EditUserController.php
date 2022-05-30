<?php

namespace App\Http\Controllers;

use App\Http\Requests\EditUserRequest;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Http;
use Illuminate\Support\Facades\Session;

class EditUserController extends Controller
{
    public function index()
    {
        return view('editUser');
    }

    public function editUser(EditUserRequest $request)
    {
        $data = [];

        $data['id'] = Session::get('userId')['userId'];

        $response = Http::get('http://localhost:4000/users/'. Session::get('userId')['userId']);


        if(isset($request['img'])) {
            $image = array();

            foreach(str_split(file_get_contents($request['img'])) as $byte){
                array_push($image, ord($byte));}

            $data['imagebytes'] = $image;
        } else {
            $data['imagebytes'] = null;
        }

        if($request['username'] != null)  {
            $data['username'] = $request['username'];
        } else {
            $data['username'] = $response->json('username');
        }

        if($request['password'] != null)  {
            $data['password'] = $request['password'];
        } else {
            $data['password'] = $response->json('password');
        }

        $data['confirmPassword'] = $data['password'];


        $response2 = Http::post('http://localhost:4000/users/edit', $data);



        return redirect()->route('chat_page');
    }
}
