<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Http;
use Illuminate\Support\Facades\Session;
use function GuzzleHttp\Promise\is_settled;

class ChatController extends Controller
{
    public function index()
    {
        if(Session::get('chat_id') != null)
        {
            return $this->getChatByChatId(Session::get('chat_id'));

        } else {

            $response3 = Http::get('http://localhost:4000/users/'. Session::get('userId')['userId']);

            $response2 = Http::get('http://localhost:4000/users/' . Session::get('userId')['userId'] . '/chats');

            return view('chat', [
                'chat_list' => $response2->json(),
                'user_name' => $response3->json()['username'],
                'user_avatar' => $response3->json()['avatar'],
            ]);
        }
    }

    public function sendMessage(Request $request)
    {
        if($request['text'] == null and !isset($request['img']))  {
            return redirect()->route('chat_page');
        }

        $userID = $request['userId'];
        $chatID = $request['chatId'];
        $text = $request['text'];

        if(isset($request['img'])) {
            $image = array();

            foreach(str_split(file_get_contents($request['img'])) as $byte){
                array_push($image, ord($byte));}
        } else {
            $image = null;
        }


        $response = Http::post('http://localhost:4000/messages/create', [
            'text' => $text,
            'imageBytes' => $image,
            'userId' => $userID,
            'chatId' => $chatID
        ]);

        if(isset($response->json()['error'])) {
            dd($response->json());
            //return(redirect('chat')->with('message', 'Введено некоректні дані'));
        } else {

        }
        return redirect()->route('chat_page');
    }

    public function getChat(Request $request)
    {
        $chatId = $request['chatId'];

        return $this->getChatByChatId($chatId);
    }

    public function getChatByChatId(int $chatId)
    {
        Session::put('chat_id', $chatId);

        $response = Http::get('http://localhost:4000/chats/' . $chatId );
        $response2 = Http::get('http://localhost:4000/users/' . Session::get('userId')['userId'] . '/chats');

        $response3 = Http::get('http://localhost:4000/users/'. Session::get('userId')['userId']);

        return view('chat', [
            'user_name' => $response3->json()['username'],
            'user_avatar' => $response3->json()['avatar'],
            'chat' => $response->json(),
            'chat_list' => $response2->json()
        ]);
    }

    public function closeChat()
    {
        Session::forget('chat_id');
        return redirect()->route('chat_page');
    }


    public function leaveChat()
    {
        $chatId = Session::get('chat_id');
        $userId = Session::get('userId');

        $response = Http::post('http://localhost:4000/chats/remove-user', [
            "chatId" => $chatId,
            "userId" => $userId['userId']
        ]);

        return redirect()->route('chat_page');
    }

    public function changeChatName(Request $request)
    {

        if(isset($request['title'])) {
            $chatId = Session::get('chat_id');
            $newTitle = $request['title'];

            //dd($chatId, $newTitle);

            $response = Http::post('http://localhost:4000/chats/edit', [
                "id" => $chatId,
                "title" => $newTitle
            ]);


        }

        return redirect()->route('chat_page');
    }

    public function addUserToChat(Request $request)
    {
        $chatId = Session::get('chat_id');

        if(isset($request['userName'])) {
            $userName = $request['userName'];

            $response = Http::get('http://localhost:4000/users/' . $userName);

            if(!isset($response->json()['error'])) {

                foreach ($response->json()['chats'] as $chat) {
                    if ($chat['id'] == $chatId) {
                        return redirect()->route('chat_page');
                    }
                }

                $userId = $response->json()['id'];

                $response2 = Http::post('http://localhost:4000/chats/add-user', [
                    "chatId" => $chatId,
                    "userId" => $userId,
                ]);

                $response3 = Http::post('http://localhost:4000/messages/create', [
                    'text' => 'User  "' . $response->json('username') . '" added to chat!',
                    'userId' => Session::get('userId')['userId'],
                    'chatId' => $chatId
                ]);
            }
        }
        return redirect()->route('chat_page');
    }

    public function createChat(Request $request)
    {


        $response = Http::post('http://localhost:4000/chats/create', [
            'title' => $request['title'],
            'creatorId' => Session::get('userId')['userId']
        ]);

        if(isset($response->json()['chatId'])) {

            $chatId = $response->json()['chatId'];

            Session::put('chat_id', $chatId);
        }

        return redirect()->route('chat_page');
    }

    public function editUser(Request $request)
    {
        dd($request);

    }

    public function deleteMessage(Request $request)
    {
        $messageId = $request['message_id'];

        $response = Http::post('http://localhost:4000/messages/delete?messageId=' . $messageId);


        return redirect()->route('chat_page');
    }

    public function editMessage(Request $request)
    {
        $messageId = $request['message_id'];
        $text = $request['text'] . "  (edited)";

        if(isset($request['img'])) {
            $image = array();

            foreach(str_split(file_get_contents($request['img'])) as $byte){
                array_push($image, ord($byte));}


            $response = Http::post('http://localhost:4000/messages/edit', [
                'id' => $messageId,
                'text' => $text,
                'imageBytes' => $image
            ]);

        } else {
            $response = Http::post('http://localhost:4000/messages/edit', [
                'id' => $messageId,
                'text' => $text,
            ]);
        }

        return redirect()->route('chat_page');
    }
}
