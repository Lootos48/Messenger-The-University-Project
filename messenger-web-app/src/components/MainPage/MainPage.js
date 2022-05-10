import { Link } from "react-router-dom";
import css from "./MainPage.module.css";

const MainPage = () => {
    return (
        <div className={css["main-page"]}>
            <div className={css["main-page__icon"]}><img src="./img/messenger-icon.svg" alt="icon" /></div>
            <h1 className={css["main-page__title"]}>You may login or register</h1>
            <nav className={css["main-page__links"]}>
                <Link className={css["main-page__link"]} to="/login">Login</Link>
                <Link className={css["main-page__link"]} to="/register">Register</Link>
            </nav>
        </div >
    );
}

export default MainPage;