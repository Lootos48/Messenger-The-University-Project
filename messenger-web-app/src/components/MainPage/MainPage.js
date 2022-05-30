import css from "./MainPage.module.css";
import { Link } from "react-router-dom";
import React, { useState } from "react";

let bytes = null;

const MainPage = () => {
    if (sessionStorage.getItem("userID") == null) {
        window.location.assign("http://localhost:3000/");
    }

    let [userChats, setUserChats] = useState([]);
    let [currentChatName, setCurrentChatName] = useState("");
    let [currentChatID, setCurrentChatID] = useState("");
    let [currentChatUsers, setCurrentChatUsers] = useState([]);
    let [messageHistory, setMessageHistory] = useState([]);
    let [turnedOnChat, setTurnedOnChat] = useState(false);
    let [userName, setUserName] = useState(sessionStorage.getItem("userName"));
    // Getting user chats evety 2 seconds
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
                    setUserChats(userChats);
                    return userChats;
                })
            // .catch(error => alert(error))
        }

        setInterval(() => {
            GetUserChats();
        }, 2000);
    }, []);

    React.useEffect(() => {
        const hiddenElements = document.querySelectorAll("." + css["_hidden"]);
        let messageRecevingInterval;

        window.addEventListener("click", event => {
            let target = event.target.closest("." + css["chat"]);

            if (target) {
                // Highlighting chosen chat
                const chatsList = document.querySelector("." + css["chats__list"]).children
                for (let i = 0; i < chatsList.length; i++) {
                    chatsList[i].style.backgroundColor = "";
                }
                target.style.backgroundColor = "gray";
                // Before messages coming hiding unnecessary elements
                hiddenElements.forEach(element => {
                    element.classList.add(css["_hidden"]);
                });

                clearInterval(messageRecevingInterval);
                sessionStorage.setItem("chatID", target.getAttribute("chatid"));
                setCurrentChatID(target.getAttribute("chatid"));
                setCurrentChatName(target.querySelector("." + css["chat__chat-name"]).innerText);

                const GetMessageHistory = (chatId) => {
                    fetch(`http://localhost:4000/chats/${chatId}/messages`, {
                        method: "get",
                    })
                        .then(response => {
                            if (response.ok) { return response.json(); }
                            else { throw new Error(response.statusText) }
                        })
                        .then(messageHistory => {
                            // Default profile picture if user don't have one
                            messageHistory.forEach(element => {
                                if (element.userAvatar == null || element.userAvatar == undefined) {
                                    element.userAvatar = "./img/profile-icon.svg";
                                }
                                else {
                                    element.userAvatar = URL.createObjectURL(element.userAvatar);
                                }
                            });
                            // After messages have came, showing hidden elements
                            hiddenElements.forEach(element => {
                                element.classList.remove(css["_hidden"]);
                            });

                            const allMessages = document.querySelectorAll("." + css["message"]);
                            if (allMessages.length) {
                                // Highlighting user own messages
                                allMessages.forEach(element => {
                                    if (element.getAttribute("userid") == sessionStorage.getItem("userID")) {
                                        element.classList.add(css["_my-whole-message"]);
                                        element.querySelector("." + css["message__main"]).classList.add(css["_my-message"]);
                                    }
                                });
                                // Messages scroll
                                document.querySelector("." + css["messages"]).scrollTo({
                                    top: allMessages[allMessages.length - 1].getBoundingClientRect().top + document.querySelector("." + css["messages"]).scrollTop,
                                    behavior: "smooth",
                                });
                            }
                            setMessageHistory(messageHistory);
                            return messageHistory;
                        })
                    // .catch(error => alert(error))
                }
                // After clicking on chat, getting chat messages every 2 seconds
                messageRecevingInterval = setInterval(() => {
                    GetMessageHistory(target.getAttribute("chatid"));
                }, 2000);
            }
        });
        // Sending messages
        document.querySelector("." + css["input__send"]).addEventListener("click", event => {
            // event.preventDefault();
            let target = event.target.closest("." + css["input__send"]);
            if (target.parentNode.querySelector("." + css["input__text"]).value) {
                // function onImageChange(image) {
                //     const imageFile = URL.createObjectURL(image);
                //     createImage(imageFile, convertImage);
                // }

                // function createImage(imageFile, callback) {
                //     const image = document.createElement('img');
                //     image.onload = () => callback(image);
                //     image.setAttribute('src', imageFile);
                // }

                // function convertImage(image) {
                //     const canvas = drawImageToCanvas(image);
                //     const ctx = canvas.getContext('2d');

                //     let result = [];
                //     for (let y = 0; y < canvas.height; y++) {
                //         result.push([]);
                //         for (let x = 0; x < canvas.width; x++) {
                //             let data = ctx.getImageData(x, y, 1, 1).data;
                //             result[y].push(data[0]);
                //             result[y].push(data[1]);
                //             result[y].push(data[2]);
                //         }
                //     }
                //     sessionStorage.setItem("bytes", JSON.stringify(result))
                // }

                // function drawImageToCanvas(image) {
                //     const canvas = document.createElement('canvas');
                //     canvas.width = image.width;
                //     canvas.height = image.height;
                //     canvas.getContext('2d').drawImage(image, 0, 0, image.width, image.height);
                //     return canvas;
                // }

                // function convertArray(array) {
                //     return JSON.stringify(array).replace(/\[/g, '{').replace(/\]/g, '}');
                // }
                // if (target.parentNode.querySelector("#upload-photo").files[0] != null) {
                // onImageChange(target.parentNode.querySelector("#upload-photo").files[0]);
                // }
                // else {
                //     bytes = null;
                // }
                // debugger;
                fetch("http://localhost:4000/messages/create", {
                    method: "post",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify({
                        text: target.parentNode.querySelector("." + css["input__text"]).value,
                        imagebytes: null,
                        userId: +sessionStorage.getItem("userID"),
                        chatId: +sessionStorage.getItem("chatID"),
                    }),
                })

                target.parentNode.querySelector("." + css["input__text"]).value = "";
                target.parentNode.querySelector("#upload-photo").files = null;
            }
        });

        let iterator = 0;
        document.addEventListener("click", event => {
            // event.preventDefault();
            // Showing and hiding chat creating menu
            let target = event.target.closest("." + css["chats__add"]);
            if (target) {
                iterator++;
                if (iterator % 2 == 1) {
                    target.nextSibling.style.display = "grid";
                }
                else {
                    target.nextSibling.style.display = "none";
                }
            }
            if (!event.target.closest("." + css["add-chat"]) && !target) {
                document.querySelector("." + css["add-chat"]).style.display = "none";
            }
            // Creating chat
            target = event.target.closest("." + css["add-chat"] + " > button");
            if (target) {
                if (document.querySelector("." + css["add-chat"] + " > input").value != "") {
                    fetch("http://localhost:4000/chats/create", {
                        method: "post",
                        headers: {
                            "Content-Type": "application/json"
                        },
                        body: JSON.stringify({
                            title: document.querySelector("." + css["add-chat"] + " > input").value,
                            creatorId: sessionStorage.getItem("userID"),
                        }),
                    });
                    document.querySelector("." + css["add-chat"] + " > input").value = "";
                    document.querySelector("." + css["add-chat"]).style.display = "none";
                }
            }
            // Logout
            target = event.target.closest("." + css["chats__exit-link"]);
            if (target) {
                sessionStorage.clear();
                window.location.assign("http://localhost:3000/");
            }
            // Showing and hiding chat editing
            target = event.target.closest("." + css["messages__dots"]);
            if (target) {
                iterator++;
                if (iterator % 2 == 1) {
                    target.nextSibling.style.display = "grid";
                }
                else {
                    target.nextSibling.style.display = "none";
                }
            }

            if (!event.target.closest("." + css["chat-menu"]) && !target) {
                document.querySelector("." + css["chat-menu"]).style.display = "none";
            }
            // Showing and hiding user settings
            if (event.target.closest("." + css["chats__user-setting-button"])) {
                iterator++;
                if (iterator % 2 == 1) {
                    document.querySelector("." + css["user-form"]).style.display = "grid";
                }
                else {
                    document.querySelector("." + css["user-form"]).style.display = "none";
                }
            }

            if (!event.target.closest("." + css["user-form"]) && !event.target.closest("." + css["chats__user-setting-button"])) {
                document.querySelector("." + css["user-form"]).style.display = "none";
            }
        });
        // Getting chat users
        setInterval(() => {
            if (document.querySelector("." + css["chat-menu"]).style.display == "grid") {
                fetch(`http://localhost:4000/chats/${+sessionStorage.getItem("chatID")}/users`, {
                    method: "get",
                })
                    .then(response => {
                        if (response.ok) { return response.json(); }
                        else { throw new Error(response.statusText) }
                    })
                    .then(chatUsers => {
                        chatUsers.forEach(user => {
                            if (user.avatar == null || user.avatar == undefined) {
                                user.avatar = "./img/profile-icon.svg"
                            }
                        })
                        return setCurrentChatUsers(chatUsers);
                    })
            }
        }, 2000)

        document.addEventListener("click", event => {
            // Exit chat
            if (event.target.closest("." + css["chat-menu__delete-chat"])) {
                fetch(`http://localhost:4000/chats/remove-user`, {
                    method: "post",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify({
                        chatId: +sessionStorage.getItem("chatID"),
                        userId: +sessionStorage.getItem("userID"),
                    }),
                })

                hiddenElements.forEach(element => {
                    element.classList.add(css["_hidden"]);
                });

                document.querySelector("." + css["chat-menu"]).style.display = "none";
            }

        })

        document.addEventListener("click", event => {
            // Add new chat's user
            if (event.target.className == css["chat-menu__invite-button"]) {
                // debugger;
                if (document.querySelector("." + css["chat-menu__input"]).value != "") {

                    fetch(`http://localhost:4000/users/${document.querySelector("." + css["chat-menu__input"]).value}`, {
                        method: "get",
                    })
                        .then(response => {
                            if (response.ok) { return response.json(); }
                            else { throw new Error(response.statusText) }
                        })
                        .then(user => {
                            fetch(`http://localhost:4000/chats/add-user`, {
                                method: "post",
                                headers: {
                                    "Content-Type": "application/json"
                                },
                                body: JSON.stringify({
                                    chatId: +sessionStorage.getItem("chatID"),
                                    userId: +user.id,
                                }),
                            })
                        })
                }
            }
        })

        document.querySelector("." + css["chats__user-setting-form"] + " > button").addEventListener("click", event => {
            // Edit user's settings
            if (document.querySelector("." + css["chats__setting-user-name"]).value != "" ||
                document.querySelector("." + css["chats__setting-user-password"]).value != "" ||
                document.querySelector("." + css["chats__setting-user-submit-password"]).value != "") {

                fetch(`http://localhost:4000/users/edit`, {
                    method: "post",
                    headers: {
                        "Content-Type": "application/json"
                    },
                    body: JSON.stringify({
                        id: sessionStorage.getItem("userID"),
                        username: document.querySelector("." + css["chats__setting-user-name"]).value,
                        password: document.querySelector("." + css["chats__setting-user-password"]).value,
                        confirmPassword: document.querySelector("." + css["chats__setting-user-submit-password"]).value,
                    })
                })
                    .then(response => {
                        setUserName(document.querySelector("." + css["chats__setting-user-name"]).value);
                    })
                    .catch(error => alert(error))
            }
        });

        document.addEventListener("click", event => {
            // Deleting message
            let target = event.target.closest("." + css["message__delete-button"]);
            if (target) {
                fetch(`http://localhost:4000/messages/delete?messageId=${target.parentNode.parentNode.getAttribute("messageid")}`, {
                    method: "post",
                })
            }
        })
    }, []);

    return (
        <div className={css["main-page"]}>
            <div className={`${css["main-page__chats"]} ${css["chats"]}`}>
                <div className={css["chats__header"]}>
                    <button className={css["chats__exit-link"]}><img src="./img/logout-icon.png" alt="logout" /></button>
                    <div className={css["chats__user-avatar"]}><img src="./img/profile-icon.svg" alt="icon" /></div>
                    <h2 className={css["chats__main-user-name"]}>{userName}</h2>
                    <button className={css["chats__user-setting-button"]}><img src="./img/settings-icon.svg" alt="icon" /></button>
                    <div className={`${css["chats__user-setting-form"]} ${css["user-form"]}`}>
                        <input className={css["chats__setting-user-name"]} required type="text" maxLength="20" placeholder="Enter your name..." />
                        <input className={css["chats__setting-user-password"]} required type="password" maxLength="32" placeholder="Enter your password..." />
                        <input className={css["chats__setting-user-submit-password"]} required type="password" maxLength="32" placeholder="Submit your password..." />
                        <button type="submit">Change</button>
                    </div>
                </div>
                <div className={css["chats__list"]}>
                    {
                        userChats.map(chatInfo => {
                            let text = " ";
                            if (chatInfo.messages.length) {
                                if (chatInfo.messages[chatInfo.messages.length - 1].hasOwnProperty("text")) {
                                    text = chatInfo.messages[chatInfo.messages.length - 1].text;
                                }
                            }

                            return (
                                <button className={`${css["chats__item"]} ${css["chat"]}`} key={chatInfo.id} chatid={chatInfo.id}>
                                    <div className={css["chat__icon"]}><img src="./img/chat-icon.png" /></div>
                                    <h3 className={css["chat__chat-name"]}>{chatInfo.title}</h3>
                                    <h4 className={css["chat__last-message"]}>{text}</h4>
                                </button>
                            )
                        })
                    }
                </div>
                <button className={css["chats__add"]}><img src="./img/plus-icon.png" alt="add chat" /></button>
                <div className={`${css["chats__add-menu"]} ${css["add-chat"]}`}>
                    <input required name="chat-title" type="text" maxLength="100" placeholder="Enter chat title..."></input>
                    <button type="submit">Create chat</button>
                </div>
            </div>
            <div className={`${css["main-page__messages"]} ${css["messages"]}`}>
                <div className={css["messages__header"]}>
                    <h2 className={`${css["messages__chat-name"]} ${css["_hidden"]}`}>{currentChatName}</h2>
                    <button className={`${css["messages__dots"]} ${css["_hidden"]}`}><img src="./img/three-dots.png" alt="additional options" /></button>
                    <div className={`${css["messages__chat-menu"]} ${css["chat-menu"]}`}>
                        <input className={css["chat-menu__input"]} required name="new-chat-user" type="text" maxLength="20" placeholder="Enter new chat's user..."></input>
                        <button className={css["chat-menu__invite-button"]}>Invite user</button>
                        <ul className={css["chat-menu__users"]}>
                            {
                                currentChatUsers.map(chatUser => {
                                    return (
                                        <li className={css["chat-menu__user-item"]} key={chatUser.id}>
                                            <img src={chatUser.avatar} alt="user-avatar"></img>
                                            <div>
                                                <span>{chatUser.username}</span>
                                            </div>
                                        </li>
                                    );
                                })
                            }
                        </ul>
                        <button className={css["chat-menu__delete-chat"]}>
                            <img src="./img/delete-icon.png" alt="delete-icon"></img>
                            <span>Exit chat</span>
                        </button>
                    </div>
                </div>
                <ul className={`${css["messages__list"]} ${css["_hidden"]}`} >
                    {
                        messageHistory.map(message => {
                            return (
                                <li className={`${css["messages__item"]} ${css["message"]}`} key={message.id} messageid={message.id} userid={message.userId}>
                                    <div className={css["message__icon"]}><img src={message.userAvatar} alt="icon" /></div>
                                    <div className={css["message__main"]}>
                                        <h3 className={css["message__sender-name"]}>{message.username}</h3>
                                        <h4 className={css["message__text"]}>{message.text}</h4>
                                        <div className={css["message__image"]}><img src={message.image} alt="" /></div>
                                        <h5 className={css["message__date"]}>{message.sendDate}</h5>
                                        <h5 className={css["message__time"]}>{message.sendTime}</h5>
                                    </div>
                                    <div className={css["message__options"]}>
                                        <button className={css["message__delete-button"]}><img src="./img/delete-icon.png" /></button>
                                    </div>
                                </li>
                            )
                        })
                    }
                </ul>
                <div className={`${css["input"]} ${css["_hidden"]}`}>
                    <input hidden type="file" name="photo" id="upload-photo" accept="image/png, image/gif, image/jpeg" />
                    <label className={css["input__attach"]} htmlFor="upload-photo"><img src="./img/attach-icon.png" alt="icon" /></label>
                    <textarea required className={css["input__text"]} type="text" maxLength="3000" placeholder="Write your message..." />
                    <button className={css["input__send"]}><img src="./img/send-icon.png" alt="icon" /></button>
                </div>
            </div>
        </div >
    );
}
//encType="multipart/form-data"
export default MainPage;