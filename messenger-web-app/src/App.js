import './App.css';
import Login from "./components/Login/Login.js";
import Register from "./components/Register/Register.js";
import StartingPage from "./components/StartingPage/StartingPage.js";
import MainPage from "./components/MainPage/MainPage.js";
import { BrowserRouter, Routes, Route } from "react-router-dom";

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
