import './App.css';
import Login from "./components/Login/Login.js";
import Register from "./components/Register/Register.js";
import StartingPage from "./components/StartingPage/StartingPage.js";
import MainPage from "./components/MainPage/MainPage.js";
import { BrowserRouter, Routes, Route } from "react-router-dom";

//////Tester
// sessionStorage.setItem("userName", "Andrii");
// sessionStorage.setItem("userID", "1");
// sessionStorage.setItem("userChats", JSON.stringify([
//   {
//     chatID: "23",
//     chatName: "Grew",
//     chatIcon: "./img/profile-icon.svg",
//     lastMessage: "What are you watching at?",
//     dateLastMessage: "01.01.22 13:45",
//   },
//   {
//     chatID: "38",
//     chatName: "Max",
//     chatIcon: "./img/profile-icon.svg",
//     lastMessage: "Hey!",
//     dateLastMessage: "08.02.21 13:24",
//   },
//   {
//     chatID: "23",
//     chatName: "Grew",
//     chatIcon: "./img/profile-icon.svg",
//     lastMessage: "What are you watching at?",
//     dateLastMessage: "01.01.22 13:45",
//   },
//   {
//     chatID: "38",
//     chatName: "Max",
//     chatIcon: "./img/profile-icon.svg",
//     lastMessage: "Hey!",
//     dateLastMessage: "08.02.21 13:24",
//   },
//   {
//     chatID: "23",
//     chatName: "Grew",
//     chatIcon: "./img/profile-icon.svg",
//     lastMessage: "What are you watching at?",
//     dateLastMessage: "01.01.22 13:45",
//   },
//   {
//     chatID: "38",
//     chatName: "Max",
//     chatIcon: "./img/profile-icon.svg",
//     lastMessage: "Hey!",
//     dateLastMessage: "08.02.21 13:24",
//   },
//   {
//     chatID: "23",
//     chatName: "Grew",
//     chatIcon: "./img/profile-icon.svg",
//     lastMessage: "What are you watching at?",
//     dateLastMessage: "01.01.22 13:45",
//   },
//   {
//     chatID: "38",
//     chatName: "Max",
//     chatIcon: "./img/profile-icon.svg",
//     lastMessage: "Hey!",
//     dateLastMessage: "08.02.21 13:24",
//   },
//   {
//     chatID: "23",
//     chatName: "Grew",
//     chatIcon: "./img/profile-icon.svg",
//     lastMessage: "What are you watching at?",
//     dateLastMessage: "01.01.22 13:45",
//   },
//   {
//     chatID: "38",
//     chatName: "Max",
//     chatIcon: "./img/profile-icon.svg",
//     lastMessage: "Hey!",
//     dateLastMessage: "08.02.21 13:24",
//   },
//   {
//     chatID: "23",
//     chatName: "Grew",
//     chatIcon: "./img/profile-icon.svg",
//     lastMessage: "What are you watching at?",
//     dateLastMessage: "01.01.22 13:45",
//   },
//   {
//     chatID: "38",
//     chatName: "Max",
//     chatIcon: "./img/profile-icon.svg",
//     lastMessage: "Hey!",
//     dateLastMessage: "08.02.21 13:24",
//   },
// ]));
// sessionStorage.setItem("messageHistory", JSON.stringify([
//   {
//     text: "Hello",
//     image: "",
//     senderName: "Grew",
//     senderLogo: "./img/profile-icon.svg",
//     date: "01.01.22 12:45",
//   },
//   {
//     text: "What are you watching at?",
//     image: "./img/lookingAttentively.png",
//     senderName: "Grew",
//     senderLogo: "./img/profile-icon.svg",
//     date: "01.01.22 13:45",
//   },
//   {
//     text: "Hello",
//     image: "",
//     senderName: "Grew",
//     senderLogo: "./img/profile-icon.svg",
//     date: "01.01.22 12:45",
//   },
//   {
//     text: "What are you watching at?",
//     image: "./img/lookingAttentively.png",
//     senderName: "Grew",
//     senderLogo: "./img/profile-icon.svg",
//     date: "01.01.22 13:45",
//   },
//   {
//     text: "Hello",
//     image: "",
//     senderName: "Grew",
//     senderLogo: "./img/profile-icon.svg",
//     date: "01.01.22 12:45",
//   },
//   {
//     text: "What are you watching at?",
//     image: "./img/lookingAttentively.png",
//     senderName: "Grew",
//     senderLogo: "./img/profile-icon.svg",
//     date: "01.01.22 13:45",
//   },
//   {
//     text: "Hello",
//     image: "",
//     senderName: "Grew",
//     senderLogo: "./img/profile-icon.svg",
//     date: "01.01.22 12:45",
//   },
//   {
//     text: "What are you watching at?",
//     image: "./img/lookingAttentively.png",
//     senderName: "Grew",
//     senderLogo: "./img/profile-icon.svg",
//     date: "01.01.22 13:45",
//   },
//   {
//     text: "Hello",
//     image: "",
//     senderName: "Grew",
//     senderLogo: "./img/profile-icon.svg",
//     date: "01.01.22 12:45",
//   },
//   {
//     text: "What are you watching at?",
//     image: "./img/lookingAttentively.png",
//     senderName: "Grew",
//     senderLogo: "./img/profile-icon.svg",
//     date: "01.01.22 13:45",
//   },
//   {
//     text: "Hello",
//     image: "",
//     senderName: "Grew",
//     senderLogo: "./img/profile-icon.svg",
//     date: "01.01.22 12:45",
//   },
//   {
//     text: "What are you watching at?",
//     image: "./img/lookingAttentively.png",
//     senderName: "Grew",
//     senderLogo: "./img/profile-icon.svg",
//     date: "01.01.22 13:45",
//   },
// ]));

function App() {
  return (
    <BrowserRouter>
      <div className="App">
        <Routes>
          <Route path="/" element={<StartingPage />} />
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          <Route path="/mainPage" element={<MainPage />} />
        </Routes>
      </div>
    </BrowserRouter>
  );
}

export default App;
