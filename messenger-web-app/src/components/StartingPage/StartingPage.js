import { Link } from "react-router-dom";
import css from "./StartingPage.module.css";

const StartingPage = () => {
    if (sessionStorage.getItem("userID") != null) {
        window.location.assign("http://localhost:3000/mainPage");
    }

    return (
        <div className={css["starting-page"]}>
            <div className={css["starting-page__icon"]}><img src="./img/messenger-icon.svg" alt="icon" /></div>
            <h1 className={css["starting-page__title"]}>You may login or register</h1>
            <nav className={css["starting-page__links"]}>
                <Link className={css["starting-page__link"]} to="/login">Login</Link>
                <Link className={css["starting-page__link"]} to="/register">Register</Link>
            </nav>
        </div >
    );
}

export default StartingPage;