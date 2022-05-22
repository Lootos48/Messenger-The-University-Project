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
//     chatID: "85",
//     chatName: "Andrew",
//     chatIcon: "./img/profile-icon.svg",
//     lastMessage: "Hmm...",
//     dateLastMessage: "01.05.22 10:45",
//   },
// ]));

// sessionStorage.setItem("messageHistory", JSON.stringify([
//   {
//     messageID: 1,
//     text: "Hello",
//     image: "",
//     senderName: "Grew",
//     senderLogo: "./img/profile-icon.svg",
//     date: "01.01.22 12:45",
//   },
//   {
//     messageID: 2,
//     text: "What are you watching at?",
//     image: "./img/lookingAttentively.png",
//     senderName: "Grew",
//     senderLogo: "./img/profile-icon.svg",
//     date: "01.01.22 13:45",
//   },
//   {
//     messageID: 3,
//     text: "Hello",
//     image: "",
//     senderName: "Grew",
//     senderLogo: "./img/profile-icon.svg",
//     date: "01.01.22 12:45",
//   },
//   {
//     messageID: 4,
//     text: "What are you watching at?",
//     image: "./img/lookingAttentively.png",
//     senderName: "Grew",
//     senderLogo: "./img/profile-icon.svg",
//     date: "01.01.22 13:45",
//   },
//   {
//     messageID: 5,
//     text: "Hello",
//     image: "",
//     senderName: "Grew",
//     senderLogo: "./img/profile-icon.svg",
//     date: "01.01.22 12:45",
//   },
//   {
//     messageID: 6,
//     text: "What are you watching at?",
//     image: "./img/lookingAttentively.png",
//     senderName: "Grew",
//     senderLogo: "./img/profile-icon.svg",
//     date: "01.01.22 13:45",
//   },
//   {
//     messageID: 7,
//     text: "Hello",
//     image: "",
//     senderName: "Grew",
//     senderLogo: "./img/profile-icon.svg",
//     date: "01.01.22 12:45",
//   },
//   {
//     messageID: 8,
//     text: "What are you watching at?",
//     image: "./img/lookingAttentively.png",
//     senderName: "Grew",
//     senderLogo: "./img/profile-icon.svg",
//     date: "01.01.22 13:45",
//   },
//   {
//     messageID: 9,
//     text: "Hello",
//     image: "",
//     senderName: "Grew",
//     senderLogo: "./img/profile-icon.svg",
//     date: "01.01.22 12:45",
//   },
//   {
//     messageID: 10,
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
