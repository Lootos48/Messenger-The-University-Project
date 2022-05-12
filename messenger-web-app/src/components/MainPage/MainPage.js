import css from "./MainPage.module.css";
import { Link } from "react-router-dom";

const GetUserChats = () => {
    const userInfoQuery = {
        query: "userInfo",
        userID: sessionStorage.getItem("userID"),
    }

    fetch("http://localhost:4000", {
        method: "post",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(userInfoQuery),
    })
        .then(response => response.JSON())
        .then(userData => {
            return {
                userChats: userData.userChats,
            }
        })
        .catch(error => alert(error))
}

const MainPage = () => {
    if (Boolean(sessionStorage.getItem("logined")) == false || sessionStorage.getItem("logined") == null) {
        window.location.assign("http://localhost:3000/");
    }

    let userName = sessionStorage.getItem("userName");
    let userChats;

    setInterval(() => {
        userChats = GetUserChats();
        if (userChats.chatIcon == null) {
            userChats.chatIcon = "./img/profile-icon.svg";
        }
    }, 30000);

    return (
        <div className={css["main-page"]}>
            <div className={css["main-page__chats chats"]}>
                <div className={css["chats__header"]}>
                    <Link className={css["chats__exit-link"]} to="/"><img src="./img/messenger-icon.svg" alt="logo" /></Link>
                    <h2 className={css["chats__main-user-name"]}>{userName}</h2>
                </div>
                <ul className={css["chats__list"]}>
                    {
                        userChats.map(chatInfo => {
                            return (
                                <li className={css["chats__item chat"]}>
                                    <div className={css["chat__icon"]}><img src={chatInfo.chatIcon} alt="icon" /></div>
                                    <h3 className={css["chat__chat-name"]}>{chatInfo.chatName}</h3>
                                    <h4 className={css["chat__last-message"]}>{chatInfo.lastMessage}</h4>
                                    <h5 className={css["chat__date-last-message"]}>{chatInfo.dateLastMessage}</h5>
                                </li>
                            )
                        })
                    }
                </ul>
            </div>
            <div className={css["main-page__messages messages"]}>
                <div className={css["messages__header"]}>
                    <div className={css["messages__user-icon"]}><img src="/////////////////////" alt="icon" /></div>
                    <h2 className={css["messages__user-name"]}>{"//////////////////////"}</h2>
                    <div className={css["messages__dots"]}><img src="./img/untouched-three-dots.svg" alt="icon" /></div>
                </div>
            </div>
        </div>
    );
}

export default MainPage;