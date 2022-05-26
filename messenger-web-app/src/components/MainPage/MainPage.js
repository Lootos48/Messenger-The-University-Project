import css from "./MainPage.module.css";
import { Link } from "react-router-dom";
import React, { useState } from "react";

const MainPage = () => {
    if (sessionStorage.getItem("userID") == null) {
        window.location.assign("http://localhost:3000/");
    }

    let [userChats, setUserChats] = useState([]);
    let [currentChatName, setCurrentChatName] = useState("");
    let [currentChatID, setCurrentChatID] = useState("");
    let [messageHistory, setMessageHistory] = useState([]);
    let [turnedOnChat, setTurnedOnChat] = useState(false);

    let userName = sessionStorage.getItem("userName");
    React.useEffect(() => {
        const GetUserChats = () => {
            fetch(`http://localhost:4000/users/${sessionStorage.getItem("userID")}/chats`, {
                method: "get",
            })
                .then(response => {
                    if (response.ok) { return response.json(); }
                    else { throw new Error(response.statusText) }
                })
                .then(userChats => {
                    // debugger;
                    setUserChats(userChats);
                    return userChats;
                })
            // .catch(error => alert(error))
        }

        setInterval(() => {
            GetUserChats();
        }, 4000);
    }, []);

    let changeState = false;
    React.useEffect(() => {
        window.addEventListener("click", event => {
            let target = event.target.closest("." + css["chat"]);
            if (target) {
                const chatsList = document.querySelector("." + css["chats__list"]).children
                for (let i = 0; i < chatsList.length; i++) {
                    chatsList[i].style.backgroundColor = "";
                }
                target.style.backgroundColor = "gray";
                // debugger;
                const hiddenElements = document.querySelectorAll("." + css["_hidden"]);
                hiddenElements.forEach(element => {
                    element.classList.remove(css["_hidden"]);
                });

                changeState = true;
                setCurrentChatID(target.getAttribute("chatid"));
                setCurrentChatName(target.querySelector("." + css["chat__chat-name"]).innerText);

                // React.useEffect(() => {

                // }, []);

                const GetMessageHistory = (chatId) => {
                    fetch(`http://localhost:4000/chats/${chatId}/messages`, {
                        method: "get",
                    })
                        .then(response => {
                            if (response.ok) { return response.json(); }
                            else { throw new Error(response.statusText) }
                        })
                        .then(messageHistory => {
                            // debugger;
                            messageHistory.forEach(element => {
                                if (element.userAvatar == null || element.userAvatar == undefined) {
                                    element.userAvatar = "./img/profile-icon.svg";
                                }
                            })
                            // debugger;
                            setMessageHistory(messageHistory);
                            return messageHistory;
                        })
                    // .catch(error => alert(error))
                }

                setInterval(() => {
                    GetMessageHistory(target.getAttribute("chatid"));
                }, 2000);
            }
        });
    }, []);

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
                                <button className={`${css["chats__item"]} ${css["chat"]}`} key={chatInfo.id} chatid={chatInfo.id}>
                                    <h3 className={css["chat__chat-name"]}>{chatInfo.title}</h3>
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
                    <h2 className={`${css["messages__chat-name"]} ${css["_hidden"]}`}>{currentChatName}</h2>
                    <button className={`${css["messages__dots"]} ${css["_hidden"]}`}><img src="./img/three-dots.svg" alt="additional options" /></button>
                </div>
                <ul className={`${css["messages__list"]} ${css["_hidden"]}`} >
                    {
                        messageHistory.map(message => {
                            // debugger;
                            return (
                                <li className={`${css["messages__item"]} ${css["message"]}`} key={message.id}>
                                    <div className={css["message__icon"]}><img src={message.userAvatar} alt="icon" /></div>
                                    <div className={css["message__main"]}>
                                        <h3 className={css["message__sender-name"]}>{message.username}</h3>
                                        <h4 className={css["message__text"]}>{message.text}</h4>
                                        <div className={css["message__image"]}><img src={message.image} alt="" /></div>
                                        <h5 className={css["message__date"]}>{message.sendTime}</h5>
                                    </div>
                                </li>
                            )
                        })
                    }
                </ul>
                <div className={`${css["input"]} ${css["_hidden"]}`}>
                    <input hidden type="file" name="photo" id="upload-photo" accept="image/png, image/gif, image/jpeg" />
                    <label className={css["input__attach"]} htmlFor="upload-photo"><img src="./img/attach-icon.svg" alt="icon" /></label>
                    <textarea className={css["input__text"]} type="text" maxLength="3000" placeholder="Write your message..." />
                    <button className={css["input__send"]}><img src="./img/send-icon.svg" alt="icon" /></button>
                </div>
            </div>
        </div >
    );
}

export default MainPage;