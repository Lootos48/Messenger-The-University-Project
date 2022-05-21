import css from "./MainPage.module.css";
import { Link } from "react-router-dom";
import React, { useState } from "react";

let currentChatName, currentChatID, messageHistory, turnedOnChat = false;

const GetUserChats = () => {
    const userChatsQuery = {
        query: "userChats",
        userID: sessionStorage.getItem("userID"),
    }

    fetch("http://localhost:4000", {
        method: "post",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(userChatsQuery),
    })
        .then(response => response.JSON())
        .then(userChats => {
            if (!userChats.chatIcon) {
                userChats.chatIcon = "./img/profile-icon.svg";
            }
            return userChats;
        })
        .catch(error => alert(error))
}

const GetMessageHistory = (chatID) => {
    const messageHistoryQuery = {
        query: "messageHistory",
        chatID,
    }

    fetch("http://localhost:4000", {
        method: "post",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(messageHistoryQuery),
    })
        .then(response => response.JSON())
        .then(messageHistory => {
            return messageHistory;
        })
        .catch(error => alert(error))
}

const MainPage = () => {
    if (sessionStorage.getItem("userID") == null) {
        window.location.assign("http://localhost:3000/");
    }

    let userName;// = sessionStorage.getItem("userName");
    let userChats;// = JSON.parse(sessionStorage.getItem("userChats"));
    // messageHistory = JSON.parse(sessionStorage.getItem("messageHistory"));

    setInterval(() => {
        userChats = GetUserChats();
        window.location.reload();
    }, 30000);

    if (turnedOnChat) {
        setInterval(() => {
            messageHistory = GetMessageHistory();
            window.location.reload();
        }, 3000);
    }

    return (
        <div className={css["main-page"]}>
            <div className={`${css["main-page__chats"]} ${css["chats"]}`}>
                <div className={css["chats__header"]}>
                    <Link className={css["chats__exit-link"]} to="/"><img src="./img/logout-icon.svg" alt="logout" /></Link>
                    <h2 className={css["chats__main-user-name"]}>{userName}</h2>
                </div>
                <div className={css["chats__list"]}>
                    {
                        userChats.map(chatInfo => {
                            return (
                                <button className={`${css["chats__item"]} ${css["chat"]}`} chatid={chatInfo.chatID}>
                                    <div className={css["chat__icon"]}><img src={chatInfo.chatIcon} alt="icon" /></div>
                                    <h3 className={css["chat__chat-name"]}>{chatInfo.chatName}</h3>
                                    <h4 className={css["chat__last-message"]}>{chatInfo.lastMessage}</h4>
                                    <h5 className={css["chat__date-last-message"]}>{chatInfo.dateLastMessage}</h5>
                                </button>
                            )
                        })
                    }
                </div>
            </div>
            <div className={`${css["main-page__messages"]} ${css["messages"]}`}>
                <div className={css["messages__header"]}>
                    <div className={`${css["messages__chat-icon"]} ${css["_hidden"]}`}><img src="./img/profile-icon.svg" alt="icon" /></div>
                    <h2 className={`${css["messages__chat-name"]} ${css["_hidden"]}`}>Hello World{currentChatName}</h2>
                    <button className={`${css["messages__dots"]} ${css["_hidden"]}`}><img src="./img/three-dots.svg" alt="additional options" /></button>
                </div>
                <ul className={`${css["messages__list"]} ${css["_hidden"]}`} >
                    {
                        messageHistory.map(message => {
                            return (
                                <li className={`${css["messages__item"]} ${css["message"]}`}>
                                    <div className={css["message__icon"]}><img src={message.senderLogo} alt="icon" /></div>
                                    <div className={css["message__main"]}>
                                        <h3 className={css["message__sender-name"]}>{message.senderName}</h3>
                                        <h4 className={css["message__text"]}>{message.text}</h4>
                                        <div className={css["message__image"]}><img src={message.image} /></div>
                                        <h5 className={css["message__date"]}>{message.date}</h5>
                                    </div>
                                </li>
                            )
                        })
                    }
                </ul>
                <div className={`${css["input"]} ${css["_hidden"]}`}>
                    <input hidden type="file" name="photo" id="upload-photo" accept="image/png, image/gif, image/jpeg" />
                    <label className={css["input__attach"]} for="upload-photo"><img src="./img/attach-icon.svg" alt="icon" /></label>
                    <textarea className={css["input__text"]} type="text" maxLength="3000" placeholder="Write your message..." />
                    <button className={css["input__send"]}><img src="./img/send-icon.svg" alt="icon" /></button>
                </div>
            </div>
        </div >
    );
}

window.addEventListener("click", event => {
    let target = event.target.closest(".chat");
    if (target) {
        currentChatID = target.getAttribute("chatid");
        currentChatName = target.querySelector("messages__chat-name").innerHTML;

        const hiddenElements = document.querySelectorAll(".hidden");
        hiddenElements.classList.remove("hidden");
        turnedOnChat = true;
    }
});

export default MainPage;