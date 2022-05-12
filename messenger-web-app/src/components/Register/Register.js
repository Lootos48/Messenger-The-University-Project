import css from "./Register.module.css";

const Register = () => {
    return (
        <div className={css["register"]}>
            <div className={css["register__messenger-icon"]}><img src="./img/messenger-icon.svg" alt="icon" /></div>
            <h1 className={css["register__title"]}>Register your new account</h1>
            <form className={css["register__form"]}>
                <input required className={css["register__input"]} id="register__email-input" type="email" placeholder="Enter your email" />
                <input required className={css["register__input"]} id="register__password-input" type="password" placeholder="Enter your password" />
                <button className={css["register__submit-button"]} type="submit">Register</button>
            </form>
        </div>
    );
}

window.addEventListener("click", (event) => {
    const target = event.target.closest(".register__submit-button");
    if (target) {
        event.preventDefault();

        const registerQueryObj = {
            query: "register",
            email: document.body.querySelector("register__email-input").value,
            password: document.body.querySelector("register__password-input").value,
        }

        fetch("http://localhost:4000", {
            method: "post",
            body: JSON.stringify(registerQueryObj),
            headers: {
                "Content-Type": "application/json"
            }
        })
            .then(response => response.JSON())
            .then(response => {
                if (response.registerAllowed) {
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

export default Register;