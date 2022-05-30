<?php

use Illuminate\Support\Facades\Route;

/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| contains the "web" middleware group. Now create something great!
|
*/

Route::get('/', function () {
    return redirect()->route('chat_page');
});


Route::get('/login', "\App\Http\Controllers\LoginController@index")->name('login_page');
Route::post('/login', "\App\Http\Controllers\LoginController@login")->name('login');


Route::get('/register', "\App\Http\Controllers\RegisterController@index")->name('register_page');
Route::post('/register', "\App\Http\Controllers\RegisterController@register")->name('register');

Route::get('/chat', "App\Http\Controllers\ChatController@index")->middleware('authorised')->name('chat_page');
Route::post('/chat', "App\Http\Controllers\ChatController@getChat")->middleware('authorised')->name('get_chat');

Route::post('/sendMessage', "App\Http\Controllers\ChatController@sendMessage")->middleware('authorised')->name('sendMessage');

Route::get('/logout', "\App\Http\Controllers\LogoutController@index")->middleware('authorised')->name('logout');
Route::get('/closeChat', "\App\Http\Controllers\ChatController@closeChat")->middleware('authorised')->name('close_chat');
Route::get('/leaveChat', "\App\Http\Controllers\ChatController@leaveChat")->middleware('authorised')->name('leave_chat');
Route::post('/changeChatName', "\App\Http\Controllers\ChatController@changeChatName")->middleware('authorised')->name('change_chat_name');
Route::post('/addUserToChat', "\App\Http\Controllers\ChatController@addUserToChat")->middleware('authorised')->name('add_user_to_chat');
Route::post('/createChat', "\App\Http\Controllers\ChatController@createChat")->middleware('authorised')->name('create_chat');
Route::post('/deleteMessage', "\App\Http\Controllers\ChatController@deleteMessage")->middleware('authorised')->name('delete_message');
Route::post('/editMessage', "\App\Http\Controllers\ChatController@editMessage")->middleware('authorised')->name('edit_message');

Route::get('/editUser', "\App\Http\Controllers\EditUserController@index")->middleware('authorised')->name('edit_user_page');
Route::post('/editUser', "\App\Http\Controllers\EditUserController@editUser")->middleware('authorised')->name('edit_user');





