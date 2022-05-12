import css from "./Login.module.css";

const Login = () => {
    return (
        <div className={css["login"]}>
            <div className={css["login__messenger-icon"]}><img src="./img/messenger-icon.svg" alt="icon" /></div>
            <h1 className={css["login__title"]}>Login to your account</h1>
            <form className={css["login__form"]}>
                <input required className={css["login__input"]} id="login__email-input" type="email" placeholder="Enter your email" />
                <input required className={css["login__input"]} id="login__password-input" type="password" placeholder="Enter your password" />
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
            email: document.body.querySelector("login__email-input").value,
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
                if (response.loginAllowed) {
                    // console.log("Logined: " + true);
                    sessionStorage.setItem("logined", true);
                    sessionStorage.setItem("userID", response.userID);
                    sessionStorage.setItem("userName", response.userName);
                    window.location.assign("http://localhost:3000/mainPage");
                }
                else {
                    // console.log("Logined: " + false);
                    sessionStorage.setItem("logined", false);
                    alert("Reject reason: " + response.rejectReason);
                }
            })
            .catch(error => {
                // console.log("Logined: " + false);
                sessionStorage.setItem("logined", false);
                alert(error);
            })
    }
});

export default Login;