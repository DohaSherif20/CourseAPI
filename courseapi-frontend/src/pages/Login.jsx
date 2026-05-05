import { useState } from "react";
import { useNavigate, Link } from "react-router-dom";
import api from "../services/api";

export default function Login() {
  const navigate = useNavigate();

  const [formData, setFormData] = useState({
    email: "",
    password: ""
  });

  const [message, setMessage] = useState("");

  const handleChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value
    });
  };

  const handleLogin = async (e) => {
    e.preventDefault();

    try {
      const res = await api.post("/Auth/login", formData);

      // 🔥 خدنا اليوزر من login مباشرة
      const user = res.data;

      console.log("Logged user:", user);

      localStorage.setItem("isLoggedIn", "true");
      localStorage.setItem("role", user.role);

      navigate("/dashboard");
    } catch (error) {
      setMessage("Invalid email or password");
      console.error(error);
    }
  };

  return (
    <div>
      <h1>Login</h1>

      <form onSubmit={handleLogin}>
        <div>
          <label>Email</label>
          <input
            type="email"
            name="email"
            value={formData.email}
            onChange={handleChange}
            required
          />
        </div>

        <div>
          <label>Password</label>
          <input
            type="password"
            name="password"
            value={formData.password}
            onChange={handleChange}
            required
          />
        </div>

        <button type="submit">Login</button>
      </form>

      <p>{message}</p>

      <p>
        Don't have an account? <Link to="/register">Register</Link>
      </p>
    </div>
  );
}