import css from "./Login.module.css";

const Login = () => {
    if (sessionStorage.getItem("userID") != null) {
        window.location.assign("http://localhost:3000/mainPage");
    }

    return (
        <div className={css["login"]}>
            <div className={css["login__messenger-icon"]}><img src="./img/messenger-icon.svg" alt="icon" /></div>
            <h1 className={css["login__title"]}>Login to your account</h1>
            <form className={css["login__form"]}>
                <input required className={css["login__input"]} id="login__user-name-input" type="text" maxLength="20" placeholder="Enter your user name" />
                <input required className={css["login__input"]} id="login__password-input" type="password" maxLength="30" placeholder="Enter your password" />
                <button className={css["login__submit-button"]} type="submit">Login</button>
            </form>
        </div>
    );
}

window.addEventListener("click", (event) => {
    const target = event.target.closest(".login__submit-button");
    if (target) {
        event.preventDefault();

        const loginQueryObj = {
            query: "login",
            email: document.body.querySelector("login__user-name-input").value,
            password: document.body.querySelector("login__password-input").value,
        }

        fetch("http://localhost:4000", {
            method: "post",
            body: JSON.stringify(loginQueryObj),
            headers: {
                "Content-Type": "application/json"
            }
        })
            .then(response => response.JSON())
            .then(response => {
                if (response.userID != null) {
                    sessionStorage.setItem("userID", response.userID);
                    // sessionStorage.setItem("userName", response.userName);
                    window.location.assign("http://localhost:3000/mainPage");
                }
                else {
                    alert("Reject reason: " + response.rejectReason);
                }
            })
            .catch(error => {
                alert(error);
            })
    }
});

export default Login;